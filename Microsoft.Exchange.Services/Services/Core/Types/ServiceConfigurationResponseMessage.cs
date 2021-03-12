using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000551 RID: 1361
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("ServiceConfigurationResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class ServiceConfigurationResponseMessage : ResponseMessage
	{
		// Token: 0x06002651 RID: 9809 RVA: 0x000A653C File Offset: 0x000A473C
		public ServiceConfigurationResponseMessage()
		{
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x000A6544 File Offset: 0x000A4744
		internal ServiceConfigurationResponseMessage(ServiceResultCode code, ServiceError error, XmlElement[] configurationElements) : base(code, error)
		{
			this.configurationElements = configurationElements;
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06002653 RID: 9811 RVA: 0x000A6555 File Offset: 0x000A4755
		// (set) Token: 0x06002654 RID: 9812 RVA: 0x000A655D File Offset: 0x000A475D
		[XmlAnyElement]
		public XmlElement[] ServiceConfiguration
		{
			get
			{
				return this.configurationElements;
			}
			set
			{
				this.configurationElements = value;
			}
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x000A6566 File Offset: 0x000A4766
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetServiceConfigurationResponseMessage;
		}

		// Token: 0x040018A4 RID: 6308
		private XmlElement[] configurationElements;
	}
}
