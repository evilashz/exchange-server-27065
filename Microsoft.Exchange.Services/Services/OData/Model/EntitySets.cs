using System;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E3D RID: 3645
	internal static class EntitySets
	{
		// Token: 0x0400329B RID: 12955
		public static readonly EdmEntityContainer EdmEntityContainer = new EdmEntityContainer(typeof(EntitySets).Namespace, "EntityContainer");

		// Token: 0x0400329C RID: 12956
		public static readonly EdmSingleton Me = new EdmSingleton(EntitySets.EdmEntityContainer, "Me", User.EdmEntityType);

		// Token: 0x0400329D RID: 12957
		public static readonly EdmEntitySet Users = new EdmEntitySet(EntitySets.EdmEntityContainer, "Users", User.EdmEntityType);
	}
}
