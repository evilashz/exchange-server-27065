using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000301 RID: 769
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxDatabaseNotUniquePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060024BE RID: 9406 RVA: 0x000506DE File Offset: 0x0004E8DE
		public MailboxDatabaseNotUniquePermanentException(string mdbIdentity) : base(MrsStrings.MailboxDatabaseNotUnique(mdbIdentity))
		{
			this.mdbIdentity = mdbIdentity;
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000506F3 File Offset: 0x0004E8F3
		public MailboxDatabaseNotUniquePermanentException(string mdbIdentity, Exception innerException) : base(MrsStrings.MailboxDatabaseNotUnique(mdbIdentity), innerException)
		{
			this.mdbIdentity = mdbIdentity;
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x00050709 File Offset: 0x0004E909
		protected MailboxDatabaseNotUniquePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mdbIdentity = (string)info.GetValue("mdbIdentity", typeof(string));
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x00050733 File Offset: 0x0004E933
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mdbIdentity", this.mdbIdentity);
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x060024C2 RID: 9410 RVA: 0x0005074E File Offset: 0x0004E94E
		public string MdbIdentity
		{
			get
			{
				return this.mdbIdentity;
			}
		}

		// Token: 0x04001003 RID: 4099
		private readonly string mdbIdentity;
	}
}
