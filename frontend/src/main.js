import "./styles/main.scss";

import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import "./registerServiceWorker";

import ApiService from "./common/api.service";
import DateFilter from "./common/date.filter";
import ErrorFilter from "./common/error.filter";
import "./plugins/fontawesome-icons";

import { abilitiesPlugin } from "@casl/vue";
import defineAbilitiesFor from "@/config/ability";

Vue.config.productionTip = false;
Vue.filter("date", DateFilter);
Vue.filter("error", ErrorFilter);

Vue.use(abilitiesPlugin, defineAbilitiesFor);

ApiService.init();

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
