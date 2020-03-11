import { Component, OnInit, Input, EventEmitter, Output } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { AlertifyService } from "../services/alertify.service";

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.css"]
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private authser: AuthService, private alert: AlertifyService) {}

  ngOnInit() {}
  Register() {
    this.authser.Register(this.model).subscribe(
      () => {
        this.alert.success("registration Sucessessfully");
      },
      error => {
        this.alert.error(error);
      }
    );
  }
  Cancel() {
    this.cancelRegister.emit(false);
    console.log("cancelled");
  }
}
