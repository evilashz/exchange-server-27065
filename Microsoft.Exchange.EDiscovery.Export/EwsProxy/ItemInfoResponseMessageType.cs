using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000263 RID: 611
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlInclude(typeof(UpdateItemInRecoverableItemsResponseMessageType))]
	[XmlInclude(typeof(UpdateItemResponseMessageType))]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class ItemInfoResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x00027237 File Offset: 0x00025437
		// (set) Token: 0x0600170A RID: 5898 RVA: 0x0002723F File Offset: 0x0002543F
		public ArrayOfRealItemsType Items
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

		// Token: 0x04000F9F RID: 3999
		private ArrayOfRealItemsType itemsField;
	}
}
