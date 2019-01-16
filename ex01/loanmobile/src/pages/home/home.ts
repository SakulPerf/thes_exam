import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { HttpClient } from '@angular/common/http';
import { InterestInfo } from '../../models/interestInfo';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {

  private svRate: number;
  private rate: number;

  constructor(public navCtrl: NavController, public http: HttpClient) {
  }

  ionViewDidLoad() {
    this.getServerRate();
  }

  SetRate() {
    this.http.post("https://localhost:5000/api/loan",
    {
        rate: this.rate,
    }).subscribe(
        it => {
          this.getServerRate();
        }, 
        error => {
            console.log(error);
        });
  }

  private getServerRate(){
    this.http.get<number>("https://localhost:5000/api/loan").subscribe(
      it => {
        this.svRate = it;
      },
      error => {
        console.log(error);
      });
  }

}
