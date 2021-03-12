using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Dar;
using Microsoft.Exchange.Management.CompliancePolicy.DarTasks;
using Microsoft.Office.CompliancePolicy.Exchange.Dar.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Dar
{
	// Token: 0x02000265 RID: 613
	internal class TaskStoreTimeBasedAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase
	{
		// Token: 0x060016BA RID: 5818 RVA: 0x000804CB File Offset: 0x0007E6CB
		public TaskStoreTimeBasedAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00080534 File Offset: 0x0007E734
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			if (invokeArgs.StoreSession.MailboxOwner.Alias.Contains("SystemMailbox{e0dc1c29-89c3-4034-b678-e6c29d823ed9}"))
			{
				try
				{
					ExceptionHandler.Handle(delegate
					{
						using (HostRpcClient hostRpcClient = new HostRpcClient(Environment.MachineName))
						{
							hostRpcClient.EnsureTenantMonitoring(invokeArgs.StoreSession.OrganizationId.GetBytes(Encoding.UTF8));
						}
					}, new ExceptionGroupHandler(ExceptionGroupHandlers.Rpc), new ExceptionHandlingOptions(TimeSpan.FromMinutes(1.0))
					{
						ClientId = "TaskStoreEventBasedAssistant",
						Operation = "EnsureTenantMonitoring"
					});
				}
				catch (AggregateException ex)
				{
					ExTraceGlobals.GeneralTracer.TraceError<string>((long)this.GetHashCode(), "Error during call to EnsureTenantMonitoring {0}", ex.ToString());
				}
			}
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x000805F0 File Offset: 0x0007E7F0
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x000805F2 File Offset: 0x0007E7F2
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x000805FA File Offset: 0x0007E7FA
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00080602 File Offset: 0x0007E802
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}
	}
}
