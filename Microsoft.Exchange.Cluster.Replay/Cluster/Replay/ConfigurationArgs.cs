using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200029A RID: 666
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConfigurationArgs
	{
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060019F4 RID: 6644 RVA: 0x0006C71B File Offset: 0x0006A91B
		// (set) Token: 0x060019F5 RID: 6645 RVA: 0x0006C723 File Offset: 0x0006A923
		public bool IsTestEnvironment { get; private set; }

		// Token: 0x060019F6 RID: 6646 RVA: 0x0006C72C File Offset: 0x0006A92C
		public ConfigurationArgs(IReplayConfiguration config, IReplicaInstanceManager replicaInstanceManager) : this(config, replicaInstanceManager, false)
		{
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x0006C738 File Offset: 0x0006A938
		internal ConfigurationArgs(IReplayConfiguration config, IReplicaInstanceManager replicaInstanceManager, bool isTestCode)
		{
			this.IsTestEnvironment = isTestCode;
			this.m_replicaInstanceManager = replicaInstanceManager;
			this.m_identityGuid = config.IdentityGuid;
			this.m_name = config.Name;
			this.m_logFilePrefix = config.LogFilePrefix;
			this.m_destinationSystemPath = config.DestinationSystemPath;
			this.m_destinationEdbPath = config.DestinationEdbPath;
			this.m_destinationLogPath = config.DestinationLogPath;
			this.m_inspectorLogPath = config.LogInspectorPath;
			this.m_ignoredLogsPath = config.E00LogBackupPath;
			this.m_sourceMachine = config.SourceMachine;
			this.m_LogFileSuffix = config.LogFileSuffix;
			this.m_autoDagVolumesRootFolderPath = config.AutoDagVolumesRootFolderPath;
			this.m_autoDagDatabasesRootFolderPath = config.AutoDagDatabasesRootFolderPath;
			this.m_autoDagDatabaseCopiesPerVolume = config.AutoDagDatabaseCopiesPerVolume;
			this.CircularLoggingEnabled = config.CircularLoggingEnabled;
			this.BuildDebugString();
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x0006C807 File Offset: 0x0006AA07
		public Guid IdentityGuid
		{
			get
			{
				return this.m_identityGuid;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x0006C80F File Offset: 0x0006AA0F
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x0006C817 File Offset: 0x0006AA17
		public string SourceEdbPath
		{
			get
			{
				return this.m_destinationEdbPath;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x0006C81F File Offset: 0x0006AA1F
		public string SourceMachine
		{
			get
			{
				return this.m_sourceMachine;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x0006C827 File Offset: 0x0006AA27
		public string LogFilePrefix
		{
			get
			{
				return this.m_logFilePrefix;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x0006C82F File Offset: 0x0006AA2F
		public string LogFileSuffix
		{
			get
			{
				return this.m_LogFileSuffix;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x0006C837 File Offset: 0x0006AA37
		public string DestinationSystemPath
		{
			get
			{
				return this.m_destinationSystemPath;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x0006C83F File Offset: 0x0006AA3F
		public string DestinationEdbPath
		{
			get
			{
				return this.m_destinationEdbPath;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x0006C847 File Offset: 0x0006AA47
		public string DestinationLogPath
		{
			get
			{
				return this.m_destinationLogPath;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x0006C84F File Offset: 0x0006AA4F
		public string InspectorLogPath
		{
			get
			{
				return this.m_inspectorLogPath;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x0006C857 File Offset: 0x0006AA57
		public string IgnoredLogsPath
		{
			get
			{
				return this.m_ignoredLogsPath;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x0006C85F File Offset: 0x0006AA5F
		public string AutoDagVolumesRootFolderPath
		{
			get
			{
				return this.m_autoDagVolumesRootFolderPath;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001A04 RID: 6660 RVA: 0x0006C867 File Offset: 0x0006AA67
		public string AutoDagDatabasesRootFolderPath
		{
			get
			{
				return this.m_autoDagDatabasesRootFolderPath;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x0006C86F File Offset: 0x0006AA6F
		public int AutoDagDatabaseCopiesPerVolume
		{
			get
			{
				return this.m_autoDagDatabaseCopiesPerVolume;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x0006C877 File Offset: 0x0006AA77
		public IReplicaInstanceManager ReplicaInstanceManager
		{
			get
			{
				return this.m_replicaInstanceManager;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x0006C87F File Offset: 0x0006AA7F
		// (set) Token: 0x06001A08 RID: 6664 RVA: 0x0006C887 File Offset: 0x0006AA87
		internal bool CircularLoggingEnabled { get; private set; }

		// Token: 0x06001A09 RID: 6665 RVA: 0x0006C890 File Offset: 0x0006AA90
		public override string ToString()
		{
			return this.m_debugString;
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x0006C898 File Offset: 0x0006AA98
		private void BuildDebugString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("ConfigurationArgs: IdentityGuid='{0}',", this.m_identityGuid.ToString());
			stringBuilder.AppendFormat("Name='{0}',", this.m_name);
			stringBuilder.AppendFormat("CircularLoggingEnabled='{0}',", this.CircularLoggingEnabled);
			stringBuilder.AppendFormat("SourceMachine='{0}',", this.m_sourceMachine);
			stringBuilder.AppendFormat("LogFilePrefix='{0}',", this.m_logFilePrefix);
			stringBuilder.AppendFormat("LogFileSuffix='{0}',", this.m_LogFileSuffix);
			stringBuilder.AppendFormat("DestinationSystemPath='{0}',", this.m_destinationSystemPath);
			stringBuilder.AppendFormat("DestinationEdbPath='{0}',", this.m_destinationEdbPath);
			stringBuilder.AppendFormat("DestinationLogPath='{0}',", this.m_destinationLogPath);
			stringBuilder.AppendFormat("InspectorLogPath='{0}',", this.m_inspectorLogPath);
			stringBuilder.AppendFormat("IgnoredLogsPath='{0}',", this.m_ignoredLogsPath);
			stringBuilder.AppendFormat("AutoDagVolumesRootFolderPath='{0}',", this.m_autoDagVolumesRootFolderPath);
			stringBuilder.AppendFormat("AutoDagDatabasesRootFolderPath='{0}',", this.m_autoDagDatabasesRootFolderPath);
			stringBuilder.AppendFormat("AutoDagDatabaseCopiesPerVolume='{0}',", this.m_autoDagDatabaseCopiesPerVolume);
			this.m_debugString = stringBuilder.ToString();
		}

		// Token: 0x04000A64 RID: 2660
		private readonly Guid m_identityGuid;

		// Token: 0x04000A65 RID: 2661
		private readonly string m_name;

		// Token: 0x04000A66 RID: 2662
		private readonly string m_logFilePrefix;

		// Token: 0x04000A67 RID: 2663
		private readonly string m_destinationSystemPath;

		// Token: 0x04000A68 RID: 2664
		private readonly string m_destinationEdbPath;

		// Token: 0x04000A69 RID: 2665
		private readonly string m_destinationLogPath;

		// Token: 0x04000A6A RID: 2666
		private readonly string m_ignoredLogsPath;

		// Token: 0x04000A6B RID: 2667
		private readonly string m_inspectorLogPath;

		// Token: 0x04000A6C RID: 2668
		private readonly string m_sourceMachine;

		// Token: 0x04000A6D RID: 2669
		private readonly string m_LogFileSuffix;

		// Token: 0x04000A6E RID: 2670
		private readonly string m_autoDagVolumesRootFolderPath;

		// Token: 0x04000A6F RID: 2671
		private readonly string m_autoDagDatabasesRootFolderPath;

		// Token: 0x04000A70 RID: 2672
		private readonly int m_autoDagDatabaseCopiesPerVolume;

		// Token: 0x04000A71 RID: 2673
		private readonly IReplicaInstanceManager m_replicaInstanceManager;

		// Token: 0x04000A72 RID: 2674
		private string m_debugString;
	}
}
