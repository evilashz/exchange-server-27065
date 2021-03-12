using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Kes
{
	// Token: 0x020001EB RID: 491
	internal class ReputationListType : ConfigurablePropertyBag
	{
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x00041814 File Offset: 0x0003FA14
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.ReputationListTypeID.ToString());
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x00041834 File Offset: 0x0003FA34
		// (set) Token: 0x060014B6 RID: 5302 RVA: 0x00041846 File Offset: 0x0003FA46
		public byte ReputationListTypeID
		{
			get
			{
				return (byte)this[ReputationListType.ReputationListTypeIDProperty];
			}
			set
			{
				this[ReputationListType.ReputationListTypeIDProperty] = value;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x00041859 File Offset: 0x0003FA59
		// (set) Token: 0x060014B8 RID: 5304 RVA: 0x0004186B File Offset: 0x0003FA6B
		public string Name
		{
			get
			{
				return (string)this[ReputationListType.NameProperty];
			}
			set
			{
				this[ReputationListType.NameProperty] = value;
			}
		}

		// Token: 0x04000A43 RID: 2627
		public static readonly HygienePropertyDefinition ReputationListTypeIDProperty = new HygienePropertyDefinition("ti_ReputationListTypeId", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000A44 RID: 2628
		public static readonly HygienePropertyDefinition NameProperty = new HygienePropertyDefinition("nvc_Description", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
