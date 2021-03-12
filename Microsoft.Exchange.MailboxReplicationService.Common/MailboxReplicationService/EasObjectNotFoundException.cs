using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200038F RID: 911
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasObjectNotFoundException : MailboxReplicationPermanentException
	{
		// Token: 0x06002786 RID: 10118 RVA: 0x00054C29 File Offset: 0x00052E29
		public EasObjectNotFoundException(string entryId) : base(MrsStrings.EasObjectNotFound(entryId))
		{
			this.entryId = entryId;
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x00054C3E File Offset: 0x00052E3E
		public EasObjectNotFoundException(string entryId, Exception innerException) : base(MrsStrings.EasObjectNotFound(entryId), innerException)
		{
			this.entryId = entryId;
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x00054C54 File Offset: 0x00052E54
		protected EasObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.entryId = (string)info.GetValue("entryId", typeof(string));
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x00054C7E File Offset: 0x00052E7E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("entryId", this.entryId);
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x0600278A RID: 10122 RVA: 0x00054C99 File Offset: 0x00052E99
		public string EntryId
		{
			get
			{
				return this.entryId;
			}
		}

		// Token: 0x04001093 RID: 4243
		private readonly string entryId;
	}
}
