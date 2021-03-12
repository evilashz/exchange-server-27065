using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.DefaultProvisioningAgent.Rus
{
	// Token: 0x02000047 RID: 71
	internal class SchemaAttributePresentationObject
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000C5FE File Offset: 0x0000A7FE
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000C606 File Offset: 0x0000A806
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000C60F File Offset: 0x0000A80F
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000C617 File Offset: 0x0000A817
		public DataSyntax DataSyntax
		{
			get
			{
				return this.dataSyntax;
			}
			set
			{
				this.dataSyntax = value;
			}
		}

		// Token: 0x040000E4 RID: 228
		private string displayName;

		// Token: 0x040000E5 RID: 229
		private DataSyntax dataSyntax;
	}
}
