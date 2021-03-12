using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x02000598 RID: 1432
	public class RequiredResourceAccess
	{
		// Token: 0x060013B8 RID: 5048 RVA: 0x0002BD5C File Offset: 0x00029F5C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static RequiredResourceAccess CreateRequiredResourceAccess(Guid resourceAppId, Collection<RequiredAppPermission> requiredAppPermissions)
		{
			RequiredResourceAccess requiredResourceAccess = new RequiredResourceAccess();
			requiredResourceAccess.resourceAppId = resourceAppId;
			if (requiredAppPermissions == null)
			{
				throw new ArgumentNullException("requiredAppPermissions");
			}
			requiredResourceAccess.requiredAppPermissions = requiredAppPermissions;
			return requiredResourceAccess;
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x0002BD8C File Offset: 0x00029F8C
		// (set) Token: 0x060013BA RID: 5050 RVA: 0x0002BD94 File Offset: 0x00029F94
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Guid resourceAppId
		{
			get
			{
				return this._resourceAppId;
			}
			set
			{
				this._resourceAppId = value;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x0002BD9D File Offset: 0x00029F9D
		// (set) Token: 0x060013BC RID: 5052 RVA: 0x0002BDA5 File Offset: 0x00029FA5
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<RequiredAppPermission> requiredAppPermissions
		{
			get
			{
				return this._requiredAppPermissions;
			}
			set
			{
				this._requiredAppPermissions = value;
			}
		}

		// Token: 0x040018E9 RID: 6377
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid _resourceAppId;

		// Token: 0x040018EA RID: 6378
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<RequiredAppPermission> _requiredAppPermissions = new Collection<RequiredAppPermission>();
	}
}
