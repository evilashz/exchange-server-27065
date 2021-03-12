using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000014 RID: 20
	internal class AADDirectoryObjectPresentationObjectSchema : ObjectSchema
	{
		// Token: 0x04000061 RID: 97
		public static readonly ProviderPropertyDefinition Members = new SimplePropertyDefinition("Members", typeof(AADDirectoryObjectPresentationObject[]), null);

		// Token: 0x04000062 RID: 98
		public static readonly ProviderPropertyDefinition ObjectId = new SimplePropertyDefinition("ObjectId", typeof(string), null);

		// Token: 0x04000063 RID: 99
		public static readonly ProviderPropertyDefinition ObjectType = new SimplePropertyDefinition("ObjectType", typeof(string), null);

		// Token: 0x04000064 RID: 100
		public static readonly ProviderPropertyDefinition Owners = new SimplePropertyDefinition("Owners", typeof(AADDirectoryObjectPresentationObject[]), null);
	}
}
