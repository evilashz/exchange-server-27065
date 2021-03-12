using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005DA RID: 1498
	public class ImpersonationAccessGrantType
	{
		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x0002F96D File Offset: 0x0002DB6D
		// (set) Token: 0x0600187C RID: 6268 RVA: 0x0002F975 File Offset: 0x0002DB75
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

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x0002F97E File Offset: 0x0002DB7E
		// (set) Token: 0x0600187E RID: 6270 RVA: 0x0002F986 File Offset: 0x0002DB86
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

		// Token: 0x04001B1B RID: 6939
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _impersonated;

		// Token: 0x04001B1C RID: 6940
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _impersonator;
	}
}
