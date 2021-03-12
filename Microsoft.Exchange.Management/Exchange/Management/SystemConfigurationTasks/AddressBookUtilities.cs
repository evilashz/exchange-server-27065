using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007D4 RID: 2004
	internal sealed class AddressBookUtilities
	{
		// Token: 0x06004622 RID: 17954 RVA: 0x00120155 File Offset: 0x0011E355
		private AddressBookUtilities()
		{
		}

		// Token: 0x06004623 RID: 17955 RVA: 0x00120160 File Offset: 0x0011E360
		internal static bool IsTenantAddressList(IConfigurationSession session, ADObjectId id)
		{
			string accountOrResourceForestFqdn = session.SessionSettings.GetAccountOrResourceForestFqdn();
			return ADSession.IsTenantIdentity(id, accountOrResourceForestFqdn);
		}

		// Token: 0x06004624 RID: 17956 RVA: 0x00120180 File Offset: 0x0011E380
		internal static void SyncGlobalAddressLists(ExchangeConfigurationContainerWithAddressLists ecc, Task.TaskWarningLoggingDelegate writeWarning)
		{
			bool flag = false;
			IEnumerable<ADObjectId> enumerable = ecc.DefaultGlobalAddressList.Except(ecc.DefaultGlobalAddressList2);
			foreach (ADObjectId item in enumerable)
			{
				if (!flag)
				{
					writeWarning(Strings.WarningFixedMissingGALEntry);
					flag = true;
				}
				ecc.DefaultGlobalAddressList2.Add(item);
			}
		}
	}
}
