using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000302 RID: 770
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxDatabaseNotFoundByIdPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060024C3 RID: 9411 RVA: 0x00050756 File Offset: 0x0004E956
		public MailboxDatabaseNotFoundByIdPermanentException(string mdbIdentity, LocalizedString notFoundReason) : base(MrsStrings.MailboxDatabaseNotFoundById(mdbIdentity, notFoundReason))
		{
			this.mdbIdentity = mdbIdentity;
			this.notFoundReason = notFoundReason;
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x00050773 File Offset: 0x0004E973
		public MailboxDatabaseNotFoundByIdPermanentException(string mdbIdentity, LocalizedString notFoundReason, Exception innerException) : base(MrsStrings.MailboxDatabaseNotFoundById(mdbIdentity, notFoundReason), innerException)
		{
			this.mdbIdentity = mdbIdentity;
			this.notFoundReason = notFoundReason;
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x00050794 File Offset: 0x0004E994
		protected MailboxDatabaseNotFoundByIdPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mdbIdentity = (string)info.GetValue("mdbIdentity", typeof(string));
			this.notFoundReason = (LocalizedString)info.GetValue("notFoundReason", typeof(LocalizedString));
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000507E9 File Offset: 0x0004E9E9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mdbIdentity", this.mdbIdentity);
			info.AddValue("notFoundReason", this.notFoundReason);
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x060024C7 RID: 9415 RVA: 0x0005081A File Offset: 0x0004EA1A
		public string MdbIdentity
		{
			get
			{
				return this.mdbIdentity;
			}
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x060024C8 RID: 9416 RVA: 0x00050822 File Offset: 0x0004EA22
		public LocalizedString NotFoundReason
		{
			get
			{
				return this.notFoundReason;
			}
		}

		// Token: 0x04001004 RID: 4100
		private readonly string mdbIdentity;

		// Token: 0x04001005 RID: 4101
		private readonly LocalizedString notFoundReason;
	}
}
