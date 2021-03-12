using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000ECF RID: 3791
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxLacksArchivePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A8E4 RID: 43236 RVA: 0x0028AB81 File Offset: 0x00288D81
		public MailboxLacksArchivePermanentException(string identity) : base(Strings.ErrorMailboxHasNoArchive(identity))
		{
			this.identity = identity;
		}

		// Token: 0x0600A8E5 RID: 43237 RVA: 0x0028AB96 File Offset: 0x00288D96
		public MailboxLacksArchivePermanentException(string identity, Exception innerException) : base(Strings.ErrorMailboxHasNoArchive(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x0600A8E6 RID: 43238 RVA: 0x0028ABAC File Offset: 0x00288DAC
		protected MailboxLacksArchivePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x0600A8E7 RID: 43239 RVA: 0x0028ABD6 File Offset: 0x00288DD6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x170036C5 RID: 14021
		// (get) Token: 0x0600A8E8 RID: 43240 RVA: 0x0028ABF1 File Offset: 0x00288DF1
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x0400602B RID: 24619
		private readonly string identity;
	}
}
