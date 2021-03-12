using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000090 RID: 144
	public class TableModifiedNotificationEvent : LogicalModelNotificationEvent
	{
		// Token: 0x06000520 RID: 1312 RVA: 0x00025314 File Offset: 0x00023514
		public TableModifiedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, TableEventType tableEventType, ExchangeId fid, ExchangeId mid, int inst, ExchangeId previousFid, ExchangeId previousMid, int previousInst, Properties row) : base(database, mailboxNumber, EventType.TableModified, userIdentity, clientType, eventFlags, null)
		{
			Statistics.NotificationTypes.TableModified.Bump();
			switch (tableEventType)
			{
			case TableEventType.Changed:
				Statistics.TableNotificationTypes.Changed.Bump();
				break;
			case TableEventType.Error:
				Statistics.TableNotificationTypes.Error.Bump();
				break;
			case TableEventType.RowAdded:
				Statistics.TableNotificationTypes.RowAdded.Bump();
				break;
			case TableEventType.RowDeleted:
				Statistics.TableNotificationTypes.RowDeleted.Bump();
				break;
			case TableEventType.RowModified:
				Statistics.TableNotificationTypes.RowModified.Bump();
				break;
			case TableEventType.SortDone:
				Statistics.TableNotificationTypes.SortDone.Bump();
				break;
			case TableEventType.RestrictDone:
				Statistics.TableNotificationTypes.RestrictDone.Bump();
				break;
			case TableEventType.SetcolDone:
				Statistics.TableNotificationTypes.SetcolDone.Bump();
				break;
			case TableEventType.Reload:
				Statistics.TableNotificationTypes.Reload.Bump();
				break;
			}
			this.tableEventType = tableEventType;
			this.fid = fid;
			this.mid = mid;
			this.inst = inst;
			this.previousFid = previousFid;
			this.previousMid = previousMid;
			this.previousInst = previousInst;
			this.row = row;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00025421 File Offset: 0x00023621
		public TableEventType TableEventType
		{
			get
			{
				return this.tableEventType;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00025429 File Offset: 0x00023629
		public ExchangeId Fid
		{
			get
			{
				return this.fid;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00025431 File Offset: 0x00023631
		public ExchangeId Mid
		{
			get
			{
				return this.mid;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00025439 File Offset: 0x00023639
		public int Inst
		{
			get
			{
				return this.inst;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00025441 File Offset: 0x00023641
		public ExchangeId PreviousFid
		{
			get
			{
				return this.previousFid;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00025449 File Offset: 0x00023649
		public ExchangeId PreviousMid
		{
			get
			{
				return this.previousMid;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00025451 File Offset: 0x00023651
		public int PreviousInst
		{
			get
			{
				return this.previousInst;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00025459 File Offset: 0x00023659
		public Properties Row
		{
			get
			{
				return this.row;
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00025464 File Offset: 0x00023664
		public override NotificationEvent.RedundancyStatus GetRedundancyStatus(NotificationEvent oldNev)
		{
			TableModifiedNotificationEvent tableModifiedNotificationEvent = oldNev as TableModifiedNotificationEvent;
			if (tableModifiedNotificationEvent != null)
			{
				if (this.TableEventType == TableEventType.Changed)
				{
					Statistics.MiscelaneousNotifications.NewTableChangedWashesAnyOld.Bump();
					return NotificationEvent.RedundancyStatus.FlagDropOld;
				}
				if (tableModifiedNotificationEvent.TableEventType == TableEventType.Changed)
				{
					Statistics.MiscelaneousNotifications.OldTableChangedWashesAnyNew.Bump();
					return NotificationEvent.RedundancyStatus.DropNewAndStop;
				}
				if (this.TableEventType == TableEventType.RowModified)
				{
					if (tableModifiedNotificationEvent.TableEventType == TableEventType.RowAdded)
					{
						if (this.Mid == tableModifiedNotificationEvent.Mid && this.Fid == tableModifiedNotificationEvent.Fid && this.Inst == tableModifiedNotificationEvent.Inst)
						{
							Statistics.MiscelaneousNotifications.NewRowModifiedWashesOldRowAdded.Bump();
							return NotificationEvent.RedundancyStatus.MergeReplaceOldAndStop;
						}
						if ((this.Mid == tableModifiedNotificationEvent.PreviousMid && this.Fid == tableModifiedNotificationEvent.PreviousFid && this.Inst == tableModifiedNotificationEvent.PreviousInst) || (this.PreviousMid == tableModifiedNotificationEvent.Mid && this.PreviousFid == tableModifiedNotificationEvent.Fid && this.PreviousInst == tableModifiedNotificationEvent.Inst))
						{
							return NotificationEvent.RedundancyStatus.FlagStopSearch;
						}
					}
					else if (tableModifiedNotificationEvent.TableEventType == TableEventType.RowModified)
					{
						if (this.Mid == tableModifiedNotificationEvent.Mid && this.Fid == tableModifiedNotificationEvent.Fid && this.Inst == tableModifiedNotificationEvent.Inst)
						{
							Statistics.MiscelaneousNotifications.NewRowModifiedWashesOldRowModified.Bump();
							return NotificationEvent.RedundancyStatus.ReplaceOldAndStop;
						}
						if ((this.Mid == tableModifiedNotificationEvent.PreviousMid && this.Fid == tableModifiedNotificationEvent.PreviousFid && this.Inst == tableModifiedNotificationEvent.PreviousInst) || (this.PreviousMid == tableModifiedNotificationEvent.Mid && this.PreviousFid == tableModifiedNotificationEvent.Fid && this.PreviousInst == tableModifiedNotificationEvent.Inst))
						{
							return NotificationEvent.RedundancyStatus.FlagStopSearch;
						}
					}
				}
				else
				{
					if (this.TableEventType != TableEventType.RowDeleted)
					{
						return NotificationEvent.RedundancyStatus.FlagStopSearch;
					}
					if (tableModifiedNotificationEvent.TableEventType == TableEventType.RowAdded)
					{
						if (this.Mid == tableModifiedNotificationEvent.Mid && this.Fid == tableModifiedNotificationEvent.Fid && this.Inst == tableModifiedNotificationEvent.Inst)
						{
							Statistics.MiscelaneousNotifications.NewRowDeletedWashesOldRowAdded.Bump();
							return NotificationEvent.RedundancyStatus.DropBothAndStop;
						}
						if (this.Mid == tableModifiedNotificationEvent.PreviousMid && this.Fid == tableModifiedNotificationEvent.PreviousFid && this.Inst == tableModifiedNotificationEvent.PreviousInst)
						{
							return NotificationEvent.RedundancyStatus.FlagStopSearch;
						}
					}
					else if (tableModifiedNotificationEvent.TableEventType == TableEventType.RowModified)
					{
						if (this.Mid == tableModifiedNotificationEvent.Mid && this.Fid == tableModifiedNotificationEvent.Fid && this.Inst == tableModifiedNotificationEvent.Inst)
						{
							Statistics.MiscelaneousNotifications.NewRowDeletedWashesOldRowModified.Bump();
							return NotificationEvent.RedundancyStatus.ReplaceOldAndStop;
						}
						if (this.Mid == tableModifiedNotificationEvent.PreviousMid && this.Fid == tableModifiedNotificationEvent.PreviousFid && this.Inst == tableModifiedNotificationEvent.PreviousInst)
						{
							return NotificationEvent.RedundancyStatus.FlagStopSearch;
						}
					}
				}
			}
			return NotificationEvent.RedundancyStatus.Continue;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00025778 File Offset: 0x00023978
		public override NotificationEvent MergeWithOldEvent(NotificationEvent oldNev)
		{
			return new TableModifiedNotificationEvent(base.Database, base.MailboxNumber, base.UserIdentity, base.ClientType, base.EventFlags, TableEventType.RowAdded, this.Fid, this.Mid, this.Inst, this.PreviousFid, this.PreviousMid, this.PreviousInst, this.Row);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000257D3 File Offset: 0x000239D3
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("TableModifiedNotificationEvent");
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000257E4 File Offset: 0x000239E4
		protected override void AppendFields(StringBuilder sb)
		{
			base.AppendFields(sb);
			sb.Append(" TableEventType:[");
			sb.Append(this.TableEventType);
			sb.Append("] Fid:[");
			sb.Append(this.Fid);
			sb.Append("] Mid:[");
			sb.Append(this.Mid);
			sb.Append("] Inst:[");
			sb.Append(this.Inst);
			sb.Append("] PreviousFid:[");
			sb.Append(this.PreviousFid);
			sb.Append("] PreviousMid:[");
			sb.Append(this.PreviousMid);
			sb.Append("] PreviousInst:[");
			sb.Append(this.PreviousInst);
			sb.Append("] Row:[");
			sb.Append(this.Row);
			sb.Append("]");
		}

		// Token: 0x0400030C RID: 780
		private TableEventType tableEventType;

		// Token: 0x0400030D RID: 781
		private ExchangeId fid;

		// Token: 0x0400030E RID: 782
		private ExchangeId mid;

		// Token: 0x0400030F RID: 783
		private int inst;

		// Token: 0x04000310 RID: 784
		private ExchangeId previousFid;

		// Token: 0x04000311 RID: 785
		private ExchangeId previousMid;

		// Token: 0x04000312 RID: 786
		private int previousInst;

		// Token: 0x04000313 RID: 787
		private Properties row;
	}
}
