using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Compliance.CrimsonEvents;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar.Diagnostics
{
	// Token: 0x02000012 RID: 18
	internal class LogItem
	{
		// Token: 0x06000098 RID: 152 RVA: 0x0000436C File Offset: 0x0000256C
		public LogItem(string component, string tag, ResultSeverityLevel severity, string message, string correlationId, string tenantId, Dictionary<string, string> customData)
		{
			if (string.IsNullOrEmpty(component))
			{
				throw new ArgumentException("component must have non-null/non-empty value");
			}
			this.eventNotificationItem = new EventNotificationItem("DarRuntime", component, tag, severity);
			this.eventNotificationItem.StateAttribute1 = correlationId;
			this.eventNotificationItem.StateAttribute2 = tenantId;
			this.eventNotificationItem.CustomProperties = customData;
			if (LogItem.IsInformational(severity))
			{
				this.eventNotificationItem.StateAttribute3 = message;
				this.eventNotificationItem.Message = null;
			}
			else
			{
				this.eventNotificationItem.Message = message;
			}
			this.eventNotificationItem.StateAttribute5 = Thread.CurrentThread.ManagedThreadId.ToString();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004419 File Offset: 0x00002619
		public static void Publish(string component, string tag)
		{
			new LogItem(component, tag, ResultSeverityLevel.Informational, null, null, null, null).Publish();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000442C File Offset: 0x0000262C
		public static void Publish(string component, string tag, string correlationId)
		{
			new LogItem(component, tag, ResultSeverityLevel.Informational, null, correlationId, null, null).Publish();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000443F File Offset: 0x0000263F
		public static void Publish(string component, string tag, string message, string correlationId)
		{
			new LogItem(component, tag, ResultSeverityLevel.Informational, message, correlationId, null, null).Publish();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004452 File Offset: 0x00002652
		public static void Publish(string component, string tag, string message, ResultSeverityLevel severity)
		{
			new LogItem(component, tag, severity, message, null, null, null).Publish();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004465 File Offset: 0x00002665
		public static void Publish(string component, string tag, string message, string correlationId, ResultSeverityLevel severity)
		{
			new LogItem(component, tag, severity, message, correlationId, null, null).Publish();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004479 File Offset: 0x00002679
		public static void Publish(string component, string tag, string message, string tenantId, string correlationId, ResultSeverityLevel severity)
		{
			new LogItem(component, tag, severity, message, correlationId, tenantId, null).Publish();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004490 File Offset: 0x00002690
		public void Publish()
		{
			string extensionXml = CrimsonHelper.ConvertDictionaryToXml(this.eventNotificationItem.CustomProperties);
			NativeMethods.ProbeResultUnmanaged probeResultUnmanaged = new ProbeResult
			{
				ResultName = this.eventNotificationItem.ResultName,
				SampleValue = this.eventNotificationItem.SampleValue,
				ServiceName = this.eventNotificationItem.ServiceName,
				IsNotified = true,
				ExecutionStartTime = this.eventNotificationItem.TimeStamp,
				ExecutionEndTime = this.eventNotificationItem.TimeStamp,
				Error = this.eventNotificationItem.Message,
				Exception = this.eventNotificationItem.Exception,
				ExtensionXml = extensionXml,
				StateAttribute1 = this.eventNotificationItem.StateAttribute1,
				StateAttribute2 = this.eventNotificationItem.StateAttribute2,
				StateAttribute3 = this.eventNotificationItem.StateAttribute3,
				StateAttribute4 = this.eventNotificationItem.StateAttribute4,
				StateAttribute5 = this.eventNotificationItem.StateAttribute5,
				WorkItemId = DefinitionIdGenerator<ProbeDefinition>.GetIdForNotification(this.eventNotificationItem.ResultName)
			}.ToUnmanaged();
			try
			{
				ComplianceCrimsonEvent complianceCrimsonEvent = ComplianceCrimsonEvents.EvtDarRuntimeLog;
				if (LogItem.IsInformational(this.eventNotificationItem.Severity))
				{
					complianceCrimsonEvent = ComplianceCrimsonEvents.EvtDarRuntimeInfo;
				}
				complianceCrimsonEvent.LogGeneric(new object[]
				{
					DateTime.UtcNow.Ticks.ToString(),
					probeResultUnmanaged.ServiceName,
					probeResultUnmanaged.ResultName,
					probeResultUnmanaged.WorkItemId.ToString(),
					LogItem.build,
					probeResultUnmanaged.MachineName,
					probeResultUnmanaged.Error,
					probeResultUnmanaged.Exception,
					probeResultUnmanaged.RetryCount.ToString(),
					probeResultUnmanaged.StateAttribute1,
					probeResultUnmanaged.StateAttribute2,
					probeResultUnmanaged.StateAttribute3,
					probeResultUnmanaged.StateAttribute4,
					probeResultUnmanaged.StateAttribute5,
					this.eventNotificationItem.Severity.ToString(),
					probeResultUnmanaged.ExecutionStartTime,
					probeResultUnmanaged.ExtensionXml,
					probeResultUnmanaged.Version.ToString()
				});
			}
			finally
			{
				if (LogSettings.IsMonitored(this.eventNotificationItem.ResultName))
				{
					this.eventNotificationItem.Publish(true);
				}
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004714 File Offset: 0x00002914
		private static bool IsInformational(ResultSeverityLevel severity)
		{
			return severity == ResultSeverityLevel.Informational || severity == ResultSeverityLevel.Verbose || severity == ResultSeverityLevel.Warning;
		}

		// Token: 0x04000034 RID: 52
		private static readonly string build = Assembly.GetExecutingAssembly().GetName().Version.ToString();

		// Token: 0x04000035 RID: 53
		private readonly EventNotificationItem eventNotificationItem;
	}
}
