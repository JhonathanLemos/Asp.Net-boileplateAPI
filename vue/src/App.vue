<template>
  <transition mode="out-in">
    <router-view></router-view>
  </transition>
</template>

<script>
export default {
  name: "App",
  watch:{
    /*
    $route (){
      this.$store.commit("ModuleGlobal/UNSET_FILTER");
    }
    */
  },
};
</script>

<style>
.v-enter,
.v-leave-to {
  transform: translate3d(-5px, 0, 0);
  opacity: 0;
}

.v-enter-active,
.v-leave-active {
  transition: all 0.3s;
}

::-webkit-scrollbar {
  width: 10px;
}

::-webkit-scrollbar-track {
  border-radius: 10px;
}

::-webkit-scrollbar-thumb {
  background: #747474;
  border-radius: 10px;
}

::-webkit-scrollbar-thumb:hover {
  background: #434343;
}

.theme--dark {
    color: #FFFFFF;
}
</style>
