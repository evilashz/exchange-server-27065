using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02001115 RID: 4373
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CutoverMigrationNotAllowedException : LocalizedException
	{
		// Token: 0x0600B458 RID: 46168 RVA: 0x0029CA51 File Offset: 0x0029AC51
		public CutoverMigrationNotAllowedException(string tenantName) : base(Strings.CutoverMigrationNotAllowed(tenantName))
		{
			this.tenantName = tenantName;
		}

		// Token: 0x0600B459 RID: 46169 RVA: 0x0029CA66 File Offset: 0x0029AC66
		public CutoverMigrationNotAllowedException(string tenantName, Exception innerException) : base(Strings.CutoverMigrationNotAllowed(tenantName), innerException)
		{
			this.tenantName = tenantName;
		}

		// Token: 0x0600B45A RID: 46170 RVA: 0x0029CA7C File Offset: 0x0029AC7C
		protected CutoverMigrationNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.tenantName = (string)info.GetValue("tenantName", typeof(string));
		}

		// Token: 0x0600B45B RID: 46171 RVA: 0x0029CAA6 File Offset: 0x0029ACA6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("tenantName", this.tenantName);
		}

		// Token: 0x17003921 RID: 14625
		// (get) Token: 0x0600B45C RID: 46172 RVA: 0x0029CAC1 File Offset: 0x0029ACC1
		public string TenantName
		{
			get
			{
				return this.tenantName;
			}
		}

		// Token: 0x04006287 RID: 25223
		private readonly string tenantName;
	}
}
