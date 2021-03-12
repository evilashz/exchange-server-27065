using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200113C RID: 4412
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MultipleAdminAuditLogConfigException : AdminAuditLogException
	{
		// Token: 0x0600B510 RID: 46352 RVA: 0x0029D9BC File Offset: 0x0029BBBC
		public MultipleAdminAuditLogConfigException(string organization) : base(Strings.MultipleAdminAuditLogConfig(organization))
		{
			this.organization = organization;
		}

		// Token: 0x0600B511 RID: 46353 RVA: 0x0029D9D1 File Offset: 0x0029BBD1
		public MultipleAdminAuditLogConfigException(string organization, Exception innerException) : base(Strings.MultipleAdminAuditLogConfig(organization), innerException)
		{
			this.organization = organization;
		}

		// Token: 0x0600B512 RID: 46354 RVA: 0x0029D9E7 File Offset: 0x0029BBE7
		protected MultipleAdminAuditLogConfigException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.organization = (string)info.GetValue("organization", typeof(string));
		}

		// Token: 0x0600B513 RID: 46355 RVA: 0x0029DA11 File Offset: 0x0029BC11
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("organization", this.organization);
		}

		// Token: 0x1700393D RID: 14653
		// (get) Token: 0x0600B514 RID: 46356 RVA: 0x0029DA2C File Offset: 0x0029BC2C
		public string Organization
		{
			get
			{
				return this.organization;
			}
		}

		// Token: 0x040062A3 RID: 25251
		private readonly string organization;
	}
}
