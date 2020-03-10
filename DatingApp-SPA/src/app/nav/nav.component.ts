import { Component, OnInit } from "@angular/core";
import { AuthService } from "../services/auth.service";

@Component({
  selector: "app-nav",
  templateUrl: "./nav.component.html",
  styleUrls: ["./nav.component.css"]
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(private authservice: AuthService) {}

  ngOnInit() {}
  Login() {
    this.authservice.Login(this.model).subscribe(
      next => {
        console.log("login successfully");
      },
      error => {
        console.log(error);
      }
    );
  }
  LoggedIn() {
    const token = localStorage.getItem("token");
    return !!token;
  }
  Logout() {
    localStorage.removeItem("token");
    console.log("successfully logout");
  }
}
