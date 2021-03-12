using System;
using System.Reflection;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders
{
	// Token: 0x020000D4 RID: 212
	public class ConditionalEscalateResponder : EscalateResponder
	{
		// Token: 0x060006E2 RID: 1762 RVA: 0x00028BAC File Offset: 0x00026DAC
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, ServiceHealthStatus targetHealthState, string escalationTeam, string escalationSubject, string escalationMessage, string conditionalEscalateProperty, NotificationServiceClass notificationServiceClass, bool enabled = true, int recurrenceIntervalSeconds = 300)
		{
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(name, serviceName, alertTypeId, alertMask, targetResource, targetHealthState, escalationTeam, escalationSubject, escalationMessage, enabled, notificationServiceClass, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition.AssemblyPath = ConditionalEscalateResponder.AssemblyPath;
			responderDefinition.TypeName = ConditionalEscalateResponder.TypeName;
			responderDefinition.RecurrenceIntervalSeconds = recurrenceIntervalSeconds;
			responderDefinition.Attributes[ConditionalEscalateResponder.ConditionalPropertyString] = conditionalEscalateProperty;
			return responderDefinition;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00028C10 File Offset: 0x00026E10
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			ProbeResult lastFailedProbeResult = WorkItemResultHelper.GetLastFailedProbeResult(this, base.Broker, cancellationToken);
			string text;
			if (lastFailedProbeResult != null && base.Definition.Attributes.TryGetValue(ConditionalEscalateResponder.ConditionalPropertyString, out text) && !string.IsNullOrWhiteSpace(text))
			{
				Type type = lastFailedProbeResult.GetType();
				PropertyInfo property = type.GetProperty(text);
				if (property != null)
				{
					string text2 = (string)property.GetValue(lastFailedProbeResult, null);
					string value;
					bool flag;
					if (!string.IsNullOrWhiteSpace(text2) && base.Definition.Attributes.TryGetValue(text2, out value) && !string.IsNullOrWhiteSpace(value) && bool.TryParse(value, out flag) && flag)
					{
						base.Result.StateAttribute4 = string.Format("Suppressing escalation for exception or criteria {0}", text2);
						return;
					}
				}
			}
			base.Result.StateAttribute4 = string.Format("Unable to suppress, escalating", new object[0]);
			base.DoResponderWork(cancellationToken);
		}

		// Token: 0x0400048A RID: 1162
		internal static readonly string ConditionalPropertyString = "ConditionalProperty";

		// Token: 0x0400048B RID: 1163
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400048C RID: 1164
		private static readonly string TypeName = typeof(ConditionalEscalateResponder).FullName;
	}
}
