using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Compliance.CrimsonEvents;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Logging
{
	// Token: 0x0200001E RID: 30
	internal class LogItem : EventNotificationItem
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x0000E9F3 File Offset: 0x0000CBF3
		public LogItem(string serviceName, string component, string notificationReason, ResultSeverityLevel severity = ResultSeverityLevel.Error) : base(serviceName, component, notificationReason, notificationReason, severity)
		{
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000EA01 File Offset: 0x0000CC01
		public LogItem(string serviceName, string component, string notificationReason, string message, ResultSeverityLevel severity = ResultSeverityLevel.Error) : base(serviceName, component, notificationReason, message, severity)
		{
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000EA10 File Offset: 0x0000CC10
		public LogItem(string serviceName, string component, string notificationReason, string message, string stateAttribute1, ResultSeverityLevel severity = ResultSeverityLevel.Error) : base(serviceName, component, notificationReason, message, stateAttribute1, severity)
		{
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000EA24 File Offset: 0x0000CC24
		public new static void Publish(string serviceName, string component, string tag, string notificationReason, ResultSeverityLevel severity = ResultSeverityLevel.Error, bool throwOnError = false)
		{
			if (string.IsNullOrEmpty(serviceName) || string.IsNullOrEmpty(component))
			{
				throw new ArgumentException("serviceName and component must have non-null/non-empty values.");
			}
			LogItem logItem = new LogItem(serviceName, component, tag, notificationReason, severity);
			logItem.Publish(throwOnError);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000EA60 File Offset: 0x0000CC60
		public new static void Publish(string serviceName, string component, string tag, string notificationReason, string stateAttribute1, ResultSeverityLevel severity = ResultSeverityLevel.Error, bool throwOnError = false)
		{
			if (string.IsNullOrEmpty(serviceName) || string.IsNullOrEmpty(component))
			{
				throw new ArgumentException("serviceName and component must have non-null/non-empty values.");
			}
			LogItem logItem = new LogItem(serviceName, component, tag, notificationReason, stateAttribute1, severity);
			logItem.Publish(throwOnError);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000EA9E File Offset: 0x0000CC9E
		public new static void PublishPeriodic(string serviceName, string component, string tag, string notificationReason, string periodicKey, TimeSpan period, ResultSeverityLevel severity = ResultSeverityLevel.Error, bool throwOnError = false)
		{
			if (string.IsNullOrEmpty(periodicKey))
			{
				throw new ArgumentException("periodicKey must have non-null/non-empty value.");
			}
			if (LogItem.CanPublishPeriodic(periodicKey, period))
			{
				LogItem.Publish(serviceName, component, tag, notificationReason, severity, throwOnError);
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000EACC File Offset: 0x0000CCCC
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

		// Token: 0x060001BD RID: 445 RVA: 0x0000ED08 File Offset: 0x0000CF08
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

		// Token: 0x040000E0 RID: 224
		private static readonly string build = Assembly.GetExecutingAssembly().GetName().Version.ToString();

		// Token: 0x040000E1 RID: 225
		private static readonly Dictionary<string, DateTime> periodicEventDictionary = new Dictionary<string, DateTime>();
	}
}
