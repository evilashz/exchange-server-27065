using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009A3 RID: 2467
	internal sealed class GetMessageCategoryCommand : SingleCmdletCommandBase<object, GetMessageCategoryResponse, GetMessageCategory, Microsoft.Exchange.Data.Storage.Management.MessageCategory>
	{
		// Token: 0x06004650 RID: 18000 RVA: 0x000F83E0 File Offset: 0x000F65E0
		public GetMessageCategoryCommand(CallContext callContext) : base(callContext, null, "Get-MessageCategory", ScopeLocation.RecipientRead)
		{
		}

		// Token: 0x06004651 RID: 18001 RVA: 0x000F841C File Offset: 0x000F661C
		protected override void PopulateResponseData(GetMessageCategoryResponse response)
		{
			PSLocalTask<GetMessageCategory, Microsoft.Exchange.Data.Storage.Management.MessageCategory> taskWrapper = this.cmdletRunner.TaskWrapper;
			IEnumerable<Microsoft.Exchange.Services.Wcf.Types.MessageCategory> source = from t in taskWrapper.AllResults
			select new Microsoft.Exchange.Services.Wcf.Types.MessageCategory
			{
				Color = t.Color,
				Name = t.Name
			};
			response.MessageCategoryCollection.MessageCategories = source.ToArray<Microsoft.Exchange.Services.Wcf.Types.MessageCategory>();
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x000F846F File Offset: 0x000F666F
		protected override PSLocalTask<GetMessageCategory, Microsoft.Exchange.Data.Storage.Management.MessageCategory> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateGetMessageCategoryTask(base.CallContext.AccessingPrincipal);
		}
	}
}
