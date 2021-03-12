using System;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000750 RID: 1872
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class MissingRightsManagementLicenseException : RightsManagementPermanentException, ISerializable
	{
		// Token: 0x0600483B RID: 18491 RVA: 0x00130BB6 File Offset: 0x0012EDB6
		public MissingRightsManagementLicenseException(StoreObjectId messageStoreId, bool messageInArchive, string mailboxOwnerLegacyDN, string publishLicense) : this(messageStoreId, messageInArchive, mailboxOwnerLegacyDN, publishLicense, null)
		{
		}

		// Token: 0x0600483C RID: 18492 RVA: 0x00130BC4 File Offset: 0x0012EDC4
		public MissingRightsManagementLicenseException(StoreObjectId messageStoreId, bool messageInArchive, string mailboxOwnerLegacyDN, string publishLicense, LocalizedException innerException) : base(ServerStrings.MissingRightsManagementLicense, innerException)
		{
			this.messageStoreId = messageStoreId;
			this.messageInArchive = messageInArchive;
			this.mailboxOwnerLegacyDN = mailboxOwnerLegacyDN;
			this.publishLicense = publishLicense;
			this.requestCorrelator = MissingRightsManagementLicenseException.GenerateRequestCorrelator();
		}

		// Token: 0x0600483D RID: 18493 RVA: 0x00130BFC File Offset: 0x0012EDFC
		protected MissingRightsManagementLicenseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.messageStoreId = (StoreObjectId)info.GetValue("MessageStoreId", typeof(StoreObjectId));
			this.publishLicense = info.GetString("PublishLicense");
			this.requestCorrelator = info.GetString("RequestCorrelator");
			this.messageInArchive = info.GetBoolean("IsMessageInArchive");
			this.mailboxOwnerLegacyDN = info.GetString("MailboxOwnerLegacyDN");
		}

		// Token: 0x170014E3 RID: 5347
		// (get) Token: 0x0600483E RID: 18494 RVA: 0x00130C75 File Offset: 0x0012EE75
		public string PublishLicense
		{
			get
			{
				return this.publishLicense;
			}
		}

		// Token: 0x170014E4 RID: 5348
		// (get) Token: 0x0600483F RID: 18495 RVA: 0x00130C7D File Offset: 0x0012EE7D
		public StoreObjectId MessageStoreId
		{
			get
			{
				return this.messageStoreId;
			}
		}

		// Token: 0x170014E5 RID: 5349
		// (get) Token: 0x06004840 RID: 18496 RVA: 0x00130C85 File Offset: 0x0012EE85
		public string RequestCorrelator
		{
			get
			{
				return this.requestCorrelator;
			}
		}

		// Token: 0x170014E6 RID: 5350
		// (get) Token: 0x06004841 RID: 18497 RVA: 0x00130C8D File Offset: 0x0012EE8D
		public override RightsManagementFailureCode FailureCode
		{
			get
			{
				return RightsManagementFailureCode.MissingLicense;
			}
		}

		// Token: 0x170014E7 RID: 5351
		// (get) Token: 0x06004842 RID: 18498 RVA: 0x00130C94 File Offset: 0x0012EE94
		public bool MessageInArchive
		{
			get
			{
				return this.messageInArchive;
			}
		}

		// Token: 0x170014E8 RID: 5352
		// (get) Token: 0x06004843 RID: 18499 RVA: 0x00130C9C File Offset: 0x0012EE9C
		public string MailboxOwnerLegacyDN
		{
			get
			{
				return this.mailboxOwnerLegacyDN;
			}
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x00130CA4 File Offset: 0x0012EEA4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("MessageStoreId", this.messageStoreId, typeof(StoreObjectId));
			info.AddValue("PublishLicense", this.publishLicense);
			info.AddValue("RequestCorrelator", this.requestCorrelator);
			info.AddValue("IsMessageInArchive", this.messageInArchive);
			info.AddValue("MailboxOwnerLegacyDN", this.mailboxOwnerLegacyDN);
			base.GetObjectData(info, context);
		}

		// Token: 0x06004845 RID: 18501 RVA: 0x00130D18 File Offset: 0x0012EF18
		private static string GenerateRequestCorrelator()
		{
			return Guid.NewGuid().GetHashCode().ToString("X8", CultureInfo.InvariantCulture);
		}

		// Token: 0x04002742 RID: 10050
		private const string PublishLicenseSerializationLabel = "PublishLicense";

		// Token: 0x04002743 RID: 10051
		private const string MessageStoreIdSerializationLabel = "MessageStoreId";

		// Token: 0x04002744 RID: 10052
		private const string RequestCorrelatorSerializationLabel = "RequestCorrelator";

		// Token: 0x04002745 RID: 10053
		private const string IsMessageInArchiveSerializationLabel = "IsMessageInArchive";

		// Token: 0x04002746 RID: 10054
		private const string MailboxOwnerLegacyDNSerializationLabel = "MailboxOwnerLegacyDN";

		// Token: 0x04002747 RID: 10055
		private string publishLicense;

		// Token: 0x04002748 RID: 10056
		private StoreObjectId messageStoreId;

		// Token: 0x04002749 RID: 10057
		private bool messageInArchive;

		// Token: 0x0400274A RID: 10058
		private string mailboxOwnerLegacyDN;

		// Token: 0x0400274B RID: 10059
		private string requestCorrelator;
	}
}
