using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Exchange.Rpc.Cluster
{
	// Token: 0x02000132 RID: 306
	internal sealed class RpcSeederArgs
	{
		// Token: 0x06000871 RID: 2161 RVA: 0x00007878 File Offset: 0x00006C78
		private void BuildDebugString()
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.AppendFormat("RpcSeederArgs: [ InstanceGuid='{0}', ", this.m_instanceGuid.ToString());
			bool fDeleteExistingFiles = this.m_fDeleteExistingFiles;
			stringBuilder.AppendFormat("DeleteExistingFiles='{0}', ", fDeleteExistingFiles.ToString());
			bool fAutoSuspend = this.m_fAutoSuspend;
			stringBuilder.AppendFormat("AutoSuspend='{0}', ", fAutoSuspend.ToString());
			stringBuilder.AppendFormat("SeedingPath='{0}', ", this.m_seedingPath);
			stringBuilder.AppendFormat("LogFolderPath='{0}', ", this.m_logFolderPath);
			stringBuilder.AppendFormat("NetworkId='{0}', ", this.m_networkId);
			bool fStreamingBackup = this.m_fStreamingBackup;
			stringBuilder.AppendFormat("StreamingBackup='{0}', ", fStreamingBackup.ToString());
			stringBuilder.AppendFormat("SourceMachineName='{0}', ", this.m_sourceMachineName);
			stringBuilder.AppendFormat("DatabaseName='{0}', ", this.m_databaseName);
			stringBuilder.AppendFormat("ManualResume='{0}', ", this.m_fManualResume);
			string arg;
			if (this.m_fSeedDatabase)
			{
				arg = "1";
			}
			else
			{
				arg = "0";
			}
			stringBuilder.AppendFormat("SeedDatabase='{0}', ", arg);
			string arg2;
			if (this.m_fSeedCiFiles)
			{
				arg2 = "1";
			}
			else
			{
				arg2 = "0";
			}
			stringBuilder.AppendFormat("SeedCiFiles='{0}', ", arg2);
			stringBuilder.AppendFormat("MaxSeedsInParallel='{0}', ", this.m_maxSeedsInParallel);
			stringBuilder.AppendFormat("SafeDeleteExistingFiles='{0}', ", this.m_fSafeDeleteExistingFiles);
			stringBuilder.AppendFormat("Flags='{0}', ", this.m_flags);
			stringBuilder.AppendFormat("CompressOverride='{0}'='{1}', ", this.m_compressOverride, <Module>.NullableBoolToString(ref this.m_compressOverride));
			stringBuilder.AppendFormat("EncryptOverride='{0}='{1}' ", this.m_encryptOverride, <Module>.NullableBoolToString(ref this.m_encryptOverride));
			stringBuilder.Append("]");
			this.m_debugString = stringBuilder.ToString();
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00007A5C File Offset: 0x00006E5C
		public RpcSeederArgs(Guid instanceGuid, [MarshalAs(UnmanagedType.U1)] bool fDeleteExistingFiles, [MarshalAs(UnmanagedType.U1)] bool fAutoSuspend, string seedingPath, string logFolderPath, string networkId, [MarshalAs(UnmanagedType.U1)] bool fStreamingBackup, string sourceMachinename, string databaseName, [MarshalAs(UnmanagedType.U1)] bool fManualResume, [MarshalAs(UnmanagedType.U1)] bool fSeedDatabase, [MarshalAs(UnmanagedType.U1)] bool fSeedCiFiles, bool? compressOverride, bool? encryptOverride, int maxSeedsInParallel, [MarshalAs(UnmanagedType.U1)] bool fsafeDeleteExistingFiles, SeederRpcFlags flags)
		{
			this.BuildDebugString();
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00007AFC File Offset: 0x00006EFC
		public sealed override string ToString()
		{
			return this.m_debugString;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x00007B10 File Offset: 0x00006F10
		public Guid InstanceGuid
		{
			get
			{
				return this.m_instanceGuid;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x00007B28 File Offset: 0x00006F28
		public bool DeleteExistingFiles
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_fDeleteExistingFiles;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x00007B3C File Offset: 0x00006F3C
		public bool AutoSuspend
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_fAutoSuspend;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x00007B50 File Offset: 0x00006F50
		public string SeedingPath
		{
			get
			{
				return this.m_seedingPath;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x00007B64 File Offset: 0x00006F64
		public string LogFolderPath
		{
			get
			{
				return this.m_logFolderPath;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x00007B78 File Offset: 0x00006F78
		public string NetworkId
		{
			get
			{
				return this.m_networkId;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x00007B8C File Offset: 0x00006F8C
		public bool StreamingBackup
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_fStreamingBackup;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x00007BA0 File Offset: 0x00006FA0
		public string SourceMachineName
		{
			get
			{
				return this.m_sourceMachineName;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x00007BB4 File Offset: 0x00006FB4
		public string DatabaseName
		{
			get
			{
				return this.m_databaseName;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x00007BC8 File Offset: 0x00006FC8
		public bool ManualResume
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_fManualResume;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x00007BDC File Offset: 0x00006FDC
		public bool SeedDatabase
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_fSeedDatabase;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x00007BF0 File Offset: 0x00006FF0
		public bool SeedCiFiles
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_fSeedCiFiles;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x00007C04 File Offset: 0x00007004
		public bool? CompressOverride
		{
			get
			{
				return this.m_compressOverride;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x00007C1C File Offset: 0x0000701C
		public bool? EncryptOverride
		{
			get
			{
				return this.m_encryptOverride;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x00007C34 File Offset: 0x00007034
		public int MaxSeedsInParallel
		{
			get
			{
				return this.m_maxSeedsInParallel;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x00007C48 File Offset: 0x00007048
		public bool SafeDeleteExistingFiles
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_fSafeDeleteExistingFiles;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x00007C5C File Offset: 0x0000705C
		public SeederRpcFlags Flags
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x04000A59 RID: 2649
		private Guid m_instanceGuid = instanceGuid;

		// Token: 0x04000A5A RID: 2650
		private bool m_fDeleteExistingFiles = fDeleteExistingFiles;

		// Token: 0x04000A5B RID: 2651
		private bool m_fAutoSuspend = fAutoSuspend;

		// Token: 0x04000A5C RID: 2652
		private string m_seedingPath = seedingPath;

		// Token: 0x04000A5D RID: 2653
		private string m_logFolderPath = logFolderPath;

		// Token: 0x04000A5E RID: 2654
		private string m_networkId = networkId;

		// Token: 0x04000A5F RID: 2655
		private bool m_fStreamingBackup = fStreamingBackup;

		// Token: 0x04000A60 RID: 2656
		private string m_sourceMachineName = sourceMachinename;

		// Token: 0x04000A61 RID: 2657
		private string m_databaseName = databaseName;

		// Token: 0x04000A62 RID: 2658
		private bool m_fManualResume = fManualResume;

		// Token: 0x04000A63 RID: 2659
		private bool m_fSeedDatabase = fSeedDatabase;

		// Token: 0x04000A64 RID: 2660
		private bool m_fSeedCiFiles = fSeedCiFiles;

		// Token: 0x04000A65 RID: 2661
		private bool? m_compressOverride = compressOverride;

		// Token: 0x04000A66 RID: 2662
		private bool? m_encryptOverride = encryptOverride;

		// Token: 0x04000A67 RID: 2663
		private string m_debugString;

		// Token: 0x04000A68 RID: 2664
		private int m_maxSeedsInParallel = maxSeedsInParallel;

		// Token: 0x04000A69 RID: 2665
		private bool m_fSafeDeleteExistingFiles = fsafeDeleteExistingFiles;

		// Token: 0x04000A6A RID: 2666
		private SeederRpcFlags m_flags = flags;
	}
}
