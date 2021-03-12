using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AA7 RID: 2727
	[Cmdlet("Install", "DefaultAcceptedDomain")]
	public sealed class InstallDefaultAcceptedDomain : NewMultitenancySystemConfigurationObjectTask<AcceptedDomain>
	{
		// Token: 0x17001D34 RID: 7476
		// (get) Token: 0x06006075 RID: 24693 RVA: 0x00191DCC File Offset: 0x0018FFCC
		// (set) Token: 0x06006076 RID: 24694 RVA: 0x00191DD9 File Offset: 0x0018FFD9
		[Parameter(Mandatory = true)]
		public SmtpDomainWithSubdomains DomainName
		{
			get
			{
				return this.DataObject.DomainName;
			}
			set
			{
				this.DataObject.DomainName = value;
			}
		}

		// Token: 0x06006077 RID: 24695 RVA: 0x00191DE8 File Offset: 0x0018FFE8
		protected override IConfigurable PrepareDataObject()
		{
			AcceptedDomain acceptedDomain = (AcceptedDomain)base.PrepareDataObject();
			acceptedDomain.SetId(this.ConfigurationSession, base.Name);
			return acceptedDomain;
		}

		// Token: 0x06006078 RID: 24696 RVA: 0x00191E14 File Offset: 0x00190014
		protected override void InternalProcessRecord()
		{
			if (this.IsAcceptedDomainListEmpty())
			{
				this.DataObject.Default = true;
				this.DataObject.AddressBookEnabled = true;
				base.InternalProcessRecord();
			}
		}

		// Token: 0x06006079 RID: 24697 RVA: 0x00191E3C File Offset: 0x0019003C
		private bool IsAcceptedDomainListEmpty()
		{
			ADPagedReader<AcceptedDomain> adpagedReader = this.ConfigurationSession.FindPaged<AcceptedDomain>(base.CurrentOrgContainerId, QueryScope.SubTree, null, null, 1);
			return !adpagedReader.GetEnumerator().MoveNext();
		}
	}
}
