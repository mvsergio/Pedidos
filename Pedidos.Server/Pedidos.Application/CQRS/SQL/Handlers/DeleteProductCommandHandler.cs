using MediatR;
using Pedidos.Application.CQRS.Notification;
using Pedidos.Application.CQRS.SQL.Commands;
using Pedidos.Domain.Interfaces;

namespace Pedidos.Application.CQRS.SQL.Handlers
{
	public class DeleteProductCommandHandler(
		IProductsRepository productRepository, 
		IOrdersRepository ordersRepository, 
		IMediator mediator) : IRequestHandler<DeleteProductCommand, bool>
	{
		public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
		{
			var product = await productRepository.GetByIdAsync(request.Id);
			if (product == null)
				return false;

			var productInOrder = ordersRepository.FilterAllAsync().Any(o => o.Items.Select(i => i.ProductId).Contains(request.Id));
			if (productInOrder)
				throw new Exception("Não foi possível excluir o produto, ele já está vinculado a um pedido.");

			await productRepository.DeleteAsync(product.Id);

			await mediator.Publish(new ProductDeletedNotification
			{
				ProductId = product.Id
			}, cancellationToken);

			return true;
		}
	}
}