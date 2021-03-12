using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200052A RID: 1322
	[XmlType("IsOffice365DomainResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class IsOffice365DomainResponse : ResponseMessage
	{
		// Token: 0x060025D3 RID: 9683 RVA: 0x000A5FA7 File Offset: 0x000A41A7
		public IsOffice365DomainResponse()
		{
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x000A5FAF File Offset: 0x000A41AF
		internal IsOffice365DomainResponse(ServiceResultCode code, ServiceError error, bool isOffice365Domain) : base(code, error)
		{
			this.IsOffice365Domain = isOffice365Domain;
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x000A5FC0 File Offset: 0x000A41C0
		public override ResponseType GetResponseType()
		{
			return ResponseType.IsOffice365DomainResponseMessage;
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060025D6 RID: 9686 RVA: 0x000A5FC4 File Offset: 0x000A41C4
		// (set) Token: 0x060025D7 RID: 9687 RVA: 0x000A5FCC File Offset: 0x000A41CC
		[XmlElement("IsOffice365Domain")]
		public bool IsOffice365Domain { get; set; }
	}
}
