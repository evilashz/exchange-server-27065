using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Compliance.CrimsonEvents;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Instrumentation
{
	// Token: 0x0200001A RID: 26
	internal class LogItem : EventNotificationItem
	{
		// Token: 0x0600005A RID: 90 RVA: 0x000049AA File Offset: 0x00002BAA
		public LogItem(string serviceName, string component, string notificationReason, ResultSeverityLevel severity = ResultSeverityLevel.Error) : base(serviceName, component, notificationReason, notificationReason, severity)
		{
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000049B8 File Offset: 0x00002BB8
		public LogItem(string serviceName, string component, string notificationReason, string message, ResultSeverityLevel severity = ResultSeverityLevel.Error) : base(serviceName, component, notificationReason, message, severity)
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000049C7 File Offset: 0x00002BC7
		public LogItem(string serviceName, string component, string notificationReason, string message, string stateAttribute1, ResultSeverityLevel severity = ResultSeverityLevel.Error) : base(serviceName, component, notificationReason, message, stateAttribute1, severity)
		{
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000049D8 File Offset: 0x00002BD8
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000049DF File Offset: 0x00002BDF
		public static bool DisableComplianceCrimsonEvents
		{
			get
			{
				return LogItem.disableComplianceCrimsonEvents;
			}
			set
			{
				LogItem.disableComplianceCrimsonEvents = value;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000049E8 File Offset: 0x00002BE8
		public new static void Publish(string serviceName, string component, string tag, string notificationReason, ResultSeverityLevel severity = ResultSeverityLevel.Error, bool throwOnError = false)
		{
			if (LogItem.DisableComplianceCrimsonEvents)
			{
				return;
			}
			if (string.IsNullOrEmpty(serviceName) || string.IsNullOrEmpty(component))
			{
				throw new ArgumentException("serviceName and component must have non-null/non-empty values.");
			}
			LogItem logItem = new LogItem(serviceName, component, tag, notificationReason, severity);
			logItem.Publish(throwOnError);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004A2C File Offset: 0x00002C2C
		public new static void Publish(string serviceName, string component, string tag, string notificationReason, string stateAttribute1, ResultSeverityLevel severity = ResultSeverityLevel.Error, bool throwOnError = false)
		{
			if (LogItem.DisableComplianceCrimsonEvents)
			{
				return;
			}
			if (string.IsNullOrEmpty(serviceName) || string.IsNullOrEmpty(component))
			{
				throw new ArgumentException("serviceName and component must have non-null/non-empty values.");
			}
			LogItem logItem = new LogItem(serviceName, component, tag, notificationReason, stateAttribute1, severity);
			logItem.Publish(throwOnError);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004A72 File Offset: 0x00002C72
		public new static void PublishPeriodic(string serviceName, string component, string tag, string notificationReason, string periodicKey, TimeSpan period, ResultSeverityLevel severity = ResultSeverityLevel.Error, bool throwOnError = false)
		{
			if (LogItem.DisableComplianceCrimsonEvents)
			{
				return;
			}
			if (string.IsNullOrEmpty(periodicKey))
			{
				throw new ArgumentException("periodicKey must have non-null/non-empty value.");
			}
			if (LogItem.CanPublishPeriodic(periodicKey, period))
			{
				LogItem.Publish(serviceName, component, tag, notificationReason, severity, throwOnError);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004AA8 File Offset: 0x00002CA8
		public new void Publish(bool throwOnError)
		{
			string extensionXml = CrimsonHelper.ConvertDictionaryToXml(base.CustomProperties);
			NativeMethods.ProbeResultUnmanaged probeResultUnmanaged = new ProbeResult
			{
				ResultName = base.ResultName,
				SampleValue = base.SampleValue,
				ServiceName = base.ServiceName,
				IsNotified = true,
				ExecutionStartTime = base.TimeStamp,
				ExecutionEndTime = base.TimeStamp,
				Error = base.Message,
				ExtensionXml = extensionXml,
				StateAttribute1 = base.StateAttribute1,
				StateAttribute2 = base.StateAttribute2,
				StateAttribute3 = base.StateAttribute3,
				StateAttribute4 = base.StateAttribute4,
				StateAttribute5 = base.StateAttribute5,
				WorkItemId = DefinitionIdGenerator<ProbeDefinition>.GetIdForNotification(base.ResultName)
			}.ToUnmanaged();
			try
			{
				ComplianceCrimsonEvent complianceCrimsonEvent = ComplianceCrimsonEvents.EvtDarRuntimeLog;
				if (base.Severity == ResultSeverityLevel.Informational)
				{
					complianceCrimsonEvent = ComplianceCrimsonEvents.EvtDiscoveryInfo;
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
					base.Severity.ToString(),
					probeResultUnmanaged.ExecutionStartTime,
					probeResultUnmanaged.ExtensionXml,
					probeResultUnmanaged.Version.ToString()
				});
			}
			catch
			{
				if (throwOnError)
				{
					throw;
				}
			}
			finally
			{
				if (LogSettings.IsMonitored(base.ResultName))
				{
					base.Publish(throwOnError);
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004CE4 File Offset: 0x00002EE4
		private static bool CanPublishPeriodic(string periodicKey, TimeSpan period)
		{
			bool result;
			lock (LogItem.periodicEventDictionary)
			{
				DateTime d;
				if (!LogItem.periodicEventDictionary.TryGetValue(periodicKey, out d) || DateTime.UtcNow > d + period)
				{
					LogItem.periodicEventDictionary[periodicKey] = DateTime.UtcNow;
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x04000047 RID: 71
		private static readonly string build = Assembly.GetExecutingAssembly().GetName().Version.ToString();

		// Token: 0x04000048 RID: 72
		private static readonly Dictionary<string, DateTime> periodicEventDictionary = new Dictionary<string, DateTime>();

		// Token: 0x04000049 RID: 73
		private static bool disableComplianceCrimsonEvents = false;
	}
}
