using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000357 RID: 855
	[ObjectScope(ConfigScopes.Server)]
	[Serializable]
	public sealed class ADOabVirtualDirectory : ExchangeVirtualDirectory
	{
		// Token: 0x0600277B RID: 10107 RVA: 0x000A6878 File Offset: 0x000A4A78
		private void AddOrRemoveAuthenticationMethod(List<AuthenticationMethod> authenticationMethods, bool authenticationMethodFlag, params AuthenticationMethod[] applicableAuthenticationMethods)
		{
			if (authenticationMethodFlag)
			{
				foreach (AuthenticationMethod item in applicableAuthenticationMethods)
				{
					if (!authenticationMethods.Contains(item))
					{
						authenticationMethods.Add(item);
					}
				}
				return;
			}
			foreach (AuthenticationMethod item2 in applicableAuthenticationMethods)
			{
				authenticationMethods.Remove(item2);
			}
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x000A68D4 File Offset: 0x000A4AD4
		private void UpdateInternalAndExternalAuthenticationMethods()
		{
			List<AuthenticationMethod> list = new List<AuthenticationMethod>();
			if (base.InternalAuthenticationMethods != null)
			{
				list.AddRange(base.InternalAuthenticationMethods);
			}
			List<AuthenticationMethod> authenticationMethods = list;
			bool basicAuthentication = this.BasicAuthentication;
			AuthenticationMethod[] applicableAuthenticationMethods = new AuthenticationMethod[1];
			this.AddOrRemoveAuthenticationMethod(authenticationMethods, basicAuthentication, applicableAuthenticationMethods);
			this.AddOrRemoveAuthenticationMethod(list, this.WindowsAuthentication, new AuthenticationMethod[]
			{
				AuthenticationMethod.WindowsIntegrated
			});
			this.AddOrRemoveAuthenticationMethod(list, this.OAuthAuthentication, new AuthenticationMethod[]
			{
				AuthenticationMethod.OAuth
			});
			MultiValuedProperty<AuthenticationMethod> multiValuedProperty = new MultiValuedProperty<AuthenticationMethod>(list);
			base.InternalAuthenticationMethods = multiValuedProperty;
			base.ExternalAuthenticationMethods = multiValuedProperty;
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x0600277D RID: 10109 RVA: 0x000A695B File Offset: 0x000A4B5B
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADOabVirtualDirectory.schema;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x0600277E RID: 10110 RVA: 0x000A6962 File Offset: 0x000A4B62
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADOabVirtualDirectory.MostDerivedClass;
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x0600277F RID: 10111 RVA: 0x000A6969 File Offset: 0x000A4B69
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06002780 RID: 10112 RVA: 0x000A697C File Offset: 0x000A4B7C
		// (set) Token: 0x06002781 RID: 10113 RVA: 0x000A6984 File Offset: 0x000A4B84
		public new string Name
		{
			get
			{
				return base.Name;
			}
			internal set
			{
				base.Name = value;
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06002782 RID: 10114 RVA: 0x000A698D File Offset: 0x000A4B8D
		// (set) Token: 0x06002783 RID: 10115 RVA: 0x000A699F File Offset: 0x000A4B9F
		[Parameter]
		public int PollInterval
		{
			get
			{
				return (int)this[ADOabVirtualDirectorySchema.PollInterval];
			}
			set
			{
				this[ADOabVirtualDirectorySchema.PollInterval] = value;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06002784 RID: 10116 RVA: 0x000A69B2 File Offset: 0x000A4BB2
		public MultiValuedProperty<ADObjectId> OfflineAddressBooks
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADOabVirtualDirectorySchema.OfflineAddressBooks];
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06002785 RID: 10117 RVA: 0x000A69C4 File Offset: 0x000A4BC4
		// (set) Token: 0x06002786 RID: 10118 RVA: 0x000A69D6 File Offset: 0x000A4BD6
		public bool RequireSSL
		{
			get
			{
				return (bool)this[ADOabVirtualDirectorySchema.RequireSSL];
			}
			internal set
			{
				this[ADOabVirtualDirectorySchema.RequireSSL] = value;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06002787 RID: 10119 RVA: 0x000A69E9 File Offset: 0x000A4BE9
		// (set) Token: 0x06002788 RID: 10120 RVA: 0x000A69FB File Offset: 0x000A4BFB
		public bool BasicAuthentication
		{
			get
			{
				return (bool)this[ADOabVirtualDirectorySchema.BasicAuthentication];
			}
			internal set
			{
				this[ADOabVirtualDirectorySchema.BasicAuthentication] = value;
				this.UpdateInternalAndExternalAuthenticationMethods();
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06002789 RID: 10121 RVA: 0x000A6A14 File Offset: 0x000A4C14
		// (set) Token: 0x0600278A RID: 10122 RVA: 0x000A6A26 File Offset: 0x000A4C26
		public bool WindowsAuthentication
		{
			get
			{
				return (bool)this[ADOabVirtualDirectorySchema.WindowsAuthentication];
			}
			internal set
			{
				this[ADOabVirtualDirectorySchema.WindowsAuthentication] = value;
				this.UpdateInternalAndExternalAuthenticationMethods();
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x0600278B RID: 10123 RVA: 0x000A6A3F File Offset: 0x000A4C3F
		// (set) Token: 0x0600278C RID: 10124 RVA: 0x000A6A51 File Offset: 0x000A4C51
		public bool OAuthAuthentication
		{
			get
			{
				return (bool)this[ADOabVirtualDirectorySchema.OAuthAuthentication];
			}
			internal set
			{
				this[ADOabVirtualDirectorySchema.OAuthAuthentication] = value;
				this.UpdateInternalAndExternalAuthenticationMethods();
			}
		}

		// Token: 0x04001814 RID: 6164
		private static readonly ADOabVirtualDirectorySchema schema = ObjectSchema.GetInstance<ADOabVirtualDirectorySchema>();

		// Token: 0x04001815 RID: 6165
		public static readonly string MostDerivedClass = "msExchOabVirtualDirectory";
	}
}
