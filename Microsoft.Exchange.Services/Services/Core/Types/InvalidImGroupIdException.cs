using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007B6 RID: 1974
	internal sealed class InvalidImGroupIdException : ServicePermanentException
	{
		// Token: 0x06003ABD RID: 15037 RVA: 0x000CF4C9 File Offset: 0x000CD6C9
		public InvalidImGroupIdException() : base(CoreResources.IDs.ErrorInvalidImGroupId)
		{
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06003ABE RID: 15038 RVA: 0x000CF4DB File Offset: 0x000CD6DB
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
