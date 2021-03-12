using System;
using System.Collections.Generic;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DC5 RID: 3525
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SharingContext
	{
		// Token: 0x0600790B RID: 30987 RVA: 0x00216B91 File Offset: 0x00214D91
		internal SharingContext(Folder folderToShare) : this(folderToShare, null)
		{
		}

		// Token: 0x0600790C RID: 30988 RVA: 0x00216B9C File Offset: 0x00214D9C
		internal SharingContext(Folder folderToShare, SharingProvider sharingProvider) : this()
		{
			Util.ThrowOnNullArgument(folderToShare, "folderToShare");
			MailboxSession mailboxSession = folderToShare.Session as MailboxSession;
			IExchangePrincipal mailboxOwner = mailboxSession.MailboxOwner;
			if (sharingProvider == null)
			{
				SharingProvider[] compatibleProviders = SharingProvider.GetCompatibleProviders(folderToShare);
				if (compatibleProviders.Length == 0)
				{
					ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: Cannot share folder {1}: no compatible provider was found.", mailboxOwner, folderToShare.Id);
					throw new CannotShareFolderException(ServerStrings.NoProviderSupportShareFolder);
				}
				for (int i = 0; i < compatibleProviders.Length; i++)
				{
					ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, SharingProvider, VersionedId>((long)this.GetHashCode(), "{0}: Find compatible provider {1} for folder {2}.", mailboxOwner, compatibleProviders[i], folderToShare.Id);
					this.AvailableSharingProviders.Add(compatibleProviders[i], null);
				}
			}
			else if (!sharingProvider.IsCompatible(folderToShare))
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId, SharingProvider>((long)this.GetHashCode(), "{0}: Cannot share folder {1} with sharing provider: {2}.", mailboxOwner, folderToShare.Id, sharingProvider);
				if (sharingProvider == SharingProvider.SharingProviderPublish)
				{
					throw new FolderNotPublishedException();
				}
				throw new CannotShareFolderException(ServerStrings.NoProviderSupportShareFolder);
			}
			else
			{
				this.AvailableSharingProviders.Add(sharingProvider, null);
				if (sharingProvider == SharingProvider.SharingProviderPublish)
				{
					this.PopulateUrls(folderToShare);
				}
			}
			this.InitiatorName = mailboxOwner.MailboxInfo.DisplayName;
			this.InitiatorSmtpAddress = mailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
			this.InitiatorEntryId = AddressBookEntryId.MakeAddressBookEntryID(mailboxOwner);
			this.FolderClass = folderToShare.ClassName;
			this.FolderId = folderToShare.StoreObjectId;
			this.IsPrimary = (mailboxSession.IsDefaultFolderType(this.FolderId) != DefaultFolderType.None);
			this.FolderName = (this.IsPrimary ? this.DataType.DisplayName.ToString(mailboxSession.InternalPreferedCulture) : folderToShare.DisplayName);
			this.MailboxId = StoreEntryId.ToProviderStoreEntryId(mailboxOwner);
			this.SharingMessageType = SharingMessageType.Invitation;
			this.SharingPermissions = SharingContextPermissions.Reviewer;
			if (StringComparer.OrdinalIgnoreCase.Equals(folderToShare.ClassName, "IPF.Appointment"))
			{
				this.SharingDetail = (this.IsPrimary ? SharingContextDetailLevel.AvailabilityOnly : SharingContextDetailLevel.FullDetails);
			}
			this.SetDefaultCapabilities();
			this.UserLegacyDN = mailboxOwner.LegacyDn;
		}

		// Token: 0x0600790D RID: 30989 RVA: 0x00216D9F File Offset: 0x00214F9F
		private SharingContext()
		{
		}

		// Token: 0x1700205D RID: 8285
		// (get) Token: 0x0600790E RID: 30990 RVA: 0x00216DB9 File Offset: 0x00214FB9
		internal Dictionary<SharingProvider, CheckRecipientsResults> AvailableSharingProviders
		{
			get
			{
				return this.availableSharingProviders;
			}
		}

		// Token: 0x1700205E RID: 8286
		// (get) Token: 0x0600790F RID: 30991 RVA: 0x00216DC1 File Offset: 0x00214FC1
		internal SharingDataType DataType
		{
			get
			{
				return SharingDataType.FromContainerClass(this.FolderClass);
			}
		}

		// Token: 0x1700205F RID: 8287
		// (get) Token: 0x06007910 RID: 30992 RVA: 0x00216DCE File Offset: 0x00214FCE
		// (set) Token: 0x06007911 RID: 30993 RVA: 0x00216DE3 File Offset: 0x00214FE3
		internal bool IsPrimary
		{
			get
			{
				return (this.SharingFlavor & SharingFlavor.PrimaryOwnership) == SharingFlavor.PrimaryOwnership;
			}
			set
			{
				if (value)
				{
					this.SharingFlavor |= SharingFlavor.PrimaryOwnership;
					return;
				}
				this.SharingFlavor &= ~SharingFlavor.PrimaryOwnership;
			}
		}

		// Token: 0x17002060 RID: 8288
		// (get) Token: 0x06007912 RID: 30994 RVA: 0x00216E0D File Offset: 0x0021500D
		// (set) Token: 0x06007913 RID: 30995 RVA: 0x00216E1C File Offset: 0x0021501C
		internal SharingMessageType SharingMessageType
		{
			get
			{
				return SharingMessageType.GetSharingMessageType(this.SharingFlavor);
			}
			set
			{
				if (value.IsRequest && !this.IsPrimary)
				{
					throw new ArgumentException("Cannot request non-default folder!");
				}
				this.SharingFlavor &= ~this.SharingMessageType.SharingFlavor;
				this.SharingFlavor |= value.SharingFlavor;
			}
		}

		// Token: 0x17002061 RID: 8289
		// (get) Token: 0x06007914 RID: 30996 RVA: 0x00216E70 File Offset: 0x00215070
		// (set) Token: 0x06007915 RID: 30997 RVA: 0x00216E78 File Offset: 0x00215078
		internal string InitiatorName { get; set; }

		// Token: 0x17002062 RID: 8290
		// (get) Token: 0x06007916 RID: 30998 RVA: 0x00216E81 File Offset: 0x00215081
		// (set) Token: 0x06007917 RID: 30999 RVA: 0x00216E89 File Offset: 0x00215089
		internal string InitiatorSmtpAddress { get; set; }

		// Token: 0x17002063 RID: 8291
		// (get) Token: 0x06007918 RID: 31000 RVA: 0x00216E92 File Offset: 0x00215092
		// (set) Token: 0x06007919 RID: 31001 RVA: 0x00216E9A File Offset: 0x0021509A
		internal byte[] InitiatorEntryId { get; set; }

		// Token: 0x17002064 RID: 8292
		// (get) Token: 0x0600791A RID: 31002 RVA: 0x00216EA3 File Offset: 0x002150A3
		// (set) Token: 0x0600791B RID: 31003 RVA: 0x00216EAB File Offset: 0x002150AB
		internal string FolderClass { get; set; }

		// Token: 0x17002065 RID: 8293
		// (get) Token: 0x0600791C RID: 31004 RVA: 0x00216EB4 File Offset: 0x002150B4
		// (set) Token: 0x0600791D RID: 31005 RVA: 0x00216EBC File Offset: 0x002150BC
		internal string FolderName { get; set; }

		// Token: 0x17002066 RID: 8294
		// (get) Token: 0x0600791E RID: 31006 RVA: 0x00216EC5 File Offset: 0x002150C5
		// (set) Token: 0x0600791F RID: 31007 RVA: 0x00216ECD File Offset: 0x002150CD
		internal string FolderEwsId { get; set; }

		// Token: 0x17002067 RID: 8295
		// (get) Token: 0x06007920 RID: 31008 RVA: 0x00216ED6 File Offset: 0x002150D6
		// (set) Token: 0x06007921 RID: 31009 RVA: 0x00216EDE File Offset: 0x002150DE
		internal StoreObjectId FolderId { get; set; }

		// Token: 0x17002068 RID: 8296
		// (get) Token: 0x06007922 RID: 31010 RVA: 0x00216EE7 File Offset: 0x002150E7
		// (set) Token: 0x06007923 RID: 31011 RVA: 0x00216EEF File Offset: 0x002150EF
		internal byte[] MailboxId { get; set; }

		// Token: 0x17002069 RID: 8297
		// (get) Token: 0x06007924 RID: 31012 RVA: 0x00216EF8 File Offset: 0x002150F8
		// (set) Token: 0x06007925 RID: 31013 RVA: 0x00216F00 File Offset: 0x00215100
		internal SharingCapabilities SharingCapabilities { get; set; }

		// Token: 0x1700206A RID: 8298
		// (get) Token: 0x06007926 RID: 31014 RVA: 0x00216F09 File Offset: 0x00215109
		// (set) Token: 0x06007927 RID: 31015 RVA: 0x00216F11 File Offset: 0x00215111
		internal SharingFlavor SharingFlavor { get; set; }

		// Token: 0x1700206B RID: 8299
		// (get) Token: 0x06007928 RID: 31016 RVA: 0x00216F1A File Offset: 0x0021511A
		// (set) Token: 0x06007929 RID: 31017 RVA: 0x00216F22 File Offset: 0x00215122
		internal EncryptedSharedFolderData[] EncryptedSharedFolderDataCollection { get; set; }

		// Token: 0x1700206C RID: 8300
		// (get) Token: 0x0600792A RID: 31018 RVA: 0x00216F2B File Offset: 0x0021512B
		// (set) Token: 0x0600792B RID: 31019 RVA: 0x00216F33 File Offset: 0x00215133
		internal SharingContextPermissions SharingPermissions { get; set; }

		// Token: 0x1700206D RID: 8301
		// (get) Token: 0x0600792C RID: 31020 RVA: 0x00216F3C File Offset: 0x0021513C
		// (set) Token: 0x0600792D RID: 31021 RVA: 0x00216F44 File Offset: 0x00215144
		internal SharingContextDetailLevel SharingDetail { get; set; }

		// Token: 0x1700206E RID: 8302
		// (get) Token: 0x0600792E RID: 31022 RVA: 0x00216F4D File Offset: 0x0021514D
		// (set) Token: 0x0600792F RID: 31023 RVA: 0x00216F55 File Offset: 0x00215155
		internal string BrowseUrl { get; set; }

		// Token: 0x1700206F RID: 8303
		// (get) Token: 0x06007930 RID: 31024 RVA: 0x00216F5E File Offset: 0x0021515E
		// (set) Token: 0x06007931 RID: 31025 RVA: 0x00216F66 File Offset: 0x00215166
		internal string ICalUrl { get; set; }

		// Token: 0x17002070 RID: 8304
		// (get) Token: 0x06007932 RID: 31026 RVA: 0x00216F6F File Offset: 0x0021516F
		// (set) Token: 0x06007933 RID: 31027 RVA: 0x00216F77 File Offset: 0x00215177
		internal string UserLegacyDN { get; set; }

		// Token: 0x17002071 RID: 8305
		// (get) Token: 0x06007934 RID: 31028 RVA: 0x00216F80 File Offset: 0x00215180
		// (set) Token: 0x06007935 RID: 31029 RVA: 0x00216F88 File Offset: 0x00215188
		internal int CountOfApplied { get; set; }

		// Token: 0x17002072 RID: 8306
		// (get) Token: 0x06007936 RID: 31030 RVA: 0x00216F91 File Offset: 0x00215191
		private SharingContextSerializer Serializer
		{
			get
			{
				if (this.serializer == null)
				{
					this.serializer = new SharingContextSerializer(this);
				}
				return this.serializer;
			}
		}

		// Token: 0x17002073 RID: 8307
		// (get) Token: 0x06007937 RID: 31031 RVA: 0x00216FAD File Offset: 0x002151AD
		private SharingContextSerializerLegacy SerializerLegacy
		{
			get
			{
				if (this.serializerLegacy == null)
				{
					this.serializerLegacy = new SharingContextSerializerLegacy(this);
				}
				return this.serializerLegacy;
			}
		}

		// Token: 0x17002074 RID: 8308
		// (get) Token: 0x06007938 RID: 31032 RVA: 0x00216FCC File Offset: 0x002151CC
		internal SharingProvider PrimarySharingProvider
		{
			get
			{
				if (this.AvailableSharingProviders.ContainsKey(SharingProvider.SharingProviderInternal))
				{
					return SharingProvider.SharingProviderInternal;
				}
				List<SharingProvider> list = new List<SharingProvider>(this.AvailableSharingProviders.Keys);
				if (list.Count <= 0)
				{
					return null;
				}
				return list[0];
			}
		}

		// Token: 0x17002075 RID: 8309
		// (get) Token: 0x06007939 RID: 31033 RVA: 0x00217014 File Offset: 0x00215214
		internal SharingProvider FallbackSharingProvider
		{
			get
			{
				if (this.AvailableSharingProviders.ContainsKey(SharingProvider.SharingProviderPublish))
				{
					return SharingProvider.SharingProviderPublish;
				}
				return null;
			}
		}

		// Token: 0x0600793A RID: 31034 RVA: 0x00217030 File Offset: 0x00215230
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"SharingMessageType = ",
				this.SharingMessageType,
				",InitiatorName = ",
				this.InitiatorName,
				",InitiatorSmtpAddress = ",
				this.InitiatorSmtpAddress,
				",InitiatorEntryId = ",
				(this.InitiatorEntryId == null) ? string.Empty : HexConverter.ByteArrayToHexString(this.InitiatorEntryId),
				",FolderClass = ",
				this.FolderClass,
				",FolderName = ",
				this.FolderName,
				",FolderEwsId = ",
				this.FolderEwsId,
				",FolderId = ",
				(this.FolderId == null) ? string.Empty : this.FolderId.ToHexEntryId(),
				",MailboxId = ",
				(this.MailboxId == null) ? string.Empty : HexConverter.ByteArrayToHexString(this.MailboxId),
				",SharingCapabilities = ",
				this.SharingCapabilities,
				",SharingFlavor = ",
				this.SharingFlavor,
				",SharingPermissions = ",
				this.SharingPermissions,
				",SharingDetail = ",
				this.SharingDetail,
				",EncryptedSharedFolderDataCollection = ",
				this.EncryptedSharedFolderDataCollection
			});
		}

		// Token: 0x0600793B RID: 31035 RVA: 0x0021719C File Offset: 0x0021539C
		internal static SharingContext DeserializeFromDraft(MessageItem messageItem)
		{
			Util.ThrowOnNullArgument(messageItem, "messageItem");
			return new SharingContext
			{
				UserLegacyDN = messageItem.Session.UserLegacyDN
			}.DeserializeFromMessage(messageItem, true);
		}

		// Token: 0x0600793C RID: 31036 RVA: 0x002171D4 File Offset: 0x002153D4
		internal static SharingContext DeserializeFromMessage(MessageItem messageItem)
		{
			Util.ThrowOnNullArgument(messageItem, "messageItem");
			return new SharingContext
			{
				UserLegacyDN = messageItem.Session.UserLegacyDN
			}.DeserializeFromMessage(messageItem, false);
		}

		// Token: 0x0600793D RID: 31037 RVA: 0x0021720C File Offset: 0x0021540C
		internal void SerializeToDraft(MessageItem messageItem)
		{
			Util.ThrowOnNullArgument(messageItem, "messageItem");
			ExTraceGlobals.SharingTracer.TraceDebug<string, SharingContext>((long)this.GetHashCode(), "{0}: Serialize as MAPI properties into draft message. SharingContext: {1}", messageItem.Session.UserLegacyDN, this);
			this.SerializerLegacy.SaveIntoMessageProperties(messageItem, false);
			if (this.PrimarySharingProvider == SharingProvider.SharingProviderPublish)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<string, SharingContext>((long)this.GetHashCode(), "{0}: Serialize as x-properties into draft message. SharingContext: {1}", messageItem.Session.UserLegacyDN, this);
				this.SerializerLegacy.SaveIntoMessageXProperties(messageItem, false);
			}
		}

		// Token: 0x0600793E RID: 31038 RVA: 0x00217290 File Offset: 0x00215490
		internal void SerializeToMessage(MessageItem messageItem)
		{
			Util.ThrowOnNullArgument(messageItem, "messageItem");
			if (this.ShouldGenerateXmlMetadata)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<string, SharingContext>((long)this.GetHashCode(), "{0}: Serialize as sharing_metadata.xml into message. SharingContext: {1}", messageItem.Session.UserLegacyDN, this);
				this.Serializer.SaveIntoMetadataXml(messageItem);
			}
			if (this.NeedToBeCompatibleWithO12InternalSharing)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<string, SharingContext>((long)this.GetHashCode(), "{0}: Serialize as MAPI properties into message. SharingContext: {1}", messageItem.Session.UserLegacyDN, this);
				this.SerializerLegacy.SaveIntoMessageProperties(messageItem, false);
			}
			else
			{
				this.SerializerLegacy.SaveIntoMessageProperties(messageItem, true);
			}
			if (this.NeedToBeCompatibleWithO12PubCalSharing)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<string, SharingContext>((long)this.GetHashCode(), "{0}: Serialize as X-Sharing properties into message. SharingContext: {1}", messageItem.Session.UserLegacyDN, this);
				this.SerializerLegacy.SaveIntoMessageXProperties(messageItem, false);
				return;
			}
			this.SerializerLegacy.SaveIntoMessageXProperties(messageItem, true);
		}

		// Token: 0x0600793F RID: 31039 RVA: 0x0021736C File Offset: 0x0021556C
		internal SharingProvider GetTargetSharingProvider(ADRecipient mailboxOwner)
		{
			Util.ThrowOnNullArgument(mailboxOwner, "mailboxOwner");
			if (this.AvailableSharingProviders.Keys.Count == 1)
			{
				using (Dictionary<SharingProvider, CheckRecipientsResults>.KeyCollection.Enumerator enumerator = this.AvailableSharingProviders.Keys.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						SharingProvider sharingProvider = enumerator.Current;
						ExTraceGlobals.SharingTracer.TraceDebug<ADRecipient, SharingProvider>((long)this.GetHashCode(), "{0}: Found target provider {1} for current user.", mailboxOwner, sharingProvider);
						return sharingProvider;
					}
				}
			}
			foreach (KeyValuePair<SharingProvider, CheckRecipientsResults> keyValuePair in this.AvailableSharingProviders)
			{
				SharingProvider key = keyValuePair.Key;
				CheckRecipientsResults value = keyValuePair.Value;
				if (value != null && mailboxOwner.IsAnyAddressMatched(ValidRecipient.ConvertToStringArray(value.ValidRecipients)))
				{
					ExTraceGlobals.SharingTracer.TraceDebug<ADRecipient, SharingProvider>((long)this.GetHashCode(), "{0}: Found target provider {1} for current user.", mailboxOwner, key);
					return key;
				}
			}
			ExTraceGlobals.SharingTracer.TraceError<ADRecipient>((long)this.GetHashCode(), "{0}: No available provider is found for this user.", mailboxOwner);
			return null;
		}

		// Token: 0x06007940 RID: 31040 RVA: 0x00217498 File Offset: 0x00215698
		internal void PopulateUrls(Folder folderToShare)
		{
			using (PublishedFolder publishedFolder = PublishedFolder.Create(folderToShare))
			{
				PublishedCalendar publishedCalendar = publishedFolder as PublishedCalendar;
				if (publishedCalendar != null)
				{
					if (this.PrimarySharingProvider == SharingProvider.SharingProviderPublish)
					{
						publishedCalendar.TrySetObscureKind(ObscureKind.Normal);
					}
					this.ICalUrl = publishedCalendar.ICalUrl;
				}
				this.BrowseUrl = publishedFolder.BrowseUrl;
			}
		}

		// Token: 0x06007941 RID: 31041 RVA: 0x00217500 File Offset: 0x00215700
		internal void SetDefaultCapabilities()
		{
			if (this.IsPrimary)
			{
				this.SharingCapabilities = (SharingCapabilities.ReadSharing | SharingCapabilities.ItemPrivacy | SharingCapabilities.ScopeSingleFolder | SharingCapabilities.AccessControl);
				return;
			}
			this.SharingCapabilities = (SharingCapabilities.ReadSharing | SharingCapabilities.WriteSharing | SharingCapabilities.ItemPrivacy | SharingCapabilities.ScopeSingleFolder | SharingCapabilities.AccessControl);
		}

		// Token: 0x06007942 RID: 31042 RVA: 0x00217524 File Offset: 0x00215724
		private SharingContext DeserializeFromMessage(MessageItem messageItem, bool isDraft)
		{
			ExTraceGlobals.SharingTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "{0}: Try reading from metadata xml of message attchment. IsDraft = {1}", messageItem.Session.UserLegacyDN, isDraft);
			if (!this.Serializer.ReadFromMetadataXml(messageItem))
			{
				ExTraceGlobals.SharingTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "{0}: No metadata xml, try reading from properties. IsDraft = {1}", messageItem.Session.UserLegacyDN, isDraft);
				this.SerializerLegacy.ReadFromMessageItem(messageItem, isDraft);
			}
			ExTraceGlobals.SharingTracer.TraceDebug<string, SharingContext>((long)this.GetHashCode(), "{0}: Deserialized from message. SharingContext: {1}", messageItem.Session.UserLegacyDN, this);
			return this;
		}

		// Token: 0x17002076 RID: 8310
		// (get) Token: 0x06007943 RID: 31043 RVA: 0x002175B3 File Offset: 0x002157B3
		private bool ShouldGenerateXmlMetadata
		{
			get
			{
				return this.PrimarySharingProvider != SharingProvider.SharingProviderPublish;
			}
		}

		// Token: 0x17002077 RID: 8311
		// (get) Token: 0x06007944 RID: 31044 RVA: 0x002175C8 File Offset: 0x002157C8
		private bool NeedToBeCompatibleWithO12InternalSharing
		{
			get
			{
				return this.IsProviderEffective(SharingProvider.SharingProviderInternal) && (this.DataType != SharingDataType.Calendar || this.SharingDetail == SharingContextDetailLevel.FullDetails || this.SharingMessageType == SharingMessageType.Request || this.SharingMessageType == SharingMessageType.DenyOfRequest);
			}
		}

		// Token: 0x17002078 RID: 8312
		// (get) Token: 0x06007945 RID: 31045 RVA: 0x00217616 File Offset: 0x00215816
		private bool NeedToBeCompatibleWithO12PubCalSharing
		{
			get
			{
				return this.IsProviderEffective(SharingProvider.SharingProviderPublish);
			}
		}

		// Token: 0x06007946 RID: 31046 RVA: 0x00217624 File Offset: 0x00215824
		private bool IsProviderEffective(SharingProvider provider)
		{
			if (!this.AvailableSharingProviders.ContainsKey(provider))
			{
				return false;
			}
			CheckRecipientsResults checkRecipientsResults = this.AvailableSharingProviders[provider];
			return checkRecipientsResults != null && checkRecipientsResults.ValidRecipients != null && checkRecipientsResults.ValidRecipients.Length != 0;
		}

		// Token: 0x040053AA RID: 21418
		private const SharingCapabilities DefaultSharingCapabilities = SharingCapabilities.ReadSharing | SharingCapabilities.WriteSharing | SharingCapabilities.ItemPrivacy | SharingCapabilities.ScopeSingleFolder | SharingCapabilities.AccessControl;

		// Token: 0x040053AB RID: 21419
		private SharingContextSerializer serializer;

		// Token: 0x040053AC RID: 21420
		private SharingContextSerializerLegacy serializerLegacy;

		// Token: 0x040053AD RID: 21421
		private Dictionary<SharingProvider, CheckRecipientsResults> availableSharingProviders = new Dictionary<SharingProvider, CheckRecipientsResults>(SharingProvider.AllSharingProviders.Length);
	}
}
