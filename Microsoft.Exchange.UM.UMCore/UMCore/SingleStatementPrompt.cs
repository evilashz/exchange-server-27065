using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001D5 RID: 469
	internal class SingleStatementPrompt : Prompt
	{
		// Token: 0x06000D9E RID: 3486 RVA: 0x0003C79C File Offset: 0x0003A99C
		internal SingleStatementPrompt(params Prompt[] parameterPrompts)
		{
			this.parameterPrompts = ((parameterPrompts != null) ? new List<Prompt>(parameterPrompts) : new List<Prompt>());
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x0003C7D0 File Offset: 0x0003A9D0
		public List<Prompt> PromptList
		{
			get
			{
				return this.promptList;
			}
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0003C7D8 File Offset: 0x0003A9D8
		public override string ToString()
		{
			foreach (Prompt prompt in this.promptList)
			{
				if (this.log.Length > 0)
				{
					this.log.AppendLine();
				}
				this.log.Append(prompt.ToString());
			}
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"statement",
				base.Config.PromptName,
				string.Empty,
				this.log.ToString()
			});
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0003C894 File Offset: 0x0003AA94
		internal override string ToSsml()
		{
			foreach (Prompt prompt in this.promptList)
			{
				this.ssml.Append(prompt.ToSsml());
			}
			return this.ssml.ToString();
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0003C900 File Offset: 0x0003AB00
		protected override void InternalInitialize()
		{
			this.promptList = PromptUtils.CreateStatementPrompt(base.PromptName, this.parameterPrompts, base.Culture);
		}

		// Token: 0x04000A9D RID: 2717
		private List<Prompt> parameterPrompts;

		// Token: 0x04000A9E RID: 2718
		private List<Prompt> promptList;

		// Token: 0x04000A9F RID: 2719
		private StringBuilder ssml = new StringBuilder();

		// Token: 0x04000AA0 RID: 2720
		private StringBuilder log = new StringBuilder();
	}
}
