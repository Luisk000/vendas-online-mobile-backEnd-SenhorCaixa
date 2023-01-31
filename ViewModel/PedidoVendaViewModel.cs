//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace SC.AplicativoSenhorCaixa.ViewModel
//{
//    public class PedidoVendaViewModel
//    {
//        public string NM_FANTASIA { get; set; }
//        public string NM_PRODUTO { get; set; }
//        public int QT_PRODUTO { get; set; }
//        public DateTime DT_PEDIDO { get; set; }
//        public DateTime? DT_ENTREGA { get; set; }


//    }
//}



//List<PedidoVendaViewModel> pedidos = new List<PedidoVendaViewModel>();

//foreach (Cliente cliente in clientes)
//{
//    List<PedidoVendaCab> pedidosCab = await _context.PEDIDOVENDACAB
//        .Where(p => p.CD_ORIGEM == 2 &&
//        p.CD_CLIENTE == cliente.CD_CLIENTE).ToListAsync();

//    foreach (PedidoVendaCab pedidoCab in pedidosCab)
//    {
//        List<PedidoVendaDet> pedidosDet = await _context.PEDIDOVENDADET
//            .Where(p => p.CD_PEDIDO == pedidoCab.CD_PEDIDO).ToListAsync();

//        foreach (PedidoVendaDet pedidoDet in pedidosDet)
//        {
//            PedidoVendaViewModel pedido = new PedidoVendaViewModel();

//            pedido.QT_PRODUTO = (int)pedidoDet.QT_PEDIDA;
//            pedido.DT_PEDIDO = pedidoCab.DT_PEDIDO;
//            pedido.DT_ENTREGA = pedidoCab.DT_ENTREGA;

//            pedido.NM_FANTASIA = await _context.CLIENTE
//                .Where(c => c.CD_CLIENTE == cliente.CD_CLIENTE)
//                .Select(c => c.NM_FANTASIA).SingleOrDefaultAsync();

//            pedido.NM_PRODUTO = await _context.PRODUTO
//                .Where(p => p.CD_PRODUTO == pedidoDet.CD_PRODUTO)
//                .Select(p => p.NM_PRODUTO).SingleOrDefaultAsync();

//            pedidos.Add(pedido);
//        }
//    }
//}

