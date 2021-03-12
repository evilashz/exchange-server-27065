using System;
using System.Collections;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000362 RID: 866
	[Serializable]
	public class ExchangeWebAppVirtualDirectory : ExchangeVirtualDirectory
	{
		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x060027C4 RID: 10180 RVA: 0x000A765E File Offset: 0x000A585E
		internal override ADObjectSchema Schema
		{
			get
			{
				return ExchangeWebAppVirtualDirectory.schema;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x060027C5 RID: 10181 RVA: 0x000A7665 File Offset: 0x000A5865
		// (set) Token: 0x060027C6 RID: 10182 RVA: 0x000A766D File Offset: 0x000A586D
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

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x060027C7 RID: 10183 RVA: 0x000A7676 File Offset: 0x000A5876
		public new MultiValuedProperty<AuthenticationMethod> InternalAuthenticationMethods
		{
			get
			{
				return base.InternalAuthenticationMethods;
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x060027C8 RID: 10184 RVA: 0x000A767E File Offset: 0x000A587E
		public new string MetabasePath
		{
			get
			{
				return base.MetabasePath;
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x060027C9 RID: 10185 RVA: 0x000A7686 File Offset: 0x000A5886
		// (set) Token: 0x060027CA RID: 10186 RVA: 0x000A7698 File Offset: 0x000A5898
		public bool BasicAuthentication
		{
			get
			{
				return (bool)this[ExchangeWebAppVirtualDirectorySchema.BasicAuthentication];
			}
			set
			{
				this[ExchangeWebAppVirtualDirectorySchema.BasicAuthentication] = value;
				ExchangeWebAppVirtualDirectory.SetAuthenticationMethodHelper(value, this, AuthenticationMethod.Basic);
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x000A76B3 File Offset: 0x000A58B3
		// (set) Token: 0x060027CC RID: 10188 RVA: 0x000A76C5 File Offset: 0x000A58C5
		public bool WindowsAuthentication
		{
			get
			{
				return (bool)this[ExchangeWebAppVirtualDirectorySchema.WindowsAuthentication];
			}
			set
			{
				this[ExchangeWebAppVirtualDirectorySchema.WindowsAuthentication] = value;
				ExchangeWebAppVirtualDirectory.SetAuthenticationMethodHelper(value, this, AuthenticationMethod.WindowsIntegrated);
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x060027CD RID: 10189 RVA: 0x000A76E0 File Offset: 0x000A58E0
		// (set) Token: 0x060027CE RID: 10190 RVA: 0x000A76F2 File Offset: 0x000A58F2
		public bool DigestAuthentication
		{
			get
			{
				return (bool)this[ExchangeWebAppVirtualDirectorySchema.DigestAuthentication];
			}
			set
			{
				this[ExchangeWebAppVirtualDirectorySchema.DigestAuthentication] = value;
				ExchangeWebAppVirtualDirectory.SetAuthenticationMethodHelper(value, this, AuthenticationMethod.Digest);
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x060027CF RID: 10191 RVA: 0x000A770D File Offset: 0x000A590D
		// (set) Token: 0x060027D0 RID: 10192 RVA: 0x000A771F File Offset: 0x000A591F
		public bool FormsAuthentication
		{
			get
			{
				return (bool)this[ExchangeWebAppVirtualDirectorySchema.FormsAuthentication];
			}
			set
			{
				this[ExchangeWebAppVirtualDirectorySchema.FormsAuthentication] = value;
				ExchangeWebAppVirtualDirectory.SetAuthenticationMethodHelper(value, this, AuthenticationMethod.Fba);
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x060027D1 RID: 10193 RVA: 0x000A773A File Offset: 0x000A593A
		// (set) Token: 0x060027D2 RID: 10194 RVA: 0x000A774C File Offset: 0x000A594C
		public bool LiveIdAuthentication
		{
			get
			{
				return (bool)this[ExchangeWebAppVirtualDirectorySchema.LiveIdAuthentication];
			}
			set
			{
				this[ExchangeWebAppVirtualDirectorySchema.LiveIdAuthentication] = value;
				ExchangeWebAppVirtualDirectory.SetAuthenticationMethodHelper(value, this, AuthenticationMethod.LiveIdFba);
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x060027D3 RID: 10195 RVA: 0x000A7767 File Offset: 0x000A5967
		// (set) Token: 0x060027D4 RID: 10196 RVA: 0x000A7779 File Offset: 0x000A5979
		public bool AdfsAuthentication
		{
			get
			{
				return (bool)this[ExchangeWebAppVirtualDirectorySchema.AdfsAuthentication];
			}
			set
			{
				this[ExchangeWebAppVirtualDirectorySchema.AdfsAuthentication] = value;
				ExchangeWebAppVirtualDirectory.SetAuthenticationMethodHelper(value, this, AuthenticationMethod.Adfs);
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x060027D5 RID: 10197 RVA: 0x000A7795 File Offset: 0x000A5995
		// (set) Token: 0x060027D6 RID: 10198 RVA: 0x000A77A7 File Offset: 0x000A59A7
		public bool OAuthAuthentication
		{
			get
			{
				return (bool)this[ExchangeWebAppVirtualDirectorySchema.OAuthAuthentication];
			}
			set
			{
				this[ExchangeWebAppVirtualDirectorySchema.OAuthAuthentication] = value;
				ExchangeWebAppVirtualDirectory.SetAuthenticationMethodHelper(value, this, AuthenticationMethod.OAuth);
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x060027D7 RID: 10199 RVA: 0x000A77C3 File Offset: 0x000A59C3
		// (set) Token: 0x060027D8 RID: 10200 RVA: 0x000A77D5 File Offset: 0x000A59D5
		public string DefaultDomain
		{
			get
			{
				return (string)this[ExchangeWebAppVirtualDirectorySchema.DefaultDomain];
			}
			set
			{
				this[ExchangeWebAppVirtualDirectorySchema.DefaultDomain] = value;
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x060027D9 RID: 10201 RVA: 0x000A77E3 File Offset: 0x000A59E3
		// (set) Token: 0x060027DA RID: 10202 RVA: 0x000A77F5 File Offset: 0x000A59F5
		public GzipLevel GzipLevel
		{
			get
			{
				return (GzipLevel)this[ExchangeWebAppVirtualDirectorySchema.ADGzipLevel];
			}
			set
			{
				this[ExchangeWebAppVirtualDirectorySchema.ADGzipLevel] = value;
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x060027DB RID: 10203 RVA: 0x000A7808 File Offset: 0x000A5A08
		// (set) Token: 0x060027DC RID: 10204 RVA: 0x000A781A File Offset: 0x000A5A1A
		public string WebSite
		{
			get
			{
				return (string)this[ExchangeWebAppVirtualDirectorySchema.WebSite];
			}
			internal set
			{
				this[ExchangeWebAppVirtualDirectorySchema.WebSite] = value;
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x060027DD RID: 10205 RVA: 0x000A7828 File Offset: 0x000A5A28
		// (set) Token: 0x060027DE RID: 10206 RVA: 0x000A783A File Offset: 0x000A5A3A
		public string DisplayName
		{
			get
			{
				return (string)this[ExchangeWebAppVirtualDirectorySchema.DisplayName];
			}
			internal set
			{
				this[ExchangeWebAppVirtualDirectorySchema.DisplayName] = value;
			}
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x000A7848 File Offset: 0x000A5A48
		internal static object LiveIdAuthenticationGetter(IPropertyBag propertyBag)
		{
			return ExchangeWebAppVirtualDirectory.GetAuthenticationMethodHelper(propertyBag, AuthenticationMethod.LiveIdFba);
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x000A7856 File Offset: 0x000A5A56
		internal static void LiveIdAuthenticationSetter(object value, IPropertyBag propertyBag)
		{
			ExchangeWebAppVirtualDirectory.SetAuthenticationMethodHelper((bool)value, propertyBag, AuthenticationMethod.LiveIdFba);
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x000A7865 File Offset: 0x000A5A65
		internal static object AdfsAuthenticationGetter(IPropertyBag propertyBag)
		{
			return ExchangeWebAppVirtualDirectory.GetAuthenticationMethodHelper(propertyBag, AuthenticationMethod.Adfs);
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x000A7874 File Offset: 0x000A5A74
		internal static void AdfsAuthenticationSetter(object value, IPropertyBag propertyBag)
		{
			ExchangeWebAppVirtualDirectory.SetAuthenticationMethodHelper((bool)value, propertyBag, AuthenticationMethod.Adfs);
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x000A7884 File Offset: 0x000A5A84
		internal static object OAuthAuthenticationGetter(IPropertyBag propertyBag)
		{
			return ExchangeWebAppVirtualDirectory.GetAuthenticationMethodHelper(propertyBag, AuthenticationMethod.OAuth);
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x000A7893 File Offset: 0x000A5A93
		internal static void OAuthAuthenticationSetter(object value, IPropertyBag propertyBag)
		{
			ExchangeWebAppVirtualDirectory.SetAuthenticationMethodHelper((bool)value, propertyBag, AuthenticationMethod.OAuth);
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x000A78A4 File Offset: 0x000A5AA4
		internal static void SetAuthenticationMethodHelper(bool value, IPropertyBag propertyBag, AuthenticationMethod method)
		{
			bool flag = ExchangeWebAppVirtualDirectory.GetAuthenticationMethodHelper(propertyBag, AuthenticationMethod.Basic);
			bool flag2 = ExchangeWebAppVirtualDirectory.GetAuthenticationMethodHelper(propertyBag, AuthenticationMethod.Digest);
			bool flag3 = ExchangeWebAppVirtualDirectory.GetAuthenticationMethodHelper(propertyBag, AuthenticationMethod.WindowsIntegrated);
			bool flag4 = ExchangeWebAppVirtualDirectory.GetAuthenticationMethodHelper(propertyBag, AuthenticationMethod.Fba);
			bool flag5 = ExchangeWebAppVirtualDirectory.GetAuthenticationMethodHelper(propertyBag, AuthenticationMethod.LiveIdFba);
			bool flag6 = ExchangeWebAppVirtualDirectory.GetAuthenticationMethodHelper(propertyBag, AuthenticationMethod.Adfs);
			bool flag7 = ExchangeWebAppVirtualDirectory.GetAuthenticationMethodHelper(propertyBag, AuthenticationMethod.OAuth);
			switch (method)
			{
			case AuthenticationMethod.Basic:
				flag = value;
				if (!flag)
				{
					flag4 = false;
				}
				else
				{
					flag5 = false;
				}
				break;
			case AuthenticationMethod.Digest:
				flag2 = value;
				if (flag2)
				{
					flag4 = false;
					flag5 = false;
				}
				break;
			case AuthenticationMethod.Fba:
				flag4 = value;
				if (!flag4)
				{
					flag = false;
				}
				else
				{
					flag = true;
					flag2 = false;
					flag3 = false;
					flag5 = false;
				}
				break;
			case AuthenticationMethod.WindowsIntegrated:
				flag3 = value;
				if (flag3)
				{
					flag4 = false;
					flag5 = false;
				}
				break;
			case AuthenticationMethod.LiveIdFba:
				flag5 = value;
				if (flag5)
				{
					flag = false;
					flag2 = false;
					flag3 = false;
					flag4 = false;
					flag6 = false;
				}
				break;
			case AuthenticationMethod.OAuth:
				flag7 = value;
				break;
			case AuthenticationMethod.Adfs:
				flag6 = value;
				if (flag6)
				{
					flag5 = false;
				}
				break;
			}
			propertyBag[ExchangeWebAppVirtualDirectorySchema.WindowsAuthentication] = flag3;
			propertyBag[ExchangeWebAppVirtualDirectorySchema.BasicAuthentication] = flag;
			propertyBag[ExchangeWebAppVirtualDirectorySchema.DigestAuthentication] = flag2;
			propertyBag[ExchangeWebAppVirtualDirectorySchema.FormsAuthentication] = flag4;
			ArrayList arrayList = new ArrayList();
			if (flag)
			{
				arrayList.Add(AuthenticationMethod.Basic);
			}
			if (flag2)
			{
				arrayList.Add(AuthenticationMethod.Digest);
			}
			if (flag3)
			{
				arrayList.Add(AuthenticationMethod.WindowsIntegrated);
				arrayList.Add(AuthenticationMethod.Ntlm);
			}
			if (flag4)
			{
				arrayList.Add(AuthenticationMethod.Fba);
			}
			if (flag5)
			{
				arrayList.Add(AuthenticationMethod.LiveIdFba);
			}
			if (flag6)
			{
				arrayList.Add(AuthenticationMethod.Adfs);
			}
			if (flag7)
			{
				arrayList.Add(AuthenticationMethod.OAuth);
			}
			MultiValuedProperty<AuthenticationMethod> value2 = new MultiValuedProperty<AuthenticationMethod>(arrayList);
			propertyBag[ADVirtualDirectorySchema.InternalAuthenticationMethods] = value2;
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x000A7A74 File Offset: 0x000A5C74
		internal static bool GetAuthenticationMethodHelper(IPropertyBag propertyBag, AuthenticationMethod method)
		{
			MultiValuedProperty<AuthenticationMethod> multiValuedProperty = (MultiValuedProperty<AuthenticationMethod>)propertyBag[ADVirtualDirectorySchema.InternalAuthenticationMethods];
			return multiValuedProperty.Contains(method);
		}

		// Token: 0x04001843 RID: 6211
		private static readonly ExchangeWebAppVirtualDirectorySchema schema = ObjectSchema.GetInstance<ExchangeWebAppVirtualDirectorySchema>();
	}
}
