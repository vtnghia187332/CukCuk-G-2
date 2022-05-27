<template>
  <div
    @dragenter.prevent="toggleActive"
    @dragleave.prevent="toggleActive"
    @dragover.prevent
    @drop.prevent="toggleActive"
    :class="{ 'active-dropzone': active }"
    class="dropzone"
  >
    <div class="dropzone-content">
      <div class="txt-content-import">Thả tệp vào đây hoặc</div>
      <div class="m-btn-upload-file">
        <!--default html file upload button-->
        <input type="file" id="dropzoneFile" class="dropzoneFile" hidden />
        <!--our custom file upload button-->
        <label for="dropzoneFile">TẢI LÊN</label>
      </div>
    </div>

    <div class="content-validate">
      Định dạng được hỗ trợ: xlsx, xls, csv, ods
    </div>
  </div>
</template>

<script>
import { ref } from "vue";
export default {
  name: "DropZone",
  setup() {
    const active = ref(false);

    const toggleActive = () => {
      active.value = !active.value;
    };

    return { active, toggleActive };
  },
};
</script>

<style scoped lang="scss">
.dropzone {
  width: 1300px;
  height: 440px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  row-gap: 16px;
  border: 2px dashed grey;
  border-radius: 4px;
  background-color: #fff;
  transition: 0.3s ease all;

  input {
    display: none;
  }
}

.dropzone-content {
  display: flex;
}

.active-dropzone {
  color: #fff;
  border-color: #fff;
  background-color: #41b883;

  label {
    background-color: #fff;
    color: #41b883;
  }
}
</style>
