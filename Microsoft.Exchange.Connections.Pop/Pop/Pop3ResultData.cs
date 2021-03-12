using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x02000018 RID: 24
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Pop3ResultData
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00005DEC File Offset: 0x00003FEC
		internal Dictionary<string, int> UniqueId2IdMap
		{
			get
			{
				return this.uniqueId2IdMap;
			}
		}

		// Token: 0x1700003A RID: 58
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00005DF4 File Offset: 0x00003FF4
		internal ExDateTime FirstReceivedDate
		{
			set
			{
				this.firstReceivedDate = new ExDateTime?(value);
			}
		}

		// Token: 0x1700003B RID: 59
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00005E02 File Offset: 0x00004002
		internal ExDateTime LastReceivedDate
		{
			set
			{
				this.lastReceivedDate = new ExDateTime?(value);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00005E10 File Offset: 0x00004010
		internal bool ReceivedDateDescends
		{
			get
			{
				return this.firstReceivedDate == null || this.lastReceivedDate == null || (this.firstReceivedDate == ExDateTime.MinValue || this.lastReceivedDate == ExDateTime.MinValue) || this.lastReceivedDate <= this.firstReceivedDate;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00005EC1 File Offset: 0x000040C1
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00005EC9 File Offset: 0x000040C9
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

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00005ED2 File Offset: 0x000040D2
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00005EDA File Offset: 0x000040DA
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

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005EE3 File Offset: 0x000040E3
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00005EEB File Offset: 0x000040EB
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

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005EF4 File Offset: 0x000040F4
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00005EFC File Offset: 0x000040FC
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

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000060AC File Offset: 0x000042AC
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

		// Token: 0x17000042 RID: 66
		internal int this[string uniqueId]
		{
			get
			{
				return this.uniqueId2IdMap[uniqueId];
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000060D7 File Offset: 0x000042D7
		internal bool Contains(string uniqueId)
		{
			return this.uniqueId2IdMap != null && this.uniqueId2IdMap.ContainsKey(uniqueId);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000060EF File Offset: 0x000042EF
		internal void SetEmailSize(int id, long size)
		{
			this.emailSizes[id - 1] = size;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000060FC File Offset: 0x000042FC
		internal long GetEmailSize(int id)
		{
			return this.emailSizes[id - 1];
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00006108 File Offset: 0x00004308
		internal bool HasUniqueId(int id)
		{
			return this.uniqueIds[id - 1] != null;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000611A File Offset: 0x0000431A
		internal void AllocateUniqueIds()
		{
			this.uniqueIds = new string[this.emailDropCount];
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000612D File Offset: 0x0000432D
		internal void AllocateEmailSizes()
		{
			this.emailSizes = new long[this.emailDropCount];
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006140 File Offset: 0x00004340
		internal void SetUniqueId(int id, string uniqueId)
		{
			this.uniqueIds[id - 1] = uniqueId;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000614D File Offset: 0x0000434D
		internal string GetUniqueId(int id)
		{
			return this.uniqueIds[id - 1];
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006159 File Offset: 0x00004359
		internal void AddDeletedMessageId(int id)
		{
			if (this.deletedMessageIds == null)
			{
				this.deletedMessageIds = new List<int>(this.emailDropCount);
			}
			this.deletedMessageIds.Add(id);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00006180 File Offset: 0x00004380
		internal void AddUniqueId(string uniqueId, int id)
		{
			if (this.uniqueId2IdMap == null)
			{
				this.uniqueId2IdMap = new Dictionary<string, int>(this.emailDropCount);
			}
			this.uniqueId2IdMap.Add(uniqueId, id);
		}

		// Token: 0x040000A7 RID: 167
		private Dictionary<string, int> uniqueId2IdMap;

		// Token: 0x040000A8 RID: 168
		private List<int> deletedMessageIds;

		// Token: 0x040000A9 RID: 169
		private Pop3Email email;

		// Token: 0x040000AA RID: 170
		private int emailDropCount;

		// Token: 0x040000AB RID: 171
		private long[] emailSizes;

		// Token: 0x040000AC RID: 172
		private string[] uniqueIds;

		// Token: 0x040000AD RID: 173
		private int? retentionDays;

		// Token: 0x040000AE RID: 174
		private bool uidlCommandSupported = true;

		// Token: 0x040000AF RID: 175
		private ExDateTime? firstReceivedDate;

		// Token: 0x040000B0 RID: 176
		private ExDateTime? lastReceivedDate;
	}
}
