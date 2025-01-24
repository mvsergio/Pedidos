import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-order-form',
  standalone: true,
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.css'],
  imports: [
    CommonModule,
    ReactiveFormsModule,
  ],
})
export class OrderFormComponent {
  orderForm: FormGroup;

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.orderForm = this.fb.group({
      customerId: [0, [Validators.required]],
      orderDate: [new Date().toISOString(), [Validators.required]],
      totalAmount: [0, [Validators.required, Validators.min(0)]],
      status: ['Pending', Validators.required],
      items: this.fb.array([]),
    });
  }

  get items(): FormArray {
    return this.orderForm.get('items') as FormArray;
  }

  addItem() {
    this.items.push(
      this.fb.group({
        productId: [0, [Validators.required]],
        productName: ['', [Validators.required]],
        quantity: [0, [Validators.required, Validators.min(1)]],
        unitPrice: [0, [Validators.required, Validators.min(0)]],
        totalPrice: [0],
      })
    );
  }

  removeItem(index: number) {
    this.items.removeAt(index);
  }

  calculateTotalAmount() {
    const totalAmount = this.items.value.reduce((sum: number, item: any) => {
      return sum + item.quantity * item.unitPrice;
    }, 0);
    this.orderForm.patchValue({ totalAmount });
  }

  submitForm() {
    if (this.orderForm.valid) {
      const formData = this.orderForm.value;

      formData.items.forEach((item: any) => {
        item.totalPrice = item.quantity * item.unitPrice;
      });

      this.http.post('https://localhost:7146/api/orders', formData).subscribe({
        next: () => alert('Pedido enviado com sucesso!'),
        error: (err) => alert('Erro ao enviar o pedido: ' + err.message),
      });
    } else {
      alert('Por favor, preencha todos os campos obrigatórios.');
    }
  }
}
