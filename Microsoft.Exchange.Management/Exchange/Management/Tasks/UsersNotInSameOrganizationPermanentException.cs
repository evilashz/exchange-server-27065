using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EB4 RID: 3764
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UsersNotInSameOrganizationPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A85A RID: 43098 RVA: 0x00289DB1 File Offset: 0x00287FB1
		public UsersNotInSameOrganizationPermanentException(string src, string tgt) : base(Strings.ErrorUsersNotInSameOrganization(src, tgt))
		{
			this.src = src;
			this.tgt = tgt;
		}

		// Token: 0x0600A85B RID: 43099 RVA: 0x00289DCE File Offset: 0x00287FCE
		public UsersNotInSameOrganizationPermanentException(string src, string tgt, Exception innerException) : base(Strings.ErrorUsersNotInSameOrganization(src, tgt), innerException)
		{
			this.src = src;
			this.tgt = tgt;
		}

		// Token: 0x0600A85C RID: 43100 RVA: 0x00289DEC File Offset: 0x00287FEC
		protected UsersNotInSameOrganizationPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.src = (string)info.GetValue("src", typeof(string));
			this.tgt = (string)info.GetValue("tgt", typeof(string));
		}

		// Token: 0x0600A85D RID: 43101 RVA: 0x00289E41 File Offset: 0x00288041
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("src", this.src);
			info.AddValue("tgt", this.tgt);
		}

		// Token: 0x170036A7 RID: 13991
		// (get) Token: 0x0600A85E RID: 43102 RVA: 0x00289E6D File Offset: 0x0028806D
		public string Src
		{
			get
			{
				return this.src;
			}
		}

		// Token: 0x170036A8 RID: 13992
		// (get) Token: 0x0600A85F RID: 43103 RVA: 0x00289E75 File Offset: 0x00288075
		public string Tgt
		{
			get
			{
				return this.tgt;
			}
		}

		// Token: 0x0400600D RID: 24589
		private readonly string src;

		// Token: 0x0400600E RID: 24590
		private readonly string tgt;
	}
}
