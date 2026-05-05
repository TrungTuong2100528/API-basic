import { Routes } from '@angular/router';
import { InventoryComponent } from './Features/inventory/inventory.component';
import { CustomerComponent } from './Features/customer/customer.component';

export const routes: Routes = [

    // Inventory là trang mặc định
    { path: '', redirectTo: 'inventory', pathMatch: 'full' },

    { path: 'inventory', component: InventoryComponent },
    { path: 'Customers', component: CustomerComponent },

    {
        path: 'login',
        loadComponent: () =>
            import('./Features/Auth/login/login.component').then(m => m.LoginComponent)
    },
    {
        path: 'register',
        loadComponent: () =>
            import('./Features/Auth/register/register.component').then(m => m.RegisterComponent)
    },

    { path: '**', redirectTo: 'inventory' }
];
