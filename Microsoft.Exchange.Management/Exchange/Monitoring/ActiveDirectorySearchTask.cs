using System;
using System.Collections.Generic;
using System.DirectoryServices;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200000E RID: 14
	internal sealed class ActiveDirectorySearchTask : ActiveDirectoryConnectivityBase
	{
		// Token: 0x0600007E RID: 126 RVA: 0x0000380C File Offset: 0x00001A0C
		internal ActiveDirectorySearchTask(ActiveDirectoryConnectivityContext context) : base(context)
		{
			base.CanContinue = !context.Instance.SkipRemainingTests;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003948 File Offset: 0x00001B48
		protected override IEnumerable<AsyncResult<ActiveDirectoryConnectivityOutcome>> BuildTransactions()
		{
			if (base.Context.UseADDriver)
			{
				yield return ActiveDirectoryConnectivityBase.SingleCommandTransactionSync(new ActiveDirectoryConnectivityBase.ActiveDirectoryConnectivityTask(this.SearchUsingADDriver));
			}
			else
			{
				yield return ActiveDirectoryConnectivityBase.SingleCommandTransactionSync(new ActiveDirectoryConnectivityBase.ActiveDirectoryConnectivityTask(this.Search));
			}
			yield break;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003965 File Offset: 0x00001B65
		protected override void InternalDispose(bool isDisposing)
		{
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000039C4 File Offset: 0x00001BC4
		private ActiveDirectoryConnectivityOutcome Search()
		{
			return this.RunSearchOperationWithTimeCheck(delegate
			{
				int result;
				using (DirectorySearcher directorySearcher = new DirectorySearcher("(objectClass=*)"))
				{
					directorySearcher.SearchRoot = base.Context.CurrentDCDirectoryEntry;
					directorySearcher.SearchScope = SearchScope.Base;
					directorySearcher.FindAll();
					result = 1;
				}
				return result;
			});
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003A38 File Offset: 0x00001C38
		private ActiveDirectoryConnectivityOutcome SearchUsingADDriver()
		{
			return this.RunSearchOperationWithTimeCheck(delegate
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, "*");
				base.Context.RecipientSession.Find(ADSession.GetDomainNamingContextForLocalForest(), QueryScope.Base, filter, null, 2);
				base.Context.CurrentDomainController = base.Context.RecipientSession.LastUsedDc;
				return 1;
			});
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003A4C File Offset: 0x00001C4C
		private ActiveDirectoryConnectivityOutcome RunSearchOperationWithTimeCheck(Func<int> operation)
		{
			ActiveDirectoryConnectivityOutcome activeDirectoryConnectivityOutcome = base.CreateOutcome(TestActiveDirectoryConnectivityTask.ScenarioId.Search, Strings.ActiveDirectorySearchScenario, Strings.ActiveDirectorySearchScenario, base.Context.CurrentDomainController);
			if (!base.CanContinue)
			{
				base.WriteVerbose(Strings.CannotContinue(Strings.ActiveDirectorySearchScenario));
				activeDirectoryConnectivityOutcome.Update(CasTransactionResultEnum.Skipped);
				return activeDirectoryConnectivityOutcome;
			}
			try
			{
				operation();
				activeDirectoryConnectivityOutcome.UpdateTarget(base.Context.CurrentDomainController);
				TimeSpan timeSpan = ExDateTime.Now - activeDirectoryConnectivityOutcome.StartTime;
				if (timeSpan.TotalMilliseconds > (double)base.Context.Instance.SearchLatencyThresholdInMilliseconds)
				{
					activeDirectoryConnectivityOutcome.Update(CasTransactionResultEnum.Success, string.Format("Over Threshold. Threshold :{0} Actual {1}", base.Context.Instance.SearchLatencyThresholdInMilliseconds, timeSpan.TotalMilliseconds));
					base.Context.Instance.WriteVerbose(string.Format("Over Threshold. Threshold :{0} Actual {1}", base.Context.Instance.SearchLatencyThresholdInMilliseconds, timeSpan.TotalMilliseconds));
					base.AddMonitoringEvent(TestActiveDirectoryConnectivityTask.ScenarioId.SearchOverLatency, EventTypeEnumeration.Error, base.GenerateErrorMessage(TestActiveDirectoryConnectivityTask.ScenarioId.SearchLatency, 0, string.Format("Over Threshold. Threshold :{0} Actual {1}", base.Context.Instance.SearchLatencyThresholdInMilliseconds, timeSpan.TotalMilliseconds), null));
				}
				else
				{
					base.AddMonitoringEvent(TestActiveDirectoryConnectivityTask.ScenarioId.SearchLatency, EventTypeEnumeration.Success, base.GenerateErrorMessage(TestActiveDirectoryConnectivityTask.ScenarioId.SearchLatency, 0, string.Empty, null));
					activeDirectoryConnectivityOutcome.Update(CasTransactionResultEnum.Success);
				}
			}
			catch (Exception ex)
			{
				ActiveDirectorySearchError errorCode;
				if (ex is ADTransientException)
				{
					errorCode = ActiveDirectorySearchError.ADTransientException;
				}
				else
				{
					errorCode = ActiveDirectorySearchError.OtherException;
				}
				TimeSpan timeSpan = ExDateTime.Now - activeDirectoryConnectivityOutcome.StartTime;
				activeDirectoryConnectivityOutcome.Update(CasTransactionResultEnum.Failure);
				base.AddMonitoringEvent(TestActiveDirectoryConnectivityTask.ScenarioId.SearchFailed, EventTypeEnumeration.Error, base.GenerateErrorMessage(TestActiveDirectoryConnectivityTask.ScenarioId.Search, (int)errorCode, ex.ToString(), null));
			}
			return activeDirectoryConnectivityOutcome;
		}
	}
}
