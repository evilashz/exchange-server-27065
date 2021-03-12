using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000059 RID: 89
	internal class SetReadFlagsOperation : MessageListBulkOperation
	{
		// Token: 0x0600023C RID: 572 RVA: 0x00010768 File Offset: 0x0000E968
		public SetReadFlagsOperation(MapiFolder folder, IList<ExchangeId> messageIds, SetReadFlagFlags flags, IList<ExchangeId> readCns, int chunkSize) : base(folder, messageIds, chunkSize)
		{
			this.flags = flags;
			this.readCns = readCns;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00010783 File Offset: 0x0000E983
		public SetReadFlagsOperation(MapiFolder folder, IList<ExchangeId> messageIds, SetReadFlagFlags flags, int chunkSize) : this(folder, messageIds, flags, null, chunkSize)
		{
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00010791 File Offset: 0x0000E991
		public SetReadFlagsOperation(MapiFolder folder, IList<ExchangeId> messageIds, SetReadFlagFlags flags, IList<ExchangeId> readCns) : this(folder, messageIds, flags, readCns, 500)
		{
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000107A3 File Offset: 0x0000E9A3
		public SetReadFlagsOperation(MapiFolder folder, IList<ExchangeId> messageIds, SetReadFlagFlags flags) : this(folder, messageIds, flags, 500)
		{
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000240 RID: 576 RVA: 0x000107B3 File Offset: 0x0000E9B3
		public int MessagesProcessedForTest
		{
			get
			{
				return this.messagesProcessed;
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000107BB File Offset: 0x0000E9BB
		protected override bool ProcessStart(MapiContext context, out int progressCount, ref ErrorCode error)
		{
			progressCount = 0;
			return true;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000107C4 File Offset: 0x0000E9C4
		protected override bool ProcessMessages(MapiContext context, MapiFolder folder, IList<ExchangeId> midsToProcess, out int progressCount, ref bool incomplete, ref ErrorCode error)
		{
			this.messagesProcessed += midsToProcess.Count;
			return BulkOperation.SetReadFlags(context, folder, this.flags, midsToProcess, BulkErrorAction.Incomplete, BulkErrorAction.Incomplete, this.readCns, out progressCount, ref incomplete, ref error);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00010800 File Offset: 0x0000EA00
		protected override Restriction GetFilterRestriction(MapiContext context)
		{
			Restriction restriction = null;
			if ((byte)(this.flags & SetReadFlagFlags.GenerateReceiptOnly) == 0)
			{
				if ((byte)(this.flags & (SetReadFlagFlags.ClearReadNotificationPending | SetReadFlagFlags.ClearNonReadNotificationPending)) == 0)
				{
					if ((byte)(this.flags & SetReadFlagFlags.ClearReadFlag) != 0)
					{
						restriction = new RestrictionProperty(PropTag.Message.Read, RelationOperator.Equal, true);
					}
					else
					{
						restriction = new RestrictionProperty(PropTag.Message.Read, RelationOperator.Equal, false);
						if ((byte)(this.flags & SetReadFlagFlags.SuppressReceipt) == 0)
						{
							restriction = new RestrictionOR(new Restriction[]
							{
								restriction,
								new RestrictionAND(new Restriction[]
								{
									new RestrictionBitmask(PropTag.Message.MailFlags, 6L, BitmaskOperation.NotEqualToZero),
									new RestrictionBitmask(PropTag.Message.MailFlags, 32L, BitmaskOperation.EqualToZero)
								})
							});
						}
					}
					if ((byte)(this.flags & SetReadFlagFlags.SuppressReceipt) != 0 && (context.ClientType == ClientType.MoMT || context.ClientType == ClientType.User || context.ClientType == ClientType.RpcHttp))
					{
						restriction = new RestrictionOR(new Restriction[]
						{
							restriction,
							new RestrictionAND(new Restriction[]
							{
								new RestrictionBitmask(PropTag.Message.MailFlags, 6L, BitmaskOperation.NotEqualToZero),
								new RestrictionBitmask(PropTag.Message.MailFlags, 32L, BitmaskOperation.EqualToZero)
							})
						});
					}
				}
				else
				{
					short num = 0;
					if ((byte)(this.flags & SetReadFlagFlags.ClearReadNotificationPending) != 0)
					{
						num |= 2;
					}
					if ((byte)(this.flags & SetReadFlagFlags.ClearNonReadNotificationPending) != 0)
					{
						num |= 4;
					}
					restriction = new RestrictionBitmask(PropTag.Message.MailFlags, (long)num, BitmaskOperation.NotEqualToZero);
				}
			}
			return restriction;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00010957 File Offset: 0x0000EB57
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SetReadFlagsOperation>(this);
		}

		// Token: 0x0400018E RID: 398
		private const int ReasonableSetReadChunkSize = 500;

		// Token: 0x0400018F RID: 399
		private SetReadFlagFlags flags;

		// Token: 0x04000190 RID: 400
		private IList<ExchangeId> readCns;

		// Token: 0x04000191 RID: 401
		private int messagesProcessed;
	}
}
