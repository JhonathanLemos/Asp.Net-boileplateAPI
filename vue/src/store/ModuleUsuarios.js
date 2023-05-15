import http from "@/lib/http.js";
import actionWrapper from "@/lib/helpers/action-wrapper";

export default {
  namespaced: true,
  actions: {

    getAll: actionWrapper({
      async action(_, payload) {
        let qrSearch =
          payload.search != null ? `&Keyword=${payload.search}` : "";
        let skipCount = payload.pageNumber * payload.itemsPerPage;
        let maxResultCount = payload.itemsPerPage;
        return await http.get(
          `/api/services/app/User/GetAll?SkipCount=${skipCount}&MaxResultCount=${maxResultCount}${qrSearch}`
        );
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    create: actionWrapper({
      async action(_, payload) {
        return await http.post(`/api/services/app/User/Create`, payload);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    update: actionWrapper({
      async action(_, payload) {
        return await http.put(`/api/services/app/User/Update`, payload);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    delete: actionWrapper({
      async action(_, payload) {
        return await http.delete(`/api/services/app/User/Delete?Id=${payload}`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    getById: actionWrapper({
      async action(_, payload) {
        return await http.get(`/api/services/app/User/Get?Id=${payload}`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    checkUserEmail: actionWrapper({
      async action(_, payload) {
        return await http.post(`/api/services/app/User/CheckUserEmail?Email=${payload}`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    setDefaultPassword: actionWrapper({
      async action(_, payload) {
        var result = await http.post(`/api/services/app/User/SetDefaultPassword?userId=${payload}`)
        return result;
      },
      async onError(e) {
        return e;
      }
    }),

    resetPassword: actionWrapper({
      async action(_, payload) {
        var result = await http.post('/api/services/app/User/ChangePassword', payload)
        return result;
      },
      async onError(e) {
        return e;
      }
    }),

    getUserByEmail: actionWrapper({
      async action(_, payload) {
        var result = await http.get(`/api/services/app/User/GetUserByEmail?email=${payload}`)
        return result;
      },
      async onError(e) {
        return e;
      }
    }),
  },
};
