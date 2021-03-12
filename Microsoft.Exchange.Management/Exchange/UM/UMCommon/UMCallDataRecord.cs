using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000D59 RID: 3417
	[Serializable]
	public class UMCallDataRecord : UMCallReportBase
	{
		// Token: 0x06008302 RID: 33538 RVA: 0x00217B89 File Offset: 0x00215D89
		public UMCallDataRecord(ObjectId identity) : base(identity)
		{
		}

		// Token: 0x170028BF RID: 10431
		// (get) Token: 0x06008303 RID: 33539 RVA: 0x00217B92 File Offset: 0x00215D92
		// (set) Token: 0x06008304 RID: 33540 RVA: 0x00217BA4 File Offset: 0x00215DA4
		public DateTime Date
		{
			get
			{
				return (DateTime)this[UMCallDataRecordSchema.Date];
			}
			internal set
			{
				this[UMCallDataRecordSchema.Date] = value;
			}
		}

		// Token: 0x170028C0 RID: 10432
		// (get) Token: 0x06008305 RID: 33541 RVA: 0x00217BB7 File Offset: 0x00215DB7
		// (set) Token: 0x06008306 RID: 33542 RVA: 0x00217BC9 File Offset: 0x00215DC9
		public TimeSpan Duration
		{
			get
			{
				return (TimeSpan)this[UMCallDataRecordSchema.Duration];
			}
			internal set
			{
				this[UMCallDataRecordSchema.Duration] = value;
			}
		}

		// Token: 0x170028C1 RID: 10433
		// (get) Token: 0x06008307 RID: 33543 RVA: 0x00217BDC File Offset: 0x00215DDC
		// (set) Token: 0x06008308 RID: 33544 RVA: 0x00217BEE File Offset: 0x00215DEE
		public string AudioCodec
		{
			get
			{
				return (string)this[UMCallDataRecordSchema.AudioCodec];
			}
			internal set
			{
				this[UMCallDataRecordSchema.AudioCodec] = value;
			}
		}

		// Token: 0x170028C2 RID: 10434
		// (get) Token: 0x06008309 RID: 33545 RVA: 0x00217BFC File Offset: 0x00215DFC
		// (set) Token: 0x0600830A RID: 33546 RVA: 0x00217C0E File Offset: 0x00215E0E
		public string DialPlan
		{
			get
			{
				return (string)this[UMCallDataRecordSchema.DialPlan];
			}
			internal set
			{
				this[UMCallDataRecordSchema.DialPlan] = value;
			}
		}

		// Token: 0x170028C3 RID: 10435
		// (get) Token: 0x0600830B RID: 33547 RVA: 0x00217C1C File Offset: 0x00215E1C
		// (set) Token: 0x0600830C RID: 33548 RVA: 0x00217C2E File Offset: 0x00215E2E
		public string CallType
		{
			get
			{
				return (string)this[UMCallDataRecordSchema.CallType];
			}
			internal set
			{
				this[UMCallDataRecordSchema.CallType] = value;
			}
		}

		// Token: 0x170028C4 RID: 10436
		// (get) Token: 0x0600830D RID: 33549 RVA: 0x00217C3C File Offset: 0x00215E3C
		// (set) Token: 0x0600830E RID: 33550 RVA: 0x00217C4E File Offset: 0x00215E4E
		public string CallingNumber
		{
			get
			{
				return (string)this[UMCallDataRecordSchema.CallingNumber];
			}
			internal set
			{
				this[UMCallDataRecordSchema.CallingNumber] = value;
			}
		}

		// Token: 0x170028C5 RID: 10437
		// (get) Token: 0x0600830F RID: 33551 RVA: 0x00217C5C File Offset: 0x00215E5C
		// (set) Token: 0x06008310 RID: 33552 RVA: 0x00217C6E File Offset: 0x00215E6E
		public string CalledNumber
		{
			get
			{
				return (string)this[UMCallDataRecordSchema.CalledNumber];
			}
			internal set
			{
				this[UMCallDataRecordSchema.CalledNumber] = value;
			}
		}

		// Token: 0x170028C6 RID: 10438
		// (get) Token: 0x06008311 RID: 33553 RVA: 0x00217C7C File Offset: 0x00215E7C
		// (set) Token: 0x06008312 RID: 33554 RVA: 0x00217C8E File Offset: 0x00215E8E
		public string Gateway
		{
			get
			{
				return (string)this[UMCallDataRecordSchema.Gateway];
			}
			internal set
			{
				this[UMCallDataRecordSchema.Gateway] = value;
			}
		}

		// Token: 0x170028C7 RID: 10439
		// (get) Token: 0x06008313 RID: 33555 RVA: 0x00217C9C File Offset: 0x00215E9C
		// (set) Token: 0x06008314 RID: 33556 RVA: 0x00217CAE File Offset: 0x00215EAE
		public string UserMailboxName
		{
			get
			{
				return (string)this[UMCallDataRecordSchema.UserMailboxName];
			}
			internal set
			{
				this[UMCallDataRecordSchema.UserMailboxName] = value;
			}
		}

		// Token: 0x170028C8 RID: 10440
		// (get) Token: 0x06008315 RID: 33557 RVA: 0x00217CBC File Offset: 0x00215EBC
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return UMCallDataRecord.schema;
			}
		}

		// Token: 0x04003F9B RID: 16283
		private static UMCallDataRecordSchema schema = ObjectSchema.GetInstance<UMCallDataRecordSchema>();
	}
}
