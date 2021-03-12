using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000008 RID: 8
	internal class FlowControlLog : IFlowControlLog
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600002E RID: 46 RVA: 0x00002674 File Offset: 0x00000874
		// (remove) Token: 0x0600002F RID: 47 RVA: 0x000026AC File Offset: 0x000008AC
		public event Action<string> TrackSummary;

		// Token: 0x06000030 RID: 48 RVA: 0x000026E4 File Offset: 0x000008E4
		public void Configure(IFlowControlLogConfig config, Server serverConfig)
		{
			ArgumentValidator.ThrowIfNull("config", config);
			ArgumentValidator.ThrowIfNull("serverConfig", serverConfig);
			if (!serverConfig.FlowControlLogEnabled)
			{
				this.enabled = false;
				return;
			}
			if (serverConfig.FlowControlLogPath == null || string.IsNullOrEmpty(serverConfig.FlowControlLogPath.PathName))
			{
				this.enabled = false;
				ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Flow Control Log path was set to null, Flow Control Log is disabled");
				return;
			}
			FlowControlLog.schema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "Transport Flow Control Log", FlowControlLog.Fields);
			this.log = new AsyncLog("FlowControlLog", new LogHeaderFormatter(FlowControlLog.schema), "FlowControlLog");
			this.log.Configure(serverConfig.FlowControlLogPath.PathName, serverConfig.FlowControlLogMaxAge, (long)(serverConfig.FlowControlLogMaxDirectorySize.IsUnlimited ? 0UL : serverConfig.FlowControlLogMaxDirectorySize.Value.ToBytes()), (long)(serverConfig.FlowControlLogMaxFileSize.IsUnlimited ? 0UL : serverConfig.FlowControlLogMaxFileSize.Value.ToBytes()), config.BufferSize, config.FlushInterval, config.AsyncInterval);
			this.enabled = true;
			this.LogReset();
			this.logSummaryTimer = new GuardedTimer(new TimerCallback(this.RaiseSummaryEvent), null, config.SummaryLoggingInterval, config.SummaryLoggingInterval);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002856 File Offset: 0x00000A56
		public void Stop()
		{
			if (this.log != null)
			{
				this.log.Close();
			}
			if (this.logSummaryTimer != null)
			{
				this.logSummaryTimer.Dispose(true);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002880 File Offset: 0x00000A80
		public void LogThrottle(ThrottlingResource resource, ThrottlingAction action, int threshold, TimeSpan thresholdInterval, ThrottlingScope scope, Guid externalOrganizationId, string sender, string recipient, string subject, ThrottlingSource source, bool loggingMode, IEnumerable<KeyValuePair<string, object>> extraData)
		{
			if (!this.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(FlowControlLog.schema);
			logRowFormatter[1] = ThrottlingEvent.Throttle;
			logRowFormatter[2] = this.GetNextSequenceNumber();
			logRowFormatter[3] = resource;
			logRowFormatter[4] = action;
			logRowFormatter[5] = threshold;
			logRowFormatter[6] = thresholdInterval;
			logRowFormatter[8] = scope;
			if (externalOrganizationId != Guid.Empty)
			{
				logRowFormatter[9] = externalOrganizationId;
			}
			logRowFormatter[10] = sender;
			logRowFormatter[11] = recipient;
			logRowFormatter[12] = subject;
			logRowFormatter[13] = source;
			logRowFormatter[14] = loggingMode;
			this.LogCustomData(logRowFormatter, extraData);
			this.Append(logRowFormatter);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002968 File Offset: 0x00000B68
		public void LogUnthrottle(ThrottlingResource resource, ThrottlingAction action, int threshold, TimeSpan thresholdInterval, int impact, int observedValue, ThrottlingScope scope, Guid externalOrganizationId, string sender, string recipient, string subject, ThrottlingSource source, bool loggingMode, IEnumerable<KeyValuePair<string, object>> extraData)
		{
			if (!this.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(FlowControlLog.schema);
			logRowFormatter[1] = ThrottlingEvent.Unthrottle;
			logRowFormatter[2] = this.GetNextSequenceNumber();
			logRowFormatter[3] = resource;
			logRowFormatter[4] = action;
			logRowFormatter[5] = threshold;
			logRowFormatter[6] = thresholdInterval;
			logRowFormatter[7] = impact;
			logRowFormatter[8] = scope;
			if (externalOrganizationId != Guid.Empty)
			{
				logRowFormatter[9] = externalOrganizationId;
			}
			logRowFormatter[10] = sender;
			logRowFormatter[11] = recipient;
			logRowFormatter[12] = subject;
			logRowFormatter[13] = source;
			logRowFormatter[14] = loggingMode;
			this.LogCustomData(logRowFormatter, extraData, "observedValue", observedValue);
			this.Append(logRowFormatter);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A6C File Offset: 0x00000C6C
		public void LogSummary(string sequenceNumber, ThrottlingResource resource, ThrottlingAction action, int threshold, TimeSpan thresholdInterval, int observedValue, int impact, ThrottlingScope scope, Guid externalOrganizationId, string sender, string recipient, string subject, ThrottlingSource source, bool loggingMode, IEnumerable<KeyValuePair<string, object>> extraData)
		{
			if (!this.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(FlowControlLog.schema);
			logRowFormatter[1] = ThrottlingEvent.SummaryThrottle;
			logRowFormatter[2] = sequenceNumber;
			logRowFormatter[3] = resource;
			logRowFormatter[4] = action;
			logRowFormatter[5] = threshold;
			logRowFormatter[6] = thresholdInterval;
			logRowFormatter[7] = impact;
			logRowFormatter[8] = scope;
			if (externalOrganizationId != Guid.Empty)
			{
				logRowFormatter[9] = externalOrganizationId;
			}
			logRowFormatter[10] = sender;
			logRowFormatter[11] = recipient;
			logRowFormatter[12] = subject;
			logRowFormatter[13] = source;
			logRowFormatter[14] = loggingMode;
			this.LogCustomData(logRowFormatter, extraData, "observedValue", observedValue);
			this.Append(logRowFormatter);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002B6C File Offset: 0x00000D6C
		public void LogSummaryWarning(ThrottlingResource resource, ThrottlingAction action, int threshold, TimeSpan thresholdInterval, ThrottlingScope scope, Guid externalOrganizationId, string sender, string recipient, string subject, ThrottlingSource source, bool loggingMode, IEnumerable<KeyValuePair<string, object>> extraData)
		{
			this.LogWarningInternal(ThrottlingEvent.SummaryWarning, resource, action, threshold, thresholdInterval, scope, externalOrganizationId, sender, recipient, subject, source, loggingMode, extraData);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B98 File Offset: 0x00000D98
		public void LogWarning(ThrottlingResource resource, ThrottlingAction action, int threshold, TimeSpan thresholdInterval, ThrottlingScope scope, Guid externalOrganizationId, string sender, string recipient, string subject, ThrottlingSource source, bool loggingMode, IEnumerable<KeyValuePair<string, object>> extraData)
		{
			this.LogWarningInternal(ThrottlingEvent.Warning, resource, action, threshold, thresholdInterval, scope, externalOrganizationId, sender, recipient, subject, source, loggingMode, extraData);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public void LogMaxLinesExceeded(string sequenceNumber, ThrottlingSource source, int threshold, int observedValue, IEnumerable<KeyValuePair<string, object>> extraData)
		{
			if (!this.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(FlowControlLog.schema);
			logRowFormatter[1] = ThrottlingEvent.SummaryThrottle;
			logRowFormatter[2] = sequenceNumber;
			logRowFormatter[3] = ThrottlingResource.MaxLinesReached;
			logRowFormatter[5] = threshold;
			this.LogCustomData(logRowFormatter, extraData, "observedValue", observedValue);
			this.Append(logRowFormatter);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002C30 File Offset: 0x00000E30
		private static string[] InitializeHeaders()
		{
			string[] array = new string[Enum.GetValues(typeof(FlowControlLog.Field)).Length];
			array[0] = "dateTime";
			array[1] = "eventId";
			array[2] = "sequenceNumber";
			array[3] = "resource";
			array[4] = "action";
			array[5] = "threshold";
			array[6] = "thresholdInterval";
			array[7] = "impact";
			array[9] = "organizationId";
			array[8] = "scope";
			array[10] = "sender";
			array[11] = "recipient";
			array[12] = "subject";
			array[13] = "source";
			array[14] = "loggingMode";
			array[15] = "customData";
			return array;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002CE0 File Offset: 0x00000EE0
		private void LogWarningInternal(ThrottlingEvent warningEventType, ThrottlingResource resource, ThrottlingAction action, int threshold, TimeSpan thresholdInterval, ThrottlingScope scope, Guid externalOrganizationId, string sender, string recipient, string subject, ThrottlingSource source, bool loggingMode, IEnumerable<KeyValuePair<string, object>> extraData)
		{
			if (!this.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(FlowControlLog.schema);
			logRowFormatter[1] = warningEventType;
			logRowFormatter[2] = this.GetNextSequenceNumber();
			logRowFormatter[3] = resource;
			logRowFormatter[4] = action;
			logRowFormatter[5] = threshold;
			logRowFormatter[6] = thresholdInterval;
			logRowFormatter[8] = scope;
			if (externalOrganizationId != Guid.Empty)
			{
				logRowFormatter[9] = externalOrganizationId;
			}
			logRowFormatter[10] = sender;
			logRowFormatter[11] = recipient;
			logRowFormatter[12] = subject;
			logRowFormatter[13] = source;
			logRowFormatter[14] = loggingMode;
			this.LogCustomData(logRowFormatter, extraData);
			this.Append(logRowFormatter);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002DC8 File Offset: 0x00000FC8
		private void LogCustomData(LogRowFormatter row, IEnumerable<KeyValuePair<string, object>> extraData, string property, object value)
		{
			List<KeyValuePair<string, object>> first = new List<KeyValuePair<string, object>>(1)
			{
				new KeyValuePair<string, object>(property, value)
			};
			IEnumerable<KeyValuePair<string, object>> enumerable = first.Concat(this.defaultProperties);
			if (extraData != null)
			{
				enumerable = extraData.Concat(enumerable);
			}
			row[15] = enumerable;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002E10 File Offset: 0x00001010
		private void LogCustomData(LogRowFormatter row, IEnumerable<KeyValuePair<string, object>> extraData)
		{
			IEnumerable<KeyValuePair<string, object>> value = this.defaultProperties;
			if (extraData != null)
			{
				value = extraData.Concat(this.defaultProperties);
			}
			row[15] = value;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002E40 File Offset: 0x00001040
		private void LogReset()
		{
			if (!this.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(FlowControlLog.schema);
			logRowFormatter[1] = ThrottlingEvent.Reset;
			this.Append(logRowFormatter);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002E78 File Offset: 0x00001078
		private void Append(LogRowFormatter row)
		{
			try
			{
				this.log.Append(row, 0);
			}
			catch (ObjectDisposedException)
			{
				ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Appending to queue quota log failed with ObjectDisposedException");
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002EB8 File Offset: 0x000010B8
		private void RaiseSummaryEvent(object state)
		{
			if (this.TrackSummary != null)
			{
				this.TrackSummary(this.GetNextSequenceNumber());
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002ED3 File Offset: 0x000010D3
		private string GetNextSequenceNumber()
		{
			this.sequenceNumber += 1L;
			return this.sequenceNumber.ToString("X16", NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x04000008 RID: 8
		internal const string ObservedValue = "observedValue";

		// Token: 0x04000009 RID: 9
		internal const string ThrottledDuration = "throttledDuration";

		// Token: 0x0400000A RID: 10
		internal const string ScopeValue = "ScopeValue";

		// Token: 0x0400000B RID: 11
		private const string LogComponentName = "FlowControlLog";

		// Token: 0x0400000C RID: 12
		private const string LogFileName = "FlowControlLog";

		// Token: 0x0400000D RID: 13
		private const string AssemblyVersionName = "Version";

		// Token: 0x0400000E RID: 14
		private static readonly string[] Fields = FlowControlLog.InitializeHeaders();

		// Token: 0x0400000F RID: 15
		private static LogSchema schema;

		// Token: 0x04000010 RID: 16
		private AsyncLog log;

		// Token: 0x04000011 RID: 17
		private bool enabled;

		// Token: 0x04000012 RID: 18
		private long sequenceNumber = DateTime.UtcNow.Ticks;

		// Token: 0x04000013 RID: 19
		private GuardedTimer logSummaryTimer;

		// Token: 0x04000014 RID: 20
		private List<KeyValuePair<string, object>> defaultProperties = new List<KeyValuePair<string, object>>
		{
			new KeyValuePair<string, object>("Version", ExWatson.ApplicationVersion.ToString())
		};

		// Token: 0x02000009 RID: 9
		private enum Field
		{
			// Token: 0x04000017 RID: 23
			Time,
			// Token: 0x04000018 RID: 24
			EventId,
			// Token: 0x04000019 RID: 25
			SequenceNumber,
			// Token: 0x0400001A RID: 26
			Resource,
			// Token: 0x0400001B RID: 27
			Action,
			// Token: 0x0400001C RID: 28
			Threshold,
			// Token: 0x0400001D RID: 29
			ThresholdInterval,
			// Token: 0x0400001E RID: 30
			Impact,
			// Token: 0x0400001F RID: 31
			Scope,
			// Token: 0x04000020 RID: 32
			OrganizationId,
			// Token: 0x04000021 RID: 33
			Sender,
			// Token: 0x04000022 RID: 34
			Recipient,
			// Token: 0x04000023 RID: 35
			Subject,
			// Token: 0x04000024 RID: 36
			Source,
			// Token: 0x04000025 RID: 37
			LoggingMode,
			// Token: 0x04000026 RID: 38
			CustomData
		}
	}
}
