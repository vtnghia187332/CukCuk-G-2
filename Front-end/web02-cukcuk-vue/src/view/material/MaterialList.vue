<template>
  <div>
    <div class="m-content-title">
      <div class="m-txt-title">Nguyên vật liệu</div>
      <div class="m-btn-feedback">
        <button class="m-btn m-btn-wicon fit-center">
          <div class="m-btn-icon m-addition-icon"></div>
          <div class="m-btn-text">Phản hồi</div>
        </button>
      </div>
    </div>
    <div class="m-btn-functions">
      <button class="m-btn m-btn-wicon fit-center btn-content" @click="btnAddOnClick">
        <div class="m-btn-icon m-addition-more-db"></div>
        <div class="m-btn-text">Thêm</div>
      </button>
      <button class="m-btn m-btn-wicon fit-center btn-content" :disabled="this.materialSelected.MaterialId == null"
        @click="handleDuplicateObject">
        <div class="m-btn-icon m-duplicate"></div>
        <div class="m-btn-text">Nhân bản</div>
      </button>
      <button class="m-btn m-btn-wicon fit-center btn-content" :disabled="this.materialSelected.MaterialId == null"
        @click="clickRowOnUpdate">
        <div class="m-btn-icon m-update"></div>
        <div class="m-btn-text">Sửa</div>
      </button>
      <button class="m-btn m-btn-wicon fit-center btn-content" :disabled="this.checkedaAssetList.length == 0"
        @click="deleteRowHandle">
        <div class="m-btn-icon m-cancel-add-icon"></div>
        <div class="m-btn-text">Xóa</div>
      </button>
      <button class="m-btn m-btn-wicon fit-center btn-content" @click="loadData">
        <div class="m-btn-icon m-refresh"></div>
        <div class="m-btn-text">Nạp</div>
      </button>

      <button class="m-btn m-btn-wicon fit-center btn-content" @click="isShowDlgImportFirstOnClick(true)">
        <div class="m-btn-icon"><i class="fa-solid fa-file-import"></i></div>
        <div class="m-btn-text">Nhập khẩu</div>
      </button>

      <button class="m-btn m-btn-wicon fit-center btn-content" @click="handleExportOnClick">
        <div class="m-btn-icon"><i class="fa-solid fa-download"></i></div>
        <div class="m-btn-text">Xuất khẩu</div>
      </button>
    </div>
    <div class="m-content-grid">
      <div class="m-grid m-grid-content">
        <div class="m-table-grid-flex">
          <div class="m-frame-table m-frame-table-content">
            <table class="m-table" cellspacing="0" cellpadding="0">
              <thead>
                <tr>
                  <th class="m-chkbox">
                    <div class="txt-grid-content"></div>
                    <div class="input-grid-content">
                      <input class="checkbox" type="checkbox" @click="selAllMaterialOnClick" :checked="
                        this.checkedaAssetList.length == this.materials.length
                      " />
                    </div>
                  </th>
                  <th class="m-150">
                    <div class="txt-grid-content">Mã nguyên vật liệu</div>
                    <div class="input-grid-content">
                      <BaseInputGrid @getValueToQuery="getValueToQuery" keyword="MaterialCode" />
                    </div>
                  </th>
                  <th class="m-150">
                    <div class="txt-grid-content">Tên nguyên vật liệu</div>
                    <div class="input-grid-content">
                      <BaseInputGrid keyword="MaterialName" @getValueToQuery="getValueToQuery" />
                    </div>
                  </th>
                  <th class="m-150">
                    <div class="txt-grid-content">Tính chất</div>
                    <div class="input-grid-content">
                      <BaseInputGrid keyword="MaterialFeature" @getValueToQuery="getValueToQuery" />
                    </div>
                  </th>
                  <th class="m-150">
                    <div class="txt-grid-content">ĐVT tính</div>
                    <div class="input-grid-content">
                      <BaseInputGrid keyword="UnitName" @getValueToQuery="getValueToQuery" />
                    </div>
                  </th>
                  <th class="m-150">
                    <div class="txt-grid-content">Nhóm nguyên vật liệu</div>
                    <div class="input-grid-content">
                      <BaseInputGrid keyword="GroupMaterial" @getValueToQuery="getValueToQuery" />
                    </div>
                  </th>

                  <th class="m-500">
                    <div class="txt-grid-content">Ghi chú</div>
                    <div class="input-grid-content">
                      <BaseInputGrid keyword="MaterialNote" @getValueToQuery="getValueToQuery" />
                    </div>
                  </th>

                  <th class="m-40">
                    <div class="txt-grid-content">Ngừng theo dõi</div>
                    <div class="input-grid-content">
                      <BaseInputGrid keyword="Following" @getValueToQuery="getValueToQuery"
                        :defaultData="this.selFollowingVals" :isHaveDrpGrid="true" propVal="val" propTxt="txt" />
                    </div>
                  </th>
                </tr>
              </thead>
              <div class="m-loader" v-if="showLoading"></div>
              <tbody v-if="!showLoading">
                <tr v-for="material in materials" :key="material.MaterialId" @click="activate(material)"
                  @dblclick="rowOnDBClick(material)">
                  <td class="m-chkbox txt-center">
                    <input class="checkbox" type="checkbox" :checked="checkedaAssetList.includes(material)" />
                  </td>
                  <td class="m-row-list">{{ material.MaterialCode }}</td>
                  <td class="m-row-list">{{ material.MaterialName }}</td>
                  <td class="m-row-list">{{ material.MaterialFeature }}</td>
                  <td class="m-row-list">{{ material.UnitName }}</td>
                  <td class="m-row-list">{{ material.GroupMaterial }}</td>
                  <td class="m-500-txt">{{ material.MaterialNote }}</td>
                  <td class="td-checkbox m-40">
                    <input class="checkbox" type="checkbox" :checked="material.Following" />
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
    <div class="m-pagination-cus">
      <div class="m-pagination">
        <button class="m-btn-changePage m-first-page" @click="goToFirstPage" :disabled="this.indexPage == 1">
          <i class="fa-solid fa-angles-left"></i>
        </button>
        <button class="m-btn-changePage m-prev-page" @click="goToPrevPage" :disabled="this.indexPage == 1">
          <i class="fa-solid fa-angle-left"></i>
        </button>
        <div class="m-pagination-text fit-center">Trang</div>
        <input type="text" class="m-current-pagination" @input="inputSearchPaging" v-model="this.indexPage" />
        <div class="m-total-pagination">
          trên <span>{{ this.totalPage }}</span>
        </div>
        <button class="m-btn-changePage m-next-page" @click="goToNextPage" :disabled="this.indexPage == this.totalPage">
          <i class="fa-solid fa-angle-right"></i>
        </button>
        <button class="m-btn-changePage m-last-page" @click="goToLastPage" :disabled="this.indexPage == this.totalPage">
          <i class="fa-solid fa-angles-right"></i>
        </button>
        <button class="m-btn-changePage m-refresh-page" @click="loadData">
          <i class="fa-solid fa-arrows-rotate"></i>
        </button>
        <select name="" id="selectionPagination" class="m-sel-pageSize" @click="changePageSize"
          :value="this.totalObjectPerPage">
          <option value="10">10</option>
          <option value="20">20</option>
          <option value="50">50</option>
          <option value="80">80</option>
          <option value="100">100</option>
        </select>
      </div>
      <div class="footer-txt-content">
        <div class="txt footer">
          Hiển thị <b>{{ this.totalObjectPerPage }}</b> trên
          <b>{{ this.totalObject }} </b>
          kết quả
        </div>
      </div>
    </div>
    <MaterialDetail @keydown.esc="closeDialogWithCondition" :isCloseDialogWithCondition="isCloseDialogWithCondition"
      v-if="isShowDialog" @closeOnClick="showOrHideDialog" @btnAddOnClick="btnAddOnClick"
      :materialSelectedInChild="materialSelected" @loadingData="loadData" :formMode="formMode"
      @handleConfirmDlg="handleConfirmDlg" :isSaveData="isSaveData" :isCancelData="isCancelData"
      @showConfirmDlg="showConfirmDlg" @handleAlertOnClick="handleAlertOnClick" :isShowErrorInput="isShowErrorInput" />
    <WarningDialog v-if="isShowWarningDlg" :txtWarningDlg="txtWarningDlg" @showWarningDlg="showWarningDlg"
      @agreeHanleOnClick="agreeHanleOnClick" />
    <ShowConfirm v-if="isShowConfirmDlg" :txtConfirmDlg="txtConfirmDlg" @showConfirmDlg="showConfirmDlg"
      @handleAfterChangeOnClick="handleAfterChangeOnClick" />
    <AlertConfirm v-if="isShowAlertDlg" :txtAlertDlg="txtAlertDlg" @showAlertDlg="showAlertDlg"
      @agreeAlertOnClick="agreeAlertOnClick" />
    <ImportDataFirst v-if="isShowImportDlgFirst" @isShowDlgImportFirstOnClick="isShowDlgImportFirstOnClick"
      @loadingData="loadData" />
  </div>
</template>

<script>
// eslint-disable-next-line
/*eslint-disable */
import axios from "axios";

import MaterialDetail from "@/view/material/MaterialDetail.vue";
import BaseInputGrid from "@/view/baseInput/BaseInputGrid.vue";
import WarningDialog from "@/view/notification/WarningDialog.vue";
import ShowConfirm from "@/view/notification/ShowConfirm.vue";
import AlertConfirm from "@/view/notification/AlertConfirm.vue";
import ImportDataFirst from "@/components/Import-Data/Import-Data-First.vue";

export default {
  components: {
    MaterialDetail,
    BaseInputGrid,
    WarningDialog,
    ShowConfirm,
    AlertConfirm,
    ImportDataFirst,
  },
  data() {
    return {
      // Danh sách nguyên vật liệu có trong Database
      allMaterials: [],
      // Mở form dialog import bước đầu tiên
      isShowImportDlgFirst: false,

      //Tạo mảng để hứng các nguyên vật liệu được chọn
      checkedaAssetList: [],

      //Các giá trị lựa chọn của Following
      selFollowingVals: [
        {
          txt: "Có",
          val: 1,
        },
        {
          txt: "Không",
          val: 0,
        },
      ],
      isHaveDrpGrid: true,
      // on - off confirm dialog
      isShowConfirmDlg: false,
      //txt-confirm-dlg
      txtConfirmDlg: null,
      // Đồng ý cất dữ liệu sau khi sửa form
      isSaveData: false,
      // Không cất dữ liệu sau khi sửa form
      isCancelData: false,
      // kiểm tra thay đổi khi đóng form
      isCheckChange: false,

      // on - off warning dialog
      isShowWarningDlg: false,
      //txt-warning-dlg
      txtWarningDlg: null,

      isShowAlertDlg: false,
      txtAlertDlg: null,
      isShowErrorInput: false,

      // Bấm nút esc để close dialog
      isCloseDialogWithCondition: false,

      // Show dialog toán tử
      isShowCodeOperator: false,
      isShowNameOperator: false,
      isShowFeatureOperator: false,
      isShowUnitOperator: false,
      isShowGroupOperator: false,
      isShowNoteOperator: false,
      isShowFollowingOperator: false,

      // Boolean isShowDialog: Tắt / bật dialog thêm mới
      isShowDialog: false,

      //danh sách nguyên vật liệu
      materials: [],

      //showLoading: Hiển thị loading dữ liệu,
      showLoading: false,

      // Enum formMode: Trả về giá trị để xác định điều kiện cho hành động -Thêm -sửa-xóa
      formMode: this.misaEnum.formMode.Add,

      //Object : đối tượng được chọn đã được chọn
      materialSelected: {},

      /** */
      // Lưu trữ số lượng row hiển thị trên 1 trang
      totalObjectPerPage: 10,
      // Tổng số trang nhân viên
      totalPage: 0,
      // Lưu trữ tổng số lượng đối tượng trên api
      totalObject: 0,
      // Lưu trữ trang index đang focus
      indexPage: 1,
      // Mảng hứng dữ liệu filter
      filterOption: [],
      /** */
    };
  },

  methods: {
    /**
     * Mô tả : Xử lý sự kiện export nguyên vật liệu ra file excel
     * @param: mảng chứa nguyên vật liệu cần export ra file excel
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 08:24 30/05/2022
     */
    async handleExportOnClick(materialToExcel) {
      materialToExcel = this.checkedaAssetList;
      // Nếu trong mảng chứa phần tử -> Xuất file excel các phần tử đó
      if (materialToExcel.length > 0) {
        await this.handleExport(materialToExcel);
        this.checkedaAssetList = [];
      }
      // Nếu không chứa -> Xuất toàn bộ nguyên vật liệu có trong Database
      else {
        await this.handleExport(materialToExcel);
        this.checkedaAssetList = [];
      }
    },



    /**
     * Mô tả : Gọi Api để xử lý hàm xuất khẩu ra file excel
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 08:30 30/05/2022
     */
    async handleExport(materialToExport) {
      let me = this;
      // time biến thành tên của file
      const tempDateTime = new Date();
      const fileName = `Nguyen_vat_lieu${tempDateTime.getTime()}.xlsx`;
      //  Khai báo mảng để hứng dữ liệu nguyên vật liệu trả về
      await axios
        .post(`${me.misaApi.exportMaterialToExcel}`, materialToExport, {
          responseType: "blob",
          contentType: "application/json-patch+json",
        })
        .then(function (res) {
          if (res) {
            var url = window.URL.createObjectURL(new Blob([res.data]));
            console.log(url);
            var a = document.createElement("a");
            a.href = url;
            //Lấy file name mà server trả về -> save file
            a.download = fileName;
            document.body.appendChild(a); // we need to append the element to the dom -> otherwise it will not work in firefox
            a.click();
            a.remove(); //afterwards we remove the element again
          }
        })
        .catch(function (res) {
          console.log(res);
        });
    },

    /**
     * Mô tả :Hiển thị dialog nhập khẩu
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 11:29 27/05/2022
     */
    isShowDlgImportFirstOnClick(isShow) {
      this.isShowImportDlgFirst = isShow;
    },

    /**
     * Mô tả : Bấm nút esc để close dialog detail
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 09:48 22/05/2022
     */
    closeDialogWithCondition() {
      this.isCloseDialogWithCondition = !this.isCloseDialogWithCondition;
    },
    /**
     * Mô tả : Nhân bản 1 đối tượng
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 16:26 13/05/2022
     */
    async handleDuplicateObject() {
      this.formMode = this.misaEnum.formMode.Duplicate;
      if (this.materialSelected) {
        // Lấy mã mới về cho Material
        this.callApiGetMaterialCode();
      }
      // 1 Lấy thông tin nhân viên từ Api
      this.showOrHideDialog(true);
    },
    /**
     * Mô tả : on - off confirm dialog
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 08:51 13/05/2022
     */
    showConfirmDlg(isShow) {
      this.isShowConfirmDlg = isShow;
      if (this.isCheckChange) {
        this.isShowDialog = false;
        this.materialSelected = {};
      }
    },
    /**
     * Mô tả : Xử lý hàm hiển thị confirm dialog
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 17:18 12/05/2022
     */
    handleConfirmDlg(txtConfirm, isCheck) {
      this.txtConfirmDlg = txtConfirm;
      //Hiển thị dialog
      this.isShowConfirmDlg = true;
      this.isCheckChange = isCheck;
    },
    /**
     * Mô tả : Xử lý sự kiện đồng ý cất dữ liệu
     * Created by: Vũ Trọng Nghĩa - MF1108
     * @param: isSave - Cất dữ liệu || isCancel - Không cất dữ liệu (Không đồng ý thay đổi data)
     * Created date: 09:00 13/05/2022
     */
    handleAfterChangeOnClick(isSave) {
      // Đồng ý cất dữ liệu
      if (isSave) {
        // Đồng ý sửa dữ liệu
        this.isSaveData = true;
        // Mode: update
        this.formMode == this.misaEnum.formMode.Update;
      }
    },
    /**
     * Mô tả : Show Alert dialog
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 15:22 15/05/2022
     */
    showAlertDlg(isShow) {
      this.isShowAlertDlg = isShow;
    },
    /**
     * Mô tả : Hiển thị form thông báo
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 15:23 15/05/2022
     */
    handleAlertOnClick(txtAlert) {
      this.isShowAlertDlg = true;
      this.txtAlertDlg = txtAlert;
    },
    /**
     * Mô tả : Hiển thị form và focus vào ô input lỗi ở MaterialDetail
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 15:26 15/05/2022
     */
    agreeAlertOnClick() {
      // đóng form
      this.isShowAlertDlg = false;
      // focus ô lỗi
      this.isShowErrorInput = !this.isShowErrorInput;
    },

    /**
     * Mô tả : on-off warning dialog
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 13:44 12/05/2022
     */
    showWarningDlg(isShow) {
      this.isShowWarningDlg = isShow;
      // clear đối tượng
      this.materialSelected = {};
    },

    escDlgWarning(e) {
      if (e) {
        this.isShowWarningDlg = false;
      }
    },

    /**
     * Mô tả : Xử lý hàm hiển thị warning dialog
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 17:18 12/05/2022
     */
    handleWarningDlg(txtWarning) {
      this.txtWarningDlg = txtWarning;
      //Hiển thị dialog
      this.isShowWarningDlg = true;
    },

    /**
     * Mô tả : Lấy dữ liệu Người dùng đã nhập từ BaseInputGrid
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 21:30 11/05/2022
     */
    getValueToQuery(valueToQuery) {
      // Input Không có giá trị -> tìm tất cả
      if (!valueToQuery) {
        // clear đối tượng
        this.filterOption = [];
        this.materialSelected = {};
      }
      // // Chưa có -> push vào mảng
      if (this.filterOption.length == 0) {
        this.filterOption.push(valueToQuery);
      } else {
        // Tồn tại phần tử trùng cột đang query
        let isDuplicateObj = this.filterOption.findIndex(
          (element) => element.columnName == valueToQuery.columnName
        );
        if (isDuplicateObj < 0) {
          // Nếu không trùng -> push vào mảng để query
          this.filterOption.push(valueToQuery);
        } else {
          // Trùng cột -> Xóa phần tử bị trùng cột -> push phần tử mới vào mảng để query
          this.filterOption.splice(isDuplicateObj, 1);
          this.filterOption.push(valueToQuery);
        }
      }
      if (this.indexPage) {
        this.indexPage = 1;
      }
      this.loadData();
    },

    /**
     * Mô tả : Thay đổi số lượng object trong 1 page
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 14:04 11/05/2022
     */
    changePageSize() {
      // Lấy selection
      let select = document.getElementById("selectionPagination");
      // Lấy giá trị selection
      let value = select.options[select.selectedIndex].value;
      if (value != this.totalObjectPerPage) {
        this.totalObjectPerPage = value;
        this.loadData();
      } else {
        return;
      }
    },
    /**
     * Mô tả : Tìm kiếm số trang
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 11:35 11/05/2022
     */
    inputSearchPaging() {
      if (!this.timerid) {
        this.timerid = setTimeout(() => {
          this.loadData();
          this.timerid = null;
        }, 700);
      }
    },

    /**
     * Mô tả : Tới trang đầu tiên của page
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 10:30 11/05/2022
     */
    goToFirstPage() {
      this.indexPage = 1;
      this.loadData();
    },

    goToPrevPage() {
      this.indexPage = this.indexPage - 1;
      if (this.indexPage == 0 || this.indexPage < 0) {
        this.indexPage = 1;
      }
      this.loadData();
    },
    goToNextPage() {
      this.indexPage = this.indexPage + 1;
      if (this.indexPage == this.totalPage || this.indexPage > this.totalPage) {
        this.indexPage = this.totalPage;
      }
      this.loadData();
    },
    goToLastPage() {
      this.indexPage = this.totalPage;
      this.loadData();
    },
    /**
     * Mô tả : On - Off dialog
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 08:23 11/05/2022
     */
    showCodeOperator() {
      this.isShowCodeOperator = !this.isShowCodeOperator;
    },
    showNameOperator() {
      this.isShowNameOperator = !this.isShowNameOperator;
    },
    showFeatureOperator() {
      this.isShowFeatureOperator = !this.isShowFeatureOperator;
    },
    showNoteOperator() {
      this.isShowNoteOperator = !this.isShowNoteOperator;
    },
    showUnitOperator() {
      this.isShowUnitOperator = !this.isShowUnitOperator;
    },
    showGroupOperator() {
      this.isShowGroupOperator = !this.isShowGroupOperator;
    },
    showFollowingOperator() {
      this.isShowFollowingOperator = !this.isShowFollowingOperator;
    },
    /**
     * Mô tả : Xác nhận trước khi xóa
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 17:33 12/05/2022
     */
    deleteRowHandle() {
      if (this.checkedaAssetList.length == 1) {
        this.handleWarningDlg(
          `Bạn có thực sự muốn xóa Nguyên liệu <"${this.materialSelected?.MaterialCode}"> không?`
        );
      } else {
        this.handleWarningDlg(
          `Bạn có thực sự muốn xóa những Nguyên liệu đã chọn không?`
        );
      }
    },
    /**
     * Mô tả : Bấm nút đồng í -> gọi api để xóa object
     * @param isDelete: true -> xóa || false -> hủy
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 14:29 15/05/2022
     */
    agreeHanleOnClick(isDelete) {
      // Đồng ý xóa
      if (isDelete) {
        // gọi api để xóa dữ liệu
        // Xóa 1 dữ liệu
        if (this.checkedaAssetList == 1) {
          this.callApiTodeleteRowHandle();
        } else {
          // Xóa nhiều dữ liệu
          this.deleteMultipleMaterials();
        }
        this.showWarningDlg(false);
      }
      // Không đồng ý xóa
      else {
        // Đóng form
        this.showWarningDlg(false);
      }
    },

    /**
     * Mô tả : Thực hiện gọi API và xóa hàng loạt Emp đã được chọn
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 14:55 25/04/2022
     */
    async deleteMultipleMaterials() {
      let me = this;
      let tempDeleteMultiple = [];
      for (let i = 0; i < this.checkedaAssetList.length; i++) {
        // console.log(this.manyEmps[i].EmployeeId);
        tempDeleteMultiple.push(this.checkedaAssetList[i].MaterialId);
      }
      await axios
        .post(
          "http://localhost:5236/api/v1/Materials/ObjectIds",
          tempDeleteMultiple
        )
        .then(function (res) {
          if (res) {
            me.showLoading = !me.showLoading;
            setTimeout(() => {
              me.showLoading = false;
            }, 200);
            //load lại data
            me.loadData();
            // Clear mảng dữ liệu manyEmps[]
            me.tempDeleteMultiple = [];
            me.checkedaAssetList = [];

            // Đóng dialog showConfirm
            this.showWarningDlg(false);
          }
        })
        .catch(function (res) {
          console.log(res);
        });
    },

    /**
     * Mô tả : Gọi API để thực hiện xóa row đã được chọn
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 08:47 01/04/2022
     */
    async callApiTodeleteRowHandle() {
      let me = this;
      try {
        if (me.materialSelected.MaterialId) {
          let materialsAPI = `${this.misaApi.deleteMaterial}/${me.materialSelected.MaterialId}`;
          await axios.delete(materialsAPI);
          // Load lại trang
          me.showLoading = !me.showLoading;
          setTimeout(() => {
            me.showLoading = false;
          }, 200);
          // Clear biến dữ liệu chứa object cần xóa (row)
          me.materialSelected = {};
          me.loadData();
          // Đóng form
          this.showWarningDlg(false);
        } else {
          this.handleWarningDlg("Mời bạn chọn nhân viên để xóa");
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả : Sử lý sự kiện tắt - bật dialog-Thêm mới
     * @param: boolean: isShow
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 08:40 01/04/2022
     */
    showOrHideDialog(isShow) {
      this.isShowDialog = isShow;
    },

    /**
     * Mô tả : Chọn tất cả nguyên vật liệu trong trang
     * @param
     * @return
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 08:16 27/05/2022
     */
    selAllMaterialOnClick() {
      // Nếu tồn tại phần tử trong mảng -> Set mảng rỗng
      if (this.checkedaAssetList.length >= this.totalObjectPerPage) {
        this.checkedaAssetList = [];
      } else {
        // Chọn tất cả các phần tử trong mảng
        for (const material of this.materials) {
          this.checkedaAssetList.push(material);
          // Loại bỏ các phần tử trùng lặp
          this.checkedaAssetList = this.uniqueMaterials(this.checkedaAssetList);
        }
      }
    },

    /**
     * Mô tả : Loại bỏ các phần tử trùng trong mảng
     * @param: Danh sách các phần tử trong mảng
     * @return: Danh sách các phần tử không trùng lặp
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 08:38 27/05/2022
     */
    uniqueMaterials(arr) {
      let newArr = [];
      for (var i = 0; i < arr.length; i++) {
        if (newArr.indexOf(arr[i]) === -1) {
          newArr.push(arr[i]);
        }
      }
      return newArr;
    },

    /**
     * Mô tả : Kiểm tra điều kiện đã chọn hàng để xóa hay chưa
     * @param: employeeId
     * Created by:materialSelected Vũ Trọng Nghĩa - MF1108
     * Created date: 08:37 01/04/2022
     */
    activate(material) {
      // this.checkedaAssetList.map((elememt) => elememt.MaterialId) ==
      //   material.MaterialId;
      // Kiểm tra xem đã tích sản phẩm trước đó chưa
      this.materialSelected = {};
      if (this.checkedaAssetList.includes(material)) {
        // Nếu tích r thì bỏ tích
        // Lấy index của sản phẩm được chọn
        const selecedIndex = this.checkedaAssetList.findIndex(
          (prd) => prd.MaterialId == material.MaterialId
        );
        // Xóa theo index splice(start, deleteCount)
        this.checkedaAssetList.splice(selecedIndex, 1);
      } else {
        // Nếu chưa thì add vào list
        this.checkedaAssetList.push(material);
        // Nếu tại độ dài mảng checkedaAssetList.length == 1 -> Gán giá trị material == materialSeleted
      }
      if (this.checkedaAssetList.length == 1) {
        // this.checkedaAssetList[0] == this.materialSelected;
        Object.assign(this.materialSelected, this.checkedaAssetList[0]);
      }
    },
    /**
     * Mô tả : Hàm sử lý sự kiện dbclick vào row trong table
     * @param: material: đối tượng được chọn
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 08:41 01/04/2022
     */
    async rowOnDBClick(material) {
      var me = this;
      // Clear đối tượng được chọn
      this.materialSelected = {};
      // Thêm vào danh sách các phần tử được chọn(Kiểm tra các phần tử bị trùng lặp)
      this.activate(material);
      this.checkedaAssetList.push(material);
      // Formode: update
      this.formMode = this.misaEnum.formMode.Update;
      try {
        await axios
          .get(`${this.misaApi.getMaterialById}/${material.MaterialId}`)
          .then(function (res) {
            if (res) {
              me.materialSelected = res.data;
              me.showOrHideDialog(true);
            }
          })
          .catch(function (res) {
            console.log(res);
          });
      } catch (error) {
        console.log(error);
      }
      // 1 Lấy thông tin nhân viên từ Api
      this.showOrHideDialog(true);
    },

    /**
     * Mô tả : Xử lý sự kiện bấm nút sửa
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 20:05 10/05/2022
     */
    async clickRowOnUpdate() {
      var me = this;
      me.formMode = this.misaEnum.formMode.Update;
      //Gọi Api -> lấy data -> bind lên form Detail
      try {
        await axios
          .get(
            `${this.misaApi.getMaterialById}/${me.materialSelected.MaterialId}`
          )
          .then(function (res) {
            if (res) {
              me.materialSelected = res.data;
              me.showOrHideDialog(true);
            }
          })
          .catch(function (res) {
            console.log(res);
          });
      } catch (error) {
        console.log(error);
      }
      this.showOrHideDialog(true);
    },

    async callApiGetMaterialCode() {
      let me = this;
      me.materialSelected.MaterialCode = null;
      try {
        await axios
          .get(this.misaApi.getNewMaterialCode)
          .then(function (res) {
            me.materialSelected.MaterialCode = res.data;
          })
          .catch(function (res) {
            console.log(res);
          });
        this.showOrHideDialog(true);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mô tả : Khi bấm nút thêm mới -> Mở form  detail
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 13:28 14/04/2022
     */
    btnAddOnClick() {
      let me = this;
      me.formMode = this.misaEnum.formMode.Add;
      me.materialSelected = {};
      me.callApiGetMaterialCode();
    },

    /**
     * Mô tả : Thực hiện gọi API để lấy dữ liệu danh sách thông tin của Nguyên vật liệu
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 08:49 01/04/2022
     */
    async loadData() {
      let me = this;
      await axios
        .put(
          `${me.misaApi.getMaterialFilter}?pageNumber=${me.indexPage}&pageSize=${me.totalObjectPerPage}`,
          me.filterOption
        )
        .then(function (res) {
          me.showLoading = !me.showLoading;
          setTimeout(() => {
            me.showLoading = false;
          }, 300);
          // Lấy dữ liệu của trang đầu tiên
          me.materials = res.data.data;
          // Gán dữ liệu cho biến tổng số trang
          me.totalPage = res.data.totalPage;
          // Gán dữ liệu tổng số lượng nhân viên
          me.totalObject = res.data.totalRecord;
        })
        .catch(function (res) {
          console.log(res);
        });
    },
  },
  /**
   * Mô tả : Gọi API load dữ liệu trước khi DOM được khởi tạo
   * Created by: Vũ Trọng Nghĩa - MF1108
   * Created date: 08:53 01/04/2022
   */
  created() {
    /**
     * Mô tả : Load data về và bắn tên table
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 16:24 16/04/2022
     */
    this.loadData();
  },
  beforeDestroy() { },
};
</script>

<style>
</style>
