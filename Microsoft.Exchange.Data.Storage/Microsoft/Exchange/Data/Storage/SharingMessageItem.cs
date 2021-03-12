using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DD3 RID: 3539
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SharingMessageItem : MessageItem
	{
		// Token: 0x0600797B RID: 31099 RVA: 0x002192A2 File Offset: 0x002174A2
		internal SharingMessageItem(ICoreItem coreItem) : base(coreItem, false)
		{
		}

		// Token: 0x17002082 RID: 8322
		// (get) Token: 0x0600797C RID: 31100 RVA: 0x002192AC File Offset: 0x002174AC
		// (set) Token: 0x0600797D RID: 31101 RVA: 0x002192C4 File Offset: 0x002174C4
		public SharingMessageType SharingMessageType
		{
			get
			{
				this.CheckDisposed("SharingMessageType::get");
				return this.SharingContext.SharingMessageType;
			}
			set
			{
				this.CheckDisposed("SharingMessageType::set");
				this.SharingContext.SharingMessageType = value;
			}
		}

		// Token: 0x17002083 RID: 8323
		// (get) Token: 0x0600797E RID: 31102 RVA: 0x002192DD File Offset: 0x002174DD
		// (set) Token: 0x0600797F RID: 31103 RVA: 0x002192F5 File Offset: 0x002174F5
		public SharingContextPermissions SharingPermissions
		{
			get
			{
				this.CheckDisposed("SharingPermissions::get");
				return this.SharingContext.SharingPermissions;
			}
			set
			{
				this.CheckDisposed("SharingPermissions::set");
				EnumValidator.ThrowIfInvalid<SharingContextPermissions>(value, "value");
				this.SharingContext.SharingPermissions = value;
			}
		}

		// Token: 0x17002084 RID: 8324
		// (get) Token: 0x06007980 RID: 31104 RVA: 0x00219319 File Offset: 0x00217519
		// (set) Token: 0x06007981 RID: 31105 RVA: 0x00219331 File Offset: 0x00217531
		public SharingContextDetailLevel SharingDetail
		{
			get
			{
				this.CheckDisposed("SharingDetail::get");
				return this.SharingContext.SharingDetail;
			}
			set
			{
				this.CheckDisposed("SharingDetail::set");
				EnumValidator.ThrowIfInvalid<SharingContextDetailLevel>(value, "value");
				this.SharingContext.SharingDetail = value;
			}
		}

		// Token: 0x17002085 RID: 8325
		// (get) Token: 0x06007982 RID: 31106 RVA: 0x00219355 File Offset: 0x00217555
		public string InitiatorName
		{
			get
			{
				this.CheckDisposed("InitiatorName::get");
				return this.SharingContext.InitiatorName;
			}
		}

		// Token: 0x17002086 RID: 8326
		// (get) Token: 0x06007983 RID: 31107 RVA: 0x0021936D File Offset: 0x0021756D
		public string InitiatorSmtpAddress
		{
			get
			{
				this.CheckDisposed("InitiatorSmtpAddress::get");
				return this.SharingContext.InitiatorSmtpAddress;
			}
		}

		// Token: 0x17002087 RID: 8327
		// (get) Token: 0x06007984 RID: 31108 RVA: 0x00219385 File Offset: 0x00217585
		public string SharedFolderName
		{
			get
			{
				this.CheckDisposed("SharedFolderName::get");
				return this.SharingContext.FolderName;
			}
		}

		// Token: 0x17002088 RID: 8328
		// (get) Token: 0x06007985 RID: 31109 RVA: 0x0021939D File Offset: 0x0021759D
		public DefaultFolderType SharedFolderType
		{
			get
			{
				this.CheckDisposed("SharedFolderType::get");
				return this.SharingContext.DataType.DefaultFolderType;
			}
		}

		// Token: 0x17002089 RID: 8329
		// (get) Token: 0x06007986 RID: 31110 RVA: 0x002193BA File Offset: 0x002175BA
		public bool IsSharedFolderPrimary
		{
			get
			{
				this.CheckDisposed("IsSharedFolderPrimary::get");
				return this.SharingContext.IsPrimary;
			}
		}

		// Token: 0x1700208A RID: 8330
		// (get) Token: 0x06007987 RID: 31111 RVA: 0x002193D2 File Offset: 0x002175D2
		public string BrowseUrl
		{
			get
			{
				this.CheckDisposed("BrowseUrl::get");
				return this.SharingContext.BrowseUrl;
			}
		}

		// Token: 0x1700208B RID: 8331
		// (get) Token: 0x06007988 RID: 31112 RVA: 0x002193EA File Offset: 0x002175EA
		public string ICalUrl
		{
			get
			{
				this.CheckDisposed("ICalUrl::get");
				return this.SharingContext.ICalUrl;
			}
		}

		// Token: 0x1700208C RID: 8332
		// (get) Token: 0x06007989 RID: 31113 RVA: 0x00219404 File Offset: 0x00217604
		// (set) Token: 0x0600798A RID: 31114 RVA: 0x0021943A File Offset: 0x0021763A
		public SharingResponseType SharingResponseType
		{
			get
			{
				this.CheckDisposed("SharingResponseType::get");
				SharingResponseType? valueAsNullable = base.GetValueAsNullable<SharingResponseType>(SharingMessageItemSchema.SharingResponseType);
				if (valueAsNullable == null)
				{
					return SharingResponseType.None;
				}
				return valueAsNullable.Value;
			}
			private set
			{
				this.CheckDisposed("SharingResponseType::set");
				this[SharingMessageItemSchema.SharingResponseType] = value;
			}
		}

		// Token: 0x1700208D RID: 8333
		// (get) Token: 0x0600798B RID: 31115 RVA: 0x00219458 File Offset: 0x00217658
		// (set) Token: 0x0600798C RID: 31116 RVA: 0x00219470 File Offset: 0x00217670
		public ExDateTime? SharingResponseTime
		{
			get
			{
				this.CheckDisposed("SharingResponseTime::get");
				return base.GetValueAsNullable<ExDateTime>(SharingMessageItemSchema.SharingResponseTime);
			}
			private set
			{
				this.CheckDisposed("SharingResponseTime::set");
				this[SharingMessageItemSchema.SharingResponseTime] = value;
			}
		}

		// Token: 0x1700208E RID: 8334
		// (get) Token: 0x0600798D RID: 31117 RVA: 0x0021948E File Offset: 0x0021768E
		// (set) Token: 0x0600798E RID: 31118 RVA: 0x002194A6 File Offset: 0x002176A6
		public ExDateTime? SharingLastSubscribeTime
		{
			get
			{
				this.CheckDisposed("SharingLastSubscribeTime::get");
				return base.GetValueAsNullable<ExDateTime>(SharingMessageItemSchema.SharingLastSubscribeTime);
			}
			private set
			{
				this.CheckDisposed("SharingLastSubscribeTime::set");
				this[SharingMessageItemSchema.SharingLastSubscribeTime] = value;
			}
		}

		// Token: 0x1700208F RID: 8335
		// (get) Token: 0x0600798F RID: 31119 RVA: 0x002194C4 File Offset: 0x002176C4
		public bool IsPublishing
		{
			get
			{
				this.CheckDisposed("IsPublishing::get");
				return this.SharingContext.PrimarySharingProvider == SharingProvider.SharingProviderPublish || this.TryGetTargetSharingProvider() == SharingProvider.SharingProviderPublish;
			}
		}

		// Token: 0x17002090 RID: 8336
		// (get) Token: 0x06007990 RID: 31120 RVA: 0x002194F2 File Offset: 0x002176F2
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return SharingMessageItemSchema.Instance;
			}
		}

		// Token: 0x17002091 RID: 8337
		// (get) Token: 0x06007991 RID: 31121 RVA: 0x00219504 File Offset: 0x00217704
		// (set) Token: 0x06007992 RID: 31122 RVA: 0x00219517 File Offset: 0x00217717
		public IFrontEndLocator FrontEndLocator
		{
			get
			{
				this.CheckDisposed("FrontEndLocator::get");
				return this.frontEndLocator;
			}
			set
			{
				this.CheckDisposed("FrontEndLocator::set");
				Util.ThrowOnNullArgument(value, "FrontEndLocator");
				this.frontEndLocator = value;
			}
		}

		// Token: 0x17002092 RID: 8338
		// (get) Token: 0x06007993 RID: 31123 RVA: 0x00219536 File Offset: 0x00217736
		// (set) Token: 0x06007994 RID: 31124 RVA: 0x00219556 File Offset: 0x00217756
		internal SharingProvider ForceSharingProvider
		{
			get
			{
				if (this.forceSharingProvider != null)
				{
					return this.forceSharingProvider;
				}
				if (this.IsPublishing)
				{
					return SharingProvider.SharingProviderPublish;
				}
				return null;
			}
			set
			{
				this.forceSharingProvider = value;
			}
		}

		// Token: 0x17002093 RID: 8339
		// (get) Token: 0x06007995 RID: 31125 RVA: 0x0021955F File Offset: 0x0021775F
		internal bool CanUseFallback
		{
			get
			{
				return this.SharingContext.FallbackSharingProvider != null;
			}
		}

		// Token: 0x17002094 RID: 8340
		// (get) Token: 0x06007996 RID: 31126 RVA: 0x00219572 File Offset: 0x00217772
		// (set) Token: 0x06007997 RID: 31127 RVA: 0x0021957A File Offset: 0x0021777A
		internal bool FallbackEnabled
		{
			get
			{
				return this.fallbackEnabled;
			}
			private set
			{
				if (value && !this.CanUseFallback)
				{
					throw new InvalidOperationException("No provider to fall back on.");
				}
				this.fallbackEnabled = value;
			}
		}

		// Token: 0x17002095 RID: 8341
		// (get) Token: 0x06007998 RID: 31128 RVA: 0x00219599 File Offset: 0x00217799
		internal SharingContext RawSharingContext
		{
			get
			{
				return this.sharingContext;
			}
		}

		// Token: 0x17002096 RID: 8342
		// (get) Token: 0x06007999 RID: 31129 RVA: 0x002195A4 File Offset: 0x002177A4
		private MailboxSession MailboxSession
		{
			get
			{
				return base.Session as MailboxSession;
			}
		}

		// Token: 0x17002097 RID: 8343
		// (get) Token: 0x0600799A RID: 31130 RVA: 0x002195C0 File Offset: 0x002177C0
		private ADRecipient MailboxOwner
		{
			get
			{
				if (this.mailboxOwner == null)
				{
					this.mailboxOwner = DirectoryHelper.ReadADRecipient(this.MailboxSession.MailboxOwner.MailboxInfo.MailboxGuid, this.MailboxSession.MailboxOwner.MailboxInfo.IsArchive, this.MailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid));
				}
				return this.mailboxOwner;
			}
		}

		// Token: 0x17002098 RID: 8344
		// (get) Token: 0x0600799B RID: 31131 RVA: 0x0021961D File Offset: 0x0021781D
		// (set) Token: 0x0600799C RID: 31132 RVA: 0x0021964F File Offset: 0x0021784F
		private SharingContext SharingContext
		{
			get
			{
				if (this.sharingContext == null)
				{
					if (base.IsDraft)
					{
						this.sharingContext = SharingContext.DeserializeFromDraft(this);
					}
					else
					{
						this.sharingContext = SharingContext.DeserializeFromMessage(this);
					}
				}
				return this.sharingContext;
			}
			set
			{
				if (this.sharingContext != null)
				{
					throw new InvalidOperationException("Sharing context has been set already.");
				}
				this.sharingContext = value;
			}
		}

		// Token: 0x0600799D RID: 31133 RVA: 0x0021966B File Offset: 0x0021786B
		public static SharingMessageItem Create(MailboxSession session, StoreId destFolderId, StoreId folderIdToShare)
		{
			return SharingMessageItem.InternalCreate(session, destFolderId, folderIdToShare, null);
		}

		// Token: 0x0600799E RID: 31134 RVA: 0x00219678 File Offset: 0x00217878
		public static SharingMessageItem CreateWithSpecficProvider(MailboxSession session, StoreId destFolderId, StoreId folderIdToShare, SharingProvider provider, bool force)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			SharingMessageItem sharingMessageItem = SharingMessageItem.InternalCreate(session, destFolderId, folderIdToShare, provider);
			if (force)
			{
				sharingMessageItem.ForceSharingProvider = provider;
			}
			return sharingMessageItem;
		}

		// Token: 0x0600799F RID: 31135 RVA: 0x002196A9 File Offset: 0x002178A9
		public static SharingMessageItem CreateForPublishing(MailboxSession session, StoreId destFolderId, StoreId folderIdToShare)
		{
			return SharingMessageItem.InternalCreate(session, destFolderId, folderIdToShare, SharingProvider.SharingProviderPublish);
		}

		// Token: 0x060079A0 RID: 31136 RVA: 0x002196B8 File Offset: 0x002178B8
		public new static SharingMessageItem Bind(StoreSession session, StoreId messageId)
		{
			return SharingMessageItem.Bind(session, messageId, null);
		}

		// Token: 0x060079A1 RID: 31137 RVA: 0x002196C4 File Offset: 0x002178C4
		public new static SharingMessageItem Bind(StoreSession session, StoreId messageId, ICollection<PropertyDefinition> propsToReturn)
		{
			if (!(session is MailboxSession))
			{
				throw new ArgumentException("session");
			}
			return ItemBuilder.ItemBind<SharingMessageItem>(session, messageId, SharingMessageItemSchema.Instance, propsToReturn);
		}

		// Token: 0x060079A2 RID: 31138 RVA: 0x002196F3 File Offset: 0x002178F3
		public SharingMessageItem AcceptRequest(StoreId destFolderId)
		{
			this.CheckDisposed("AcceptRequest");
			Util.ThrowOnNullArgument(destFolderId, "destFolderId");
			return this.CreateRequestResponseMessage(SharingResponseType.Allowed, destFolderId);
		}

		// Token: 0x060079A3 RID: 31139 RVA: 0x00219713 File Offset: 0x00217913
		public SharingMessageItem DenyRequest(StoreId destFolderId)
		{
			this.CheckDisposed("DenyRequest");
			Util.ThrowOnNullArgument(destFolderId, "destFolderId");
			return this.CreateRequestResponseMessage(SharingResponseType.Denied, destFolderId);
		}

		// Token: 0x060079A4 RID: 31140 RVA: 0x00219733 File Offset: 0x00217933
		public void DenyRequestWithoutResponse()
		{
			this.CheckDisposed("DenyRequestWithoutResponse");
			this.PostResponded(SharingResponseType.Denied);
		}

		// Token: 0x060079A5 RID: 31141 RVA: 0x00219748 File Offset: 0x00217948
		public SubscribeResults SubscribeAndOpen()
		{
			this.CheckDisposed("SubscribeAndOpen");
			return this.Subscribe();
		}

		// Token: 0x060079A6 RID: 31142 RVA: 0x00219768 File Offset: 0x00217968
		public SubscribeResults Subscribe()
		{
			this.CheckDisposed("Subscribe");
			if (base.IsDraft)
			{
				throw new InvalidOperationException("Cannot subscribe draft message.");
			}
			SharingProvider targetSharingProvider = this.GetTargetSharingProvider();
			SubscribeResults result = targetSharingProvider.PerformSubscribe(this.MailboxSession, this.SharingContext);
			this.SaveSubscribeTime();
			return result;
		}

		// Token: 0x060079A7 RID: 31143 RVA: 0x002197B4 File Offset: 0x002179B4
		public void SetSubmitFlags(SharingSubmitFlags sharingSubmitFlags)
		{
			this.CheckDisposed("SetSubmitFlags");
			EnumValidator.ThrowIfInvalid<SharingSubmitFlags>(sharingSubmitFlags, "sharingSubmitFlags");
			if ((sharingSubmitFlags & SharingSubmitFlags.Auto) == SharingSubmitFlags.Auto && this.CanUseFallback)
			{
				this.FallbackEnabled = true;
				return;
			}
			this.FallbackEnabled = false;
		}

		// Token: 0x060079A8 RID: 31144 RVA: 0x002197E9 File Offset: 0x002179E9
		protected override void OnBeforeSave()
		{
			if (base.IsDraft && this.sharingContext != null && !this.isSending)
			{
				this.sharingContext.SerializeToDraft(this);
			}
			base.OnBeforeSave();
		}

		// Token: 0x060079A9 RID: 31145 RVA: 0x00219818 File Offset: 0x00217A18
		protected override void OnBeforeSend()
		{
			base.CoreItem.SaveRecipients();
			CheckRecipientsResults checkRecipientsResults = this.CheckRecipients();
			if (checkRecipientsResults.InvalidRecipients != null && checkRecipientsResults.InvalidRecipients.Length > 0)
			{
				throw new InvalidSharingRecipientsException(checkRecipientsResults.InvalidRecipients, new RecipientNotSupportedByAnyProviderException());
			}
			this.PerformInvitation();
			this.SharingContext.SerializeToMessage(this);
			this.AddBodyPrefix(this.CreateBodyPrefix());
			this.isSending = true;
			if (this.SharingMessageType.IsResponseToRequest)
			{
				SharingMessageItem sharingMessageItem = this.TryGetOriginalMessage();
				if (sharingMessageItem != null)
				{
					try
					{
						SharingResponseType responseTypeFromMessageType = SharingMessageItem.GetResponseTypeFromMessageType(this.SharingMessageType);
						sharingMessageItem.PostResponded(responseTypeFromMessageType);
					}
					finally
					{
						sharingMessageItem.Dispose();
					}
				}
			}
			this[MessageItemSchema.RecipientReassignmentProhibited] = !this.IsPublishing;
			base.OnBeforeSend();
		}

		// Token: 0x060079AA RID: 31146 RVA: 0x002198E4 File Offset: 0x00217AE4
		private static SharingMessageItem InternalCreate(MailboxSession mailboxSession, StoreId destFolderId, StoreId folderIdToShare, SharingProvider provider)
		{
			Util.ThrowOnNullArgument(mailboxSession, "mailboxSession");
			Util.ThrowOnNullArgument(destFolderId, "destFolderId");
			Util.ThrowOnNullArgument(folderIdToShare, "folderIdToShare");
			SharingMessageItem result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				SharingMessageItem sharingMessageItem = ItemBuilder.CreateNewItem<SharingMessageItem>(mailboxSession, destFolderId, ItemCreateInfo.SharingMessageItemInfo, CreateMessageType.Normal);
				disposeGuard.Add<SharingMessageItem>(sharingMessageItem);
				sharingMessageItem[InternalSchema.ItemClass] = "IPM.Sharing";
				using (Folder folder = Folder.Bind(mailboxSession, folderIdToShare))
				{
					sharingMessageItem.SharingContext = new SharingContext(folder, provider);
				}
				disposeGuard.Success();
				result = sharingMessageItem;
			}
			return result;
		}

		// Token: 0x060079AB RID: 31147 RVA: 0x00219998 File Offset: 0x00217B98
		private static SharingResponseType GetResponseTypeFromMessageType(SharingMessageType messageType)
		{
			if (messageType == SharingMessageType.AcceptOfRequest)
			{
				return SharingResponseType.Allowed;
			}
			if (messageType == SharingMessageType.DenyOfRequest)
			{
				return SharingResponseType.Denied;
			}
			throw new ArgumentException("messageType");
		}

		// Token: 0x060079AC RID: 31148 RVA: 0x002199B8 File Offset: 0x00217BB8
		private static SharingMessageType GetMessageTypeFromResponseType(SharingResponseType responseType)
		{
			switch (responseType)
			{
			case SharingResponseType.Allowed:
				return SharingMessageType.AcceptOfRequest;
			case SharingResponseType.Denied:
				return SharingMessageType.DenyOfRequest;
			default:
				throw new ArgumentException("responseType");
			}
		}

		// Token: 0x060079AD RID: 31149 RVA: 0x002199F0 File Offset: 0x00217BF0
		private static LocalizedString GetSubjectPrefixFromResponseType(SharingResponseType responseType)
		{
			switch (responseType)
			{
			case SharingResponseType.Allowed:
				return ClientStrings.SharingRequestAllowed;
			case SharingResponseType.Denied:
				return ClientStrings.SharingRequestDenied;
			default:
				throw new ArgumentException("responseType");
			}
		}

		// Token: 0x060079AE RID: 31150 RVA: 0x00219A28 File Offset: 0x00217C28
		private SharingMessageItem CreateRequestResponseMessage(SharingResponseType responseType, StoreId destFolderId)
		{
			if (base.IsDraft)
			{
				throw new InvalidOperationException("Cannot response on draft message.");
			}
			if (!this.SharingMessageType.IsRequest)
			{
				throw new InvalidOperationException("Only can response to a request message.");
			}
			StoreId defaultFolderId = this.MailboxSession.GetDefaultFolderId(this.SharedFolderType);
			SharingMessageItem sharingMessageItem = SharingMessageItem.InternalCreate(this.MailboxSession, destFolderId, defaultFolderId, this.GetTargetSharingProvider());
			sharingMessageItem.SharingMessageType = SharingMessageItem.GetMessageTypeFromResponseType(responseType);
			sharingMessageItem.Recipients.Add(base.From);
			sharingMessageItem[SharingMessageItemSchema.SharingOriginalMessageEntryId] = HexConverter.HexStringToByteArray(base.StoreObjectId.ToHexEntryId());
			sharingMessageItem[InternalSchema.NormalizedSubject] = this[InternalSchema.NormalizedSubject];
			sharingMessageItem[InternalSchema.SubjectPrefix] = SharingMessageItem.GetSubjectPrefixFromResponseType(responseType).ToString(this.MailboxSession.InternalPreferedCulture);
			return sharingMessageItem;
		}

		// Token: 0x060079AF RID: 31151 RVA: 0x00219AFC File Offset: 0x00217CFC
		private SharingProvider GetTargetSharingProvider()
		{
			SharingProvider sharingProvider = this.TryGetTargetSharingProvider();
			if (sharingProvider == null)
			{
				throw new InvalidSharingTargetRecipientException();
			}
			return sharingProvider;
		}

		// Token: 0x060079B0 RID: 31152 RVA: 0x00219B1A File Offset: 0x00217D1A
		private SharingProvider TryGetTargetSharingProvider()
		{
			if (this.MailboxSession.MailboxOwner.MailboxInfo.IsArchive)
			{
				return null;
			}
			return this.SharingContext.GetTargetSharingProvider(this.MailboxOwner);
		}

		// Token: 0x060079B1 RID: 31153 RVA: 0x00219B48 File Offset: 0x00217D48
		private CheckRecipientsResults CheckRecipients()
		{
			List<string> list = new List<string>(base.Recipients.Count);
			List<string> list2 = new List<string>(base.Recipients.Count);
			foreach (Recipient recipient in base.Recipients)
			{
				string valueOrDefault = recipient.GetValueOrDefault<string>(ParticipantSchema.SmtpAddress, string.Empty);
				if (!string.IsNullOrEmpty(valueOrDefault))
				{
					list2.Add(valueOrDefault);
				}
				else
				{
					list.Add(recipient.Participant.EmailAddress);
				}
			}
			List<ValidRecipient> list3 = new List<ValidRecipient>(list2.Count);
			string[] array = list2.ToArray();
			List<SharingProvider> list4 = new List<SharingProvider>(this.SharingContext.AvailableSharingProviders.Keys);
			foreach (SharingProvider sharingProvider in list4)
			{
				if (array == null || array.Length == 0)
				{
					break;
				}
				CheckRecipientsResults checkRecipientsResults;
				if (this.ForceSharingProvider != null)
				{
					if (sharingProvider == this.ForceSharingProvider)
					{
						checkRecipientsResults = new CheckRecipientsResults(ValidRecipient.ConvertFromStringArray(array));
					}
					else
					{
						checkRecipientsResults = new CheckRecipientsResults(array);
					}
					this.SharingContext.AvailableSharingProviders[sharingProvider] = checkRecipientsResults;
				}
				else
				{
					checkRecipientsResults = sharingProvider.CheckRecipients(this.MailboxOwner, this.SharingContext, array);
				}
				list3.AddRange(checkRecipientsResults.ValidRecipients);
				array = checkRecipientsResults.InvalidRecipients;
			}
			list.AddRange(array);
			return new CheckRecipientsResults(list3.ToArray(), list.ToArray());
		}

		// Token: 0x060079B2 RID: 31154 RVA: 0x00219CEC File Offset: 0x00217EEC
		private SharingMessageItem TryGetOriginalMessage()
		{
			byte[] valueOrDefault = base.GetValueOrDefault<byte[]>(SharingMessageItemSchema.SharingOriginalMessageEntryId, null);
			if (valueOrDefault == null)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "{0}: SharingOriginalMessageEntryId was not found", this.MailboxSession.MailboxOwner);
				return null;
			}
			StoreObjectId storeObjectId = null;
			try
			{
				storeObjectId = StoreObjectId.FromProviderSpecificId(valueOrDefault, StoreObjectType.Message);
			}
			catch (CorruptDataException)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "{0}: SharingOriginalMessageEntryId is invalid", this.MailboxSession.MailboxOwner);
				return null;
			}
			SharingMessageItem result;
			try
			{
				result = SharingMessageItem.Bind(this.MailboxSession, storeObjectId);
			}
			catch (ObjectNotFoundException)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, StoreObjectId>((long)this.GetHashCode(), "{0}: Original sharing request message was not found {1}", this.MailboxSession.MailboxOwner, storeObjectId);
				result = null;
			}
			return result;
		}

		// Token: 0x060079B3 RID: 31155 RVA: 0x00219DB4 File Offset: 0x00217FB4
		private void PostResponded(SharingResponseType responseType)
		{
			if (!this.SharingMessageType.IsRequest)
			{
				throw new InvalidOperationException("Only can response to a request message.");
			}
			base.OpenAsReadWrite();
			this.SharingResponseType = responseType;
			this.SharingResponseTime = new ExDateTime?(ExDateTime.Now);
			ConflictResolutionResult conflictResolutionResult = base.Save(SaveMode.ResolveConflicts);
			if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: Conflict occurred when saving response-status into request message {1}", this.MailboxSession.MailboxOwner, base.Id);
			}
			if (responseType == SharingResponseType.Denied)
			{
				try
				{
					SharingProvider targetSharingProvider = this.GetTargetSharingProvider();
					targetSharingProvider.PerformRevocation(this.MailboxSession, this.SharingContext);
				}
				catch (StoragePermanentException arg)
				{
					ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, StoragePermanentException>((long)this.GetHashCode(), "{0}: Error occurred when revoking sharing from denied requester. Exception = {1}", this.MailboxSession.MailboxOwner, arg);
				}
			}
		}

		// Token: 0x060079B4 RID: 31156 RVA: 0x00219E94 File Offset: 0x00218094
		private void PerformInvitation()
		{
			Dictionary<SharingProvider, ValidRecipient[]> dictionary = new Dictionary<SharingProvider, ValidRecipient[]>();
			List<ValidRecipient> list = new List<ValidRecipient>();
			SharingProvider fallbackSharingProvider = this.SharingContext.FallbackSharingProvider;
			using (FolderPermissionContext current = FolderPermissionContext.GetCurrent(this.MailboxSession, this.SharingContext))
			{
				bool flag = false;
				try
				{
					foreach (KeyValuePair<SharingProvider, CheckRecipientsResults> keyValuePair in this.SharingContext.AvailableSharingProviders)
					{
						SharingProvider key = keyValuePair.Key;
						CheckRecipientsResults value = keyValuePair.Value;
						if (this.FallbackEnabled && key == fallbackSharingProvider)
						{
							if (value != null)
							{
								list.AddRange(value.ValidRecipients);
							}
						}
						else if (value != null)
						{
							PerformInvitationResults performInvitationResults = key.PerformInvitation(this.MailboxSession, this.SharingContext, value.ValidRecipients, this.FrontEndLocator);
							ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, SharingProvider, PerformInvitationResults>((long)this.GetHashCode(), "{0}: Performed invitation by provider {1}. Result = {2}", this.MailboxSession.MailboxOwner, key, performInvitationResults);
							if (performInvitationResults.Result == PerformInvitationResultType.Failed || performInvitationResults.Result == PerformInvitationResultType.PartiallySuccess)
							{
								if (!this.FallbackEnabled)
								{
									StoreObjectId folderId = this.SharingContext.FolderId;
									InvalidSharingRecipientsResolution invalidSharingRecipientsResolution;
									if (!this.CanUseFallback)
									{
										invalidSharingRecipientsResolution = new InvalidSharingRecipientsResolution(folderId);
									}
									else
									{
										using (Folder folder = Folder.Bind(this.MailboxSession, folderId))
										{
											this.SharingContext.PopulateUrls(folder);
										}
										invalidSharingRecipientsResolution = new InvalidSharingRecipientsResolution(this.BrowseUrl, this.ICalUrl);
									}
									ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, InvalidSharingRecipientsResolution>((long)this.GetHashCode(), "{0}: No fall back for these invalid recipients. Resolution = {1}", this.MailboxSession.MailboxOwner, invalidSharingRecipientsResolution);
									throw new InvalidSharingRecipientsException(performInvitationResults.FailedRecipients, invalidSharingRecipientsResolution);
								}
								ValidRecipient[] array = Array.ConvertAll<InvalidRecipient, ValidRecipient>(performInvitationResults.FailedRecipients, (InvalidRecipient invalidRecipient) => new ValidRecipient(invalidRecipient.SmtpAddress, null));
								ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, SharingProvider, int>((long)this.GetHashCode(), "{0}: Fall back on provider {1} for these {2} failed recipients.", this.MailboxSession.MailboxOwner, fallbackSharingProvider, array.Length);
								list.AddRange(array);
								dictionary.Add(key, performInvitationResults.SucceededRecipients);
							}
						}
					}
					if (this.FallbackEnabled)
					{
						foreach (KeyValuePair<SharingProvider, ValidRecipient[]> keyValuePair2 in dictionary)
						{
							SharingProvider key2 = keyValuePair2.Key;
							ValidRecipient[] value2 = keyValuePair2.Value;
							this.SharingContext.AvailableSharingProviders[key2] = new CheckRecipientsResults(value2);
						}
						this.SharingContext.AvailableSharingProviders[fallbackSharingProvider] = new CheckRecipientsResults(list.ToArray());
						PerformInvitationResults performInvitationResults2 = fallbackSharingProvider.PerformInvitation(this.MailboxSession, this.SharingContext, list.ToArray(), this.FrontEndLocator);
						ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, SharingProvider, PerformInvitationResults>((long)this.GetHashCode(), "{0}: Performed invitation by fallback provider {1}. Result = {2}", this.MailboxSession.MailboxOwner, fallbackSharingProvider, performInvitationResults2);
						if (performInvitationResults2.Result == PerformInvitationResultType.Failed || performInvitationResults2.Result == PerformInvitationResultType.PartiallySuccess)
						{
							throw new InvalidOperationException("The fallback provider should never fail.");
						}
					}
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						current.Disable();
					}
				}
			}
		}

		// Token: 0x060079B5 RID: 31157 RVA: 0x0021A224 File Offset: 0x00218424
		private void SaveSubscribeTime()
		{
			base.OpenAsReadWrite();
			this.SharingLastSubscribeTime = new ExDateTime?(ExDateTime.Now);
			ConflictResolutionResult conflictResolutionResult = base.Save(SaveMode.ResolveConflicts);
			if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
			{
				ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal>((long)this.GetHashCode(), "{0}: Conflict occurred when saving last-subscribe-time.", this.MailboxSession.MailboxOwner);
			}
		}

		// Token: 0x060079B6 RID: 31158 RVA: 0x0021A27C File Offset: 0x0021847C
		private void AddBodyPrefix(string prefix)
		{
			byte[] array = null;
			BodyReadConfiguration configuration = new BodyReadConfiguration(base.Body.RawFormat, base.Body.RawCharset.Name);
			using (Stream stream = base.Body.OpenReadStream(configuration))
			{
				array = Util.StreamHandler.ReadBytesFromStream(stream);
			}
			BodyWriteConfiguration bodyWriteConfiguration = new BodyWriteConfiguration(base.Body.RawFormat, base.Body.RawCharset.Name);
			bodyWriteConfiguration.SetTargetFormat(base.Body.Format, base.Body.Charset);
			bodyWriteConfiguration.AddInjectedText(prefix, null, BodyInjectionFormat.Text);
			using (Stream stream2 = base.Body.OpenWriteStream(bodyWriteConfiguration))
			{
				stream2.Write(array, 0, array.Length);
			}
		}

		// Token: 0x060079B7 RID: 31159 RVA: 0x0021A358 File Offset: 0x00218558
		private string CreateBodyPrefix()
		{
			CultureInfo internalPreferedCulture = this.MailboxSession.InternalPreferedCulture;
			string displayName = this.MailboxSession.MailboxOwner.MailboxInfo.DisplayName;
			string email = this.MailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
			LocalizedString displayName2 = this.SharingContext.DataType.DisplayName;
			StringBuilder stringBuilder = new StringBuilder();
			if (!this.IsPublishing)
			{
				if (this.SharingContext.SharingMessageType == SharingMessageType.Invitation)
				{
					if (this.SharingContext.IsPrimary)
					{
						stringBuilder.AppendLine(ClientStrings.SharingInvitation(displayName, email, displayName2).ToString(internalPreferedCulture));
					}
					else
					{
						stringBuilder.AppendLine(ClientStrings.SharingInvitationNonPrimary(displayName, email, this.SharedFolderName, displayName2).ToString(internalPreferedCulture));
					}
					stringBuilder.AppendLine();
					stringBuilder.AppendLine(ClientStrings.SharingInvitationInstruction.ToString(internalPreferedCulture));
				}
				else if (this.SharingContext.SharingMessageType == SharingMessageType.Request)
				{
					stringBuilder.AppendLine(ClientStrings.SharingRequest(displayName, email, displayName2).ToString(internalPreferedCulture));
					stringBuilder.AppendLine();
					stringBuilder.AppendLine(ClientStrings.SharingRequestInstruction.ToString(internalPreferedCulture));
				}
				else if (this.SharingContext.SharingMessageType == SharingMessageType.InvitationAndRequest)
				{
					stringBuilder.AppendLine(ClientStrings.SharingInvitationAndRequest(displayName, email, displayName2).ToString(internalPreferedCulture));
					stringBuilder.AppendLine();
					stringBuilder.AppendLine(ClientStrings.SharingInvitationAndRequestInstruction.ToString(internalPreferedCulture));
				}
				else if (this.SharingContext.SharingMessageType == SharingMessageType.AcceptOfRequest)
				{
					stringBuilder.AppendLine(ClientStrings.SharingAccept(displayName, email, displayName2).ToString(internalPreferedCulture));
				}
				else if (this.SharingContext.SharingMessageType == SharingMessageType.DenyOfRequest)
				{
					stringBuilder.AppendLine(ClientStrings.SharingDecline(displayName, email, displayName2).ToString(internalPreferedCulture));
				}
			}
			if (this.SharingContext.SharingMessageType.IsInvitationOrAcceptOfRequest)
			{
				if (this.SharingContext.BrowseUrl != null)
				{
					stringBuilder.AppendLine();
					stringBuilder.AppendLine(ClientStrings.SharingAnonymous(displayName, displayName2, this.SharedFolderName, this.SharingContext.BrowseUrl.ToString()).ToString(internalPreferedCulture));
				}
				if (this.SharingContext.ICalUrl != null)
				{
					UriBuilder uriBuilder = new UriBuilder(this.SharingContext.ICalUrl);
					uriBuilder.Scheme = "webcal";
					uriBuilder.Port = -1;
					stringBuilder.AppendLine();
					stringBuilder.AppendLine(ClientStrings.SharingICal(uriBuilder.Uri.ToString()).ToString(internalPreferedCulture));
				}
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("*~*~*~*~*~*~*~*~*~*");
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		// Token: 0x040053FB RID: 21499
		private bool isSending;

		// Token: 0x040053FC RID: 21500
		private SharingContext sharingContext;

		// Token: 0x040053FD RID: 21501
		private ADRecipient mailboxOwner;

		// Token: 0x040053FE RID: 21502
		private SharingProvider forceSharingProvider;

		// Token: 0x040053FF RID: 21503
		private bool fallbackEnabled;

		// Token: 0x04005400 RID: 21504
		private IFrontEndLocator frontEndLocator;
	}
}
