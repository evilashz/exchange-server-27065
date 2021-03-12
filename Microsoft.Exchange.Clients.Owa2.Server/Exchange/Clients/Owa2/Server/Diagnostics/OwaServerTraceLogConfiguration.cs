using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000452 RID: 1106
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OwaServerTraceLogConfiguration : ILogConfiguration
	{
		// Token: 0x0600254A RID: 9546 RVA: 0x000875BC File Offset: 0x000857BC
		public OwaServerTraceLogConfiguration()
		{
			this.IsLoggingEnabled = AppConfigLoader.GetConfigBoolValue("OWAIsServerTraceLoggingEnabled", true);
			this.LogPath = AppConfigLoader.GetConfigStringValue("OWATraceLogPath", OwaServerTraceLogConfiguration.DefaultLogPath);
			this.MaxLogAge = AppConfigLoader.GetConfigTimeSpanValue("OWATraceMaxLogAge", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromDays(5.0));
			this.MaxLogDirectorySizeInBytes = (long)OwaAppConfigLoader.GetConfigByteQuantifiedSizeValue("OWATraceMaxLogDirectorySize", ByteQuantifiedSize.FromGB(1UL)).ToBytes();
			this.MaxLogFileSizeInBytes = (long)OwaAppConfigLoader.GetConfigByteQuantifiedSizeValue("OWATraceMaxLogFileSize", ByteQuantifiedSize.FromMB(10UL)).ToBytes();
			this.OwaTraceLoggingThreshold = AppConfigLoader.GetConfigDoubleValue("OwaTraceLoggingThreshold", 0.0, 0.0, 0.0);
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x0600254B RID: 9547 RVA: 0x00087687 File Offset: 0x00085887
		public static string DefaultLogPath
		{
			get
			{
				if (OwaServerTraceLogConfiguration.defaultLogPath == null)
				{
					OwaServerTraceLogConfiguration.defaultLogPath = Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\Owa\\ServerTrace");
				}
				return OwaServerTraceLogConfiguration.defaultLogPath;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x0600254C RID: 9548 RVA: 0x000876A9 File Offset: 0x000858A9
		// (set) Token: 0x0600254D RID: 9549 RVA: 0x000876B1 File Offset: 0x000858B1
		public bool IsLoggingEnabled { get; private set; }

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x0600254E RID: 9550 RVA: 0x000876BA File Offset: 0x000858BA
		public bool IsActivityEventHandler
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x0600254F RID: 9551 RVA: 0x000876BD File Offset: 0x000858BD
		// (set) Token: 0x06002550 RID: 9552 RVA: 0x000876C5 File Offset: 0x000858C5
		public string LogPath { get; private set; }

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06002551 RID: 9553 RVA: 0x000876CE File Offset: 0x000858CE
		// (set) Token: 0x06002552 RID: 9554 RVA: 0x000876D6 File Offset: 0x000858D6
		public TimeSpan MaxLogAge { get; private set; }

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x000876DF File Offset: 0x000858DF
		// (set) Token: 0x06002554 RID: 9556 RVA: 0x000876E7 File Offset: 0x000858E7
		public long MaxLogDirectorySizeInBytes { get; private set; }

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06002555 RID: 9557 RVA: 0x000876F0 File Offset: 0x000858F0
		// (set) Token: 0x06002556 RID: 9558 RVA: 0x000876F8 File Offset: 0x000858F8
		public long MaxLogFileSizeInBytes { get; private set; }

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x00087701 File Offset: 0x00085901
		// (set) Token: 0x06002558 RID: 9560 RVA: 0x00087709 File Offset: 0x00085909
		public double OwaTraceLoggingThreshold { get; private set; }

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06002559 RID: 9561 RVA: 0x00087712 File Offset: 0x00085912
		public string LogComponent
		{
			get
			{
				return "OwaServerTraceLog";
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x0600255A RID: 9562 RVA: 0x00087719 File Offset: 0x00085919
		public string LogPrefix
		{
			get
			{
				return OwaServerTraceLogConfiguration.LogPrefixValue;
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x0600255B RID: 9563 RVA: 0x00087720 File Offset: 0x00085920
		public string LogType
		{
			get
			{
				return "OWA Server Trace Log";
			}
		}

		// Token: 0x04001565 RID: 5477
		private const string LogTypeValue = "OWA Server Trace Log";

		// Token: 0x04001566 RID: 5478
		private const string LogComponentValue = "OwaServerTraceLog";

		// Token: 0x04001567 RID: 5479
		private const string DefaultRelativeFilePath = "Logging\\Owa\\ServerTrace";

		// Token: 0x04001568 RID: 5480
		public static readonly string LogPrefixValue = "OwaServerTrace";

		// Token: 0x04001569 RID: 5481
		private static string defaultLogPath;
	}
}
