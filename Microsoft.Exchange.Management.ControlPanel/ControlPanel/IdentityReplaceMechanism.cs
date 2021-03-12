using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006AA RID: 1706
	public class IdentityReplaceMechanism : TranslationMechanismBase
	{
		// Token: 0x060048F0 RID: 18672 RVA: 0x000DF165 File Offset: 0x000DD365
		public IdentityReplaceMechanism(LocalizedString messageWithoutDisplayName) : base(messageWithoutDisplayName, false)
		{
		}

		// Token: 0x060048F1 RID: 18673 RVA: 0x000DF16F File Offset: 0x000DD36F
		protected override string TranslationWithDisplayName(Identity id, string originalMessage)
		{
			return originalMessage.Replace(id.RawIdentity, id.DisplayName);
		}
	}
}
