using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Fips
{
	// Token: 0x0200050E RID: 1294
	internal class Constants
	{
		// Token: 0x0400175A RID: 5978
		public const string Monitor = "Monitor";

		// Token: 0x0400175B RID: 5979
		public const string CollectFIPSLogsResponder = "CollectFIPSLogsResponder";

		// Token: 0x0400175C RID: 5980
		public const string EscalateResponder = "EscalateResponder";

		// Token: 0x0400175D RID: 5981
		public const string PerfcounterFormatWithInstance = "{0}\\{1}\\{2}";

		// Token: 0x0400175E RID: 5982
		public const string CategoryMSExchangeHygieneAntimalware = "MSExchange Hygiene Antimalware";

		// Token: 0x0400175F RID: 5983
		public const string CategoryMSExchangeHygieneClassification = "MSExchange Hygiene Classification";

		// Token: 0x04001760 RID: 5984
		public const string CounterEngineErrors = "Engine Errors";

		// Token: 0x04001761 RID: 5985
		public const string WorkItemPrefixEngineErrors = "EngineErrors";

		// Token: 0x04001762 RID: 5986
		public const string AntimalwareType = "Antivirus";

		// Token: 0x04001763 RID: 5987
		public const string ClassificationType = "Classification";

		// Token: 0x04001764 RID: 5988
		public const string workitemNameFormat = "{0}{1}{2}";

		// Token: 0x04001765 RID: 5989
		public const string CmdletGetFailedUpdatesCounter = "Get-Counter";

		// Token: 0x04001766 RID: 5990
		public const string CmdletGetEngineUpdateInformation = "Get-EngineUpdateInformation";

		// Token: 0x04001767 RID: 5991
		public const string ConsecutiveFailedUpdatesCounterName = "\\msexchange hygiene updates engine(*)\\consecutive failed updates";

		// Token: 0x04001768 RID: 5992
		public const string PseudoLocUpdatesEngineCounterCategory = "[MSExchange Hygiene Updates Engine xxx xxx xxx xx]";

		// Token: 0x04001769 RID: 5993
		public const string PseudoLocConsecutiveFailedUpdatesCounterName = "\\[msexchange hygiene updates engine xxx xxx xxx xx](*)\\[consecutive failed updates xxx xxx xxx]";

		// Token: 0x0400176A RID: 5994
		public const int DefaultConsecutiveFailures = 8;

		// Token: 0x0400176B RID: 5995
		public const int MaximumConsecutiveFailures = 24;

		// Token: 0x0400176C RID: 5996
		public const int MinimumConsecutiveFailures = 2;

		// Token: 0x0400176D RID: 5997
		public const int DefaultFailedEngine = 1;

		// Token: 0x0400176E RID: 5998
		public const int MinimumFailedEngine = 1;

		// Token: 0x0400176F RID: 5999
		public const int MaximumFailedEngine = 3;
	}
}
