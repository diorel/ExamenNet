import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { PaymentRequestService } from '../../services/payment-request.service';
import { CreatePaymentRequest } from '../../../../core/models/create-payment-request.model';

@Component({
  selector: 'app-payment-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './payment-form.component.html',
  styleUrls: ['./payment-form.component.css']
})
export class PaymentFormComponent {
  @Output() paymentCreated = new EventEmitter<void>();

  form: FormGroup;
  isLoading = false;
  successMessage = '';
  errorMessage = '';

  constructor(private fb: FormBuilder, private service: PaymentRequestService) {
    this.form = this.fb.group({
      requesterName: ['', [Validators.required, Validators.maxLength(100)]],
      amount: [null, [Validators.required, Validators.min(0.01)]],
      currency: ['', [Validators.required]],
      description: ['']
    });
  }

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isLoading = true;
    this.successMessage = '';
    this.errorMessage = '';

    const dto: CreatePaymentRequest = {
      requesterName: this.form.value.requesterName,
      amount: this.form.value.amount,
      currency: this.form.value.currency,
      description: this.form.value.description || null
    };

    this.service.create(dto).subscribe({
      next: () => {
        this.form.reset();
        this.successMessage = 'Solicitud creada exitosamente';
        this.paymentCreated.emit();
        this.isLoading = false;
      },
      error: (err: HttpErrorResponse) => {
        if (err.error && typeof err.error === 'string') {
          this.errorMessage = err.error;
        } else if (err.error && err.error.message) {
          this.errorMessage = err.error.message;
        } else if (err.error && err.error.title) {
          this.errorMessage = err.error.title;
        } else {
          this.errorMessage = `Error ${err.status}: ${err.statusText}`;
        }
        this.isLoading = false;
      }
    });
  }
}
