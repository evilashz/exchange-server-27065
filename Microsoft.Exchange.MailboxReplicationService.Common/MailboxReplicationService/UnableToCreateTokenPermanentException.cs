using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002DB RID: 731
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToCreateTokenPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002405 RID: 9221 RVA: 0x0004F5FD File Offset: 0x0004D7FD
		public UnableToCreateTokenPermanentException(string user) : base(MrsStrings.UnableToCreateToken(user))
		{
			this.user = user;
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x0004F612 File Offset: 0x0004D812
		public UnableToCreateTokenPermanentException(string user, Exception innerException) : base(MrsStrings.UnableToCreateToken(user), innerException)
		{
			this.user = user;
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x0004F628 File Offset: 0x0004D828
		protected UnableToCreateTokenPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x0004F652 File Offset: 0x0004D852
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06002409 RID: 9225 RVA: 0x0004F66D File Offset: 0x0004D86D
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x04000FE2 RID: 4066
		private readonly string user;
	}
}
