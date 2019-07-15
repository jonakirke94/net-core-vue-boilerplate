<template>
  <footer></footer>
</template>

<script>
export default {
  name: "TheFooter"
};
</script>

<style lang="scss" scoped>
@import '../styles/component';

footer {
    background: getColor('base', 'primary');
		width: 100%;
		margin-top:auto; 
		height: 8px;
}

</style>
