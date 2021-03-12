using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009DD RID: 2525
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class FederationInformationPropertyBag : PropertyBag
	{
		// Token: 0x06005A4D RID: 23117 RVA: 0x0017A09B File Offset: 0x0017829B
		public FederationInformationPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
		}

		// Token: 0x06005A4E RID: 23118 RVA: 0x0017A0A5 File Offset: 0x001782A5
		public FederationInformationPropertyBag()
		{
		}

		// Token: 0x17001AFE RID: 6910
		// (get) Token: 0x06005A4F RID: 23119 RVA: 0x0017A0AD File Offset: 0x001782AD
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return FederationInformationSchema.Identity;
			}
		}

		// Token: 0x17001AFF RID: 6911
		// (get) Token: 0x06005A50 RID: 23120 RVA: 0x0017A0B4 File Offset: 0x001782B4
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return SimpleProviderObjectSchema.ExchangeVersion;
			}
		}

		// Token: 0x17001B00 RID: 6912
		// (get) Token: 0x06005A51 RID: 23121 RVA: 0x0017A0BB File Offset: 0x001782BB
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return SimpleProviderObjectSchema.ObjectState;
			}
		}
	}
}
