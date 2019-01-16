import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { HttpClient } from '@angular/common/http';
import { InterestInfo } from '../../models/interestInfo';

@Component({
  selector: 'page-about',
  templateUrl: 'about.html'
})
export class AboutPage {

  private volume: number;
  private years: number;
  private interests: InterestInfo[] = [];

  constructor(public navCtrl: NavController, public http: HttpClient) {
  }

  Calculate() {
    this.http.post<InterestInfo[]>("https://localhost:5000/api/loan/calculate",
    {
        volume: this.volume,
        years: this.years
    }).subscribe(
        it => {
          this.interests = it;
        }, 
        error => {
            console.log(error);
        });
  }

}
