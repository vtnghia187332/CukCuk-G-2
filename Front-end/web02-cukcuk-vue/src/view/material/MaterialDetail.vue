<template>
  <!-- dialog -->
  <div class="m-dialog">
    <div class="m-dialog-box">
      <div class="m-dialog m-dlg-detail">
        <div class="dialog-header dlg-header-show">
          <div class="m-popup-title">Thêm Nguyên vật liệu</div>
          <div class="m-icon-detail">
            <div class="m-popup-close">
              <i class="fa-solid fa-maximize"></i>
            </div>
            <div
              class="m-popup-close"
              @click="
                btnCloseDialogOnClick(
                  !isMatch(this.compareObject, this.material),
                  !isMatch(this.compareConvertions, this.convertions)
                )
              "
            >
              <i class="fa-solid fa-x"></i>
            </div>
          </div>
        </div>

        <div class="m-popup-content dlg-content-show dlg-content-detail">
          <div class="m-popup-content-text">
            <div class="m-form-detail">
              <div class="m-detail-name">
                Tên <span class="m-compulsory-input">(*)</span>
              </div>
              <input
                type="text"
                class="m-detail-input"
                id="txtMaterialName"
                ref="txtMaterialName"
                @input="inputMaterialName"
                @change="change"
                tabindex="1"
                v-model="material.MaterialName"
              />
            </div>
            <div class="m-form-detail">
              <div class="m-detail-name">
                Mã <span class="m-compulsory-input">(*)</span>
              </div>
              <input
                type="text"
                class="m-detail-input"
                id="txtMaterialCode"
                ref="txtMaterialCode"
                @input="inputMaterialCode"
                tabindex="2"
                v-model="material.MaterialCode"
              />
            </div>
          </div>
          <div class="m-popup-content-text">
            <div class="m-form-detail">
              <div class="m-detail-name">
                ĐVT <span class="m-compulsory-input">(*)</span>
              </div>
              <BaseCombobox
                ref="UnitId"
                :api="misaApi.getUnit"
                propTxt="UnitName"
                propValue="UnitId"
                v-model="this.material.UnitId"
                tabindex="3"
              />
            </div>
            <div class="m-form-detail">
              <div class="m-detail-name">Kho ngầm định</div>
              <BaseCombobox
                ref="StorageId"
                :api="misaApi.getStorage"
                propTxt="StorageName"
                propValue="StorageId"
                v-model="this.material.StorageId"
                tabindex="4"
              />
            </div>
          </div>
          <div class="m-popup-content-text">
            <div class="m-form-detail">
              <div class="m-detail-name">Hạn sử dụng</div>
              <BaseDatepicker tabindex="5" v-model="dateFormat" />
            </div>
            <div class="m-form-detail">
              <div class="m-detail-name">SL tồn tối thiểu</div>
              <input
                v-model="this.material.MaterialStockMin"
                type="text"
                class="m-detail-input"
                tabindex="6"
              />
            </div>
          </div>
          <div class="m-popup-content-text">
            <div class="m-form-detail m-form-detail-des">
              <div class="m-detail-name m-detail-name-des">Mô tả</div>
              <textarea
                v-model="this.material.MaterialNote"
                class="m-detail-input m-detail-input-des"
                name=""
                id=""
                cols=""
                rows=""
                tabindex="7"
              ></textarea>
            </div>
          </div>
          <div class="m-header-form-controls">
            <div
              class="m-title-form-control"
              @click="ConvertUnit"
              v-bind:class="{
                'm-title-form-control-active': this.isActiveTabUnit == true,
              }"
            >
              Đơn vị chuyển đổi
            </div>
            <div
              class="m-title-form-control"
              @click="ConverHistoryPhone"
              v-bind:class="{
                'm-title-form-control-active':
                  this.isActiveTabHistoryPhone == true,
              }"
            >
              Lịch sử thuê bao
            </div>
            <!-- <div class="m-title-form-control">Quản lý thiết bị</div> -->
          </div>

          <!-- table -->
          <div class="m-grid m-grid-detail" v-if="this.isActiveTabUnit == true">
            <div class="m-table-grid-flex">
              <div class="m-frame-table m-frame-table-detail">
                <table class="m-table" cellspacing="0" cellpadding="0">
                  <thead>
                    <tr>
                      <th class="m-sequenc-number txt-center">STT</th>
                      <th class="m-name-convertion">Đơn vị chuyển đổi</th>
                      <th class="m-rate-number">Tỉ lệ chuyển đổi</th>
                      <th class="m-cal-convertion">Phép tính</th>
                      <th class="m-des-convertion">Mô tả</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr
                      v-for="(convertion, index) in convertions"
                      :key="convertion.ConvertionId"
                      @dblclick="handleConvertionOnDbClick(convertion, index)"
                      :class="{
                        selectedRowCovertion: index == this.currentIndex,
                      }"
                    >
                      <td class="m-sequenc-number txt-center m-sequence-number">
                        {{ index + 1 }}
                      </td>
                      <td class="m-name-convertion txt-left">
                        <input
                          ref="txtConvertionName"
                          type="text"
                          class="m-input-table-convert"
                          v-model="convertion.ConvertionName"
                        />
                      </td>
                      <td class="m-rate-number">
                        <input
                          type="text"
                          class="m-input-table-convert txt-right"
                          v-model="convertion.RatingCovertion"
                          tabindex=""
                        />
                      </td>
                      <td class="m-cal-convertion txt-left" style="padding: 0">
                        <BaseCombobox
                          style="outline: none; width: 249px"
                          :defaultData="this.arrayCalculation"
                          v-model="convertion.ConvertionCalculation"
                          propTxt="ConvertionCalculationName"
                          propValue="ConvertionCalculation"
                          tabindex=""
                        />
                      </td>
                      <td class="m-des-convertion txt-left">
                        <input
                          type="text"
                          class="m-input-table-convert"
                          :value="updateFormatDescription(convertion)"
                          tabindex=""
                        />
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
          <!-- /table -->
          <div
            class="m-grid m-grid-detail"
            v-if="this.isActiveTabHistoryPhone == true"
          ></div>
          <div class="m-table-detail-icon">
            <button
              tabindex=""
              class="m-btn m-btn-wicon fit-center m-btn-detail-table"
              @click="addConvertionOnClick"
            >
              <div class="m-btn-icon m-add-data-row"></div>
              <div class="m-btn-text">Thêm dòng</div>
            </button>
            <button
              class="m-btn m-btn-wicon fit-center m-btn-detail-table"
              @click="deleteConvertionRowSelected"
              :disabled="this.currentIndex == null"
              tabindex=""
            >
              <div class="m-btn-icon m-cancel-icon"></div>
              <div class="m-btn-text">Xóa dòng</div>
            </button>
          </div>
        </div>
        <div class="m-popup-footer m-detail-footer">
          <div class="left-detail-footer" @click="Test">
            <button class="m-btn m-btn-wicon fit-center m-btn-fotter">
              <div class="m-btn-icon m-help"></div>
              <div class="m-btn-text">Giúp</div>
            </button>
          </div>
          <div class="right-detail-footer">
            <button
              class="m-btn m-btn-wicon fit-center m-btn-fotter"
              @click="btnSaveOnClick(false)"
              tabindex=""
            >
              <div class="m-btn-icon m-addition-db"></div>
              <div class="m-btn-text">Cất</div>
            </button>
            <button
              class="m-btn m-btn-wicon fit-center m-btn-fotter"
              @click="btnSaveOnClick(true)"
              tabindex=""
            >
              <div class="m-btn-icon m-addition-more-db"></div>
              <div class="m-btn-text">Cất và thêm</div>
            </button>
            <button
              class="m-btn m-btn-wicon fit-center m-btn-fotter"
              tabindex=""
              @click="
                btnCloseDialogOnClick(
                  !isMatch(this.compareObject, this.material),
                  !isMatch(this.compareConvertions, this.convertions)
                )
              "
            >
              <div class="m-btn-icon m-cancel-add-icon"></div>
              <div class="m-btn-text">Hủy bỏ</div>
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- /dialog -->
</template>

<script>
// eslint-disable-next-line
/*eslint-disable */
import axios from "axios";
import BaseCombobox from "@/view/baseCombobox/BaseCombobox.vue";
import BaseDatepicker from "@/view/datePicker/DatePicker.vue";

export default {
  components: { BaseCombobox, BaseDatepicker },
  props: [
    "isShow",
    "formMode",
    "materialSelectedInChild",
    "isSaveData",
    "isCancelData",
    "isShowErrorInput",
    "isCloseDialogWithCondition",
  ],
  data() {
    return {
      // array calculation
      arrayCalculation: [
        {
          ConvertionCalculationName: "+",
          ConvertionCalculation: 0,
        },
        {
          ConvertionCalculationName: "-",
          ConvertionCalculation: 1,
        },
        {
          ConvertionCalculationName: "*",
          ConvertionCalculation: 2,
        },
        {
          ConvertionCalculationName: "/",
          ConvertionCalculation: 3,
        },
      ],
      // Time để clear TimeOut
      timeoutId: 0,

      //Khởi tạo mảng để hứng các địa chỉ ô lỗi validation trả về
      erroInputForm: [],
      txtErrorInput: [],

      // Biến lưu trữ ô input đầu tiên bị lỗi
      firstInputErr: null,

      // Đã vượt qua tất cả các validation
      isValid: true,

      //Đối tượng để so sánh
      compareObject: {},

      // Đối tượng lưu thông tin của nhân viên
      material: {},

      // Mảng chứa danh sách đơn vị chuyển đổi
      convertions: [],

      // Đối tượng chuyển đổi được chọn
      convertionSelected: {},

      currentIndex: null,

      // danh sách đối tượng chuyển đổi dữ liệu
      convertionForUpdate: [],
      convertionForInsert: [],

      // Mảng dữ liệu để so sánh đối tượng chuyển đổi
      compareConvertions: [],

      // Set on - off form small table
      isActiveTabUnit: true,
      isActiveTabHistoryPhone: false,

      // Activate row Selected Convertion
      isSelectedConvertion: false,

      // Thực hiện thêm mới những đối tượng nguyên vật liệu và bộ chuyển đổi
      _materialWConvertions: {
        material: {},
        convertions: [],
      },
    };
  },
  watch: {
    // Bấm nút btn "đồng ý" sau khi sửa dữ liệu mà bấm nút "thoát"
    isSaveData: function (newValue) {
      if (newValue) {
        this.SaveAfterChangeData();
      }
    },
    isCloseDialogWithCondition: function (newValue) {
      if (newValue) {
        this.btnCloseDialogOnClick(
          !this.isMatch(this.compareObject, this.material),
          !isMatch(this.compareConvertions, this.convertions)
        );
      }
    },
    // Sau khi thoát alert dialog -> Focus và ô input bị lỗi
    isShowErrorInput: function () {
      // focus vào ô đầu tiên
      if (this.firstInputErr) {
        this.$nextTick(() => {
          this.$refs[this.firstInputErr].focus();
        });
      }
    },
  },
  computed: {
    /**
     * Mô tả : Format date - picker (Chỉnh lại ngày tháng bị lệch)
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 08:09 27/04/2022
     */
    dateFormat: {
      get() {
        if (!this.material.MaterialExpiry) {
          return;
        } else {
          return new Date(this.material.MaterialExpiry);
        }
      },
      set(newValue) {
        this.material.MaterialExpiry = newValue.toDateString();
      },
    },
  },
  methods: {
    /**
     * Mô tả : Kiểm tra format của code
     * @param: strCode:  Mã code truyền vào
     * @returns: true: Đúng định dạng || false: không đúng định dạng
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 13:52 22/05/2022
     */
    checkFormatEmpCode(strCode) {
      return /[0-9]+$/.test(strCode);
    },
    /**
     * Mô tả : Thay đổi format của ô miêu tả bộ chuyển đổi (description convertion)
     * @return: format lại bộ chuyển đổi
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 08:42 22/05/2022
     */
    updateFormatDescription(convertion) {
      let desConvertion = null;
      if (
        convertion.ConvertionName &&
        this.material.UnitName &&
        convertion["RatingCovertion"] &&
        convertion["ConvertionCalculationName"]
      ) {
        desConvertion =
          "1" +
          " " +
          convertion.ConvertionName +
          "=" +
          " " +
          convertion["RatingCovertion"] +
          " " +
          convertion["ConvertionCalculationName"] +
          " " +
          this.material.UnitName;
        return desConvertion;
      } else {
        return null;
      }
    },

    /**
     * Mô tả : Thêm dòng mới vào bảng Covertion
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 14:38 17/05/2022
     */
    addConvertionOnClick() {
      this.convertion = {};
      this.convertionSelected = {};
      this.convertion.MaterialId = this.material.MaterialId;
      // Thêm mảng rỗng vào mảng để chuẩn bị xử lý
      this.convertions.push(this.convertion);
    },

    /**
     * Mô tả : Chọn đối tượng convertionSelected
     * @param: convertion
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 10:49 17/05/2022
     */
    handleConvertionOnDbClick(convertion, index) {
      if (this.currentIndex == index) {
        this.currentIndex = null;
        this.convertionSelected = {};
      } else {
        this.currentIndex = index;
        this.convertionSelected = convertion;
      }
    },

    /**
     * Mô tả : Xóa dòng convertion được chọn
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 11:16 18/05/2022
     */
    deleteConvertionRowSelected() {
      // Nếu tồn tại ConvertionId -> Gọi Api để xóa
      // if (this.convertionSelected?.ConvertionId) {
      //   // gọi api để xóa convertion
      //    this.deleteConvertionById();
      //   // load lại data convertion
      //    this.getConvertionByMaterialId();
      // } else {
      // Nếu không tồn tại -> Xóa phần tử trong mảng
      this.convertions.splice(this.currentIndex, 1);
      // clear currentIndex của row bảng convertions
      this.currentIndex = null;
      // }
    },

    /**
     * Mô tả : Lấy dữ liệu bảng chuyển đổi đơn vị theo MaterialId(Danh mục bảng Material)
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 09:33 17/05/2022
     */
    async getConvertionByMaterialId() {
      let me = this;
      if (this.material?.MaterialId) {
        try {
          await axios
            .get(
              `${this.misaApi?.getConvertionByMaterialId}/${this.material?.MaterialId}`
            )
            .then(function (res) {
              if (res) {
                // đối tượng binding data
                me.convertions = res.data;
                // Xử lý mô tả của bộ chuyển đổi
                console.log(me.convertions);
              }
            })
            .catch(function (res) {
              console.log(res);
            });
        } catch (error) {
          console.log(error);
        }
      } else return;
    },

    /**
     * Mô tả : Gọi Api để xóa bản ghi chuyển đổi dữ liệu
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 14:10 18/05/2022
     */
    async deleteConvertionById() {
      let me = this;
      try {
        await axios
          .delete(
            `${this.misaApi?.deleteConvertion}/${this.convertionSelected?.ConvertionId}`
          )
          .then(function (res) {
            if (res) {
              // Load lại dữ liệu
            }
          })
          .catch(function (res) {
            console.log(res);
          });
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả :Sửa dữ liệu bảng chuyển đổi đơn vị
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 09:33 17/05/2022
     */
    async updateConvertionByMaterialId() {
      let me = this;
      if (me.material?.MaterialId) {
        try {
          await axios
            .put(
              `${me.misaApi?.updateMulConvertionByMaterialId}/${me.material?.MaterialId}`,
              me.convertionForUpdate
            )
            .then(function (res) {
              if (res) {
                if (me.convertionForUpdate) {
                  me.convertionForUpdate = [];
                }
              }
            })
            .catch(function (res) {
              console.log(res);
            });
        } catch (error) {
          console.log(error);
        }
      } else return;
    },

    /**
     * Mô tả : Thêm mới bản ghi convertion
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 14:48 17/05/2022
     */
    async insertConvertionByMaterialId() {
      let me = this;
      if (this.material?.MaterialId) {
        try {
          await axios
            .post(
              `${this.misaApi?.insertMulConvertionByMaterialId}/${this.material?.MaterialId}`,
              this.convertionForInsert
            )
            .then(function (res) {
              if (res) {
                if (me.convertionForInsert) {
                  me.convertionForInsert = [];
                }
              }
            })
            .catch(function (res) {
              console.log(res);
            });
        } catch (error) {
          console.log(error);
        }
      } else return;
    },

    /**
     * Mô tả : Hàm validate data
     * @param: truyền vào 1 đối tượng
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 17:26 23/04/2022
     */
    validateData(object) {
      // Xóa toàn bộ dữ liệu ô input mới
      this.clearErrDataInput();
      if (object) {
        /**
         * Mô tả : Bắt sự kiện bắt buộc phải nhập code
         * Created by: Vũ Trọng Nghĩa - MF1108
         * Created date: 09:27 01/04/2022
         */
        if (!object.MaterialCode) {
          this.txtErrorInput?.push(
            "Mã Nguyên vật liệu không được phép để trống"
          );
          this.isValid = false;
          this.erroInputForm?.push(this.$refs.txtMaterialCode);
        }
        if (
          object.MaterialCode &&
          !this.checkFormatEmpCode(object.MaterialCode)
        ) {
          this.txtErrorInput?.push("Mã Nguyên vật liệu sai định dạng");
          this.isValid = false;
          this.erroInputForm?.push(this.$refs.txtMaterialCode);
        }

        /**
         * Mô tả : Validate độ dài dữ liệu code
         * Created by: Vũ Trọng Nghĩa - MF1108
         * Created date: 09:27 01/04/2022
         */
        if (object.MaterialCode && object.MaterialCode.length >= 20) {
          this.txtErrorInput?.push(
            "Độ dài của Mã Nguyên vật liệu không vượt 20 kí tự"
          );
          this.isValid = false;
          this.erroInputForm?.push(this.$refs.txtMaterialCode);
        }

        /**
         * Mô tả : Valitedate dữ liệu tên Nguyên vật liệu không được phép để trống
         * Created by: Vũ Trọng Nghĩa - MF1108
         * Created date: 09:26 01/04/2022
         */
        if (!object.MaterialName) {
          this.txtErrorInput?.push("Tên Nguyên vật liệu không được để trống");
          this.erroInputForm?.push(this.$refs.txtMaterialName);
          this.isValid = false;
        }
        if (!object.UnitId) {
          this.isValid = false;
          this.$refs.UnitId?.setError();
        }
        // Sau khi check xong -> Set biến isValid = true
      } else {
        console.log("Không có dữ liệu đâu nhé !!");
        return;
      }
    },

    /**
     * Mô tả : xóa toàn bộ dữ liệu - css ô input lỗi
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 15:14 24/04/2022
     */
    clearErrDataInput() {
      // isValid = true -> Nếu không xảy ra lỗi -> Tiếp tục bước tiếp
      this.isValid = true;
      // clear toàn bộ ô input lỗi
      this.erroInputForm = [];
      // cler txt lỗi
      this.txtErrorInput = [];
      // xóa css lỗi các ô input
      let clearInputErr = document.querySelectorAll(".m-error-input");
      for (let i = 0; i < clearInputErr.length; i++) {
        clearInputErr[i].classList.remove("m-error-input");
      }
      let clearOutInputErr = document.querySelectorAll(".outline-error-input");
      for (let i = 0; i < clearOutInputErr.length; i++) {
        clearOutInputErr[i].classList.remove("outline-error-input");
      }
    },

    /**
     * Mô tả : Tìm địa chỉ ô input bị lỗi và border đỏ vào ô input
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 10:06 24/04/2022
     */
    warningErrorInput() {
      if (this.erroInputForm) {
        //  if isValid = false - có lỗi ở ô input
        for (let i = 0; i < this.erroInputForm.length; i++) {
          this.firstInputErr = this.erroInputForm[0].id;
          //xóa focus ô bị lỗi
          this.erroInputForm[i].classList.add("outline-error-input");
          //thêm border error vào ô input lỗi
          this.erroInputForm[i].classList.add("m-error-input");
        }
        // Hiển thị cảnh báo lỗi
        for (let i = 0; i < this.txtErrorInput.length; i++) {
          this.showWarningDialog(this.txtErrorInput[0]);
        }
      }
    },

    /**
     * Mô tả : Call api và lấy mã nguyên liệu mới
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 15:24 13/05/2022
     */
    async getNewMaterialCode() {
      let me = this;
      try {
        me.material = {};
        await axios
          .get(this.misaApi?.getNewMaterialCode)
          .then(function (res) {
            me.material.MaterialCode = res.data;
            // Clear biến so sánh đối tượng nguyên vật liệu

            me.compareObject = {};
            me.compareObject.MaterialCode = me.material.MaterialCode;
          })
          .catch(function (res) {
            console.log(res);
          });
        // this.showOrHideDialog(true);
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Mô tả : Active tab(router) trong table MaterialDetail
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 11:06 11/05/2022
     */
    ConvertUnit() {
      this.isActiveTabUnit = true;
      this.isActiveTabHistoryPhone = false;
    },
    ConverHistoryPhone() {
      this.isActiveTabUnit = false;
      this.isActiveTabHistoryPhone = true;
    },

    /**
     * Mô tả : Forcus vào ô imput
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 17:01 20/04/2022
     */
    async focusFirstInput() {
      await this.$nextTick(() => {
        this.$refs.txtMaterialName.focus();
      });
    },

    /**
     * Mô tả : reloading data
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 21:35 14/04/2022
     */
    loadingData() {
      this.$emit("loadingData");
    },
    /**
     * Mô tả : Hiển thị dialog warning
     * @param msg: Thông điệp muốn hiển thị lên form
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 20:59 15/04/2022
     */
    showWarningDialog(msg) {
      this.$emit("handleAlertOnClick", msg);
    },

    /**
     * Mô tả : Xử lý sự kiện bấm nút Cất
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 09:32 01/04/2022
     */
    async btnSaveOnClick(isAddAndSave) {
      //Tách biệt - insert convertion / update convertion
      for (const _convertion of this.convertions) {
        if (_convertion.ConvertionId) {
          //Check tồn ConvertionId -> add vào mảng convertionCompare
          for (const _compareConvertion of this.compareConvertions) {
            //so sánh -> nếu có sự thay đổi -> add vào mảng convertionForUpdate
            if (
              _convertion.ConvertionId == _compareConvertion.ConvertionId &&
              !this.isMatch(_convertion, _compareConvertion)
            ) {
              // Nếu tồn tại ConvertionId -> Update
              this.convertionForUpdate.push(_convertion);
            } else {
              continue;
            }
          }
        }
        // Không tồn tại ConvertionId -> insert
        else {
          this.convertionForInsert.push(_convertion);
        }
      }

      let me = this;
      // Gán đối tượng mới
      let material = me.material;
      // validate data
      me.validateData(material);
      if (me.isValid) {
        // Gọi api thực hiện lưu dữ liệu
        if (me.formMode == me.misaEnum.formMode?.Add) {
          // Thêm mới bản ghi
          // Gán vào đối tượng để Gọi Api thực hiện querry
          me._materialWConvertions.material = material;
          me._materialWConvertions.convertions = me.convertions;
          /**
           * Mô tả : Gọi API để thực hiện thêm dữ liệu
           * @param Enum MISAEnum.FormMode.Add = 1 - CREATE
           * Created by: Vũ Trọng Nghĩa - MF1108
           * Created date: 09:25 01/04/2022
           */
          await axios
            .post(
              `${me.misaApi.postMaterialWithConvertions}`,
              me._materialWConvertions
            )
            .then(function (res) {
              if (res) {
                // reset đối tượng Emp đang được xử lý
                me.material = {};
                me.convertions = [];

                if (isAddAndSave) {
                  // Lấy mã mới
                  me.getNewMaterialCode();
                  // focus vòa ô mã nguyên liệu
                  me.focusFirstInput();
                } else {
                  //đóng form
                  me.btnCloseDialogOnClick();
                }
                // Load lại data
                me.loadingData();
              }
            })
            .catch(function (res) {
              //  Hiển thị dialog cảnh bảo
              console.log(res);
              if (res.response.data.Data) {
                me.showWarningDialog(res.response.data.Data.MaterialCode);
              }
            });
        } else if (me.formMode == me.misaEnum.formMode?.Duplicate) {
          // Nhân bản bản ghi
          // Gán vào đối tượng để Gọi Api thực hiện querry
          me._materialWConvertions.material = material;
          me._materialWConvertions.convertions = me.convertions;
          await axios
            .post(
              `${me.misaApi.postMaterialWithConvertions}`,
              me._materialWConvertions
            )
            .then(function (res) {
              if (res) {
                // reset đối tượng Emp đang được xử lý
                me.material = {};
                me.convertions = [];
                // Load lại data
                me.loadingData();
                me.btnCloseDialogOnClick();
              }
            })
            .catch(function (res) {
              //  Hiển thị dialog cảnh bảo
              console.log(res);
              if (res.response?.data?.Data) {
                me.showWarningDialog(res.response.data.Data.MaterialCode);
              }
            });
        } else {
          // Cập nhật bộ chuyển đổi theo Id Material
          if (this.convertionForUpdate?.length != 0) {
            //Cập nhật bảng chuyển đổi
            await this.updateConvertionByMaterialId();
          }
          // Thêm bộ chuyển đổi theo Id Material
          if (this.convertionForInsert?.length != 0) {
            //Thêm bảng chuyển đổi
            await this.insertConvertionByMaterialId();
          }
          // Xóa bộ chuyển đổi theo Id Mateiral
          if (this.convertionSelected?.ConvertionId) {
            // gọi api để xóa convertion
            this.deleteConvertionById();
            // load lại data convertion
            this.getConvertionByMaterialId();
          }
          /**
           * Mô tả : Gọi API để sửa dữ liệu nhân viên đã được chọn
           * Created by: Vũ Trọng Nghĩa - MF1108
           * Created date: 09:24 01/04/2022
           */
          await axios
            .put(
              `http://localhost:5236/api/v1/Materials/${material.MaterialId}`,
              material
            )
            .then(function (res) {
              if (res) {
                // reset đối tượng Emp đang được xử lý
                me.material = {};
                me.convertions = [];
                if (isAddAndSave) {
                  // Lấy mã mới
                  me.getNewMaterialCode();
                  // focus vòa ô mã nguyên liệu
                  me.focusFirstInput();
                } else {
                  //đóng form
                  me.btnCloseDialogOnClick();
                }
                // load lại dữ liệu
                me.loadingData();
              }
            })
            .catch(function (res) {
              //  Hiển thị dialog cảnh bảo
              console.log(res);
              if (res.response?.data?.Data) {
                me.showWarningDialog(res.response.data.Data.MaterialCode);
              }
            });
        }
      } else {
        me.warningErrorInput();
      }
    },

    /**
     * Mô tả : Đóng form Detail - gọi lên MaterialList
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 21:00 15/04/2022
     */
    btnCloseDialogOnClick(isMatchMaterial = false, isMatchConvertion = false) {
      console.log(this.isMatch(this.compareConvertions, this.convertions));
      if (isMatchMaterial || isMatchConvertion) {
        // Nếu thay đổi, hiển thị thông báo xác nhận -> đóng form
        this.$emit(
          "handleConfirmDlg",
          "Dữ liệu đã bị thay dổi, bạn có muốn cất không?",
          true
        );
      } else {
        // Nếu không thay đổi dữ liệu -> đóng form
        this.$emit("closeOnClick", false);
      }
    },

    // Đồng ý sau khi thay đổi dữ liệu
    SaveAfterChangeData() {
      // Gọi hàm -> validate & call api
      this.btnSaveOnClick(false);
      // Đóng form
      this.$emit("showConfirmDlg", false);
    },

    /**
     * Mô tả : So sánh giữa 2 object với nhau
     * @return true: nếu giống nhau | false: nếu khác nhau
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 08:30 13/05/2022
     */
    isMatch(object, compareObject) {
      const isSameObject =
        JSON.stringify(object) === JSON.stringify(compareObject);
      return isSameObject;
    },

    /**
     * Mô tả : Khi KH input tên MaterialName -> format lại MaterialCode
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 14:32 16/05/2022
     */
    inputMaterialName() {
      // Clear timeout
      if (this.timeoutId >= 0) {
        clearTimeout(this.timeoutId);
      }
      this.timeoutId = window.setTimeout(() => {
        // Lấy words của MaterialName
        let words = this.convertTxtCode(String(this.material.MaterialName));
        // Lấy num của MaterialCode
        let num = this.convertNumCode(String(this.material.MaterialCode));
        // Trả về code
        this.material.MaterialCode = words + "-" + num;
      }, 500);
    },

    /**
     * Mô tả : Convert chuỗi -> lấy Uppercase letter đầu tiên của từ
     * @param: sentence : chuỗi cần convert
     * @return Uppercase letter
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 14:02 16/05/2022
     */
    convertTxtCode(sentences) {
      // spilt word
      let reg = /(?<!\p{L}\p{M}*)\p{L}\p{M}*/gu;
      let txtKeyWord = sentences?.match(reg).join("");
      return txtKeyWord;
    },

    /**
     * Mô tả : Convert chuỗi lấy ra số kí tự
     * @param   MaterialCode
     * @return NumberCode
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 14:48 16/05/2022
     */
    convertNumCode(sentences) {
      // split number
      let numberKeyWord = sentences?.match(/\d+/g)[0];
      return numberKeyWord;
    },
  },

  async beforeMount() {
    // Khi form được hiện lên (mounted vào DOM)
    // Gán đối tượng để khi đóng form >> so sánh xem có thay đổi
    this.material = this.materialSelectedInChild;

    // Gọi api lấy danh sách chuyển đổi đơn vị ứng với MaterialId
    await this.getConvertionByMaterialId();

    if (this.material) {
      // Format MaterialCode
      // Nếu tồn tại tên bộ chuyển đổi
      let words = null;
      if (this.material.MaterialName) {
        words = this.convertTxtCode(String(this.material.MaterialName));
      } else {
        //Convert code Bộ chuyển đổi được trả về từ server
        words = this.convertTxtCode(String(this.material.MaterialCode));
      }
      let num = this.convertNumCode(String(this.material.MaterialCode));
      this.material.MaterialCode = words + "-" + num;
    }
    // Gán vào biến tạm để so sánh 2 object (so sánh sự thay đổi khi đóng form)
    this.comcompareObject = {};
    Object.assign(this.compareObject, this.material);

    // Gán vào biến tạm để so sánh 2 object khi thực hiện thêm mới - sửa bộ chuyển đổi
    // Khi sửa: cơ chế v-model ảnh hưởng lên mảng ban đầu converions()
    this.compareConvertions = JSON.parse(JSON.stringify(this.convertions));
  },
  mounted() {
    // Focus vào ô đầu tiên
    this.$nextTick(() => {
      this.$refs.txtMaterialName?.focus();
    });
  },
};
</script>

<style></style>
