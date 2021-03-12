using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004CA RID: 1226
	[Serializable]
	public sealed class IPBlockListProvider : IPListProvider
	{
		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06003787 RID: 14215 RVA: 0x000D8907 File Offset: 0x000D6B07
		internal override ADObjectId ParentPath
		{
			get
			{
				return IPBlockListProvider.parentPath;
			}
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x06003788 RID: 14216 RVA: 0x000D890E File Offset: 0x000D6B0E
		internal override string MostDerivedObjectClass
		{
			get
			{
				return IPBlockListProvider.mostDerivedClass;
			}
		}

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x06003789 RID: 14217 RVA: 0x000D8915 File Offset: 0x000D6B15
		// (set) Token: 0x0600378A RID: 14218 RVA: 0x000D8927 File Offset: 0x000D6B27
		[Parameter(Mandatory = false)]
		public AsciiString RejectionResponse
		{
			get
			{
				return (AsciiString)this[IPListProviderSchema.RejectionMessage];
			}
			set
			{
				this[IPListProviderSchema.RejectionMessage] = value;
			}
		}

		// Token: 0x0400257A RID: 9594
		private static string mostDerivedClass = "msExchMessageHygieneIPBlockListProvider";

		// Token: 0x0400257B RID: 9595
		private static ADObjectId parentPath = new ADObjectId("CN=IPBlockListProviderConfig,CN=Message Hygiene,CN=Transport Settings");
	}
}
