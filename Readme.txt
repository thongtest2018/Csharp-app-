Họ và tên: Bùi Minh Thông
MSSV: 1988089

Các bước để chạy project

- Attach CSLD MYSHOP.mdf vào sql server
- Chạy file setup.exe
- Mở file 1988089.exe.config kiểm tra thẻ connectionstring attribute datasource có giống với sqlserver đã attach data không (Mặc định là SQLEXPRESS)
- Chạy chương trình

Video demo:
https://youtu.be/2dAFnaGBQ6A

Danh sách các chức năng đã làm được:
+ Thêm, xóa, sửa loại sản phẩm
+ Thêm, xóa, sửa sản phẩm có lưu ảnh trong cơ sở dữ liệu
+ Tìm kiếm theo tên sản phẩm
+ Phân trang
+ Hiển thị danh sách sản phẩm theo loại sản phẩm có phân trang
+ Filter sản phẩm theo khoảng giá

+ Tạo danh sách đơn hàng có phân trang
+ Thêm mới, sửa, xóa đơn hàng
+ Tìm kiếm đơn hàng theo ID, theo tên khách hàng, theo ngày tháng tạo đơn hàng, theo trạng thái của đơn hàng

+ Tạo danh sách các sản phảm với số lượng tồn trong kho
+ Duyệt các sản phẩm có số lượng nhỏ hơn só lượng do người dùng nhập vào

+ Tạo báo cáo hình bánh cho doanh thu theo khoảng thời gian của mỗi loại hàng

Chức năng chưa làm được:
+ Filter sản phẩm theo ram và bộ nhớ trong và kết hợp tất cả

