<template>
  <div class="m-dialog-box">
    <div class="m-dialog m-dlg-show m-dlg-import">
      <div class="dialog-header dlg-header-show">
        <div class="m-popup-title m-import-dlg">Nhập khẩu nguyên vật liệu</div>
        <div class="m-popup-close" @click="showAlertDlg">
          <i class="fa-solid fa-x"></i>
        </div>
      </div>
      <div class="m-popup-content dlg-content-show">
        <div class="m-popup-title-content">Bước 1: Tải tệp lên</div>
        <div class="m-popup-content-import">
          <div class="m-content-import">
            <div class="m-txt-content-import">
              <DropZone
                v-if="dropzoneFile.name == null"
                @drop.prevent="drop"
                @change="selectedFile"
              />

              <div class="frame-import-list" v-if="dropzoneFile.name != null">
                <div class="m-frame-import-list">
                  <div class="import-list-left">
                    <div class="icon-import-list">
                      <i class="fa-solid fa-database"></i>
                    </div>
                    <div class="txt-import-list">
                      {{ dropzoneFile.name }}
                    </div>
                  </div>
                  <div class="import-list-right">
                    <button class="m-btn fit-center">
                      <div class="m-btn-text m-btn-import">
                        TẢI LÊN TỆP KHÁC
                      </div>
                    </button>
                  </div>
                </div>
                <div class="m-popup-content-footer">
                  <div class="m-txt-footer">
                    Để có kết quả nhập khẩu chính xác, hãy sử dụng tệp mẫu.
                    <span>Tải xuống tệp mẫu</span>
                  </div>
                  <div class="m-txt-footer">
                    Mỗi dòng dữ liệu đều trong tệp nhập khẩu tương ứng với 1
                    nguyên vật liệu
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="m-popup-content-footer" v-if="dropzoneFile.name == null">
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
// eslint-disable-next-line
/*eslint-disable */
import { ref } from "vue";
import DropZone from "@/components/Import-Data/Drop-zone.vue";

export default {
  components: { DropZone },
  props: [],
  setup() {
    let dropzoneFile = ref("");

    const drop = (e) => {
      dropzoneFile.value = e.dataTransfer.files[0];
    };

    const selectedFile = () => {
      dropzoneFile.value = document.querySelector(".dropzoneFile").files[0];
    };

    return { dropzoneFile, drop, selectedFile };
  },
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
<style lang="scss" scoped>
.home {
  height: 100vh;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  background-color: #f1f1f1;

  h1 {
    font-size: 40px;
    margin-bottom: 32px;
  }

  .file-info {
    margin-top: 32px;
  }
}
</style>
