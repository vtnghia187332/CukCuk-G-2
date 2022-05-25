<template>
  <div class="m-navbar">
    <div class="m-navbar-list">
      <router-link to="/material" class="m-nabar">
        <div class="m-nabar-icon">
          <i class="fa-solid fa-table-list"></i>
        </div>
        <div class="m-navbar-txt">Nguyên vật liệu</div>
      </router-link>

      <router-link to="/employee" class="m-nabar">
        <div class="m-nabar-icon">
          <i class="fa-solid fa-people-carry-box"></i>
        </div>
        <div class="m-navbar-txt">Nhân viên</div>
      </router-link>
    </div>
  </div>
</template>

<script>
export default {};
</script>

<style>
</style>