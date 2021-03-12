using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Filtering;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200022E RID: 558
	internal class StoreItemScanResultStorageProvider : ScanResultStorageProvider
	{
		// Token: 0x06001546 RID: 5446 RVA: 0x0004BCB2 File Offset: 0x00049EB2
		public StoreItemScanResultStorageProvider(Item storeItem) : base(storeItem)
		{
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x0004BCBC File Offset: 0x00049EBC
		public override IEnumerable<DiscoveredDataClassification> GetDlpDetectedClassificationObjects()
		{
			string dlpDetectedClassificationObjectsAsString = this.GetDlpDetectedClassificationObjectsAsString();
			return DiscoveredDataClassification.DeserializeFromXml(dlpDetectedClassificationObjectsAsString);
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0004BCD8 File Offset: 0x00049ED8
		private string GetDlpDetectedClassificationObjectsAsString()
		{
			try
			{
				using (Stream stream = base.StoreItem.OpenPropertyStream(ItemSchema.DlpDetectedClassificationObjects, PropertyOpenMode.ReadOnly))
				{
					UnicodeEncoding encoding = new UnicodeEncoding();
					using (StreamReader streamReader = new StreamReader(stream, encoding))
					{
						return streamReader.ReadToEnd();
					}
				}
			}
			catch (ObjectNotFoundException)
			{
			}
			return null;
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0004BD50 File Offset: 0x00049F50
		public override void SetDlpDetectedClassificationObjects(IEnumerable<DiscoveredDataClassification> dlpDetectedClassificationObjects)
		{
			string text = DiscoveredDataClassification.SerializeToXml(dlpDetectedClassificationObjects);
			if (string.IsNullOrEmpty(text))
			{
				base.StoreItem.SetOrDeleteProperty(ItemSchema.DlpDetectedClassificationObjects, null);
				return;
			}
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
			base.StoreItem[ItemSchema.DlpDetectedClassificationObjects] = unicodeEncoding.GetBytes(text);
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0004BD9B File Offset: 0x00049F9B
		public override void ResetDlpDetectedClassificationObjects()
		{
			base.StoreItem.SetOrDeleteProperty(ItemSchema.DlpDetectedClassificationObjects, null);
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0004BDAE File Offset: 0x00049FAE
		public override void SetDlpDetectedClassifications(string dcIds)
		{
			if (dcIds == null)
			{
				dcIds = string.Empty;
			}
			base.StoreItem[ItemSchema.DlpDetectedClassifications] = dcIds;
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0004BDCB File Offset: 0x00049FCB
		public override void ResetDlpDetectedClassifications()
		{
			base.StoreItem.Delete(ItemSchema.DlpDetectedClassifications);
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0004BDE0 File Offset: 0x00049FE0
		public override void SetHasDlpDetectedClassifications()
		{
			base.StoreItem[ItemSchema.HasDlpDetectedClassifications] = string.Empty;
			AttachmentCollection attachmentCollection = base.StoreItem.AttachmentCollection;
			if (attachmentCollection != null)
			{
				foreach (AttachmentHandle handle in attachmentCollection)
				{
					using (Attachment attachment = base.StoreItem.AttachmentCollection.Open(handle))
					{
						if (attachment != null && !ScanResultStorageProvider.IsExcludedFromDlp(attachment))
						{
							attachment[AttachmentSchema.HasDlpDetectedClassifications] = string.Empty;
							attachment.Save();
						}
					}
				}
			}
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0004BE90 File Offset: 0x0004A090
		public override void ResetHasDlpDetectedClassifications(bool alsoInAttachments = false)
		{
			base.StoreItem.Delete(ItemSchema.HasDlpDetectedClassifications);
			if (alsoInAttachments)
			{
				AttachmentCollection attachmentCollection = base.StoreItem.AttachmentCollection;
				if (attachmentCollection != null)
				{
					foreach (AttachmentHandle handle in attachmentCollection)
					{
						using (Attachment attachment = base.StoreItem.AttachmentCollection.Open(handle))
						{
							StoreItemScanResultStorageProvider.ResetHasDlpDetectedClassifications(attachment);
						}
					}
				}
			}
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0004BF24 File Offset: 0x0004A124
		private static void ResetHasDlpDetectedClassifications(Attachment attachment)
		{
			if (attachment != null && !ScanResultStorageProvider.IsExcludedFromDlp(attachment))
			{
				attachment.Delete(AttachmentSchema.HasDlpDetectedClassifications);
				attachment.Save();
			}
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0004BF44 File Offset: 0x0004A144
		public override bool NeedsClassificationScan()
		{
			string valueOrDefault = base.StoreItem.PropertyBag.GetValueOrDefault<string>(ItemSchema.HasDlpDetectedClassifications, null);
			return valueOrDefault == null;
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0004BF6C File Offset: 0x0004A16C
		public override bool NeedsClassificationScan(Attachment attachment)
		{
			if (attachment == null)
			{
				return false;
			}
			string valueOrDefault = attachment.PropertyBag.GetValueOrDefault<string>(AttachmentSchema.HasDlpDetectedClassifications, null);
			return valueOrDefault == null;
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0004BF94 File Offset: 0x0004A194
		public override void SetFipsRecoveryOptions(RecoveryOptions options)
		{
			base.StoreItem[ItemSchema.RecoveryOptions] = (int)options;
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x0004BFAC File Offset: 0x0004A1AC
		public override RecoveryOptions GetFipsRecoveryOptions()
		{
			return (RecoveryOptions)base.StoreItem.PropertyBag.GetValueOrDefault<int>(ItemSchema.RecoveryOptions, 0);
		}
	}
}
