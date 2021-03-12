using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000A95 RID: 2709
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TenantTransportSettingsNotFoundException : TenantContainerNotFoundException
	{
		// Token: 0x06007FB7 RID: 32695 RVA: 0x001A45C4 File Offset: 0x001A27C4
		public TenantTransportSettingsNotFoundException(string orgId) : base(DirectoryStrings.TenantTransportSettingsNotFoundException(orgId))
		{
			this.orgId = orgId;
		}

		// Token: 0x06007FB8 RID: 32696 RVA: 0x001A45D9 File Offset: 0x001A27D9
		public TenantTransportSettingsNotFoundException(string orgId, Exception innerException) : base(DirectoryStrings.TenantTransportSettingsNotFoundException(orgId), innerException)
		{
			this.orgId = orgId;
		}

		// Token: 0x06007FB9 RID: 32697 RVA: 0x001A45EF File Offset: 0x001A27EF
		protected TenantTransportSettingsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.orgId = (string)info.GetValue("orgId", typeof(string));
		}

		// Token: 0x06007FBA RID: 32698 RVA: 0x001A4619 File Offset: 0x001A2819
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("orgId", this.orgId);
		}

		// Token: 0x17002EBA RID: 11962
		// (get) Token: 0x06007FBB RID: 32699 RVA: 0x001A4634 File Offset: 0x001A2834
		public string OrgId
		{
			get
			{
				return this.orgId;
			}
		}

		// Token: 0x04005594 RID: 21908
		private readonly string orgId;
	}
}
