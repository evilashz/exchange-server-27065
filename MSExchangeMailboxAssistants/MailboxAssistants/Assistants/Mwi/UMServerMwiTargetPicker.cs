using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Mwi
{
	// Token: 0x0200010C RID: 268
	internal class UMServerMwiTargetPicker : UMServerRpcTargetPickerBase<IMwiTarget>
	{
		// Token: 0x06000AFF RID: 2815 RVA: 0x00047822 File Offset: 0x00045A22
		protected override IMwiTarget CreateTarget(Server server)
		{
			return new ExRpcMwiTarget(server);
		}

		// Token: 0x040006FE RID: 1790
		public static readonly UMServerMwiTargetPicker Instance = new UMServerMwiTargetPicker();
	}
}
