import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { HttpClient } from '@angular/common/http';
import { ProductInfo } from '../../models/productInfo';
import { CartInfo } from '../../models/cartInfo';

@Component({
  selector: 'page-about',
  templateUrl: 'about.html'
})
export class AboutPage {

  private productId: number;
  private productAmount: number;
  private cartInfo: CartInfo = new CartInfo();

  constructor(public navCtrl: NavController, public http: HttpClient) {
    this.cartInfo.products = [];
  }

  ionViewDidLoad() {
    this.http.get<CartInfo>("https://localhost:44373/api/cart").subscribe(
      it => {
        this.cartInfo = it;
      },
      error => {
        console.log(error);
      });
  }

  AddProductToCart() {
    this.http.post<CartInfo>("https://localhost:44373/api/cart",
      {
        productId: this.productId,
        amount: this.productAmount
      }).subscribe(
        it => {
          this.cartInfo = it;
        },
        error => {
          console.log(error);
        });
  }

}
