using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020001A3 RID: 419
	internal static class StoreHelper
	{
		// Token: 0x060011B8 RID: 4536 RVA: 0x000567B7 File Offset: 0x000549B7
		internal static void SetStoreUserInformationReader(IStoreUserInformationReader storeUserInformationReader)
		{
			MbxRecipientSession.StoreUserInformationReader = storeUserInformationReader;
		}
	}
}
