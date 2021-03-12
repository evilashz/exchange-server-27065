using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Aggregation;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009A6 RID: 2470
	internal sealed class GetSendAddressCommand : SingleCmdletCommandBase<object, GetSendAddressResponse, GetSendAddress, SendAddress>
	{
		// Token: 0x0600465D RID: 18013 RVA: 0x000F8716 File Offset: 0x000F6916
		public GetSendAddressCommand(CallContext callContext) : base(callContext, null, "Get-SendAddress", ScopeLocation.RecipientRead)
		{
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x000F8728 File Offset: 0x000F6928
		protected override void PopulateTaskParameters()
		{
			GetSendAddress task = this.cmdletRunner.TaskWrapper.Task;
			this.cmdletRunner.SetTaskParameter("Mailbox", task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x000F8798 File Offset: 0x000F6998
		protected override void PopulateResponseData(GetSendAddressResponse response)
		{
			IEnumerable<SendAddress> allResults = this.cmdletRunner.TaskWrapper.AllResults;
			response.SendAddressCollection.SendAddresses = (from r in allResults
			select new SendAddressData
			{
				AddressId = r.AddressId,
				DisplayName = r.DisplayName
			}).ToArray<SendAddressData>();
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x000F87E9 File Offset: 0x000F69E9
		protected override PSLocalTask<GetSendAddress, SendAddress> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateGetSendAddressTask(base.CallContext.AccessingPrincipal);
		}
	}
}
