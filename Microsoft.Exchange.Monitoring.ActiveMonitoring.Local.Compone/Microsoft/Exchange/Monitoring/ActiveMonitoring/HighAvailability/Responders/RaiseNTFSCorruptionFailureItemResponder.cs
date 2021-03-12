using System;
using System.Reflection;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Responders
{
	// Token: 0x020001CC RID: 460
	public class RaiseNTFSCorruptionFailureItemResponder : RaiseFailureItemResponder
	{
		// Token: 0x06000D08 RID: 3336 RVA: 0x00055010 File Offset: 0x00053210
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, ServiceHealthStatus targetHealthState, string throttleGroupName = null)
		{
			return new ResponderDefinition
			{
				AssemblyPath = RaiseNTFSCorruptionFailureItemResponder.AssemblyPath,
				TypeName = RaiseNTFSCorruptionFailureItemResponder.TypeName,
				Name = name,
				ServiceName = serviceName,
				AlertTypeId = alertTypeId,
				AlertMask = alertMask,
				TargetResource = targetResource,
				TargetHealthState = targetHealthState,
				RecurrenceIntervalSeconds = 3600,
				WaitIntervalSeconds = 3600,
				TimeoutSeconds = 300,
				MaxRetryAttempts = 48,
				Enabled = true
			};
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00055096 File Offset: 0x00053296
		protected override string GetFailureItemMessage()
		{
			return Strings.FailureItemMessageForNTFSCorruption;
		}

		// Token: 0x0400099D RID: 2461
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400099E RID: 2462
		private static readonly string TypeName = typeof(RaiseNTFSCorruptionFailureItemResponder).FullName;
	}
}
