using System;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.WSSecurity
{
	// Token: 0x02000B4E RID: 2894
	[XmlRoot(Namespace = "http://www.w3.org/2005/08/addressing", ElementName = "To", IsNullable = false)]
	public sealed class WSAddressingToHeader : SoapHeader
	{
		// Token: 0x06003E4F RID: 15951 RVA: 0x000A2A59 File Offset: 0x000A0C59
		public WSAddressingToHeader()
		{
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x000A2A61 File Offset: 0x000A0C61
		internal WSAddressingToHeader(string value)
		{
			this.value = value;
			base.MustUnderstand = true;
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06003E51 RID: 15953 RVA: 0x000A2A77 File Offset: 0x000A0C77
		// (set) Token: 0x06003E52 RID: 15954 RVA: 0x000A2A7F File Offset: 0x000A0C7F
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

		// Token: 0x04003632 RID: 13874
		private string value;
	}
}
