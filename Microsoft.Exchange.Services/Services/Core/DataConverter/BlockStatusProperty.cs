using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000153 RID: 339
	internal sealed class BlockStatusProperty : ComplexPropertyBase, IToServiceObjectCommand, ISetUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x0600094B RID: 2379 RVA: 0x0002D7DC File Offset: 0x0002B9DC
		public BlockStatusProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0002D7E5 File Offset: 0x0002B9E5
		public static BlockStatusProperty CreateCommand(CommandContext commandContext)
		{
			return new BlockStatusProperty(commandContext);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0002D7ED File Offset: 0x0002B9ED
		public void ToXml()
		{
			throw new InvalidOperationException("BlockStatusProperty.ToXml should not be called.");
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0002D7FC File Offset: 0x0002B9FC
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			Item item = commandSettings.StoreObject as Item;
			ItemResponseShape itemResponseShape = commandSettings.ResponseShape as ItemResponseShape;
			serviceObject.PropertyBag[propertyInformation] = Util.GetItemBlockStatus(item, itemResponseShape.BlockExternalImages, itemResponseShape.BlockExternalImagesIfSenderUntrusted);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0002D864 File Offset: 0x0002BA64
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			Item item = (Item)updateCommandSettings.StoreObject;
			bool valueOrDefault = setPropertyUpdate.ServiceObject.GetValueOrDefault<bool>(this.commandContext.PropertyInformation);
			if (item != null)
			{
				item[ItemSchema.BlockStatus] = (valueOrDefault ? BlockStatus.DontKnow : BlockStatus.NoNeverAgain);
			}
		}
	}
}
