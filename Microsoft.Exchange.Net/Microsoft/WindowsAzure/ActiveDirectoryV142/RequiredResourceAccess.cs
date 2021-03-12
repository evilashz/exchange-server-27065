using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005DD RID: 1501
	public class RequiredResourceAccess
	{
		// Token: 0x0600189A RID: 6298 RVA: 0x0002FAB0 File Offset: 0x0002DCB0
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

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x0002FAE0 File Offset: 0x0002DCE0
		// (set) Token: 0x0600189C RID: 6300 RVA: 0x0002FAE8 File Offset: 0x0002DCE8
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

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x0002FAF1 File Offset: 0x0002DCF1
		// (set) Token: 0x0600189E RID: 6302 RVA: 0x0002FAF9 File Offset: 0x0002DCF9
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

		// Token: 0x04001B29 RID: 6953
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid _resourceAppId;

		// Token: 0x04001B2A RID: 6954
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<RequiredAppPermission> _requiredAppPermissions = new Collection<RequiredAppPermission>();
	}
}
