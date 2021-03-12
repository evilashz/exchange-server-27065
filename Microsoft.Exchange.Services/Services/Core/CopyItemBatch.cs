using System;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002BB RID: 699
	internal sealed class CopyItemBatch : MoveCopyItemBatchCommandBase<CopyItemRequest, CopyItem>
	{
		// Token: 0x060012E6 RID: 4838 RVA: 0x0005C285 File Offset: 0x0005A485
		public CopyItemBatch(CallContext callContext, CopyItemRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x0005C290 File Offset: 0x0005A490
		internal override ServiceResult<ItemType> Execute()
		{
			ExTraceGlobals.CopyItemCallTracer.TraceDebug((long)this.GetHashCode(), "CopyItemBatch.Execute called");
			if (base.CurrentStep == 0)
			{
				int num;
				if (base.TryCopyItemBatch(out num))
				{
					this.objectsChanged += num;
					base.LogCommandOptimizationToIIS(true);
				}
				else
				{
					base.FallbackCommand = new CopyItem(base.CallContext, base.Request);
					base.FallbackCommand.PreExecuteCommand();
				}
			}
			if (base.FallbackCommand != null)
			{
				base.FallbackCommand.CurrentStep = base.CurrentStep;
				return base.FallbackCommand.Execute();
			}
			return base.ServiceResults[base.CurrentStep];
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x0005C330 File Offset: 0x0005A530
		internal override IExchangeWebMethodResponse GetResponse()
		{
			CopyItemResponse copyItemResponse = new CopyItemResponse();
			copyItemResponse.BuildForResults<ItemType>(base.Results);
			return copyItemResponse;
		}
	}
}
