<template>
  <v-app>
    <div class="d-flex justify-center mb-6 mt-12 bg-login">
      <NovaSenha @resetParent="resetParent" @closeModal="loadingBtnSave = false" @success="successChangePassword"
        :object="object" v-model="openModalNewPassWord"></NovaSenha>

      <div class="containerForm mt-12">
        <div class="d-flex justify-center mb-6 bg-surface-variant">

          <img src="../../assets/img/hr.png" alt="" width="100px" height="90px" class="ma-2 pa-2" id="img-logo" />
        </div>

        <v-form class="form-login text-center mt-1" ref="form">
          <v-alert dismissible type="error" v-show="alertError" transition="scroll-y-transition" class="mt-6 mb-8"
            style="text-align: left">{{ alertErrorText }}</v-alert>

          <v-alert dismissible type="success" v-show="alertSuccess" transition="scroll-y-transition" class="mt-6 mb-8"
            style="text-align: left">{{ alertSuccessText }}</v-alert>

          <v-text-field rounded label="Login" v-model="inputData.userNameOrEmailAddress" outlined
            :rules="[rules.required]" validate-on-blur></v-text-field>

          <v-text-field rounded label="Senha" v-model="inputData.password" :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
            :rules="[rules.required]" :type="show ? 'text' : 'password'" v-on:keyup.enter="tryAuthenticate()"
            @click:append="show = !show" outlined validate-on-blur color="myApi-neutral"></v-text-field>

          <v-btn rounded large class="myApi-purple white--text" @click="tryAuthenticate()"
            :loading="loadingBtn">ACESSAR</v-btn>
        </v-form>
      </div>

    </div>
  </v-app>
</template>

<script>
import { mapActions } from "vuex";
import NovaSenha from "../../components/NovaSenha.vue";

export default {
  name: "ViewLogin",

  data: () => ({
    show: false,
    rules: {
      required: (value) => !!value || "Preencha este campo!",
    },
    object: null,
    loadingBtn: false,
    openModalNewPassWord: null,
    inputData: {
      rememberClient: true,
    },
    loadingBtnSave: false,
    alertError: false,
    alertErrorText: null,
    alertSuccess: false,
    alertSuccessText: null,
  }),

  methods: {
    ...mapActions("ModuleSession", {
      getCurrentLoginInformations: "getCurrentLoginInformation",
      login: "login",
      firstLogin: "firstLogin",
    }),

    async successChangePassword() {
      this.alertSuccess = true;
      this.alertSuccessText = "Senha alterada com sucesso";
      setTimeout(() => {
        this.alertSuccess = false;
        return location.reload();
      }, 5000);
      await this.setStorageUserInfo();
    },

    async firstLoginMethod(v) {
      var response = await this.firstLogin(v);
      if (response.status == 500) {
        this.loadingBtn = false;
        this.alertError = true;
        this.alertErrorText = response.data.error.message;
        setTimeout(() => {
          this.alertError = false;
        }, 5000);
        return false;
      } else if (response.data.result) {
        this.openModalNewPassWord = true;
        this.object = {
          userNameOrEmailAddress: this.inputData.userNameOrEmailAddress,
          password: v.password,
          rememberClient: true,
        };
        return true;
      } else {
        return null;
      }
    },

    async tryAuthenticate() {
      if (this.$refs.form.validate()) {
        this.alertError = false;
        this.loadingBtn = true;

        var result = await this.firstLoginMethod(this.inputData);
        if (result != null) return;

        var response = await this.login(this.inputData);

        if (response.status == 200) {
          await this.setStorageUserInfo();
          return location.reload();
        }
      }
    },

    async setStorageUserInfo() {
      let response = await this.getCurrentLoginInformations();
      let userInfo = response.data.result.user;
      let userInitials = (
        userInfo.name.charAt(0) + userInfo.surname.charAt(0)
      ).toUpperCase();
      localStorage.setItem("userEmail", userInfo.emailAddress);
      localStorage.setItem("userName", userInfo.name);
      localStorage.setItem("userSurname", userInfo.surname);
      localStorage.setItem("userInitials", userInitials);
    },

    resetParent(v) {
      this.loadingBtn = v;
      this.inputData = [];
    },
  },

  components: {
    NovaSenha,
  },
};
</script>

<style>
.wrap-login {
  height: 100%;
}

.container-login {
  width: 40%;
}

.form-login {
  width: 100%;
}

.containerForm {
  width: 30%;
  padding: 20px;
  border-radius: 4px;
  padding: 20px;
}
</style>
