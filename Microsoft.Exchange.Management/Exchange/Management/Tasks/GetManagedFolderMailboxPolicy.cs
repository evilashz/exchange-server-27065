using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200043D RID: 1085
	[Cmdlet("get", "ManagedFolderMailboxPolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetManagedFolderMailboxPolicy : GetMailboxPolicyBase<ManagedFolderMailboxPolicy>
	{
		// Token: 0x06002626 RID: 9766 RVA: 0x0009857C File Offset: 0x0009677C
		protected override void WriteResult<T>(IEnumerable<T> dataObjects)
		{
			TaskLogger.LogEnter();
			foreach (T t in dataObjects)
			{
				IConfigurable configurable = t;
				ManagedFolderMailboxPolicy managedFolderMailboxPolicy = (ManagedFolderMailboxPolicy)configurable;
				if (!managedFolderMailboxPolicy.AreDefaultManagedFolderLinksUnique(base.DataSession as IConfigurationSession))
				{
					this.WriteWarning(Strings.WarningMisconfiguredElcMailboxPolicy(managedFolderMailboxPolicy.Name));
				}
				else
				{
					base.WriteResult(managedFolderMailboxPolicy);
				}
			}
			TaskLogger.LogExit();
		}
	}
}
