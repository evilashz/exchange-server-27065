using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001DA RID: 474
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetRoomListsResponseMessageType : ResponseMessageType
	{
		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x000256EA File Offset: 0x000238EA
		// (set) Token: 0x060013CD RID: 5069 RVA: 0x000256F2 File Offset: 0x000238F2
		[XmlArrayItem("Address", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public EmailAddressType[] RoomLists
		{
			get
			{
				return this.roomListsField;
			}
			set
			{
				this.roomListsField = value;
			}
		}

		// Token: 0x04000DAA RID: 3498
		private EmailAddressType[] roomListsField;
	}
}
