import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

import ModuleSession from "@/store/ModuleSession.js";
import ModuleGlobal from "@/store/ModuleGlobal.js";
import ModuleUsuarios from "@/store/ModuleUsuarios.js";
import ModuleRoles from "@/store/ModuleRoles.js";
import ModuleClientes from "@/store/ModuleClientes.js";
import ModuleProdutos from "@/store/ModuleProdutos.js";

export default new Vuex.Store({
  modules: {
    ModuleSession,
    ModuleGlobal,
    ModuleUsuarios,
    ModuleRoles,
    ModuleClientes,
    ModuleProdutos
  },
});
