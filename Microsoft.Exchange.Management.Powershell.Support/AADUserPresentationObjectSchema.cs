using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000019 RID: 25
	internal sealed class AADUserPresentationObjectSchema : AADDirectoryObjectPresentationObjectSchema
	{
		// Token: 0x04000075 RID: 117
		public static readonly ProviderPropertyDefinition DisplayName = new SimplePropertyDefinition("DisplayName", typeof(string), null);

		// Token: 0x04000076 RID: 118
		public static readonly ProviderPropertyDefinition MailNickname = new SimplePropertyDefinition("MailNickname", typeof(string), null);
	}
}
