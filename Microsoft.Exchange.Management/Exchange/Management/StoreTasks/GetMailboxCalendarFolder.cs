using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007C5 RID: 1989
	[Cmdlet("Get", "MailboxCalendarFolder", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxCalendarFolder : GetTenantXsoObjectWithFolderIdentityTaskBase<MailboxCalendarFolder>
	{
		// Token: 0x17001515 RID: 5397
		// (get) Token: 0x060045D0 RID: 17872 RVA: 0x0011EEA5 File Offset: 0x0011D0A5
		protected override ObjectId RootId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001516 RID: 5398
		// (get) Token: 0x060045D1 RID: 17873 RVA: 0x0011EEA8 File Offset: 0x0011D0A8
		protected override bool DeepSearch
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060045D2 RID: 17874 RVA: 0x0011EEAC File Offset: 0x0011D0AC
		protected sealed override IConfigDataProvider CreateSession()
		{
			ADUser mailboxOwner = this.PrepareMailboxUser();
			base.InnerMailboxFolderDataProvider = new MailboxCalendarFolderDataProvider(base.SessionSettings, mailboxOwner, "Get-MailboxCalendarFolder");
			return base.InnerMailboxFolderDataProvider;
		}

		// Token: 0x060045D3 RID: 17875 RVA: 0x0011EEE0 File Offset: 0x0011D0E0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			LocalizedString? localizedString;
			IEnumerable<MailboxCalendarFolder> dataObjects = base.GetDataObjects(this.Identity, base.OptionalIdentityData, out localizedString);
			this.WriteResult<MailboxCalendarFolder>(dataObjects);
			TaskLogger.LogExit();
		}
	}
}
