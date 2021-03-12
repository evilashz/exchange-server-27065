using System;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000F70 RID: 3952
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MailboxOwnerAdapter : IMailboxOwner
	{
		// Token: 0x0600870E RID: 34574 RVA: 0x00250402 File Offset: 0x0024E602
		protected MailboxOwnerAdapter(IConstraintProvider constraintProvider, RecipientTypeDetails recipientTypeDetails, LogonType logonType)
		{
			this.recipientTypeDetails = recipientTypeDetails;
			this.logonType = logonType;
			this.constraintProvider = constraintProvider;
		}

		// Token: 0x170023CC RID: 9164
		// (get) Token: 0x0600870F RID: 34575 RVA: 0x00250420 File Offset: 0x0024E620
		public bool SideConversationProcessingEnabled
		{
			get
			{
				return this.IsLogonTypeSupported && this.IsRecipientTypeSupported && this.VariantConfig.DataStorage.ModernMailInfra.Enabled;
			}
		}

		// Token: 0x170023CD RID: 9165
		// (get) Token: 0x06008710 RID: 34576 RVA: 0x00250458 File Offset: 0x0024E658
		public bool ThreadedConversationProcessingEnabled
		{
			get
			{
				return this.IsLogonTypeSupported && this.IsRecipientTypeSupported && this.VariantConfig.DataStorage.ThreadedConversation.Enabled;
			}
		}

		// Token: 0x170023CE RID: 9166
		// (get) Token: 0x06008711 RID: 34577 RVA: 0x00250490 File Offset: 0x0024E690
		public bool ModernConversationPreparationEnabled
		{
			get
			{
				return this.VariantConfig.DataStorage.ModernConversationPrep.Enabled;
			}
		}

		// Token: 0x170023CF RID: 9167
		// (get) Token: 0x06008712 RID: 34578 RVA: 0x002504B5 File Offset: 0x0024E6B5
		public bool RequestExtraPropertiesWhenSearching
		{
			get
			{
				return this.ModernConversationPreparationEnabled || this.ModernConversationEnabled;
			}
		}

		// Token: 0x170023D0 RID: 9168
		// (get) Token: 0x06008713 RID: 34579 RVA: 0x002504C7 File Offset: 0x0024E6C7
		public bool SearchDuplicatedMessagesEnabled
		{
			get
			{
				return this.ModernConversationEnabled;
			}
		}

		// Token: 0x170023D1 RID: 9169
		// (get) Token: 0x06008714 RID: 34580 RVA: 0x002504CF File Offset: 0x0024E6CF
		public bool IsGroupMailbox
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008715 RID: 34581 RVA: 0x002504D2 File Offset: 0x0024E6D2
		public bool SentToMySelf(ICorePropertyBag item)
		{
			return MailboxOwnerAdapter.CheckUserEquality(item, ItemSchema.From, this.User) || MailboxOwnerAdapter.CheckUserEquality(item, ItemSchema.Sender, this.User);
		}

		// Token: 0x170023D2 RID: 9170
		// (get) Token: 0x06008716 RID: 34582 RVA: 0x002504FA File Offset: 0x0024E6FA
		private bool ModernConversationEnabled
		{
			get
			{
				return this.SideConversationProcessingEnabled || this.ThreadedConversationProcessingEnabled;
			}
		}

		// Token: 0x170023D3 RID: 9171
		// (get) Token: 0x06008717 RID: 34583 RVA: 0x0025050C File Offset: 0x0024E70C
		private bool IsLogonTypeSupported
		{
			get
			{
				switch (this.logonType)
				{
				case LogonType.Owner:
				case LogonType.Delegated:
				case LogonType.Transport:
					return true;
				}
				return false;
			}
		}

		// Token: 0x170023D4 RID: 9172
		// (get) Token: 0x06008718 RID: 34584 RVA: 0x0025053B File Offset: 0x0024E73B
		private bool IsRecipientTypeSupported
		{
			get
			{
				return this.recipientTypeDetails == RecipientTypeDetails.UserMailbox;
			}
		}

		// Token: 0x170023D5 RID: 9173
		// (get) Token: 0x06008719 RID: 34585 RVA: 0x00250547 File Offset: 0x0024E747
		protected VariantConfigurationSnapshot VariantConfig
		{
			get
			{
				if (this.variantConfiguration == null)
				{
					this.variantConfiguration = VariantConfiguration.GetSnapshot(this.constraintProvider, null, null);
				}
				return this.variantConfiguration;
			}
		}

		// Token: 0x170023D6 RID: 9174
		// (get) Token: 0x0600871A RID: 34586 RVA: 0x0025056A File Offset: 0x0024E76A
		protected IGenericADUser User
		{
			get
			{
				if (this.user == null)
				{
					this.user = this.CalculateGenericADUser();
				}
				return this.user;
			}
		}

		// Token: 0x0600871B RID: 34587
		protected abstract IGenericADUser CalculateGenericADUser();

		// Token: 0x0600871C RID: 34588 RVA: 0x002505AC File Offset: 0x0024E7AC
		private static bool CheckUserEquality(ICorePropertyBag item, StorePropertyDefinition participantPropertyDefinition, IGenericADUser adUser)
		{
			Participant participant = item.GetValueOrDefault<Participant>(participantPropertyDefinition, null);
			if (participant == null)
			{
				return false;
			}
			byte[] valueOrDefault = participant.GetValueOrDefault<byte[]>(ParticipantSchema.ParticipantSID);
			byte[] array = MailboxOwnerAdapter.CalculateEffectiveId(adUser);
			return (array != null && valueOrDefault != null && ByteArrayComparer.Instance.Equals(valueOrDefault, array)) || StringComparer.OrdinalIgnoreCase.Equals(adUser.LegacyDn, participant.EmailAddress) || StringComparer.OrdinalIgnoreCase.Equals(adUser.PrimarySmtpAddress.ToString(), participant.EmailAddress) || adUser.EmailAddresses.Any((ProxyAddress address) => StringComparer.OrdinalIgnoreCase.Equals(address.AddressString, participant.EmailAddress));
		}

		// Token: 0x0600871D RID: 34589 RVA: 0x00250670 File Offset: 0x0024E870
		private static byte[] CalculateEffectiveId(IGenericADUser adUser)
		{
			byte[] result = null;
			SecurityIdentifier securityIdentifier = IdentityHelper.CalculateEffectiveSid(adUser.Sid, adUser.MasterAccountSid);
			if (securityIdentifier != null)
			{
				result = ValueConvertor.ConvertValueToBinary(securityIdentifier, null);
			}
			return result;
		}

		// Token: 0x04005A3C RID: 23100
		private readonly LogonType logonType;

		// Token: 0x04005A3D RID: 23101
		private readonly RecipientTypeDetails recipientTypeDetails;

		// Token: 0x04005A3E RID: 23102
		private readonly IConstraintProvider constraintProvider;

		// Token: 0x04005A3F RID: 23103
		private VariantConfigurationSnapshot variantConfiguration;

		// Token: 0x04005A40 RID: 23104
		private IGenericADUser user;
	}
}
