using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x02000595 RID: 1429
	public class ImpersonationAccessGrantType
	{
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x0002BC19 File Offset: 0x00029E19
		// (set) Token: 0x0600139A RID: 5018 RVA: 0x0002BC21 File Offset: 0x00029E21
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string impersonated
		{
			get
			{
				return this._impersonated;
			}
			set
			{
				this._impersonated = value;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x0002BC2A File Offset: 0x00029E2A
		// (set) Token: 0x0600139C RID: 5020 RVA: 0x0002BC32 File Offset: 0x00029E32
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string impersonator
		{
			get
			{
				return this._impersonator;
			}
			set
			{
				this._impersonator = value;
			}
		}

		// Token: 0x040018DB RID: 6363
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _impersonated;

		// Token: 0x040018DC RID: 6364
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _impersonator;
	}
}
