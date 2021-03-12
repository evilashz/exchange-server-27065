using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005DE RID: 1502
	public class RequiredAppPermission
	{
		// Token: 0x060018A0 RID: 6304 RVA: 0x0002FB18 File Offset: 0x0002DD18
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

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x0002FB4F File Offset: 0x0002DD4F
		// (set) Token: 0x060018A2 RID: 6306 RVA: 0x0002FB57 File Offset: 0x0002DD57
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

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x0002FB60 File Offset: 0x0002DD60
		// (set) Token: 0x060018A4 RID: 6308 RVA: 0x0002FB68 File Offset: 0x0002DD68
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

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x0002FB71 File Offset: 0x0002DD71
		// (set) Token: 0x060018A6 RID: 6310 RVA: 0x0002FB79 File Offset: 0x0002DD79
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

		// Token: 0x04001B2B RID: 6955
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid _permissionId;

		// Token: 0x04001B2C RID: 6956
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _directAccessGrant;

		// Token: 0x04001B2D RID: 6957
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _impersonationAccessGrants = new Collection<string>();
	}
}
