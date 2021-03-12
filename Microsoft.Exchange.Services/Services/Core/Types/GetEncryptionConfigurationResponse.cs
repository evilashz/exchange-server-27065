using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004F7 RID: 1271
	[XmlType(TypeName = "GetEncryptionConfigurationResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetEncryptionConfigurationResponse : ResponseMessage
	{
		// Token: 0x060024DF RID: 9439 RVA: 0x000A53F8 File Offset: 0x000A35F8
		public GetEncryptionConfigurationResponse()
		{
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x000A5400 File Offset: 0x000A3600
		internal GetEncryptionConfigurationResponse(ServiceResultCode code, ServiceError error, GetEncryptionConfigurationResponse getEncryptionConfigurationResponse) : base(code, error)
		{
			if (getEncryptionConfigurationResponse != null)
			{
				this.ImageBase64 = getEncryptionConfigurationResponse.ImageBase64;
				this.EmailText = getEncryptionConfigurationResponse.EmailText;
				this.PortalText = getEncryptionConfigurationResponse.PortalText;
				this.DisclaimerText = getEncryptionConfigurationResponse.DisclaimerText;
				this.OTPEnabled = getEncryptionConfigurationResponse.OTPEnabled;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x060024E1 RID: 9441 RVA: 0x000A5454 File Offset: 0x000A3654
		// (set) Token: 0x060024E2 RID: 9442 RVA: 0x000A545C File Offset: 0x000A365C
		[XmlElement(ElementName = "ImageBase64", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string ImageBase64 { get; set; }

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060024E3 RID: 9443 RVA: 0x000A5465 File Offset: 0x000A3665
		// (set) Token: 0x060024E4 RID: 9444 RVA: 0x000A546D File Offset: 0x000A366D
		[XmlElement(ElementName = "EmailText", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string EmailText { get; set; }

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060024E5 RID: 9445 RVA: 0x000A5476 File Offset: 0x000A3676
		// (set) Token: 0x060024E6 RID: 9446 RVA: 0x000A547E File Offset: 0x000A367E
		[XmlElement(ElementName = "PortalText", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string PortalText { get; set; }

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x060024E7 RID: 9447 RVA: 0x000A5487 File Offset: 0x000A3687
		// (set) Token: 0x060024E8 RID: 9448 RVA: 0x000A548F File Offset: 0x000A368F
		[XmlElement(ElementName = "DisclaimerText", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string DisclaimerText { get; set; }

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060024E9 RID: 9449 RVA: 0x000A5498 File Offset: 0x000A3698
		// (set) Token: 0x060024EA RID: 9450 RVA: 0x000A54A0 File Offset: 0x000A36A0
		[XmlElement(ElementName = "OTPEnabled", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public bool OTPEnabled { get; set; }

		// Token: 0x060024EB RID: 9451 RVA: 0x000A54A9 File Offset: 0x000A36A9
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetEncryptionConfigurationResponseMessage;
		}
	}
}
