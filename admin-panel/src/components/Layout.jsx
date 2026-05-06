import { Link } from "react-router-dom";
import { logout } from "../services/auth";

export default function Layout({ children }) {
  return (
    <div>
      <nav style={{ background: "#333", padding: 10 }}>
        <Link to="/dashboard" style={{ color: "white", marginRight: 10 }}>
          Dashboard
        </Link>

        <Link to="/products" style={{ color: "white", marginRight: 10 }}>
          Products
        </Link>

        <button onClick={logout}>Logout</button>
      </nav>

      <div style={{ padding: 20 }}>{children}</div>
    </div>
  );
}