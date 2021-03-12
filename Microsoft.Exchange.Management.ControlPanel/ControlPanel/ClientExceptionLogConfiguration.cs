using System;
using System.IO;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001AE RID: 430
	internal sealed class ClientExceptionLogConfiguration : ILogConfiguration
	{
		// Token: 0x060023A3 RID: 9123 RVA: 0x0006D37C File Offset: 0x0006B57C
		public ClientExceptionLogConfiguration()
		{
			this.IsLoggingEnabled = AppConfigLoader.GetConfigBoolValue("IsClientExceptionLoggingEnabled", true);
			this.LogPath = AppConfigLoader.GetConfigStringValue("ClientExceptionLogPath", ClientExceptionLogConfiguration.DefaultLogPath);
			this.MaxLogAge = AppConfigLoader.GetConfigTimeSpanValue("ClientExceptionMaxLogAge", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromDays(30.0));
			this.MaxLogDirectorySizeInBytes = (long)ClientLogConfiguration.GetConfigByteQuantifiedSizeValue("ClientExceptionMaxLogDirectorySize", ByteQuantifiedSize.FromGB(1UL)).ToBytes();
			this.MaxLogFileSizeInBytes = (long)ClientLogConfiguration.GetConfigByteQuantifiedSizeValue("ClientExceptionMaxLogFileSize", ByteQuantifiedSize.FromMB(10UL)).ToBytes();
		}

		// Token: 0x17001ADD RID: 6877
		// (get) Token: 0x060023A4 RID: 9124 RVA: 0x0006D41C File Offset: 0x0006B61C
		public static string DefaultLogPath
		{
			get
			{
				if (ClientExceptionLogConfiguration.defaultLogPath == null)
				{
					ClientExceptionLogConfiguration.defaultLogPath = Path.Combine(ConfigurationContext.Setup.InstallPath, "Logging\\ECP\\ClientException");
				}
				return ClientExceptionLogConfiguration.defaultLogPath;
			}
		}

		// Token: 0x17001ADE RID: 6878
		// (get) Token: 0x060023A5 RID: 9125 RVA: 0x0006D43E File Offset: 0x0006B63E
		// (set) Token: 0x060023A6 RID: 9126 RVA: 0x0006D446 File Offset: 0x0006B646
		public bool IsLoggingEnabled { get; private set; }

		// Token: 0x17001ADF RID: 6879
		// (get) Token: 0x060023A7 RID: 9127 RVA: 0x0006D44F File Offset: 0x0006B64F
		public bool IsActivityEventHandler
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AE0 RID: 6880
		// (get) Token: 0x060023A8 RID: 9128 RVA: 0x0006D452 File Offset: 0x0006B652
		// (set) Token: 0x060023A9 RID: 9129 RVA: 0x0006D45A File Offset: 0x0006B65A
		public string LogPath { get; private set; }

		// Token: 0x17001AE1 RID: 6881
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x0006D463 File Offset: 0x0006B663
		// (set) Token: 0x060023AB RID: 9131 RVA: 0x0006D46B File Offset: 0x0006B66B
		public TimeSpan MaxLogAge { get; private set; }

		// Token: 0x17001AE2 RID: 6882
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x0006D474 File Offset: 0x0006B674
		// (set) Token: 0x060023AD RID: 9133 RVA: 0x0006D47C File Offset: 0x0006B67C
		public long MaxLogDirectorySizeInBytes { get; private set; }

		// Token: 0x17001AE3 RID: 6883
		// (get) Token: 0x060023AE RID: 9134 RVA: 0x0006D485 File Offset: 0x0006B685
		// (set) Token: 0x060023AF RID: 9135 RVA: 0x0006D48D File Offset: 0x0006B68D
		public long MaxLogFileSizeInBytes { get; private set; }

		// Token: 0x17001AE4 RID: 6884
		// (get) Token: 0x060023B0 RID: 9136 RVA: 0x0006D496 File Offset: 0x0006B696
		public string LogComponent
		{
			get
			{
				return "ECPClientExceptionLog";
			}
		}

		// Token: 0x17001AE5 RID: 6885
		// (get) Token: 0x060023B1 RID: 9137 RVA: 0x0006D49D File Offset: 0x0006B69D
		public string LogPrefix
		{
			get
			{
				return ClientExceptionLogConfiguration.LogPrefixValue;
			}
		}

		// Token: 0x17001AE6 RID: 6886
		// (get) Token: 0x060023B2 RID: 9138 RVA: 0x0006D4A4 File Offset: 0x0006B6A4
		public string LogType
		{
			get
			{
				return "ECP Client Exception Log";
			}
		}

		// Token: 0x04001E19 RID: 7705
		private const string LogTypeValue = "ECP Client Exception Log";

		// Token: 0x04001E1A RID: 7706
		private const string LogComponentValue = "ECPClientExceptionLog";

		// Token: 0x04001E1B RID: 7707
		private const string DefaultRelativeFilePath = "Logging\\ECP\\ClientException";

		// Token: 0x04001E1C RID: 7708
		public static readonly string LogPrefixValue = "ECPClientException";

		// Token: 0x04001E1D RID: 7709
		private static string defaultLogPath;
	}
}
