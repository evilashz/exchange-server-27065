using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x020001F9 RID: 505
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class Pop3ResultData
	{
		// Token: 0x170005F3 RID: 1523
		// (set) Token: 0x060010C6 RID: 4294 RVA: 0x00036A5C File Offset: 0x00034C5C
		internal ExDateTime FirstReceivedDate
		{
			set
			{
				this.firstReceivedDate = new ExDateTime?(value);
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (set) Token: 0x060010C7 RID: 4295 RVA: 0x00036A6A File Offset: 0x00034C6A
		internal ExDateTime LastReceivedDate
		{
			set
			{
				this.lastReceivedDate = new ExDateTime?(value);
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x00036A78 File Offset: 0x00034C78
		internal bool ReceivedDateDescends
		{
			get
			{
				return this.firstReceivedDate == null || this.lastReceivedDate == null || (this.firstReceivedDate == ExDateTime.MinValue || this.lastReceivedDate == ExDateTime.MinValue) || this.lastReceivedDate <= this.firstReceivedDate;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x00036B29 File Offset: 0x00034D29
		// (set) Token: 0x060010CA RID: 4298 RVA: 0x00036B31 File Offset: 0x00034D31
		internal int EmailDropCount
		{
			get
			{
				return this.emailDropCount;
			}
			set
			{
				this.emailDropCount = value;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x00036B3A File Offset: 0x00034D3A
		// (set) Token: 0x060010CC RID: 4300 RVA: 0x00036B42 File Offset: 0x00034D42
		internal Pop3Email Email
		{
			get
			{
				return this.email;
			}
			set
			{
				this.email = value;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x00036B4B File Offset: 0x00034D4B
		// (set) Token: 0x060010CE RID: 4302 RVA: 0x00036B53 File Offset: 0x00034D53
		internal int? RetentionDays
		{
			get
			{
				return this.retentionDays;
			}
			set
			{
				this.retentionDays = value;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x00036B5C File Offset: 0x00034D5C
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x00036B64 File Offset: 0x00034D64
		internal bool UidlCommandSupported
		{
			get
			{
				return this.uidlCommandSupported;
			}
			set
			{
				this.uidlCommandSupported = value;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x00036D14 File Offset: 0x00034F14
		internal IEnumerable<string> DeletedMessageUniqueIds
		{
			get
			{
				if (this.deletedMessageIds != null)
				{
					foreach (int messageId in this.deletedMessageIds)
					{
						yield return this.uniqueIds[messageId - 1];
					}
				}
				yield break;
			}
		}

		// Token: 0x170005FB RID: 1531
		internal int this[string uniqueId]
		{
			get
			{
				return this.uniqueId2IdMap[uniqueId];
			}
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00036D3F File Offset: 0x00034F3F
		internal bool Contains(string uniqueId)
		{
			return this.uniqueId2IdMap != null && this.uniqueId2IdMap.ContainsKey(uniqueId);
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00036D57 File Offset: 0x00034F57
		internal void SetEmailSize(int id, long size)
		{
			this.emailSizes[id - 1] = size;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00036D64 File Offset: 0x00034F64
		internal long GetEmailSize(int id)
		{
			return this.emailSizes[id - 1];
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00036D70 File Offset: 0x00034F70
		internal bool HasUniqueId(int id)
		{
			return this.uniqueIds[id - 1] != null;
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00036D82 File Offset: 0x00034F82
		internal void AllocateUniqueIds()
		{
			this.uniqueIds = new string[this.emailDropCount];
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00036D95 File Offset: 0x00034F95
		internal void AllocateEmailSizes()
		{
			this.emailSizes = new long[this.emailDropCount];
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00036DA8 File Offset: 0x00034FA8
		internal void SetUniqueId(int id, string uniqueId)
		{
			this.uniqueIds[id - 1] = uniqueId;
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00036DB5 File Offset: 0x00034FB5
		internal string GetUniqueId(int id)
		{
			return this.uniqueIds[id - 1];
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00036DC1 File Offset: 0x00034FC1
		internal void AddDeletedMessageId(int id)
		{
			if (this.deletedMessageIds == null)
			{
				this.deletedMessageIds = new List<int>(this.emailDropCount);
			}
			this.deletedMessageIds.Add(id);
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x00036DE8 File Offset: 0x00034FE8
		internal void AddUniqueId(string uniqueId, int id)
		{
			if (this.uniqueId2IdMap == null)
			{
				this.uniqueId2IdMap = new Dictionary<string, int>(this.emailDropCount);
			}
			this.uniqueId2IdMap.Add(uniqueId, id);
		}

		// Token: 0x04000977 RID: 2423
		private Dictionary<string, int> uniqueId2IdMap;

		// Token: 0x04000978 RID: 2424
		private List<int> deletedMessageIds;

		// Token: 0x04000979 RID: 2425
		private Pop3Email email;

		// Token: 0x0400097A RID: 2426
		private int emailDropCount;

		// Token: 0x0400097B RID: 2427
		private long[] emailSizes;

		// Token: 0x0400097C RID: 2428
		private string[] uniqueIds;

		// Token: 0x0400097D RID: 2429
		private int? retentionDays;

		// Token: 0x0400097E RID: 2430
		private bool uidlCommandSupported = true;

		// Token: 0x0400097F RID: 2431
		private ExDateTime? firstReceivedDate;

		// Token: 0x04000980 RID: 2432
		private ExDateTime? lastReceivedDate;
	}
}
