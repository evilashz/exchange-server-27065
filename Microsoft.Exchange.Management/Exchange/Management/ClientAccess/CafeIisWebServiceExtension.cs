using System;
using Microsoft.Exchange.Management.IisTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClientAccess
{
	// Token: 0x020000C4 RID: 196
	internal struct CafeIisWebServiceExtension
	{
		// Token: 0x040002F7 RID: 759
		internal const string GroupID = "MSExchangeCafe";

		// Token: 0x040002F8 RID: 760
		internal const Strings.IDs DescriptionID = Strings.IDs.CafeIisWebServiceExtensionsDescription;

		// Token: 0x040002F9 RID: 761
		internal static readonly IisWebServiceExtension[] AllExtensions = new IisWebServiceExtension[]
		{
			new IisWebServiceExtension("owaauth.dll", "FrontEnd\\HttpProxy\\bin", false, true)
		};

		// Token: 0x020000C5 RID: 197
		internal enum Index
		{
			// Token: 0x040002FB RID: 763
			First,
			// Token: 0x040002FC RID: 764
			Cafe = 0
		}
	}
}
