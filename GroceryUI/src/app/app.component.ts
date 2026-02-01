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
  
  private apiUrl = 'http://localhost:5079/api/orders';
  products: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.fetchProducts();
  }

  // API 1: Fetch product list
  fetchProducts() {
    this.http.get<any[]>(`${this.apiUrl}/products`).subscribe({
      next: (data) => this.products = data,
      error: (err) => alert("Backend unreachable. Check port 5079.")
    });
  }

  // API 2: Place order and handle business logic responses
  order(productId: number) {
    this.http.post(this.apiUrl, { productId, quantity: 1 }).subscribe({
      next: (res: any) => {
        alert(res.message);
        this.fetchProducts(); // Refresh stock after success
      },
      error: (err) => alert("Failed: " + (err.error?.message || "Order error"))
    });
  }
}