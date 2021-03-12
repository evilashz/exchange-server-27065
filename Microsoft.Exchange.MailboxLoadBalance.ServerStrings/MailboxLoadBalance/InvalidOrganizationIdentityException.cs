using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x0200001E RID: 30
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidOrganizationIdentityException : MailboxLoadBalancePermanentException
	{
		// Token: 0x06000095 RID: 149 RVA: 0x000030CB File Offset: 0x000012CB
		public InvalidOrganizationIdentityException(string orgName, string externalId) : base(MigrationWorkflowServiceStrings.ErrorInvalidExternalOrganizationId(orgName, externalId))
		{
			this.orgName = orgName;
			this.externalId = externalId;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000030E8 File Offset: 0x000012E8
		public InvalidOrganizationIdentityException(string orgName, string externalId, Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorInvalidExternalOrganizationId(orgName, externalId), innerException)
		{
			this.orgName = orgName;
			this.externalId = externalId;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003108 File Offset: 0x00001308
		protected InvalidOrganizationIdentityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.orgName = (string)info.GetValue("orgName", typeof(string));
			this.externalId = (string)info.GetValue("externalId", typeof(string));
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000315D File Offset: 0x0000135D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("orgName", this.orgName);
			info.AddValue("externalId", this.externalId);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003189 File Offset: 0x00001389
		public string OrgName
		{
			get
			{
				return this.orgName;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003191 File Offset: 0x00001391
		public string ExternalId
		{
			get
			{
				return this.externalId;
			}
		}

		// Token: 0x04000036 RID: 54
		private readonly string orgName;

		// Token: 0x04000037 RID: 55
		private readonly string externalId;
	}
}
