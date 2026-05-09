import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { PaymentRequest } from '../../../core/models/payment-request.model';
import { CreatePaymentRequest } from '../../../core/models/create-payment-request.model';

@Injectable({ providedIn: 'root' })
export class PaymentRequestService {
  private readonly baseUrl = `${environment.apiUrl}/payment-requests`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<PaymentRequest[]> {
    return this.http.get<PaymentRequest[]>(this.baseUrl);
  }

  create(dto: CreatePaymentRequest): Observable<PaymentRequest> {
    return this.http.post<PaymentRequest>(this.baseUrl, dto);
  }
}
