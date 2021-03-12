using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E8B RID: 3723
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToSelectTargetDatabaseException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A78E RID: 42894 RVA: 0x00288A25 File Offset: 0x00286C25
		public UnableToSelectTargetDatabaseException(string identity) : base(Strings.ErrorUnableToDetermineTargetDatabase(identity))
		{
			this.identity = identity;
		}

		// Token: 0x0600A78F RID: 42895 RVA: 0x00288A3A File Offset: 0x00286C3A
		public UnableToSelectTargetDatabaseException(string identity, Exception innerException) : base(Strings.ErrorUnableToDetermineTargetDatabase(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x0600A790 RID: 42896 RVA: 0x00288A50 File Offset: 0x00286C50
		protected UnableToSelectTargetDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x0600A791 RID: 42897 RVA: 0x00288A7A File Offset: 0x00286C7A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x1700367F RID: 13951
		// (get) Token: 0x0600A792 RID: 42898 RVA: 0x00288A95 File Offset: 0x00286C95
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04005FE5 RID: 24549
		private readonly string identity;
	}
}
