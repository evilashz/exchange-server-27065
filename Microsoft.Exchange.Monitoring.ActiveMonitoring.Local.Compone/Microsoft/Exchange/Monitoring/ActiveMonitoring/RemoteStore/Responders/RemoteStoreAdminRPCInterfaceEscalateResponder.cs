using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.RemoteStore.Responders
{
	// Token: 0x02000422 RID: 1058
	public class RemoteStoreAdminRPCInterfaceEscalateResponder : EscalateResponder
	{
		// Token: 0x06001AEB RID: 6891 RVA: 0x0009506C File Offset: 0x0009326C
		public static ResponderDefinition CreateDefinition(string responderName, string serviceName, string alertTypeId, string alertMask, string targetResource, ServiceHealthStatus targetHealthState, string escalationTeam, string escalationSubject, string escalationMessage, NotificationServiceClass notificationServiceClass, bool enabled = true, int recurrenceIntervalSeconds = 300)
		{
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(responderName, serviceName, alertTypeId, alertMask, targetResource, targetHealthState, escalationTeam, escalationSubject, escalationMessage, enabled, notificationServiceClass, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition.RecurrenceIntervalSeconds = recurrenceIntervalSeconds;
			responderDefinition.AssemblyPath = RemoteStoreAdminRPCInterfaceEscalateResponder.AssemblyPath;
			responderDefinition.TypeName = RemoteStoreAdminRPCInterfaceEscalateResponder.TypeName;
			return responderDefinition;
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x000950BC File Offset: 0x000932BC
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			MonitorResult lastFailedMonitorResult = WorkItemResultHelper.GetLastFailedMonitorResult(this, base.Broker, cancellationToken);
			if (lastFailedMonitorResult != null && lastFailedMonitorResult.IsAlert && !string.IsNullOrWhiteSpace(lastFailedMonitorResult.StateAttribute1))
			{
				base.Result.StateAttribute5 = lastFailedMonitorResult.StateAttribute1;
				if (!this.RaiseEscalationBasedOnExceptionType(lastFailedMonitorResult.StateAttribute1, base.Definition.Attributes))
				{
					base.Result.StateAttribute4 = "SuppressedEscalationBasedOnExceptionType";
					return;
				}
			}
			base.Result.StateAttribute4 = "UnableToSuppressEscalation";
			base.DoResponderWork(cancellationToken);
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00095144 File Offset: 0x00093344
		internal bool RaiseEscalationBasedOnExceptionType(string data, Dictionary<string, string> extensionAttributes)
		{
			if (string.IsNullOrWhiteSpace(data) || extensionAttributes == null || extensionAttributes.Count == 0)
			{
				return true;
			}
			string[] array = data.TrimEnd(new char[]
			{
				']'
			}).Split(new char[]
			{
				']'
			});
			foreach (string text in array)
			{
				string[] array3 = text.Trim(new char[]
				{
					'['
				}).Split(new char[]
				{
					','
				});
				if (array3.Length > 1 && !string.IsNullOrWhiteSpace(array3[0]))
				{
					for (int j = 1; j < array3.Length; j++)
					{
						string text2 = array3[j].Trim();
						string value;
						bool flag;
						if (!string.IsNullOrWhiteSpace(text2) && extensionAttributes.TryGetValue(text2, out value) && !string.IsNullOrWhiteSpace(value) && bool.TryParse(value, out flag) && flag)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0400127B RID: 4731
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400127C RID: 4732
		private static readonly string TypeName = typeof(RemoteStoreAdminRPCInterfaceEscalateResponder).FullName;
	}
}
