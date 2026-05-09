import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { PaymentRequestService } from '../../services/payment-request.service';
import { PaymentRequest } from '../../../../core/models/payment-request.model';

@Component({
  selector: 'app-payment-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './payment-list.component.html',
  styleUrls: ['./payment-list.component.css']
})
export class PaymentListComponent implements OnInit {
  payments: PaymentRequest[] = [];
  isLoading = false;
  errorMessage = '';

  constructor(private service: PaymentRequestService) {}

  ngOnInit(): void {
    this.loadPayments();
  }

  private loadPayments(): void {
    this.isLoading = true;
    this.service.getAll().subscribe({
      next: (data: PaymentRequest[]) => {
        this.payments = data;
        this.isLoading = false;
      },
      error: (err: HttpErrorResponse) => {
        if (err.error && typeof err.error === 'string') {
          this.errorMessage = err.error;
        } else if (err.error && err.error.message) {
          this.errorMessage = err.error.message;
        } else {
          this.errorMessage = `Error ${err.status}: No se pudo cargar la lista de solicitudes.`;
        }
        this.isLoading = false;
      }
    });
  }

  reload(): void {
    this.errorMessage = '';
    this.loadPayments();
  }
}
