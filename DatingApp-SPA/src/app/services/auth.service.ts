import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";
@Injectable({
  providedIn: "root"
})
export class AuthService {
  baseUrls = "http://localhost:5000/api/auth/";
  constructor(private http: HttpClient) {}
  Login(model: any) {
    return this.http.post(this.baseUrls + "Login", model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem("token", user.token);
        }
      })
    );
  }
  Register(model: any) {
    return this.http.post(this.baseUrls + "Register", model);
  }
}
