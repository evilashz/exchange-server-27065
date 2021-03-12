using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EB7 RID: 3767
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRestoreConnectedMailboxPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A86D RID: 43117 RVA: 0x0028A062 File Offset: 0x00288262
		public CannotRestoreConnectedMailboxPermanentException(string identity) : base(Strings.ErrorCannotRestoreFromConnectedMailbox(identity))
		{
			this.identity = identity;
		}

		// Token: 0x0600A86E RID: 43118 RVA: 0x0028A077 File Offset: 0x00288277
		public CannotRestoreConnectedMailboxPermanentException(string identity, Exception innerException) : base(Strings.ErrorCannotRestoreFromConnectedMailbox(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x0600A86F RID: 43119 RVA: 0x0028A08D File Offset: 0x0028828D
		protected CannotRestoreConnectedMailboxPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x0600A870 RID: 43120 RVA: 0x0028A0B7 File Offset: 0x002882B7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x170036AE RID: 13998
		// (get) Token: 0x0600A871 RID: 43121 RVA: 0x0028A0D2 File Offset: 0x002882D2
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04006014 RID: 24596
		private readonly string identity;
	}
}
