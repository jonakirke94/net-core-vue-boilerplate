import { AbilityBuilder } from "@casl/ability";

// https://www.npmjs.com/package/@casl/ability
const all = ['read', 'update', 'delete', 'post'];
const abilitiesMapping = {
	
  user: {
    abilities: [
      { entity: "User", functions: all },
      { entity: "Post", functions: all }
    ]
  },
  public: {
    abilities: [{ entity: "Settings", functions: ["read"] }]
  }
};

export default function defineAbilitiesFor(auth) {
  return AbilityBuilder.define(can => {
    let role = auth.isAuthenticated ? "user" : "public";
    const roleAbilities = abilitiesMapping[role];
    roleAbilities.abilities.forEach(roleAbility => {
      can(roleAbility.functions, roleAbility.entity);
    });
  });
}
