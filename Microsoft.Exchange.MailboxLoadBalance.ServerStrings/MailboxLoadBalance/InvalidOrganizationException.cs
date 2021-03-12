using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x0200001D RID: 29
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidOrganizationException : MailboxLoadBalancePermanentException
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00003053 File Offset: 0x00001253
		public InvalidOrganizationException(string orgName) : base(MigrationWorkflowServiceStrings.ErrorInvalidOrganization(orgName))
		{
			this.orgName = orgName;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003068 File Offset: 0x00001268
		public InvalidOrganizationException(string orgName, Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorInvalidOrganization(orgName), innerException)
		{
			this.orgName = orgName;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000307E File Offset: 0x0000127E
		protected InvalidOrganizationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.orgName = (string)info.GetValue("orgName", typeof(string));
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000030A8 File Offset: 0x000012A8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("orgName", this.orgName);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000030C3 File Offset: 0x000012C3
		public string OrgName
		{
			get
			{
				return this.orgName;
			}
		}

		// Token: 0x04000035 RID: 53
		private readonly string orgName;
	}
}
