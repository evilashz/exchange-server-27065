using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001E4 RID: 484
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidTenantGuidException : LocalizedException
	{
		// Token: 0x06000F92 RID: 3986 RVA: 0x000369BB File Offset: 0x00034BBB
		public InvalidTenantGuidException(Guid tenantGuid) : base(Strings.InvalidTenantGuidException(tenantGuid))
		{
			this.tenantGuid = tenantGuid;
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x000369D0 File Offset: 0x00034BD0
		public InvalidTenantGuidException(Guid tenantGuid, Exception innerException) : base(Strings.InvalidTenantGuidException(tenantGuid), innerException)
		{
			this.tenantGuid = tenantGuid;
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x000369E6 File Offset: 0x00034BE6
		protected InvalidTenantGuidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.tenantGuid = (Guid)info.GetValue("tenantGuid", typeof(Guid));
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00036A10 File Offset: 0x00034C10
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("tenantGuid", this.tenantGuid);
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x00036A30 File Offset: 0x00034C30
		public Guid TenantGuid
		{
			get
			{
				return this.tenantGuid;
			}
		}

		// Token: 0x040007B2 RID: 1970
		private readonly Guid tenantGuid;
	}
}
