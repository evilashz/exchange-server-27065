using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Filtering;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000230 RID: 560
	internal class ClientScanResultStorageProvider : ScanResultStorageProvider
	{
		// Token: 0x0600155E RID: 5470 RVA: 0x0004C06B File Offset: 0x0004A26B
		public ClientScanResultStorageProvider(string clientData, Item storeItem) : base(storeItem)
		{
			this.clientScanResultStorage = ClientScanResultStorage.CreateInstance(clientData);
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x0004C080 File Offset: 0x0004A280
		public override IEnumerable<DiscoveredDataClassification> GetDlpDetectedClassificationObjects()
		{
			return this.clientScanResultStorage.DlpDetectedClassificationObjects;
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0004C08D File Offset: 0x0004A28D
		public override void SetDlpDetectedClassificationObjects(IEnumerable<DiscoveredDataClassification> dlpDetectedClassificationObjects)
		{
			if (dlpDetectedClassificationObjects == null)
			{
				this.ResetDlpDetectedClassificationObjects();
				return;
			}
			this.clientScanResultStorage.DlpDetectedClassificationObjects = dlpDetectedClassificationObjects.ToList<DiscoveredDataClassification>();
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x0004C0AA File Offset: 0x0004A2AA
		public override void ResetDlpDetectedClassificationObjects()
		{
			this.clientScanResultStorage.DlpDetectedClassificationObjects = new List<DiscoveredDataClassification>();
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0004C0BC File Offset: 0x0004A2BC
		public override void SetDlpDetectedClassifications(string dcIds)
		{
			if (dcIds == null)
			{
				this.ResetDlpDetectedClassifications();
				return;
			}
			this.clientScanResultStorage.DetectedClassificationIds = dcIds;
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0004C0D4 File Offset: 0x0004A2D4
		public override void ResetDlpDetectedClassifications()
		{
			this.clientScanResultStorage.DetectedClassificationIds = string.Empty;
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0004C0E8 File Offset: 0x0004A2E8
		public override void SetHasDlpDetectedClassifications()
		{
			this.ResetClassifiedParts();
			this.clientScanResultStorage.ClassifiedParts.Add(ScanResultStorageProvider.MessageBodyName);
			AttachmentCollection attachmentCollection = base.StoreItem.AttachmentCollection;
			if (attachmentCollection != null)
			{
				foreach (AttachmentHandle handle in attachmentCollection)
				{
					using (Attachment attachment = base.StoreItem.AttachmentCollection.Open(handle))
					{
						if (attachment != null && !ScanResultStorageProvider.IsExcludedFromDlp(attachment))
						{
							this.clientScanResultStorage.ClassifiedParts.Add(string.Format(ScanResultStorageProvider.UniqueIdFormat, attachment.FileName, attachment.Id.ToBase64String()));
						}
					}
				}
			}
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0004C1B4 File Offset: 0x0004A3B4
		private void ResetClassifiedParts()
		{
			this.clientScanResultStorage.ClassifiedParts = new List<string>();
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0004C1D4 File Offset: 0x0004A3D4
		public override void ResetHasDlpDetectedClassifications(bool alsoInAttachments = false)
		{
			if (alsoInAttachments)
			{
				this.ResetClassifiedParts();
				return;
			}
			this.clientScanResultStorage.ClassifiedParts.RemoveAll((string o) => o.Equals(ScanResultStorageProvider.MessageBodyName, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0004C21C File Offset: 0x0004A41C
		public override bool NeedsClassificationScan()
		{
			return !this.clientScanResultStorage.ClassifiedParts.Exists((string s) => string.Equals(s, ScanResultStorageProvider.MessageBodyName, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0004C284 File Offset: 0x0004A484
		public override bool NeedsClassificationScan(Attachment attachment)
		{
			return attachment != null && !this.clientScanResultStorage.ClassifiedParts.Exists((string s) => string.Equals(s, string.Format(ScanResultStorageProvider.UniqueIdFormat, attachment.FileName, attachment.Id.ToBase64String()), StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0004C2C7 File Offset: 0x0004A4C7
		public override void SetFipsRecoveryOptions(RecoveryOptions options)
		{
			this.clientScanResultStorage.RecoveryOptions = (int)options;
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0004C2D8 File Offset: 0x0004A4D8
		public override RecoveryOptions GetFipsRecoveryOptions()
		{
			return (RecoveryOptions)this.clientScanResultStorage.RecoveryOptions;
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x0004C2F2 File Offset: 0x0004A4F2
		public string GetScanResultData()
		{
			return this.clientScanResultStorage.ToClientString();
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0004C2FF File Offset: 0x0004A4FF
		public string GetDetectedClassificationIds()
		{
			return this.clientScanResultStorage.DetectedClassificationIds;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0004C30C File Offset: 0x0004A50C
		internal ClientScanResultStorage GetClientScanResultStorageForTesting()
		{
			return this.clientScanResultStorage;
		}

		// Token: 0x04000B82 RID: 2946
		private ClientScanResultStorage clientScanResultStorage;
	}
}
