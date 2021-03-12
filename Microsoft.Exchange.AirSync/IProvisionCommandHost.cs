using System;
using System.Net;
using System.Xml;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000CE RID: 206
	internal interface IProvisionCommandHost
	{
		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000C02 RID: 3074
		XmlNode XmlRequest { get; }

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000C03 RID: 3075
		XmlDocument XmlResponse { get; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000C04 RID: 3076
		ProtocolLogger ProtocolLogger { get; }

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000C05 RID: 3077
		uint? HeaderPolicyKey { get; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000C06 RID: 3078
		int ProtocolVersion { get; }

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000C07 RID: 3079
		IPolicyData PolicyData { get; }

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000C08 RID: 3080
		IGlobalInfo GlobalInfo { get; }

		// Token: 0x06000C09 RID: 3081
		void SetErrorResponse(HttpStatusCode httpStatusCode, StatusCode easStatusCode);

		// Token: 0x06000C0A RID: 3082
		void SendRemoteWipeConfirmationMessage(ExDateTime wipeAckTime);

		// Token: 0x06000C0B RID: 3083
		void ResetMobileServiceSelector();

		// Token: 0x06000C0C RID: 3084
		void ProcessDeviceInformationSettings(XmlNode inboundDeviceInformationNode, XmlNode provisionResponseNode);
	}
}
