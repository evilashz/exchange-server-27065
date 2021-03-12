using System;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.WSSecurity
{
	// Token: 0x02000B4C RID: 2892
	[XmlRoot(Namespace = "http://www.w3.org/2005/08/addressing", ElementName = "MessageID", IsNullable = false)]
	public sealed class WSAddressingMessageIDHeader : SoapHeader
	{
		// Token: 0x06003E47 RID: 15943 RVA: 0x000A29F4 File Offset: 0x000A0BF4
		internal static WSAddressingMessageIDHeader Create(string messageId)
		{
			return new WSAddressingMessageIDHeader
			{
				Value = messageId
			};
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06003E48 RID: 15944 RVA: 0x000A2A0F File Offset: 0x000A0C0F
		// (set) Token: 0x06003E49 RID: 15945 RVA: 0x000A2A17 File Offset: 0x000A0C17
		[XmlText]
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x0400362E RID: 13870
		private string value;
	}
}
