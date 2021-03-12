using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003B0 RID: 944
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class AvailabilityAddressSpace : ADConfigurationObject
	{
		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06002B2D RID: 11053 RVA: 0x000B38CE File Offset: 0x000B1ACE
		internal override ADObjectSchema Schema
		{
			get
			{
				return AvailabilityAddressSpace.schema;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06002B2E RID: 11054 RVA: 0x000B38D5 File Offset: 0x000B1AD5
		internal override string MostDerivedObjectClass
		{
			get
			{
				return AvailabilityAddressSpace.mostDerivedClass;
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06002B2F RID: 11055 RVA: 0x000B38DC File Offset: 0x000B1ADC
		internal override ADObjectId ParentPath
		{
			get
			{
				return AvailabilityConfig.Container;
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06002B31 RID: 11057 RVA: 0x000B38EB File Offset: 0x000B1AEB
		// (set) Token: 0x06002B32 RID: 11058 RVA: 0x000B38FD File Offset: 0x000B1AFD
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true)]
		public string ForestName
		{
			get
			{
				return (string)this[AvailabilityAddressSpaceSchema.ForestName];
			}
			set
			{
				this[AvailabilityAddressSpaceSchema.ForestName] = value;
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06002B33 RID: 11059 RVA: 0x000B390B File Offset: 0x000B1B0B
		// (set) Token: 0x06002B34 RID: 11060 RVA: 0x000B391D File Offset: 0x000B1B1D
		public string UserName
		{
			get
			{
				return (string)this[AvailabilityAddressSpaceSchema.UserName];
			}
			internal set
			{
				this[AvailabilityAddressSpaceSchema.UserName] = value;
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06002B35 RID: 11061 RVA: 0x000B392B File Offset: 0x000B1B2B
		// (set) Token: 0x06002B36 RID: 11062 RVA: 0x000B393D File Offset: 0x000B1B3D
		[Parameter(Mandatory = false)]
		public bool UseServiceAccount
		{
			get
			{
				return (bool)this[AvailabilityAddressSpaceSchema.UseServiceAccount];
			}
			set
			{
				this[AvailabilityAddressSpaceSchema.UseServiceAccount] = value;
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06002B37 RID: 11063 RVA: 0x000B3950 File Offset: 0x000B1B50
		// (set) Token: 0x06002B38 RID: 11064 RVA: 0x000B3962 File Offset: 0x000B1B62
		[Parameter(Mandatory = true)]
		public AvailabilityAccessMethod AccessMethod
		{
			get
			{
				return (AvailabilityAccessMethod)this[AvailabilityAddressSpaceSchema.AccessMethod];
			}
			set
			{
				this[AvailabilityAddressSpaceSchema.AccessMethod] = value;
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06002B39 RID: 11065 RVA: 0x000B3975 File Offset: 0x000B1B75
		// (set) Token: 0x06002B3A RID: 11066 RVA: 0x000B3987 File Offset: 0x000B1B87
		[Parameter(Mandatory = false)]
		public Uri ProxyUrl
		{
			get
			{
				return (Uri)this[AvailabilityAddressSpaceSchema.ProxyUrl];
			}
			set
			{
				this[AvailabilityAddressSpaceSchema.ProxyUrl] = value;
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x000B3995 File Offset: 0x000B1B95
		// (set) Token: 0x06002B3C RID: 11068 RVA: 0x000B39A7 File Offset: 0x000B1BA7
		[Parameter(Mandatory = false)]
		public Uri TargetAutodiscoverEpr
		{
			get
			{
				return (Uri)this[AvailabilityAddressSpaceSchema.TargetAutodiscoverEpr];
			}
			set
			{
				this[AvailabilityAddressSpaceSchema.TargetAutodiscoverEpr] = value;
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06002B3D RID: 11069 RVA: 0x000B39B5 File Offset: 0x000B1BB5
		public ADObjectId ParentPathId
		{
			get
			{
				return AvailabilityConfig.Container;
			}
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x000B39BC File Offset: 0x000B1BBC
		internal SecureString GetPassword()
		{
			SecureString secureString = new SecureString();
			string text = (string)this[AvailabilityAddressSpaceSchema.Password];
			if (!string.IsNullOrEmpty(text))
			{
				for (int i = 0; i < text.Length; i++)
				{
					secureString.AppendChar(text[i]);
				}
			}
			return secureString;
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x000B3A07 File Offset: 0x000B1C07
		internal void SetPassword(SecureString securePassword)
		{
			if (securePassword != null)
			{
				this[AvailabilityAddressSpaceSchema.Password] = securePassword.ConvertToUnsecureString();
			}
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x000B3A20 File Offset: 0x000B1C20
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.UseServiceAccount && !string.IsNullOrEmpty(this.UserName))
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ASOnlyOneAuthenticationMethodAllowed, base.Id, string.Empty));
			}
			AvailabilityAccessMethod accessMethod = this.AccessMethod;
			switch (accessMethod)
			{
			case AvailabilityAccessMethod.PerUserFB:
			case AvailabilityAccessMethod.OrgWideFB:
			case AvailabilityAccessMethod.OrgWideFBBasic:
				if (!this.UseServiceAccount && string.IsNullOrEmpty(this.UserName))
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ASAccessMethodNeedsAuthenticationAccount, base.Id, string.Empty));
				}
				break;
			case AvailabilityAccessMethod.PublicFolder:
				if (this.UseServiceAccount || !string.IsNullOrEmpty(this.UserName))
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ASInvalidAuthenticationOptionsForAccessMethod, base.Id, string.Empty));
				}
				break;
			case AvailabilityAccessMethod.InternalProxy:
				if (!this.UseServiceAccount)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ASAccessMethodNeedsAuthenticationAccount, base.Id, string.Empty));
				}
				break;
			default:
				errors.Add(new ObjectValidationError(DirectoryStrings.ASInvalidAccessMethod, base.Id, string.Empty));
				break;
			}
			Uri proxyUrl = this.ProxyUrl;
			if (proxyUrl != null && accessMethod != AvailabilityAccessMethod.InternalProxy)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ASInvalidProxyASUrlOption, base.Id, string.Empty));
			}
		}

		// Token: 0x040019F0 RID: 6640
		private static AvailabilityAddressSpaceSchema schema = ObjectSchema.GetInstance<AvailabilityAddressSpaceSchema>();

		// Token: 0x040019F1 RID: 6641
		private static string mostDerivedClass = "msExchAvailabilityAddressSpace";
	}
}
