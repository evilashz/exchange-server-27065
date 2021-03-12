using System;
using System.IO;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000017 RID: 23
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MsiConfigurationInfo
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00006017 File Offset: 0x00004217
		// (set) Token: 0x06000162 RID: 354 RVA: 0x0000601E File Offset: 0x0000421E
		public static SetupContext SetupContext
		{
			get
			{
				return MsiConfigurationInfo.setupContext;
			}
			set
			{
				MsiConfigurationInfo.setupContext = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000163 RID: 355
		public abstract string Name { get; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000164 RID: 356
		public abstract Guid ProductCode { get; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000165 RID: 357
		protected abstract string LogFileName { get; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00006026 File Offset: 0x00004226
		public string LogFilePath
		{
			get
			{
				return Path.Combine(MsiConfigurationInfo.setupLogDirectory, this.LogFileName);
			}
		}

		// Token: 0x04000052 RID: 82
		private static SetupContext setupContext;

		// Token: 0x04000053 RID: 83
		private static readonly string setupLogDirectory = ConfigurationContext.Setup.SetupLoggingPath;
	}
}
