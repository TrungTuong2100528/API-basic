import { useEffect, useState } from "react";
import { api } from "../../../services/api";

export default function Product() {
  const [products, setProducts] = useState([]);

  const [form, setForm] = useState({
    productName: "",
    availableQty: "",
    reOderPoint: "",
  });

  const [editingId, setEditingId] = useState(null);
  const [loading, setLoading] = useState(false);

  // ================= LOAD DATA =================
  const load = async () => {
    setLoading(true);
    try {
      const data = await api("inventory");

      console.log("DATA từ API:", data);

      setProducts(data || []);
    } catch {
      alert("Lỗi load dữ liệu");
    }
    setLoading(false);
  };

  // ================= HANDLE INPUT =================
  const handleChange = (e) => {
    setForm({
      ...form,
      [e.target.name]: e.target.value,
    });
  };

  // ================= CREATE / UPDATE =================
  const save = async () => {
    if (!form.productName) {
      alert("Nhập tên sản phẩm");
      return;
    }

    const payload = {
      productID: editingId || 0,
      productName: form.productName,
      availableQty: Number(form.availableQty),
      reOderPoint: Number(form.reOderPoint),
    };

    try {
      if (editingId) {
        await api("inventory", {
          method: "PUT",
          body: JSON.stringify(payload),
        });
      } else {
        await api("inventory", {
          method: "POST",
          body: JSON.stringify(payload),
        });
      }

      resetForm();
      load();
    } catch {
      alert("Lỗi lưu dữ liệu");
    }
  };

  // ================= DELETE =================
  const remove = async (id) => {
    if (!window.confirm("Xoá sản phẩm?")) return;

    try {
      await api(`inventory?productId=${id}`, {
        method: "DELETE",
      });

      load();
    } catch {
      alert("Xoá thất bại");
    }
  };

  // ================= EDIT =================
  const edit = (p) => {
    setForm({
      productName: p.productName,
      availableQty: p.availableQty,
      reOderPoint: p.reOderPoint,
    });

    setEditingId(p.productID);
  };

  // ================= RESET =================
  const resetForm = () => {
    setForm({
      productName: "",
      availableQty: "",
      reOderPoint: "",
    });
    setEditingId(null);
  };

  // ================= INIT =================
  useEffect(() => {
    load();
  }, []);

  return (
    <div style={{ padding: 20 }}>
      <h2>Quản lý sản phẩm</h2>

      {/* ================= FORM ================= */}
      <div style={{ marginBottom: 20 }}>
        <input
          name="productName"
          placeholder="Tên sản phẩm"
          value={form.productName}
          onChange={handleChange}
        />

        <input
          name="availableQty"
          placeholder="Tồn kho"
          type="number"
          value={form.availableQty}
          onChange={handleChange}
        />

        <input
          name="reOderPoint"
          placeholder="Ngưỡng nhập lại"
          type="number"
          value={form.reOderPoint}
          onChange={handleChange}
        />

        <button onClick={save}>
          {editingId ? "Cập nhật" : "Thêm"}
        </button>

        {editingId && <button onClick={resetForm}>Huỷ</button>}
      </div>

      {/* ================= TABLE ================= */}
      {loading ? (
        <p>Đang tải...</p>
      ) : (
        <table border="1" width="100%" cellPadding="10">
          <thead>
            <tr>
              <th>ID</th>
              <th>Tên sản phẩm</th>
              <th>Tồn kho</th>
              <th>Ngưỡng nhập</th>
              <th>Hành động</th>
            </tr>
          </thead>

          <tbody>
            {products.length === 0 ? (
              <tr>
                <td colSpan="5">Không có dữ liệu</td>
              </tr>
            ) : (
              products.map((p, index) => (
                <tr key={p.productID || index}>
                  <td>{p.productID}</td>
                  <td>{p.productName}</td>
                  <td>{p.availableQty}</td>
                  <td>{p.reOderPoint}</td>
                  <td>
                    <button onClick={() => edit(p)}>Sửa</button>
                    <button onClick={() => remove(p.productID)}>
                      Xoá
                    </button>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      )}
    </div>
  );
}