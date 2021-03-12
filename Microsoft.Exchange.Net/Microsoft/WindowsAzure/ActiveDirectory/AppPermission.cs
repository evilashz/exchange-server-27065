using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x02000594 RID: 1428
	public class AppPermission
	{
		// Token: 0x06001381 RID: 4993 RVA: 0x0002BAF4 File Offset: 0x00029CF4
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

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x0002BB40 File Offset: 0x00029D40
		// (set) Token: 0x06001383 RID: 4995 RVA: 0x0002BB48 File Offset: 0x00029D48
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

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x0002BB51 File Offset: 0x00029D51
		// (set) Token: 0x06001385 RID: 4997 RVA: 0x0002BB59 File Offset: 0x00029D59
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

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x0002BB62 File Offset: 0x00029D62
		// (set) Token: 0x06001387 RID: 4999 RVA: 0x0002BB6A File Offset: 0x00029D6A
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

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x0002BB73 File Offset: 0x00029D73
		// (set) Token: 0x06001389 RID: 5001 RVA: 0x0002BB7B File Offset: 0x00029D7B
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

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x0002BB84 File Offset: 0x00029D84
		// (set) Token: 0x0600138B RID: 5003 RVA: 0x0002BB8C File Offset: 0x00029D8C
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

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x0002BB95 File Offset: 0x00029D95
		// (set) Token: 0x0600138D RID: 5005 RVA: 0x0002BB9D File Offset: 0x00029D9D
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

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x0002BBA6 File Offset: 0x00029DA6
		// (set) Token: 0x0600138F RID: 5007 RVA: 0x0002BBAE File Offset: 0x00029DAE
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

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x0002BBB7 File Offset: 0x00029DB7
		// (set) Token: 0x06001391 RID: 5009 RVA: 0x0002BBBF File Offset: 0x00029DBF
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

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x0002BBC8 File Offset: 0x00029DC8
		// (set) Token: 0x06001393 RID: 5011 RVA: 0x0002BBD0 File Offset: 0x00029DD0
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

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x0002BBD9 File Offset: 0x00029DD9
		// (set) Token: 0x06001395 RID: 5013 RVA: 0x0002BBE1 File Offset: 0x00029DE1
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

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x0002BBEA File Offset: 0x00029DEA
		// (set) Token: 0x06001397 RID: 5015 RVA: 0x0002BBF2 File Offset: 0x00029DF2
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

		// Token: 0x040018D0 RID: 6352
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _claimValue;

		// Token: 0x040018D1 RID: 6353
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _description;

		// Token: 0x040018D2 RID: 6354
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _directAccessGrantTypes = new Collection<string>();

		// Token: 0x040018D3 RID: 6355
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x040018D4 RID: 6356
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ImpersonationAccessGrantType> _impersonationAccessGrantTypes = new Collection<ImpersonationAccessGrantType>();

		// Token: 0x040018D5 RID: 6357
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _isDisabled;

		// Token: 0x040018D6 RID: 6358
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _origin;

		// Token: 0x040018D7 RID: 6359
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid _permissionId;

		// Token: 0x040018D8 RID: 6360
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _resourceScopeType;

		// Token: 0x040018D9 RID: 6361
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _userConsentDescription;

		// Token: 0x040018DA RID: 6362
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _userConsentDisplayName;
	}
}
