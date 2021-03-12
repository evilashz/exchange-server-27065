using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management
{
	// Token: 0x02001137 RID: 4407
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AuditLogSearchNonUniqueArbitrationMailboxFoundException : LocalizedException
	{
		// Token: 0x0600B4FB RID: 46331 RVA: 0x0029D898 File Offset: 0x0029BA98
		public AuditLogSearchNonUniqueArbitrationMailboxFoundException(string organization) : base(Strings.AuditLogSearchNonUniqueArbitrationMailbox(organization))
		{
			this.organization = organization;
		}

		// Token: 0x0600B4FC RID: 46332 RVA: 0x0029D8AD File Offset: 0x0029BAAD
		public AuditLogSearchNonUniqueArbitrationMailboxFoundException(string organization, Exception innerException) : base(Strings.AuditLogSearchNonUniqueArbitrationMailbox(organization), innerException)
		{
			this.organization = organization;
		}

		// Token: 0x0600B4FD RID: 46333 RVA: 0x0029D8C3 File Offset: 0x0029BAC3
		protected AuditLogSearchNonUniqueArbitrationMailboxFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.organization = (string)info.GetValue("organization", typeof(string));
		}

		// Token: 0x0600B4FE RID: 46334 RVA: 0x0029D8ED File Offset: 0x0029BAED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("organization", this.organization);
		}

		// Token: 0x1700393C RID: 14652
		// (get) Token: 0x0600B4FF RID: 46335 RVA: 0x0029D908 File Offset: 0x0029BB08
		public string Organization
		{
			get
			{
				return this.organization;
			}
		}

		// Token: 0x040062A2 RID: 25250
		private readonly string organization;
	}
}
