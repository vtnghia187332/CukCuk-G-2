<template>
  <div>
    <TheHeader />
    <Navbar />
    <TheContent />
  </div>
</template>

<script>
import TheHeader from "@/components/layout/TheHeader.vue";
import Navbar from "@/components/layout/TheNavbar.vue";
import TheContent from "@/components/layout/TheContent.vue";

export default {
  name: "App",
  components: {
    TheHeader,
    Navbar,
    TheContent,
  },
};
</script>

<style>
@import url("@/css/main.css");
</style>
