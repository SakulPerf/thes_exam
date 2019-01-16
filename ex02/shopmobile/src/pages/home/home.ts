import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { HttpClient } from '@angular/common/http';
import { ProductInfo } from '../../models/productInfo';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {

  private name: string;
  private price: number;
  private products: ProductInfo[] = [];

  constructor(public navCtrl: NavController, public http: HttpClient) {
  }

  ionViewDidLoad() {
    this.getAllProducts();
  }

  AddNewProduct() {
    this.http.post<ProductInfo[]>("https://localhost:44373/api/shop",
      {
        price: this.price,
        name: this.name
      }).subscribe(
        it => {
          this.products = it;
        },
        error => {
          console.log(error);
        });
  }

  private getAllProducts(){
    this.http.get<ProductInfo[]>("https://localhost:44373/api/shop").subscribe(
      it => {
        this.products = it;
      },
      error => {
        console.log(error);
      });
  }

}
