using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000DD RID: 221
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class OrganizationInDryRunModeException : MigrationTransientException
	{
		// Token: 0x060006FB RID: 1787 RVA: 0x0000F4BB File Offset: 0x0000D6BB
		public OrganizationInDryRunModeException(string tenant, string requestedType) : base(UpgradeHandlerStrings.OrganizationInDryRunMode(tenant, requestedType))
		{
			this.tenant = tenant;
			this.requestedType = requestedType;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0000F4D8 File Offset: 0x0000D6D8
		public OrganizationInDryRunModeException(string tenant, string requestedType, Exception innerException) : base(UpgradeHandlerStrings.OrganizationInDryRunMode(tenant, requestedType), innerException)
		{
			this.tenant = tenant;
			this.requestedType = requestedType;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0000F4F8 File Offset: 0x0000D6F8
		protected OrganizationInDryRunModeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.tenant = (string)info.GetValue("tenant", typeof(string));
			this.requestedType = (string)info.GetValue("requestedType", typeof(string));
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0000F54D File Offset: 0x0000D74D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("tenant", this.tenant);
			info.AddValue("requestedType", this.requestedType);
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x0000F579 File Offset: 0x0000D779
		public string Tenant
		{
			get
			{
				return this.tenant;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0000F581 File Offset: 0x0000D781
		public string RequestedType
		{
			get
			{
				return this.requestedType;
			}
		}

		// Token: 0x04000387 RID: 903
		private readonly string tenant;

		// Token: 0x04000388 RID: 904
		private readonly string requestedType;
	}
}
