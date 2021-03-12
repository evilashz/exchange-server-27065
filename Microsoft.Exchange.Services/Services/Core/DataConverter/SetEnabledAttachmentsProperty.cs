using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000152 RID: 338
	internal sealed class SetEnabledAttachmentsProperty : AttachmentsProperty, ISetCommand, ISetUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x06000945 RID: 2373 RVA: 0x0002D63C File Offset: 0x0002B83C
		public SetEnabledAttachmentsProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0002D645 File Offset: 0x0002B845
		public new static SetEnabledAttachmentsProperty CreateCommand(CommandContext commandContext)
		{
			return new SetEnabledAttachmentsProperty(commandContext);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0002D650 File Offset: 0x0002B850
		public override void ToServiceObject()
		{
			bool createItemWithAttachments = EWSSettings.CreateItemWithAttachments;
			base.InternalToServiceObject(createItemWithAttachments);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0002D66C File Offset: 0x0002B86C
		public void Set()
		{
			EWSSettings.CreateItemWithAttachments = true;
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			this.SetAttachments(commandSettings.ServiceObject, commandSettings.StoreObject, false);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0002D699 File Offset: 0x0002B899
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			this.SetAttachments(setPropertyUpdate.ServiceObject, updateCommandSettings.StoreObject, true);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0002D6B0 File Offset: 0x0002B8B0
		private void SetAttachments(ServiceObject serviceObject, StoreObject storeObject, bool isUpdating)
		{
			AttachmentType[] array = serviceObject[this.commandContext.PropertyInformation] as AttachmentType[];
			if (array.Length == 0)
			{
				return;
			}
			if (EWSSettings.AttachmentNestLevel > Global.MaxAttachmentNestLevel)
			{
				throw new AttachmentNestLevelLimitExceededException();
			}
			EWSSettings.AttachmentNestLevel++;
			Item item = (Item)storeObject;
			if (isUpdating && item.ClassName.Contains(".SMIME") && array.Length > 0 && array[0] is FileAttachmentType && array[0].Name == "smime.p7m" && (array[0].ContentType == "application/x-pkcs7-mime" || array[0].ContentType == "application/pkcs7-mime" || array[0].ContentType == "application/x-pkcs7-signature" || array[0].ContentType == "application/pkcs7-signature"))
			{
				item.AttachmentCollection.RemoveAll();
			}
			using (AttachmentBuilder attachmentBuilder = new AttachmentBuilder(new AttachmentHierarchy(item), array, this.commandContext.IdConverter))
			{
				attachmentBuilder.CreateAllAttachments();
			}
			EWSSettings.AttachmentNestLevel--;
		}
	}
}
