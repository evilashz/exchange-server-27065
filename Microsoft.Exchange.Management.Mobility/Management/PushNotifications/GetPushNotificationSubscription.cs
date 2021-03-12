using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.Management.StoreTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.PushNotifications
{
	// Token: 0x0200004C RID: 76
	[Cmdlet("Get", "PushNotificationSubscription", DefaultParameterSetName = "Default")]
	public sealed class GetPushNotificationSubscription : GetTaskBase<ADRecipient>
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000C794 File Offset: 0x0000A994
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000C7AB File Offset: 0x0000A9AB
		[Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000C7BE File Offset: 0x0000A9BE
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000C7E4 File Offset: 0x0000A9E4
		[Parameter(Mandatory = false, ParameterSetName = "ShowAll")]
		public SwitchParameter ShowAll
		{
			get
			{
				return (SwitchParameter)(base.Fields["ShowAll"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ShowAll"] = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000C7FC File Offset: 0x0000A9FC
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000C804 File Offset: 0x0000AA04
		[Parameter(Mandatory = false)]
		public Unlimited<uint> ResultSize
		{
			get
			{
				return base.InternalResultSize;
			}
			set
			{
				base.InternalResultSize = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000C80D File Offset: 0x0000AA0D
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000C839 File Offset: 0x0000AA39
		[Parameter(Mandatory = false, ParameterSetName = "ExpirationTime")]
		public uint ExpirationTimeInHours
		{
			get
			{
				if (base.Fields["ExpirationTime"] != null)
				{
					return (uint)base.Fields["ExpirationTime"];
				}
				return 72U;
			}
			set
			{
				base.Fields["ExpirationTime"] = value;
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000C851 File Offset: 0x0000AA51
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(ConnectionFailedPermanentException).IsInstanceOfType(exception);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000C86E File Offset: 0x0000AA6E
		protected override IConfigDataProvider CreateSession()
		{
			return base.TenantGlobalCatalogSession;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000C878 File Offset: 0x0000AA78
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ADUser aduser = (ADUser)base.GetDataObject<ADUser>(this.Mailbox, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.Mailbox.ToString())), new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.Mailbox.ToString())));
			ExchangePrincipal principal = ExchangePrincipal.FromADUser(aduser, RemotingOptions.AllowCrossSite);
			using (MailboxSession mailboxSession = StoreTasksHelper.OpenMailboxSession(principal, "Get-PushNotificationSubscription"))
			{
				StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.PushNotificationRoot);
				if (defaultFolderId != null)
				{
					using (IFolder folder = GetPushNotificationSubscription.xsoFactory.BindToFolder(mailboxSession, defaultFolderId))
					{
						IEnumerable<IStorePropertyBag> enumerable;
						if (this.ShowAll)
						{
							enumerable = new SubscriptionItemEnumerator(folder, this.ResultSize);
						}
						else
						{
							enumerable = new ActiveSubscriptionItemEnumerator(folder, this.ExpirationTimeInHours, this.ResultSize);
						}
						foreach (IStorePropertyBag propertyBag in enumerable)
						{
							this.WriteResult(this.CreatePresentationObject(propertyBag, aduser, mailboxSession));
						}
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000C9B8 File Offset: 0x0000ABB8
		private PushNotificationSubscription CreatePresentationObject(IStorePropertyBag propertyBag, ADUser aduser, MailboxSession mailboxSession)
		{
			VersionedId valueOrDefault = propertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
			string valueOrDefault2 = propertyBag.GetValueOrDefault<string>(PushNotificationSubscriptionItemSchema.SubscriptionId, null);
			string serializedNotificationSubscription = PushNotificationStorage.GetSerializedNotificationSubscription(mailboxSession, propertyBag, GetPushNotificationSubscription.xsoFactory);
			base.WriteVerbose(Strings.WriteVerboseSerializedSubscription(serializedNotificationSubscription));
			return new PushNotificationSubscription(aduser.ObjectId, valueOrDefault, valueOrDefault2, serializedNotificationSubscription);
		}

		// Token: 0x040000C0 RID: 192
		private static readonly XSOFactory xsoFactory = new XSOFactory();
	}
}
