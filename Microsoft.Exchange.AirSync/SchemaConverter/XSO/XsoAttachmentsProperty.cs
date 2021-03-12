using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.SchemaConverter;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x020001FF RID: 511
	[Serializable]
	internal class XsoAttachmentsProperty : XsoProperty, IAttachmentsProperty, IMultivaluedProperty<AttachmentData>, IProperty, IEnumerable<AttachmentData>, IEnumerable
	{
		// Token: 0x060013ED RID: 5101 RVA: 0x00072CE8 File Offset: 0x00070EE8
		public XsoAttachmentsProperty(IdMapping idmapping) : base(null, PropertyType.ReadOnly)
		{
			this.idmapping = idmapping;
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x00072CF9 File Offset: 0x00070EF9
		public int Count
		{
			get
			{
				if (this.IsItemDelegated())
				{
					return 0;
				}
				return ((Item)base.XsoItem).AttachmentCollection.Count;
			}
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x00073204 File Offset: 0x00071404
		public IEnumerator<AttachmentData> GetEnumerator()
		{
			Item message = base.XsoItem as Item;
			if (message == null)
			{
				throw new UnexpectedTypeException("Item", base.XsoItem);
			}
			string baseId = null;
			if (this.idmapping != null)
			{
				baseId = this.idmapping[MailboxSyncItemId.CreateForNewItem(message.Id.ObjectId)];
			}
			if (baseId == null)
			{
				baseId = message.Id.ObjectId.ToBase64String();
			}
			if (base.XsoItem is MessageItem && ((MessageItem)message).IsRestricted)
			{
				object prop = message.TryGetProperty(MessageItemSchema.DRMLicense);
				if (prop is byte[][])
				{
					byte[][] license = (byte[][])prop;
					if (license.Length > 0)
					{
						yield return new AttachmentData
						{
							AttMethod = 1,
							AttSize = (long)license[0].Length,
							DisplayName = "message.rpmsg.license",
							AttName = HttpUtility.UrlEncode(baseId + ":DRMLicense")
						};
					}
					else
					{
						AirSyncDiagnostics.TraceDebug<int>(ExTraceGlobals.XsoTracer, this, "The license property on the DRM message is incorrect. Length = {0}", license.Length);
					}
				}
				else
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.XsoTracer, this, "Failed to fetch the license property on the DRM message. Prop = {0}", new object[]
					{
						prop
					});
				}
			}
			int index = -1;
			foreach (AttachmentHandle handle in message.AttachmentCollection)
			{
				index++;
				AttachmentData attachmentData = default(AttachmentData);
				Attachment attachment = null;
				try
				{
					attachment = message.AttachmentCollection.Open(handle);
					if (BodyUtility.IsClearSigned(message) && (string.Compare(attachment.FileName, "smime.p7m", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(attachment.ContentType, "multipart/signed", StringComparison.OrdinalIgnoreCase) == 0))
					{
						continue;
					}
					attachmentData.AttName = HttpUtility.UrlEncode(baseId + ":" + index);
					if (string.IsNullOrEmpty(attachmentData.DisplayName))
					{
						if (!string.IsNullOrEmpty(attachment.FileName))
						{
							attachmentData.DisplayName = attachment.FileName;
						}
						else if (attachment.AttachmentType == AttachmentType.EmbeddedMessage)
						{
							using (Item itemAsReadOnly = ((ItemAttachment)attachment).GetItemAsReadOnly(null))
							{
								attachmentData.DisplayName = (itemAsReadOnly.TryGetProperty(ItemSchema.Subject) as string);
							}
						}
					}
					if (string.IsNullOrEmpty(attachmentData.DisplayName))
					{
						attachmentData.DisplayName = "????";
					}
					attachmentData.AttMethod = (int)attachment.TryGetProperty(AttachmentSchema.AttachMethod);
					attachmentData.AttSize = attachment.Size;
				}
				finally
				{
					if (attachment != null)
					{
						attachment.Dispose();
						attachment = null;
					}
				}
				yield return attachmentData;
			}
			yield break;
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00073220 File Offset: 0x00071420
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000C4B RID: 3147
		[NonSerialized]
		private IdMapping idmapping;
	}
}
