using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200091D RID: 2333
	internal sealed class GetMasterCategoryList
	{
		// Token: 0x0600439A RID: 17306 RVA: 0x000E5349 File Offset: 0x000E3549
		public GetMasterCategoryList(MailboxSession mclOwnerMailboxSession)
		{
			this.mclOwnerMailboxSession = mclOwnerMailboxSession;
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x000E5358 File Offset: 0x000E3558
		public MasterCategoryListType Execute()
		{
			MasterCategoryListType result = null;
			if (!this.CanAccessCalendarFolder(this.mclOwnerMailboxSession))
			{
				ExTraceGlobals.MasterCategoryListCallTracer.TraceDebug<string>(0L, "Not able to access calendar folder to retrieve the MasterCategoryListType for {0}", this.mclOwnerMailboxSession.DisplayAddress);
			}
			else
			{
				try
				{
					result = MasterCategoryListHelper.GetMasterCategoryListType(this.mclOwnerMailboxSession, this.mclOwnerMailboxSession.Culture);
				}
				catch (AccessDeniedException ex)
				{
					ExTraceGlobals.MasterCategoryListCallTracer.TraceDebug<string, string>(0L, "Not able to access calendar folder to retrieve the MasterCategoryListType for {0}. Exception:{1}", this.mclOwnerMailboxSession.DisplayAddress, ex.ToString());
				}
			}
			return result;
		}

		// Token: 0x0600439C RID: 17308 RVA: 0x000E53E4 File Offset: 0x000E35E4
		internal bool CanAccessCalendarFolder(MailboxSession mailboxSession)
		{
			return mailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar) != null;
		}

		// Token: 0x0400277C RID: 10108
		private MailboxSession mclOwnerMailboxSession;
	}
}
