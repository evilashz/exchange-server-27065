using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Store.Responders
{
	// Token: 0x020004E7 RID: 1255
	public class StoreWatsonResponder : WatsonResponder
	{
		// Token: 0x06001F20 RID: 7968 RVA: 0x000BE760 File Offset: 0x000BC960
		internal static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, string targetExtension, ServiceHealthStatus targetHealthState, string exceptionType, string watsonEventType)
		{
			ResponderDefinition responderDefinition = WatsonResponder.CreateDefinition(name, serviceName, alertTypeId, alertMask, targetResource, targetHealthState, exceptionType, watsonEventType, ReportOptions.None);
			responderDefinition.TargetExtension = targetExtension;
			responderDefinition.AssemblyPath = StoreWatsonResponder.AssemblyPath;
			responderDefinition.TypeName = StoreWatsonResponder.TypeName;
			return responderDefinition;
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x000BE7A0 File Offset: 0x000BC9A0
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.StoreTracer, base.TraceContext, "StoreWatsonResponder.DoResponderWork: Starting store watson responder", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Store\\StoreWatsonResponder.cs", 87);
			Guid databaseGuid;
			if (!Guid.TryParse(base.Definition.TargetExtension, out databaseGuid))
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.StoreTracer, base.TraceContext, "StoreWatsonResponder.DoResponderWork: skipping watson responder due to invalid database guid", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Store\\StoreWatsonResponder.cs", 141);
				base.Result.StateAttribute1 = base.Definition.TargetResource;
				base.Result.StateAttribute2 = base.Definition.TargetExtension;
				base.Result.StateAttribute5 = "InvalidDatabaseGuidSkippingWatson";
				return;
			}
			int num;
			if (!int.TryParse(base.Definition.Attributes["CopyQueueLengthThreshold"], out num))
			{
				num = 10;
			}
			int num2;
			if (!int.TryParse(base.Definition.Attributes["ReplayQueueLengthThreshold"], out num2))
			{
				num2 = 100;
			}
			base.Result.StateAttribute1 = base.Definition.TargetResource;
			base.Result.StateAttribute2 = base.Definition.TargetExtension;
			base.Result.StateAttribute6 = (double)num;
			base.Result.StateAttribute7 = (double)num2;
			if (this.AllowedToInvokeWatson(databaseGuid, num, num2))
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.StoreTracer, base.TraceContext, "StoreWatsonResponder.DoResponderWork: Invoking watson responder after verification of healthy copies", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Store\\StoreWatsonResponder.cs", 122);
				base.Result.StateAttribute5 = "HaveHealthyCopiesInvokingWatson";
				base.DoResponderWork(cancellationToken);
				return;
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.StoreTracer, base.TraceContext, "StoreWatsonResponder.DoResponderWork: Skipping watson responder due to NOT enough healthy copies", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Store\\StoreWatsonResponder.cs", 134);
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x000BE93C File Offset: 0x000BCB3C
		private bool AllowedToInvokeWatson(Guid databaseGuid, int copyQueueThreshold, int replayQueueThreshold)
		{
			List<CopyStatusClientCachedEntry> allCopyStatusesForDatabase = CachedDbStatusReader.Instance.GetAllCopyStatusesForDatabase(databaseGuid);
			if (allCopyStatusesForDatabase == null)
			{
				base.Result.StateAttribute5 = "UnableToGetCopyStatus";
				return false;
			}
			if (allCopyStatusesForDatabase.Count == 1)
			{
				return true;
			}
			StringBuilder stringBuilder = new StringBuilder(40);
			foreach (CopyStatusClientCachedEntry copyStatusClientCachedEntry in allCopyStatusesForDatabase)
			{
				if (copyStatusClientCachedEntry != null && copyStatusClientCachedEntry.Result == CopyStatusRpcResult.Success)
				{
					stringBuilder.AppendLine(string.Format("MailboxServer={0}, CopyStatus={1}", copyStatusClientCachedEntry.CopyStatus.MailboxServer, copyStatusClientCachedEntry.CopyStatus.CopyStatus));
					if (string.Equals(copyStatusClientCachedEntry.CopyStatus.MailboxServer, Environment.MachineName, StringComparison.OrdinalIgnoreCase))
					{
						if (!copyStatusClientCachedEntry.IsActive)
						{
							base.Result.StateAttribute4 = stringBuilder.ToString();
							return true;
						}
					}
					else if (copyStatusClientCachedEntry.CopyStatus.CopyStatus == CopyStatusEnum.Healthy && copyStatusClientCachedEntry.CopyStatus.GetCopyQueueLength() <= (long)copyQueueThreshold && copyStatusClientCachedEntry.CopyStatus.GetReplayQueueLength() <= (long)replayQueueThreshold)
					{
						base.Result.StateAttribute4 = stringBuilder.ToString();
						return true;
					}
				}
			}
			base.Result.StateAttribute4 = stringBuilder.ToString();
			base.Result.StateAttribute5 = "NotEnoughHealthyCopiesSkippingWatson";
			return false;
		}

		// Token: 0x04001681 RID: 5761
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04001682 RID: 5762
		private static readonly string TypeName = typeof(StoreWatsonResponder).FullName;
	}
}
