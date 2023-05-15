<template>
  <div class="d-flex flex-row justify-space-between align-center mb-5">
    <div class="d-flex flex-row align-center">
      <h1 class="myApi-neutral--text text--var-1">{{ setTituloView }}</h1>
      <v-btn
        fab
        small
        class="mx-3 myApi-purple white--text elevation-0"
        @click="$emit('clickAdd')"
        v-if="!disableButton && setBtnInclude"
      >
        <v-icon>mdi-plus</v-icon>
      </v-btn>
    </div>

    <div class="d-flex flex-row align-center">
      <v-text-field
        clearable
        @click:clear="resetSearch"
        solo
        hide-details
        dense
        class="mr-3"
        style="width: 30vw"
        :label="labelSearch"
        prepend-inner-icon="mdi-magnify"
        full-width
        v-if="setBusca"
        @keyup.enter="searchText"
        v-model="search"
      />
    </div>
  </div>
</template>

<script>
export default {
  name: "ActionsTable",
  data: () => ({
    search: null,
  }),
  props: ["params", "labelSearch", "setSearch", "disableButton"],
  methods: {

    searchText() {
      this.$store.dispatch("ModuleGlobal/SET_SEARCH", this.search);
    },

    resetSearch() {
      this.$store.dispatch("ModuleGlobal/SET_SEARCH", null);
    },
  },

  watch:{
    setSearch(v) {
      this.search = v;
    },
  },
  computed: {

    setTituloView() {
      return this.params.tituloView;
    },
    setBusca() {
      if (this.params.pesquisar == "true") {
        return true;
      }
      return false;
    },
    setBtnInclude() {
      if (this.params.btnInclude == "true") {
        return true;
      }
      return false;
    },
    setBtnAddViagens() {
      if (this.paramsAddViagens.btnIncludeViagens == "false") {
        return false;
      }
      return true;
    },
  },
};
</script>
