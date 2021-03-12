using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000552 RID: 1362
	internal interface ILiveIdHelper
	{
		// Token: 0x060021EA RID: 8682
		NetID CreateMember(SmtpAddress smtpAddress, IConfigurationSession session, string password);

		// Token: 0x060021EB RID: 8683
		NetID GetMember(SmtpAddress smtpAddress, IConfigurationSession session);

		// Token: 0x060021EC RID: 8684
		void ResetPassword(SmtpAddress smtpAddress, NetID netId, IConfigurationSession session, string password);
	}
}
