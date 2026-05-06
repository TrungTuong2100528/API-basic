import { getToken } from "./auth";

const BASE_URL = "https://localhost:7243/api";

export const api = async (endpoint, options = {}) => {
  const token = getToken();

  const res = await fetch(`${BASE_URL}/${endpoint}`, {
    ...options,
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
      ...(options.headers || {}),
    },
  });

  if (res.status === 401) {
    alert("Chưa đăng nhập");
    window.location.href = "/";
  }

  if (res.status === 403) {
    alert("Không có quyền");
  }

  return res.json();
};