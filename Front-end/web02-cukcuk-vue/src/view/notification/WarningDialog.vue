<template>
  <div class="m-dialog-box">
    <div class="m-dialog m-dlg-show">
      <div class="dialog-header dlg-header-show">
        <div class="m-popup-title">CUKCUK - Phần mềm quản lý nhà hàng</div>
        <div class="m-popup-close" @click="showWarningDlg">
          <i class="fa-solid fa-x"></i>
        </div>
      </div>
      <div class="m-popup-content dlg-content-show">
        <div class="m-popup-content-avatar"></div>
        <div class="m-popup-content-text">{{ this.txtWarningDlg }}</div>
      </div>
      <div class="m-popup-footer dlg-footer-show">
        <button
          class="m-btn m-btn-wicon fit-center m-btn-fotter"
          @click="agreeHanleOnClick"
        >
          <div class="m-btn-icon">
            <i class="fa-solid fa-check"></i>
          </div>
          <div class="m-btn-text">Đồng ý</div>
        </button>
        <button
          class="m-btn m-btn-wicon fit-center m-btn-fotter"
          @click="showWarningDlg"
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
  props: ["txtWarningDlg"],
  methods: {
    /**
     * Mô tả : off dialog warning dialog
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 13:55 12/05/2022
     */
    showWarningDlg() {
      this.$emit("showWarningDlg", false);
    },
    agreeHanleOnClick() {
      this.$emit("agreeHanleOnClick", true);
    },
  },
};
</script>

<style scoped>
</style>