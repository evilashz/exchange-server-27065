using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.CommonTypes
{
	// Token: 0x02000079 RID: 121
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ApplicationDataTypeRequest
	{
		// Token: 0x0600057C RID: 1404 RVA: 0x00019423 File Offset: 0x00017623
		internal ApplicationDataTypeRequest()
		{
			this.itemsElementNameField = new List<ItemsChoiceType1>();
			this.itemsField = new List<object>();
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00019441 File Offset: 0x00017641
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x00019449 File Offset: 0x00017649
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

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00019452 File Offset: 0x00017652
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x0001945A File Offset: 0x0001765A
		[XmlIgnore]
		internal List<ItemsChoiceType1> ItemsElementName
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

		// Token: 0x040002F7 RID: 759
		private List<object> itemsField;

		// Token: 0x040002F8 RID: 760
		private List<ItemsChoiceType1> itemsElementNameField;
	}
}
