using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AnchorService.Storage
{
	// Token: 0x0200003A RID: 58
	internal class AnchorXmlSerializableObject<TXmlSerializable> : AnchorPersistableBase where TXmlSerializable : XMLSerializableBase
	{
		// Token: 0x0600027A RID: 634 RVA: 0x0000930E File Offset: 0x0000750E
		public AnchorXmlSerializableObject(AnchorContext context) : base(context)
		{
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00009318 File Offset: 0x00007518
		public override PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return AnchorHelper.AggregateProperties(new IList<PropertyDefinition>[]
				{
					base.PropertyDefinitions,
					new PropertyDefinition[]
					{
						ItemSchema.TextBody,
						StoreObjectSchema.ItemClass
					}
				});
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00009356 File Offset: 0x00007556
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000935E File Offset: 0x0000755E
		public TXmlSerializable PersistedObject { get; set; }

		// Token: 0x0600027E RID: 638 RVA: 0x00009367 File Offset: 0x00007567
		public static string GetItemClass()
		{
			return string.Format("IPM.MS-Exchange.Anchor.{0}", typeof(TXmlSerializable).FullName);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00009382 File Offset: 0x00007582
		public static AnchorRowSelectorResult SelectByItemClassAndStopProcessing(IDictionary<PropertyDefinition, object> row)
		{
			if (!AnchorXmlSerializableObject<TXmlSerializable>.GetItemClass().Equals(row[StoreObjectSchema.ItemClass]))
			{
				return AnchorRowSelectorResult.RejectRowStopProcessing;
			}
			return AnchorRowSelectorResult.AcceptRow;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000939E File Offset: 0x0000759E
		public override IAnchorStoreObject FindStoreObject(IAnchorDataProvider dataProvider, StoreObjectId id, PropertyDefinition[] properties)
		{
			return dataProvider.FindMessage(id, properties);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000093A8 File Offset: 0x000075A8
		public override void WriteToMessageItem(IAnchorStoreObject message, bool loaded)
		{
			base.WriteToMessageItem(message, loaded);
			message[StoreObjectSchema.ItemClass] = AnchorXmlSerializableObject<TXmlSerializable>.GetItemClass();
			PropertyDefinition textBody = ItemSchema.TextBody;
			TXmlSerializable persistedObject = this.PersistedObject;
			message[textBody] = persistedObject.Serialize(false);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000093F0 File Offset: 0x000075F0
		public override bool ReadFromMessageItem(IAnchorStoreObject message)
		{
			if (!base.ReadFromMessageItem(message))
			{
				return false;
			}
			if (!AnchorXmlSerializableObject<TXmlSerializable>.GetItemClass().Equals(message[StoreObjectSchema.ItemClass]))
			{
				return false;
			}
			string text = message[ItemSchema.TextBody] as string;
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			this.PersistedObject = XMLSerializableBase.Deserialize<TXmlSerializable>(text, true);
			return true;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000944A File Offset: 0x0000764A
		protected override IAnchorStoreObject CreateStoreObject(IAnchorDataProvider dataProvider)
		{
			return dataProvider.CreateMessage();
		}
	}
}
