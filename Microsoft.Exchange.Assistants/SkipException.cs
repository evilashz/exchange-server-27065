using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000058 RID: 88
	internal class SkipException : AIPermanentException
	{
		// Token: 0x060002BE RID: 702 RVA: 0x0000EEEB File Offset: 0x0000D0EB
		public SkipException(Exception innerException) : base(Strings.descSkipException, innerException)
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000EEF9 File Offset: 0x0000D0F9
		public SkipException(LocalizedString explain) : base(explain, null)
		{
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000EF03 File Offset: 0x0000D103
		public SkipException(LocalizedString explain, Exception innerException) : base(explain, innerException)
		{
		}
	}
}
