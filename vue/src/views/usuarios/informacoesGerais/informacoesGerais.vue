<template>
  <div>
    <v-form ref="form">
      <div class="d-flex flex-column">
        <div class="d-flex">
          <div style="width: 10%" class="mr-2">
            <div v-if="!readonly">
              <v-switch
                :true-value="true"
                :false-value="false"
                v-model="user.isActive"
                :label="user.isActive ? 'Ativo' : 'Inativo'"
                :color="user.isActive ? 'purple' : ''"
              ></v-switch>
            </div>
            <div v-else>
              <v-chip
                outlined
                class="mt-4"
                :color="user.isActive ? 'green' : 'red'"
              >
                {{ user.isActive == true ? "Ativo" : "Inativo" }}
              </v-chip>
            </div>
          </div>

          <v-text-field
            :disabled="readonly"
            :clearable="!readonly"
            class="mr-4 mt-2"
            label="Nome"
            :style="readonly ? 'width: 39%' : 'width: 41%'"
            v-model="user.name"
            :rules="[rules.required]"
            validate-on-blur
          >
          </v-text-field>

          <v-text-field
            style="width: 50%"
            v-if="$route.meta.editar == false"
            :rules="[rules.email, rules.required]"
            class="mt-2"
            label="E-mail"
            v-model="user.emailAddress"
            clearable
            validate-on-blur
          >
          </v-text-field>

          <v-input
            v-if="$route.meta.editar == true"
            class="my-4"
            dense
            :style="
              readonly
                ? 'font-size: 15px; width: 50%'
                : 'font-size: 15px; width: 27%'
            "
            :messages="user.emailAddress"
          >
            <span style="font-size: 12px; color: #8a8a8a"> E-mail </span>
            <template v-slot:message="{ message }">
              <b style="font-size: 14px" v-html="message"></b>
            </template>
          </v-input>

          <v-btn
            v-if="$route.meta.editar == true && !readonly"
            @click="$emit('mudarSenha')"
            class="mb-6 mt-4 ml-4"
            rounded
            dense
            dark
            style="background: purple"
          >
            <span>RETORNAR PARA SENHA ORIGINAL</span>
          </v-btn>
        </div>

        <div class="d-flex">
          <v-text-field
            :disabled="readonly"
            :clearable="!readonly"
            style="width: 50%"
            v-mask="['(##) ####-####', '(##) #####-####']"
            class="mr-4"
            label="Telefone (opcional)"
            v-model="user.phoneNumber"
            :rules="[rules.telefone]"
            validate-on-blur
          >
          </v-text-field>
        </div>
      </div>
    </v-form>

    <div
      v-if="user.userProfile === 2"
      class="my-1 mt-4 pa-2 d-flex align-center"
      style="
        width: 100%;
        height: 10%;
        color: white;
        background: #2196f3;
        border-radius: 5px;
      "
    >
      <v-icon class="ml-2 mr-2" color="white">mdi-alert-circle</v-icon>

    </div>
  </div>
</template>

<script>
import getRouterData from "@/mixins/getRouterData.js";
import validationRules from "../../../mixins/validationRules";
import crudController from "@/mixins/crudController.js";
import { mask } from "vue-the-mask";

export default {
  directives: { mask },
  name: "usuariosInformacoesGerais",

  data: () => ({
    expandedPanel: null,
    status: true,
    disabled: true,
    validaEdicao: null,
    search: "",
    reset: null,
    Editar: false,
    trip: {
      name: "",
      location: null,
      start: null,
      end: null,
    },
  }),

  props: [
    "user",
    "readonly",
  ],

  mixins: [
    getRouterData,
    validationRules,
    crudController({ store: "ModuleUsuarios" }),
  ],

  computed: {
    validateForm() {
      return this.$refs.form.validate();
    },
  },

  methods: {

    changeUserProfile() {
      this.user.roleProfile = null;
      this.expandedPanel = null;
    },

    setValidaEdicao(v) {
      this.validaEdicao = v;
    },
  },
};
</script>

<style scoped>
.row {
  display: flex;
  align-items: center;
  flex-wrap: nowrap;
  gap: 40%;
}

</style>
