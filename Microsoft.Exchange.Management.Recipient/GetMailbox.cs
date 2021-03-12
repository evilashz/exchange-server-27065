using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000068 RID: 104
	[OutputType(new Type[]
	{
		typeof(Mailbox)
	})]
	[Cmdlet("Get", "Mailbox", DefaultParameterSetName = "Identity")]
	public sealed class GetMailbox : GetMailboxOrSyncMailbox
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0001F6DB File Offset: 0x0001D8DB
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x0001F6E3 File Offset: 0x0001D8E3
		[Parameter(Mandatory = false)]
		public new long UsnForReconciliationSearch
		{
			get
			{
				return base.UsnForReconciliationSearch;
			}
			set
			{
				base.UsnForReconciliationSearch = value;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001F6EC File Offset: 0x0001D8EC
		protected override bool IsRetryableTask
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001F6F0 File Offset: 0x0001D8F0
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			IConfigurable configurable = base.ConvertDataObjectToPresentationObject(dataObject);
			Mailbox mailbox = configurable as Mailbox;
			if (mailbox == null)
			{
				return null;
			}
			TenantPublicFolderConfiguration value = TenantPublicFolderConfigurationCache.Instance.GetValue(mailbox.OrganizationId);
			bool flag = mailbox.RecipientTypeDetails == Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.PublicFolderMailbox;
			if (!flag)
			{
				if (mailbox.DefaultPublicFolderMailboxValue == null || mailbox.DefaultPublicFolderMailboxValue.IsDeleted)
				{
					PublicFolderRecipient publicFolderRecipient = value.GetPublicFolderRecipient(mailbox.ExchangeGuid, null);
					if (publicFolderRecipient != null)
					{
						if (base.NeedSuppressingPiiData)
						{
							string text;
							string text2;
							mailbox.DefaultPublicFolderMailbox = SuppressingPiiData.Redact(publicFolderRecipient.ObjectId, out text, out text2);
						}
						else
						{
							mailbox.DefaultPublicFolderMailbox = publicFolderRecipient.ObjectId;
						}
					}
				}
				else
				{
					mailbox.DefaultPublicFolderMailbox = mailbox.DefaultPublicFolderMailboxValue;
				}
			}
			mailbox.IsRootPublicFolderMailbox = (flag && value.GetHierarchyMailboxInformation().HierarchyMailboxGuid == mailbox.ExchangeGuid);
			if (this.UsnForReconciliationSearch >= 0L)
			{
				mailbox.ReconciliationId = mailbox.NetID;
			}
			mailbox.ResetChangeTracking();
			return mailbox;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001F7DC File Offset: 0x0001D9DC
		private bool DoesIdentityContainWildCard()
		{
			string text = (this.Identity == null) ? null : this.Identity.ToString();
			return !string.IsNullOrEmpty(text) && (text.StartsWith("*") || text.EndsWith("*"));
		}
	}
}
