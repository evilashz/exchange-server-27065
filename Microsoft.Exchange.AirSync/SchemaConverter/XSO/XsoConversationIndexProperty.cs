using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000212 RID: 530
	internal class XsoConversationIndexProperty : XsoProperty, IByteArrayProperty, IProperty
	{
		// Token: 0x0600145E RID: 5214 RVA: 0x00075DF8 File Offset: 0x00073FF8
		public XsoConversationIndexProperty(PropertyType type) : base(ItemSchema.ConversationIndex, type)
		{
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x00075E08 File Offset: 0x00074008
		public byte[] ByteArrayData
		{
			get
			{
				byte[] array = (byte[])base.XsoItem.TryGetProperty(ItemSchema.ConversationIndex);
				if (array == null)
				{
					return null;
				}
				BufferBuilder bufferBuilder = null;
				ConversationIndex index;
				if (ConversationIndex.TryCreate(array, out index) && index != ConversationIndex.Empty && index.Components != null && index.Components.Count > 0)
				{
					bufferBuilder = new BufferBuilder(index.Components.Count * index.Components[0].Length);
					for (int i = 0; i < index.Components.Count; i++)
					{
						bufferBuilder.Append(index.Components[i]);
					}
				}
				if (bufferBuilder == null)
				{
					return null;
				}
				return bufferBuilder.GetBuffer();
			}
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x00075EB8 File Offset: 0x000740B8
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
		}
	}
}
