using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200034C RID: 844
	internal sealed class MoveItemBatch : MoveCopyItemBatchCommandBase<MoveItemRequest, MoveItem>
	{
		// Token: 0x060017C8 RID: 6088 RVA: 0x0007F89B File Offset: 0x0007DA9B
		public MoveItemBatch(CallContext callContext, MoveItemRequest request) : base(callContext, request)
		{
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x0007F8A5 File Offset: 0x0007DAA5
		internal override TimeSpan? MaxExecutionTime
		{
			get
			{
				return new TimeSpan?(TimeSpan.FromMinutes(5.0));
			}
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0007F8BC File Offset: 0x0007DABC
		internal override ServiceResult<ItemType> Execute()
		{
			ExTraceGlobals.CopyItemCallTracer.TraceDebug((long)this.GetHashCode(), "MoveItemBatch.Execute called");
			if (base.Request.Ids != null && base.Request.Ids.Length > base.CurrentStep && base.LogItemId())
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "PreMoveId: ", string.Format("{0}:{1}", base.Request.Ids[base.CurrentStep].GetId(), base.Request.Ids[base.CurrentStep].GetChangeKey()));
			}
			if (base.CurrentStep == 0)
			{
				int num;
				if (base.TryMoveItemBatch(out num))
				{
					this.objectsChanged += num;
					base.LogCommandOptimizationToIIS(true);
				}
				else
				{
					base.FallbackCommand = new MoveItem(base.CallContext, base.Request);
					base.FallbackCommand.PreExecuteCommand();
				}
			}
			ServiceResult<ItemType> serviceResult;
			if (base.FallbackCommand != null)
			{
				base.FallbackCommand.CurrentStep = base.CurrentStep;
				serviceResult = base.FallbackCommand.Execute();
			}
			else
			{
				serviceResult = base.ServiceResults[base.CurrentStep];
			}
			if (serviceResult != null && serviceResult.Value != null && serviceResult.Value.ItemId != null && base.LogItemId())
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "PostMoveId: ", string.Format("{0}:{1}", serviceResult.Value.ItemId.Id, serviceResult.Value.ItemId.GetChangeKey()));
			}
			return serviceResult;
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0007FA2C File Offset: 0x0007DC2C
		internal override IExchangeWebMethodResponse GetResponse()
		{
			MoveItemResponse moveItemResponse = new MoveItemResponse();
			moveItemResponse.BuildForResults<ItemType>(base.Results);
			return moveItemResponse;
		}
	}
}
