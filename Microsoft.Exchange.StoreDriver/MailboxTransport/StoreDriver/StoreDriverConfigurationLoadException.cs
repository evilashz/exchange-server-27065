using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver
{
	// Token: 0x0200000F RID: 15
	internal class StoreDriverConfigurationLoadException : LocalizedException
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00003BFA File Offset: 0x00001DFA
		internal StoreDriverConfigurationLoadException(string errorString) : base(new LocalizedString(errorString))
		{
		}
	}
}
