using System;
using System.Reflection;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders
{
	// Token: 0x020000D5 RID: 213
	internal class EscalateResponderWithCustomMessage : EscalateResponder
	{
		// Token: 0x060006E6 RID: 1766 RVA: 0x00028D20 File Offset: 0x00026F20
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, ServiceHealthStatus targetHealthState, string escalationTeam, string escalationSubjectUnhealthy, string escalationMessageUnhealthy, bool enabled = true, NotificationServiceClass notificationServiceClass = NotificationServiceClass.Urgent, int minimumSecondsBetweenEscalates = 14400, string dailySchedulePattern = "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59")
		{
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(name, serviceName, alertTypeId, alertMask, targetResource, targetHealthState, escalationTeam, escalationSubjectUnhealthy, escalationMessageUnhealthy, enabled, notificationServiceClass, minimumSecondsBetweenEscalates, string.Empty, false);
			responderDefinition.AssemblyPath = EscalateResponderWithCustomMessage.AssemblyPath;
			responderDefinition.TypeName = EscalateResponderWithCustomMessage.TypeName;
			return responderDefinition;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00028D64 File Offset: 0x00026F64
		internal override void GetEscalationSubjectAndMessage(MonitorResult monitorResult, out string escalationSubject, out string escalationMessage, bool rethrow = false, Action<ResponseMessageReader> textGeneratorModifier = null)
		{
			escalationSubject = base.Definition.EscalationSubject;
			escalationMessage = this.GetCustomMessage();
			if (string.IsNullOrEmpty(escalationMessage))
			{
				escalationMessage = base.Definition.EscalationMessage;
				base.GetEscalationSubjectAndMessage(monitorResult, out escalationSubject, out escalationMessage, rethrow, textGeneratorModifier);
				return;
			}
			lock (EscalateResponderHelper.CustomMessageDictionaryLock)
			{
				EscalateResponderHelper.AlertMaskToCustomMessageMap.Remove(base.Definition.AlertMask);
			}
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00028DEC File Offset: 0x00026FEC
		private string GetCustomMessage()
		{
			EscalateResponderHelper.AdditionalMessageContainer additionalMessageContainer = new EscalateResponderHelper.AdditionalMessageContainer(DateTime.MinValue, string.Empty);
			string result = string.Empty;
			DateTime t = DateTime.UtcNow.AddMinutes(-15.0);
			try
			{
				lock (EscalateResponderHelper.CustomMessageDictionaryLock)
				{
					EscalateResponderHelper.AlertMaskToCustomMessageMap.TryGetValue(base.Definition.AlertMask, out additionalMessageContainer);
				}
				if (!string.IsNullOrEmpty(additionalMessageContainer.additionalMessage) && additionalMessageContainer.updateTime >= t)
				{
					result = additionalMessageContainer.additionalMessage;
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceError<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Failed to add message from file: {0}", ex.ToString(), null, "GetCustomMessage", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\EscalateResponderWithCustomMessage.cs", 155);
			}
			return result;
		}

		// Token: 0x0400048D RID: 1165
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400048E RID: 1166
		private static readonly string TypeName = typeof(EscalateResponderWithCustomMessage).FullName;
	}
}
