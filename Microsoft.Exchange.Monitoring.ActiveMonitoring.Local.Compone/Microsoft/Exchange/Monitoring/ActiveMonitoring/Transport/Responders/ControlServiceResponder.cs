using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Audit;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Transport.Responders
{
	// Token: 0x020004EE RID: 1262
	public class ControlServiceResponder : ResponderWorkItem
	{
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001F38 RID: 7992 RVA: 0x000BF15C File Offset: 0x000BD35C
		// (set) Token: 0x06001F39 RID: 7993 RVA: 0x000BF164 File Offset: 0x000BD364
		internal string WindowsServiceName { get; set; }

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001F3A RID: 7994 RVA: 0x000BF16D File Offset: 0x000BD36D
		// (set) Token: 0x06001F3B RID: 7995 RVA: 0x000BF175 File Offset: 0x000BD375
		internal int ControlCode { get; set; }

		// Token: 0x06001F3C RID: 7996 RVA: 0x000BF180 File Offset: 0x000BD380
		internal static ResponderDefinition CreateDefinition(string responderName, string monitorName, string windowsServiceName, ServiceHealthStatus responderTargetState, int controlCode, string throttleGroupName = null)
		{
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = ControlServiceResponder.AssemblyPath;
			responderDefinition.TypeName = ControlServiceResponder.TypeName;
			responderDefinition.Name = responderName;
			responderDefinition.ServiceName = "Exchange";
			responderDefinition.AlertTypeId = "*";
			responderDefinition.AlertMask = monitorName;
			responderDefinition.RecurrenceIntervalSeconds = 300;
			responderDefinition.TimeoutSeconds = 300;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.TargetHealthState = responderTargetState;
			responderDefinition.WaitIntervalSeconds = 30;
			responderDefinition.Enabled = true;
			responderDefinition.Attributes["WindowsServiceName"] = windowsServiceName;
			responderDefinition.Attributes["ControlCode"] = controlCode.ToString();
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, throttleGroupName, RecoveryActionId.ControlService, windowsServiceName, null);
			return responderDefinition;
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x000BF250 File Offset: 0x000BD450
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			this.InitializeAttributes();
			RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.ControlService, this.WindowsServiceName, this, true, cancellationToken, null);
			recoveryActionRunner.Execute(delegate()
			{
				this.InternalControlService(cancellationToken);
			});
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x000BF2A0 File Offset: 0x000BD4A0
		private void InitializeServiceAttributes(AttributeHelper attributeHelper)
		{
			this.WindowsServiceName = attributeHelper.GetString("WindowsServiceName", true, null);
			this.ControlCode = attributeHelper.GetInt("ControlCode", true, -1, null, null);
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x000BF2E8 File Offset: 0x000BD4E8
		private void InitializeAttributes()
		{
			AttributeHelper attributeHelper = new AttributeHelper(base.Definition);
			this.InitializeServiceAttributes(attributeHelper);
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x000BF388 File Offset: 0x000BD588
		private void InternalControlService(CancellationToken cancellationToken)
		{
			Privilege.RunWithPrivilege("SeDebugPrivilege", true, delegate
			{
				using (ServiceHelper serviceHelper = new ServiceHelper(this.WindowsServiceName, cancellationToken))
				{
					using (Process process = serviceHelper.GetProcess())
					{
						if (process == null)
						{
							return;
						}
					}
					serviceHelper.ControlService(this.ControlCode);
				}
			});
		}

		// Token: 0x040016D4 RID: 5844
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040016D5 RID: 5845
		private static readonly string TypeName = typeof(ControlServiceResponder).FullName;

		// Token: 0x020004EF RID: 1263
		internal static class AttributeNames
		{
			// Token: 0x040016D8 RID: 5848
			internal const string WindowsServiceName = "WindowsServiceName";

			// Token: 0x040016D9 RID: 5849
			internal const string ControlCode = "ControlCode";

			// Token: 0x040016DA RID: 5850
			internal const string ThrottleGroupName = "ThrottleGroupName";
		}

		// Token: 0x020004F0 RID: 1264
		internal static class DefaultValues
		{
			// Token: 0x040016DB RID: 5851
			internal const string ThrottleGroupName = "";
		}
	}
}
