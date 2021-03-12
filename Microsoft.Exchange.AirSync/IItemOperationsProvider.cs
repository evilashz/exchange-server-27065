using System;
using System.Xml;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200003D RID: 61
	internal interface IItemOperationsProvider : IReusable
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060003A8 RID: 936
		bool RightsManagementSupport { get; }

		// Token: 0x060003A9 RID: 937
		void BuildErrorResponse(string statusCode, XmlNode responseNode, ProtocolLogger protocolLogger);

		// Token: 0x060003AA RID: 938
		void BuildResponse(XmlNode responseNode);

		// Token: 0x060003AB RID: 939
		void Execute();

		// Token: 0x060003AC RID: 940
		void ParseRequest(XmlNode fetchNode);
	}
}
