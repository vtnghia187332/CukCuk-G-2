<template>
  <div class="m-dialog-box">
    <div class="m-dialog m-dlg-show">
      <div class="dialog-header dlg-header-show">
        <div class="m-popup-title">CUKCUK - Phần mềm quản lý nhà hàng</div>
        <div class="m-popup-close" @click="showConfirmDlg">
          <i class="fa-solid fa-x"></i>
        </div>
      </div>
      <div class="m-popup-content dlg-content-show">
        <div class="m-popup-content-avatar"></div>
        <div class="m-popup-content-text">{{ this.txtConfirmDlg }}</div>
      </div>
      <div class="m-popup-footer dlg-footer-show">
        <button
          class="m-btn m-btn-wicon fit-center m-btn-fotter"
          @click="handleSaveAfterChangeOnClick"
        >
          <div class="m-btn-icon">
            <i class="fa-solid fa-check"></i>
          </div>
          <div class="m-btn-text">Đồng ý</div>
        </button>
        <button
          class="m-btn m-btn-wicon fit-center m-btn-fotter"
          @click="showConfirmDlg"
        >
          <div class="m-btn-icon">
            <i class="fa-solid fa-xmark"></i>
          </div>
          <div class="m-btn-text">Hủy</div>
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["txtConfirmDlg"],
  methods: {
    /**
     * Mô tả : off dialog confirm dialog
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 13:55 12/05/2022
     */
    showConfirmDlg() {
      this.$emit("showConfirmDlg", false);
    },

    /**
     * Mô tả : Xử lý sự kiện đồng ý tắt form
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 09:00 13/05/2022
     */
    handleSaveAfterChangeOnClick() {
      // @param: isSave - isCancel
      this.$emit("handleAfterChangeOnClick", true);
    },
  },
};
</script>

<style scoped>
</style>