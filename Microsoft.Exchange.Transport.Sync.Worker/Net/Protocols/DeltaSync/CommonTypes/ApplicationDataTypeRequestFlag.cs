using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.CommonTypes
{
	// Token: 0x0200007E RID: 126
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ApplicationDataTypeRequestFlag
	{
		// Token: 0x06000599 RID: 1433 RVA: 0x00019564 File Offset: 0x00017764
		internal ApplicationDataTypeRequestFlag()
		{
			this.itemsElementNameField = new List<ItemsChoiceType>();
			this.itemsField = new List<object>();
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00019582 File Offset: 0x00017782
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x0001958A File Offset: 0x0001778A
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

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00019593 File Offset: 0x00017793
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x0001959B File Offset: 0x0001779B
		[XmlIgnore]
		internal List<ItemsChoiceType> ItemsElementName
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

		// Token: 0x04000303 RID: 771
		private List<object> itemsField;

		// Token: 0x04000304 RID: 772
		private List<ItemsChoiceType> itemsElementNameField;
	}
}
