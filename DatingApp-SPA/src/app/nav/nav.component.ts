import { Component, OnInit } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { AlertifyService } from "../services/alertify.service";

@Component({
  selector: "app-nav",
  templateUrl: "./nav.component.html",
  styleUrls: ["./nav.component.css"]
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(
    private authservice: AuthService,
    private alert: AlertifyService
  ) {}

  ngOnInit() {}
  Login() {
    this.authservice.Login(this.model).subscribe(
      next => {
        this.alert.error("Successfuly Login");
      },
      error => {
        this.alert.error(error);
      }
    );
  }
  LoggedIn() {
    return this.authservice.LoggedIn();
  }
  Logout() {
    localStorage.removeItem("token");
    this.alert.message("successfully logout");
  }
}
