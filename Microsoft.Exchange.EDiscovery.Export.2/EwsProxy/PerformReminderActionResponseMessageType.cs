using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001D3 RID: 467
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class PerformReminderActionResponseMessageType : ResponseMessageType
	{
		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x000255CC File Offset: 0x000237CC
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x000255D4 File Offset: 0x000237D4
		[XmlArrayItem("ItemId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ItemIdType[] UpdatedItemIds
		{
			get
			{
				return this.updatedItemIdsField;
			}
			set
			{
				this.updatedItemIdsField = value;
			}
		}

		// Token: 0x04000D99 RID: 3481
		private ItemIdType[] updatedItemIdsField;
	}
}
