
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';   
import { AppComponent } from './app.component';
import { NgChartsModule } from 'ng2-charts';               

@NgModule({
  declarations: [AppComponent],                           
  imports: [
    BrowserModule, 
    HttpClientModule, 
    NgChartsModule                                         
  ],
  bootstrap: [AppComponent]                               
})
export class AppModule {}
