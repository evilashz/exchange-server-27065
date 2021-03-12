using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.MapiTasks
{
	// Token: 0x02000496 RID: 1174
	[Serializable]
	public class UpdatePublicFolderMailboxResult
	{
		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x060029D3 RID: 10707 RVA: 0x000A63D8 File Offset: 0x000A45D8
		// (set) Token: 0x060029D4 RID: 10708 RVA: 0x000A63E0 File Offset: 0x000A45E0
		public LocalizedString Message { get; set; }

		// Token: 0x060029D5 RID: 10709 RVA: 0x000A63E9 File Offset: 0x000A45E9
		public UpdatePublicFolderMailboxResult(LocalizedString msg)
		{
			this.Message = msg;
		}
	}
}
