import { Component, OnInit } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { AlertifyService } from "../services/alertify.service";
import { Router } from "@angular/router";
import { THIS_EXPR } from "@angular/compiler/src/output/output_ast";

@Component({
  selector: "app-nav",
  templateUrl: "./nav.component.html",
  styleUrls: ["./nav.component.css"]
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(
    private authservice: AuthService,
    private alert: AlertifyService,
    private router: Router
  ) {}

  ngOnInit() {}
  Login() {
    this.authservice.Login(this.model).subscribe(
      next => {
        this.alert.error("Successfuly Login");
      },
      error => {
        this.alert.error(error);
      },
      () => {
        this.router.navigate(["/members"]);
      }
    );
  }
  LoggedIn() {
    return this.authservice.LoggedIn();
  }
  Logout() {
    localStorage.removeItem("token");
    this.alert.message("successfully logout");
    this.router.navigate(["/home"]);
  }
}
