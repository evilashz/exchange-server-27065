using System;

namespace Microsoft.Exchange.InfoWorker.Common.OOF
{
	// Token: 0x02000034 RID: 52
	internal class OofRulesSaveException : IWTransientException
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00006B1F File Offset: 0x00004D1F
		public OofRulesSaveException() : base(Strings.descOofRuleSaveException, null)
		{
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00006B2D File Offset: 0x00004D2D
		public OofRulesSaveException(Exception innerException) : base(Strings.descOofRuleSaveException, innerException)
		{
		}
	}
}
