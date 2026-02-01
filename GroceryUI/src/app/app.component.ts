import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { HttpClient, HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  standalone: true,
  imports: [CommonModule, IonicModule, HttpClientModule],
})
export class AppComponent implements OnInit {

  private apiUrl = 'http://localhost:5251/api/orders';
  products: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.fetchProducts();
  }

  // API 1: Fetch product list from the backend
  fetchProducts() {
    this.http.get<any[]>(`${this.apiUrl}/products`).subscribe({
      next: (data) => {
        this.products = data;
        console.log("Inventory loaded:", data);
      },
      error: (err) => {
        console.error("Backend connection failed on 5251", err);
        alert("Check Backend: Ensure 'dotnet run' is active on port 5251.");
      }
    });
  }

  // API 2: Place an order
  // This triggers the mandatory transaction logic in your OrderService.cs
  order(productId: number, quantity: any) {
  const q = parseInt(quantity);
  this.http.post(this.apiUrl, { productId, quantity: q }).subscribe({
    next: (res: any) => {
      alert(res.message);
      this.fetchProducts();
    },
    error: (err) => alert("Failed: " + err.error.message)
  });
}
}