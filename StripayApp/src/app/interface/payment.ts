export interface Payment {
  paymentId: number;
  chargeId: string;
  amount: number;
  stripeCustomerId: string;
  cardName: string;
  cardExpiryMonth: number;
  cardExpiryYear: number;
  cardLast4Digits: string;
}
