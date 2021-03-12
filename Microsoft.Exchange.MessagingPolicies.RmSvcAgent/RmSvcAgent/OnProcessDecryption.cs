using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Transport.RightsManagement;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x02000024 RID: 36
	// (Invoke) Token: 0x060000B7 RID: 183
	internal delegate object OnProcessDecryption(DecryptionStatus status, TransportDecryptionSetting settings, AgentAsyncState state, Exception exception);
}
