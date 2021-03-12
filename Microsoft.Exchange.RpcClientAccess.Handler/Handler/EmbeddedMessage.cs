using System;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000059 RID: 89
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class EmbeddedMessage : Message
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x0001C61F File Offset: 0x0001A81F
		internal EmbeddedMessage(CoreItem parentCoreItem, Logon logon, Encoding string8Encoding) : base(parentCoreItem, logon, string8Encoding)
		{
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001C62C File Offset: 0x0001A82C
		protected override StoreId GetMessageIdAfterSave()
		{
			return default(StoreId);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0001C644 File Offset: 0x0001A844
		protected override MessageAdaptor CreateDownloadMessageAdaptor(DownloadBodyOption downloadBodyOption, FastTransferSendOption sendOptions, bool isFastTransferCopyProperties)
		{
			base.PropertyBag.Load(CoreObjectSchema.AllPropertiesOnStore);
			return new MessageAdaptor(base.ReferenceCoreItem, new MessageAdaptor.Options
			{
				IsReadOnly = true,
				IsEmbedded = true,
				DownloadBodyOption = downloadBodyOption,
				IsUpload = sendOptions.IsUpload(),
				IsFastTransferCopyProperties = isFastTransferCopyProperties
			}, base.LogonObject.LogonString8Encoding, sendOptions.WantUnicode(), null);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0001C6B4 File Offset: 0x0001A8B4
		protected override MessageAdaptor CreateUploadMessageAdaptor()
		{
			return new MessageAdaptor(base.ReferenceCoreItem, new MessageAdaptor.Options
			{
				IsReadOnly = false,
				IsEmbedded = true,
				DownloadBodyOption = DownloadBodyOption.AllBodyProperties
			}, base.LogonObject.LogonString8Encoding, true, base.LogonObject);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001C701 File Offset: 0x0001A901
		protected override bool SaveChangesToLinkedDocumentLibraryIfNecessary()
		{
			return false;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001C704 File Offset: 0x0001A904
		public override StoreId SaveChanges(SaveChangesMode saveChangesMode)
		{
			return base.SaveChanges(saveChangesMode);
		}
	}
}
