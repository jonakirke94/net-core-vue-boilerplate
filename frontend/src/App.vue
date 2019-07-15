<template>

<div class="app">
			<div class="container">
				<div class="banner">
					<h1>Crochet</h1>
				</div>
				<nav-bar />

				<div class="card">
					<router-view></router-view>
				</div>

			</div>

			<the-footer></the-footer>
</div>


</template>

<script>
import TheFooter from "@/components/TheFooter";
import NavBar from "@/components/layout/NavBar";

export default {
  name: "App",
  components: {
		TheFooter,
		NavBar
  }
};
</script>

<style lang="scss" scoped>
@import './styles/component';

.banner {
	position: relative;
	text-align: center;
	
	h1 {
		text-shadow: 0 1px 3px rgba(0,0,0,.3);
		font-weight: 700!important;
		text-align: center;
		font-size: 3.5rem;
		padding-bottom: .5rem;
		color: #fff;
		font-family: $headlines;
		margin-bottom: 0rem;
		margin-top: $gutter;

		&::after {
		background-image: -webkit-gradient(linear,right top,left top,from(rgba(255,255,255,0)),color-stop(5%,#fff),color-stop(90%,#fff),to(rgba(255,255,255,0)));
    background-image: linear-gradient(-90deg,rgba(255,255,255,0) 0,#fff 5%,#fff 90%,rgba(255,255,255,0) 100%);
    content: "";
    display: block;
    height: 2px;
    opacity: .08;
    position: absolute;
    bottom: 0;
    width: 100%;
	}

	}
}


</style>
