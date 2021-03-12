using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x02000599 RID: 1433
	public class RequiredAppPermission
	{
		// Token: 0x060013BE RID: 5054 RVA: 0x0002BDC4 File Offset: 0x00029FC4
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static RequiredAppPermission CreateRequiredAppPermission(Guid permissionId, bool directAccessGrant, Collection<string> impersonationAccessGrants)
		{
			RequiredAppPermission requiredAppPermission = new RequiredAppPermission();
			requiredAppPermission.permissionId = permissionId;
			requiredAppPermission.directAccessGrant = directAccessGrant;
			if (impersonationAccessGrants == null)
			{
				throw new ArgumentNullException("impersonationAccessGrants");
			}
			requiredAppPermission.impersonationAccessGrants = impersonationAccessGrants;
			return requiredAppPermission;
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x0002BDFB File Offset: 0x00029FFB
		// (set) Token: 0x060013C0 RID: 5056 RVA: 0x0002BE03 File Offset: 0x0002A003
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Guid permissionId
		{
			get
			{
				return this._permissionId;
			}
			set
			{
				this._permissionId = value;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x0002BE0C File Offset: 0x0002A00C
		// (set) Token: 0x060013C2 RID: 5058 RVA: 0x0002BE14 File Offset: 0x0002A014
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool directAccessGrant
		{
			get
			{
				return this._directAccessGrant;
			}
			set
			{
				this._directAccessGrant = value;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x0002BE1D File Offset: 0x0002A01D
		// (set) Token: 0x060013C4 RID: 5060 RVA: 0x0002BE25 File Offset: 0x0002A025
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> impersonationAccessGrants
		{
			get
			{
				return this._impersonationAccessGrants;
			}
			set
			{
				this._impersonationAccessGrants = value;
			}
		}

		// Token: 0x040018EB RID: 6379
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid _permissionId;

		// Token: 0x040018EC RID: 6380
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _directAccessGrant;

		// Token: 0x040018ED RID: 6381
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _impersonationAccessGrants = new Collection<string>();
	}
}
