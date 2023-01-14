import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './_modules/shared.module';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './not-found/not-found.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { PhotoEditorComponent } from './members/photo-editor/photo-editor.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { DateInputComponent } from './_forms/date-input/date-input.component';
import { MemberMessagesComponent } from './members/member-messages/member-messages.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { HasRoleDirective } from './_directives/has-role.directive';
import { PhotoManagementComponent } from './admin/photo-management/photo-management.component';
import { SingleArticleComponent } from './articles/single-article/single-article.component';
import { SidebarLeftComponent } from './articles/sidebar-left/sidebar-left.component';
import { SidebarRightComponent } from './articles/sidebar-right/sidebar-right.component';
import { ListArticlesComponent } from './articles/list-articles/list-articles.component';
import { ArticleManagementComponent } from './admin/article-management/article-management.component';
import { InfoManagementComponent } from './admin/info-management/info-management.component';
import { AddArticleComponent } from './admin/article-management/add-article/add-article.component';
import { EditArticleComponent } from './admin/article-management/edit-article/edit-article.component';
import { AddInfoComponent } from './admin/info-management/add-info/add-info.component';
import { AddInfoCategoryComponent } from './admin/info-management/add-info-category/add-info-category.component';
import { EditInfoComponent } from './admin/info-management/edit-info/edit-info.component';
import { EditInfoCategoryComponent } from './admin/info-management/edit-info-category/edit-info-category.component';
import { JobManagementComponent } from './admin/job-management/job-management.component';
import { AddJobComponent } from './admin/job-management/add-job/add-job.component';
import { AddJobCategoryComponent } from './admin/job-management/add-job-category/add-job-category.component';
import { EditJobComponent } from './admin/job-management/edit-job/edit-job.component';
import { EditJobCategoryComponent } from './admin/job-management/edit-job-category/edit-job-category.component';
import { LoaderInterceptor } from './_interceptors/loader.interceptor';
import { SlugifyPipe, SlugifyPipeOptions } from './_pipes/url-converter.pipe';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { JobsComponent } from './jobs/jobs.component';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    MemberListComponent,
    MemberDetailComponent,
    ListsComponent,
    MessagesComponent,
    NotFoundComponent,
    PhotoEditorComponent,
    TextInputComponent,
    DateInputComponent,
    MemberMessagesComponent,
    AdminPanelComponent,
    HasRoleDirective,
    PhotoManagementComponent,
    SingleArticleComponent,
    SidebarLeftComponent,
    SidebarRightComponent,
    ListArticlesComponent,
    ArticleManagementComponent,
    InfoManagementComponent,
    AddArticleComponent,
    EditArticleComponent,
    AddInfoComponent,
    AddInfoCategoryComponent,
    EditInfoComponent,
    EditInfoCategoryComponent,
    JobManagementComponent,
    AddJobComponent,
    AddJobCategoryComponent,
    EditJobComponent,
    EditJobCategoryComponent,
    SlugifyPipe,
    RegisterComponent,
    LoginComponent,
    JobsComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    SharedModule,
    ReactiveFormsModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
