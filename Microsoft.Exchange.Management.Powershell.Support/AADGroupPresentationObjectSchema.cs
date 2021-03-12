using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000016 RID: 22
	internal sealed class AADGroupPresentationObjectSchema : AADDirectoryObjectPresentationObjectSchema
	{
		// Token: 0x04000067 RID: 103
		public static readonly ProviderPropertyDefinition AllowAccessTo = new SimplePropertyDefinition("AllowAccessTo", typeof(AADDirectoryObjectPresentationObject[]), null);

		// Token: 0x04000068 RID: 104
		public static readonly ProviderPropertyDefinition Description = new SimplePropertyDefinition("Description", typeof(string), null);

		// Token: 0x04000069 RID: 105
		public static readonly ProviderPropertyDefinition DisplayName = new SimplePropertyDefinition("DisplayName", typeof(string), null);

		// Token: 0x0400006A RID: 106
		public static readonly ProviderPropertyDefinition ExchangeResources = new SimplePropertyDefinition("ExchangeResources", typeof(string[]), null);

		// Token: 0x0400006B RID: 107
		public static readonly ProviderPropertyDefinition GroupType = new SimplePropertyDefinition("GroupType", typeof(string), null);

		// Token: 0x0400006C RID: 108
		public static readonly ProviderPropertyDefinition IsPublic = new SimplePropertyDefinition("IsPublic", typeof(bool?), null);

		// Token: 0x0400006D RID: 109
		public static readonly ProviderPropertyDefinition Mail = new SimplePropertyDefinition("Mail", typeof(string), null);

		// Token: 0x0400006E RID: 110
		public static readonly ProviderPropertyDefinition MailEnabled = new SimplePropertyDefinition("MailEnabled", typeof(bool?), null);

		// Token: 0x0400006F RID: 111
		public static readonly ProviderPropertyDefinition PendingMembers = new SimplePropertyDefinition("PendingMembers", typeof(AADDirectoryObjectPresentationObject[]), null);

		// Token: 0x04000070 RID: 112
		public static readonly ProviderPropertyDefinition ProxyAddresses = new SimplePropertyDefinition("ProxyAddresses", typeof(string[]), null);

		// Token: 0x04000071 RID: 113
		public static readonly ProviderPropertyDefinition SecurityEnabled = new SimplePropertyDefinition("SecurityEnabled", typeof(bool?), null);

		// Token: 0x04000072 RID: 114
		public static readonly ProviderPropertyDefinition SharePointResources = new SimplePropertyDefinition("SharePointResources", typeof(string[]), null);
	}
}
