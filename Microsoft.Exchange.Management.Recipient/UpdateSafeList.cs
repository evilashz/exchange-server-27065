using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Transport.RecipientAPI;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000107 RID: 263
	[Cmdlet("Update", "SafeList", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public class UpdateSafeList : RecipientObjectActionTask<MailboxIdParameter, ADRecipient>
	{
		// Token: 0x06001325 RID: 4901 RVA: 0x000463B0 File Offset: 0x000445B0
		public UpdateSafeList()
		{
			this.Type = UpdateType.SafeSenders;
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x000463BF File Offset: 0x000445BF
		// (set) Token: 0x06001327 RID: 4903 RVA: 0x000463D6 File Offset: 0x000445D6
		[Parameter(Mandatory = false)]
		public UpdateType Type
		{
			get
			{
				return (UpdateType)base.Fields["Type"];
			}
			set
			{
				base.Fields["Type"] = value;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x000463EE File Offset: 0x000445EE
		// (set) Token: 0x06001329 RID: 4905 RVA: 0x00046413 File Offset: 0x00044613
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeDomains
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeDomains"] ?? UpdateSafeList.DefaultIncludeDomains);
			}
			set
			{
				base.Fields["IncludeDomains"] = value;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x0004642B File Offset: 0x0004462B
		// (set) Token: 0x0600132B RID: 4907 RVA: 0x00046450 File Offset: 0x00044650
		[Parameter(Mandatory = false)]
		public SwitchParameter EnsureJunkEmailRule
		{
			get
			{
				return (SwitchParameter)(base.Fields["EnsureJunkEmailRule"] ?? UpdateSafeList.DefaultEnsureJunkEmailRule);
			}
			set
			{
				base.Fields["EnsureJunkEmailRule"] = value;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x00046468 File Offset: 0x00044668
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageUpdateSafeList(this.Identity.ToString());
			}
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0004647C File Offset: 0x0004467C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			bool flag = false;
			this.summary = default(UpdateSafeList.SafeListSummary);
			CmdletProxy.ThrowExceptionIfProxyIsNeeded(base.CurrentTaskContext, (ADUser)this.DataObject, false, this.ConfirmationMessage, null);
			try
			{
				ADUser aduser = this.DataObject as ADUser;
				if (aduser == null || (aduser.RecipientType != RecipientType.UserMailbox && aduser.RecipientType != RecipientType.MailUser))
				{
					base.WriteError(new RecipientTypeInvalidException(this.Identity.ToString()), ErrorCategory.InvalidData, aduser);
				}
				ExchangePrincipal mailboxOwner = ExchangePrincipal.FromADUser(base.SessionSettings, aduser);
				using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, "Client=Management;Action=Update-SafeList"))
				{
					if (!mailboxSession.Capabilities.CanHaveJunkEmailRule)
					{
						base.WriteError(new MailboxNotJunkRuleCapableException(this.Identity.ToString()), ErrorCategory.InvalidData, aduser);
					}
					else if (this.EnsureJunkEmailRule)
					{
						this.CheckAndCreateJunkEmailRule(mailboxSession);
					}
					else
					{
						JunkEmailRule filteredJunkEmailRule = mailboxSession.FilteredJunkEmailRule;
						switch (this.Type)
						{
						case UpdateType.SafeSenders:
						case UpdateType.SafeRecipients:
						case UpdateType.BlockedSenders:
							flag = this.CheckAndUpdateHashes(filteredJunkEmailRule, this.Type);
							break;
						case UpdateType.Both:
							if (this.CheckAndUpdateHashes(filteredJunkEmailRule, UpdateType.SafeRecipients))
							{
								flag = true;
							}
							if (this.CheckAndUpdateHashes(filteredJunkEmailRule, UpdateType.SafeSenders))
							{
								flag = true;
							}
							break;
						case UpdateType.All:
							if (this.CheckAndUpdateHashes(filteredJunkEmailRule, UpdateType.SafeRecipients))
							{
								flag = true;
							}
							if (this.CheckAndUpdateHashes(filteredJunkEmailRule, UpdateType.SafeSenders))
							{
								flag = true;
							}
							if (this.CheckAndUpdateHashes(filteredJunkEmailRule, UpdateType.BlockedSenders))
							{
								flag = true;
							}
							break;
						}
						if (flag)
						{
							base.InternalProcessRecord();
							if ((this.Type == UpdateType.SafeSenders || this.Type == UpdateType.Both || this.Type == UpdateType.All) && (this.summary.SkippedContacts > 0 || this.summary.SkippedSafeSenderDomains > 0 || this.summary.SkippedSafeSenders > 0))
							{
								this.WriteWarning(Strings.SafeSendersNotUpdated(this.summary.SkippedContacts, this.summary.SkippedSafeSenders, this.summary.SkippedSafeSenderDomains, this.Identity.ToString()));
							}
							if ((this.Type == UpdateType.SafeRecipients || this.Type == UpdateType.Both || this.Type == UpdateType.All) && (this.summary.SkippedSafeRecipientDomains > 0 || this.summary.SkippedSafeRecipients > 0))
							{
								this.WriteWarning(Strings.SafeRecipientsNotUpdated(this.summary.SkippedSafeRecipients, this.summary.SkippedSafeRecipientDomains, this.Identity.ToString()));
							}
							if ((this.Type == UpdateType.BlockedSenders || this.Type == UpdateType.All) && (this.summary.SkippedBlockedSenderDomains > 0 || this.summary.SkippedBlockedSenders > 0))
							{
								this.WriteWarning(Strings.BlockedSendersNotUpdated(this.summary.SkippedBlockedSenders, this.summary.SkippedBlockedSenderDomains, this.Identity.ToString()));
							}
						}
						if (flag || !filteredJunkEmailRule.AllRestrictionsLoaded)
						{
							filteredJunkEmailRule.Save();
						}
					}
				}
			}
			catch (JunkEmailValidationException ex)
			{
				TaskLogger.LogError(ex);
				base.WriteError(ex, ErrorCategory.InvalidData, this.DataObject);
			}
			catch (StorageTransientException ex2)
			{
				TaskLogger.LogError(ex2);
				base.WriteError(ex2, ErrorCategory.ReadError, this.DataObject);
			}
			catch (StoragePermanentException ex3)
			{
				TaskLogger.LogError(ex3);
				base.WriteError(ex3, ErrorCategory.InvalidOperation, this.DataObject);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x00046810 File Offset: 0x00044A10
		private bool CheckAndUpdateHashes(JunkEmailRule junkEmailRule, UpdateType currentUpdateType)
		{
			AddressHashes addressHashes = this.ComputeHashForMailbox(junkEmailRule, currentUpdateType);
			if (!this.CompareHashes(addressHashes, this.DataObject, currentUpdateType))
			{
				byte[] array = addressHashes.GetBytes();
				if (array.Length == 0)
				{
					array = null;
				}
				switch (currentUpdateType)
				{
				case UpdateType.SafeSenders:
					this.DataObject.SafeSendersHash = array;
					break;
				case UpdateType.SafeRecipients:
					this.DataObject.SafeRecipientsHash = array;
					break;
				case UpdateType.BlockedSenders:
					this.DataObject.BlockedSendersHash = array;
					break;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0004688C File Offset: 0x00044A8C
		private void CheckAndCreateJunkEmailRule(MailboxSession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			StoreObjectId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.Inbox);
			StoreObjectId defaultFolderId2 = session.GetDefaultFolderId(DefaultFolderType.JunkEmail);
			if (defaultFolderId == null || defaultFolderId2 == null)
			{
				this.WriteWarning(Strings.CannotCreateJunkEmailRule(this.Identity.ToString()));
				return;
			}
			if (session.GetJunkEmailRuleStatus() == JunkEmailRule.JunkEmailStatus.None)
			{
				JunkEmailRule filteredJunkEmailRule = session.FilteredJunkEmailRule;
				filteredJunkEmailRule.IsEnabled = true;
				filteredJunkEmailRule.Save();
				base.WriteObject(Strings.CreatedJunkEmailRule(this.Identity.ToString()).ToString());
			}
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00046914 File Offset: 0x00044B14
		private int UpdateAddressHashes(AddressHashes addressHashes, int maxEntries, IEnumerable<string> addressCollection)
		{
			int num = 0;
			foreach (string addressOrDomain in addressCollection)
			{
				if (addressHashes.Count >= maxEntries)
				{
					break;
				}
				num++;
				addressHashes.Add(addressOrDomain);
			}
			return num;
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0004696C File Offset: 0x00044B6C
		private AddressHashes ComputeHashForMailbox(JunkEmailRule junkEmailRule, UpdateType type)
		{
			AddressHashes addressHashes = new AddressHashes();
			switch (type)
			{
			case UpdateType.SafeSenders:
			{
				if (this.IncludeDomains)
				{
					int num = this.UpdateAddressHashes(addressHashes, 3072, junkEmailRule.TrustedSenderDomainCollection);
					this.summary.SkippedSafeSenderDomains = junkEmailRule.TrustedSenderDomainCollection.Count - num;
				}
				if (junkEmailRule.IsContactsFolderTrusted)
				{
					int num2 = this.UpdateAddressHashes(addressHashes, 3072, junkEmailRule.TrustedContactsEmailCollection);
					this.summary.SkippedContacts = junkEmailRule.TrustedContactsEmailCollection.Count - num2;
				}
				int num3 = this.UpdateAddressHashes(addressHashes, 3072, junkEmailRule.TrustedSenderEmailCollection);
				this.summary.SkippedSafeSenders = junkEmailRule.TrustedSenderEmailCollection.Count - num3;
				break;
			}
			case UpdateType.SafeRecipients:
			{
				if (this.IncludeDomains)
				{
					int num4 = this.UpdateAddressHashes(addressHashes, 2048, junkEmailRule.TrustedRecipientDomainCollection);
					this.summary.SkippedSafeRecipientDomains = junkEmailRule.TrustedRecipientDomainCollection.Count - num4;
				}
				int num5 = this.UpdateAddressHashes(addressHashes, 2048, junkEmailRule.TrustedRecipientEmailCollection);
				this.summary.SkippedSafeRecipients = junkEmailRule.TrustedRecipientEmailCollection.Count - num5;
				break;
			}
			case UpdateType.BlockedSenders:
			{
				if (this.IncludeDomains)
				{
					int num6 = this.UpdateAddressHashes(addressHashes, 2048, junkEmailRule.BlockedSenderDomainCollection);
					this.summary.SkippedBlockedSenderDomains = junkEmailRule.BlockedSenderDomainCollection.Count - num6;
				}
				int num7 = this.UpdateAddressHashes(addressHashes, 2048, junkEmailRule.BlockedSenderEmailCollection);
				this.summary.SkippedBlockedSenders = junkEmailRule.BlockedSenderEmailCollection.Count - num7;
				break;
			}
			}
			return addressHashes;
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00046B1C File Offset: 0x00044D1C
		private bool CompareHashes(AddressHashes ssl, ADRecipient recipient, UpdateType type)
		{
			byte[] array = null;
			byte[] array2 = null;
			switch (type)
			{
			case UpdateType.SafeSenders:
				array2 = recipient.SafeSendersHash;
				break;
			case UpdateType.SafeRecipients:
				array2 = recipient.SafeRecipientsHash;
				break;
			case UpdateType.BlockedSenders:
				array2 = recipient.BlockedSendersHash;
				break;
			}
			if (ssl != null)
			{
				array = ssl.GetBytes();
			}
			if (array2 == null)
			{
				return array == null || array.Length == 0;
			}
			if (array.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040003BB RID: 955
		private const int MaxSafeSenderEntries = 3072;

		// Token: 0x040003BC RID: 956
		private const int MaxSafeRecipientEntries = 2048;

		// Token: 0x040003BD RID: 957
		private const int MaxBlockedSenderEntries = 2048;

		// Token: 0x040003BE RID: 958
		private static readonly SwitchParameter DefaultIncludeDomains = new SwitchParameter(false);

		// Token: 0x040003BF RID: 959
		private static readonly SwitchParameter DefaultEnsureJunkEmailRule = new SwitchParameter(false);

		// Token: 0x040003C0 RID: 960
		private UpdateSafeList.SafeListSummary summary;

		// Token: 0x02000108 RID: 264
		private struct SafeListSummary
		{
			// Token: 0x040003C1 RID: 961
			public int SkippedSafeSenderDomains;

			// Token: 0x040003C2 RID: 962
			public int SkippedContacts;

			// Token: 0x040003C3 RID: 963
			public int SkippedSafeSenders;

			// Token: 0x040003C4 RID: 964
			public int SkippedSafeRecipients;

			// Token: 0x040003C5 RID: 965
			public int SkippedSafeRecipientDomains;

			// Token: 0x040003C6 RID: 966
			public int SkippedBlockedSenders;

			// Token: 0x040003C7 RID: 967
			public int SkippedBlockedSenderDomains;
		}
	}
}
