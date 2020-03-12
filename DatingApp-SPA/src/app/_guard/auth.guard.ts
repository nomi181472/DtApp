import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { AlertifyService } from "../services/alertify.service";
import { AuthService } from "../services/auth.service";

@Injectable({
  providedIn: "root"
})
export class AuthGuard implements CanActivate {
  constructor(
    private alert: AlertifyService,
    private auth: AuthService,
    private route: Router
  ) {}
  canActivate(): boolean {
    if (this.auth.LoggedIn()) return true;

    this.alert.error("you are unlogin");
    this.route.navigate(["/home"]);
    return false;
  }
}
