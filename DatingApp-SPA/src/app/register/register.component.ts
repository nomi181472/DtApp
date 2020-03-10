import { Component, OnInit, Input, EventEmitter, Output } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { NEXT } from "@angular/core/src/render3/interfaces/view";

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.css"]
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private authser: AuthService) {}

  ngOnInit() {}
  Register() {
    this.authser.Register(this.model).subscribe(
      () => {
        console.log("reg Sucess");
      },
      error => {
        console.log(error);
      }
    );
  }
  Cancel() {
    this.cancelRegister.emit(false);
    console.log("cancelled");
  }
}
