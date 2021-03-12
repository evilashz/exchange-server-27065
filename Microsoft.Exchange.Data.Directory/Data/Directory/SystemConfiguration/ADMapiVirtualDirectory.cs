using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000349 RID: 841
	[Serializable]
	public sealed class ADMapiVirtualDirectory : ExchangeVirtualDirectory
	{
		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x060026F8 RID: 9976 RVA: 0x000A51D9 File Offset: 0x000A33D9
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADMapiVirtualDirectory.schema;
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x060026F9 RID: 9977 RVA: 0x000A51E0 File Offset: 0x000A33E0
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADMapiVirtualDirectory.MostDerivedClass;
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x060026FA RID: 9978 RVA: 0x000A51E7 File Offset: 0x000A33E7
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x060026FB RID: 9979 RVA: 0x000A51FA File Offset: 0x000A33FA
		// (set) Token: 0x060026FC RID: 9980 RVA: 0x000A520C File Offset: 0x000A340C
		public MultiValuedProperty<AuthenticationMethod> IISAuthenticationMethods
		{
			get
			{
				return (MultiValuedProperty<AuthenticationMethod>)this[ADMapiVirtualDirectorySchema.IISAuthenticationMethods];
			}
			set
			{
				this[ADMapiVirtualDirectorySchema.IISAuthenticationMethods] = value;
			}
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x000A521A File Offset: 0x000A341A
		internal static object GetIISAuthenticationMethods(IPropertyBag propertyBag)
		{
			return ADVirtualDirectory.InternalAuthenticationMethodsGetter(propertyBag);
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x000A5222 File Offset: 0x000A3422
		internal static void SetIISAuthenticationMethods(object value, IPropertyBag propertyBag)
		{
			ADVirtualDirectory.InternalAuthenticationMethodsSetter(value, propertyBag);
			ADVirtualDirectory.ExternalAuthenticationMethodsSetter(value, propertyBag);
		}

		// Token: 0x040017C3 RID: 6083
		private static readonly ADMapiVirtualDirectorySchema schema = ObjectSchema.GetInstance<ADMapiVirtualDirectorySchema>();

		// Token: 0x040017C4 RID: 6084
		public static readonly string MostDerivedClass = "msExchMapiVirtualDirectory";
	}
}
