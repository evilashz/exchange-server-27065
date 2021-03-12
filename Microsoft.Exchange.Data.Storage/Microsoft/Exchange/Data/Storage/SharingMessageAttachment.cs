using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DD2 RID: 3538
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class SharingMessageAttachment
	{
		// Token: 0x06007977 RID: 31095 RVA: 0x00219080 File Offset: 0x00217280
		internal static void SetSharingMessage(MessageItem item, SharingMessage sharingMessage)
		{
			using (StreamAttachment orCreateSharingMessageAttachment = SharingMessageAttachment.GetOrCreateSharingMessageAttachment(item))
			{
				orCreateSharingMessageAttachment.PropertyBag[AttachmentSchema.AttachMimeTag] = "application/x-sharing-metadata-xml";
				orCreateSharingMessageAttachment.PropertyBag[AttachmentSchema.AttachLongFileName] = "sharing_metadata.xml";
				using (Stream contentStream = orCreateSharingMessageAttachment.GetContentStream(PropertyOpenMode.Create))
				{
					sharingMessage.SerializeToStream(contentStream);
					contentStream.Flush();
				}
				orCreateSharingMessageAttachment.Save();
			}
		}

		// Token: 0x06007978 RID: 31096 RVA: 0x00219110 File Offset: 0x00217310
		internal static SharingMessage GetSharingMessage(MessageItem item)
		{
			StreamAttachment sharingMessageAttachment = SharingMessageAttachment.GetSharingMessageAttachment(item);
			if (sharingMessageAttachment != null)
			{
				using (sharingMessageAttachment)
				{
					using (Stream stream = sharingMessageAttachment.TryGetContentStream(PropertyOpenMode.ReadOnly))
					{
						SharingMessage sharingMessage = null;
						try
						{
							sharingMessage = SharingMessage.DeserializeFromStream(stream);
						}
						catch (InvalidOperationException)
						{
						}
						if (sharingMessage != null)
						{
							ValidationResults validationResults = sharingMessage.Validate();
							if (validationResults.Result == ValidationResult.Success)
							{
								return sharingMessage;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06007979 RID: 31097 RVA: 0x002191A0 File Offset: 0x002173A0
		private static StreamAttachment GetSharingMessageAttachment(MessageItem item)
		{
			bool flag = true;
			foreach (AttachmentHandle handle in item.AttachmentCollection)
			{
				Attachment attachment = item.AttachmentCollection.Open(handle, null);
				try
				{
					string valueOrDefault = attachment.GetValueOrDefault<string>(AttachmentSchema.AttachMimeTag, string.Empty);
					string valueOrDefault2 = attachment.GetValueOrDefault<string>(AttachmentSchema.AttachLongFileName, string.Empty);
					if (StringComparer.InvariantCultureIgnoreCase.Equals(valueOrDefault, "application/x-sharing-metadata-xml") && StringComparer.InvariantCultureIgnoreCase.Equals(valueOrDefault2, "sharing_metadata.xml") && attachment is StreamAttachment)
					{
						flag = false;
						return (StreamAttachment)attachment;
					}
				}
				finally
				{
					if (flag)
					{
						attachment.Dispose();
					}
				}
			}
			return null;
		}

		// Token: 0x0600797A RID: 31098 RVA: 0x00219278 File Offset: 0x00217478
		private static StreamAttachment GetOrCreateSharingMessageAttachment(MessageItem item)
		{
			StreamAttachment streamAttachment = SharingMessageAttachment.GetSharingMessageAttachment(item);
			if (streamAttachment == null)
			{
				streamAttachment = (StreamAttachment)item.AttachmentCollection.Create(AttachmentType.Stream);
			}
			return streamAttachment;
		}
	}
}
