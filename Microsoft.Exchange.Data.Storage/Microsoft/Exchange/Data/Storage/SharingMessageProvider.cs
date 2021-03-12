using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DF9 RID: 3577
	[Serializable]
	public sealed class SharingMessageProvider
	{
		// Token: 0x170020DE RID: 8414
		// (get) Token: 0x06007AE2 RID: 31458 RVA: 0x0021F57C File Offset: 0x0021D77C
		// (set) Token: 0x06007AE3 RID: 31459 RVA: 0x0021F584 File Offset: 0x0021D784
		[XmlAttribute]
		public string Type { get; set; }

		// Token: 0x170020DF RID: 8415
		// (get) Token: 0x06007AE4 RID: 31460 RVA: 0x0021F58D File Offset: 0x0021D78D
		// (set) Token: 0x06007AE5 RID: 31461 RVA: 0x0021F595 File Offset: 0x0021D795
		[XmlAttribute]
		public string TargetRecipients { get; set; }

		// Token: 0x170020E0 RID: 8416
		// (get) Token: 0x06007AE6 RID: 31462 RVA: 0x0021F59E File Offset: 0x0021D79E
		// (set) Token: 0x06007AE7 RID: 31463 RVA: 0x0021F5A6 File Offset: 0x0021D7A6
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/sharing/2008", IsNullable = false)]
		public string FolderId { get; set; }

		// Token: 0x170020E1 RID: 8417
		// (get) Token: 0x06007AE8 RID: 31464 RVA: 0x0021F5AF File Offset: 0x0021D7AF
		// (set) Token: 0x06007AE9 RID: 31465 RVA: 0x0021F5B7 File Offset: 0x0021D7B7
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/sharing/2008", IsNullable = false)]
		public string MailboxId { get; set; }

		// Token: 0x170020E2 RID: 8418
		// (get) Token: 0x06007AEA RID: 31466 RVA: 0x0021F5C0 File Offset: 0x0021D7C0
		// (set) Token: 0x06007AEB RID: 31467 RVA: 0x0021F5C8 File Offset: 0x0021D7C8
		[XmlArrayItem(ElementName = "EncryptedSharedFolderData", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArray(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", IsNullable = false)]
		public EncryptedSharedFolderData[] EncryptedSharedFolderDataCollection { get; set; }

		// Token: 0x170020E3 RID: 8419
		// (get) Token: 0x06007AEC RID: 31468 RVA: 0x0021F5D1 File Offset: 0x0021D7D1
		// (set) Token: 0x06007AED RID: 31469 RVA: 0x0021F5D9 File Offset: 0x0021D7D9
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/sharing/2008", IsNullable = false)]
		public string BrowseUrl { get; set; }

		// Token: 0x170020E4 RID: 8420
		// (get) Token: 0x06007AEE RID: 31470 RVA: 0x0021F5E2 File Offset: 0x0021D7E2
		// (set) Token: 0x06007AEF RID: 31471 RVA: 0x0021F5EA File Offset: 0x0021D7EA
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/sharing/2008", IsNullable = false)]
		public string ICalUrl { get; set; }

		// Token: 0x170020E5 RID: 8421
		// (get) Token: 0x06007AF0 RID: 31472 RVA: 0x0021F5F3 File Offset: 0x0021D7F3
		internal bool IsExchangeInternalProvider
		{
			get
			{
				return this.Type == "ms-exchange-internal";
			}
		}

		// Token: 0x170020E6 RID: 8422
		// (get) Token: 0x06007AF1 RID: 31473 RVA: 0x0021F605 File Offset: 0x0021D805
		internal bool IsExchangeExternalProvider
		{
			get
			{
				return this.Type == "ms-exchange-external";
			}
		}

		// Token: 0x170020E7 RID: 8423
		// (get) Token: 0x06007AF2 RID: 31474 RVA: 0x0021F617 File Offset: 0x0021D817
		internal bool IsExchangePubCalProvider
		{
			get
			{
				return this.Type == "ms-exchange-publish";
			}
		}

		// Token: 0x06007AF3 RID: 31475 RVA: 0x0021F62C File Offset: 0x0021D82C
		internal ValidationResults Validate(SharingMessageKind sharingMessageKind)
		{
			string type;
			if ((type = this.Type) != null)
			{
				if (!(type == "ms-exchange-internal") && !(type == "ms-exchange-external"))
				{
					if (type == "ms-exchange-publish")
					{
						if (sharingMessageKind == SharingMessageKind.Invitation)
						{
							return this.ValidateAsPublishing();
						}
						return new ValidationResults(ValidationResult.Failure, "Unexpected sharing message kind: " + sharingMessageKind.ToString());
					}
				}
				else
				{
					switch (sharingMessageKind)
					{
					case SharingMessageKind.Invitation:
					case SharingMessageKind.AcceptOfRequest:
						return this.ValidateAsInvitationOrAcceptOfRequest();
					case SharingMessageKind.Request:
					case SharingMessageKind.DenyOfRequest:
						return this.ValidateAsRequestOrDenyOfRequest();
					default:
						return new ValidationResults(ValidationResult.Unknown, "unknown sharing message kind: " + sharingMessageKind.ToString());
					}
				}
			}
			return new ValidationResults(ValidationResult.Unknown, "unknown provider type: " + this.Type);
		}

		// Token: 0x06007AF4 RID: 31476 RVA: 0x0021F6F2 File Offset: 0x0021D8F2
		private ValidationResults ValidateAsRequestOrDenyOfRequest()
		{
			if (this.FolderId == null && this.MailboxId == null && this.EncryptedSharedFolderDataCollection == null)
			{
				return ValidationResults.Success;
			}
			return new ValidationResults(ValidationResult.Failure, "FolderId, MailboxId or EncryptedSharedFolderDataCollection not expected");
		}

		// Token: 0x06007AF5 RID: 31477 RVA: 0x0021F720 File Offset: 0x0021D920
		private ValidationResults ValidateAsInvitationOrAcceptOfRequest()
		{
			if (string.IsNullOrEmpty(this.FolderId))
			{
				return new ValidationResults(ValidationResult.Failure, "FolderId is expected");
			}
			if (this.MailboxId == null && this.EncryptedSharedFolderDataCollection == null)
			{
				return new ValidationResults(ValidationResult.Failure, "EncryptedSharedFolderDataCollection or MailboxId expected");
			}
			if (this.MailboxId != null && this.EncryptedSharedFolderDataCollection != null)
			{
				return new ValidationResults(ValidationResult.Failure, "EncryptedSharedFolderDataCollection and MailboxId are not allowed together");
			}
			return ValidationResults.Success;
		}

		// Token: 0x06007AF6 RID: 31478 RVA: 0x0021F784 File Offset: 0x0021D984
		private ValidationResults ValidateAsPublishing()
		{
			if (string.IsNullOrEmpty(this.BrowseUrl))
			{
				return new ValidationResults(ValidationResult.Failure, "BrowseUrl is expected");
			}
			if (this.FolderId != null || this.MailboxId != null || this.EncryptedSharedFolderDataCollection != null)
			{
				return new ValidationResults(ValidationResult.Failure, "FolderId, MailboxId or EncryptedSharedFolderDataCollection not expected");
			}
			return ValidationResults.Success;
		}

		// Token: 0x040054A1 RID: 21665
		internal const string ExchangeInternal = "ms-exchange-internal";

		// Token: 0x040054A2 RID: 21666
		internal const string ExchangeExternal = "ms-exchange-external";

		// Token: 0x040054A3 RID: 21667
		internal const string ExchangePubCal = "ms-exchange-publish";

		// Token: 0x040054A4 RID: 21668
		internal const string ExchangeConsumer = "ms-exchange-consumer";
	}
}
