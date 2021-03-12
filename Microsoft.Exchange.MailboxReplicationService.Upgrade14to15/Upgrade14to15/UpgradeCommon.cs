using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000B9 RID: 185
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class UpgradeCommon
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00009D64 File Offset: 0x00007F64
		public static string DefaultSymphonyCertificateSubject
		{
			get
			{
				switch (CommonUtils.ForestType)
				{
				case ForestType.TestTopology:
					return "CN=exchangeonline-redirection-test-symphony";
				case ForestType.ServiceDogfood:
					return "CN=auth.outlook.com, OU=Exchange, O=Microsoft Corporation, L=Redmond, S=Washington, C=US";
				case ForestType.ServiceProduction:
					return "CN=auth.outlook.com, OU=Exchange, O=Microsoft Corporation, L=Redmond, S=Washington, C=US";
				default:
					return "CN=auth.outlook.com, OU=Exchange, O=Microsoft Corporation, L=Redmond, S=Washington, C=US";
				}
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00009DA4 File Offset: 0x00007FA4
		public static Uri DefaultSymphonyWebserviceUri
		{
			get
			{
				string uriString;
				switch (CommonUtils.ForestType)
				{
				case ForestType.TestTopology:
					uriString = "https://365upgrade.devfabric.bosxlab.com:443";
					break;
				case ForestType.ServiceDogfood:
					uriString = "https://365upgrade.ccsctp.com";
					break;
				case ForestType.ServiceProduction:
					uriString = "https://365upgrade.microsoftonline.com";
					break;
				default:
					uriString = "https://365upgrade.microsoftonline.com";
					break;
				}
				return new Uri(uriString);
			}
		}

		// Token: 0x040002AA RID: 682
		public const string SDFWebServiceUri = "https://365upgrade.ccsctp.com";

		// Token: 0x040002AB RID: 683
		public const string ProdWebServiceUri = "https://365upgrade.microsoftonline.com";

		// Token: 0x040002AC RID: 684
		public const string ProdCertificateSubject = "CN=auth.outlook.com, OU=Exchange, O=Microsoft Corporation, L=Redmond, S=Washington, C=US";

		// Token: 0x040002AD RID: 685
		public const string SDFCertificateSubject = "CN=auth.outlook.com, OU=Exchange, O=Microsoft Corporation, L=Redmond, S=Washington, C=US";

		// Token: 0x040002AE RID: 686
		public const string TopologyWebServiceUri = "https://365upgrade.devfabric.bosxlab.com:443";

		// Token: 0x040002AF RID: 687
		public const string TopologyCertificateSubject = "CN=exchangeonline-redirection-test-symphony";
	}
}
