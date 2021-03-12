using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005D9 RID: 1497
	public class AppPermission
	{
		// Token: 0x06001863 RID: 6243 RVA: 0x0002F848 File Offset: 0x0002DA48
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static AppPermission CreateAppPermission(Collection<string> directAccessGrantTypes, Collection<ImpersonationAccessGrantType> impersonationAccessGrantTypes, bool isDisabled, Guid permissionId)
		{
			AppPermission appPermission = new AppPermission();
			if (directAccessGrantTypes == null)
			{
				throw new ArgumentNullException("directAccessGrantTypes");
			}
			appPermission.directAccessGrantTypes = directAccessGrantTypes;
			if (impersonationAccessGrantTypes == null)
			{
				throw new ArgumentNullException("impersonationAccessGrantTypes");
			}
			appPermission.impersonationAccessGrantTypes = impersonationAccessGrantTypes;
			appPermission.isDisabled = isDisabled;
			appPermission.permissionId = permissionId;
			return appPermission;
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x0002F894 File Offset: 0x0002DA94
		// (set) Token: 0x06001865 RID: 6245 RVA: 0x0002F89C File Offset: 0x0002DA9C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string claimValue
		{
			get
			{
				return this._claimValue;
			}
			set
			{
				this._claimValue = value;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x0002F8A5 File Offset: 0x0002DAA5
		// (set) Token: 0x06001867 RID: 6247 RVA: 0x0002F8AD File Offset: 0x0002DAAD
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x0002F8B6 File Offset: 0x0002DAB6
		// (set) Token: 0x06001869 RID: 6249 RVA: 0x0002F8BE File Offset: 0x0002DABE
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> directAccessGrantTypes
		{
			get
			{
				return this._directAccessGrantTypes;
			}
			set
			{
				this._directAccessGrantTypes = value;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x0002F8C7 File Offset: 0x0002DAC7
		// (set) Token: 0x0600186B RID: 6251 RVA: 0x0002F8CF File Offset: 0x0002DACF
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string displayName
		{
			get
			{
				return this._displayName;
			}
			set
			{
				this._displayName = value;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x0002F8D8 File Offset: 0x0002DAD8
		// (set) Token: 0x0600186D RID: 6253 RVA: 0x0002F8E0 File Offset: 0x0002DAE0
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<ImpersonationAccessGrantType> impersonationAccessGrantTypes
		{
			get
			{
				return this._impersonationAccessGrantTypes;
			}
			set
			{
				this._impersonationAccessGrantTypes = value;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x0002F8E9 File Offset: 0x0002DAE9
		// (set) Token: 0x0600186F RID: 6255 RVA: 0x0002F8F1 File Offset: 0x0002DAF1
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool isDisabled
		{
			get
			{
				return this._isDisabled;
			}
			set
			{
				this._isDisabled = value;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x0002F8FA File Offset: 0x0002DAFA
		// (set) Token: 0x06001871 RID: 6257 RVA: 0x0002F902 File Offset: 0x0002DB02
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string origin
		{
			get
			{
				return this._origin;
			}
			set
			{
				this._origin = value;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x0002F90B File Offset: 0x0002DB0B
		// (set) Token: 0x06001873 RID: 6259 RVA: 0x0002F913 File Offset: 0x0002DB13
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

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x0002F91C File Offset: 0x0002DB1C
		// (set) Token: 0x06001875 RID: 6261 RVA: 0x0002F924 File Offset: 0x0002DB24
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string resourceScopeType
		{
			get
			{
				return this._resourceScopeType;
			}
			set
			{
				this._resourceScopeType = value;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x0002F92D File Offset: 0x0002DB2D
		// (set) Token: 0x06001877 RID: 6263 RVA: 0x0002F935 File Offset: 0x0002DB35
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string userConsentDescription
		{
			get
			{
				return this._userConsentDescription;
			}
			set
			{
				this._userConsentDescription = value;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x0002F93E File Offset: 0x0002DB3E
		// (set) Token: 0x06001879 RID: 6265 RVA: 0x0002F946 File Offset: 0x0002DB46
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string userConsentDisplayName
		{
			get
			{
				return this._userConsentDisplayName;
			}
			set
			{
				this._userConsentDisplayName = value;
			}
		}

		// Token: 0x04001B10 RID: 6928
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _claimValue;

		// Token: 0x04001B11 RID: 6929
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _description;

		// Token: 0x04001B12 RID: 6930
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _directAccessGrantTypes = new Collection<string>();

		// Token: 0x04001B13 RID: 6931
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001B14 RID: 6932
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ImpersonationAccessGrantType> _impersonationAccessGrantTypes = new Collection<ImpersonationAccessGrantType>();

		// Token: 0x04001B15 RID: 6933
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _isDisabled;

		// Token: 0x04001B16 RID: 6934
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _origin;

		// Token: 0x04001B17 RID: 6935
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid _permissionId;

		// Token: 0x04001B18 RID: 6936
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _resourceScopeType;

		// Token: 0x04001B19 RID: 6937
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _userConsentDescription;

		// Token: 0x04001B1A RID: 6938
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _userConsentDisplayName;
	}
}
