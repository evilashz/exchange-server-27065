using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007B8 RID: 1976
	internal sealed class InvalidItemForOperationException : ServicePermanentException
	{
		// Token: 0x06003AC1 RID: 15041 RVA: 0x000CF4FC File Offset: 0x000CD6FC
		static InvalidItemForOperationException()
		{
			InvalidItemForOperationException.errorMappings.Add("CreateItemAttachment", (CoreResources.IDs)4225005690U);
			InvalidItemForOperationException.errorMappings.Add("CreateItem", CoreResources.IDs.ErrorInvalidItemForOperationCreateItem);
			InvalidItemForOperationException.errorMappings.Add("ExpandDL", (CoreResources.IDs)2181052460U);
			InvalidItemForOperationException.errorMappings.Add("SendItem", (CoreResources.IDs)4123291671U);
			InvalidItemForOperationException.errorMappings.Add(typeof(AcceptItemType).Name, CoreResources.IDs.ErrorInvalidItemForOperationAcceptItem);
			InvalidItemForOperationException.errorMappings.Add(typeof(DeclineItemType).Name, CoreResources.IDs.ErrorInvalidItemForOperationDeclineItem);
			InvalidItemForOperationException.errorMappings.Add(typeof(TentativelyAcceptItemType).Name, CoreResources.IDs.ErrorInvalidItemForOperationTentative);
			InvalidItemForOperationException.errorMappings.Add(typeof(CancelCalendarItemType).Name, CoreResources.IDs.ErrorInvalidItemForOperationCancelItem);
			InvalidItemForOperationException.errorMappings.Add(typeof(RemoveItemType).Name, CoreResources.IDs.ErrorInvalidItemForOperationRemoveItem);
			InvalidItemForOperationException.errorMappings.Add(typeof(ArchiveItem).Name, CoreResources.IDs.ErrorInvalidItemForOperationArchiveItem);
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x000CF649 File Offset: 0x000CD849
		public InvalidItemForOperationException(string operation) : base(InvalidItemForOperationException.errorMappings[operation])
		{
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06003AC3 RID: 15043 RVA: 0x000CF65C File Offset: 0x000CD85C
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}

		// Token: 0x040020AA RID: 8362
		public const string CreateItemAttachmentOperation = "CreateItemAttachment";

		// Token: 0x040020AB RID: 8363
		public const string CreateItemOperation = "CreateItem";

		// Token: 0x040020AC RID: 8364
		public const string ExpandDLOperation = "ExpandDL";

		// Token: 0x040020AD RID: 8365
		public const string SendItemOperation = "SendItem";

		// Token: 0x040020AE RID: 8366
		private static Dictionary<string, Enum> errorMappings = new Dictionary<string, Enum>();
	}
}
