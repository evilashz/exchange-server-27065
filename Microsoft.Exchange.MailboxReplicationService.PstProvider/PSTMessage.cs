using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.PST;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000D RID: 13
	internal class PSTMessage : IMessage, IDisposable
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00004BC1 File Offset: 0x00002DC1
		public PSTMessage(PstMailbox pstMailbox, IMessage iPstMessage) : this(pstMailbox, iPstMessage, false)
		{
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004BCC File Offset: 0x00002DCC
		public PSTMessage(PstMailbox pstMailbox, IMessage iPstMessage, bool isEmbedded)
		{
			if (pstMailbox.IPst == null || iPstMessage == null)
			{
				throw new ArgumentNullException((pstMailbox.IPst == null) ? "iPst" : "iPstMessage");
			}
			this.pstMailbox = pstMailbox;
			this.iPstMessage = iPstMessage;
			this.isEmbedded = isEmbedded;
			this.propertyBag = new PSTPropertyBag(this.pstMailbox, iPstMessage.PropertyBag);
			this.recipients = new List<IRecipient>(0);
			this.attachments = new List<IAttachment>(0);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004C48 File Offset: 0x00002E48
		public PstMailbox PstMailbox
		{
			get
			{
				return this.pstMailbox;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004C50 File Offset: 0x00002E50
		public IMessage IPstMessage
		{
			get
			{
				return this.iPstMessage;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004C58 File Offset: 0x00002E58
		public PSTPropertyBag RawPropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004C60 File Offset: 0x00002E60
		public IPropertyBag PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004C68 File Offset: 0x00002E68
		public bool IsAssociated
		{
			get
			{
				return this.iPstMessage.IsAssociated;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004C75 File Offset: 0x00002E75
		public bool IsEmbedded
		{
			get
			{
				return this.isEmbedded;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004C7D File Offset: 0x00002E7D
		public List<IRecipient> Recipients
		{
			get
			{
				return this.recipients;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004C85 File Offset: 0x00002E85
		public List<IAttachment> Attachments
		{
			get
			{
				return this.attachments;
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004C90 File Offset: 0x00002E90
		public IEnumerable<IRecipient> GetRecipients()
		{
			if (this.recipients.Count != this.iPstMessage.RecipientIds.Count)
			{
				foreach (uint num in this.iPstMessage.RecipientIds)
				{
					this.recipients.Add(new PSTRecipient(this.recipients.Count, new PSTPropertyBag(this.pstMailbox, this.iPstMessage.ReadRecipient(num))));
				}
			}
			return this.recipients.Cast<IRecipient>();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004D3C File Offset: 0x00002F3C
		public IRecipient CreateRecipient()
		{
			IPropertyBag iPstPropertyBag = this.iPstMessage.AddRecipient();
			PSTRecipient pstrecipient = new PSTRecipient(this.recipients.Count, new PSTPropertyBag(this.pstMailbox, iPstPropertyBag));
			this.recipients.Add(pstrecipient);
			return pstrecipient;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004D7F File Offset: 0x00002F7F
		public void RemoveRecipient(int rowId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004D86 File Offset: 0x00002F86
		public void Save()
		{
			this.iPstMessage.Save();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004D93 File Offset: 0x00002F93
		public void SetLongTermId(StoreLongTermId longTermId)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004D9A File Offset: 0x00002F9A
		public void Dispose()
		{
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004D9C File Offset: 0x00002F9C
		public IAttachment CreateAttachment()
		{
			IAttachment iPstAttachment = this.iPstMessage.AddAttachment();
			PSTAttachment pstattachment = new PSTAttachment(this, iPstAttachment);
			this.attachments.Add(pstattachment);
			return pstattachment;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004F84 File Offset: 0x00003184
		public IEnumerable<IAttachmentHandle> GetAttachments()
		{
			if (this.attachments.Count != this.iPstMessage.AttachmentIds.Count)
			{
				foreach (uint num in this.iPstMessage.AttachmentIds)
				{
					IAttachment iPstAttachment = this.iPstMessage.ReadAttachment(num);
					this.attachments.Add(new PSTAttachment(this, iPstAttachment));
				}
			}
			for (int i = 0; i < this.attachments.Count; i++)
			{
				yield return new PSTMessage.PSTAttachmentHandle(this.attachments, i);
			}
			yield break;
		}

		// Token: 0x0400002B RID: 43
		private PstMailbox pstMailbox;

		// Token: 0x0400002C RID: 44
		private IMessage iPstMessage;

		// Token: 0x0400002D RID: 45
		private PSTPropertyBag propertyBag;

		// Token: 0x0400002E RID: 46
		private List<IRecipient> recipients;

		// Token: 0x0400002F RID: 47
		private List<IAttachment> attachments;

		// Token: 0x04000030 RID: 48
		private bool isEmbedded;

		// Token: 0x0200000E RID: 14
		private class PSTAttachmentHandle : IAttachmentHandle
		{
			// Token: 0x060000C2 RID: 194 RVA: 0x00004FA1 File Offset: 0x000031A1
			public PSTAttachmentHandle(IList<IAttachment> attachmentList, int attachmentIndex)
			{
				this.attachmentList = attachmentList;
				this.attachmentIndex = attachmentIndex;
			}

			// Token: 0x060000C3 RID: 195 RVA: 0x00004FB7 File Offset: 0x000031B7
			public IAttachment GetAttachment()
			{
				return this.attachmentList[this.attachmentIndex];
			}

			// Token: 0x04000031 RID: 49
			private readonly IList<IAttachment> attachmentList;

			// Token: 0x04000032 RID: 50
			private readonly int attachmentIndex;
		}
	}
}
