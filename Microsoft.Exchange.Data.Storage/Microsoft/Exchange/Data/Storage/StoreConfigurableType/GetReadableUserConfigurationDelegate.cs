using System;

namespace Microsoft.Exchange.Data.Storage.StoreConfigurableType
{
	// Token: 0x020009F3 RID: 2547
	// (Invoke) Token: 0x06005D1E RID: 23838
	internal delegate IReadableUserConfiguration GetReadableUserConfigurationDelegate(MailboxSession session, string configuration, UserConfigurationTypes type, bool createIfNonexisting);
}
