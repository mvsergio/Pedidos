export class Order {
  id: number;
  customerId: number;
  orderDate: string;
  totalAmount: number;
  status: string;
  items: OrderItem[];

  constructor(data: Partial<Order>) {
    this.id = data.id || 0;
    this.customerId = data.customerId || 0;
    this.orderDate = data.orderDate || '';
    this.totalAmount = data.totalAmount || 0;
    this.status = data.status || 'Pending';
    this.items = data.items?.map(item => new OrderItem(item)) || [];
  }
}

export class OrderItem {
  id: number;
  orderId: number;
  productId: number;
  productName: string;
  quantity: number;
  unitPrice: number;
  totalPrice: number;

  constructor(data: Partial<OrderItem>) {
    this.id = data.id || 0;
    this.orderId = data.orderId || 0;
    this.productId = data.productId || 0;
    this.productName = data.productName || '';
    this.quantity = data.quantity || 0;
    this.unitPrice = data.unitPrice || 0;
    this.totalPrice = data.totalPrice || 0;
  }
}
