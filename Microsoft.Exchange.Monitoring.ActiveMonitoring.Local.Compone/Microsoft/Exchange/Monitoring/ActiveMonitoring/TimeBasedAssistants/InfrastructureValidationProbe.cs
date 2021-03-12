﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x02000525 RID: 1317
	internal class InfrastructureValidationProbe : ProbeWorkItem
	{
		// Token: 0x06002061 RID: 8289 RVA: 0x000C6334 File Offset: 0x000C4534
		protected override void DoWork(CancellationToken cancellationToken)
		{
			List<Exception> list = new List<Exception>();
			base.Result.StateAttribute1 = base.Definition.TargetResource;
			base.Result.StateAttribute2 = base.Definition.TargetExtension;
			InfrastructureValidationCriteria infrastructureValidationCriteria = new InfrastructureValidationCriteria();
			Dictionary<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>> dictionary = TimeBasedAssistantsDiscoveryHelpers.ReadTimeBasedAssistantsDiagnostics("assistant=ProbeTimeBasedAssistant, history");
			if (dictionary.Count > 1)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (AssistantInfo assistantInfo in dictionary.Keys)
				{
					stringBuilder.Append(assistantInfo.AssistantName);
					stringBuilder.Append(";");
				}
				list.Add(new ParseDiagnosticsStringException(string.Format("Invalid Diagnostics Information '{0}' ", stringBuilder)));
			}
			else if (dictionary.Count == 1)
			{
				Dictionary<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>> dictionary2 = infrastructureValidationCriteria.FindOutOfCriteria(dictionary);
				if (dictionary2.Any<KeyValuePair<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>>>())
				{
					string error = TimeBasedAssistantsDiscoveryHelpers.GenerateMessageFromDiagnostics(dictionary2).ToString();
					list.Add(new InfrastructureValidationException(error));
				}
			}
			if (list.Any<Exception>())
			{
				throw new AggregateException(list);
			}
		}

		// Token: 0x040017D1 RID: 6097
		private const string AssistantsComponentArgument = "assistant=ProbeTimeBasedAssistant, history";
	}
}
