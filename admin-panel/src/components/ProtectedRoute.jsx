import { getToken } from "../services/auth";
import { Navigate } from "react-router-dom";

export default function ProtectedRoute({ children }) {
  const token = getToken();

  if (!token) {
    return <Navigate to="/" />;
  }

  return children;
}