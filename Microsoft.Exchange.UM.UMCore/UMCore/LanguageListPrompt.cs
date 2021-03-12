using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000164 RID: 356
	internal class LanguageListPrompt : VariablePrompt<List<CultureInfo>>
	{
		// Token: 0x06000A79 RID: 2681 RVA: 0x0002C674 File Offset: 0x0002A874
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (CultureInfo cultureInfo in this.languages)
			{
				stringBuilder.Append(cultureInfo.ToString());
				stringBuilder.Append(",");
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"language",
				base.Config.PromptName,
				string.Empty,
				stringBuilder.ToString()
			});
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0002C730 File Offset: 0x0002A930
		internal override string ToSsml()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "LanguageListPrompt returning ssmlstring: {0}.", new object[]
			{
				this.ssmlString
			});
			return this.ssmlString;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002C764 File Offset: 0x0002A964
		protected override void InternalInitialize()
		{
			this.languages = base.InitVal;
			this.ssmlString = string.Empty;
			foreach (CultureInfo initVal in this.languages)
			{
				LanguagePrompt languagePrompt = new LanguagePrompt();
				languagePrompt.Initialize(base.Config, base.Culture, initVal);
				this.ssmlString += languagePrompt.ToSsml();
			}
		}

		// Token: 0x0400095D RID: 2397
		private List<CultureInfo> languages;

		// Token: 0x0400095E RID: 2398
		private string ssmlString;
	}
}
