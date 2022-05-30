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
        <div class="first-form-drop" v-if="!isShowTableOnClick">
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
              Mỗi dòng dữ liệu đều trong tệp nhập khẩu tương ứng với 1 nguyên
              vật liệu
            </div>
          </div>
        </div>
        <div class="second-form-drop" v-if="isShowTableOnClick">
          <div class="m-popup-title-content">Bước 2: Kiểm tra dữ liệu</div>
          <div class="frame-import-table">
            <div class="m-grid m-grid-import">
              <div class="m-table-grid-flex">
                <div class="m-frame-table m-frame-table-import">
                  <table
                    class="m-table m-table-import"
                    cellspacing="0"
                    cellpadding="0"
                  >
                    <thead>
                      <tr>
                        <th class="">MÃ NGUYÊN VẬT LIỆU</th>
                        <th class="">TÊN NGUYÊN VẬT LIỆU</th>
                        <th class="">TÍNH CHẤT</th>
                        <th class="">ĐƠN VỊ TÍNH</th>
                        <th class="">LÝ DO</th>
                        <th class=""></th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                      </tr>
                      <tr>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                      </tr>
                      <tr>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                      </tr>
                      <tr>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                      </tr>
                      <tr>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                      </tr>
                      <tr>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                      </tr>
                      <tr>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                      </tr>
                      <tr>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                      </tr>
                      <tr>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                      </tr>
                      <tr>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                      </tr>
                      <tr>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                      </tr>
                      <tr>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                      </tr>
                      <tr>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                      </tr>
                      <tr>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                      </tr>
                      <tr>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                      </tr>
                      <tr>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                      </tr>
                      <tr>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                      </tr>
                      <tr>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                      </tr>
                      <tr>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                        <td class=""></td>
                      </tr>
                    </tbody>
                  </table>
                </div>
              </div>
            </div>
          </div>
          <!-- footer -->
          <div class="m-table-import-footer">
            <div class="m-import-footer-left">
              Hiển thị 1-10 trên 120 kết quả
            </div>
            <div class="m-import-footer-right">2</div>
          </div>
        </div>
      </div>
      <div class="m-popup-footer dlg-footer-show">
        <div class="first-form-drop-footer" v-if="!isShowTableOnClick">
          <button
            class="m-btn m-btn-wicon fit-center m-btn-fotter"
            @click="showAlertDlg"
          >
            <div class="m-btn-text">Hủy</div>
          </button>
          <button
            class="m-btn m-btn-wicon fit-center m-btn-fotter"
            @click="handleTableForImportOnClick"
          >
            <div class="m-btn-text">Tiếp theo</div>
          </button>
        </div>
        <div class="second-form-drop-fotter" v-if="isShowTableOnClick">
          <button class="m-btn m-btn-wicon fit-center m-btn-fotter">
            <div class="m-btn-text">Quay lại</div>
          </button>
          <button class="m-btn m-btn-wicon fit-center m-btn-fotter">
            <div class="m-btn-text">Nhập khẩu</div>
          </button>
        </div>
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
  data() {
    return {
      // Hiển thị table trước khi import nguyên vật liệu
      isShowTableOnClick: false,
    };
  },
  props: [],
  setup() {
    let dropzoneFile = ref("");

    const drop = (e) => {
      dropzoneFile.value = e.dataTransfer.files[0];
    };

    const selectedFile = () => {
      dropzoneFile.value = document.querySelector(".dropzoneFile").files[0];
      console.log(dropzoneFile.value);
    };

    return { dropzoneFile, drop, selectedFile };
  },
  methods: {
    /**
     * Mô tả : Hiển thị table trước khi import xuống database
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 16:08 30/05/2022
     */
    handleTableForImportOnClick() {
      this.isShowTableOnClick = true;
    },
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
