using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000005 RID: 5
	internal abstract class ActiveDirectoryConnectivityBase : DisposeTrackableBase
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002A44 File Offset: 0x00000C44
		protected ActiveDirectoryConnectivityBase(ActiveDirectoryConnectivityContext context)
		{
			this.context = context;
			this.CanContinue = true;
		}

		// Token: 0x06000030 RID: 48
		protected abstract IEnumerable<AsyncResult<ActiveDirectoryConnectivityOutcome>> BuildTransactions();

		// Token: 0x06000031 RID: 49 RVA: 0x00002A5A File Offset: 0x00000C5A
		protected ActiveDirectoryConnectivityOutcome CreateOutcome(TestActiveDirectoryConnectivityTask.ScenarioId id, LocalizedString scenario, string performanceCounter, string domainController)
		{
			return new ActiveDirectoryConnectivityOutcome(this.context, id, scenario, performanceCounter, domainController);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A6C File Offset: 0x00000C6C
		protected void WriteVerbose(LocalizedString message)
		{
			this.context.WriteVerbose(message);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A7A File Offset: 0x00000C7A
		protected void WriteWarning(LocalizedString message)
		{
			this.context.WriteWarning(message);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A88 File Offset: 0x00000C88
		protected void WriteDebug(LocalizedString message)
		{
			this.context.WriteDebug(message);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A98 File Offset: 0x00000C98
		protected void AddMonitoringEvent(TestActiveDirectoryConnectivityTask.ScenarioId eventId, EventTypeEnumeration eventType, LocalizedString eventMessage)
		{
			if (this.context.MonitoringData == null)
			{
				return;
			}
			string text = eventMessage.ToString();
			if (eventType != EventTypeEnumeration.Success)
			{
				text = this.context.GetDiagnosticInfo(text);
			}
			MonitoringEvent item = new MonitoringEvent(TestActiveDirectoryConnectivityTask.CmdletMonitoringEventSource, (int)eventId, eventType, text);
			this.context.MonitoringData.Events.Add(item);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002AF5 File Offset: 0x00000CF5
		protected ActiveDirectoryConnectivityContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002AFD File Offset: 0x00000CFD
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002B05 File Offset: 0x00000D05
		protected bool CanContinue { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002B0E File Offset: 0x00000D0E
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002B16 File Offset: 0x00000D16
		protected DomainController CurrentDomainController { get; set; }

		// Token: 0x0600003B RID: 59 RVA: 0x00002B20 File Offset: 0x00000D20
		protected LocalizedString GenerateErrorMessage(TestActiveDirectoryConnectivityTask.ScenarioId scenario, int errorCode, string errorMessage, LocalizedException e)
		{
			string error = errorCode.ToString() + ((errorMessage == null) ? "" : ("(" + errorMessage + ")"));
			return Strings.ActiveDirectoryConnectivityTestFailed(scenario.ToString(), error, (e == null) ? "<Null>" : e.ToString());
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B77 File Offset: 0x00000D77
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ActiveDirectoryConnectivityBase>(this);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002B80 File Offset: 0x00000D80
		protected static AsyncResult<ActiveDirectoryConnectivityOutcome> SingleCommandTransactionAsync(ActiveDirectoryConnectivityBase.ActiveDirectoryConnectivityAsyncTask setupCommand)
		{
			AsyncResult<ActiveDirectoryConnectivityOutcome> asyncResult = new AsyncResult<ActiveDirectoryConnectivityOutcome>();
			setupCommand(asyncResult);
			return asyncResult;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B9C File Offset: 0x00000D9C
		protected static AsyncResult<ActiveDirectoryConnectivityOutcome> SingleCommandTransactionSync(ActiveDirectoryConnectivityBase.ActiveDirectoryConnectivityTask command)
		{
			AsyncResult<ActiveDirectoryConnectivityOutcome> result = new AsyncResult<ActiveDirectoryConnectivityOutcome>();
			ActiveDirectoryConnectivityBase.SingleCommandTransaction(command, result);
			return result;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002BB8 File Offset: 0x00000DB8
		protected static AsyncResult<ActiveDirectoryConnectivityOutcome> SingleCommandTransaction(ActiveDirectoryConnectivityBase.ActiveDirectoryConnectivityTask command, AsyncResult<ActiveDirectoryConnectivityOutcome> result)
		{
			ExDateTime now = ExDateTime.Now;
			ActiveDirectoryConnectivityOutcome item = command();
			result.Outcomes.Add(item);
			result.Complete();
			return result;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002BE7 File Offset: 0x00000DE7
		protected virtual string GenerateReportContext()
		{
			return null;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002E80 File Offset: 0x00001080
		internal static IEnumerable<AsyncResult<ActiveDirectoryConnectivityOutcome>> BuildTransactionHelper(params Func<ActiveDirectoryConnectivityBase>[] createInstances)
		{
			int i = 0;
			while (i < createInstances.Length)
			{
				Func<ActiveDirectoryConnectivityBase> createInstance = createInstances[i];
				using (ActiveDirectoryConnectivityBase instance = createInstance())
				{
					if (instance != null)
					{
						goto IL_78;
					}
				}
				IL_DF:
				i++;
				continue;
				goto IL_DF;
				IL_78:
				ActiveDirectoryConnectivityBase instance;
				foreach (AsyncResult<ActiveDirectoryConnectivityOutcome> transaction in instance.BuildTransactions())
				{
					yield return transaction;
				}
				goto IL_DF;
			}
			yield break;
		}

		// Token: 0x04000027 RID: 39
		private readonly ActiveDirectoryConnectivityContext context;

		// Token: 0x02000006 RID: 6
		// (Invoke) Token: 0x06000043 RID: 67
		protected delegate ActiveDirectoryConnectivityOutcome ActiveDirectoryConnectivityTask();

		// Token: 0x02000007 RID: 7
		// (Invoke) Token: 0x06000047 RID: 71
		protected delegate void ActiveDirectoryConnectivityAsyncTask(AsyncResult<ActiveDirectoryConnectivityOutcome> state);
	}
}
