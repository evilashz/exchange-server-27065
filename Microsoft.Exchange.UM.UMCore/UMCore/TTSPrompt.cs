using System;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000010 RID: 16
	internal abstract class TTSPrompt<T> : VariablePrompt<T>
	{
		// Token: 0x06000101 RID: 257 RVA: 0x000054B5 File Offset: 0x000036B5
		public TTSPrompt()
		{
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000054BD File Offset: 0x000036BD
		public TTSPrompt(string promptName, CultureInfo culture, T value) : base(promptName, culture, value)
		{
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000054C8 File Offset: 0x000036C8
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000054D0 File Offset: 0x000036D0
		internal string RawText
		{
			get
			{
				return this.rawText;
			}
			set
			{
				this.rawText = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (set) Token: 0x06000105 RID: 261 RVA: 0x000054D9 File Offset: 0x000036D9
		internal override CultureInfo TTSLanguage
		{
			set
			{
				if (string.Equals(base.Config.Language, "message", StringComparison.OrdinalIgnoreCase))
				{
					this.ttsLanguage = value;
				}
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000054FA File Offset: 0x000036FA
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00005502 File Offset: 0x00003702
		protected string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000550B File Offset: 0x0000370B
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00005513 File Offset: 0x00003713
		protected CultureInfo TtsLanguage
		{
			get
			{
				return this.ttsLanguage;
			}
			set
			{
				this.ttsLanguage = value;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000551C File Offset: 0x0000371C
		internal override string ToSsml()
		{
			string text = string.Concat(new string[]
			{
				"<prosody rate=\"",
				base.ProsodyRate,
				"\">",
				this.text,
				"</prosody>"
			});
			if (this.ttsLanguage == null)
			{
				return text;
			}
			return string.Concat(new string[]
			{
				"<voice xml:lang=\"",
				this.ttsLanguage.Name,
				"\">",
				text,
				"</voice>"
			});
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000055A4 File Offset: 0x000037A4
		protected override void InternalInitialize()
		{
			this.ttsLanguage = null;
			if (!string.Equals(base.Config.Language, "message", StringComparison.OrdinalIgnoreCase))
			{
				try
				{
					this.ttsLanguage = CultureInfo.GetCultureInfo(base.Config.Language);
					if (object.Equals(this.ttsLanguage, CultureInfo.InvariantCulture))
					{
						this.ttsLanguage = null;
					}
				}
				catch (ArgumentException)
				{
					this.ttsLanguage = null;
				}
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000561C File Offset: 0x0000381C
		protected override string AddProsodyWithVolume(string text)
		{
			return Util.AddProsodyWithVolume((this.ttsLanguage != null) ? this.ttsLanguage : base.Culture, text);
		}

		// Token: 0x04000054 RID: 84
		private CultureInfo ttsLanguage;

		// Token: 0x04000055 RID: 85
		private string text;

		// Token: 0x04000056 RID: 86
		private string rawText;
	}
}
