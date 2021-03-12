using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004AD RID: 1197
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ContactInfoForLinkingFromDirectory : IContactLinkingMatchProperties
	{
		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x06003520 RID: 13600 RVA: 0x000D6A4F File Offset: 0x000D4C4F
		// (set) Token: 0x06003521 RID: 13601 RVA: 0x000D6A57 File Offset: 0x000D4C57
		public Guid GALLinkID { get; protected set; }

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x06003522 RID: 13602 RVA: 0x000D6A60 File Offset: 0x000D4C60
		// (set) Token: 0x06003523 RID: 13603 RVA: 0x000D6A68 File Offset: 0x000D4C68
		public byte[] AddressBookEntryId { get; protected set; }

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x06003524 RID: 13604 RVA: 0x000D6A71 File Offset: 0x000D4C71
		// (set) Token: 0x06003525 RID: 13605 RVA: 0x000D6A79 File Offset: 0x000D4C79
		public string[] SmtpAddressCache { get; protected set; }

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x06003526 RID: 13606 RVA: 0x000D6A82 File Offset: 0x000D4C82
		// (set) Token: 0x06003527 RID: 13607 RVA: 0x000D6A8A File Offset: 0x000D4C8A
		public HashSet<string> EmailAddresses { get; protected set; }

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x06003528 RID: 13608 RVA: 0x000D6A93 File Offset: 0x000D4C93
		// (set) Token: 0x06003529 RID: 13609 RVA: 0x000D6A9B File Offset: 0x000D4C9B
		public string IMAddress { get; protected set; }

		// Token: 0x0600352A RID: 13610 RVA: 0x000D6AA4 File Offset: 0x000D4CA4
		protected ContactInfoForLinkingFromDirectory(ADRawEntry person)
		{
			this.GALLinkID = person.Id.ObjectGuid;
			this.AddressBookEntryId = ContactInfoForLinkingFromDirectory.GetAddressBookId(person);
			this.SmtpAddressCache = ContactInfoForLinkingFromDirectory.GetSmtpAddressCache(person);
			this.EmailAddresses = ContactInfoForLinkingFromDirectory.GetEmailAddresses(person);
			this.IMAddress = (person[ADUserSchema.RTCSIPPrimaryUserAddress] as string);
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x000D6B02 File Offset: 0x000D4D02
		public static ContactInfoForLinkingFromDirectory Create(ADRawEntry person)
		{
			Util.ThrowOnNullArgument(person, "person");
			return new ContactInfoForLinkingFromDirectory(person);
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x000D6B18 File Offset: 0x000D4D18
		public static ContactInfoForLinkingFromDirectory Create(IRecipientSession adSession, ADObjectId adObjectId)
		{
			Util.ThrowOnNullArgument(adSession, "adSession");
			Util.ThrowOnNullArgument(adObjectId, "adObjectId");
			ADRawEntry adrawEntry = adSession.ReadADRawEntry(adObjectId, ContactInfoForLinkingFromDirectory.RequiredADProperties);
			if (adrawEntry != null)
			{
				return new ContactInfoForLinkingFromDirectory(adrawEntry);
			}
			return null;
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x000D6B54 File Offset: 0x000D4D54
		private static HashSet<string> GetEmailAddresses(ADRawEntry person)
		{
			HashSet<string> hashSet = new HashSet<string>();
			object obj;
			if (person.TryGetValueWithoutDefault(ADRecipientSchema.EmailAddresses, out obj))
			{
				ProxyAddressCollection proxyAddressCollection = obj as ProxyAddressCollection;
				if (proxyAddressCollection != null)
				{
					foreach (ProxyAddress proxyAddress in proxyAddressCollection)
					{
						SmtpProxyAddress smtpProxyAddress = proxyAddress as SmtpProxyAddress;
						if (smtpProxyAddress != null)
						{
							string text = ContactInfoForLinking.CanonicalizeEmailAddress(smtpProxyAddress.SmtpAddress);
							if (!string.IsNullOrEmpty(text))
							{
								hashSet.Add(text);
							}
						}
					}
				}
			}
			return hashSet;
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x000D6BF0 File Offset: 0x000D4DF0
		private static string[] GetSmtpAddressCache(ADRawEntry person)
		{
			List<string> list = new List<string>(1);
			object obj;
			if (person.TryGetValueWithoutDefault(ADRecipientSchema.EmailAddresses, out obj))
			{
				ProxyAddressCollection proxyAddressCollection = obj as ProxyAddressCollection;
				if (proxyAddressCollection != null)
				{
					foreach (ProxyAddress proxyAddress in proxyAddressCollection)
					{
						SmtpProxyAddress smtpProxyAddress = proxyAddress as SmtpProxyAddress;
						if (smtpProxyAddress != null)
						{
							string text = smtpProxyAddress.ToString();
							if (!string.IsNullOrEmpty(text))
							{
								list.Add(text);
							}
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x000D6C8C File Offset: 0x000D4E8C
		private static byte[] GetAddressBookId(ADRawEntry person)
		{
			string text = (string)person[ADRecipientSchema.LegacyExchangeDN];
			RecipientDisplayType? recipientDisplayType = (RecipientDisplayType?)person[ADRecipientSchema.RecipientDisplayType];
			Eidt eidt;
			if (!string.IsNullOrEmpty(text) && recipientDisplayType != null && ContactInfoForLinkingFromDirectory.TryGetEidt(recipientDisplayType.Value, out eidt))
			{
				return Microsoft.Exchange.Data.Storage.AddressBookEntryId.MakeAddressBookEntryID(text, eidt);
			}
			return null;
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x000D6CE8 File Offset: 0x000D4EE8
		private static bool TryGetEidt(RecipientDisplayType type, out Eidt eidt)
		{
			if (type <= RecipientDisplayType.ArbitrationMailbox)
			{
				if (type <= RecipientDisplayType.ACLableSyncedUSGasContact)
				{
					if (type != RecipientDisplayType.ACLableSyncedRemoteMailUser && type != RecipientDisplayType.ACLableSyncedUSGasContact)
					{
						goto IL_76;
					}
				}
				else
				{
					if (type == RecipientDisplayType.MailboxUser)
					{
						goto IL_71;
					}
					switch (type)
					{
					case RecipientDisplayType.RemoteMailUser:
						break;
					case RecipientDisplayType.ConferenceRoomMailbox:
					case RecipientDisplayType.EquipmentMailbox:
					case RecipientDisplayType.ArbitrationMailbox:
						goto IL_71;
					case (RecipientDisplayType)9:
						goto IL_76;
					default:
						goto IL_76;
					}
				}
			}
			else if (type <= RecipientDisplayType.ACLableMailboxUser)
			{
				if (type != RecipientDisplayType.TeamMailboxUser && type != RecipientDisplayType.ACLableMailboxUser)
				{
					goto IL_76;
				}
				goto IL_71;
			}
			else if (type != RecipientDisplayType.ACLableRemoteMailUser)
			{
				if (type != RecipientDisplayType.ACLableTeamMailboxUser)
				{
					goto IL_76;
				}
				goto IL_71;
			}
			eidt = Eidt.RemoteUser;
			return true;
			IL_71:
			eidt = Eidt.User;
			return true;
			IL_76:
			eidt = Eidt.User;
			return false;
		}

		// Token: 0x04001C3F RID: 7231
		internal static readonly ADPropertyDefinition[] RequiredADProperties = new ADPropertyDefinition[]
		{
			ADObjectSchema.Id,
			ADRecipientSchema.LegacyExchangeDN,
			ADRecipientSchema.RecipientDisplayType,
			ADRecipientSchema.EmailAddresses,
			ADUserSchema.RTCSIPPrimaryUserAddress
		};
	}
}
