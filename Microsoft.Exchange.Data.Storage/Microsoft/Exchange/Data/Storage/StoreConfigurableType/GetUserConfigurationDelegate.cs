using System;

namespace Microsoft.Exchange.Data.Storage.StoreConfigurableType
{
	// Token: 0x020009F2 RID: 2546
	// (Invoke) Token: 0x06005D1A RID: 23834
	internal delegate UserConfiguration GetUserConfigurationDelegate(MailboxSession session, string configuration, UserConfigurationTypes type, bool createIfNonexisting);
}
