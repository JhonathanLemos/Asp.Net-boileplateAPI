<template>
  <div class="d-flex flex-column mx-10 " style="width: 100%; overflow: auto">
    <!-- DIALOG CONFIRM SALVAR -->
    <v-dialog v-model="dialogConfirmSalvar" persistent max-width="450px">
      <div class="white pa-8 d-flex flex-column align-center text-center">
        <v-icon style="font-size: 60px; border-radius: 15px" color="purple">mdi-alert</v-icon>

        <div class="d-flex align-start">
          <h4 class="myApi-neutral--text text--var-1 pb-5 pt-5 mb-4">
            Após salvar não será possível alterar <br />
            o endereço de e-mail do cadastro. <br />
            Deseja continuar?
          </h4>
        </div>

        <div class="d-flex flex-row justify-end">
          <v-btn rounded large text class="mr-8" @click="dialogConfirmSalvar = false">CANCELAR</v-btn>

          <v-btn rounded large class="white--text" style="background: purple" @click="submit"
            :loading="loadingBtnDelete">SALVAR</v-btn>
        </div>
      </div>
    </v-dialog>
    <!-- FIM -->
    <AlertComponentVue :type="type" :active="active" :message="message"></AlertComponentVue>


    <top-section></top-section>

    <div class="d-flex justify-space-between align-end background">
      <h2 class="myApi-neutral--text text--var-1">{{ $route.meta.title }}</h2>

      <div v-if="editPermission" class="d-flex flex-row justify-end align-end">
        <v-btn dense rounded class="mr-3" text @click="$router.push(`/home/produtos`)">
          CANCELAR
        </v-btn>

        <v-btn rounded dense class="myApi-purple white--text" @click="submit()" :loading="loadingBtnSave">SALVAR</v-btn>
      </div>
    </div>


    <v-form ref="form" class="background">
      <div class="d-flex">
        <v-text-field class="mr-4 mt-2" label="Nome Produto" v-model="dataNewItem.nomeProduto" validate-on-blur
          :rules="[rules.required]">
        </v-text-field>
        <v-text-field class="mr-4 mt-2" label="Código produto" v-model="dataNewItem.codigoProduto" validate-on-blur
          :rules="[rules.required]">
        </v-text-field>
        <v-text-field type="number" class="mr-4 mt-2" label="Quantidade" v-model="dataNewItem.quantidade" validate-on-blur
          :rules="[rules.required]">
        </v-text-field>
        <v-autocomplete class="mr-4 mt-2" :items="itemsClientes" label="Cliente" v-model="dataNewItem.clienteId"
          validate-on-blur :rules="[rules.required]">
        </v-autocomplete>
      </div>
    </v-form>
  </div>
</template>

<script>
import TopSection from "@/components/TopSection.vue";
import getRouterData from "@/mixins/getRouterData.js";
import crudController from "@/mixins/crudController.js";
import validationRules from "../../mixins/validationRules";
import { mask } from "vue-the-mask";
import AlertComponentVue from "../../components/AlertComponent.vue";
import { createNamespacedHelpers } from "vuex";
const moduleClientes = createNamespacedHelpers("ModuleClientes");

export default {
  directives: { mask },
  data: () => ({
    dialogConfirmSalvar: null,
    dataNewItem: {},
    itemsClientes: []
  }),

  computed: {
    editPermission() {
      return this.hasPermission(this.$PermissionNames.permissionCustomerEdit);
    },
  },

  watch: {
    active() {
      if (this.success) this.$router.push({ path: `/home/produtos` });
    },
  },

  async mounted() {
    var id = this.$route.params.id;
    if (id) {
      var result = await this.getItemById(id)
      this.dataNewItem = result
      this.dataNewItem.clienteId = this.dataNewItem.clienteId.toString();
    }
    this.getClientes()
  },

  methods: {
    ...moduleClientes.mapActions({ selectCustomers: "selectCustomers" }),

    async getClientes() {
      var result = await this.selectCustomers()
      this.itemsClientes = result.data.result
    },

    async submit() {
      this.dataNewItem.clienteId = parseInt(this.dataNewItem.clienteId)
      var id = this.$route.params.id;
      if (!id) {
        this.addNewItem();
      }
      else
        this.updateItem(id);
    }
  },

  mixins: [
    getRouterData,
    crudController({ store: "ModuleProdutos" }),
    validationRules
  ],

  components: {
    TopSection,
    AlertComponentVue
  },
};
</script>

<style scoped>
.background {
  background-color: white;
  padding: 20px;
  border-end-start-radius: 6px;
}
</style>
