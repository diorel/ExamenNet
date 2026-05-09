export interface CreatePaymentRequest {
  requesterName: string;
  amount: number;
  currency: string;
  description?: string | null;
}
