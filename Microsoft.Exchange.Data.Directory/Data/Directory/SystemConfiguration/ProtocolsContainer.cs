using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200053F RID: 1343
	[ObjectScope(ConfigScopes.Server)]
	[Serializable]
	public class ProtocolsContainer : Container
	{
		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x06003C42 RID: 15426 RVA: 0x000E6793 File Offset: 0x000E4993
		internal override ADObjectSchema Schema
		{
			get
			{
				return ProtocolsContainer.schema;
			}
		}

		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x06003C43 RID: 15427 RVA: 0x000E679A File Offset: 0x000E499A
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ProtocolsContainer.mostDerivedClass;
			}
		}

		// Token: 0x040028C5 RID: 10437
		internal static readonly string DefaultName = "Protocols";

		// Token: 0x040028C6 RID: 10438
		private static ProtocolsContainerSchema schema = ObjectSchema.GetInstance<ProtocolsContainerSchema>();

		// Token: 0x040028C7 RID: 10439
		private static string mostDerivedClass = "protocolCfgSharedServer";
	}
}
