using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000C0 RID: 192
	public class JetSearchCriteriaBitMask : SearchCriteriaBitMask, IJetSearchCriteria
	{
		// Token: 0x06000813 RID: 2067 RVA: 0x000274C6 File Offset: 0x000256C6
		public JetSearchCriteriaBitMask(Column lhs, Column rhs, SearchCriteriaBitMask.SearchBitMaskOp op) : base(lhs, rhs, op)
		{
		}
	}
}
