using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000A97 RID: 2711
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PerimeterSettingsAmbiguousException : TenantContainerNotFoundException
	{
		// Token: 0x06007FC1 RID: 32705 RVA: 0x001A46B4 File Offset: 0x001A28B4
		public PerimeterSettingsAmbiguousException(string orgId) : base(DirectoryStrings.PerimeterSettingsAmbiguousException(orgId))
		{
			this.orgId = orgId;
		}

		// Token: 0x06007FC2 RID: 32706 RVA: 0x001A46C9 File Offset: 0x001A28C9
		public PerimeterSettingsAmbiguousException(string orgId, Exception innerException) : base(DirectoryStrings.PerimeterSettingsAmbiguousException(orgId), innerException)
		{
			this.orgId = orgId;
		}

		// Token: 0x06007FC3 RID: 32707 RVA: 0x001A46DF File Offset: 0x001A28DF
		protected PerimeterSettingsAmbiguousException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.orgId = (string)info.GetValue("orgId", typeof(string));
		}

		// Token: 0x06007FC4 RID: 32708 RVA: 0x001A4709 File Offset: 0x001A2909
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("orgId", this.orgId);
		}

		// Token: 0x17002EBC RID: 11964
		// (get) Token: 0x06007FC5 RID: 32709 RVA: 0x001A4724 File Offset: 0x001A2924
		public string OrgId
		{
			get
			{
				return this.orgId;
			}
		}

		// Token: 0x04005596 RID: 21910
		private readonly string orgId;
	}
}
