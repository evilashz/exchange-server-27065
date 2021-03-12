using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x0200001E RID: 30
	internal sealed class ResourceLog
	{
		// Token: 0x06000147 RID: 327 RVA: 0x00006448 File Offset: 0x00004648
		public ResourceLog(bool enabled, string logSource, string logDirectory, TimeSpan maxAge, TimeSpan streamFlushInterval, TimeSpan backgroundWriteInterval, long maxDirectorySize = 0L, long maxLogFileSize = 0L, int bufferSize = 0)
		{
			this.enabled = enabled;
			if (enabled)
			{
				ArgumentValidator.ThrowIfNullOrEmpty("logDirectory", logDirectory);
				ArgumentValidator.ThrowIfNullOrEmpty("logSource", logSource);
				ArgumentValidator.ThrowIfInvalidValue<TimeSpan>("maxAge", maxAge, (TimeSpan age) => age > TimeSpan.Zero);
				ArgumentValidator.ThrowIfInvalidValue<TimeSpan>("streamFlushInterval", streamFlushInterval, (TimeSpan interval) => interval > TimeSpan.Zero);
				ArgumentValidator.ThrowIfInvalidValue<TimeSpan>("backgroundWriteInterval", backgroundWriteInterval, (TimeSpan interval) => interval > TimeSpan.Zero);
				ArgumentValidator.ThrowIfInvalidValue<long>("maxDirectorySize", maxDirectorySize, (long number) => number >= 0L);
				ArgumentValidator.ThrowIfInvalidValue<long>("maxLogFileSize", maxLogFileSize, (long number) => number >= 0L);
				ArgumentValidator.ThrowIfNegative("bufferSize", bufferSize);
				this.logSource = logSource;
				this.schema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "Transport Resource Log", ResourceLog.Fields);
				this.asyncLogWrapper = AsyncLogWrapperFactory.CreateAsyncLogWrapper("ResourceLog_" + Process.GetCurrentProcess().ProcessName, new LogHeaderFormatter(this.schema), "ResourceLog");
				this.asyncLogWrapper.Configure(logDirectory, maxAge, maxDirectorySize, maxLogFileSize, bufferSize, streamFlushInterval, backgroundWriteInterval);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000065D3 File Offset: 0x000047D3
		public IAsyncLogWrapper AsyncLogWrapper
		{
			get
			{
				return this.asyncLogWrapper;
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000065DB File Offset: 0x000047DB
		public void LogResourceUsePeriodic(ResourceUse resourceUse, Dictionary<string, object> customData)
		{
			this.LogResourceUse(0, resourceUse, customData);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000065E6 File Offset: 0x000047E6
		public void LogResourceUseChange(ResourceUse resourceUse, Dictionary<string, object> customData)
		{
			this.LogResourceUse(1, resourceUse, customData);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000065F4 File Offset: 0x000047F4
		private static string[] InitializeHeaders()
		{
			string[] array = new string[Enum.GetValues(typeof(ResourceLog.Field)).Length];
			array[0] = "DateTime";
			array[1] = "EventId";
			array[2] = "LogSource";
			array[3] = "ResourceId";
			array[4] = "OldValue";
			array[5] = "NewValue";
			array[6] = "CustomData";
			return array;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006654 File Offset: 0x00004854
		private void LogResourceUse(int eventId, ResourceUse resourceUse, Dictionary<string, object> customData)
		{
			if (!this.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.schema);
			logRowFormatter[1] = eventId;
			logRowFormatter[2] = this.logSource;
			logRowFormatter[3] = resourceUse.Resource.ToString();
			logRowFormatter[4] = resourceUse.PreviousUseLevel.ToString();
			logRowFormatter[5] = resourceUse.CurrentUseLevel.ToString();
			if (customData != null)
			{
				logRowFormatter[6] = customData;
			}
			this.Append(logRowFormatter);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000066E2 File Offset: 0x000048E2
		private void Append(LogRowFormatter row)
		{
			this.asyncLogWrapper.Append(row, 0);
		}

		// Token: 0x04000087 RID: 135
		private const string LogComponentName = "ResourceLog";

		// Token: 0x04000088 RID: 136
		private const string LogFilePrefix = "ResourceLog";

		// Token: 0x04000089 RID: 137
		private static readonly string[] Fields = ResourceLog.InitializeHeaders();

		// Token: 0x0400008A RID: 138
		private readonly IAsyncLogWrapper asyncLogWrapper;

		// Token: 0x0400008B RID: 139
		private readonly LogSchema schema;

		// Token: 0x0400008C RID: 140
		private readonly bool enabled;

		// Token: 0x0400008D RID: 141
		private readonly string logSource;

		// Token: 0x0200001F RID: 31
		private enum Field
		{
			// Token: 0x04000094 RID: 148
			Time,
			// Token: 0x04000095 RID: 149
			EventId,
			// Token: 0x04000096 RID: 150
			LogSource,
			// Token: 0x04000097 RID: 151
			ResourceId,
			// Token: 0x04000098 RID: 152
			OldValue,
			// Token: 0x04000099 RID: 153
			NewValue,
			// Token: 0x0400009A RID: 154
			CustomData
		}

		// Token: 0x02000020 RID: 32
		private enum ResourceLogEvent
		{
			// Token: 0x0400009C RID: 156
			Periodic,
			// Token: 0x0400009D RID: 157
			Change
		}
	}
}
