using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000021 RID: 33
	internal class AsrExtensionSearchResult : AsrSearchResult
	{
		// Token: 0x0600019A RID: 410 RVA: 0x00007C6D File Offset: 0x00005E6D
		internal AsrExtensionSearchResult(string extension)
		{
			this.selectedPhoneNumber = PhoneNumber.CreateExtension(extension);
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00007C81 File Offset: 0x00005E81
		public PhoneNumber Extension
		{
			get
			{
				return this.selectedPhoneNumber;
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00007C8C File Offset: 0x00005E8C
		internal override void SetManagerVariables(ActivityManager manager, BaseUMCallSession vo)
		{
			manager.WriteVariable("resultType", ResultType.UserExtension);
			manager.WriteVariable("resultTypeString", ResultType.UserExtension.ToString());
			manager.WriteVariable("selectedUser", null);
			manager.WriteVariable("directorySearchResult", null);
			manager.WriteVariable("selectedPhoneNumber", this.selectedPhoneNumber);
		}

		// Token: 0x0400007E RID: 126
		private PhoneNumber selectedPhoneNumber;
	}
}
