<template>
  <div
    class="elevation-5 rounded-lg white d-flex flex-column"
    style="position: relative; max-height: 100%"
  >
    <div class="d-flex justify-center">
        <img
          src="../assets/img/hr.png"
          alt=""
          width="50px"
          height="50px"
          class="mb-5 mt-5"
          id="img-logo"
        />

      <v-btn
        icon
        @click.stop="expandMenu"
        class="myApi-purple btn-expand"
        small
        id="btn-expand"
      >
        <v-icon id="icon-expand" color="myApi-neutral var-5"
          >mdi-chevron-right</v-icon
        >
      </v-btn>
    </div>
    <div class="d-flex align-center justify-center flex-grow-1">
      <v-navigation-drawer
        v-model="drawer"
        :mini-variant="mini"
        hide-overlay
        class="rounded-xl"
        mobile-breakpoint="0"
        style="overflow: visible"
      >
        <div
          class="d-flex flex-column justify-space-between"
          style="height: 95%"
        >
        <v-list flat class="mt-10">
            <v-list-item-group>
              <v-list-item link :to="{ name: 'ViewUsuarios' }">
                <v-list-item-icon>
                  <v-icon class="myApi-purple--text">mdi-account</v-icon>
                </v-list-item-icon>
                <v-list-item-content>
                  <v-list-item-title class="myApi-neutral--text text--var-2"
                    >Usuários</v-list-item-title
                  >
                </v-list-item-content>
              </v-list-item>

              <v-list-item link :to="{ name: 'ViewClientes' }">
                <v-list-item-icon>
                  <v-icon class="myApi-purple--text">mdi-account-group</v-icon>
                </v-list-item-icon>
                <v-list-item-content>
                  <v-list-item-title class="myApi-neutral--text text--var-2"
                    >Clientes</v-list-item-title
                  >
                </v-list-item-content>
              </v-list-item>

              <v-list-item link :to="{ name: 'ViewProdutos' }">
                <v-list-item-icon>
                  <v-icon class="myApi-purple--text">mdi-plus-thick</v-icon>
                </v-list-item-icon>
                <v-list-item-content>
                  <v-list-item-title class="myApi-neutral--text text--var-2"
                    >Produtos</v-list-item-title
                  >
                </v-list-item-content>
              </v-list-item>
            </v-list-item-group>
          </v-list>

          <v-list flat>
            <v-list-item-group color="myApi-neutral--text">
              <v-list-item link to="/home/administracao">
                <v-list-item-icon>
                  <v-icon class="myApi-purple--text">mdi-cog</v-icon>
                </v-list-item-icon>

                <v-list-item-content>
                  <v-list-item-title class="myApi-neutral--text text--var-2"
                    >Administração</v-list-item-title
                  >
                </v-list-item-content>
              </v-list-item>
            </v-list-item-group>
          </v-list>
        </div>
        <div style="font-size: 12px;" class="d-flex justify-end mr-7 mb-1 mt-2" v-if="!mini">Versão: {{ appVersion }}</div>
      </v-navigation-drawer>
    </div>
  </div>
</template>

<script>
  import {version} from "../../package.json"
export default {
  name: 'MenuLateral',
  data: () => ({
    appVersion: version,
    drawer: true,
    mini: true,
  }),

  methods: {
    expandMenu() {
      if (this.mini) {
        document.getElementById('btn-expand').classList.add('btn-expand-rotate')
        document.getElementById('img-logo').classList.add('img-logo-expand')
      } else {
        document
          .getElementById('btn-expand')
          .classList.remove('btn-expand-rotate')
        document.getElementById('img-logo').classList.remove('img-logo-expand')
      }
      this.mini = !this.mini
    },
  },
}
</script>

<style>
.img-logo {
  width: 45px;
}

.img-logo-expand {
  transform: scale(1.6);
}

.btn-expand {
  position: absolute !important;
  z-index: 50;
  right: -15px;
  top: 100px;
}

.btn-expand-rotate {
  transform: rotate(180deg);
}
</style>
