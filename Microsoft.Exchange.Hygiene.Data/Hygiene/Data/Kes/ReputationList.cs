using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Kes
{
	// Token: 0x020001E9 RID: 489
	internal class ReputationList : ConfigurablePropertyBag
	{
		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x00041484 File Offset: 0x0003F684
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.ReputationListID.ToString());
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x000414A4 File Offset: 0x0003F6A4
		// (set) Token: 0x06001496 RID: 5270 RVA: 0x000414B6 File Offset: 0x0003F6B6
		public byte ReputationListID
		{
			get
			{
				return (byte)this[ReputationList.ReputationListIDProperty];
			}
			set
			{
				this[ReputationList.ReputationListIDProperty] = value;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x000414C9 File Offset: 0x0003F6C9
		// (set) Token: 0x06001498 RID: 5272 RVA: 0x000414DB File Offset: 0x0003F6DB
		public string Name
		{
			get
			{
				return (string)this[ReputationList.NameProperty];
			}
			set
			{
				this[ReputationList.NameProperty] = value;
			}
		}

		// Token: 0x04000A36 RID: 2614
		public static readonly HygienePropertyDefinition ReputationListIDProperty = new HygienePropertyDefinition("ti_ReputationListId", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000A37 RID: 2615
		public static readonly HygienePropertyDefinition NameProperty = new HygienePropertyDefinition("nvc_Description", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
