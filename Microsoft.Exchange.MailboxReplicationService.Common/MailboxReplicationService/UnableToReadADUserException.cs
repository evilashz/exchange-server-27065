using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200036B RID: 875
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToReadADUserException : MailboxReplicationPermanentException
	{
		// Token: 0x060026D4 RID: 9940 RVA: 0x00053BB8 File Offset: 0x00051DB8
		public UnableToReadADUserException(string userId) : base(MrsStrings.UnableToReadADUser(userId))
		{
			this.userId = userId;
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x00053BCD File Offset: 0x00051DCD
		public UnableToReadADUserException(string userId, Exception innerException) : base(MrsStrings.UnableToReadADUser(userId), innerException)
		{
			this.userId = userId;
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x00053BE3 File Offset: 0x00051DE3
		protected UnableToReadADUserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userId = (string)info.GetValue("userId", typeof(string));
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x00053C0D File Offset: 0x00051E0D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userId", this.userId);
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x060026D8 RID: 9944 RVA: 0x00053C28 File Offset: 0x00051E28
		public string UserId
		{
			get
			{
				return this.userId;
			}
		}

		// Token: 0x04001071 RID: 4209
		private readonly string userId;
	}
}
