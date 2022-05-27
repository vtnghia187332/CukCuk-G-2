<template>
  <div class="m-dialog-box" >
    <div class="m-dialog m-dlg-show m-dlg-import">
      <div class="dialog-header dlg-header-show">
        <div class="m-popup-title m-import-dlg">Nhập khẩu nguyên vật liệu</div>
        <div class="m-popup-close"></div>
      </div>
      <div class="m-popup-content dlg-content-show">
        <div class="m-popup-title-content">Bước 1: Tải tệp lên</div>

        <div class="frame-import-list">
          <div class="m-frame-import-list">
            <div class="import-list-left">
              <div class="icon-import-list">
                <i class="fa-solid fa-database"></i>
              </div>
              <div class="txt-import-list">Danh sach nguyen vat lieu.xlxs</div>
            </div>
            <div class="import-list-right">
              <button class="m-btn fit-center">
                <div class="m-btn-text m-btn-import">TẢI LÊN TỆP KHÁC</div>
              </button>
            </div>
          </div>
          <div class="m-popup-content-footer">
            <div class="m-txt-footer">
              Để có kết quả nhập khẩu chính xác, hãy sử dụng tệp mẫu.
              <span>Tải xuống tệp mẫu</span>
            </div>
            <div class="m-txt-footer">
              Mỗi dòng dữ liệu đều trong tệp nhập khẩu tương ứng với 1 nguyên
              vật liệu
            </div>
          </div>
        </div>
        
      </div>
      <div class="m-popup-footer dlg-footer-show">
        <button class="m-btn m-btn-wicon fit-center m-btn-fotter">
          <div class="m-btn-text">Hủy</div>
        </button>
        <button class="m-btn m-btn-wicon fit-center m-btn-fotter">
          <div class="m-btn-text">Tiếp theo</div>
        </button>
      </div>
    </div>
  </div>
</template>
<script>
export default {};
</script>
<style scoped></style>
