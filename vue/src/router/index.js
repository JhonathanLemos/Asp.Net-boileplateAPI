import Vue from "vue";
import VueRouter from "vue-router";
import Util from "@/lib/util";
import ViewAuth from "@/views/authenticate/ViewAuth.vue"
import ViewGlobal from "@/views/globalViews/ViewGlobal.vue"
import ViewUsuariosNovo from "@/views/usuarios/ViewUsuariosNovo.vue"
import ViewUsuariosCreate from "@/views/usuarios/ViewUsuariosCreate.vue"
import ViewOperacoes from "@/views/operacoes/ViewOperacoes.vue"
import ViewClientes from "../views/clientes/ViewClientes.vue"
import ViewClientesCreate from "../views/clientes/ViewClientesCreate.vue"
import ViewProdutos from "../views/produtos/ViewProdutos.vue"
import ViewProdutosCreate from "../views/produtos/ViewProdutosCreate.vue"

Vue.use(VueRouter);

export const loginRoutes = {
  path: "/",
  name: "ViewAuth",
  meta: {
    title: "Autenticação",
  },
  component: ViewAuth,
};

export const appRoutes = [
  {
    path: "/home",
    name: "home",
    permission: "",
    meta: { title: "Home" },
    component: ViewGlobal,
    children: [
      {
        path: "administracao",
        permission: "",
        meta: { title: "Administração" },
        name: "ViewOperacoes",
        component: ViewOperacoes,
      },
      {
        path: "usuarios",
        props: true,
        meta: { title: "Usuários" },
        name: "ViewUsuarios",
        component: ViewUsuariosNovo,
      },
      {
        path: "usuarios/Add",
        props: true,
        // permission: "Administrative.User.Edit",
        meta: { title: "Novo Usuário", editar: false },
        name: "ViewUsuariosCreate",
        component: ViewUsuariosCreate,
      },
      {
        path: "usuarios/edit/:id",
        props: true,
        // permission: "Administrative.User.Edit",
        meta: { title: "Editar Usuário", editar: true },
        name: "ViewUsuariosCreate",
        component: ViewUsuariosCreate,
      },
      {
        path: "clientes",
        props: true,
        meta: { title: "Clientes" },
        name: "ViewClientes",
        component: ViewClientes,
      },
      {
        path: "clientes/Add",
        props: true,
        // permission: "Administrative.Customer.Edit",
        meta: { title: "Novo Cliente", editar: false },
        name: "ViewClientesCreate",
        component: ViewClientesCreate,
      },
      {
        path: "clientes/edit/:id",
        props: true,
        // permission: "Administrative.Customer.Edit",
        meta: { title: "Editar Cliente", editar: true },
        name: "ViewClientesCreate",
        component: ViewClientesCreate,
      },
      {
        path: "produtos",
        props: true,
        meta: { title: "Produtos" },
        name: "ViewProdutos",
        component: ViewProdutos,
      },
      {
        path: "produtos/Add",
        props: true,
        // permission: "Administrative.Customer.Edit",
        meta: { title: "Novo Produto", editar: false },
        name: "ViewProdutosCreate",
        component: ViewProdutosCreate,
      },
      {
        path: "produtos/Edit/:id",
        props: true,
        // permission: "Administrative.Customer.Edit",
        meta: { title: "Editar Produto", editar: false },
        name: "ViewProdutosCreate",
        component: ViewProdutosCreate,
      },
    ],
  },
];

export const errorRoutes = [
  {
    path: "/forbbiden",
    name: "ViewErrorPermission",
    meta: {
      title: "Acesso Negado",
    },
    component: () => "@/views/authenticate/ViewAuth.vue", // SUBSTITUIR POR PAGINA DE ERRO
  },
];

const routes = [loginRoutes, ...appRoutes, ...errorRoutes];

const router = new VueRouter({
  mode: "hash",
  base: process.env.BASE_URL,
  routes,
});

router.beforeEach((to, from, next) => {
  Util.title(to.meta.title);

  if (!Util.abp.session.userId && to.name !== "ViewAuth") {
    next({ name: "ViewAuth" });
  } else if (!!Util.abp.session.userId && to.name === "ViewAuth") {
    Util.title(to.meta.title);

    next({ name: "ViewOperacoes" });
  } else {
    const curRouterObj = Util.getRouterObjByName([...appRoutes], to.name);

    if (curRouterObj && curRouterObj.permission) {
      if (window.abp.auth.isGranted(curRouterObj.permission)) {
        Util.toDefaultPage([...appRoutes], to.name, router, next);
      } else {
        next({ replace: true, name: "ViewErrorPermission" });
      }
    } else {
      Util.toDefaultPage([...routes], to.name, router, next);
    }
  }
});

export default router;
