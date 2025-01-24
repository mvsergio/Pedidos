import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [RouterModule],
  template: `
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
      <div class="container-fluid">
        <a class="navbar-brand" href="#">Pedidos</a>
        <button
          class="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarNav"
          aria-controls="navbarNav"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
          <ul class="navbar-nav">
            <li class="nav-item">
              <a class="nav-link" routerLink="/customers" routerLinkActive="active">Clientes</a>
            </li>
            <li class="nav-item dropdown">
              <a
                class="nav-link dropdown-toggle"
                href="#"
                id="ordersDropdown"
                role="button"
                data-bs-toggle="dropdown"
                aria-expanded="false"
              >
                Produtos
              </a>
              <ul class="dropdown-menu" aria-labelledby="productsDropdown">
                <li>
                  <a class="dropdown-item" routerLink="/products" routerLinkActive="active">Lista de Produtos</a>
                </li>
                <li>
                  <a class="dropdown-item" routerLink="/products/new" routerLinkActive="active">Novo Produto</a>
                </li>
              </ul>
            </li>
            <li class="nav-item dropdown">
              <a
                class="nav-link dropdown-toggle"
                href="#"
                id="ordersDropdown"
                role="button"
                data-bs-toggle="dropdown"
                aria-expanded="false"
              >
                Vendas
              </a>
              <ul class="dropdown-menu" aria-labelledby="ordersDropdown">
                <li>
                  <a class="dropdown-item" routerLink="/orders" routerLinkActive="active">Lista de Vendas</a>
                </li>
                <li>
                  <a class="dropdown-item" routerLink="/orders/new" routerLinkActive="active">Novo Pedido</a>
                </li>
              </ul>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  `,
})
export class MenuComponent {}
