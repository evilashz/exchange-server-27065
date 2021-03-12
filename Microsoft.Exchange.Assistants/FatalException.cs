using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200004F RID: 79
	internal sealed class FatalException : Exception
	{
		// Token: 0x060002AB RID: 683 RVA: 0x0000EB8B File Offset: 0x0000CD8B
		public FatalException(LocalizedString description) : base(description)
		{
		}
	}
}
