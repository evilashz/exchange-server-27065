using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000304 RID: 772
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxIsNotInExpectedMDBPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060024CD RID: 9421 RVA: 0x00050859 File Offset: 0x0004EA59
		public MailboxIsNotInExpectedMDBPermanentException(Guid mdbGuid) : base(MrsStrings.SourceMailboxIsNotInSourceMDB(mdbGuid))
		{
			this.mdbGuid = mdbGuid;
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x0005086E File Offset: 0x0004EA6E
		public MailboxIsNotInExpectedMDBPermanentException(Guid mdbGuid, Exception innerException) : base(MrsStrings.SourceMailboxIsNotInSourceMDB(mdbGuid), innerException)
		{
			this.mdbGuid = mdbGuid;
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x00050884 File Offset: 0x0004EA84
		protected MailboxIsNotInExpectedMDBPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mdbGuid = (Guid)info.GetValue("mdbGuid", typeof(Guid));
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x000508AE File Offset: 0x0004EAAE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mdbGuid", this.mdbGuid);
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x060024D1 RID: 9425 RVA: 0x000508CE File Offset: 0x0004EACE
		public Guid MdbGuid
		{
			get
			{
				return this.mdbGuid;
			}
		}

		// Token: 0x04001006 RID: 4102
		private readonly Guid mdbGuid;
	}
}
