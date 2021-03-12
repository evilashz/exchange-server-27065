using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004D5 RID: 1237
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("ExpandDLResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class ExpandDLResponseMessage : ResponseMessage
	{
		// Token: 0x06002432 RID: 9266 RVA: 0x000A48BD File Offset: 0x000A2ABD
		public ExpandDLResponseMessage()
		{
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x000A48C5 File Offset: 0x000A2AC5
		internal ExpandDLResponseMessage(ServiceResultCode code, ServiceError error, XmlNode dlExpansionSet) : base(code, error)
		{
			this.dlExpansionSet = dlExpansionSet;
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06002434 RID: 9268 RVA: 0x000A48D6 File Offset: 0x000A2AD6
		// (set) Token: 0x06002435 RID: 9269 RVA: 0x000A48DE File Offset: 0x000A2ADE
		[XmlAnyElement]
		public XmlNode DLExpansionSet
		{
			get
			{
				return this.dlExpansionSet;
			}
			set
			{
				this.dlExpansionSet = value;
			}
		}

		// Token: 0x0400156B RID: 5483
		private XmlNode dlExpansionSet;
	}
}
