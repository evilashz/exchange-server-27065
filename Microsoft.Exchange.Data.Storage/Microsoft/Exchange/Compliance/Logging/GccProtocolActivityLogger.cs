using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Win32;

namespace Microsoft.Exchange.Compliance.Logging
{
	// Token: 0x020007CD RID: 1997
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class GccProtocolActivityLogger
	{
		// Token: 0x06004AED RID: 19181 RVA: 0x00139764 File Offset: 0x00137964
		public GccProtocolActivityLogger(string protocol)
		{
			if (GccProtocolActivityLogger.schema == null)
			{
				GccProtocolActivityLogger.schema = new LogSchema("Microsoft Exchange Server", GccProtocolActivityLogger.version, "Global Criminal Compliance Log", GccProtocolActivityLogger.Fields);
			}
			this.protocol = protocol;
			this.notifyReadyToClose = new ManualResetEvent(true);
			this.notifyDoneClosing = new ManualResetEvent(false);
		}

		// Token: 0x1700155F RID: 5471
		// (get) Token: 0x06004AEE RID: 19182 RVA: 0x001397BB File Offset: 0x001379BB
		// (set) Token: 0x06004AEF RID: 19183 RVA: 0x001397D3 File Offset: 0x001379D3
		public static GccProtocolActivityLoggerConfig Config
		{
			get
			{
				if (GccProtocolActivityLogger.config == null)
				{
					GccProtocolActivityLogger.config = new GccProtocolActivityLoggerConfig();
				}
				return GccProtocolActivityLogger.config;
			}
			internal set
			{
				GccProtocolActivityLogger.config = value;
			}
		}

		// Token: 0x06004AF0 RID: 19184 RVA: 0x001397DC File Offset: 0x001379DC
		public void Initialize()
		{
			string path = Regex.Replace(this.protocol, "[/\\:\\\\]", "_");
			string path2 = Path.Combine(GccProtocolActivityLogger.Config.LogRoot, path);
			this.log = new Log("gcc", new LogHeaderFormatter(GccProtocolActivityLogger.schema), "gcc-" + this.protocol, true);
			this.log.Configure(path2, GccProtocolActivityLogger.Config.MaxAge, GccProtocolActivityLogger.Config.MaxDirectorySize, GccProtocolActivityLogger.Config.MaxLogfileSize);
			this.rowFormatter = new LogRowFormatter(GccProtocolActivityLogger.schema);
			this.rowFormatter[1] = this.protocol;
		}

		// Token: 0x06004AF1 RID: 19185 RVA: 0x00139888 File Offset: 0x00137A88
		public void Append(string account, IPAddress clientIP, IPAddress serverIP, ExDateTime accessTimestamp, TimeSpan accessDuration, bool messageDownload)
		{
			if (this.shuttingDown != 0)
			{
				return;
			}
			try
			{
				if (Interlocked.Increment(ref this.callerCount) == 1)
				{
					this.notifyReadyToClose.Reset();
				}
				this.rowFormatter[2] = account;
				this.rowFormatter[3] = clientIP;
				this.rowFormatter[4] = serverIP;
				this.rowFormatter[5] = accessTimestamp;
				this.rowFormatter[6] = accessDuration;
				this.rowFormatter[7] = messageDownload;
				this.log.Append(this.rowFormatter, 0);
			}
			finally
			{
				if (Interlocked.Decrement(ref this.callerCount) == 0)
				{
					this.notifyReadyToClose.Set();
				}
			}
		}

		// Token: 0x06004AF2 RID: 19186 RVA: 0x00139958 File Offset: 0x00137B58
		public void Close()
		{
			int num = Interlocked.Exchange(ref this.shuttingDown, 1);
			this.notifyReadyToClose.WaitOne();
			if (num == 0)
			{
				try
				{
					if (this.log != null)
					{
						this.log.Close();
						this.log = null;
					}
					return;
				}
				finally
				{
					this.notifyDoneClosing.Set();
				}
			}
			this.notifyDoneClosing.WaitOne();
		}

		// Token: 0x06004AF3 RID: 19187 RVA: 0x001399C8 File Offset: 0x00137BC8
		private static string GetExchangeInstallPath()
		{
			return (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup", "MsiInstallPath", null);
		}

		// Token: 0x06004AF4 RID: 19188 RVA: 0x001399EC File Offset: 0x00137BEC
		private static string GetExchangeVersion()
		{
			int num = (int)(Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup", "MsiProductMajor", null) ?? 14);
			int num2 = (int)(Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup", "MsiProductMinor", null) ?? 0);
			int num3 = (int)(Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup", "MsiBuildMajor", null) ?? 0);
			int num4 = (int)(Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup", "MsiBuildMinor", null) ?? 0);
			return string.Format("{0:00}.{1:00}.{2:0000}.{3:000}", new object[]
			{
				num,
				num2,
				num3,
				num4
			});
		}

		// Token: 0x040028B5 RID: 10421
		private const string ExchangeSetupRegkey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup";

		// Token: 0x040028B6 RID: 10422
		private static readonly string[] Fields = new string[]
		{
			"time",
			"protocol",
			"account",
			"client-ip",
			"server-ip",
			"access-timestamp",
			"access-duration",
			"message-download"
		};

		// Token: 0x040028B7 RID: 10423
		private static readonly string version = GccProtocolActivityLogger.GetExchangeVersion();

		// Token: 0x040028B8 RID: 10424
		private static LogSchema schema;

		// Token: 0x040028B9 RID: 10425
		private static GccProtocolActivityLoggerConfig config;

		// Token: 0x040028BA RID: 10426
		private Log log;

		// Token: 0x040028BB RID: 10427
		private LogRowFormatter rowFormatter;

		// Token: 0x040028BC RID: 10428
		private string protocol;

		// Token: 0x040028BD RID: 10429
		private int shuttingDown;

		// Token: 0x040028BE RID: 10430
		private int callerCount;

		// Token: 0x040028BF RID: 10431
		private ManualResetEvent notifyReadyToClose;

		// Token: 0x040028C0 RID: 10432
		private ManualResetEvent notifyDoneClosing;

		// Token: 0x020007CE RID: 1998
		internal enum Field
		{
			// Token: 0x040028C2 RID: 10434
			Time,
			// Token: 0x040028C3 RID: 10435
			Protocol,
			// Token: 0x040028C4 RID: 10436
			Account,
			// Token: 0x040028C5 RID: 10437
			ClientIP,
			// Token: 0x040028C6 RID: 10438
			ServerIP,
			// Token: 0x040028C7 RID: 10439
			AccessTimestamp,
			// Token: 0x040028C8 RID: 10440
			AccessDuration,
			// Token: 0x040028C9 RID: 10441
			MessageDownload
		}
	}
}
