<template>
		<nav>
			<ul>			
				<router-link tag="li" to="/settings" exact-active-class="active" v-if="isAuthenticated">
					<font-awesome-icon icon="th-large" />
					<span class="label">Projekter</span>
				</router-link>

			<router-link tag="li" to="/" exact-active-class="active" v-if="!isAuthenticated">
					<font-awesome-icon icon="home" />
					<span class="label">Home</span>
				</router-link>

				<router-link tag="li" to="/login" class="pull-right" active-class="active" v-if="!isAuthenticated">
					<font-awesome-icon icon="sign-in-alt" />
					<span class="label">Login</span>
				</router-link>

				<router-link tag="li" to="/add" active-class="active" v-if="!isAuthenticated">
					<font-awesome-icon icon="user-plus" />
					<span class="label">Register</span>
				</router-link>

				<router-link tag="li" to="/add" class="pull-right" active-class="active" v-if="isAuthenticated">
					<font-awesome-icon icon="user-cog" />
					<span class="label">Profile</span>
				</router-link>

				<li v-if="isAuthenticated" @click="logout">
					<font-awesome-icon icon="sign-out-alt" />
					<span class="label">Logout</span>
				</li>
			</ul>			
	</nav>
</template>

<script>

import { mapGetters } from "vuex";
import { LOGOUT } from "@/store/actions.type";


export default {
	name: "NavBar",
	data() {
    return {
      minimized: false,
    };
	},
	methods: {
		logout() {
			this.$store.dispatch(LOGOUT);
    	this.$router.push({ name: "home" });
		}
	},
	computed: {
    ...mapGetters(["isAuthenticated"])
  },
};
</script>

<style lang="scss">
@import '../../styles/component';

nav {
	position: relative;
	padding-top: $gutter / 2;
	padding-bottom: $gutter / 2;

	ul {
		margin: 0;
		padding: 0;
		display: flex;

		@include media-sm-down {
			// display all items evenly on mobile
			justify-content: space-between;
		}
		
		li {
			display: inline-block;
			margin:	$margin / 4;
			background: 0 0;
    	border-radius: $border-radius;
    	font-weight: 600;
    	line-height: 24px;
    	padding: $gutter / 4;
    	text-decoration: none;
			transition: background-color,color .3s ease;
			color: #d3d3d3;
			font-family: $primary-font;
		
			&:first-of-type {
				margin-left: 0;
			}

			.label {
				padding-left: .5rem;
			}
			
			&:hover {
				background-color: rgba(0,0,0,.1);
				cursor: pointer;
			}

			&.active {
				background-color: rgba(0,0,0,.1);
				color: #fff;
			}

			&.pull-right {
				margin-left: auto;
			}

			@include media-lg-down {
				text-align: center;
				background-color: rgba(0,0,0,.1);
				width: 100px;

				&.active {
					background-color: rgba(0,0,0,.3);
					color: #fff;
				}

				span {
					display: block;
				}
			}

			@include media-sm-down {
				// eliminate pull-right
				margin-left: auto;
				margin-right: auto;
			}
		}
	
	}
}
</style>
