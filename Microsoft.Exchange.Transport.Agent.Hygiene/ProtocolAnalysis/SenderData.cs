using System;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis
{
	// Token: 0x02000033 RID: 51
	[Serializable]
	internal abstract class SenderData
	{
		// Token: 0x06000121 RID: 289 RVA: 0x00009E10 File Offset: 0x00008010
		protected SenderData(DateTime tsCreate)
		{
			this.startTime = tsCreate;
			this.Rcpts = new int[2];
			this.Helo = new int[6];
			this.Callid = new int[6];
			this.ValidScl = new int[10];
			this.InvalidScl = new int[10];
			this.Length = new int[15];
			this.validUniqCnt = new UniqueCount();
			this.invalidUniqCnt = new UniqueCount();
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00009E8B File Offset: 0x0000808B
		public int UniqueValidRcptCount
		{
			get
			{
				return this.validUniqCnt.Count();
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00009E98 File Offset: 0x00008098
		public int UniqueInvalidRcptCount
		{
			get
			{
				return this.invalidUniqCnt.Count();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00009EA5 File Offset: 0x000080A5
		public DateTime StartTime
		{
			get
			{
				return this.startTime;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00009EAD File Offset: 0x000080AD
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00009EB5 File Offset: 0x000080B5
		public bool SenderBlocked
		{
			get
			{
				return this.Blocked;
			}
			set
			{
				this.Blocked = value;
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00009EC0 File Offset: 0x000080C0
		public void Merge(SenderData source)
		{
			this.NumMsgs += source.NumMsgs;
			int num = 0;
			while (num < this.Rcpts.Length && num < source.Rcpts.Length)
			{
				this.Rcpts[num] += source.Rcpts[num];
				num++;
			}
			num = 0;
			while (num < this.Helo.Length && num < source.Helo.Length)
			{
				this.Helo[num] += source.Helo[num];
				num++;
			}
			num = 0;
			while (num < this.Callid.Length && num < source.Callid.Length)
			{
				this.Callid[num] += source.Callid[num];
				num++;
			}
			num = 0;
			while (num < this.ValidScl.Length && num < source.ValidScl.Length)
			{
				this.ValidScl[num] += source.ValidScl[num];
				num++;
			}
			num = 0;
			while (num < this.InvalidScl.Length && num < source.InvalidScl.Length)
			{
				this.InvalidScl[num] += source.InvalidScl[num];
				num++;
			}
			num = 0;
			while (num < this.Length.Length && num < source.Length.Length)
			{
				this.Length[num] += source.Length[num];
				num++;
			}
			this.validUniqCnt.Merge(source.validUniqCnt);
			this.invalidUniqCnt.Merge(source.invalidUniqCnt);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000A076 File Offset: 0x00008276
		public virtual void OnValidRecipient(string recipient)
		{
			this.Rcpts[0]++;
			this.validUniqCnt.AddItem(recipient);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000A09D File Offset: 0x0000829D
		public virtual void OnInvalidRecipient(string recipient)
		{
			this.Rcpts[1]++;
			this.invalidUniqCnt.AddItem(recipient);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000A0C4 File Offset: 0x000082C4
		public virtual void OnUnknownRecipient(string recipient)
		{
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000A0C8 File Offset: 0x000082C8
		public virtual void OnEndOfData(int scl, long msgLength, CallerIdStatus status)
		{
			int num = (int)Math.Round(Math.Log((double)(msgLength + 1L)));
			if (num >= this.Length.Length)
			{
				num = this.Length.Length - 1;
			}
			this.NumMsgs++;
			this.Length[num]++;
			switch (status)
			{
			case CallerIdStatus.Valid:
				this.Callid[0]++;
				return;
			case CallerIdStatus.Invalid:
				this.Callid[1]++;
				return;
			case CallerIdStatus.Indeterminate:
				this.Callid[2]++;
				return;
			case CallerIdStatus.EpdError:
				this.Callid[3]++;
				return;
			case CallerIdStatus.Error:
				this.Callid[4]++;
				return;
			case CallerIdStatus.Null:
				this.Callid[5]++;
				return;
			default:
				return;
			}
		}

		// Token: 0x04000123 RID: 291
		public const int SclBuckets = 10;

		// Token: 0x04000124 RID: 292
		public const int LengthBuckets = 15;

		// Token: 0x04000125 RID: 293
		public const int HelloNullRdns = 0;

		// Token: 0x04000126 RID: 294
		public const int HelloEmpty = 1;

		// Token: 0x04000127 RID: 295
		public const int HelloMatchAll = 2;

		// Token: 0x04000128 RID: 296
		public const int HelloMatch2nd = 3;

		// Token: 0x04000129 RID: 297
		public const int HelloMatchLocal = 4;

		// Token: 0x0400012A RID: 298
		public const int HelloNoMatch = 5;

		// Token: 0x0400012B RID: 299
		public const int RcptValid = 0;

		// Token: 0x0400012C RID: 300
		public const int RcptInvalid = 1;

		// Token: 0x0400012D RID: 301
		public const int CallIdValid = 0;

		// Token: 0x0400012E RID: 302
		public const int CallIdInvalid = 1;

		// Token: 0x0400012F RID: 303
		public const int CallIdIndeterminate = 2;

		// Token: 0x04000130 RID: 304
		public const int CallIdEpdError = 3;

		// Token: 0x04000131 RID: 305
		public const int CallIdError = 4;

		// Token: 0x04000132 RID: 306
		public const int CallIdNull = 5;

		// Token: 0x04000133 RID: 307
		private DateTime startTime;

		// Token: 0x04000134 RID: 308
		internal bool Blocked;

		// Token: 0x04000135 RID: 309
		internal int NumMsgs;

		// Token: 0x04000136 RID: 310
		internal int[] Rcpts;

		// Token: 0x04000137 RID: 311
		internal int[] Helo;

		// Token: 0x04000138 RID: 312
		internal int[] Callid;

		// Token: 0x04000139 RID: 313
		internal int[] ValidScl;

		// Token: 0x0400013A RID: 314
		internal int[] InvalidScl;

		// Token: 0x0400013B RID: 315
		internal int[] Length;

		// Token: 0x0400013C RID: 316
		private UniqueCount validUniqCnt;

		// Token: 0x0400013D RID: 317
		private UniqueCount invalidUniqCnt;
	}
}
