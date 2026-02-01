import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { HttpClient, HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  standalone: true,
  // We import HttpClientModule here so the app can make web requests
  imports: [CommonModule, IonicModule, HttpClientModule],
})
export class AppComponent implements OnInit {
  private apiUrl = 'http://localhost:5079/api/orders';

  products: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.fetchProducts();
  }

  // API 1: GET /products
  // Fetches the list from http://localhost:5079/api/orders/products
  fetchProducts() {
    this.http.get<any[]>(`${this.apiUrl}/products`).subscribe({
      next: (data) => {
        this.products = data;
        console.log("Products loaded successfully", data);
      },
      error: (err) => {
        console.error("Connection error!", err);
        alert("Cannot connect to backend on port 5079. Is 'dotnet run' active?");
      }
    });
  }

  // API 2: POST /orders
  // Sends order to http://localhost:5079/api/orders
  order(productId: number) {
    const payload = { productId: productId, quantity: 1 };

    this.http.post(this.apiUrl, payload).subscribe({
      next: (res: any) => {
        alert("Success: " + res.message);
        this.fetchProducts(); // Refresh list to see stock decrease
      },
      error: (err) => {
        // This shows the "Not enough stock" error from your Service layer
        const errorMsg = err.error?.message || "Order failed";
        alert("Failed: " + errorMsg);
      }
    });
  }
}