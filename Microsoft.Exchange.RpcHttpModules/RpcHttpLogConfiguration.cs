using System;
using System.IO;
using System.Web.Hosting;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcHttpModules
{
	// Token: 0x02000011 RID: 17
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class RpcHttpLogConfiguration : ILogConfiguration
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002F64 File Offset: 0x00001164
		public RpcHttpLogConfiguration()
		{
			this.IsLoggingEnabled = true;
			this.LogPath = RpcHttpLogConfiguration.DefaultLogPath;
			this.MaxLogAge = TimeSpan.FromDays(30.0);
			this.MaxLogDirectorySizeInBytes = 1073741824L;
			this.MaxLogFileSizeInBytes = 10485760L;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002FB8 File Offset: 0x000011B8
		public static string DefaultLogPath
		{
			get
			{
				string path = string.Format("{0}\\W3SVC{1}", "Logging\\RpcHttp", HostingEnvironment.ApplicationHost.GetSiteID());
				return RpcHttpLogConfiguration.defaultLogPath = Path.Combine(ExchangeSetupContext.InstallPath, path);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002FF0 File Offset: 0x000011F0
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002FF8 File Offset: 0x000011F8
		public bool IsLoggingEnabled { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003001 File Offset: 0x00001201
		public bool IsActivityEventHandler
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003004 File Offset: 0x00001204
		// (set) Token: 0x06000054 RID: 84 RVA: 0x0000300C File Offset: 0x0000120C
		public string LogPath { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003015 File Offset: 0x00001215
		// (set) Token: 0x06000056 RID: 86 RVA: 0x0000301D File Offset: 0x0000121D
		public TimeSpan MaxLogAge { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003026 File Offset: 0x00001226
		// (set) Token: 0x06000058 RID: 88 RVA: 0x0000302E File Offset: 0x0000122E
		public long MaxLogDirectorySizeInBytes { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003037 File Offset: 0x00001237
		// (set) Token: 0x0600005A RID: 90 RVA: 0x0000303F File Offset: 0x0000123F
		public long MaxLogFileSizeInBytes { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003048 File Offset: 0x00001248
		public string LogComponent
		{
			get
			{
				return "RpcHttpLog";
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000304F File Offset: 0x0000124F
		public string LogPrefix
		{
			get
			{
				return RpcHttpLogConfiguration.LogPrefixValue;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003056 File Offset: 0x00001256
		public string LogType
		{
			get
			{
				return "RpcHttp Log";
			}
		}

		// Token: 0x04000021 RID: 33
		private const string LogTypeValue = "RpcHttp Log";

		// Token: 0x04000022 RID: 34
		private const string LogComponentValue = "RpcHttpLog";

		// Token: 0x04000023 RID: 35
		private const string DefaultRelativeFilePath = "Logging\\RpcHttp";

		// Token: 0x04000024 RID: 36
		public static readonly string LogPrefixValue = "RpcHttp";

		// Token: 0x04000025 RID: 37
		private static string defaultLogPath;
	}
}
