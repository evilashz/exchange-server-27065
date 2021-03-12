using System;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.WSSecurity
{
	// Token: 0x02000B4D RID: 2893
	[XmlRoot(Namespace = "http://www.w3.org/2005/08/addressing", ElementName = "ReplyTo", IsNullable = false)]
	public sealed class WSAddressingReplyToHeader : SoapHeader
	{
		// Token: 0x06003E4A RID: 15946 RVA: 0x000A2A20 File Offset: 0x000A0C20
		public WSAddressingReplyToHeader()
		{
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06003E4B RID: 15947 RVA: 0x000A2A28 File Offset: 0x000A0C28
		// (set) Token: 0x06003E4C RID: 15948 RVA: 0x000A2A30 File Offset: 0x000A0C30
		[XmlElement]
		public string Address
		{
			get
			{
				return this.address;
			}
			set
			{
				this.address = value;
			}
		}

		// Token: 0x06003E4D RID: 15949 RVA: 0x000A2A39 File Offset: 0x000A0C39
		private WSAddressingReplyToHeader(string address)
		{
			this.address = address;
		}

		// Token: 0x0400362F RID: 13871
		private const string AnonymousReplyToLocation = "http://www.w3.org/2005/08/addressing/anonymous";

		// Token: 0x04003630 RID: 13872
		private string address;

		// Token: 0x04003631 RID: 13873
		public static WSAddressingReplyToHeader Anonymous = new WSAddressingReplyToHeader("http://www.w3.org/2005/08/addressing/anonymous");
	}
}
