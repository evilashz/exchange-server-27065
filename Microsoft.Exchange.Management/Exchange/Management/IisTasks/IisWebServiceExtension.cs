using System;

namespace Microsoft.Exchange.Management.IisTasks
{
	// Token: 0x02000410 RID: 1040
	public struct IisWebServiceExtension
	{
		// Token: 0x06002457 RID: 9303 RVA: 0x00090C4F File Offset: 0x0008EE4F
		internal IisWebServiceExtension(string executableName, string relativePath, bool allow, bool uiDeletable)
		{
			this.ExecutableName = executableName;
			this.RelativePath = relativePath;
			this.Allow = allow;
			this.UiDeletable = uiDeletable;
		}

		// Token: 0x04001CDB RID: 7387
		internal string ExecutableName;

		// Token: 0x04001CDC RID: 7388
		internal string RelativePath;

		// Token: 0x04001CDD RID: 7389
		internal bool Allow;

		// Token: 0x04001CDE RID: 7390
		internal bool UiDeletable;
	}
}
