using System;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E26 RID: 3622
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	internal sealed class AllowedOAuthGrantAttribute : Attribute
	{
		// Token: 0x06005D66 RID: 23910 RVA: 0x00123102 File Offset: 0x00121302
		public AllowedOAuthGrantAttribute(string grant)
		{
			this.Grant = grant;
		}

		// Token: 0x1700151F RID: 5407
		// (get) Token: 0x06005D67 RID: 23911 RVA: 0x00123111 File Offset: 0x00121311
		// (set) Token: 0x06005D68 RID: 23912 RVA: 0x00123119 File Offset: 0x00121319
		public string Grant { get; private set; }
	}
}
