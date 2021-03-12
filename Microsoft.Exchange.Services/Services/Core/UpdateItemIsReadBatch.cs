using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000399 RID: 921
	internal sealed class UpdateItemIsReadBatch : ItemBatchCommandBase<UpdateItemRequest, UpdateItemResponseWrapper>
	{
		// Token: 0x060019ED RID: 6637 RVA: 0x0009515F File Offset: 0x0009335F
		public UpdateItemIsReadBatch(CallContext callContext, UpdateItemRequest request) : base(callContext, request)
		{
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060019EE RID: 6638 RVA: 0x00095169 File Offset: 0x00093369
		internal override int StepCount
		{
			get
			{
				if (base.Request.ItemChanges != null)
				{
					return base.Request.ItemChanges.Length;
				}
				return 0;
			}
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x00095187 File Offset: 0x00093387
		internal override void PreExecuteCommand()
		{
			this.updateItemCommand = new UpdateItem(base.CallContext, base.Request);
			this.updateItemCommand.PreExecuteCommand();
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x000951AC File Offset: 0x000933AC
		internal override ServiceResult<UpdateItemResponseWrapper> Execute()
		{
			ExTraceGlobals.UpdateItemCallTracer.TraceDebug((long)this.GetHashCode(), "UpdateItemIsReadBatch.Execute called");
			if (base.CurrentStep == 0 && this.TryToUpdateIsReadFlags())
			{
				base.LogCommandOptimizationToIIS(true);
			}
			this.updateItemCommand.CurrentStep = base.CurrentStep;
			return this.updateItemCommand.Execute();
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x00095202 File Offset: 0x00093402
		internal override void PostExecuteCommand()
		{
			this.updateItemCommand.PostExecuteCommand();
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x00095210 File Offset: 0x00093410
		private bool TryToUpdateIsReadFlags()
		{
			bool suppressReadReceipts = base.Request.SuppressReadReceipts;
			List<StoreId> list = new List<StoreId>(base.Request.MarkAsReadItemChanges.Count);
			for (int i = 0; i < base.Request.MarkAsReadItemChanges.Count; i++)
			{
				list.Add(base.Request.MarkAsReadItemChanges[i].Value);
			}
			List<StoreId> list2 = new List<StoreId>(base.Request.MarkAsUnreadItemChanges.Count);
			for (int j = 0; j < base.Request.MarkAsUnreadItemChanges.Count; j++)
			{
				list2.Add(base.Request.MarkAsUnreadItemChanges[j].Value);
			}
			List<StoreId> list3 = new List<StoreId>(list);
			list3.AddRange(list2);
			BaseItemId baseItemId = (base.Request.MarkAsReadItemChanges.Count > 0) ? base.Request.MarkAsReadItemChanges[0].Key.ItemId : base.Request.MarkAsUnreadItemChanges[0].Key.ItemId;
			IdAndSession idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(baseItemId);
			if (base.VerifyItemsCanBeBatched(list3, idAndSession, null, ref suppressReadReceipts))
			{
				if (list.Count > 0)
				{
					try
					{
						idAndSession.Session.MarkAsRead(suppressReadReceipts, list.ToArray(), true, false);
						foreach (KeyValuePair<ItemChange, StoreId> keyValuePair in base.Request.MarkAsReadItemChanges)
						{
							keyValuePair.Key.ChangesAlreadyProcessed = true;
						}
					}
					catch (PartialCompletionException arg)
					{
						ExTraceGlobals.UpdateItemCallTracer.TraceWarning<PartialCompletionException>((long)this.GetHashCode(), "UpdateItemIsReadBatch.TryToUpdateIsReadFlags hit PartialCompletionException when trying to MarkAsRead, ex: {0}", arg);
					}
				}
				if (list2.Count > 0)
				{
					try
					{
						idAndSession.Session.MarkAsUnread(suppressReadReceipts, list2.ToArray(), true);
						foreach (KeyValuePair<ItemChange, StoreId> keyValuePair2 in base.Request.MarkAsUnreadItemChanges)
						{
							keyValuePair2.Key.ChangesAlreadyProcessed = true;
						}
					}
					catch (PartialCompletionException arg2)
					{
						ExTraceGlobals.UpdateItemCallTracer.TraceWarning<PartialCompletionException>((long)this.GetHashCode(), "UpdateItemIsReadBatch.TryToUpdateIsReadFlags hit PartialCompletionException when trying to MarkAsUnread, ex: {0}", arg2);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x0009548C File Offset: 0x0009368C
		internal override IExchangeWebMethodResponse GetResponse()
		{
			UpdateItemResponse updateItemResponse = new UpdateItemResponse();
			updateItemResponse.BuildForUpdateItemResults(base.Results);
			return updateItemResponse;
		}

		// Token: 0x04001145 RID: 4421
		private UpdateItem updateItemCommand;
	}
}
