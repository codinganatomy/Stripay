import { Component, OnInit, ViewChild } from '@angular/core';
import { StripeCardElementOptions, StripeElementsOptions } from '@stripe/stripe-js';
import { StripeCardCvcComponent, StripeCardExpiryComponent, StripeCardGroupDirective, StripeCardNumberComponent, StripeService } from 'ngx-stripe';
import { CardInfo } from './interface/CardInfo';
import { firstValueFrom } from 'rxjs';
import { PaymentApi } from './api/payment.api';
import { FormsModule } from '@angular/forms';
import { NgFor } from '@angular/common';
import { Payment } from './interface/payment';

@Component({
  selector: 'app-root',
  imports: [
    StripeCardNumberComponent,
    StripeCardCvcComponent,
    StripeCardExpiryComponent,
    StripeCardGroupDirective,
    FormsModule,
    NgFor
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [PaymentApi]
})
export class AppComponent implements OnInit{
  title = 'StripayApp';

  payments: Payment[] = [];

  @ViewChild(StripeCardNumberComponent) card!: StripeCardNumberComponent;
  cardHolderName: string = '';
  amount: number = 0;
  email: string = '';

  cardOptions: StripeCardElementOptions = {
    style: {
      base: {
        iconColor: '#666EE8',
        color: '#31325F',
        fontWeight: '500',
        fontFamily:
          'ui-sans-serif',
        fontSize: '14px',
        '::placeholder': {
          color: '#CFD7E0'
        }
      }
    }
  };

  elementsOptions: StripeElementsOptions = {
    locale: 'en'
  };


  constructor(
    private readonly stripeService: StripeService,
    private readonly paymentApi: PaymentApi,
  ) {}

  ngOnInit() {
    this.getPayments();
  }

  async pay(): Promise<void> {
      this.stripeService
        .createPaymentMethod({
          type: 'card',
          card: this.card.element,
          billing_details: {
            name: this.cardHolderName
          }
        })
        .subscribe(async (result) => {
          if (result.error) {
            console.log(result.error.message);
          } else {
            if (result.paymentMethod) {
              const cardInfo: CardInfo = {
                cardName: this.cardHolderName,
                cardExpiryMonth: result.paymentMethod.card!.exp_month,
                cardExpiryYear: result.paymentMethod.card!.exp_year,
                lastDigits: result.paymentMethod.card!.last4
              };

              await this.postPayment(
                this.email,
                this.amount,
                result.paymentMethod.id,
                cardInfo
              );
            } else {
              console.log('Invalid payment method');
            }
          }
        });
  }

  async postPayment(
    email: string,
    amount: number,
    paymentMethodId: string | null,
    cardInfo: CardInfo | null
  ): Promise<void> {
    try {
      await firstValueFrom(
        this.paymentApi
          .postPayment(
            email,
            amount,
            paymentMethodId,
            cardInfo
          )
      );
      alert("Payment successfull!");
      this.getPayments();
    } catch (error) {
      alert(error);
    }
  }

  getPayments(): void {
    this.paymentApi
      .getPayments()
      .subscribe((data) => {
        this.payments = data;
      });
  }
}

