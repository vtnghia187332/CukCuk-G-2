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
        <div class="first-form-drop" v-show="!isShowTableOnClick">
          <div class="m-popup-title-content">Bước 1: Tải tệp lên</div>

          <div class="m-popup-content-import">
            <div class="m-content-import">
              <div class="m-txt-content-import">
                <DropZone v-show="dropzoneFile.name == null" @drop.prevent="drop" @change="selectedFile" />

                <div class="frame-import-list" v-show="dropzoneFile.name != null">
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
                      <div class="m-btn-upload-file">
                        <!--default html file upload button-->
                        <input type="file" id="dropzoneFile" class="dropzoneFile" @change="selectedFile" hidden />
                        <!--our custom file upload button-->
                        <label for="dropzoneFile">TẢI LÊN TỆP KHÁC</label>
                      </div>
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
          <div class="m-popup-content-footer" v-show="dropzoneFile.name == null">
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
        <div class="second-form-drop" v-show="isShowTableOnClick">
          <div class="m-popup-title-content-second">
            <div class="m-popup-title-content">Bước 2: Kiểm tra dữ liệu</div>
            <div class="m-popup-title-content-right">
              <div class="m-valid-row-preview">Hợp lệ: <span>{{ isValidObject }}</span></div>
              <div class="m-inValid-row-preview">Không hợp lệ: <span>{{ isInValidObject }}</span></div>
            </div>
          </div>
          <div class="frame-import-table">
            <div class="m-grid m-grid-import">
              <div class="m-table-grid-flex">
                <div class="m-frame-table m-frame-table-import">
                  <table class="m-table m-table-import" cellspacing="0" cellpadding="0">
                    <thead>
                      <tr>
                        <th class="m-table-validate txt-left">
                          MÃ NGUYÊN VẬT LIỆU
                        </th>
                        <th class="m-table-validate txt-left">
                          TÊN NGUYÊN VẬT LIỆU
                        </th>
                        <th class="m-table-validate txt-left">TÍNH CHẤT</th>
                        <th class="m-table-validate txt-left">ĐƠN VỊ TÍNH</th>
                        <th class="txt-left">LÝ DO</th>
                        <th class="m-table-validate-action txt-left"></th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr v-for="(material, index) in materialsToImport" :key="index">
                        <td class="">
                          <BaseInput @input="changeValue(material.MaterialCode, index)" ref="MaterialCode"
                            v-model="material.MaterialCode" class="width-100" />
                        </td>
                        <td class="">
                          <BaseInput @input="changeValue(material.MaterialName, index)" ref="MaterialName"
                            v-model="material.MaterialName" class="width-100" />
                        </td>
                        <td class="">
                          <BaseInput @input="changeValue(material.MaterialFeature, index)"
                            v-model="material.MaterialFeature" class="width-100" />
                        </td>
                        <td class="">
                          <BaseInput @input="changeValue(material.UnitName, index)" ref="UnitName"
                            v-model="material.UnitName" class="width-100" />
                        </td>
                        <td class="">
                          <div class="reason-error txt-error-table">
                            {{ this.listErrValidate[index] }}
                          </div>
                        </td>
                        <td @click="deleteObjectInPreview(index)">X</td>
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
            <div class="m-import-footer-right">
              <!-- Pagination -->

              <!-- Pagination -->
            </div>
          </div>
        </div>
      </div>
      <div class="m-popup-footer dlg-footer-show">
        <div class="first-form-drop-footer" v-show="!isShowTableOnClick">
          <button class="m-btn m-btn-wicon fit-center m-btn-fotter" @click="showAlertDlg">
            <div class="m-btn-text">Hủy</div>
          </button>
          <button class="m-btn m-btn-wicon fit-center m-btn-fotter" @click="handleTableForImportOnClick"
            :disabled="dropzoneFile.name == null">
            <div class="m-btn-text">Tiếp theo</div>
          </button>
        </div>
        <div class="second-form-drop-fotter" v-show="isShowTableOnClick">
          <button class="m-btn m-btn-wicon fit-center m-btn-fotter" @click="turnbackChangeForm">
            <div class="m-btn-text">Quay lại</div>
          </button>
          <button class="m-btn m-btn-wicon fit-center m-btn-fotter" @click="saveMaterialsFromExcel">
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
import BaseInput from "@/components/base/BaseInput.vue";
import BasePagination from "@/components/base/BasePagination.vue";
import axios from "axios";

export default {
  components: { DropZone, BaseInput, BasePagination },
  data() {
    return {
      // Hiển thị table trước khi import nguyên vật liệu
      isShowTableOnClick: false,

      //Danh sách nguyên vật liệu được thêm vào từ file excel
      materialsToImport: [],

      // chứa thông tin lỗi validate từ file excel
      listErrValidate: {},

      //Biến để check validate preview trên client
      isValidForInsert: true,

      //Hiển thị số dòng hợp lệ trong bảng preview
      isValidObject: 0,

      //Hiển thị số dòng không hợp lệ trong bảng preview
      isInValidObject: 0,
    };
  },
  props: [],
  methods: {
    /**
    * Mô tả : test input
    * Created by: Vũ Trọng Nghĩa - MF1108
    * Created date: 14:48 02/06/2022
    */
    changeValue(value, index) { // Input
      // CLear lỗi
      this.listErrValidate[index] = null;
      this.validateDataFromClient(this.materialsToImport)
    },
    /**
    * Mô tả : Lọc dữ liệu, trả về số bản ghi hợp lệ và không hợp lệ
    * Created by: Vũ Trọng Nghĩa - MF1108
    * Created date: 10:49 02/06/2022
    */
    checkNumberOfValidObj() {
      this.isValidObject = 0;
      this.isInValidObject = 0;
      //Lọc dữ liệu và hiển thị số dòng hợp lệ và không hợp lệ
      for (const data of this.materialsToImport) {
        if (data.IsValid == true) {
          // Thêm vào tổng số dòng hợp lệ
          this.isValidObject++;
        }
        else {
          // Thêm vào tổng số dòng không hợp lệ
          this.isInValidObject++;
        }
      }
    },
    /**
     * Mô tả : xóa dòng trong bảng preview
     *  @param index:Địa chỉ của object
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 21:52 01/06/2022
     */
    deleteObjectInPreview(index) {
      this.materialsToImport.splice(index, 1);
      //Validate Data
      //check số lượng bản ghi hợp lệ
      this.validateDataFromClient(this.materialsToImport);
      this.checkNumberOfValidObj();
    },
    /**
     * Mô tả : Thực hiện validate nguyên vật liệu
     * @param: Mảng dữ liệu của danh sách materials được import từ file excel
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 09:46 31/05/2022
     */
    validateMaterialsFormServer(arrayMaterials) {
      // Clear set error của refs
      this.clearErrorData();
      debugger
      if (arrayMaterials.length > 0) {
        for (let index = 0; index < arrayMaterials.length; index++) {
          //Hứng đối tượng từ mảng dữ liệu
          let tempMaterial = arrayMaterials[index];
          let keyErrs = Object.keys(tempMaterial.ErrorValidateNotValid);
          let object = tempMaterial.ErrorValidateNotValid;

          // this.listErrValidate[index] = []
          let err = []

          for (const key of keyErrs) {
            if (
              tempMaterial.IsValid == false &&
              tempMaterial.ErrorValidateNotValid[key]
            )
              // focus vào ô lỗi
              this.$refs[key][index].setError();
            // Hiển thị thông tin lỗi
            err.push(object[key])
          }
          this.listErrValidate[index] = err[0];
        }
      }
    },
    /**
     * Mô tả : Clear Error By using ref
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 22:24 01/06/2022
     */
    clearErrorData() {
      this.listErrValidate = [];
      let tempRefs = Object.keys(this.$refs);
      for (const key of tempRefs) {
        for (let index = 0; index < this.$refs[key].length; index++) {
          this.$refs[key][index].removeError();
        }
      }
    },

    /**
     * Mô tả : Thực hiện validate dữ liệu từ Client ở from drop data
     * @param: Danh sách nguyên vật liệu từ Client
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 15:30 01/06/2022
     */
    validateDataFromClient(clientDatas) {
      this.isValidForInsert = true;
      this.clearErrorData();
      //Clear lỗi
      this.listErrValidate = [];
      for (let index = 0; index < clientDatas.length; index++) {
        const data = clientDatas[index];
        let err = []
        // Check NULL
        if (!data.MaterialCode) {
          //Focus vào ô bị lỗi
          this.$nextTick(() => {
            this.$refs.MaterialCode[index].setError();
          });
          //Hiển thị lỗi trả về
          err.push("Mã nguyên vật liệu không được phép để trống");
          this.isValidForInsert = false;
        }
        if (!data.MaterialName) {
          //Focus vào ô bị lỗi
          this.$nextTick(() => {
            this.$refs.MaterialName[index].setError();
          });
          //Hiển thị lỗi trả về
          err.push("Tên nguyên vật liệu không được phép để trống")

          this.isValidForInsert = false;
        }
        if (!data.UnitName) {
          //Focus vào ô bị lỗi
          this.$nextTick(() => {
            this.$refs.UnitName[index].setError();
          });
          //Hiển thị lỗi trả về
          err.push("Tên ĐVT không được phép để trống")
          this.isValidForInsert = false;
        }
        //Kiểm tra mã code trùng trong bảng preview
        clientDatas.filter((item) => {
          if (
            data.MaterialCode == item.MaterialCode &&
            data.MaterialId != item.MaterialId
          ) {
            this.$nextTick(() => {
              this.$refs.MaterialCode[index].setError();
            });
            err.push("Mã nguyên vật liệu không được phép để trùng trong file")
            this.isValidForInsert = false;
          }
        });

        if (!this.isValidForInsert) {
          this.listErrValidate[index] = err[0]
        }
      }
    },

    /**
     * Mô tả : Thực hiện lưu nguyên vật liệu vào cơ sở dữ liệu
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 09:41 31/05/2022
     */
    async saveMaterialsFromExcel() {
      // Validate trên Client

      this.validateDataFromClient(this.materialsToImport);
      if (this.isValidForInsert == true) {
        for (const material of this.materialsToImport) {
          material.ErrorValidateNotValid = {};
          // Set biến isValid = true(Thỏa mãn yêu cầu)
          material.IsValid = true;
        }
        //gọi api để lưu data sau khi sửa trên Client
        await this.ImportFileToServer();

      }
    },

    /**
     * Mô tả : Gọi API để thực hiện thêm dữ liệu vào CSDL
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 00:00 01/06/2022
     */
    async ImportFileToServer() {
      let me = this;
      await axios
        .post(
          "http://localhost:5236/api/v1/Materials/file",
          me.materialsToImport
        )
        .then(function (res) {
          if (res) {
            //Nếu danh sách trả về rỗng -> Tắt form
            if (res.data.length == 0) {
              me.showAlertDlg();
            }

            // Nếu danh sách trả về có giá trị -> Hiển thị những nguyên vật liệu bị lỗi
            me.materialsToImport = [];
            me.materialsToImport = res.data;
            me.loadingData();
            //Thực hiện validate và hiển thị dòng lỗi
            me.validateMaterialsFormServer(me.materialsToImport);
          }
        })
        .catch(function (res) {
          console.log(res);
        });
    },

    /**
     * Mô tả : Quay Lại form hiển thị file và chọn file khác
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 08:43 31/05/2022
     */
    turnbackChangeForm() {
      this.isShowTableOnClick = false;
    },

    /**
     * Mô tả : Hiển thị table trước khi import xuống database
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 16:08 30/05/2022
     */
    async handleTableForImportOnClick() {
      //Gọi Api để lấy về danh sách nguyên vật liệu trong file excel
      await this.handleImportFileExcel();

      if (this.materialsToImport.length > 0) {
        // Hiển thị form thay đổi lựa chọn file
        this.isShowTableOnClick = true;
        // Validate dữ liệu
        this.validateMaterialsFormServer(this.materialsToImport);
      }
    },
    /**
     * Mô tả : Xử lý code gọi API để xử lý file import -> Trả về danh sách nguyên vật liệu
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 21:57 30/05/2022
     */
    async handleImportFileExcel() {
      var me = this;
      var formData = new FormData();
      formData.append("formFile", me.dropzoneFile);
      await axios
        .post("http://localhost:5236/api/v1/Materials/import", formData, {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        })
        .then(function (res) {
          // Lấy dữ liệu từ Api trả về
          me.materialsToImport = res.data.Result;
          me.checkNumberOfValidObj();

        })
        .catch(function (error) {
          console.log(error);
        });
    },
    /**
    * Mô tả : Load lại data trang danh sách nguyên vật liệu
    * Created by: Vũ Trọng Nghĩa - MF1108
    * Created date: 08:08 02/06/2022
    */
    loadingData() {
      this.$emit("loadingData");
    },
    /**
     * Mô tả : off dialog
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 13:55 12/05/2022
     */
    showAlertDlg() {
      this.$emit("isShowDlgImportFirstOnClick", false);
      this.materialsToImport = [];
      this.dropzoneFile = null;
    },
  },

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

.m-table-validate {
  width: 200px;
  min-width: 200px;
  max-width: 200px;
  box-sizing: border-box;
}

.m-table-validate-action {
  width: 30px;
  min-width: 30px;
  max-width: 30px;
  box-sizing: border-box;
}

// pagination
.m-paging {
  display: flex;
  position: sticky;
  background-color: #fff;
  bottom: 0;
  left: 0;
  z-index: 5;
  // width: (100% - 32px);
  padding: 16px;
  font-size: 13px;
}

.m-paging .page-navigation {
  display: flex;
  align-items: center;
}

.page-navigation .page-index .page-num.selected {
  border: 1px solid #e0e0e0;
  font-weight: 700;
}

.page-navigation .page-index .page-num.hidden {
  display: none !important;
}
</style>
