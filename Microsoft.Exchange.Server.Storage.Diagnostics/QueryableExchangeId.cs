using System;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000045 RID: 69
	public class QueryableExchangeId
	{
		// Token: 0x060001EC RID: 492 RVA: 0x0000DAF7 File Offset: 0x0000BCF7
		public QueryableExchangeId(ExchangeId exchangeId)
		{
			this.exchangeId = exchangeId;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000DB08 File Offset: 0x0000BD08
		public ushort Replid
		{
			get
			{
				return this.exchangeId.Replid;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000DB24 File Offset: 0x0000BD24
		public Guid Guid
		{
			get
			{
				return this.exchangeId.Guid;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000DB40 File Offset: 0x0000BD40
		public ulong Counter
		{
			get
			{
				return this.exchangeId.Counter;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000DB5C File Offset: 0x0000BD5C
		public byte[] Globcnt
		{
			get
			{
				return this.exchangeId.Globcnt;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000DB78 File Offset: 0x0000BD78
		public byte[] Binary8ByteFormat
		{
			get
			{
				return this.exchangeId.To8ByteArray();
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000DB94 File Offset: 0x0000BD94
		public byte[] Binary9ByteFormat
		{
			get
			{
				return this.exchangeId.To9ByteArray();
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000DBB0 File Offset: 0x0000BDB0
		public byte[] Binary22ByteFormat
		{
			get
			{
				return this.exchangeId.To22ByteArray();
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000DBCC File Offset: 0x0000BDCC
		public byte[] Binary24ByteFormat
		{
			get
			{
				return this.exchangeId.To24ByteArray();
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000DBE8 File Offset: 0x0000BDE8
		public byte[] Binary26ByteFormat
		{
			get
			{
				return this.exchangeId.To26ByteArray();
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000DC04 File Offset: 0x0000BE04
		public long LongFormat
		{
			get
			{
				return this.exchangeId.ToLong();
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000DC20 File Offset: 0x0000BE20
		public string ExmonStringFormat
		{
			get
			{
				return string.Format("{0:x}-{1:x}", this.exchangeId.Replid, this.exchangeId.Counter);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000DC60 File Offset: 0x0000BE60
		public string TraceStringFormat
		{
			get
			{
				return this.exchangeId.ToString();
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000DC84 File Offset: 0x0000BE84
		public bool IsReplidKnown
		{
			get
			{
				return this.exchangeId.IsReplidKnown;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000DCA0 File Offset: 0x0000BEA0
		public bool IsNull
		{
			get
			{
				return this.exchangeId.IsNull;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000DCBC File Offset: 0x0000BEBC
		public bool IsZero
		{
			get
			{
				return this.exchangeId.IsZero;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000DCD8 File Offset: 0x0000BED8
		public bool IsNullOrZero
		{
			get
			{
				return this.exchangeId.IsNullOrZero;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000DCF4 File Offset: 0x0000BEF4
		public bool IsValid
		{
			get
			{
				return this.exchangeId.IsValid;
			}
		}

		// Token: 0x0400012E RID: 302
		private readonly ExchangeId exchangeId;
	}
}
