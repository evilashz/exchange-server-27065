using System;
using Microsoft.Exchange.PST;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000002 RID: 2
	internal class PSTAttachment : IAttachment, IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public PSTAttachment(PSTMessage parentMessage, IAttachment iPstAttachment)
		{
			this.parentMessage = parentMessage;
			this.iPstAttachment = iPstAttachment;
			this.propertyBag = new PSTPropertyBag(parentMessage.PstMailbox, iPstAttachment.PropertyBag);
			PropertyValue property = this.propertyBag.GetProperty(PropertyTag.AttachmentNumber);
			if (property.IsError)
			{
				property = new PropertyValue(PropertyTag.AttachmentNumber, (int)iPstAttachment.AttachmentNumber);
			}
			this.propertyBag.SetProperty(property);
			this.attachmentNumber = (int)property.Value;
			if (iPstAttachment.Message != null)
			{
				this.embeddedMessage = new PSTMessage(parentMessage.PstMailbox, iPstAttachment.Message, true);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002177 File Offset: 0x00000377
		public IPropertyBag PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000217F File Offset: 0x0000037F
		public bool IsEmbeddedMessage
		{
			get
			{
				return this.embeddedMessage != null;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002190 File Offset: 0x00000390
		public int AttachmentNumber
		{
			get
			{
				PropertyValue propertyValue = this.propertyBag.GetAnnotatedProperty(PropertyTag.AttachmentNumber).PropertyValue;
				if (propertyValue.IsError)
				{
					return this.attachmentNumber;
				}
				return (int)propertyValue.Value;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021D0 File Offset: 0x000003D0
		public IMessage GetEmbeddedMessage()
		{
			if (this.embeddedMessage == null)
			{
				IMessage iPstMessage = this.iPstAttachment.AddMessageAttachment();
				this.embeddedMessage = new PSTMessage(this.parentMessage.PstMailbox, iPstMessage, true);
			}
			return this.embeddedMessage;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002210 File Offset: 0x00000410
		public void Save()
		{
			try
			{
				this.iPstAttachment.Save();
			}
			catch (PSTExceptionBase innerException)
			{
				throw new UnableToCreatePSTMessagePermanentException(this.parentMessage.PstMailbox.IPst.FileName, innerException);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002258 File Offset: 0x00000458
		public void Dispose()
		{
		}

		// Token: 0x04000001 RID: 1
		private PSTMessage parentMessage;

		// Token: 0x04000002 RID: 2
		private int attachmentNumber;

		// Token: 0x04000003 RID: 3
		private PSTPropertyBag propertyBag;

		// Token: 0x04000004 RID: 4
		private PSTMessage embeddedMessage;

		// Token: 0x04000005 RID: 5
		private IAttachment iPstAttachment;
	}
}
