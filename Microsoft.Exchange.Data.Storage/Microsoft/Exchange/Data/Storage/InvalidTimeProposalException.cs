using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000742 RID: 1858
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InvalidTimeProposalException : InvalidOperationException
	{
		// Token: 0x06004814 RID: 18452 RVA: 0x00130965 File Offset: 0x0012EB65
		public InvalidTimeProposalException(string message) : base(message)
		{
		}
	}
}
