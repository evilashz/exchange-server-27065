using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001E6 RID: 486
	internal class StatementParser
	{
		// Token: 0x06000E43 RID: 3651 RVA: 0x00040531 File Offset: 0x0003E731
		public StatementParser(string promptName, CultureInfo culture, string locString)
		{
			this.locString = locString.Trim();
			this.promptName = promptName;
			this.culture = culture;
			this.match = Regex.Match(locString, StatementParser.FormatParameterRegex);
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x0004056B File Offset: 0x0003E76B
		// (set) Token: 0x06000E45 RID: 3653 RVA: 0x00040573 File Offset: 0x0003E773
		internal string SubPromptFileName { get; set; }

		// Token: 0x06000E46 RID: 3654 RVA: 0x0004057C File Offset: 0x0003E77C
		public StatementChunk NextChunk()
		{
			StatementChunk result = null;
			string text = string.Empty;
			if (this.match.Success)
			{
				text = this.locString.Substring(this.startIndex, this.match.Index - this.startIndex).Trim();
				if (StatementParser.IsSpokenText(text))
				{
					result = this.CreateTextOrFileChunk(text);
					this.startIndex = this.match.Index;
					this.fileChunkNum++;
				}
				else
				{
					result = this.CreateVariableChunk(int.Parse(this.match.Groups[StatementParser.FormatParameterGroup].Value, CultureInfo.InvariantCulture));
					this.startIndex = this.match.Index + this.match.Length;
					this.match = this.match.NextMatch();
				}
			}
			else if (this.startIndex < this.locString.Length)
			{
				text = this.locString.Substring(this.startIndex, this.locString.Length - this.startIndex).Trim();
				this.startIndex = this.locString.Length;
				if (StatementParser.IsSpokenText(text))
				{
					result = this.CreateTextOrFileChunk(text);
				}
			}
			return result;
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x000406B8 File Offset: 0x0003E8B8
		private StatementChunk CreateVariableChunk(int varNum)
		{
			return new StatementChunk
			{
				Type = ChunkType.Variable,
				Value = varNum
			};
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x000406E4 File Offset: 0x0003E8E4
		private StatementChunk CreateTextOrFileChunk(string chunkValue)
		{
			string path = this.promptName + "." + this.fileChunkNum.ToString(this.culture) + ".wav";
			this.SubPromptFileName = Path.Combine(Util.WavPathFromCulture(this.culture), path);
			StatementChunk result;
			if (GlobalActivityManager.ConfigClass.RecordingFileNameCache.Contains(this.SubPromptFileName))
			{
				result = new StatementChunk
				{
					Type = ChunkType.File,
					Value = chunkValue
				};
			}
			else
			{
				if (!GlobCfg.AllowTemporaryTTS)
				{
					throw new FileNotFoundException(this.SubPromptFileName);
				}
				result = new StatementChunk
				{
					Type = ChunkType.Text,
					Value = chunkValue
				};
			}
			return result;
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00040788 File Offset: 0x0003E988
		protected static bool IsSpokenText(string chunk)
		{
			if (chunk == null || chunk.Length < 1)
			{
				return false;
			}
			foreach (char c in chunk)
			{
				if (char.IsLetterOrDigit(c))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000ADF RID: 2783
		private static readonly string FormatParameterGroup = "paramNumber";

		// Token: 0x04000AE0 RID: 2784
		private static readonly string FormatParameterRegex = "\\{(?<" + StatementParser.FormatParameterGroup + ">\\d+)\\}";

		// Token: 0x04000AE1 RID: 2785
		private string promptName;

		// Token: 0x04000AE2 RID: 2786
		private CultureInfo culture;

		// Token: 0x04000AE3 RID: 2787
		private string locString;

		// Token: 0x04000AE4 RID: 2788
		private Match match;

		// Token: 0x04000AE5 RID: 2789
		private int startIndex;

		// Token: 0x04000AE6 RID: 2790
		private int fileChunkNum = 1;
	}
}
