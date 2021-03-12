using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000058 RID: 88
	internal abstract class MessageListBulkOperation : BulkOperation
	{
		// Token: 0x06000232 RID: 562 RVA: 0x000103EB File Offset: 0x0000E5EB
		public MessageListBulkOperation(MapiFolder folder, IList<ExchangeId> messageIds, int chunkSize) : base(chunkSize)
		{
			this.folder = folder;
			this.messageIds = messageIds;
			this.firstChunk = true;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00010409 File Offset: 0x0000E609
		public MapiFolder Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00010411 File Offset: 0x0000E611
		public IList<ExchangeId> MessageIds
		{
			get
			{
				return this.messageIds;
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0001041C File Offset: 0x0000E61C
		public override bool DoChunk(MapiContext context, out bool progress, out bool incomplete, out ErrorCode error)
		{
			progress = false;
			incomplete = false;
			error = ErrorCode.NoError;
			int num = 0;
			bool flag = false;
			if (this.firstChunk)
			{
				if (!this.CheckSourceFolder(context))
				{
					error = ErrorCode.CreateObjectDeleted((LID)37400U);
					return true;
				}
				int num2;
				if (!this.ProcessStart(context, out num2, ref error))
				{
					flag = true;
				}
				else
				{
					if (num2 != 0)
					{
						num += num2;
						progress = true;
					}
					this.firstChunk = false;
				}
			}
			else if (!this.CheckSourceFolder(context))
			{
				incomplete = true;
				flag = true;
			}
			while (!flag && num < base.ChunkSize)
			{
				int num3 = Math.Min(base.ChunkSize - num, (base.ChunkSize + 1) / 2);
				IList<ExchangeId> list = null;
				if (this.messageIds == null || this.messageIds.Count == 0)
				{
					if (this.messageView == null)
					{
						this.messageView = new MapiViewMessage();
						this.messageView.Configure(context, this.folder.Logon, this.folder, ViewMessageConfigureFlags.NoNotifications | ViewMessageConfigureFlags.DoNotUseLazyIndex);
						this.messageView.SetColumns(context, BulkOperation.ColumnsToFetchMid, MapiViewSetColumnsFlag.NoColumnValidation);
						Restriction filterRestriction = this.GetFilterRestriction(context);
						if (filterRestriction != null)
						{
							this.messageView.Restrict(context, 0, filterRestriction);
						}
					}
					IList<Properties> list2 = this.messageView.QueryRowsBatch(context, num3, QueryRowsFlags.None);
					if (list2 != null && list2.Count != 0)
					{
						if (this.tempMidsList == null)
						{
							this.tempMidsList = new List<ExchangeId>(list2.Count);
						}
						else
						{
							this.tempMidsList.Clear();
						}
						for (int i = 0; i < list2.Count; i++)
						{
							this.tempMidsList.Add(ExchangeId.CreateFrom26ByteArray(context, this.folder.Logon.StoreMailbox.ReplidGuidMap, (byte[])list2[i][0].Value));
						}
						list = this.tempMidsList;
					}
					else
					{
						flag = true;
					}
				}
				else if (this.currentIndex == 0 && this.messageIds.Count < num3)
				{
					list = this.messageIds;
					this.currentIndex += list.Count;
					flag = true;
				}
				else if (this.currentIndex == this.messageIds.Count)
				{
					flag = true;
				}
				else
				{
					num3 = Math.Min(this.messageIds.Count - this.currentIndex, num3);
					if (this.tempMidsList == null)
					{
						this.tempMidsList = new List<ExchangeId>(num3);
					}
					else
					{
						this.tempMidsList.Clear();
					}
					for (int j = 0; j < num3; j++)
					{
						this.tempMidsList.Add(this.messageIds[this.currentIndex++]);
					}
					list = this.tempMidsList;
					if (this.currentIndex == this.messageIds.Count)
					{
						flag = true;
					}
				}
				if (list != null && list.Count != 0)
				{
					int num2;
					if (!this.ProcessMessages(context, this.folder, list, out num2, ref incomplete, ref error))
					{
						flag = true;
					}
					else if (num2 != 0)
					{
						num += num2;
						progress = true;
					}
				}
			}
			if (flag)
			{
				this.ProcessEnd(context, incomplete, error);
			}
			return flag;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00010730 File Offset: 0x0000E930
		protected virtual bool CheckSourceFolder(MapiContext context)
		{
			return this.folder.CheckAlive(context);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0001073E File Offset: 0x0000E93E
		protected virtual bool ProcessStart(MapiContext context, out int progressCount, ref ErrorCode error)
		{
			progressCount = 0;
			return true;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00010744 File Offset: 0x0000E944
		protected virtual void ProcessEnd(MapiContext context, bool incomplete, ErrorCode error)
		{
		}

		// Token: 0x06000239 RID: 569
		protected abstract bool ProcessMessages(MapiContext context, MapiFolder folder, IList<ExchangeId> midsToProcess, out int progressCount, ref bool incomplete, ref ErrorCode error);

		// Token: 0x0600023A RID: 570 RVA: 0x00010746 File Offset: 0x0000E946
		protected virtual Restriction GetFilterRestriction(MapiContext context)
		{
			return null;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00010749 File Offset: 0x0000E949
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.messageView != null)
			{
				this.messageView.Dispose();
				this.messageView = null;
			}
		}

		// Token: 0x04000188 RID: 392
		private readonly MapiFolder folder;

		// Token: 0x04000189 RID: 393
		private readonly IList<ExchangeId> messageIds;

		// Token: 0x0400018A RID: 394
		private bool firstChunk;

		// Token: 0x0400018B RID: 395
		private int currentIndex;

		// Token: 0x0400018C RID: 396
		private List<ExchangeId> tempMidsList;

		// Token: 0x0400018D RID: 397
		private MapiViewMessage messageView;
	}
}
