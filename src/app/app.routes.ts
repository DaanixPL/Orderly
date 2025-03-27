import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './page/home/home.component';
import { RestaurantListComponent } from './page/restaurant-list/restaurant-list.component';
import { ContactComponent } from './page/contact/contact.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'restaurants', component: RestaurantListComponent }, 
  { path: 'contact', component: ContactComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
