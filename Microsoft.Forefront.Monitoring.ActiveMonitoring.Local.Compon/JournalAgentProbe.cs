using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000054 RID: 84
	public sealed class JournalAgentProbe : TransportSmtpProbe
	{
		// Token: 0x0600023B RID: 571 RVA: 0x0000EA30 File Offset: 0x0000CC30
		protected override bool VerifyPreviousResults(CancellationToken cancellationToken)
		{
			if (!base.VerifyPreviousResults(cancellationToken) || !base.ResultsFromPreviousRun.Any<Notification>())
			{
				return false;
			}
			Notification notification = base.ResultsFromPreviousRun.FirstOrDefault((Notification item) => item.Value.Contains("ORIG"));
			base.TraceDebugCheckMail("prevResult={0}. {1}", new object[]
			{
				(notification == null) ? string.Empty : notification.Type,
				(notification == null) ? string.Empty : notification.Value
			});
			if (notification == null)
			{
				return false;
			}
			Match match = Regex.Match(notification.Value, "ruleid=(?<ruleid>[^|]*).*\\|mid=(?<mid>[^|]*)", RegexOptions.IgnoreCase);
			if (!match.Success)
			{
				return false;
			}
			base.CheckMailResult.StateAttribute23 = match.Groups["ruleid"].Value;
			base.CheckMailResult.StateAttribute24 = match.Groups["mid"].Value;
			return true;
		}
	}
}
