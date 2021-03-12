using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001D7 RID: 471
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetRoomsResponseMessageType : ResponseMessageType
	{
		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x000256B0 File Offset: 0x000238B0
		// (set) Token: 0x060013C6 RID: 5062 RVA: 0x000256B8 File Offset: 0x000238B8
		[XmlArrayItem("Room", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public RoomType[] Rooms
		{
			get
			{
				return this.roomsField;
			}
			set
			{
				this.roomsField = value;
			}
		}

		// Token: 0x04000DA8 RID: 3496
		private RoomType[] roomsField;
	}
}
