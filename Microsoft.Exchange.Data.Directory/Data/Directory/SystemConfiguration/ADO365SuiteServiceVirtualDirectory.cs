using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000377 RID: 887
	[Serializable]
	public sealed class ADO365SuiteServiceVirtualDirectory : ADExchangeServiceVirtualDirectory
	{
		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06002926 RID: 10534 RVA: 0x000AD680 File Offset: 0x000AB880
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADO365SuiteServiceVirtualDirectory.MostDerivedClass;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06002927 RID: 10535 RVA: 0x000AD687 File Offset: 0x000AB887
		// (set) Token: 0x06002928 RID: 10536 RVA: 0x000AD699 File Offset: 0x000AB899
		public bool LiveIdAuthentication
		{
			get
			{
				return (bool)this[ADO365SuiteServiceVirtualDirectorySchema.LiveIdAuthentication];
			}
			set
			{
				this[ADO365SuiteServiceVirtualDirectorySchema.LiveIdAuthentication] = value;
			}
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x000AD6AC File Offset: 0x000AB8AC
		internal static object LiveIdAuthenticationGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<AuthenticationMethod> multiValuedProperty = (MultiValuedProperty<AuthenticationMethod>)propertyBag[ADVirtualDirectorySchema.InternalAuthenticationMethods];
			return multiValuedProperty.Contains(AuthenticationMethod.LiveIdFba);
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x000AD6D8 File Offset: 0x000AB8D8
		internal static void LiveIdAuthenticationSetter(object value, IPropertyBag propertyBag)
		{
			List<AuthenticationMethod> list = new List<AuthenticationMethod>();
			MultiValuedProperty<AuthenticationMethod> multiValuedProperty = (MultiValuedProperty<AuthenticationMethod>)propertyBag[ADVirtualDirectorySchema.InternalAuthenticationMethods];
			if (multiValuedProperty != null)
			{
				list.AddRange(multiValuedProperty);
			}
			ADExchangeServiceVirtualDirectory.AddOrRemoveAuthenticationMethod(list, new bool?((bool)value), new AuthenticationMethod[]
			{
				AuthenticationMethod.LiveIdFba
			});
			MultiValuedProperty<AuthenticationMethod> value2 = new MultiValuedProperty<AuthenticationMethod>(list);
			propertyBag[ADVirtualDirectorySchema.InternalAuthenticationMethods] = value2;
			propertyBag[ADVirtualDirectorySchema.ExternalAuthenticationMethods] = value2;
		}

		// Token: 0x04001922 RID: 6434
		internal const string VDirName = "O365SuiteService";

		// Token: 0x04001923 RID: 6435
		internal const string FrontEndWebSiteName = "Default Web Site";

		// Token: 0x04001924 RID: 6436
		internal const string BackEndWebSiteName = "Exchange Back End";

		// Token: 0x04001925 RID: 6437
		public static readonly string MostDerivedClass = "msExchVirtualDirectory";
	}
}
