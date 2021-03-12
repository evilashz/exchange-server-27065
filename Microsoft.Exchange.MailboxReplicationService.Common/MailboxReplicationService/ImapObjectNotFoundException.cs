using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000379 RID: 889
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ImapObjectNotFoundException : MailboxReplicationPermanentException
	{
		// Token: 0x06002717 RID: 10007 RVA: 0x00054183 File Offset: 0x00052383
		public ImapObjectNotFoundException(string entryId) : base(MrsStrings.ImapObjectNotFound(entryId))
		{
			this.entryId = entryId;
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x00054198 File Offset: 0x00052398
		public ImapObjectNotFoundException(string entryId, Exception innerException) : base(MrsStrings.ImapObjectNotFound(entryId), innerException)
		{
			this.entryId = entryId;
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x000541AE File Offset: 0x000523AE
		protected ImapObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.entryId = (string)info.GetValue("entryId", typeof(string));
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x000541D8 File Offset: 0x000523D8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("entryId", this.entryId);
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x0600271B RID: 10011 RVA: 0x000541F3 File Offset: 0x000523F3
		public string EntryId
		{
			get
			{
				return this.entryId;
			}
		}

		// Token: 0x0400107C RID: 4220
		private readonly string entryId;
	}
}
