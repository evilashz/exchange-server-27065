using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001F2 RID: 498
	internal class TextListPrompt : VariablePrompt<List<string>>
	{
		// Token: 0x06000E9C RID: 3740 RVA: 0x0004174C File Offset: 0x0003F94C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in this.promptNameList)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(",");
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"textList",
				base.Config.PromptName,
				string.Empty,
				stringBuilder.ToString()
			});
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00041800 File Offset: 0x0003FA00
		internal override string ToSsml()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "TextListPrompt returning ssmlstring: {0}.", new object[]
			{
				this.ssmlString
			});
			return this.ssmlString;
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00041834 File Offset: 0x0003FA34
		protected override void InternalInitialize()
		{
			this.promptNameList = base.InitVal;
			this.ssmlString = string.Empty;
			foreach (string initVal in this.promptNameList)
			{
				TextPrompt textPrompt = new TextPrompt();
				textPrompt.Initialize(base.Config, base.Culture, initVal);
				this.ssmlString += textPrompt.ToSsml();
				this.ssmlString += "<break time=\"400ms\"/>";
			}
		}

		// Token: 0x04000AFE RID: 2814
		internal const string BreakSsml = "<break time=\"400ms\"/>";

		// Token: 0x04000AFF RID: 2815
		private List<string> promptNameList;

		// Token: 0x04000B00 RID: 2816
		private string ssmlString;
	}
}
