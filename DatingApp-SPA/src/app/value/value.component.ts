import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-value',
  templateUrl: './value.component.html',
  styleUrls: ['./value.component.css']
})
export class ValueComponent implements OnInit {
  values: any;
  // inject HttpClient de su dung
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getValue();
  }

  getValue() {
    // subscribe de nhan response tra ve tu server, neu co result tra ve, thuc hien gan gia tri vao "values"
    // , neu phat sinh error thi log ra error
    this.http.get('http://localhost:5000/api/values').subscribe(response => {
      this.values = response;
    }, error => {
      console.log(error);
    });
  }

}
