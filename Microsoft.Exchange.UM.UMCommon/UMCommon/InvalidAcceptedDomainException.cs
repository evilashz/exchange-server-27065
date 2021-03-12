using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001DC RID: 476
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidAcceptedDomainException : LocalizedException
	{
		// Token: 0x06000F6D RID: 3949 RVA: 0x000366DE File Offset: 0x000348DE
		public InvalidAcceptedDomainException(string organizationId) : base(Strings.InvalidAcceptedDomainException(organizationId))
		{
			this.organizationId = organizationId;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x000366F3 File Offset: 0x000348F3
		public InvalidAcceptedDomainException(string organizationId, Exception innerException) : base(Strings.InvalidAcceptedDomainException(organizationId), innerException)
		{
			this.organizationId = organizationId;
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00036709 File Offset: 0x00034909
		protected InvalidAcceptedDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.organizationId = (string)info.GetValue("organizationId", typeof(string));
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x00036733 File Offset: 0x00034933
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("organizationId", this.organizationId);
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x0003674E File Offset: 0x0003494E
		public string OrganizationId
		{
			get
			{
				return this.organizationId;
			}
		}

		// Token: 0x040007AD RID: 1965
		private readonly string organizationId;
	}
}
