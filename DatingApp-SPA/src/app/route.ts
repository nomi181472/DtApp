import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { MemberListComponent } from "./Member-list/Member-list.component";
import { MessagesComponent } from "./messages/messages.component";
import { ListComponent } from "./list/list.component";
import { AuthGuard } from "./_guard/auth.guard";

export const appRoute: Routes = [
  { path: "", component: HomeComponent },
  {
    path:"",
    runGuardsAndResolvers:"always",
    canActivate:[AuthGuard],
    children:[
      { path: "members", component: MemberListComponent, canActivate: [AuthGuard ] },
      { path: "messages", component: MessagesComponent },
      { path: "lists", component: ListComponent },
    ]
  }
    

  { path: "**", redirectTo: "", pathMatch: "full" }
];
 