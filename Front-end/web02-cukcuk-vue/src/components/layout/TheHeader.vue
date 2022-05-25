<template>
  <div class="m-header">
    <div class="m-header-logo">
      <div class="m-btn-changeSize">
        <i class="fa-solid fa-bars"></i>
      </div>
      <div class="m-logo-page"></div>
    </div>
    <div class="m-header-content">
      <div class="m-combobox-selection">
        <input
          type="text"
          class="m-input-selection"
          placeholder="Chuỗi nhà hàng"
        />
        <button class="m-combobox-btn-selection m-combobox-detail-form">
          <i class="fa-solid fa-sort-down drp-btn"></i>
        </button>
        <div class="m-combobox-data-selection">
          <div class="m-combobox-item-selection">Nam</div>
          <div class="m-combobox-item-selection">Nữ</div>
          <div class="m-combobox-item-selection">Khác</div>
        </div>
      </div>
      <div class="m-header-left">
        <div class="m-txt-vertion">R58.0</div>
        <!-- combobox -->
        <div class="m-combobox-selection m-combobox-selection-language">
          <input
            type="text"
            class="m-input-selection"
            placeholder="Tiếng việt (Tiếng việt)"
          />
          <button class="m-combobox-btn-selection m-combobox-detail-form">
            <i class="fa-solid fa-sort-down drp-btn"></i>
          </button>
          <div class="m-combobox-data-selection">
            <div class="m-combobox-item-selection">Nam</div>
            <div class="m-combobox-item-selection">Nữ</div>
            <div class="m-combobox-item-selection">Khác</div>
          </div>
        </div>
        <!-- combobox -->
        <div class="m-tooltip-icon">
          <div class="m-icon-fuction">
            <div class="m-icon-component">
              <i class="fa-solid fa-phone"></i>
            </div>
          </div>
        </div>
        <div class="m-center-dropdown-user m-center-block">
          <div class="m-btn-header-avatar">
            <i class="fa-solid fa-user-astronaut"></i>
          </div>
          <div class="m-header-name">Vũ Trọng Nghĩa</div>
          <div class="m-header-icon-dropdown">
            <i class="fa-solid fa-sort-down drp-btn"></i>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {};
</script>

<style>
</style>