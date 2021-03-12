using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EC7 RID: 3783
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxLacksDatabasePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A8BB RID: 43195 RVA: 0x0028A76C File Offset: 0x0028896C
		public MailboxLacksDatabasePermanentException(string identity) : base(Strings.ErrorMailboxLacksDatabase(identity))
		{
			this.identity = identity;
		}

		// Token: 0x0600A8BC RID: 43196 RVA: 0x0028A781 File Offset: 0x00288981
		public MailboxLacksDatabasePermanentException(string identity, Exception innerException) : base(Strings.ErrorMailboxLacksDatabase(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x0600A8BD RID: 43197 RVA: 0x0028A797 File Offset: 0x00288997
		protected MailboxLacksDatabasePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x0600A8BE RID: 43198 RVA: 0x0028A7C1 File Offset: 0x002889C1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x170036BC RID: 14012
		// (get) Token: 0x0600A8BF RID: 43199 RVA: 0x0028A7DC File Offset: 0x002889DC
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04006022 RID: 24610
		private readonly string identity;
	}
}
