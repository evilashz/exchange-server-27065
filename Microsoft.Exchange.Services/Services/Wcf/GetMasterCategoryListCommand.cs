using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200091E RID: 2334
	internal class GetMasterCategoryListCommand : ServiceCommand<MasterCategoryListActionResponse>
	{
		// Token: 0x0600439D RID: 17309 RVA: 0x000E53F3 File Offset: 0x000E35F3
		public GetMasterCategoryListCommand(CallContext context, GetMasterCategoryListRequest request) : base(context)
		{
			this.request = request;
			this.request.ValidateRequest();
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x000E5410 File Offset: 0x000E3610
		protected override MasterCategoryListActionResponse InternalExecute()
		{
			MailboxSession mailboxSession;
			try
			{
				mailboxSession = this.GetMailboxSession(this.request.SmtpAddress.ToString());
			}
			catch (NonExistentMailboxException)
			{
				ExTraceGlobals.MasterCategoryListCallTracer.TraceDebug<SmtpAddress>(0L, "Not able to access mailbox to retrieve the MasterCategoryList for {0}", this.request.SmtpAddress);
				return new MasterCategoryListActionResponse(MasterCategoryListActionError.MasterCategoryListErrorUnableToAccessMclOwnerMailbox);
			}
			if (mailboxSession == null)
			{
				ExTraceGlobals.MasterCategoryListCallTracer.TraceDebug<SmtpAddress>(0L, "Not able to access mailbox to retrieve the MasterCategoryList for {0}", this.request.SmtpAddress);
				return new MasterCategoryListActionResponse(MasterCategoryListActionError.MasterCategoryListErrorUnableToAccessMclOwnerMailbox);
			}
			MasterCategoryListType masterCategoryListType = new GetMasterCategoryList(mailboxSession).Execute();
			if (masterCategoryListType == null)
			{
				return new MasterCategoryListActionResponse(MasterCategoryListActionError.MasterCategoryListErrorUnableToLoad);
			}
			return new MasterCategoryListActionResponse(MasterCategoryListHelper.GetMasterList(masterCategoryListType));
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x000E54C0 File Offset: 0x000E36C0
		protected MailboxSession GetMailboxSession(string smtpAddress)
		{
			return base.CallContext.SessionCache.GetMailboxSessionBySmtpAddress(smtpAddress);
		}

		// Token: 0x0400277D RID: 10109
		private GetMasterCategoryListRequest request;
	}
}
