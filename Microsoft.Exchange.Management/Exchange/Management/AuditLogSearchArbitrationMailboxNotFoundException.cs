using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management
{
	// Token: 0x02001136 RID: 4406
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AuditLogSearchArbitrationMailboxNotFoundException : LocalizedException
	{
		// Token: 0x0600B4F6 RID: 46326 RVA: 0x0029D820 File Offset: 0x0029BA20
		public AuditLogSearchArbitrationMailboxNotFoundException(string organization) : base(Strings.AuditLogSearchArbitrationMailboxNotFound(organization))
		{
			this.organization = organization;
		}

		// Token: 0x0600B4F7 RID: 46327 RVA: 0x0029D835 File Offset: 0x0029BA35
		public AuditLogSearchArbitrationMailboxNotFoundException(string organization, Exception innerException) : base(Strings.AuditLogSearchArbitrationMailboxNotFound(organization), innerException)
		{
			this.organization = organization;
		}

		// Token: 0x0600B4F8 RID: 46328 RVA: 0x0029D84B File Offset: 0x0029BA4B
		protected AuditLogSearchArbitrationMailboxNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.organization = (string)info.GetValue("organization", typeof(string));
		}

		// Token: 0x0600B4F9 RID: 46329 RVA: 0x0029D875 File Offset: 0x0029BA75
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("organization", this.organization);
		}

		// Token: 0x1700393B RID: 14651
		// (get) Token: 0x0600B4FA RID: 46330 RVA: 0x0029D890 File Offset: 0x0029BA90
		public string Organization
		{
			get
			{
				return this.organization;
			}
		}

		// Token: 0x040062A1 RID: 25249
		private readonly string organization;
	}
}
