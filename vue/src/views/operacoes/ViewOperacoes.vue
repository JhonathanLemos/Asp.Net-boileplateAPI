<template>
  <div class="d-flex flex-column mx-10" style="overflow-y: hidden; width: 100%">
    <top-section></top-section>

    <div class="flex-grow-1 d-flex flex-column" style="overflow-y: hidden">
      <h1 class="myApi-neutral--text text--var-1 mb-5">Administração</h1>
      <div class="d-flex justify-space-between" style="height: 82%">
        <div class="white rounded-lg pa-8 elevation-5 mb-2" style="width: 30%; overflow-y: auto"
          v-for="bloco in EstruturaOperacoes.blocos" :key="EstruturaOperacoes.blocos[bloco]">
          <h2 class="pb-2 myApi-neutral--text text--var-1">{{ bloco.name }}</h2>

          <hr />

          <ul class="menu-list" style="overflow-y: auto">
            <li v-for="item in bloco.itens" :key="item[item]">
              <div v-if="hasPermissionGranted(item.router)">
                <router-link :to="{ name: item.router }">
                  <v-icon medium class="myApi-purple--text mr-3">{{
                    item.icon
                  }}</v-icon>{{ item.name }}
                </router-link>
              </div>
            </li>
          </ul>
        </div>
        <div style="width: 5%" ></div>
      </div>
    </div>
  </div>
</template>

<script>
import TopSection from '@/components/TopSection.vue'
import EstruturaOperacoes from '@/config/EstruturaOperacoes.json'

export default {
  name: 'ViewMainOperacoes',
  data: () => ({
    EstruturaOperacoes,
  }),
  components: {
    TopSection,
  },

  methods: {
    hasPermissionGranted(value) {
      switch (value) {
        case 'ViewUsuarios':
          return this.hasPermission(this.$PermissionNames.permissionUser);
          case 'ViewClientes':
          return this.hasPermission(this.$PermissionNames.permissionCustomer);
          case 'ViewClientes':
          return this.hasPermission(this.$PermissionNames.permissionCustomer);
      }
      return true;
    },
  },

  mounted() {
    this.$store.dispatch("ModuleGlobal/SET_SEARCH", null);
  }
}
</script>

<style>
a {
  text-decoration: none !important;
  color: inherit !important;
}

.v-application ul {
  padding: 0 10px !important;
}

.menu-list li {
  list-style: none;
  display: flex;
  flex-direction: row;
  align-items: center;
  font-weight: 400;
  color: #434343;
  font-size: 16px;
  margin: 10px 0;
  cursor: pointer;
}

.menu-list li:hover {
  font-weight: 500;
}
</style>
