using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.UnifiedContent.Exchange;
using Microsoft.Filtering;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200022D RID: 557
	internal abstract class ScanResultStorageProvider : IExtendedMapiFilteringContext, IMapiFilteringContext
	{
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x0004BA50 File Offset: 0x00049C50
		// (set) Token: 0x06001533 RID: 5427 RVA: 0x0004BA58 File Offset: 0x00049C58
		protected Item StoreItem { get; set; }

		// Token: 0x06001534 RID: 5428 RVA: 0x0004BA61 File Offset: 0x00049C61
		protected ScanResultStorageProvider(Item storeItem)
		{
			if (storeItem == null)
			{
				throw new ArgumentNullException("storeItem");
			}
			this.StoreItem = storeItem;
		}

		// Token: 0x06001535 RID: 5429
		public abstract IEnumerable<DiscoveredDataClassification> GetDlpDetectedClassificationObjects();

		// Token: 0x06001536 RID: 5430
		public abstract void SetDlpDetectedClassificationObjects(IEnumerable<DiscoveredDataClassification> dlpDetectedClassificationObjects);

		// Token: 0x06001537 RID: 5431
		public abstract void ResetDlpDetectedClassificationObjects();

		// Token: 0x06001538 RID: 5432
		public abstract void SetDlpDetectedClassifications(string dcIds);

		// Token: 0x06001539 RID: 5433
		public abstract void ResetDlpDetectedClassifications();

		// Token: 0x0600153A RID: 5434
		public abstract void SetHasDlpDetectedClassifications();

		// Token: 0x0600153B RID: 5435
		public abstract void ResetHasDlpDetectedClassifications(bool alsoInAttachments = false);

		// Token: 0x0600153C RID: 5436
		public abstract bool NeedsClassificationScan();

		// Token: 0x0600153D RID: 5437
		public abstract bool NeedsClassificationScan(Attachment attachment);

		// Token: 0x0600153E RID: 5438
		public abstract void SetFipsRecoveryOptions(RecoveryOptions options);

		// Token: 0x0600153F RID: 5439
		public abstract RecoveryOptions GetFipsRecoveryOptions();

		// Token: 0x06001540 RID: 5440 RVA: 0x0004BA80 File Offset: 0x00049C80
		public virtual bool NeedsClassificationForBodyOrAnyAttachments()
		{
			if (this.NeedsClassificationScan())
			{
				return true;
			}
			AttachmentCollection attachmentCollection = this.StoreItem.AttachmentCollection;
			if (attachmentCollection != null && attachmentCollection.Count > 0)
			{
				foreach (AttachmentHandle handle in attachmentCollection)
				{
					using (Attachment attachment = this.StoreItem.AttachmentCollection.Open(handle))
					{
						if (attachment != null && !ScanResultStorageProvider.IsExcludedFromDlp(attachment) && this.NeedsClassificationScan(attachment))
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0004BB30 File Offset: 0x00049D30
		public virtual void ResetAllClassifications()
		{
			this.ResetHasDlpDetectedClassifications(true);
			this.ResetDlpDetectedClassifications();
			this.ResetDlpDetectedClassificationObjects();
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0004BB48 File Offset: 0x00049D48
		public virtual void RefreshBodyClassifications()
		{
			this.ResetHasDlpDetectedClassifications(false);
			List<string> list = new List<string>();
			list.Add(ScanResultStorageProvider.MessageBodyName);
			IEnumerable<DiscoveredDataClassification> dlpDetectedClassificationObjects = FipsResultParser.DeleteClassifications(this.GetDlpDetectedClassificationObjects(), list, false);
			this.SetDlpDetectedClassificationObjects(dlpDetectedClassificationObjects);
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0004BB84 File Offset: 0x00049D84
		public virtual void RefreshAttachmentClassifications()
		{
			List<string> list = new List<string>();
			AttachmentCollection attachmentCollection = this.StoreItem.AttachmentCollection;
			if (attachmentCollection != null && attachmentCollection.Count > 0)
			{
				foreach (AttachmentHandle handle in attachmentCollection)
				{
					using (Attachment attachment = this.StoreItem.AttachmentCollection.Open(handle))
					{
						if (!ScanResultStorageProvider.IsExcludedFromDlp(attachment))
						{
							list.Add(string.Format(ScanResultStorageProvider.UniqueIdFormat, attachment.FileName, attachment.Id.ToBase64String()));
						}
					}
				}
			}
			list.Add(ScanResultStorageProvider.MessageBodyName);
			IEnumerable<DiscoveredDataClassification> dlpDetectedClassificationObjects = FipsResultParser.DeleteClassifications(this.GetDlpDetectedClassificationObjects(), list, true);
			this.SetDlpDetectedClassificationObjects(dlpDetectedClassificationObjects);
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0004BC60 File Offset: 0x00049E60
		public static bool IsExcludedFromDlp(Attachment attachment)
		{
			return attachment == null || string.IsNullOrEmpty(attachment.FileName);
		}

		// Token: 0x04000B79 RID: 2937
		public const char UniqueIdFormatSeparatorChar = ':';

		// Token: 0x04000B7A RID: 2938
		public const string UniqueIdFormatSeparator = ":";

		// Token: 0x04000B7B RID: 2939
		public static readonly string UniqueIdFormat = string.Join<string>(":", new string[]
		{
			"{0}",
			"{1}"
		});

		// Token: 0x04000B7C RID: 2940
		public static readonly string MessageBodyName = "Message Body";
	}
}
