using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration;

namespace Microsoft.Exchange.Services.Core.PolicyNudges
{
	// Token: 0x020003BF RID: 959
	internal abstract class PolicyNudgeConfiguration
	{
		// Token: 0x06001AF4 RID: 6900
		internal abstract XmlElement SerializeConfiguration(XElement clientConfig, CachedOrganizationConfiguration serverConfig, ADObjectId senderADObjectId, XmlDocument xmlDoc);
	}
}
