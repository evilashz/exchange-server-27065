using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000108 RID: 264
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class MarkAsJunkResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x000212D0 File Offset: 0x0001F4D0
		// (set) Token: 0x06000BC0 RID: 3008 RVA: 0x000212D8 File Offset: 0x0001F4D8
		public ItemIdType MovedItemId
		{
			get
			{
				return this.movedItemIdField;
			}
			set
			{
				this.movedItemIdField = value;
			}
		}

		// Token: 0x04000867 RID: 2151
		private ItemIdType movedItemIdField;
	}
}
