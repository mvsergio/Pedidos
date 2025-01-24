/*
import { Routes } from '@angular/router';

export const appRoutes: Routes = [
  {
    path: 'customers',
    loadComponent: () => import('./components/customer-list/customer-list.component').then(c => c.CustomerListComponent)
  },
  {
    path: 'orders',
    loadComponent: () => import('./components/order-list/order-list.component').then(c => c.OrderListComponent)
  },
  { path: '',
    redirectTo: '/customers',
    pathMatch: 'full' },
  { path: '**',
    redirectTo: '/customers'
  }
];
*/
import { Routes } from '@angular/router';
import { OrderFormComponent } from './components/order-form/order-form.component';

export const appRoutes: Routes = [
  {
    path: 'customers',
    loadComponent: () => import('./components/customer-list/customer-list.component').then(c => c.CustomerListComponent)
  },
  {
    path: 'orders',
    children: [
      { path: '',    loadComponent: () => import('./components/order-list/order-list.component').then(c => c.OrderListComponent) },
      { path: 'new', loadComponent: () => import('./components/order-form/order-form.component').then(c => c.OrderFormComponent) },
    ],
  },
  { path: '',
    redirectTo: '/customers',
    pathMatch: 'full' },
  { path: '**',
    redirectTo: '/customers'
  }
];

/*

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter([
      { path: '', component: MenuComponent },
      {
        path: 'orders',
        children: [
          { path: '', loadComponent: () => import('./app/orders/orders.component').then(m => m.OrdersComponent) }, // Lista de vendas
          { path: 'new', component: OrderFormComponent }, // Novo Pedido
        ],
      },
      { path: 'customers', loadComponent: () => import('./app/customers/customers.component').then(m => m.CustomersComponent) },
    ]),
    provideHttpClient(),
  ],
}).catch(err => console.error(err));

*/
