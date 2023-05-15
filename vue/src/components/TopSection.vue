<template>
  <div
    class="
      container-top-actions
      d-flex
      flex-row
      justify-space-between
      align-center
      mb-2
    "
  >
    <div class="d-flex flex-column flex-row">
      <template>
        <v-btn icon large class="elevation-0 mb-0" @click="setRoute"
          ><v-icon>mdi-arrow-left</v-icon></v-btn
        >
        <p
          style="font-size: 13px"
          class="myApi-neutral--text text--var-2 ml-1 mt-0"
        >
          Voltar
        </p>
      </template>
    </div>

    <div class="d-flex flex-row">
      <v-menu left min-width="250px" rounded offset-y>
        <template v-slot:activator="{ on }">
          <v-btn icon x-large v-on="on">
            <v-avatar color="myApi-purple" size="45">
              <span class="white--text">{{ userInitials }}</span>
            </v-avatar>
          </v-btn>
        </template>

        <v-card>
          <v-list-item-content class="justify-center">
            <div class="d-flex flex-column justify-center align-center">
              <h3 class="mt-4">
                {{ userName + " " + surname }}
              </h3>

              <p class="text-caption mt-1">
                {{ userEmail }}
              </p>

              <v-btn depressed rounded text @click="redirectToUserPage()">Editar Conta</v-btn>

              <v-btn depressed rounded text @click="setLogout()">Sair</v-btn>
            </div>
          </v-list-item-content>
        </v-card>
      </v-menu>
    </div>
  </div>
</template>

<script>
import { mapActions } from "vuex";

export default {
  props: ["nameRoute"],
  name: "TopSection",
  data: () => ({
    userEmail: window.localStorage.getItem("userEmail"),
    userName: window.localStorage.getItem("userName"),
    userInitials: window.localStorage.getItem("userInitials"),
    surname: window.localStorage.getItem("userSurname"),
  }),

  methods: {
    ...mapActions("ModuleSession", { logout: "logout" }),
    ...mapActions("ModuleUsuarios", {
      getUserByEmail: "getUserByEmail",
    }),

    setLogout() {
      this.logout();
      return location.reload();
    },

    async redirectToUserPage(){
      var result = await this.getUserByEmail(this.userEmail);
      this.$router.push({ path: `/home/usuarios/edit/${result.data.result}` });
    },

    setRoute() {
      if (this.nameRoute) {
        this.$router.push({
          name: this.nameRoute,
          params: {
            isUpdate: true,
            options: this.$route.params.options,
          },
        });
      } else this.$router.back();
    },
  },
};
</script>

<style>
</style>
