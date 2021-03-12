using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000392 RID: 914
	[Serializable]
	public class ADSnackyServiceVirtualDirectory : ADExchangeServiceVirtualDirectory
	{
		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x060029B8 RID: 10680 RVA: 0x000AEF6C File Offset: 0x000AD16C
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADSnackyServiceVirtualDirectory.MostDerivedClass;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x060029B9 RID: 10681 RVA: 0x000AEF73 File Offset: 0x000AD173
		// (set) Token: 0x060029BA RID: 10682 RVA: 0x000AEF85 File Offset: 0x000AD185
		public bool LiveIdAuthentication
		{
			get
			{
				return (bool)this[ADSnackyServiceVirtualDirectorySchema.LiveIdAuthentication];
			}
			set
			{
				this[ADSnackyServiceVirtualDirectorySchema.LiveIdAuthentication] = value;
			}
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x000AEF98 File Offset: 0x000AD198
		internal static object LiveIdAuthenticationGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<AuthenticationMethod> multiValuedProperty = (MultiValuedProperty<AuthenticationMethod>)propertyBag[ADVirtualDirectorySchema.InternalAuthenticationMethods];
			return multiValuedProperty.Contains(AuthenticationMethod.LiveIdFba);
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x000AEFC4 File Offset: 0x000AD1C4
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

		// Token: 0x0400197E RID: 6526
		internal const string VDirName = "SnackyService";

		// Token: 0x0400197F RID: 6527
		internal const string FrontEndWebSiteName = "Default Web Site";

		// Token: 0x04001980 RID: 6528
		internal const string BackEndWebSiteName = "Exchange Back End";

		// Token: 0x04001981 RID: 6529
		public static readonly string MostDerivedClass = "msExchVirtualDirectory";
	}
}
