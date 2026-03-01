import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatCard } from "@angular/material/card";

@Component({
  selector: 'app-server-error',
  imports: [MatCard],
  templateUrl: './server-error.component.html',
  styleUrl: './server-error.component.css',
})
export class ServerErrorComponent {
error?: any

constructor(private router: Router){
  const navigation = router.currentNavigation();
  this.error = navigation?.extras.state?.['error']
}
}
