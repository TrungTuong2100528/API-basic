import { fetchWithAuth } from "../../../services/api";

function AdminDashboard() {

  const loadProducts = async () => {
    const res = await fetchWithAuth(
      "https://localhost:7243/api/inventory"
    );

    const data = await res.json();
    console.log(data);
  };

  return (
    <div>
      <h2>Admin</h2>
      <button onClick={loadProducts}>
        Load Products
      </button>
    </div>
  );
}

export default AdminDashboard;