using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002B9 RID: 697
	internal abstract class ItemBatchCommandBase<RequestType, SingleItemType> : MultiStepServiceCommand<RequestType, SingleItemType> where RequestType : BaseRequest
	{
		// Token: 0x060012BE RID: 4798 RVA: 0x0005B83F File Offset: 0x00059A3F
		public ItemBatchCommandBase(CallContext callContext, RequestType request) : base(callContext, request)
		{
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0005B878 File Offset: 0x00059A78
		protected bool VerifyItemsCanBeBatched(List<StoreId> itemIds, IdAndSession sourceIdAndSession, StoreSession destSession, ref bool suppressReadReceipts)
		{
			StoreObjectId asStoreObjectId = sourceIdAndSession.GetAsStoreObjectId();
			StoreObjectId firstFolderId = IdConverter.GetParentIdFromItemId(asStoreObjectId);
			List<StoreObjectId> list = new List<StoreObjectId>(itemIds.Count);
			foreach (StoreId id in itemIds)
			{
				list.Add(IdConverter.GetParentIdFromItemId(IdConverter.GetAsStoreObjectId(id)));
			}
			if (destSession != null && !sourceIdAndSession.Session.MailboxGuid.Equals(destSession.MailboxGuid))
			{
				ExTraceGlobals.CopyItemCallTracer.TraceDebug((long)this.GetHashCode(), "Batch operation cannot be optimized. Source and destination are different mailboxes.");
				return false;
			}
			if (sourceIdAndSession.Session is PublicFolderSession)
			{
				int num = (from fid in list
				where !fid.Equals(firstFolderId)
				select fid).Count<StoreObjectId>();
				if (num > 0)
				{
					ExTraceGlobals.CopyItemCallTracer.TraceDebug((long)this.GetHashCode(), "Batch operation cannot be optimized. All items are not in the same public folder.");
					return false;
				}
				return true;
			}
			else
			{
				MailboxSession mailboxSession = sourceIdAndSession.Session as MailboxSession;
				MailboxSession mailboxSession2 = destSession as MailboxSession;
				if (mailboxSession.MailboxOwner.MailboxInfo.IsArchive || (mailboxSession2 != null && mailboxSession2.MailboxOwner.MailboxInfo.IsArchive))
				{
					ExTraceGlobals.CopyItemCallTracer.TraceDebug((long)this.GetHashCode(), "Batch operation cannot be optimized. Source and/or destination is an archive mailbox.");
					return false;
				}
				StoreObjectId junkEmailFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.JunkEmail);
				if (junkEmailFolderId != null)
				{
					int num2 = list.Count((StoreObjectId fid) => fid.Equals(junkEmailFolderId));
					if (junkEmailFolderId.Equals(firstFolderId))
					{
						if (num2 == itemIds.Count)
						{
							suppressReadReceipts = true;
							return true;
						}
					}
					else if (num2 > 0)
					{
						ExTraceGlobals.CopyItemCallTracer.TraceDebug((long)this.GetHashCode(), "Batch operation cannot be optimized. Some of items are in the JunkEmail folder.");
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0005BA60 File Offset: 0x00059C60
		internal void LogCommandOptimizationToIIS(bool optimized)
		{
			if (base.CallContext != null && base.CallContext.HttpContext != null && base.CallContext.HttpContext.Response != null)
			{
				base.CallContext.ProtocolLog.AppendGenericInfo("Optimized", optimized.ToString());
			}
		}
	}
}
