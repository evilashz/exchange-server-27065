using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.CommonTypes
{
	// Token: 0x02000085 RID: 133
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ApplicationDataTypeResponse
	{
		// Token: 0x060005B2 RID: 1458 RVA: 0x00019670 File Offset: 0x00017870
		internal ApplicationDataTypeResponse()
		{
			this.itemsElementNameField = new List<ItemsChoiceType3>();
			this.itemsField = new List<object>();
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0001968E File Offset: 0x0001788E
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x00019696 File Offset: 0x00017896
		[XmlChoiceIdentifier("ItemsElementName")]
		internal List<object> Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0001969F File Offset: 0x0001789F
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x000196A7 File Offset: 0x000178A7
		[XmlIgnore]
		internal List<ItemsChoiceType3> ItemsElementName
		{
			get
			{
				return this.itemsElementNameField;
			}
			set
			{
				this.itemsElementNameField = value;
			}
		}

		// Token: 0x04000331 RID: 817
		private List<object> itemsField;

		// Token: 0x04000332 RID: 818
		private List<ItemsChoiceType3> itemsElementNameField;
	}
}
