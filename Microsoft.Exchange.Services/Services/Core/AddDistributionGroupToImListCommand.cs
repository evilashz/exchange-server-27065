using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000296 RID: 662
	internal class AddDistributionGroupToImListCommand : SingleStepServiceCommand<AddDistributionGroupToImListRequest, ImGroup>
	{
		// Token: 0x060011AA RID: 4522 RVA: 0x00055A7C File Offset: 0x00053C7C
		public AddDistributionGroupToImListCommand(CallContext callContext, AddDistributionGroupToImListRequest request) : base(callContext, request)
		{
			this.smtpAddress = request.SmtpAddress;
			this.displayName = request.DisplayName;
			this.session = callContext.SessionCache.GetMailboxIdentityMailboxSession();
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00055AAF File Offset: 0x00053CAF
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new AddDistributionGroupToImListResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00055AD8 File Offset: 0x00053CD8
		internal override ServiceResult<ImGroup> Execute()
		{
			if (!SmtpAddress.IsValidSmtpAddress(this.smtpAddress))
			{
				throw new InvalidImDistributionGroupSmtpAddressException();
			}
			RawImGroup rawImGroup = new AddDistributionGroupToImList(this.session, this.smtpAddress, this.displayName, new XSOFactory(), Global.UnifiedContactStoreConfiguration).Execute();
			ImGroup value = ImGroup.LoadFromRawImGroup(rawImGroup, this.session);
			return new ServiceResult<ImGroup>(value);
		}

		// Token: 0x04000CBF RID: 3263
		private readonly MailboxSession session;

		// Token: 0x04000CC0 RID: 3264
		private readonly string smtpAddress;

		// Token: 0x04000CC1 RID: 3265
		private readonly string displayName;
	}
}
