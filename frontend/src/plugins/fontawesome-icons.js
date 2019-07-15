
import Vue from "vue";

import { library } from "@fortawesome/fontawesome-svg-core";
import {
  faSignInAlt,
  faSignOutAlt,
  faThLarge,
  faHome,
  faUserCog,
  faUserPlus
} from "@fortawesome/free-solid-svg-icons";

const icons = [
  faSignInAlt,
  faSignOutAlt,
  faThLarge,
  faHome,
  faUserCog,
  faUserPlus
];
library.add(...icons);

export default library;

import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

Vue.component('font-awesome-icon', FontAwesomeIcon);