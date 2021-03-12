using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000E2 RID: 226
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidRequestedTypeException : MigrationTransientException
	{
		// Token: 0x06000719 RID: 1817 RVA: 0x0000F8C6 File Offset: 0x0000DAC6
		public InvalidRequestedTypeException(string orgId, UpgradeRequestTypes currentType, string requestedType) : base(UpgradeHandlerStrings.InvalidRequestedType(orgId, currentType, requestedType))
		{
			this.orgId = orgId;
			this.currentType = currentType;
			this.requestedType = requestedType;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0000F8EB File Offset: 0x0000DAEB
		public InvalidRequestedTypeException(string orgId, UpgradeRequestTypes currentType, string requestedType, Exception innerException) : base(UpgradeHandlerStrings.InvalidRequestedType(orgId, currentType, requestedType), innerException)
		{
			this.orgId = orgId;
			this.currentType = currentType;
			this.requestedType = requestedType;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0000F914 File Offset: 0x0000DB14
		protected InvalidRequestedTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.orgId = (string)info.GetValue("orgId", typeof(string));
			this.currentType = (UpgradeRequestTypes)info.GetValue("currentType", typeof(UpgradeRequestTypes));
			this.requestedType = (string)info.GetValue("requestedType", typeof(string));
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0000F98C File Offset: 0x0000DB8C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("orgId", this.orgId);
			info.AddValue("currentType", this.currentType);
			info.AddValue("requestedType", this.requestedType);
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0000F9D9 File Offset: 0x0000DBD9
		public string OrgId
		{
			get
			{
				return this.orgId;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0000F9E1 File Offset: 0x0000DBE1
		public UpgradeRequestTypes CurrentType
		{
			get
			{
				return this.currentType;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0000F9E9 File Offset: 0x0000DBE9
		public string RequestedType
		{
			get
			{
				return this.requestedType;
			}
		}

		// Token: 0x04000391 RID: 913
		private readonly string orgId;

		// Token: 0x04000392 RID: 914
		private readonly UpgradeRequestTypes currentType;

		// Token: 0x04000393 RID: 915
		private readonly string requestedType;
	}
}
