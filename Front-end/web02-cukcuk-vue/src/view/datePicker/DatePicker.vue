<template>
  <div>
    <label v-if="label?.length > 0"
      >{{ label }}{{ " " }}
      <span v-if="required"> <span style="color: red">*</span></span></label
    >
    <Datepicker
      v-bind="$attrs"
      :modelValue="dateFormat"
      autoApply
      placeholder="DD/MM/YYYY"
      format="dd/MM/yyyy"
      :minDate="new Date(new Date().setHours(0, 0, 0, 0))"
      :enableTimePicker="false"
      showNowButton
      @update:modelValue="(value) => handleDate(value)"
      inputClassName="dp-custom-input"
      textInput
    >
      <template #calendar-header="{ index }">
        <div>
          {{ index == 6 ? "CN" : `T${index}` }}
        </div>
      </template>
      <template #now-button="{ selectCurrentDate }">
        <span @click="selectCurrentDate()" class="current"> Hôm nay </span>
      </template>
      <template #clock-icon> </template>
    </Datepicker>
  </div>
</template>

<script>
// eslint-disable-next-line
/*eslint-disable */
export default {
  name: "BaseDatepicker",
  props: {
    // label
    label: {
      type: String,
      default: "",
    },
    // v-model
    modelValue: {
      type: String,
      default: "",
    },
    required: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return { date: null };
  },
  watch: {
    /**
     * Mô tả : Cập nhật giá trị ngày được hiển thị khi truyền dữ liệu qua v-model
     * Created by: Vũ Trọng Nghĩa
     * Created date: 20:50 22/04/2022
     */
    // modelValue: function () {
    //   if (this.modelValue) this.date = new Date(this.modelValue);
    //   else this.date = null;
    // },
  },
  computed: {
    /**
     * Mô tả : Format date - picker (Chỉnh lại ngày tháng bị lệch)
     * Created by: Vũ Trọng Nghĩa - MF1108
     * Created date: 08:09 27/04/2022
     */
    dateFormat: {
      get() {
        if (!this.modelValue) {
          return;
        }
        return new Date(this.modelValue);
      },
      set(newValue) {
        this.modelValue = newValue.toDateString();
      },
    },
  },

  methods: {
    /**
     * Mô tả : Sự kiện khi thay đổi ngày
     * @param value giá trị ngày được thay đổi
     * @return
     * Created by: Vũ Trọng Nghĩa
     * Created date: 22:42 21/04/2022
     */
    handleDate: function (value) {
      if (value) this.date = new Date(value).toDateString();
      else this.date = null;
    },
  },
};
</script>

<style scoped>
@import url("@/css/components/datePicker.css");
.current {
  display: inline-block !important;
  color: #2ca01c !important;
  margin: 10px 0 !important;
  cursor: pointer;
}
</style>
