using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006DD RID: 1757
	[Serializable]
	public class ADTrust : ADPresentationObject
	{
		// Token: 0x17001AB3 RID: 6835
		// (get) Token: 0x0600513F RID: 20799 RVA: 0x0012CAE6 File Offset: 0x0012ACE6
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return ADTrust.schema;
			}
		}

		// Token: 0x06005140 RID: 20800 RVA: 0x0012CAED File Offset: 0x0012ACED
		public ADTrust()
		{
		}

		// Token: 0x06005141 RID: 20801 RVA: 0x0012CAF5 File Offset: 0x0012ACF5
		public ADTrust(ADDomainTrustInfo dataObject) : base(dataObject)
		{
		}

		// Token: 0x17001AB4 RID: 6836
		// (get) Token: 0x06005142 RID: 20802 RVA: 0x0012CAFE File Offset: 0x0012ACFE
		public new string Name
		{
			get
			{
				return (string)this[ADObjectSchema.Name];
			}
		}

		// Token: 0x17001AB5 RID: 6837
		// (get) Token: 0x06005143 RID: 20803 RVA: 0x0012CB10 File Offset: 0x0012AD10
		public ADTrustType TrustType
		{
			get
			{
				TrustAttributeFlag trustAttributeFlag = (TrustAttributeFlag)this[ADDomainTrustInfoSchema.TrustAttributes];
				if ((trustAttributeFlag & TrustAttributeFlag.ForestTransitive) == TrustAttributeFlag.ForestTransitive)
				{
					return ADTrustType.Forest;
				}
				if ((trustAttributeFlag & TrustAttributeFlag.ForestTransitive) != TrustAttributeFlag.ForestTransitive && (trustAttributeFlag & TrustAttributeFlag.WithinForest) != TrustAttributeFlag.WithinForest)
				{
					return ADTrustType.External;
				}
				return ADTrustType.None;
			}
		}

		// Token: 0x04003718 RID: 14104
		private static ADTrustSchema schema = ObjectSchema.GetInstance<ADTrustSchema>();
	}
}
