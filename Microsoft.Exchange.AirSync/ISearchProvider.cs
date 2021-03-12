using System;
using System.Xml;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000089 RID: 137
	internal interface ISearchProvider
	{
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000736 RID: 1846
		int NumberResponses { get; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000737 RID: 1847
		bool RightsManagementSupport { get; }

		// Token: 0x06000738 RID: 1848
		void BuildResponse(XmlElement responseNode);

		// Token: 0x06000739 RID: 1849
		void Execute();

		// Token: 0x0600073A RID: 1850
		void ParseOptions(XmlElement optionsNode);

		// Token: 0x0600073B RID: 1851
		void ParseQueryNode(XmlElement queryNode);
	}
}
