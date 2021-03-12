using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AA3 RID: 2723
	[Cmdlet("Install", "UceContentFilterConfig")]
	public sealed class InstallUceContentFilterConfig : NewFixedNameSystemConfigurationObjectTask<UceContentFilter>
	{
		// Token: 0x06006055 RID: 24661 RVA: 0x00191658 File Offset: 0x0018F858
		protected override IConfigurable PrepareDataObject()
		{
			UceContentFilter uceContentFilter = (UceContentFilter)base.PrepareDataObject();
			uceContentFilter.SetId((IConfigurationSession)base.DataSession, "UCE Content Filter");
			return uceContentFilter;
		}

		// Token: 0x06006056 RID: 24662 RVA: 0x00191688 File Offset: 0x0018F888
		protected override void InternalProcessRecord()
		{
			UceContentFilter[] array = this.ConfigurationSession.FindAllPaged<UceContentFilter>().ReadAllPages();
			if (array != null && array.Length > 0)
			{
				base.WriteVerbose(Strings.UceContentFilterAlreadyExists(array[0].DistinguishedName));
				return;
			}
			base.InternalProcessRecord();
		}

		// Token: 0x0400353A RID: 13626
		private const string CanonicalName = "UCE Content Filter";
	}
}
