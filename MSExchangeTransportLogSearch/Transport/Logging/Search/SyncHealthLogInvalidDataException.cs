using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000065 RID: 101
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SyncHealthLogInvalidDataException : LocalizedException
	{
		// Token: 0x060001EC RID: 492 RVA: 0x0000D037 File Offset: 0x0000B237
		public SyncHealthLogInvalidDataException(bool timestampInvalid, bool eventIdInvalid, bool eventDataInvalid) : base(Strings.SyncHealthLogInvalidData(timestampInvalid, eventIdInvalid, eventDataInvalid))
		{
			this.timestampInvalid = timestampInvalid;
			this.eventIdInvalid = eventIdInvalid;
			this.eventDataInvalid = eventDataInvalid;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000D05C File Offset: 0x0000B25C
		public SyncHealthLogInvalidDataException(bool timestampInvalid, bool eventIdInvalid, bool eventDataInvalid, Exception innerException) : base(Strings.SyncHealthLogInvalidData(timestampInvalid, eventIdInvalid, eventDataInvalid), innerException)
		{
			this.timestampInvalid = timestampInvalid;
			this.eventIdInvalid = eventIdInvalid;
			this.eventDataInvalid = eventDataInvalid;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000D084 File Offset: 0x0000B284
		protected SyncHealthLogInvalidDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.timestampInvalid = (bool)info.GetValue("timestampInvalid", typeof(bool));
			this.eventIdInvalid = (bool)info.GetValue("eventIdInvalid", typeof(bool));
			this.eventDataInvalid = (bool)info.GetValue("eventDataInvalid", typeof(bool));
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000D0F9 File Offset: 0x0000B2F9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("timestampInvalid", this.timestampInvalid);
			info.AddValue("eventIdInvalid", this.eventIdInvalid);
			info.AddValue("eventDataInvalid", this.eventDataInvalid);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000D136 File Offset: 0x0000B336
		public bool TimestampInvalid
		{
			get
			{
				return this.timestampInvalid;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000D13E File Offset: 0x0000B33E
		public bool EventIdInvalid
		{
			get
			{
				return this.eventIdInvalid;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000D146 File Offset: 0x0000B346
		public bool EventDataInvalid
		{
			get
			{
				return this.eventDataInvalid;
			}
		}

		// Token: 0x04000189 RID: 393
		private readonly bool timestampInvalid;

		// Token: 0x0400018A RID: 394
		private readonly bool eventIdInvalid;

		// Token: 0x0400018B RID: 395
		private readonly bool eventDataInvalid;
	}
}
