import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { CardInfo } from '../interface/CardInfo';

@Injectable({ providedIn: 'root' })
export class PaymentApi {
  baseUrl = 'http://localhost:5151/api/v1.0/payments';

  constructor(
    private readonly http: HttpClient
  ) {}

  postPayment(
    email: string,
    amount: number,
    paymentMethodId: string | null,
    cardInfo: CardInfo | null
  ): Observable<any> {
    return this.http.post<any>(this.baseUrl, {
      customerEmail: email,
      amount: amount,
      paymentMethodId: paymentMethodId,
      cardName: cardInfo?.cardName,
      cardExpiryMonth: cardInfo?.cardExpiryMonth,
      cardExpiryYear: cardInfo?.cardExpiryYear,
      cardLast4Digits: cardInfo?.lastDigits
    });
  }

  getPayments(): Observable<any> {
    return this.http.get<any>(this.baseUrl);
  }
}
