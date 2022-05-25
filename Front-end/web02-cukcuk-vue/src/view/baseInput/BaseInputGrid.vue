<template>
  <div class="base-input">
    <button class="m-sel-drp-icon" @click="showOperator">
      {{ this.operaterChoosed.operatorIcon }}
    </button>
    <input
      v-model="inputSearch"
      @input="handleInputOnType"
      type="text"
      class="m-base-input"
    />
    <div class="drp-sel-operator-list" v-if="this.isShowOperator">
      <div
        class="drp-sel-operator"
        @click="handleOperatorOnClick(operator)"
        v-bind:class="{
          'drp-sel-operator-active': operator.id == operaterChoosed.operator,
        }"
        v-for="operator in Object.values(this.misaOperator)"
        :key="operator.id"
      >
        {{ operator.iconOperator }} :
        {{ operator.txtOperator }}
      </div>
    </div>
    <div
      class="m-icon-drp-input-grid"
      v-if="isHaveDrpGrid"
      @click="showDrpGridOnClick"
    >
      <i class="fa-solid fa-caret-down"></i>
    </div>
    <div class="sel-drpdown-grid" v-if="isShowDrpGrid">
      <div
        class="sel-drpdown-val"
        v-for="(baseData, index) in defaultData"
        :key="index"
        @click="chooseDataOnClick(index)"
      >
        {{ baseData[propTxt] }}
      </div>
    </div>
  </div>
</template>

<script>
// eslint-disable-next-line
/*eslint-disable */
export default {
  props: ["keyword", "isHaveDrpGrid", "propVal", "propTxt", "defaultData"],
  data() {
    return {
      //on-off selection dropdown
      isShowDrpGrid: false,

      // Đối tượng hứng dữ liệu: input-operator-columnName để truyển lên cha -> query
      tempObj: {
        // Toán tử
        operator: 0,
        // Tên Key
        columnName: null,
        // Tên value
        columnValue: null,
      },
      // operator đang được chọn - mặc định là contain
      operaterChoosed: {
        // Toán tử
        operator: this.misaOperator.contain.value,
        // Tên Key
        columnName: null,
        // Tên value
        columnValue: null,
        //icon
        operatorIcon: this.misaOperator.contain.iconOperator,
      },
      // Từ khóa để tìm kiếm
      inputSearch: null,
      // Time để clear TimeOut To <input>
      timeoutId: 0,
      // On-Of dlg operator
      isShowOperator: false,

      //operator được chọn
      isContainSelected: false,
      isEqualSelected: false,
      isBeginSelected: false,
      isEndSelected: false,
      isNotContainSelected: false,
    };
  },
  computed: {
    /**
     * Mô tả : Mapping giá trị selection lên value của ô
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 22:24 18/05/2022
     */
    mappingValue() {
      let mappedValue = null;
      this.defaultData.filter((item) => {
        if (item[this.propVal] == this.tempObj.columnValue) {
          mappedValue = item[this.propTxt];
        }
      });
      if (!mappedValue) return null;
      return mappedValue;
    },
  },
  methods: {
    /**
     * Mô tả : Khi chọn giá trị -> bind lên đối tượng để query
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 21:59 18/05/2022
     */
    chooseDataOnClick(index) {
      // binding giá trị vào object để query
      // //Lấy dữ liệu operator
      this.tempObj.operator = this.operaterChoosed.operator;
      // //lấy dữ liệu keyword
      this.tempObj.columnName = this.keyword;
      // // lấy dữ liệu từ khóa
      this.tempObj.columnValue = this.defaultData[index][this.propVal];
      // Gán lên giá trị(value) input
      this.inputSearch = this.defaultData[index][this.propTxt];
      this.bindValueToQuery(this.tempObj);
      // đóng dropdown
      this.isShowDrpGrid = false;
    },
    /**
     * Mô tả : On - off selection dropdown grid
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 20:48 18/05/2022
     */
    showDrpGridOnClick() {
      this.isShowDrpGrid = !this.isShowDrpGrid;
    },
    /**
     * Mô tả : Lấy dữ liệu người dùng muốn tìm kiếm
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 09:07 12/05/2022
     */
    getDataFromQuery() {
      // //Lấy dữ liệu operator
      this.tempObj.operator = this.operaterChoosed.operator;
      // //lấy dữ liệu keyword
      this.tempObj.columnName = this.keyword;
      // // lấy dữ liệu từ khóa
      this.tempObj.columnValue = this.inputSearch;
      // tìm kiếm trường: Ngừng theo dõi, với từ khóa là "Không"
      if (
        this.keyword == "Following" &&
        (this.inputSearch == "Không" || this.inputSearch == "không")
      ) {
        this.tempObj.operator = this.misaOperator.contain.value;
        this.tempObj.columnName = "Following";
        this.tempObj.columnValue = 0;
      }
      // tìm kiếm trường: Ngừng theo dõi, với từ khóa là "Có"
      else if (
        this.keyword == "Following" &&
        (this.inputSearch == "Có" || this.inputSearch == "có")
      ) {
        this.tempObj.operator = this.misaOperator.contain.value;
        this.tempObj.columnName = "Following";
        this.tempObj.columnValue = 1;
      }
      // bind to component cha
      else if (!this.keyword) {
        this.tempObj = {};
      }
      this.bindValueToQuery(this.tempObj);
    },

    /**
     * Mô tả : Sử lý sự kiện type input
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 08:29 12/05/2022
     */
    handleInputOnType() {
      if (this.timeoutId >= 0) {
        clearTimeout(this.timeoutId);
      }
      this.timeoutId = window.setTimeout(() => {
        this.getDataFromQuery();
      }, 500); // time to wait to listen for next keypress if not pressed callMethod will execute
    },

    /**
     * Mô tả : Xử lý tắt - bật dialog Operator
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 20:54 11/05/2022
     */
    showOperator() {
      this.isShowOperator = !this.isShowOperator;
    },
    /**
     * Mô tả : Xử lý hàm sự kiện click operator
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 20:55 11/05/2022
     */
    handleOperatorOnClick(operator) {
      //nếu bấm 2 lần -> gán lại giá trị operaterChoosed là mặc định
      if (this.operaterChoosed?.operator == operator.value) {
        this.operaterChoosed.operator = this.misaOperator.contain.value;
        // set icon
        this.operaterChoosed.operatorIcon =
          this.misaOperator.contain.iconOperator;
      } else {
        //Gán giá trị operator được chọn
        this.operaterChoosed.operator = operator.value;
        // set icon
        this.operaterChoosed.operatorIcon = operator.iconOperator;
      }
      //Truyền dữ liệu lên cha compent lựa chọn operator
      this.getDataFromQuery(this.operaterChoosed.operator);
      this.isShowOperator = false;
    },

    /**
     * Mô tả : Đẩy lên Component cha sự lựa chọn của operator
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 21:01 11/05/2022
     */
    bindValueToQuery(valueToQuery) {
      this.$emit("getValueToQuery", valueToQuery);
    },
  },
};
</script>

<style scoped>
@import url("@/css/components/base-input.css");
</style>