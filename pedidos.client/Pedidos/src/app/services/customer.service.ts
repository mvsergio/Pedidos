// src/app/services/customer.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Customer {
  id: number;
  name: string;
  email: string;
  phone: string;
}

@Injectable({
  providedIn: 'root', // O serviço estará disponível globalmente
})
export class CustomerService {
  private apiUrl = 'https://localhost:7146/api/customers'; // URL da API

  constructor(private http: HttpClient) {}

  // Método para buscar os clientes
  getCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.apiUrl);
  }
}
