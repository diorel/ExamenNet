import { Component, ViewChild } from '@angular/core';
import { PaymentFormComponent } from './features/payment-requests/components/payment-form/payment-form.component';
import { PaymentListComponent } from './features/payment-requests/components/payment-list/payment-list.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [PaymentFormComponent, PaymentListComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  @ViewChild(PaymentListComponent) paymentList!: PaymentListComponent;

  onPaymentCreated(): void {
    this.paymentList.reload();
  }
}
