using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000211 RID: 529
	internal class XsoConversationIdProperty : XsoProperty, IByteArrayProperty, IProperty
	{
		// Token: 0x0600145B RID: 5211 RVA: 0x00075DB8 File Offset: 0x00073FB8
		public XsoConversationIdProperty(PropertyType type) : base(ItemSchema.ConversationId, type)
		{
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x00075DC8 File Offset: 0x00073FC8
		public byte[] ByteArrayData
		{
			get
			{
				ConversationId conversationId = (ConversationId)base.XsoItem.TryGetProperty(ItemSchema.ConversationId);
				if (conversationId == null)
				{
					return null;
				}
				return conversationId.GetBytes();
			}
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x00075DF6 File Offset: 0x00073FF6
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
		}
	}
}
