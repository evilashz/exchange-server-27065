using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001D2 RID: 466
	internal class SilencePrompt : Prompt
	{
		// Token: 0x06000D90 RID: 3472 RVA: 0x0003C38A File Offset: 0x0003A58A
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0003C391 File Offset: 0x0003A591
		internal override string ToSsml()
		{
			return this.ssml;
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0003C39C File Offset: 0x0003A59C
		protected override void InternalInitialize()
		{
			if (base.Config.PromptName.EndsWith("ms", StringComparison.InvariantCulture))
			{
				this.numSec = int.Parse(base.Config.PromptName.Substring(0, base.Config.PromptName.Length - 2), CultureInfo.InvariantCulture) / 1000;
			}
			else if (base.Config.PromptName.EndsWith("s", StringComparison.InvariantCulture))
			{
				this.numSec = int.Parse(base.Config.PromptName.Substring(0, base.Config.PromptName.Length - 1), CultureInfo.InvariantCulture);
			}
			else
			{
				this.numSec = 1;
			}
			string text = Path.Combine(Util.WavPathFromCulture(base.Culture), "Silence-1.wav");
			if (!File.Exists(text))
			{
				throw new FileNotFoundException(text);
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.numSec; i++)
			{
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "<audio src=\"{0}\" />", new object[]
				{
					text
				}));
			}
			this.ssml = stringBuilder.ToString();
		}

		// Token: 0x04000A98 RID: 2712
		private int numSec;

		// Token: 0x04000A99 RID: 2713
		private string ssml;
	}
}
