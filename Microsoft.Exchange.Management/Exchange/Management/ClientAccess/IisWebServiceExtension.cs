using System;
using Microsoft.Exchange.Management.IisTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClientAccess
{
	// Token: 0x020000C6 RID: 198
	internal struct IisWebServiceExtension
	{
		// Token: 0x040002FD RID: 765
		internal const string GroupID = "MSExchangeClientAccess";

		// Token: 0x040002FE RID: 766
		internal const Strings.IDs DescriptionID = Strings.IDs.ClientAccessIisWebServiceExtensionsDescription;

		// Token: 0x040002FF RID: 767
		internal static readonly IisWebServiceExtension[] AllExtensions = new IisWebServiceExtension[]
		{
			new IisWebServiceExtension("owaauth.dll", "ClientAccess\\owa\\auth", false, true)
		};

		// Token: 0x020000C7 RID: 199
		internal enum Index
		{
			// Token: 0x04000301 RID: 769
			First,
			// Token: 0x04000302 RID: 770
			Owa = 0
		}
	}
}
