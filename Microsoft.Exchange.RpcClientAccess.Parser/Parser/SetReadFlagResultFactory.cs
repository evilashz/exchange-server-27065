using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000126 RID: 294
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SetReadFlagResultFactory : StandardResultFactory
	{
		// Token: 0x060005C5 RID: 1477 RVA: 0x00010D08 File Offset: 0x0000EF08
		internal SetReadFlagResultFactory(byte logonIndex, StoreLongTermId longTermId, bool isPublicLogon) : base(RopId.SetReadFlag)
		{
			this.logonIndex = logonIndex;
			this.longTermId = longTermId;
			this.isPublicLogon = isPublicLogon;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x00010D27 File Offset: 0x0000EF27
		internal StoreLongTermId LongTermId
		{
			get
			{
				return this.longTermId;
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00010D2F File Offset: 0x0000EF2F
		public RopResult CreateSuccessfulResult(bool hasChanged)
		{
			if (!this.isPublicLogon)
			{
				hasChanged = false;
			}
			return new SuccessfulSetReadFlagResult(hasChanged, this.logonIndex, this.longTermId);
		}

		// Token: 0x04000323 RID: 803
		private readonly byte logonIndex;

		// Token: 0x04000324 RID: 804
		private readonly StoreLongTermId longTermId;

		// Token: 0x04000325 RID: 805
		private readonly bool isPublicLogon;
	}
}
