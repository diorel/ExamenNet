export interface PaymentRequest {
  id: number;
  requesterName: string;
  amount: number;
  currency: string;
  createdAt: string;
  description?: string | null;
}
