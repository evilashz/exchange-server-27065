using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x02000537 RID: 1335
	internal abstract class TimeBasedAssistantsOutOfSlaProbe : ProbeWorkItem
	{
		// Token: 0x060020C6 RID: 8390
		protected abstract TimeBasedAssistantsOutOfSlaDecisionMaker CreateDecisionMakerInstance();

		// Token: 0x060020C7 RID: 8391 RVA: 0x000C7EC4 File Offset: 0x000C60C4
		protected override void DoWork(CancellationToken cancellationToken)
		{
			base.Result.StateAttribute1 = base.Definition.TargetResource;
			base.Result.StateAttribute2 = base.Definition.TargetExtension;
			TimeBasedAssistantsOutOfSlaDecisionMaker timeBasedAssistantsOutOfSlaDecisionMaker = this.CreateDecisionMakerInstance();
			Dictionary<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>> fullDiagnostics = TimeBasedAssistantsDiscoveryHelpers.ReadTimeBasedAssistantsDiagnostics("history");
			Dictionary<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>> dictionary = timeBasedAssistantsOutOfSlaDecisionMaker.FindOutOfCriteria(fullDiagnostics);
			if (dictionary.Any<KeyValuePair<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>>>())
			{
				string error = TimeBasedAssistantsDiscoveryHelpers.GenerateMessageForLastNFailures(dictionary, timeBasedAssistantsOutOfSlaDecisionMaker);
				throw new AssistantsOutOfSlaException(error);
			}
		}

		// Token: 0x04001819 RID: 6169
		private const string AssistantsComponentArgument = "history";
	}
}
