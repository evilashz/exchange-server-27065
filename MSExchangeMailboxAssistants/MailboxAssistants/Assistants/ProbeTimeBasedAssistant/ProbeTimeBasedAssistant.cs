using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ProbeTimeBasedAssistant
{
	// Token: 0x0200023F RID: 575
	internal sealed class ProbeTimeBasedAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase
	{
		// Token: 0x0600159E RID: 5534 RVA: 0x0007A746 File Offset: 0x00078946
		public ProbeTimeBasedAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x0007A754 File Offset: 0x00078954
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			this.TraceDebugExecuting(ExTraceGlobals.ProbeTimeBasedAssistantTracer.IsTraceEnabled(TraceType.DebugTrace) ? MethodBase.GetCurrentMethod().Name : string.Empty);
			if (ExTraceGlobals.ProbeTimeBasedAssistantTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.ProbeTimeBasedAssistantTracer.TraceDebug<ProbeTimeBasedAssistant, Guid>((long)this.GetHashCode(), "{0} is starting to process mailbox with GUID {1}", this, invokeArgs.MailboxData.MailboxGuid);
			}
			customDataToLog.Add(new KeyValuePair<string, object>("ProbeTimeBasedAssistant", invokeArgs.MailboxData.MailboxGuid));
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0007A7D4 File Offset: 0x000789D4
		public void OnWorkCycleCheckpoint()
		{
			this.TraceDebugExecuting(ExTraceGlobals.ProbeTimeBasedAssistantTracer.IsTraceEnabled(TraceType.DebugTrace) ? MethodBase.GetCurrentMethod().Name : string.Empty);
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x0007A7FA File Offset: 0x000789FA
		public override AssistantTaskContext InitialStep(AssistantTaskContext context)
		{
			this.TraceDebugExecuting(ExTraceGlobals.ProbeTimeBasedAssistantTracer.IsTraceEnabled(TraceType.DebugTrace) ? MethodBase.GetCurrentMethod().Name : string.Empty);
			return base.InitialStep(context);
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x0007A827 File Offset: 0x00078A27
		public override AssistantTaskContext InitializeContext(MailboxData data, TimeBasedDatabaseJob job)
		{
			this.TraceDebugExecuting(ExTraceGlobals.ProbeTimeBasedAssistantTracer.IsTraceEnabled(TraceType.DebugTrace) ? MethodBase.GetCurrentMethod().Name : string.Empty);
			return base.InitializeContext(data, job);
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0007A858 File Offset: 0x00078A58
		public override List<ResourceKey> GetResourceDependencies()
		{
			this.TraceDebugExecuting(ExTraceGlobals.ProbeTimeBasedAssistantTracer.IsTraceEnabled(TraceType.DebugTrace) ? MethodBase.GetCurrentMethod().Name : string.Empty);
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
			if (!snapshot.WorkloadManagement.DiskLatency.Enabled)
			{
				return base.GetResourceDependencies();
			}
			List<ResourceKey> resourceDependencies = base.GetResourceDependencies();
			resourceDependencies.Add(new DiskLatencyResourceKey(base.DatabaseInfo.Guid));
			return resourceDependencies;
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0007A8D2 File Offset: 0x00078AD2
		public override MailboxData CreateOnDemandMailboxData(Guid itemGuid, string parameters)
		{
			this.TraceDebugExecuting(ExTraceGlobals.ProbeTimeBasedAssistantTracer.IsTraceEnabled(TraceType.DebugTrace) ? MethodBase.GetCurrentMethod().Name : string.Empty);
			return base.CreateOnDemandMailboxData(itemGuid, parameters);
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x0007A900 File Offset: 0x00078B00
		private void TraceDebugExecuting(string method)
		{
			if (ExTraceGlobals.ProbeTimeBasedAssistantTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.ProbeTimeBasedAssistantTracer.TraceDebug<ProbeTimeBasedAssistant, string>((long)this.GetHashCode(), "{0} is executing {1}", this, method);
			}
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0007A927 File Offset: 0x00078B27
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0007A92F File Offset: 0x00078B2F
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0007A937 File Offset: 0x00078B37
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}
	}
}
