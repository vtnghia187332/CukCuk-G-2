<template>
  <div class="m-dialog-box">
    <div class="m-dialog m-dlg-show m-dlg-import">
      <div class="dialog-header dlg-header-show">
        <div class="m-popup-title m-import-dlg">Nhập khẩu nguyên vật liệu</div>
        <div class="m-popup-close"></div>
      </div>
      <div class="m-popup-content dlg-content-show">
        <div class="m-popup-title-content">Bước 1: Tải tệp lên</div>
        <div class="m-popup-content-import">
          <div class="m-content-import">
            <div class="m-txt-content-import">
              <div class="txt-content-import">Thả tệp vào đây hoặc</div>
              <div class="m-btn-upload-file">
                <!--default html file upload button-->
                <input type="file" id="actual-btn" hidden />
                <!--our custom file upload button-->
                <label for="actual-btn">TẢI LÊN</label>
              </div>
            </div>
            <div class="content-validate">
              Định dạng được hỗ trợ: xlsx, xls, csv, ods
            </div>
          </div>
        </div>
        <div class="m-popup-content-footer">
          <div class="m-txt-footer">
            Để có kết quả nhập khẩu chính xác, hãy sử dụng tệp mẫu.
            <span>Tải xuống tệp mẫu</span>
          </div>
          <div class="m-txt-footer">
            Mỗi dòng dữ liệu đều trong tệp nhập khẩu tương ứng với 1 nguyên vật
            liệu
          </div>
        </div>
      </div>
      <div class="m-popup-footer dlg-footer-show">
        <button
          class="m-btn m-btn-wicon fit-center m-btn-fotter"
          @click="showAlertDlg"
        >
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
export default {
  props: [],
  methods: {
    /**
     * Mô tả : off dialog
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 13:55 12/05/2022
     */
    showAlertDlg() {
      this.$emit("isShowDlgImportFirstOnClick", false);
    },
  },
};
</script>
<style scoped></style>
