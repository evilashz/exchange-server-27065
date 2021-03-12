using System;
using System.Collections;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000100 RID: 256
	internal class ConstPromptBookmarkConfig : PromptConfigBase
	{
		// Token: 0x060006D9 RID: 1753 RVA: 0x0001BBE4 File Offset: 0x00019DE4
		internal ConstPromptBookmarkConfig(string promptName, string promptText, string conditionString, CultureInfo culture, ActivityManagerConfig managerConfig) : base(promptName, "bookmark", conditionString, managerConfig)
		{
			this.bookmark = new PromptBookmark();
			PromptSetting config = new PromptSetting(this);
			this.bookmark.Initialize(config, culture);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001BC21 File Offset: 0x00019E21
		internal override void AddPrompts(ArrayList promptList, ActivityManager manager, CultureInfo culture)
		{
			promptList.Add(this.bookmark);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001BC30 File Offset: 0x00019E30
		internal override void Validate()
		{
		}

		// Token: 0x040007D9 RID: 2009
		private PromptBookmark bookmark;
	}
}
