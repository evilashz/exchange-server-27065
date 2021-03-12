using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x0200022B RID: 555
	internal class UnpublishedObject : ADObject
	{
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x00045A78 File Offset: 0x00043C78
		public override ObjectId Identity
		{
			get
			{
				return this.ObjectId;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x00045A80 File Offset: 0x00043C80
		// (set) Token: 0x06001684 RID: 5764 RVA: 0x00045A92 File Offset: 0x00043C92
		public ADObjectId ObjectId
		{
			get
			{
				return this[UnpublishedObjectSchema.ObjectIdProp] as ADObjectId;
			}
			set
			{
				this[UnpublishedObjectSchema.ObjectIdProp] = value;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x00045AA0 File Offset: 0x00043CA0
		// (set) Token: 0x06001686 RID: 5766 RVA: 0x00045AB2 File Offset: 0x00043CB2
		public ADObjectId TenantId
		{
			get
			{
				return this[UnpublishedObjectSchema.TenantIdProp] as ADObjectId;
			}
			set
			{
				this[UnpublishedObjectSchema.TenantIdProp] = value;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x00045AC0 File Offset: 0x00043CC0
		// (set) Token: 0x06001688 RID: 5768 RVA: 0x00045AD2 File Offset: 0x00043CD2
		public DirectoryObjectClass ObjectType
		{
			get
			{
				return (DirectoryObjectClass)this[UnpublishedObjectSchema.ObjectTypeProp];
			}
			set
			{
				this[UnpublishedObjectSchema.ObjectTypeProp] = value;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x00045AE5 File Offset: 0x00043CE5
		// (set) Token: 0x0600168A RID: 5770 RVA: 0x00045AF7 File Offset: 0x00043CF7
		public string ServiceInstance
		{
			get
			{
				return this[UnpublishedObjectSchema.ServiceInstanceProp] as string;
			}
			set
			{
				this[UnpublishedObjectSchema.ServiceInstanceProp] = value;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x00045B05 File Offset: 0x00043D05
		// (set) Token: 0x0600168C RID: 5772 RVA: 0x00045B17 File Offset: 0x00043D17
		public DateTime CreatedDate
		{
			get
			{
				return (DateTime)this[UnpublishedObjectSchema.CreatedDateProp];
			}
			set
			{
				this[UnpublishedObjectSchema.CreatedDateProp] = value;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x00045B2A File Offset: 0x00043D2A
		// (set) Token: 0x0600168E RID: 5774 RVA: 0x00045B3C File Offset: 0x00043D3C
		public DateTime LastRetriedDate
		{
			get
			{
				return (DateTime)this[UnpublishedObjectSchema.LastRetriedDateProp];
			}
			set
			{
				this[UnpublishedObjectSchema.LastRetriedDateProp] = value;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x00045B4F File Offset: 0x00043D4F
		// (set) Token: 0x06001690 RID: 5776 RVA: 0x00045B61 File Offset: 0x00043D61
		public string ErrorMessage
		{
			get
			{
				return (string)this[UnpublishedObjectSchema.ErrorMessageProp];
			}
			set
			{
				this[UnpublishedObjectSchema.ErrorMessageProp] = value;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x00045B6F File Offset: 0x00043D6F
		internal override ADObjectSchema Schema
		{
			get
			{
				return UnpublishedObject.schema;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x00045B76 File Offset: 0x00043D76
		internal override string MostDerivedObjectClass
		{
			get
			{
				return UnpublishedObject.mostDerivedClass;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x00045B7D File Offset: 0x00043D7D
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04000B5E RID: 2910
		private static readonly UnpublishedObjectSchema schema = ObjectSchema.GetInstance<UnpublishedObjectSchema>();

		// Token: 0x04000B5F RID: 2911
		private static string mostDerivedClass = "UnpublishedObject";
	}
}
