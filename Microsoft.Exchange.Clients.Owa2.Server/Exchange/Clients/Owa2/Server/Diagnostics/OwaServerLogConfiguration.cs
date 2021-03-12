using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x0200044F RID: 1103
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OwaServerLogConfiguration : ILogConfiguration
	{
		// Token: 0x0600252E RID: 9518 RVA: 0x0008651C File Offset: 0x0008471C
		public OwaServerLogConfiguration()
		{
			this.IsLoggingEnabled = AppConfigLoader.GetConfigBoolValue("OWAIsLoggingEnabled", true);
			this.LogPath = AppConfigLoader.GetConfigStringValue("OWALogPath", OwaServerLogConfiguration.DefaultLogPath);
			this.MaxLogAge = AppConfigLoader.GetConfigTimeSpanValue("OWAMaxLogAge", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromDays(30.0));
			this.MaxLogDirectorySizeInBytes = (long)OwaAppConfigLoader.GetConfigByteQuantifiedSizeValue("OWAMaxLogDirectorySize", ByteQuantifiedSize.FromGB(1UL)).ToBytes();
			this.MaxLogFileSizeInBytes = (long)OwaAppConfigLoader.GetConfigByteQuantifiedSizeValue("OWAMaxLogFileSize", ByteQuantifiedSize.FromMB(10UL)).ToBytes();
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x0600252F RID: 9519 RVA: 0x000865BC File Offset: 0x000847BC
		public static string DefaultLogPath
		{
			get
			{
				if (OwaServerLogConfiguration.defaultLogPath == null)
				{
					OwaServerLogConfiguration.defaultLogPath = Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\OWA\\Server");
				}
				return OwaServerLogConfiguration.defaultLogPath;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06002530 RID: 9520 RVA: 0x000865DE File Offset: 0x000847DE
		// (set) Token: 0x06002531 RID: 9521 RVA: 0x000865E6 File Offset: 0x000847E6
		public bool IsLoggingEnabled { get; private set; }

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06002532 RID: 9522 RVA: 0x000865EF File Offset: 0x000847EF
		public bool IsActivityEventHandler
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06002533 RID: 9523 RVA: 0x000865F2 File Offset: 0x000847F2
		// (set) Token: 0x06002534 RID: 9524 RVA: 0x000865FA File Offset: 0x000847FA
		public string LogPath { get; private set; }

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06002535 RID: 9525 RVA: 0x00086603 File Offset: 0x00084803
		// (set) Token: 0x06002536 RID: 9526 RVA: 0x0008660B File Offset: 0x0008480B
		public TimeSpan MaxLogAge { get; private set; }

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06002537 RID: 9527 RVA: 0x00086614 File Offset: 0x00084814
		// (set) Token: 0x06002538 RID: 9528 RVA: 0x0008661C File Offset: 0x0008481C
		public long MaxLogDirectorySizeInBytes { get; private set; }

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06002539 RID: 9529 RVA: 0x00086625 File Offset: 0x00084825
		// (set) Token: 0x0600253A RID: 9530 RVA: 0x0008662D File Offset: 0x0008482D
		public long MaxLogFileSizeInBytes { get; private set; }

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x00086636 File Offset: 0x00084836
		public string LogComponent
		{
			get
			{
				return "OWAServerLog";
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x0600253C RID: 9532 RVA: 0x0008663D File Offset: 0x0008483D
		public string LogPrefix
		{
			get
			{
				return OwaServerLogConfiguration.LogPrefixValue;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x0600253D RID: 9533 RVA: 0x00086644 File Offset: 0x00084844
		public string LogType
		{
			get
			{
				return "OWA Server Log";
			}
		}

		// Token: 0x0400150E RID: 5390
		private const string LogTypeValue = "OWA Server Log";

		// Token: 0x0400150F RID: 5391
		private const string LogComponentValue = "OWAServerLog";

		// Token: 0x04001510 RID: 5392
		private const string DefaultRelativeFilePath = "Logging\\OWA\\Server";

		// Token: 0x04001511 RID: 5393
		public static readonly string LogPrefixValue = "OWAServer";

		// Token: 0x04001512 RID: 5394
		private static string defaultLogPath;
	}
}
