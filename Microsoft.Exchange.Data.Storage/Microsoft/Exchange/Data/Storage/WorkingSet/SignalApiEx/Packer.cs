using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkingSet.SignalApi;

namespace Microsoft.Exchange.Data.Storage.WorkingSet.SignalApiEx
{
	// Token: 0x02000EE4 RID: 3812
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class Packer
	{
		// Token: 0x06008390 RID: 33680 RVA: 0x0023BA04 File Offset: 0x00239C04
		public static void Pack(Signal signal, MessageItem mailMessage)
		{
			List<Item> list = new List<Item>();
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream, Encoding.UTF8))
				{
					binaryWriter.Write(1);
					list = signal.WriteObject(binaryWriter);
					using (TextWriter textWriter = mailMessage.Body.OpenTextWriter(BodyFormat.TextPlain))
					{
						textWriter.Write(Convert.ToBase64String(memoryStream.ToArray()));
					}
				}
			}
			foreach (Item item in list)
			{
				ExchangeItem exchangeItem = item as ExchangeItem;
				if (exchangeItem != null)
				{
					if (exchangeItem.Item.IsNew)
					{
						throw new ArgumentException("Item must be saved in order to attach.");
					}
					using (ItemAttachment itemAttachment = mailMessage.AttachmentCollection.AddExistingItem(exchangeItem.Item))
					{
						itemAttachment.ContentId = exchangeItem.AttachContentId.ToString();
						itemAttachment.Save();
					}
				}
			}
		}
	}
}
