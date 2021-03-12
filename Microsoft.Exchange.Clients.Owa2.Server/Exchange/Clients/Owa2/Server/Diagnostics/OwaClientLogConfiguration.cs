using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x0200044B RID: 1099
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OwaClientLogConfiguration : ILogConfiguration
	{
		// Token: 0x06002501 RID: 9473 RVA: 0x000861DC File Offset: 0x000843DC
		public OwaClientLogConfiguration()
		{
			this.IsLoggingEnabled = AppConfigLoader.GetConfigBoolValue("OWAIsClientLoggingEnabled", true);
			this.LogPath = AppConfigLoader.GetConfigStringValue("OWAClientLogPath", OwaClientLogConfiguration.DefaultLogPath);
			this.MaxLogAge = AppConfigLoader.GetConfigTimeSpanValue("OWAClientMaxLogAge", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromDays(30.0));
			this.MaxLogDirectorySizeInBytes = (long)OwaAppConfigLoader.GetConfigByteQuantifiedSizeValue("OWAClientMaxLogDirectorySize", ByteQuantifiedSize.FromGB(1UL)).ToBytes();
			this.MaxLogFileSizeInBytes = (long)OwaAppConfigLoader.GetConfigByteQuantifiedSizeValue("OWAClientMaxLogFileSize", ByteQuantifiedSize.FromMB(10UL)).ToBytes();
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06002502 RID: 9474 RVA: 0x0008627C File Offset: 0x0008447C
		public static string DefaultLogPath
		{
			get
			{
				if (OwaClientLogConfiguration.defaultLogPath == null)
				{
					OwaClientLogConfiguration.defaultLogPath = Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\OWA\\Client");
				}
				return OwaClientLogConfiguration.defaultLogPath;
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06002503 RID: 9475 RVA: 0x0008629E File Offset: 0x0008449E
		// (set) Token: 0x06002504 RID: 9476 RVA: 0x000862A6 File Offset: 0x000844A6
		public bool IsLoggingEnabled { get; private set; }

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06002505 RID: 9477 RVA: 0x000862AF File Offset: 0x000844AF
		public bool IsActivityEventHandler
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002506 RID: 9478 RVA: 0x000862B2 File Offset: 0x000844B2
		// (set) Token: 0x06002507 RID: 9479 RVA: 0x000862BA File Offset: 0x000844BA
		public string LogPath { get; private set; }

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06002508 RID: 9480 RVA: 0x000862C3 File Offset: 0x000844C3
		// (set) Token: 0x06002509 RID: 9481 RVA: 0x000862CB File Offset: 0x000844CB
		public TimeSpan MaxLogAge { get; private set; }

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x0600250A RID: 9482 RVA: 0x000862D4 File Offset: 0x000844D4
		// (set) Token: 0x0600250B RID: 9483 RVA: 0x000862DC File Offset: 0x000844DC
		public long MaxLogDirectorySizeInBytes { get; private set; }

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x0600250C RID: 9484 RVA: 0x000862E5 File Offset: 0x000844E5
		// (set) Token: 0x0600250D RID: 9485 RVA: 0x000862ED File Offset: 0x000844ED
		public long MaxLogFileSizeInBytes { get; private set; }

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x0600250E RID: 9486 RVA: 0x000862F6 File Offset: 0x000844F6
		public string LogComponent
		{
			get
			{
				return "OWAClientLog";
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x0600250F RID: 9487 RVA: 0x000862FD File Offset: 0x000844FD
		public string LogPrefix
		{
			get
			{
				return OwaClientLogConfiguration.LogPrefixValue;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06002510 RID: 9488 RVA: 0x00086304 File Offset: 0x00084504
		public string LogType
		{
			get
			{
				return "OWA Client Log";
			}
		}

		// Token: 0x040014F8 RID: 5368
		private const string LogTypeValue = "OWA Client Log";

		// Token: 0x040014F9 RID: 5369
		private const string LogComponentValue = "OWAClientLog";

		// Token: 0x040014FA RID: 5370
		private const string DefaultRelativeFilePath = "Logging\\OWA\\Client";

		// Token: 0x040014FB RID: 5371
		public static readonly string LogPrefixValue = "OWAClient";

		// Token: 0x040014FC RID: 5372
		private static string defaultLogPath;
	}
}
