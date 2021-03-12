using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.Management.StoreTasks;

namespace Microsoft.Exchange.Management.PushNotifications
{
	// Token: 0x02000058 RID: 88
	[Cmdlet("Remove", "PushNotificationSubscription", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "RemoveAll")]
	public sealed class RemovePushNotificationSubscription : RemoveTaskBase<MailboxIdParameter, ADUser>
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000E6CA File Offset: 0x0000C8CA
		// (set) Token: 0x0600039C RID: 924 RVA: 0x0000E6E1 File Offset: 0x0000C8E1
		[Parameter(Position = 0, Mandatory = true, ParameterSetName = "RemoveStorage")]
		[ValidateNotNullOrEmpty]
		[Parameter(Position = 0, Mandatory = true, ParameterSetName = "RemoveAll")]
		public MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000E6F4 File Offset: 0x0000C8F4
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0000E71A File Offset: 0x0000C91A
		[Parameter(Mandatory = false, ParameterSetName = "RemoveStorage")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoveAll")]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? false);
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000E732 File Offset: 0x0000C932
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x0000E758 File Offset: 0x0000C958
		[Parameter(Mandatory = false, ParameterSetName = "RemoveStorage")]
		public SwitchParameter RemoveStorage
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoveStorage"] ?? false);
			}
			set
			{
				base.Fields["RemoveStorage"] = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000E770 File Offset: 0x0000C970
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x0000E787 File Offset: 0x0000C987
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "IndividualRemove", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public PushNotificationStoreId SubscriptionStoreId
		{
			get
			{
				return (PushNotificationStoreId)base.Fields["SubscriptionStoreId"];
			}
			set
			{
				base.Fields["SubscriptionStoreId"] = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000E79A File Offset: 0x0000C99A
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x0000E7A2 File Offset: 0x0000C9A2
		public override MailboxIdParameter Identity { get; set; }

		// Token: 0x060003A5 RID: 933 RVA: 0x0000E7AB File Offset: 0x0000C9AB
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(ConnectionFailedPermanentException).IsInstanceOfType(exception);
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000E7C8 File Offset: 0x0000C9C8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.SubscriptionStoreId != null)
				{
					return Strings.ConfirmationMessageRemoveSinglePushNotificationSubscription(this.SubscriptionStoreId.ToString());
				}
				return Strings.ConfirmationMessageRemovePushNotificationSubscription(this.Mailbox.ToString());
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000E7F9 File Offset: 0x0000C9F9
		protected override IConfigDataProvider CreateSession()
		{
			return base.TenantGlobalCatalogSession;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000E804 File Offset: 0x0000CA04
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (this.Mailbox != null)
			{
				this.Identity = this.Mailbox;
			}
			else if (this.SubscriptionStoreId != null)
			{
				this.Identity = new MailboxIdParameter(this.SubscriptionStoreId.MailboxOwnerId.ToString());
			}
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000E860 File Offset: 0x0000CA60
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ExchangePrincipal principal = ExchangePrincipal.FromADUser(base.DataObject, RemotingOptions.AllowCrossSite);
			using (MailboxSession mailboxSession = StoreTasksHelper.OpenMailboxSession(principal, "Remove-PushNotificationSubscription"))
			{
				using (IPushNotificationStorage pushNotificationStorage = PushNotificationStorage.Find(mailboxSession))
				{
					if (pushNotificationStorage != null)
					{
						if (this.SubscriptionStoreId != null)
						{
							pushNotificationStorage.DeleteSubscription(this.SubscriptionStoreId.StoreObjectIdValue);
						}
						else if (this.Force || base.ShouldContinue(Strings.ConfirmRemoveUserPushNotificationSubscriptions(this.Mailbox.ToString())))
						{
							if (base.ParameterSetName.Equals("RemoveStorage"))
							{
								PushNotificationStorage.DeleteStorage(mailboxSession);
							}
							else
							{
								pushNotificationStorage.DeleteAllSubscriptions();
							}
						}
					}
				}
			}
			TaskLogger.LogExit();
		}
	}
}
