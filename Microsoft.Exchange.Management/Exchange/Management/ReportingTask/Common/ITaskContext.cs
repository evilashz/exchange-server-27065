using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x0200069F RID: 1695
	public interface ITaskContext
	{
		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x06003C08 RID: 15368
		Guid CurrentOrganizationGuid { get; }

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x06003C09 RID: 15369
		Guid CurrentOrganizationExternalDirectoryId { get; }

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x06003C0A RID: 15370
		bool IsCurrentOrganizationForestWide { get; }

		// Token: 0x06003C0B RID: 15371
		void WriteError(LocalizedException localizedException, ExchangeErrorCategory exchangeErrorCategory, object target);

		// Token: 0x06003C0C RID: 15372
		void WriteWarning(LocalizedString text);

		// Token: 0x06003C0D RID: 15373
		void WriteVerbose(LocalizedString text);
	}
}
