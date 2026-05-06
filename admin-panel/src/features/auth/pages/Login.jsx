import { useState } from "react";
import { login, saveToken } from "../../../services/auth";

export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = async () => {
    try {
      const res = await login({ email, password });

      saveToken(res.token);

      window.location.href = "/dashboard";
    } catch {
      alert("Sai tài khoản");
    }
  };

  return (
    <div>
      <h2>Admin Login</h2>

      <input placeholder="Email" onChange={e => setEmail(e.target.value)} />
      <input type="password" placeholder="Password" onChange={e => setPassword(e.target.value)} />

      <button onClick={handleLogin}>Login</button>
    </div>
  );
}