using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200004E RID: 78
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ProductMsiConfigurationInfo : MsiConfigurationInfo
	{
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000C44D File Offset: 0x0000A64D
		public override string Name
		{
			get
			{
				return "exchangeserver.msi";
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000C454 File Offset: 0x0000A654
		public override Guid ProductCode
		{
			get
			{
				return ProductMsiConfigurationInfo.productCode;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000C45B File Offset: 0x0000A65B
		protected override string LogFileName
		{
			get
			{
				return "ExchangeSetup.msilog";
			}
		}

		// Token: 0x040000F1 RID: 241
		private const string MsiFileName = "exchangeserver.msi";

		// Token: 0x040000F2 RID: 242
		private const string MsiLogFileName = "ExchangeSetup.msilog";

		// Token: 0x040000F3 RID: 243
		protected static readonly Guid productCode = new Guid("{4934D1EA-BE46-48B1-8847-F1AF20E892C1}");
	}
}
