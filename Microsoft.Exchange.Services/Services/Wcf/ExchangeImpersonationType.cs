using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DC0 RID: 3520
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlRoot(ElementName = "ExchangeImpersonation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class ExchangeImpersonationType
	{
		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x06005987 RID: 22919 RVA: 0x001180B2 File Offset: 0x001162B2
		// (set) Token: 0x06005988 RID: 22920 RVA: 0x001180BA File Offset: 0x001162BA
		[XmlElement]
		[DataMember]
		public ConnectingSIDType ConnectingSID
		{
			get
			{
				return this.connectingSIDField;
			}
			set
			{
				this.connectingSIDField = value;
			}
		}

		// Token: 0x04003193 RID: 12691
		private ConnectingSIDType connectingSIDField;
	}
}
