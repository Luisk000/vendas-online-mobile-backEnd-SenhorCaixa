using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SC.AplicativoSenhorCaixa.Data;
using SC.AplicativoSenhorCaixa.Models;
using SC.AplicativoSenhorCaixa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.AplicativoSenhorCaixa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly FirebirdContext _context;
        private readonly CodPedidoContext _codContext;

        public ProdutoController(FirebirdContext context, CodPedidoContext codContext)
        {
            _context = context;
            _codContext = codContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetProduto()
        {
            try
            {
                var result = await _context.PRODUTO.ToListAsync();

                return Json(result);
            }
            catch
            {
                return BadRequest("Banco de dados falhou");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProdutoById(int id)
        {
            try
            {
                var result = await _context.CLIENTE
                    .Where(cli => cli.CD_CLIENTE == id)
                    .Join(_context.TABELAPRECOCLIENTE,
                    cli => cli.CD_CLIENTE,
                    tab => tab.CD_CLIENTE,
                    (cli, tab) => new { cli, tab })
                    .Join(_context.PRODUTO,
                    tab => tab.tab.CD_PRODUTO,
                    prod => prod.CD_PRODUTO,
                    (tab, prod) => new {
                        cD_CLIENTE = tab.cli.CD_CLIENTE,
                        nM_FANTASIA = tab.cli.NM_FANTASIA,
                        cD_PRODUTO = prod.CD_PRODUTO,
                        nM_PRODUTO = prod.NM_PRODUTO,
                        qT_QTDENAEMBALAGEM = prod.QT_QTDENAEMBALAGEM
                    }).ToListAsync();

                return Json(result);
            }
            catch(Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpGet]
        [Route("[action]/{userCpf}")]
        public async Task<IActionResult> GetCarrinho(string userCpf)
        {
            try
            {
                List<PedidoCarrinhoViewModel> pedidosCarrinho = await GetListaCarrinho(userCpf);
                return Json(pedidosCarrinho);
            }
            catch (Exception ex)
            {
                return Json("Erro: " + ex);
            } 
        }

        private async Task<List<PedidoCarrinhoViewModel>> GetListaCarrinho(string userCpf)
        {
            int[] lojasNoBanco = await _context.CARRINHO.Select(c => c.CD_CLIENTE).ToArrayAsync();
            int[] lojas = lojasNoBanco.Distinct().ToArray();
            List<PedidoCarrinhoViewModel> pedidosCarrinho = new List<PedidoCarrinhoViewModel>();

            foreach (int loja in lojas)
            {
                PedidoCarrinhoViewModel pedidoCarrinho = new PedidoCarrinhoViewModel();
                pedidoCarrinho.CD_CLIENTE = loja;

                pedidoCarrinho.NM_FANTASIA = _context.CLIENTE
                    .Where(c => c.CD_CLIENTE == loja)
                    .Select(c => c.NM_FANTASIA).FirstOrDefault();

                pedidoCarrinho.produtosCarrinho = await _context.CARRINHO
                    .Where(c => c.CD_CLIENTE == loja && c.NM_CPFUSUARIO == userCpf)
                    .ToArrayAsync();

                pedidosCarrinho.Add(pedidoCarrinho);
            }
            return pedidosCarrinho;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AdicionarCarrinho([FromBody] Carrinho produtoCarrinho)
        {
            try
            {
                await _context.CARRINHO.AddAsync(produtoCarrinho);
                await _context.SaveChangesAsync();
                return Json("Sucesso");
            }
            catch (Exception ex)
            {
                return Json("Erro: " + ex);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RemoverCarrinho([FromBody] Carrinho produtoCarrinho)
        {
            try
            {
                Carrinho[] pedidos = await _context.CARRINHO
                         .Where(c => c.CD_CLIENTE == produtoCarrinho.CD_CLIENTE).ToArrayAsync();

                _context.CARRINHO.RemoveRange(pedidos);
                await _context.SaveChangesAsync();

                return Json("Sucesso");
            }
            catch (Exception ex)
            {
                return Json("Erro: " + ex);
            }           
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ConfirmarPedidoCab([FromBody] Carrinho pedido)
        {
            try
            {
                CodPedido cod = new CodPedido();
                await _codContext.CODPEDIDO.AddAsync(cod);
                await _codContext.SaveChangesAsync();

                PedidoVendaCab pedidoCab = new PedidoVendaCab();
                pedidoCab.CD_CLIENTE = pedido.CD_CLIENTE;
                //pedidoCab.CD_PEDIDO = cod.CD_PEDIDO;
                pedidoCab.CD_PEDIDO = 90025;
                await _context.PEDIDOVENDACAB.AddAsync(pedidoCab);
                await _context.SaveChangesAsync();

                return Json(pedidoCab.CD_PEDIDO);
            }
            catch (Exception ex)
            {
                return Json("Erro: " + ex);
            }         
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ConfirmarPedidosDet(Carrinho[] produtos)
        {
            try
            {
                foreach (Carrinho produto in produtos)
                {
                    PedidoVendaDet pedidoDet = new PedidoVendaDet();
                    pedidoDet.QT_PEDIDA = produto.QT_PRODUTO;
                    pedidoDet.CD_PRODUTO = produto.CD_PRODUTO;
                    pedidoDet.CD_PEDIDO = (int)produto.CD_PEDIDO;

                    await _context.PEDIDOVENDADET.AddAsync(pedidoDet);
                }
                await _context.SaveChangesAsync();
                return Json("Sucesso");
            }
            catch (Exception ex)
            {
                return Json("Erro: " + ex);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetPedidosPendentes([FromBody] Cliente cliente)
        {
            try
            {
                var pedidos = await _context.CLIENTE
                .Where(c => c.CD_REDELOJA == cliente.CD_REDELOJA)
                .Join(_context.PEDIDOVENDACAB,
                    cli => cli.CD_CLIENTE,
                    pcab => pcab.CD_CLIENTE,
                    (cli, pcab) => new { cli, pcab })
                .Join(_context.PEDIDOVENDADET,
                    pcab => pcab.pcab.CD_PEDIDO,
                    pdet => pdet.CD_PEDIDO,
                    (pcab, pdet) => new { pcab, pdet })
                .Join(_context.PRODUTO,
                    pdet => pdet.pdet.CD_PRODUTO,
                    prod => prod.CD_PRODUTO,
                    (pdet, prod) => new {
                        cD_CLIENTE = pdet.pcab.cli.CD_CLIENTE,
                        nM_FANTASIA = pdet.pcab.cli.NM_FANTASIA,                
                        dT_PEDIDO = pdet.pcab.pcab.DT_PEDIDO,
                        dT_ENTREGA = pdet.pcab.pcab.DT_ENTREGA,
                        qT_PRODUTO = pdet.pdet.QT_PEDIDA,                     
                        cD_PEDIDO = pdet.pdet.CD_PEDIDO,
                        cD_ITEM = pdet.pdet.CD_ITEM,
                        nM_PRODUTO = prod.NM_PRODUTO,
                    }).ToListAsync();


                return Json(pedidos);
            }
            catch (Exception ex)
            {
                return Json("Erro: " + ex);
            }
        }
    }
}
