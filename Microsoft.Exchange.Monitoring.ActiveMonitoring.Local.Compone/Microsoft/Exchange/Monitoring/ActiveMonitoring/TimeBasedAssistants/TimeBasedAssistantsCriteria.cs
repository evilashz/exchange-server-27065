using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x02000522 RID: 1314
	internal abstract class TimeBasedAssistantsCriteria
	{
		// Token: 0x06002055 RID: 8277 RVA: 0x000C5DE0 File Offset: 0x000C3FE0
		public Dictionary<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>> FindOutOfCriteria(Dictionary<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>> fullDiagnostics)
		{
			ArgumentValidator.ThrowIfNull("fullDiagnostics", fullDiagnostics);
			Dictionary<MailboxDatabase, WindowJob[]> dictionary = null;
			Dictionary<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>> dictionary2 = new Dictionary<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>>();
			foreach (KeyValuePair<AssistantInfo, Dictionary<MailboxDatabase, WindowJob[]>> keyValuePair in fullDiagnostics)
			{
				AssistantInfo key = keyValuePair.Key;
				IEnumerable<MailboxDatabase> keys = keyValuePair.Value.Keys;
				foreach (MailboxDatabase mailboxDatabase in keys)
				{
					if (mailboxDatabase.IsAssistantEnabled)
					{
						WindowJob[] array = keyValuePair.Value[mailboxDatabase];
						if (!this.IsSatisfied(key, mailboxDatabase, array))
						{
							if (dictionary == null)
							{
								dictionary = new Dictionary<MailboxDatabase, WindowJob[]>();
							}
							dictionary.Add(mailboxDatabase, array);
						}
					}
				}
				if (dictionary != null)
				{
					dictionary2.Add(key, dictionary);
					dictionary = null;
				}
			}
			return dictionary2;
		}

		// Token: 0x06002056 RID: 8278
		protected abstract bool IsSatisfied(AssistantInfo assistantInfo, MailboxDatabase database, WindowJob[] history);

		// Token: 0x040017C6 RID: 6086
		protected readonly DateTime CurrentSampleTime = DateTime.UtcNow;
	}
}
