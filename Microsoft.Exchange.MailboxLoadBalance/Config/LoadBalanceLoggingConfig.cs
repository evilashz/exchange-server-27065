using System;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Config
{
	// Token: 0x02000035 RID: 53
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadBalanceLoggingConfig : ObjectLogConfiguration
	{
		// Token: 0x0600021F RID: 543 RVA: 0x0000790C File Offset: 0x00005B0C
		public LoadBalanceLoggingConfig(string logName)
		{
			AnchorUtil.ThrowOnNullOrEmptyArgument(logName, "logName");
			this.LogName = logName;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00007926 File Offset: 0x00005B26
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000792E File Offset: 0x00005B2E
		public string LogName { get; private set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00007937 File Offset: 0x00005B37
		public override bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000793C File Offset: 0x00005B3C
		public override string LoggingFolder
		{
			get
			{
				string text = LoadBalanceADSettings.Instance.Value.LogFilePath;
				if (string.IsNullOrWhiteSpace(text))
				{
					text = Path.Combine(ExchangeSetupContext.InstallPath ?? Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), string.Format("logging\\{0}Logs", "MailboxLoadBalance"));
				}
				return Path.Combine(text, this.LogName);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000799A File Offset: 0x00005B9A
		public override string LogComponentName
		{
			get
			{
				return "MLB";
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000225 RID: 549 RVA: 0x000079A1 File Offset: 0x00005BA1
		public override string FilenamePrefix
		{
			get
			{
				return this.LogName;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000079A9 File Offset: 0x00005BA9
		public override long MaxLogDirSize
		{
			get
			{
				return LoadBalanceADSettings.Instance.Value.LogMaxDirectorySize;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000227 RID: 551 RVA: 0x000079BA File Offset: 0x00005BBA
		public override long MaxLogFileSize
		{
			get
			{
				return LoadBalanceADSettings.Instance.Value.LogMaxFileSize;
			}
		}

		// Token: 0x0400009B RID: 155
		internal const string DefaultLogComponentName = "MLB";
	}
}
