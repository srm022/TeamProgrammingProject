import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { GroupsComponent } from './groups/groups.component';
import { GroupCreatorComponent } from './groupCreator/groupCreator.component';
import { ToastModule } from 'ng2-toastr/ng2-toastr';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { GroupEditComponent } from './group-edit/group-edit.component';
import { GroupUsersDisplayComponent } from './group-users-display/group-users-display.component'; 


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    GroupsComponent,
    GroupCreatorComponent,
    GroupEditComponent,
    GroupUsersDisplayComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    MatProgressSpinnerModule,
    ToastModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'groups', component: GroupsComponent },
      { path: 'groupCreator', component: GroupCreatorComponent },
      { path: 'group-users-display', component:  GroupUsersDisplayComponent}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
