using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200013D RID: 317
	internal class FilePrompt : Prompt
	{
		// Token: 0x060008DB RID: 2267 RVA: 0x00026861 File Offset: 0x00024A61
		public FilePrompt()
		{
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00026869 File Offset: 0x00024A69
		internal FilePrompt(string fileName, CultureInfo culture) : base(fileName, culture)
		{
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00026874 File Offset: 0x00024A74
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"wave",
				this.filename,
				this.filename,
				this.filename
			});
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x000268BC File Offset: 0x00024ABC
		internal static string BuildAudioSsml(string fileName, string prosodyRate)
		{
			return string.Format(CultureInfo.InvariantCulture, "<prosody rate=\"{0}\"><audio src=\"{1}\" /></prosody>", new object[]
			{
				prosodyRate,
				SpeechUtils.XmlEncode(fileName)
			});
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x000268ED File Offset: 0x00024AED
		internal override string ToSsml()
		{
			return FilePrompt.BuildAudioSsml(this.filename, base.ProsodyRate);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00026900 File Offset: 0x00024B00
		protected override void InternalInitialize()
		{
			this.filename = Path.Combine(Util.WavPathFromCulture(base.Culture), base.Config.PromptName);
			if (!GlobalActivityManager.ConfigClass.RecordingFileNameCache.Contains(this.filename) && !File.Exists(this.filename))
			{
				throw new FileNotFoundException(this.filename);
			}
		}

		// Token: 0x040008BD RID: 2237
		internal const string SSMLTemplate = "<audio src=\"{0}\" />";

		// Token: 0x040008BE RID: 2238
		internal const string SSMLProsodyTemplate = "<prosody rate=\"{0}\"><audio src=\"{1}\" /></prosody>";

		// Token: 0x040008BF RID: 2239
		private string filename;
	}
}
