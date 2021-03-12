using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000068 RID: 104
	internal sealed class InitialMessageFinder
	{
		// Token: 0x060007FB RID: 2043 RVA: 0x00045DD6 File Offset: 0x00043FD6
		public InitialMessageFinder(int numberOfMessages)
		{
			this.messageNodes = new List<InitialMessageFinder.MessageNode>(numberOfMessages);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00045DEC File Offset: 0x00043FEC
		public void AddMessage(object propertyValue, DateTime deliveryTime, byte[] conversationIndex)
		{
			InitialMessageFinder.MessageNode item = new InitialMessageFinder.MessageNode(propertyValue, deliveryTime, conversationIndex);
			this.messageNodes.Add(item);
			if (this.messageNodes.Count == 1 || this.oldestMessageNode.DeliveryTime > item.DeliveryTime)
			{
				this.oldestMessageNode = item;
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00045E40 File Offset: 0x00044040
		public object GetInitialMessagePropertyValue()
		{
			InitialMessageFinder.MessageNode messageNode = this.oldestMessageNode;
			foreach (InitialMessageFinder.MessageNode messageNode2 in this.messageNodes)
			{
				switch (InitialMessageFinder.Relate(messageNode.ConversationIndex, messageNode2.ConversationIndex))
				{
				case InitialMessageFinder.Relation.Child:
					messageNode = messageNode2;
					break;
				}
			}
			return messageNode.PropertyValue;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00045ECC File Offset: 0x000440CC
		private static InitialMessageFinder.Relation Relate(byte[] leftConversationIndex, byte[] rightConversationIndex)
		{
			if (leftConversationIndex == null || rightConversationIndex == null || leftConversationIndex.Length == rightConversationIndex.Length)
			{
				return InitialMessageFinder.Relation.Unrelated;
			}
			int num = Math.Min(leftConversationIndex.Length, rightConversationIndex.Length);
			for (int i = 0; i < num; i++)
			{
				if (leftConversationIndex[i] != rightConversationIndex[i])
				{
					return InitialMessageFinder.Relation.Unrelated;
				}
			}
			if (leftConversationIndex.Length != num)
			{
				return InitialMessageFinder.Relation.Child;
			}
			return InitialMessageFinder.Relation.Parent;
		}

		// Token: 0x040003F2 RID: 1010
		private readonly List<InitialMessageFinder.MessageNode> messageNodes;

		// Token: 0x040003F3 RID: 1011
		private InitialMessageFinder.MessageNode oldestMessageNode;

		// Token: 0x02000069 RID: 105
		private struct MessageNode
		{
			// Token: 0x060007FF RID: 2047 RVA: 0x00045F14 File Offset: 0x00044114
			public MessageNode(object propertyValue, DateTime deliveryTime, byte[] conversationIndex)
			{
				this.propertyValue = propertyValue;
				this.deliveryTime = deliveryTime;
				this.conversationIndex = conversationIndex;
			}

			// Token: 0x170001C2 RID: 450
			// (get) Token: 0x06000800 RID: 2048 RVA: 0x00045F2B File Offset: 0x0004412B
			public object PropertyValue
			{
				get
				{
					return this.propertyValue;
				}
			}

			// Token: 0x170001C3 RID: 451
			// (get) Token: 0x06000801 RID: 2049 RVA: 0x00045F33 File Offset: 0x00044133
			public DateTime DeliveryTime
			{
				get
				{
					return this.deliveryTime;
				}
			}

			// Token: 0x170001C4 RID: 452
			// (get) Token: 0x06000802 RID: 2050 RVA: 0x00045F3B File Offset: 0x0004413B
			public byte[] ConversationIndex
			{
				get
				{
					return this.conversationIndex;
				}
			}

			// Token: 0x040003F4 RID: 1012
			private object propertyValue;

			// Token: 0x040003F5 RID: 1013
			private DateTime deliveryTime;

			// Token: 0x040003F6 RID: 1014
			private byte[] conversationIndex;
		}

		// Token: 0x0200006A RID: 106
		private enum Relation
		{
			// Token: 0x040003F8 RID: 1016
			Parent = 1,
			// Token: 0x040003F9 RID: 1017
			Child = -1,
			// Token: 0x040003FA RID: 1018
			Unrelated
		}
	}
}
