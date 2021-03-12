using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.SchemaConverter;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000203 RID: 515
	[Serializable]
	internal abstract class XsoRecipientProperty : XsoStringProperty
	{
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x00073A13 File Offset: 0x00071C13
		// (set) Token: 0x06001400 RID: 5120 RVA: 0x00073A1B File Offset: 0x00071C1B
		internal RecipientItemType RecipientItemType { get; private set; }

		// Token: 0x06001401 RID: 5121 RVA: 0x00073A24 File Offset: 0x00071C24
		public XsoRecipientProperty(RecipientItemType recipientType, PropertyType type) : base(null, type)
		{
			this.RecipientItemType = recipientType;
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x00073A38 File Offset: 0x00071C38
		public override string StringData
		{
			get
			{
				MessageItem messageItem = base.XsoItem as MessageItem;
				string text = null;
				try
				{
					if (messageItem != null)
					{
						text = EmailAddressConverter.GetRecipientString(messageItem.Recipients, this.RecipientItemType, messageItem.Session.MailboxOwner);
						AirSyncDiagnostics.TraceDebug<RecipientItemType, string>(ExTraceGlobals.XsoTracer, this, "value for Message '{0}':{1}", this.RecipientItemType, text);
					}
				}
				catch (PropertyErrorException ex)
				{
					if (ex.PropertyErrors[0].PropertyErrorCode == PropertyErrorCode.NotEnoughMemory)
					{
						messageItem.Load();
						text = EmailAddressConverter.GetRecipientString(messageItem.Recipients, this.RecipientItemType, messageItem.Session.MailboxOwner);
						AirSyncDiagnostics.TraceDebug<RecipientItemType, string>(ExTraceGlobals.XsoTracer, this, "After calling Load- value for Message '{0}':{1}", this.RecipientItemType, text);
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					base.State = PropertyState.SetToDefault;
				}
				return text;
			}
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x00073AFC File Offset: 0x00071CFC
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			MessageItem messageItem = (MessageItem)base.XsoItem;
			EmailAddressConverter.SetRecipientCollection(messageItem.Recipients, this.RecipientItemType, ((IStringProperty)srcProperty).StringData);
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x00073B34 File Offset: 0x00071D34
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			if (Command.CurrentCommand.Request.Version >= 160)
			{
				MessageItem messageItem = (MessageItem)base.XsoItem;
				EmailAddressConverter.ClearRecipients(messageItem.Recipients, this.RecipientItemType);
				return;
			}
			base.InternalSetToDefault(srcProperty);
		}
	}
}
