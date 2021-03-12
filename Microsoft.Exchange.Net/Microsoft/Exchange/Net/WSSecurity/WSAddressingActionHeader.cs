using System;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.WSSecurity
{
	// Token: 0x02000B4B RID: 2891
	[XmlRoot(Namespace = "http://www.w3.org/2005/08/addressing", ElementName = "Action", IsNullable = false)]
	public sealed class WSAddressingActionHeader : SoapHeader
	{
		// Token: 0x06003E42 RID: 15938 RVA: 0x000A29BC File Offset: 0x000A0BBC
		public WSAddressingActionHeader()
		{
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x000A29C4 File Offset: 0x000A0BC4
		internal WSAddressingActionHeader(string value)
		{
			this.value = value;
			base.MustUnderstand = true;
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06003E44 RID: 15940 RVA: 0x000A29DA File Offset: 0x000A0BDA
		// (set) Token: 0x06003E45 RID: 15941 RVA: 0x000A29E2 File Offset: 0x000A0BE2
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

		// Token: 0x0400362D RID: 13869
		private string value;
	}
}
