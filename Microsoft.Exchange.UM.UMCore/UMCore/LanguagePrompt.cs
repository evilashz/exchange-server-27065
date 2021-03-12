using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000165 RID: 357
	internal class LanguagePrompt : VariablePrompt<CultureInfo>
	{
		// Token: 0x06000A7D RID: 2685 RVA: 0x0002C800 File Offset: 0x0002AA00
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"language",
				base.Config.PromptName,
				string.Empty,
				this.language.ToString()
			});
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002C850 File Offset: 0x0002AA50
		internal override string ToSsml()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "LanguagePrompt returning ssmlstring: {0}.", new object[]
			{
				this.ssmlString
			});
			return this.ssmlString;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002C884 File Offset: 0x0002AA84
		protected override void InternalInitialize()
		{
			this.language = UmCultures.GetDisambiguousLanguageFamily(base.InitVal);
			this.IntializeSSML();
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0002C89D File Offset: 0x0002AA9D
		private void IntializeSSML()
		{
			if (!this.TryGetLanguageFileSSML(out this.ssmlString))
			{
				this.InitializeDefaultSSML();
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0002C8B4 File Offset: 0x0002AAB4
		private bool TryGetLanguageFileSSML(out string ssml)
		{
			ssml = null;
			string text = Path.Combine(Util.WavPathFromCulture(base.Culture), string.Format(CultureInfo.InvariantCulture, "Language-{0}.wav", new object[]
			{
				UmCultures.GetLanguagePromptLCID(this.language)
			}));
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "LanguagePrompt looking for file '{0}'.", new object[]
			{
				text
			});
			if (File.Exists(text))
			{
				ssml = string.Format(CultureInfo.InvariantCulture, "<audio src=\"{0}\" />", new object[]
				{
					text
				});
			}
			return ssml != null;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0002C948 File Offset: 0x0002AB48
		private void InitializeDefaultSSML()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "LanguagePrompt: Initializing default SSML.", new object[0]);
			string name = string.Format(CultureInfo.InvariantCulture, "Language-{0}", new object[]
			{
				UmCultures.GetLanguagePromptLCID(this.language)
			});
			string text = PromptConfigBase.PromptResourceManager.GetString(name, base.Culture);
			if (text == null)
			{
				text = this.language.DisplayName;
			}
			this.ssmlString = this.AddProsodyWithVolume(SpeechUtils.XmlEncode(text));
		}

		// Token: 0x0400095F RID: 2399
		private const string LanguageFileNameFormat = "Language-{0}.wav";

		// Token: 0x04000960 RID: 2400
		private CultureInfo language;

		// Token: 0x04000961 RID: 2401
		private string ssmlString;
	}
}
