using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.AirSync
{
	// Token: 0x02000E31 RID: 3633
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoActiveSyncOrganizationSettingsException : LocalizedException
	{
		// Token: 0x0600A5FE RID: 42494 RVA: 0x00286F85 File Offset: 0x00285185
		public NoActiveSyncOrganizationSettingsException(string organizationId) : base(Strings.NoActiveSyncOrganizationSettingsException(organizationId))
		{
			this.organizationId = organizationId;
		}

		// Token: 0x0600A5FF RID: 42495 RVA: 0x00286F9A File Offset: 0x0028519A
		public NoActiveSyncOrganizationSettingsException(string organizationId, Exception innerException) : base(Strings.NoActiveSyncOrganizationSettingsException(organizationId), innerException)
		{
			this.organizationId = organizationId;
		}

		// Token: 0x0600A600 RID: 42496 RVA: 0x00286FB0 File Offset: 0x002851B0
		protected NoActiveSyncOrganizationSettingsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.organizationId = (string)info.GetValue("organizationId", typeof(string));
		}

		// Token: 0x0600A601 RID: 42497 RVA: 0x00286FDA File Offset: 0x002851DA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("organizationId", this.organizationId);
		}

		// Token: 0x17003657 RID: 13911
		// (get) Token: 0x0600A602 RID: 42498 RVA: 0x00286FF5 File Offset: 0x002851F5
		public string OrganizationId
		{
			get
			{
				return this.organizationId;
			}
		}

		// Token: 0x04005FBD RID: 24509
		private readonly string organizationId;
	}
}
