<template>
	<div class="center">
		<input @click="$emit('toggle')" class="burger-check" id="burger-check" type="checkbox">
		<label for="burger-check" class="burger"></label>
	</div> 
</template>


<script>
export default {
  name: "Burger",

};
</script>

<style lang="scss">
@import '../../styles/component';

.center {
	width: 100px;
	margin: 0 auto;
}

// Burger menu button
$burger--thickness: 4px;
.burger {
	$width: 28px;
	
	display: inline-block;
  border: 0;
	background: none;
	outline: 0;
	padding: 0;
	cursor: pointer;

	border-bottom: $burger--thickness solid currentColor;
	width: $width;

	transition: border-bottom 1s ease-in-out;
	-webkit-transition: border-bottom 1s ease-in-out; 

	// Fix for extra space in Firefox
	&::-moz-focus-inner {
		border: 0;
		padding: 0;
	}	 

	&:before {
		content: "";
		display: block;
		border-bottom: $burger--thickness solid currentColor;
		width: 100%;
		margin-bottom: 5px;
		transition: transform 0.5s ease-in-out;    
		-webkit-transition: -webkit-transform 0.5s ease-in-out;        
	}

	&:after {
		content: "";
		display: block;
		border-bottom: $burger--thickness solid currentColor;
		width: 100%;
		margin-bottom: 5px;
		transition: transform 0.5s ease-in-out;
		-webkit-transition: -webkit-transform 0.5s ease-in-out;
	}
}

.burger-check {
  display: none;
}

.burger-check:checked ~ .burger {
  border-bottom: $burger--thickness solid transparent;
  transition: border-bottom 0.8s ease-in-out;
  -webkit-transition: border-bottom 0.8s ease-in-out;

  &:before {
    transform: rotate(-405deg) translateY(1px) translateX(-3px);
    -webkit-transform: rotate(-405deg) translateY(1px) translateX(-3px);      
    transition: transform 0.5s ease-in-out;
    -webkit-transition: -webkit-transform 0.5s ease-in-out;
  }

  &:after {
    transform: rotate(405deg) translateY(-4px) translateX(-5px); 
    -webkit-transform: rotate(405deg) translateY(-4px) translateX(-5px);       
    transition: transform 0.5s ease-in-out;
    -webkit-transition: -webkit-transform 0.5s ease-in-out;
  }
}

// * * * Navigation * * *
$navigation__transition: max-height 0.5s ease-in-out;

.navigation {
  overflow: hidden;
  max-height: 0;
  
  transition: $navigation__transition;
}

.burger-check:checked ~ .navigation {
  max-height: 500px;
  
  transition: $navigation__transition;
}

</style>
