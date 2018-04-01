import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
//import { SpinnerService } from '@chevtek/angular-spinners';


@Component({
    selector: 'home',
    templateUrl: './home.component.html'
    
})
export class HomeComponent {
    title: string; 
    
    userid: string;
    result: string;
    //protected spinnerService: SpinnerService, 
    constructor(public http: Http, @Inject('BASE_URL') public baseUrl: string) {
        this.http.get(this.baseUrl + 'api/User').subscribe(res => { this.title = "Hello, " + res.text() });
    }

   

    public GetUser(userid: string) {
        //this.spinnerService.show('mySpinner');
        if (userid) {
           
            this.http.get(this.baseUrl + 'api/User/' + userid).subscribe(res => {this.result = res.text() });
          }
        //this.spinnerService.hide('mySpinner');

    }
}




export class AppModule { }



