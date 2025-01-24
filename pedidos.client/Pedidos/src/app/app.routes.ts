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
