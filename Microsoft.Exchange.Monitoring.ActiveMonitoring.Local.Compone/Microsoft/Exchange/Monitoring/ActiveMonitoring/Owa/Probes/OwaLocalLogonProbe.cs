using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Owa.Probes
{
	// Token: 0x02000264 RID: 612
	public abstract class OwaLocalLogonProbe : OwaLogonProbe
	{
		// Token: 0x06001160 RID: 4448 RVA: 0x00073F58 File Offset: 0x00072158
		protected MailboxDatabaseInfo GetMailboxDatabaseInfo(ProbeDefinition definition, ICollection<MailboxDatabaseInfo> mailboxDatabases)
		{
			if (!string.IsNullOrEmpty(definition.TargetResource))
			{
				foreach (MailboxDatabaseInfo mailboxDatabaseInfo in mailboxDatabases)
				{
					if (mailboxDatabaseInfo.MailboxDatabaseName.Equals(definition.TargetResource))
					{
						return mailboxDatabaseInfo;
					}
				}
				throw new ArgumentException(Strings.OwaMailboxDatabaseDoesntExist(definition.TargetResource));
			}
			int index = new Random().Next(mailboxDatabases.Count);
			return mailboxDatabases.ElementAt(index);
		}
	}
}
