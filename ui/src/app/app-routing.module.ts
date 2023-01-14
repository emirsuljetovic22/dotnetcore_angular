import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AdminPanelComponent } from "./admin/admin-panel/admin-panel.component";
import { AddArticleComponent } from "./admin/article-management/add-article/add-article.component";
import { ArticleManagementComponent } from "./admin/article-management/article-management.component";
import { EditArticleComponent } from "./admin/article-management/edit-article/edit-article.component";
import { ListArticlesComponent } from "./articles/list-articles/list-articles.component";
import { SingleArticleComponent } from "./articles/single-article/single-article.component";
import { HomeComponent } from "./home/home.component";
import { JobsComponent } from "./jobs/jobs.component";
import { ListsComponent } from "./lists/lists.component";
import { MemberDetailComponent } from "./members/member-detail/member-detail.component";
import { MemberEditComponent } from "./members/member-edit/member-edit.component";
import { MemberListComponent } from "./members/member-list/member-list.component";
import { MessagesComponent } from "./messages/messages.component";
import { NotFoundComponent } from "./not-found/not-found.component";
import { RegisterComponent } from "./register/register.component";
import { AdminGuard } from "./_guards/admin.guard";
import { AuthGuard } from "./_guards/auth.guard";
import { PreventUnsavedChangesGuard } from "./_guards/prevent-unsaved-changes.guard";
import { MemberDetailedResolver } from "./_resolvers/member-detailed.resolver";

const routes: Routes = [
  {path: '', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'members', component: MemberListComponent},
      {path: 'member/edit', component: MemberEditComponent, canDeactivate: [PreventUnsavedChangesGuard]},
      {path: 'members/:username', component: MemberDetailComponent, resolve: {member: MemberDetailedResolver}},
      {path: 'lists', component: ListsComponent},
      {path: 'messages', component: MessagesComponent},

      {path: 'jobs', component: JobsComponent},

      {path: 'admin', component: AdminPanelComponent, canActivate: [AdminGuard]},
      {path: 'admin/articles', component: ArticleManagementComponent, canActivate: [AdminGuard]},
      {path: 'admin/articles/edit/:url', component: EditArticleComponent, canActivate: [AdminGuard]},
      {path: 'admin/articles/add', component: AddArticleComponent, canActivate: [AdminGuard]}
    ]
  },
  {path: 'news', component: ListArticlesComponent},
  {path: 'news/:url', component: SingleArticleComponent},
  {path: 'home', component: HomeComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'not-found', component: NotFoundComponent},
  {path: '**', component: NotFoundComponent, pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
