using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002D7 RID: 727
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MRSInstanceFailedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060023ED RID: 9197 RVA: 0x0004F2BB File Offset: 0x0004D4BB
		public MRSInstanceFailedPermanentException(string mrsInstance, LocalizedString exceptionMessage) : base(MrsStrings.MRSInstanceFailed(mrsInstance, exceptionMessage))
		{
			this.mrsInstance = mrsInstance;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x0004F2D8 File Offset: 0x0004D4D8
		public MRSInstanceFailedPermanentException(string mrsInstance, LocalizedString exceptionMessage, Exception innerException) : base(MrsStrings.MRSInstanceFailed(mrsInstance, exceptionMessage), innerException)
		{
			this.mrsInstance = mrsInstance;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x0004F2F8 File Offset: 0x0004D4F8
		protected MRSInstanceFailedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mrsInstance = (string)info.GetValue("mrsInstance", typeof(string));
			this.exceptionMessage = (LocalizedString)info.GetValue("exceptionMessage", typeof(LocalizedString));
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x0004F34D File Offset: 0x0004D54D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mrsInstance", this.mrsInstance);
			info.AddValue("exceptionMessage", this.exceptionMessage);
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x060023F1 RID: 9201 RVA: 0x0004F37E File Offset: 0x0004D57E
		public string MrsInstance
		{
			get
			{
				return this.mrsInstance;
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x0004F386 File Offset: 0x0004D586
		public LocalizedString ExceptionMessage
		{
			get
			{
				return this.exceptionMessage;
			}
		}

		// Token: 0x04000FDA RID: 4058
		private readonly string mrsInstance;

		// Token: 0x04000FDB RID: 4059
		private readonly LocalizedString exceptionMessage;
	}
}
