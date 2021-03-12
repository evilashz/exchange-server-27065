using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000381 RID: 897
	[Serializable]
	public class ADPushNotificationsVirtualDirectory : ADExchangeServiceVirtualDirectory
	{
		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x000AD8E0 File Offset: 0x000ABAE0
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADPushNotificationsVirtualDirectory.MostDerivedClassName;
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x000AD8E7 File Offset: 0x000ABAE7
		// (set) Token: 0x06002948 RID: 10568 RVA: 0x000AD8F9 File Offset: 0x000ABAF9
		public bool LiveIdAuthentication
		{
			get
			{
				return (bool)this[ADPushNotificationsVirtualDirectorySchema.LiveIdAuthentication];
			}
			set
			{
				this[ADPushNotificationsVirtualDirectorySchema.LiveIdAuthentication] = value;
			}
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x000AD90C File Offset: 0x000ABB0C
		internal static object LiveIdAuthenticationGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<AuthenticationMethod> multiValuedProperty = (MultiValuedProperty<AuthenticationMethod>)propertyBag[ADVirtualDirectorySchema.InternalAuthenticationMethods];
			return multiValuedProperty.Contains(AuthenticationMethod.LiveIdFba);
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x000AD938 File Offset: 0x000ABB38
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

		// Token: 0x04001931 RID: 6449
		internal const string VDirName = "PushNotifications";

		// Token: 0x04001932 RID: 6450
		internal const string FrontEndWebSiteName = "Default Web Site";

		// Token: 0x04001933 RID: 6451
		internal const string BackEndWebSiteName = "Exchange Back End";

		// Token: 0x04001934 RID: 6452
		private static readonly string MostDerivedClassName = "msExchPushNotificationsVirtualDirectory";
	}
}
