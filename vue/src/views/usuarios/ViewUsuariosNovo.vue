<template>
  <div
    class="d-flex flex-column justify-space-between mx-10"
    style="width: 100%; height: 98vh"
  >
   <alert-component></alert-component>
    <!-- DIALOG CONFIRM DELETE -->
    <v-dialog v-model="dialogDeleteConfirm" persistent max-width="450px">
      <div class="white pa-8 d-flex flex-column align-center text-center">
        <v-icon style="font-size: 60px" color="myApi-purple"
          >mdi-alert-circle-outline</v-icon
        >

        <div class="d-flex align-start">
          <h4 class="myApi-neutral--text text--var-1 pb-5 pt-5">
            Deseja excluir este registro? <br>
            Esta ação não poderá ser desfeita. Deseja continuar?
          </h4>
        </div>

        <div class="d-flex flex-row justify-end">
          <v-btn
            rounded
            large
            text
            class="mr-4"
            @click="dialogDeleteConfirm = false"
            >CANCELAR</v-btn
          >

          <v-btn
            rounded
            large
            class="myApi-purple white--text"
            @click="deleteItem(2)"
            :loading="loadingBtnDelete"
            >PROSSEGUIR</v-btn
          >
        </div>
      </div>
    </v-dialog>

    <top-section></top-section>

    <actions-table
      :params="getRouterData"
      :disableButton="!editPermission"
      v-on:clickAdd="$router.push(`usuarios/Add`)"
      labelSearch="Pesquisar por Nome"
    ></actions-table>

    <div class="flex-grow-1 rounded-lg pa-4 white elevation-5">
      <v-data-table
        :key="key"
        disable-sort
        :headers="headers"
        :items="rows"
        :options.sync="options"
        :page="page"
        :server-items-length="totalItems"
        noDataText="Nenhum registro encontrado"
        :loading="tableLoading"
        loading-text="Aguarde, carregando usuários..."
        :footer-props="{
          itemsPerPageOptions: [10, 50, 100],
          itemsPerPageText: 'Itens por Página',
        }"
      >
        <!-- Icone Manutenção -->
        <template v-slot:[`item.col1`]="{ item }">
          <div v-if="editPermission" class="d-flex flex-nowrap">
            <v-btn
              icon
              @click="$router.push(`/home/usuarios/edit/${item.col1}`)"
            >
              <v-icon> mdi-pencil </v-icon>
            </v-btn>

            <v-btn icon @click="deleteItem(1, item.col1)">
              <v-icon> mdi-delete </v-icon>
            </v-btn>
          </div>

          <div v-else class="d-flex flex-nowrap">

          </div>
        </template>

        <template v-slot:[`item.col4`]="{ item }">
          <v-chip :color="item.col5.color" outlined style="font-weight: 500">
            {{ item.col5.text }}
          </v-chip>
        </template>
      </v-data-table>
    </div>
  </div>
</template>

<script>
import TopSection from "@/components/TopSection.vue";
import ActionsTable from "@/components/ActionsTable.vue";
import getRouterData from "@/mixins/getRouterData.js";
import dataTable from "@/mixins/dataTable.js";
import crudController from "@/mixins/crudController.js";
import validationRules from "../../mixins/validationRules";
import AlertComponent from "../../components/AlertComponent.vue";

export default {
  data: () => ({
    headers: [
      { text: "", value: "col1", width: "10%" },
      { text: "Nome", value: "col2", width: "10%" },
      { text: "E-mail", value: "col3", width: "20%" },
      { text: "Status", value: "col4", width: "10%" },
    ],
    rows: [],
  }),

  computed: {
    editPermission() {
      return this.hasPermission(this.$PermissionNames.permissionUserEdit);
    },
  },

  methods: {

    buildRows(data) {
      let dataRows = [];
      data.forEach((e) => {
        dataRows.push({
          col1: e.id,
          col2: e.name + " " + e.surname,
          col3: e.emailAddress,
          col4: e.userProfile,
          col5: {
            text: e.isActive === true ? "Ativo" : "Inativo",
            color: e.isActive === true ? "success" : "grey",
          },
        });
      });

      this.rows = dataRows;
    },
  },

  mixins: [
    validationRules,
    getRouterData,
    dataTable({ store: "ModuleUsuarios" }),
    crudController({ store: "ModuleUsuarios" }),
  ],
  components: { TopSection, ActionsTable, AlertComponent },
};
</script>

