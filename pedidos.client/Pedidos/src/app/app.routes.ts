import { Routes } from '@angular/router';

export const appRoutes: Routes = [
  {
    path: 'customers',
    loadComponent: () => import('./components/customer-list/customer-list.component').then(c => c.CustomerListComponent)
  },
  {
    path: 'products',
    children: [
      { path: '',    loadComponent: () => import('./components/product-list/product-list.component').then(c => c.ProductListComponent) },
      { path: 'new', loadComponent: () => import('./components/product-form/product-form.component').then(c => c.ProductFormComponent) },
    ],
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
