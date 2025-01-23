import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CustomerListComponent } from './components/customer-list/customer-list.component';
import { OrderListComponent } from "./components/order-list/order-list.component";

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  imports: [RouterOutlet, CustomerListComponent, OrderListComponent],
})
export class AppComponent {
  title = 'Pedidos';
}
