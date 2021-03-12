using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.WorkingSet;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage.WorkingSet.Publisher
{
	// Token: 0x02000EED RID: 3821
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class WorkingSetPublisherLogConfiguration : ILogConfiguration
	{
		// Token: 0x060083DC RID: 33756 RVA: 0x0023CC88 File Offset: 0x0023AE88
		private WorkingSetPublisherLogConfiguration()
		{
			this.prefix = "WorkingSetPublisher_" + ApplicationName.Current.UniqueId + "_";
			this.directoryPath = (WorkingSetPublisherLogConfiguration.DirectoryPath.Value ?? Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\WorkingSetPublisher\\"));
		}

		// Token: 0x170022FD RID: 8957
		// (get) Token: 0x060083DD RID: 33757 RVA: 0x0023CCDD File Offset: 0x0023AEDD
		public static WorkingSetPublisherLogConfiguration Default
		{
			get
			{
				if (WorkingSetPublisherLogConfiguration.defaultInstance == null)
				{
					WorkingSetPublisherLogConfiguration.defaultInstance = new WorkingSetPublisherLogConfiguration();
				}
				return WorkingSetPublisherLogConfiguration.defaultInstance;
			}
		}

		// Token: 0x170022FE RID: 8958
		// (get) Token: 0x060083DE RID: 33758 RVA: 0x0023CCF5 File Offset: 0x0023AEF5
		public bool IsLoggingEnabled
		{
			get
			{
				return WorkingSetPublisherLogConfiguration.Enabled.Value;
			}
		}

		// Token: 0x170022FF RID: 8959
		// (get) Token: 0x060083DF RID: 33759 RVA: 0x0023CD01 File Offset: 0x0023AF01
		public bool IsActivityEventHandler
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002300 RID: 8960
		// (get) Token: 0x060083E0 RID: 33760 RVA: 0x0023CD04 File Offset: 0x0023AF04
		public string LogPath
		{
			get
			{
				return this.directoryPath;
			}
		}

		// Token: 0x17002301 RID: 8961
		// (get) Token: 0x060083E1 RID: 33761 RVA: 0x0023CD0C File Offset: 0x0023AF0C
		public string LogPrefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x17002302 RID: 8962
		// (get) Token: 0x060083E2 RID: 33762 RVA: 0x0023CD14 File Offset: 0x0023AF14
		public string LogComponent
		{
			get
			{
				return "WorkingSetPublisherLog";
			}
		}

		// Token: 0x17002303 RID: 8963
		// (get) Token: 0x060083E3 RID: 33763 RVA: 0x0023CD1B File Offset: 0x0023AF1B
		public string LogType
		{
			get
			{
				return "Working Set Publisher Log";
			}
		}

		// Token: 0x17002304 RID: 8964
		// (get) Token: 0x060083E4 RID: 33764 RVA: 0x0023CD24 File Offset: 0x0023AF24
		public long MaxLogDirectorySizeInBytes
		{
			get
			{
				return (long)WorkingSetPublisherLogConfiguration.MaxDirectorySize.Value.ToBytes();
			}
		}

		// Token: 0x17002305 RID: 8965
		// (get) Token: 0x060083E5 RID: 33765 RVA: 0x0023CD44 File Offset: 0x0023AF44
		public long MaxLogFileSizeInBytes
		{
			get
			{
				return (long)WorkingSetPublisherLogConfiguration.MaxFileSize.Value.ToBytes();
			}
		}

		// Token: 0x17002306 RID: 8966
		// (get) Token: 0x060083E6 RID: 33766 RVA: 0x0023CD63 File Offset: 0x0023AF63
		public TimeSpan MaxLogAge
		{
			get
			{
				return WorkingSetPublisherLogConfiguration.MaxAge.Value;
			}
		}

		// Token: 0x0400581E RID: 22558
		private const string Type = "Working Set Publisher Log";

		// Token: 0x0400581F RID: 22559
		private const string Component = "WorkingSetPublisherLog";

		// Token: 0x04005820 RID: 22560
		private static readonly Trace Tracer = ExTraceGlobals.WorkingSetPublisherTracer;

		// Token: 0x04005821 RID: 22561
		private static readonly BoolAppSettingsEntry Enabled = new BoolAppSettingsEntry("WorkingSetPublisherLogEnabled", true, WorkingSetPublisherLogConfiguration.Tracer);

		// Token: 0x04005822 RID: 22562
		private static readonly StringAppSettingsEntry DirectoryPath = new StringAppSettingsEntry("WorkingSetPublisherLogPath", null, WorkingSetPublisherLogConfiguration.Tracer);

		// Token: 0x04005823 RID: 22563
		private static readonly TimeSpanAppSettingsEntry MaxAge = new TimeSpanAppSettingsEntry("WorkingSetPublisherLogMaxAge", TimeSpanUnit.Minutes, TimeSpan.FromDays(7.0), WorkingSetPublisherLogConfiguration.Tracer);

		// Token: 0x04005824 RID: 22564
		private static readonly ByteQuantifiedSizeAppSettingsEntry MaxDirectorySize = new ByteQuantifiedSizeAppSettingsEntry("WorkingSetPublisherLogMaxDirectorySize", ByteQuantifiedSize.FromMB(250UL), WorkingSetPublisherLogConfiguration.Tracer);

		// Token: 0x04005825 RID: 22565
		private static readonly ByteQuantifiedSizeAppSettingsEntry MaxFileSize = new ByteQuantifiedSizeAppSettingsEntry("WorkingSetPublisherLogMaxFileSize", ByteQuantifiedSize.FromMB(10UL), WorkingSetPublisherLogConfiguration.Tracer);

		// Token: 0x04005826 RID: 22566
		private static WorkingSetPublisherLogConfiguration defaultInstance;

		// Token: 0x04005827 RID: 22567
		private readonly string prefix;

		// Token: 0x04005828 RID: 22568
		private readonly string directoryPath;
	}
}
