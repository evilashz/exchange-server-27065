using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x02000521 RID: 1313
	internal class TimeBasedAssistantsActiveDatabaseProbe : ProbeWorkItem
	{
		// Token: 0x06002053 RID: 8275 RVA: 0x000C5D04 File Offset: 0x000C3F04
		protected override void DoWork(CancellationToken cancellationToken)
		{
			base.Result.StateAttribute1 = base.Definition.TargetResource;
			base.Result.StateAttribute2 = base.Definition.TargetExtension;
			Dictionary<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>> fullDiagnostics = TimeBasedAssistantsDiscoveryHelpers.ReadTimeBasedAssistantsDiagnostics("summary");
			TimeBasedAssistantsActiveDatabaseCriteria timeBasedAssistantsActiveDatabaseCriteria = new TimeBasedAssistantsActiveDatabaseCriteria();
			List<KeyValuePair<string, Guid[]>> list = timeBasedAssistantsActiveDatabaseCriteria.FindOutOfCriteria(fullDiagnostics, false);
			if (list.Any<KeyValuePair<string, Guid[]>>())
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<string, Guid[]> keyValuePair in list)
				{
					stringBuilder.Append(string.Format("Assistant: {0}, not running databases: {1}", keyValuePair.Key, string.Join<Guid>(",", keyValuePair.Value)));
				}
				throw new AssistantsActiveDatabaseException(stringBuilder.ToString());
			}
		}

		// Token: 0x040017C5 RID: 6085
		private const string AssistantsComponentArgument = "summary";
	}
}
