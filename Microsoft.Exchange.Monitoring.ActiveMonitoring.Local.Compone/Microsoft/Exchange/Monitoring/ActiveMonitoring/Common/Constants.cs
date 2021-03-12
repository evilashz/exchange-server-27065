using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000123 RID: 291
	public sealed class Constants
	{
		// Token: 0x040005EA RID: 1514
		public const string E14PowershellEndpointFormat = "https://{0}psh.outlook.com/PowerShell-LiveID";

		// Token: 0x040005EB RID: 1515
		public const string ProdPowershellEndpoint = "https://outlook.office365.com/PowerShell-LiveID";

		// Token: 0x040005EC RID: 1516
		public const string SDFPowershellEndpoint = "https://sdfpilot.outlook.com/PowerShell-LiveID";

		// Token: 0x040005ED RID: 1517
		public const string GallatinPowershellEndpoint = "https://partner.outlook.cn/PowerShell-LiveID";

		// Token: 0x040005EE RID: 1518
		public const string PRODEWSUrl = "https://outlook.office365.com/ews/exchange.asmx";

		// Token: 0x040005EF RID: 1519
		public const string SDFEWSUrl = "https://sdfpilot.outlook.com/ews/exchange.asmx";

		// Token: 0x040005F0 RID: 1520
		public const string GallatinEWSUrl = "https://partner.outlook.cn/ews/exchange.asmx";

		// Token: 0x040005F1 RID: 1521
		public const string EWSBPOSDURLFormat = "{0}/ews/exchange.asmx";

		// Token: 0x040005F2 RID: 1522
		public const string E14EWSUrlFormat = "https://{0}.outlook.com/ews/exchange.asmx";

		// Token: 0x040005F3 RID: 1523
		public const string EWSVersionKey = "EWSVersion";

		// Token: 0x040005F4 RID: 1524
		public const string SourceEWSVersionKey = "SourceEWSVersion";

		// Token: 0x040005F5 RID: 1525
		public const string TargetEWSVersionKey = "TargetEWSVersion";

		// Token: 0x040005F6 RID: 1526
		public const string SourceEwsUrlKey = "SourceEwsUrl";

		// Token: 0x040005F7 RID: 1527
		public const string TargetEwsUrlKey = "TargetEwsUrl";

		// Token: 0x040005F8 RID: 1528
		public const string ExchangeVersionKey = "ExchangeVersion";

		// Token: 0x040005F9 RID: 1529
		public const string XDiagInfoKey = "X-DiagInfo";

		// Token: 0x040005FA RID: 1530
		public const string XFEServerKey = "X-FEServer";

		// Token: 0x040005FB RID: 1531
		public const string RequestIdKey = "request-id";
	}
}
