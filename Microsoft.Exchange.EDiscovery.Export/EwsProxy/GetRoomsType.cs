using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200034F RID: 847
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetRoomsType : BaseRequestType
	{
		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x0002973D File Offset: 0x0002793D
		// (set) Token: 0x06001B71 RID: 7025 RVA: 0x00029745 File Offset: 0x00027945
		public EmailAddressType RoomList
		{
			get
			{
				return this.roomListField;
			}
			set
			{
				this.roomListField = value;
			}
		}

		// Token: 0x0400124E RID: 4686
		private EmailAddressType roomListField;
	}
}
