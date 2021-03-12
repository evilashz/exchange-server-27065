using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EB9 RID: 3769
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRestoreIntoSelfPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A877 RID: 43127 RVA: 0x0028A152 File Offset: 0x00288352
		public CannotRestoreIntoSelfPermanentException(string identity) : base(Strings.ErrorCannotRestoreIntoSelf(identity))
		{
			this.identity = identity;
		}

		// Token: 0x0600A878 RID: 43128 RVA: 0x0028A167 File Offset: 0x00288367
		public CannotRestoreIntoSelfPermanentException(string identity, Exception innerException) : base(Strings.ErrorCannotRestoreIntoSelf(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x0600A879 RID: 43129 RVA: 0x0028A17D File Offset: 0x0028837D
		protected CannotRestoreIntoSelfPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x0600A87A RID: 43130 RVA: 0x0028A1A7 File Offset: 0x002883A7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x170036B0 RID: 14000
		// (get) Token: 0x0600A87B RID: 43131 RVA: 0x0028A1C2 File Offset: 0x002883C2
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04006016 RID: 24598
		private readonly string identity;
	}
}
