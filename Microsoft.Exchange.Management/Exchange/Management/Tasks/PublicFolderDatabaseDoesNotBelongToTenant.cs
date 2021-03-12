using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E85 RID: 3717
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PublicFolderDatabaseDoesNotBelongToTenant : LocalizedException
	{
		// Token: 0x0600A76B RID: 42859 RVA: 0x002885A8 File Offset: 0x002867A8
		public PublicFolderDatabaseDoesNotBelongToTenant(string pfId, string tenantId) : base(Strings.PublicFolderDatabaseDoesNotBelongToTenant(pfId, tenantId))
		{
			this.pfId = pfId;
			this.tenantId = tenantId;
		}

		// Token: 0x0600A76C RID: 42860 RVA: 0x002885C5 File Offset: 0x002867C5
		public PublicFolderDatabaseDoesNotBelongToTenant(string pfId, string tenantId, Exception innerException) : base(Strings.PublicFolderDatabaseDoesNotBelongToTenant(pfId, tenantId), innerException)
		{
			this.pfId = pfId;
			this.tenantId = tenantId;
		}

		// Token: 0x0600A76D RID: 42861 RVA: 0x002885E4 File Offset: 0x002867E4
		protected PublicFolderDatabaseDoesNotBelongToTenant(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.pfId = (string)info.GetValue("pfId", typeof(string));
			this.tenantId = (string)info.GetValue("tenantId", typeof(string));
		}

		// Token: 0x0600A76E RID: 42862 RVA: 0x00288639 File Offset: 0x00286839
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("pfId", this.pfId);
			info.AddValue("tenantId", this.tenantId);
		}

		// Token: 0x17003674 RID: 13940
		// (get) Token: 0x0600A76F RID: 42863 RVA: 0x00288665 File Offset: 0x00286865
		public string PfId
		{
			get
			{
				return this.pfId;
			}
		}

		// Token: 0x17003675 RID: 13941
		// (get) Token: 0x0600A770 RID: 42864 RVA: 0x0028866D File Offset: 0x0028686D
		public string TenantId
		{
			get
			{
				return this.tenantId;
			}
		}

		// Token: 0x04005FDA RID: 24538
		private readonly string pfId;

		// Token: 0x04005FDB RID: 24539
		private readonly string tenantId;
	}
}
