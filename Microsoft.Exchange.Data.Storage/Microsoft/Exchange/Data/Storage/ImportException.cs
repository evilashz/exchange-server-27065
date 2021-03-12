using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000736 RID: 1846
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ImportException : StoragePermanentException
	{
		// Token: 0x060047F1 RID: 18417 RVA: 0x001307AB File Offset: 0x0012E9AB
		public ImportException(LocalizedString message, ImportResult importResult) : base(message)
		{
			EnumValidator.ThrowIfInvalid<ImportResult>(importResult, "importResult");
			this.importResult = importResult;
		}

		// Token: 0x170014DF RID: 5343
		// (get) Token: 0x060047F2 RID: 18418 RVA: 0x001307C6 File Offset: 0x0012E9C6
		public ImportResult ImportResult
		{
			get
			{
				return this.importResult;
			}
		}

		// Token: 0x0400273A RID: 10042
		private readonly ImportResult importResult;
	}
}
