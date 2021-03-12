using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x0200044D RID: 1101
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OwaClientTraceLogConfiguration : ILogConfiguration
	{
		// Token: 0x06002518 RID: 9496 RVA: 0x00086380 File Offset: 0x00084580
		public OwaClientTraceLogConfiguration()
		{
			this.IsLoggingEnabled = AppConfigLoader.GetConfigBoolValue("OWAIsClientTraceLoggingEnabled", true);
			this.LogPath = AppConfigLoader.GetConfigStringValue("OWAClientTraceLogPath", OwaClientTraceLogConfiguration.DefaultLogPath);
			this.MaxLogAge = AppConfigLoader.GetConfigTimeSpanValue("OWAClientTraceMaxLogAge", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromDays(30.0));
			this.MaxLogDirectorySizeInBytes = (long)OwaAppConfigLoader.GetConfigByteQuantifiedSizeValue("OWAClientTraceMaxLogDirectorySize", ByteQuantifiedSize.FromGB(1UL)).ToBytes();
			this.MaxLogFileSizeInBytes = (long)OwaAppConfigLoader.GetConfigByteQuantifiedSizeValue("OWAClientTraceMaxLogFileSize", ByteQuantifiedSize.FromMB(10UL)).ToBytes();
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06002519 RID: 9497 RVA: 0x00086420 File Offset: 0x00084620
		public static string DefaultLogPath
		{
			get
			{
				if (OwaClientTraceLogConfiguration.defaultLogPath == null)
				{
					OwaClientTraceLogConfiguration.defaultLogPath = Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\OWA\\ClientTrace");
				}
				return OwaClientTraceLogConfiguration.defaultLogPath;
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x0600251A RID: 9498 RVA: 0x00086442 File Offset: 0x00084642
		// (set) Token: 0x0600251B RID: 9499 RVA: 0x0008644A File Offset: 0x0008464A
		public bool IsLoggingEnabled { get; private set; }

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x0600251C RID: 9500 RVA: 0x00086453 File Offset: 0x00084653
		public bool IsActivityEventHandler
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x0600251D RID: 9501 RVA: 0x00086456 File Offset: 0x00084656
		// (set) Token: 0x0600251E RID: 9502 RVA: 0x0008645E File Offset: 0x0008465E
		public string LogPath { get; private set; }

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x0600251F RID: 9503 RVA: 0x00086467 File Offset: 0x00084667
		// (set) Token: 0x06002520 RID: 9504 RVA: 0x0008646F File Offset: 0x0008466F
		public TimeSpan MaxLogAge { get; private set; }

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06002521 RID: 9505 RVA: 0x00086478 File Offset: 0x00084678
		// (set) Token: 0x06002522 RID: 9506 RVA: 0x00086480 File Offset: 0x00084680
		public long MaxLogDirectorySizeInBytes { get; private set; }

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x00086489 File Offset: 0x00084689
		// (set) Token: 0x06002524 RID: 9508 RVA: 0x00086491 File Offset: 0x00084691
		public long MaxLogFileSizeInBytes { get; private set; }

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06002525 RID: 9509 RVA: 0x0008649A File Offset: 0x0008469A
		public string LogComponent
		{
			get
			{
				return "OWAClientTraceLog";
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06002526 RID: 9510 RVA: 0x000864A1 File Offset: 0x000846A1
		public string LogPrefix
		{
			get
			{
				return OwaClientTraceLogConfiguration.LogPrefixValue;
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06002527 RID: 9511 RVA: 0x000864A8 File Offset: 0x000846A8
		public string LogType
		{
			get
			{
				return "OWA Client Trace Log";
			}
		}

		// Token: 0x04001503 RID: 5379
		private const string LogTypeValue = "OWA Client Trace Log";

		// Token: 0x04001504 RID: 5380
		private const string LogComponentValue = "OWAClientTraceLog";

		// Token: 0x04001505 RID: 5381
		private const string DefaultRelativeFilePath = "Logging\\OWA\\ClientTrace";

		// Token: 0x04001506 RID: 5382
		public static readonly string LogPrefixValue = "OWAClientTrace";

		// Token: 0x04001507 RID: 5383
		private static string defaultLogPath;
	}
}
