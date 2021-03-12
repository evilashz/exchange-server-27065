using System;
using System.Data;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000091 RID: 145
	internal abstract class ItemsProperty : ConfigurablePropertyBag
	{
		// Token: 0x06000512 RID: 1298 RVA: 0x00010D2E File Offset: 0x0000EF2E
		public ItemsProperty(DataTable items)
		{
			this.Items = items;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00010D3D File Offset: 0x0000EF3D
		public ItemsProperty()
		{
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00010D48 File Offset: 0x0000EF48
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00010D6D File Offset: 0x0000EF6D
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x00010D7F File Offset: 0x0000EF7F
		public DataTable Items
		{
			get
			{
				return (DataTable)this[ItemsProperty.ItemsTableProp];
			}
			set
			{
				this[ItemsProperty.ItemsTableProp] = value;
			}
		}

		// Token: 0x04000340 RID: 832
		public static readonly HygienePropertyDefinition ItemsTableProp = new HygienePropertyDefinition("tvp_Items", typeof(DataTable));
	}
}
