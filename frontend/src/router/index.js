import Vue from "vue";
import Router from "vue-router";
import store from "@/store";
import defineAbilitiesFor from "@/config/ability";
import { CHECK_AUTH } from "@/store/actions.type";

Vue.use(Router);

const router = new Router({
  routes: [
    {
      name: "home",
      path: "/",
      component: () => import("@/views/Home")
    },
    {
      name: "login",
      path: "/login",
      component: () => import("@/views/Login")
    },
    {
      name: "register",
      path: "/register",
      component: () => import("@/views/Register")
    },
    {
      name: "settings",
      path: "/settings",
      component: () => import("@/views/Settings"),
      meta: {
        auth: "User"
      }
    }
    // Handle child routes with a default, by giving the name to the
    // child.
    // SO: https://github.com/vuejs/vue-router/issues/777
    /*     {
      path: "/@:username",
      component: () => import("@/views/Profile"),
      children: [
        {
          path: "",
          name: "profile",
          component: () => import("@/views/ProfileArticles")
        }
      ]
    } */
  ]
});

// Ensure we checked auth before each page load.
router.beforeEach(async (to, from, next) => {
  // skip for index
  if (to === "/" || to === "") {
    next();
  }

  to.matched.some(async route => {
    if (route.meta.auth) {
      await store.dispatch(CHECK_AUTH);

      const auth = store.state.auth;
      const abilities = defineAbilitiesFor(auth);

      const canNavigate = abilities.can(
        route.meta.action || "read",
        route.meta.auth
      );

      if (!canNavigate) {
        return next("/login");
      }
    }

    next();
  });
});

export default router;
