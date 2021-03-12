using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002A8 RID: 680
	[Serializable]
	public class AuthRedirect : ADNonExchangeObject
	{
		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001F89 RID: 8073 RVA: 0x0008C0CF File Offset: 0x0008A2CF
		internal override ADObjectSchema Schema
		{
			get
			{
				return AuthRedirect.SchemaObject;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001F8A RID: 8074 RVA: 0x0008C0D6 File Offset: 0x0008A2D6
		internal override string MostDerivedObjectClass
		{
			get
			{
				return AuthRedirect.MostDerivedClass;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001F8B RID: 8075 RVA: 0x0008C0DD File Offset: 0x0008A2DD
		// (set) Token: 0x06001F8C RID: 8076 RVA: 0x0008C0E5 File Offset: 0x0008A2E5
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001F8D RID: 8077 RVA: 0x0008C0EE File Offset: 0x0008A2EE
		// (set) Token: 0x06001F8E RID: 8078 RVA: 0x0008C100 File Offset: 0x0008A300
		public AuthScheme AuthScheme
		{
			get
			{
				return (AuthScheme)this[AuthRedirectSchema.AuthScheme];
			}
			set
			{
				this[AuthRedirectSchema.AuthScheme] = value;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001F8F RID: 8079 RVA: 0x0008C113 File Offset: 0x0008A313
		// (set) Token: 0x06001F90 RID: 8080 RVA: 0x0008C125 File Offset: 0x0008A325
		[ValidateNotNullOrEmpty]
		[Parameter]
		public string TargetUrl
		{
			get
			{
				return (string)this[AuthRedirectSchema.TargetUrl];
			}
			set
			{
				this[AuthRedirectSchema.TargetUrl] = value;
			}
		}

		// Token: 0x040012BF RID: 4799
		internal static readonly string AuthRedirectKeywords = "C0B7AC3F-FE64-4b4b-A907-9226F8027CCE";

		// Token: 0x040012C0 RID: 4800
		internal static readonly ComparisonFilter AuthRedirectKeywordsFilter = new ComparisonFilter(ComparisonOperator.Equal, AuthRedirectSchema.Keywords, AuthRedirect.AuthRedirectKeywords);

		// Token: 0x040012C1 RID: 4801
		private static readonly AuthRedirectSchema SchemaObject = ObjectSchema.GetInstance<AuthRedirectSchema>();

		// Token: 0x040012C2 RID: 4802
		private static readonly string MostDerivedClass = "serviceConnectionPoint";
	}
}
