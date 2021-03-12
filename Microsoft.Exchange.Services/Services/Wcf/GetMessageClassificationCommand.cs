using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.SecureMail;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009A4 RID: 2468
	internal sealed class GetMessageClassificationCommand : SingleCmdletCommandBase<object, GetMessageClassificationResponse, GetMessageClassification, Microsoft.Exchange.Data.Directory.SystemConfiguration.MessageClassification>
	{
		// Token: 0x06004654 RID: 18004 RVA: 0x000F8486 File Offset: 0x000F6686
		public GetMessageClassificationCommand(CallContext callContext) : base(callContext, null, "Get-MessageClassification", ScopeLocation.RecipientRead)
		{
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x000F84CC File Offset: 0x000F66CC
		protected override void PopulateResponseData(GetMessageClassificationResponse response)
		{
			IEnumerable<Microsoft.Exchange.Data.Directory.SystemConfiguration.MessageClassification> allResults = this.cmdletRunner.TaskWrapper.AllResults;
			IEnumerable<Microsoft.Exchange.Services.Wcf.Types.MessageClassification> source = from e in allResults
			where e.PermissionMenuVisible
			select new Microsoft.Exchange.Services.Wcf.Types.MessageClassification
			{
				DisplayName = e.DisplayName,
				Guid = e.Guid
			};
			response.MessageClassificationCollection.MessageClassifications = source.ToArray<Microsoft.Exchange.Services.Wcf.Types.MessageClassification>();
		}

		// Token: 0x06004656 RID: 18006 RVA: 0x000F8541 File Offset: 0x000F6741
		protected override PSLocalTask<GetMessageClassification, Microsoft.Exchange.Data.Directory.SystemConfiguration.MessageClassification> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateGetMessageClassificationTask(base.CallContext.AccessingPrincipal);
		}
	}
}
