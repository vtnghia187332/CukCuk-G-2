<template>
  <div class="m-input-wrapper">
    <input class="m-input" :type="inputType" :placeholder="placeholder" ref="input" :value="modelValue"
      @input="onChangeHandler" />
    <div :class="this.iconType" v-if="hasIcon"></div>
  </div>
</template>
<script>
// eslint-disable-next-line
/*eslint-disable */
export default {
  name: "base-input",
  props: {
    // UI Prop
    placeholder: { type: String },
    iconName: { type: String },
    inputType: {
      type: String,
      default: "text",
    },
    modelValue: {
      type: String,
      default: "",
    },
    // Function Prop
    propName: { type: String },
  },
  created() {
    if (this.iconName) {
      this.iconType = `m-icon icon--${this.iconName}`;
      this.hasIcon = true;
    }
  },
  methods: {
    onChangeHandler(e) {
      e.preventDefault();
      let value = e.target.value;
      if (value) {
        this.removeError();
        // Emit lên và xóa hiển thị thông báo lỗi//
        
      }
      this.$emit("update:modelValue", value);
    },
    /**
     * Mô tả : Hàm set focus input
     * Created by: Nguyễn Hữu Lộc - MF1099
     * Created date: 23:39 23/04/2022
     */
    // setFocus() {
    //   this.$refs.input.focus();
    // },
    /**
     * Mô tả : Hàm set bỏ border lỗi input
     * Created by: Nguyễn Hữu Lộc - MF1099
     * Created date: 23:39 23/04/2022
     */
    removeError() {
      this.$refs.input.style = "";
      this.$refs.input.title = "";
    },
    /**
     * Mô tả : Hàm thêm border lỗi
     * Created by: Nguyễn Hữu Lộc - MF1099
     * Created date: 23:40 23/04/2022
     */
    setError() {
      this.$refs.input.style.height = "26px";
      this.$refs.input.style.border = "1px solid red";

      // this.$refs.input.title = _content;
      // this.setFocus();
    },
  },
  data() {
    return {
      // Component UI
      iconType: null,
      hasIcon: false,
    };
  },
};
</script>
<style scoped>
.m-input-wrapper {
  display: flex;
  justify-content: flex-start;
  align-items: center;
  height: 32px;
  position: relative;
}

input[type="date"]::-webkit-inner-spin-button,
input[type="date"]::-webkit-calendar-picker-indicator {
  width: 15px;
  height: 15px;
  position: absolute;
  right: 0;
  cursor: pointer;
  margin-right: 5px;
  z-index: 2;
  background: url("@/assets/img/Sprites.645f8243.svg") no-repeat;
  background-position: -128px -312px;
}


.m-input:focus {
  border: none;
  outline: none;
  border: 1px solid #2ca01c;
  border-radius: 3px;
  height: 26px;
}

.m-input {
  height: 30px;
  font-size: 12px;
  border-radius: 3px;
  outline: none;
  background-color: transparent;
  border: none;
  outline: none;
}

input.m-input[type="date"] {
  font-family: MISA NotoSans;
}

.m-input-wrapper input.m-input::placeholder {
  opacity: 0.9;
  font-family: MISA NotoSans Italic;
}

.m-input-wrapper .m-icon {
  padding-right: 5px;
  padding-left: 10px;
  position: absolute;
  right: 0;
  z-index: 0;
}

.m-input-wrapper:hover():not(:focus) .m-input {
  outline: 1px solid #e2e2e2 !important;
}
</style>