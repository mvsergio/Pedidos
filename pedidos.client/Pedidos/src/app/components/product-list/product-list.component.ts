import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="container mt-4">
      <h2>Lista de Produtos</h2>
      <table class="table table-bordered table-hover">
        <thead class="table-light">
          <tr>
            <th>ID</th>
            <th>Nome</th>
            <th>Preço</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          <tr *ngFor="let product of products">
            <td>{{ product.id }}</td>
            <td>{{ product.name }}</td>
            <td>{{ product.price | currency: 'BRL' }}</td>
            <td>
              <button
                class="btn btn-danger btn-sm"
                (click)="deleteProduct(product.id)"
              >
                Excluir
              </button>
            </td>
          </tr>
        </tbody>
      </table>
      <div *ngIf="products.length === 0" class="text-center mt-3">
        <p>Nenhum produto encontrado.</p>
      </div>
    </div>
  `,
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe({
      next: (data) => {
        this.products = data;
      },
      error: (err) => {
        alert('Erro ao buscar produtos: ' + err.message);
      },
    });
  }

  deleteProduct(id: number): void {
    if (confirm('Tem certeza que deseja excluir este produto?')) {
      this.productService.deleteProduct(id).subscribe({
        next: () => {
          alert('Produto excluído com sucesso!');
          this.loadProducts(); // Atualiza a lista após a exclusão
        },
        error: (err) => {
          alert('Erro ao excluir o produto: ' + err.message);
        },
      });
    }
  }
}
