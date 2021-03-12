using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000445 RID: 1093
	public class OwaClientStrings : OwaPageCached
	{
		// Token: 0x06002764 RID: 10084 RVA: 0x000E058C File Offset: 0x000DE78C
		public void RenderVariable(string variableValue, string variableName)
		{
			RenderingUtilities.RenderStringVariable(base.SanitizingResponse, variableName, variableValue);
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x000E059B File Offset: 0x000DE79B
		public void RenderVariable(Strings.IDs stringID, string variableName)
		{
			RenderingUtilities.RenderStringVariable(base.SanitizingResponse, variableName, stringID);
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x000E05AA File Offset: 0x000DE7AA
		private void CheckDuplicatedStringID(Strings.IDs stringID)
		{
			if (this.stringIDList == null)
			{
				this.stringIDList = new List<Strings.IDs>();
			}
			this.stringIDList.Add(stringID);
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x000E05CB File Offset: 0x000DE7CB
		private void CheckDuplicatedVariableName(string variableName)
		{
			if (this.variableNameList == null)
			{
				this.variableNameList = new List<string>();
			}
			this.variableNameList.Add(variableName);
		}

		// Token: 0x04001B93 RID: 7059
		public const string HotKeyCtrl = "Ctrl";

		// Token: 0x04001B94 RID: 7060
		public const string HotKeyAlt = "Alt";

		// Token: 0x04001B95 RID: 7061
		public const string HotKeyMacCtrl = "MacCtrl";

		// Token: 0x04001B96 RID: 7062
		public const string HotKeyShift = "Shift";

		// Token: 0x04001B97 RID: 7063
		public const string HotKeyTab = "Tab";

		// Token: 0x04001B98 RID: 7064
		private List<Strings.IDs> stringIDList;

		// Token: 0x04001B99 RID: 7065
		private List<string> variableNameList;
	}
}
