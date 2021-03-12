using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.CommonTypes
{
	// Token: 0x02000086 RID: 134
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ApplicationDataTypeResponseFlag
	{
		// Token: 0x060005B7 RID: 1463 RVA: 0x000196B0 File Offset: 0x000178B0
		internal ApplicationDataTypeResponseFlag()
		{
			this.itemsElementNameField = new List<ItemsChoiceType2>();
			this.itemsField = new List<object>();
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x000196CE File Offset: 0x000178CE
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x000196D6 File Offset: 0x000178D6
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

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x000196DF File Offset: 0x000178DF
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x000196E7 File Offset: 0x000178E7
		[XmlIgnore]
		internal List<ItemsChoiceType2> ItemsElementName
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

		// Token: 0x04000333 RID: 819
		private List<object> itemsField;

		// Token: 0x04000334 RID: 820
		private List<ItemsChoiceType2> itemsElementNameField;
	}
}
