export class Product {
  id: number;
  name: string;
  price: number;

  constructor(data: Partial<Product> = {}) {
    this.id = data.id || 0;
    this.name = data.name || '';
    this.price = data.price || 0;
  }
}
