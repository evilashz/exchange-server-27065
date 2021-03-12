using System;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000002 RID: 2
	internal abstract class Prompt
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public Prompt()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		internal Prompt(string promptName, CultureInfo culture)
		{
			PromptSetting promptSetting = new PromptSetting(promptName);
			this.Initialize(promptSetting, culture);
		}

		// Token: 0x17000001 RID: 1
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020FB File Offset: 0x000002FB
		internal virtual CultureInfo TTSLanguage
		{
			set
			{
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020FD File Offset: 0x000002FD
		internal string ProsodyRate
		{
			get
			{
				return this.prosodyRate;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002105 File Offset: 0x00000305
		internal bool IsInitialized
		{
			get
			{
				return this.isInitialized;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000210D File Offset: 0x0000030D
		internal string PromptName
		{
			get
			{
				return this.config.PromptName;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000211A File Offset: 0x0000031A
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002122 File Offset: 0x00000322
		protected PromptSetting Config
		{
			get
			{
				return this.config;
			}
			set
			{
				this.config = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000212B File Offset: 0x0000032B
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002133 File Offset: 0x00000333
		protected CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
			set
			{
				this.culture = value;
			}
		}

		// Token: 0x0600000B RID: 11
		internal abstract string ToSsml();

		// Token: 0x0600000C RID: 12 RVA: 0x0000213C File Offset: 0x0000033C
		internal void SetProsodyRate(float rate)
		{
			if (string.Equals(this.config.ProsodyRate, "user", StringComparison.OrdinalIgnoreCase))
			{
				this.prosodyRate = (rate * 100f).ToString(CultureInfo.InvariantCulture) + "%";
				if (rate >= 0f)
				{
					this.prosodyRate = "+" + this.prosodyRate;
				}
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021A4 File Offset: 0x000003A4
		internal void Initialize(PromptSetting config, CultureInfo c)
		{
			if (!this.IsInitialized)
			{
				this.config = config;
				this.culture = c;
				this.prosodyRate = (string.Equals(config.ProsodyRate, "user", StringComparison.OrdinalIgnoreCase) ? "+0%" : config.ProsodyRate);
				this.isInitialized = true;
				this.InternalInitialize();
			}
		}

		// Token: 0x0600000E RID: 14
		protected abstract void InternalInitialize();

		// Token: 0x04000001 RID: 1
		internal const string LogFormat = "Type={0}, Name={1}, File={2}, Value={3}";

		// Token: 0x04000002 RID: 2
		internal const string LogFormatWithExtraInfo = "Type={0}, Name={1}, File={2}, Value={3} Extra={4}";

		// Token: 0x04000003 RID: 3
		private PromptSetting config;

		// Token: 0x04000004 RID: 4
		private CultureInfo culture;

		// Token: 0x04000005 RID: 5
		private string prosodyRate;

		// Token: 0x04000006 RID: 6
		private bool isInitialized;
	}
}
