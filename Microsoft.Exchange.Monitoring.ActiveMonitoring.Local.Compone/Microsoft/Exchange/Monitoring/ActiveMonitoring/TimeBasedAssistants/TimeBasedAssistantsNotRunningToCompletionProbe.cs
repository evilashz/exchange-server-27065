using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x02000531 RID: 1329
	internal class TimeBasedAssistantsNotRunningToCompletionProbe : ProbeWorkItem
	{
		// Token: 0x060020B8 RID: 8376 RVA: 0x000C7878 File Offset: 0x000C5A78
		protected override void DoWork(CancellationToken cancellationToken)
		{
			base.Result.StateAttribute1 = base.Definition.TargetResource;
			base.Result.StateAttribute2 = base.Definition.TargetExtension;
			TimeBasedAssistantsNotRunningToCompletionCriteria timeBasedAssistantsNotRunningToCompletionCriteria = new TimeBasedAssistantsNotRunningToCompletionCriteria();
			Dictionary<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>> fullDiagnostics = TimeBasedAssistantsDiscoveryHelpers.ReadTimeBasedAssistantsDiagnostics("history");
			Dictionary<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>> dictionary = timeBasedAssistantsNotRunningToCompletionCriteria.FindOutOfCriteria(fullDiagnostics);
			if (dictionary.Any<KeyValuePair<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>>>())
			{
				string error = TimeBasedAssistantsDiscoveryHelpers.GenerateMessageForLastNFailures(dictionary, timeBasedAssistantsNotRunningToCompletionCriteria);
				throw new AssistantsNotRunningToCompletionException(error);
			}
		}

		// Token: 0x04001801 RID: 6145
		private const string AssistantsComponentArgument = "history";
	}
}
