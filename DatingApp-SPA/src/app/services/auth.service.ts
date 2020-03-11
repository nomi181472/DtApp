import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";
import { JwtHelperService } from "@auth0/angular-jwt";
@Injectable({
  providedIn: "root"
})
export class AuthService {
  baseUrls = "http://localhost:5000/api/auth/";
  jwt = new JwtHelperService();
  decodedToken: any;
  constructor(public http: HttpClient) {}
  Login(model: any) {
    return this.http.post(this.baseUrls + "Login", model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem("token", user.token);
          this.decodedToken = this.jwt.decodeToken(user.token);
          console.log(this.decodedToken);
        }
      })
    );
  }
  Register(model: any) {
    return this.http.post(this.baseUrls + "Register", model);
  }
  LoggedIn() {
    const token = localStorage.getItem("token");
    return !this.jwt.isTokenExpired(token);
  }
}
