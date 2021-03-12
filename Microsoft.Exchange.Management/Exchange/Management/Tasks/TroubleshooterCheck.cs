using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Monitoring;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000419 RID: 1049
	internal abstract class TroubleshooterCheck
	{
		// Token: 0x0600247F RID: 9343 RVA: 0x00091565 File Offset: 0x0008F765
		public TroubleshooterCheck(PropertyBag fields)
		{
			this.fields = fields;
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x00091574 File Offset: 0x0008F774
		public static List<TroubleshooterCheck> RunChecks(ICollection<TroubleshooterCheck> expectedCheckList, TroubleshooterCheck.ContinueToNextCheck continueToNextCheck, out MonitoringData results)
		{
			List<TroubleshooterCheck> list = new List<TroubleshooterCheck>();
			results = new MonitoringData();
			foreach (TroubleshooterCheck troubleshooterCheck in expectedCheckList)
			{
				bool errorsReturned = false;
				MonitoringData monitoringData = troubleshooterCheck.InternalRunCheck();
				foreach (MonitoringEvent monitoringEvent in monitoringData.Events)
				{
					if (monitoringEvent.EventType == EventTypeEnumeration.Error)
					{
						list.Add(troubleshooterCheck);
						errorsReturned = true;
						break;
					}
				}
				results.Events.AddRange(monitoringData.Events);
				results.PerformanceCounters.AddRange(monitoringData.PerformanceCounters);
				if (!continueToNextCheck(troubleshooterCheck, monitoringData, errorsReturned))
				{
					break;
				}
			}
			return list;
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x00091658 File Offset: 0x0008F858
		public virtual MonitoringData Resolve(MonitoringData monitoringData)
		{
			return monitoringData;
		}

		// Token: 0x06002482 RID: 9346
		public abstract MonitoringData InternalRunCheck();

		// Token: 0x06002483 RID: 9347 RVA: 0x0009165B File Offset: 0x0008F85B
		public static bool ShouldContinue(TroubleshooterCheck troubleshooterCheck, MonitoringData monitoringData, bool errorsReturned)
		{
			return !errorsReturned;
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x00091661 File Offset: 0x0008F861
		public MonitoringEvent TSResolutionFailed(string serverName)
		{
			return new MonitoringEvent(AssistantTroubleshooterBase.EventSource, 5204, EventTypeEnumeration.Error, Strings.TSResolutionFailed(serverName));
		}

		// Token: 0x04001CF2 RID: 7410
		public const string ResolveProblems = "ResolveProblems";

		// Token: 0x04001CF3 RID: 7411
		public const string MonitoringContext = "MonitoringContext";

		// Token: 0x04001CF4 RID: 7412
		protected PropertyBag fields;

		// Token: 0x0200041A RID: 1050
		// (Invoke) Token: 0x06002486 RID: 9350
		public delegate bool ContinueToNextCheck(TroubleshooterCheck task, MonitoringData taskResults, bool errorsReturned);
	}
}
