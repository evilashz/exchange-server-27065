using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000DF RID: 223
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class OrganizationHasConstraintsException : MigrationPermanentException
	{
		// Token: 0x06000706 RID: 1798 RVA: 0x0000F601 File Offset: 0x0000D801
		public OrganizationHasConstraintsException(UpgradeRequestTypes requestedType, string orgId, string orgName, string constraints) : base(UpgradeHandlerStrings.OrganizationHasConstraints(requestedType, orgId, orgName, constraints))
		{
			this.requestedType = requestedType;
			this.orgId = orgId;
			this.orgName = orgName;
			this.constraints = constraints;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0000F630 File Offset: 0x0000D830
		public OrganizationHasConstraintsException(UpgradeRequestTypes requestedType, string orgId, string orgName, string constraints, Exception innerException) : base(UpgradeHandlerStrings.OrganizationHasConstraints(requestedType, orgId, orgName, constraints), innerException)
		{
			this.requestedType = requestedType;
			this.orgId = orgId;
			this.orgName = orgName;
			this.constraints = constraints;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0000F664 File Offset: 0x0000D864
		protected OrganizationHasConstraintsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.requestedType = (UpgradeRequestTypes)info.GetValue("requestedType", typeof(UpgradeRequestTypes));
			this.orgId = (string)info.GetValue("orgId", typeof(string));
			this.orgName = (string)info.GetValue("orgName", typeof(string));
			this.constraints = (string)info.GetValue("constraints", typeof(string));
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0000F6FC File Offset: 0x0000D8FC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("requestedType", this.requestedType);
			info.AddValue("orgId", this.orgId);
			info.AddValue("orgName", this.orgName);
			info.AddValue("constraints", this.constraints);
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0000F75A File Offset: 0x0000D95A
		public UpgradeRequestTypes RequestedType
		{
			get
			{
				return this.requestedType;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0000F762 File Offset: 0x0000D962
		public string OrgId
		{
			get
			{
				return this.orgId;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0000F76A File Offset: 0x0000D96A
		public string OrgName
		{
			get
			{
				return this.orgName;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0000F772 File Offset: 0x0000D972
		public string Constraints
		{
			get
			{
				return this.constraints;
			}
		}

		// Token: 0x0400038A RID: 906
		private readonly UpgradeRequestTypes requestedType;

		// Token: 0x0400038B RID: 907
		private readonly string orgId;

		// Token: 0x0400038C RID: 908
		private readonly string orgName;

		// Token: 0x0400038D RID: 909
		private readonly string constraints;
	}
}
