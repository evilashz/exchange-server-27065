using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200001E RID: 30
	public abstract class Message : Item
	{
		// Token: 0x06000495 RID: 1173 RVA: 0x0002CD14 File Offset: 0x0002AF14
		protected Message(Context context, Table table, PhysicalColumn sizeColumn, Mailbox mailbox, bool changeTrackingEnabled, bool newMessage, bool existsInDatabase, bool writeThrough, params ColumnValue[] initialValues) : base(context, table, sizeColumn, mailbox, changeTrackingEnabled, newMessage, existsInDatabase, writeThrough, initialValues)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				if (newMessage && !existsInDatabase)
				{
					this.SetProperty(context, PropTag.Message.HasAttach, false);
					this.SetProperty(context, PropTag.Message.MailFlags, 0);
					this.recipientList = new RecipientCollection(this);
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0002CDA0 File Offset: 0x0002AFA0
		protected Message(Context context, Table table, PhysicalColumn sizeColumn, Mailbox mailbox, bool changeTrackingEnabled, bool writeThrough, Reader reader) : base(context, table, sizeColumn, mailbox, changeTrackingEnabled, writeThrough, reader)
		{
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0002CDB3 File Offset: 0x0002AFB3
		public int AttachCount
		{
			get
			{
				if (base.Subobjects != null)
				{
					return base.Subobjects.ChildrenCount;
				}
				return 0;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0002CDCA File Offset: 0x0002AFCA
		public override bool IsDirty
		{
			get
			{
				return base.IsDirty || (this.recipientList != null && this.recipientList.Changed);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0002CDEB File Offset: 0x0002AFEB
		protected override bool IsDirtyExceptDataRow
		{
			get
			{
				return base.IsDirtyExceptDataRow || (this.recipientList != null && this.recipientList.Changed);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600049A RID: 1178
		public abstract bool IsEmbedded { get; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0002CE0C File Offset: 0x0002B00C
		protected override StorePropTag ItemSubobjectsBinPropTag
		{
			get
			{
				return PropTag.Message.ItemSubobjectsBin;
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0002CE14 File Offset: 0x0002B014
		public RecipientCollection GetRecipients(Context context)
		{
			if (this.recipientList == null)
			{
				byte[][] blob = (byte[][])this.GetPropertyValue(context, PropTag.Message.MessageRecipientsMVBin);
				this.recipientList = new RecipientCollection(this, blob);
			}
			return this.recipientList;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0002CE50 File Offset: 0x0002B050
		public Attachment OpenAttachment(Context context, int attachmentNumber)
		{
			if (base.Subobjects == null || !base.Subobjects.ContainsChild(attachmentNumber))
			{
				return null;
			}
			return (Attachment)this.OpenChild(context, attachmentNumber, base.Subobjects.GetChildInid(attachmentNumber).Value);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0002CE98 File Offset: 0x0002B098
		public Attachment CreateAttachment(Context context)
		{
			int attachNum = base.ReserveChildNumber();
			return Attachment.CreateAttachment(context, this, attachNum);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0002CEB4 File Offset: 0x0002B0B4
		protected override Item OpenChild(Context context, int childNumber, long inid)
		{
			Item result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				Attachment attachment = disposeGuard.Add<Attachment>(Attachment.OpenAttachment(context, this, childNumber, inid));
				if (base.Subobjects.GetChildSize(childNumber) == -1L)
				{
					base.Subobjects.SetChildSize(childNumber, attachment.OriginalSize);
				}
				disposeGuard.Success();
				result = attachment;
			}
			return result;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0002CF28 File Offset: 0x0002B128
		protected override Item CopyChild(Context context, int childNumber, long inid)
		{
			Item item = Attachment.CopyAttachment(context, this, childNumber, inid);
			if (base.Subobjects.GetChildSize(childNumber) == -1L)
			{
				base.Subobjects.SetChildSize(childNumber, item.OriginalSize);
			}
			return item;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0002CF62 File Offset: 0x0002B162
		public IEnumerable<int> GetAttachmentNumbers()
		{
			if (base.Subobjects != null)
			{
				return base.Subobjects.GetChildNumbers();
			}
			return Enumerable.Empty<int>();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0002CF7D File Offset: 0x0002B17D
		public byte[] GetAttachmentsBlob()
		{
			if (base.Subobjects != null)
			{
				return base.Subobjects.SerializeChildren();
			}
			return null;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0002CF94 File Offset: 0x0002B194
		public bool GetIsRead(Context context)
		{
			return (bool)this.GetPropertyValue(context, PropTag.Message.Read);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0002CFA7 File Offset: 0x0002B1A7
		public bool SetIsRead(Context context, bool value)
		{
			if (value != this.GetIsRead(context))
			{
				this.SetProperty(context, PropTag.Message.Read, value);
				return true;
			}
			return false;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0002CFC9 File Offset: 0x0002B1C9
		public bool GetIsDeliveryCompleted(Context context)
		{
			return PropertyBagHelpers.TestPropertyFlags(context, this, PropTag.Message.MailFlags, 1, 1);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0002CFD9 File Offset: 0x0002B1D9
		public bool SetIsDeliveryCompleted(Context context, bool value)
		{
			return PropertyBagHelpers.SetPropertyFlags(context, this, PropTag.Message.MailFlags, value, 1);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0002CFF0 File Offset: 0x0002B1F0
		public bool GetNeedsReadNotification(Context context)
		{
			TopMessage topMessage = this as TopMessage;
			return (topMessage == null || !topMessage.ParentFolder.IsPerUserReadUnreadTrackingEnabled) && PropertyBagHelpers.TestPropertyFlags(context, this, PropTag.Message.MailFlags, 34, 2);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0002D028 File Offset: 0x0002B228
		public bool SetNeedsReadNotification(Context context, bool value)
		{
			TopMessage topMessage = this as TopMessage;
			return (topMessage == null || !topMessage.ParentFolder.IsPerUserReadUnreadTrackingEnabled) && PropertyBagHelpers.SetPropertyFlags(context, this, PropTag.Message.MailFlags, value, 2);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0002D064 File Offset: 0x0002B264
		public bool GetNeedsNotReadNotification(Context context)
		{
			TopMessage topMessage = this as TopMessage;
			return (topMessage == null || !topMessage.ParentFolder.IsPerUserReadUnreadTrackingEnabled) && PropertyBagHelpers.TestPropertyFlags(context, this, PropTag.Message.MailFlags, 36, 4);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0002D09C File Offset: 0x0002B29C
		public bool SetNeedsNotReadNotification(Context context, bool value)
		{
			TopMessage topMessage = this as TopMessage;
			return (topMessage == null || !topMessage.ParentFolder.IsPerUserReadUnreadTrackingEnabled) && PropertyBagHelpers.SetPropertyFlags(context, this, PropTag.Message.MailFlags, value, 4);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0002D0D5 File Offset: 0x0002B2D5
		public bool GetOOFCanBeSent(Context context)
		{
			return PropertyBagHelpers.TestPropertyFlags(context, this, PropTag.Message.MailFlags, 8, 8);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0002D0E5 File Offset: 0x0002B2E5
		public bool SetOOFCanBeSent(Context context, bool value)
		{
			return PropertyBagHelpers.SetPropertyFlags(context, this, PropTag.Message.MailFlags, value, 8);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0002D0FA File Offset: 0x0002B2FA
		public bool GetSentRepresentingAddedByTransport(Context context)
		{
			return PropertyBagHelpers.TestPropertyFlags(context, this, PropTag.Message.MailFlags, 16, 16);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0002D10C File Offset: 0x0002B30C
		public bool SetSentRepresentingAddedByTransport(Context context, bool value)
		{
			return PropertyBagHelpers.SetPropertyFlags(context, this, PropTag.Message.MailFlags, value, 16);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0002D124 File Offset: 0x0002B324
		public bool GetReadReceiptSent(Context context)
		{
			TopMessage topMessage = this as TopMessage;
			return (topMessage != null && topMessage.ParentFolder.IsPerUserReadUnreadTrackingEnabled) || PropertyBagHelpers.TestPropertyFlags(context, this, PropTag.Message.MailFlags, 32, 32);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0002D15C File Offset: 0x0002B35C
		public bool SetReadReceiptSent(Context context, bool value)
		{
			TopMessage topMessage = this as TopMessage;
			return (topMessage == null || !topMessage.ParentFolder.IsPerUserReadUnreadTrackingEnabled) && PropertyBagHelpers.SetPropertyFlags(context, this, PropTag.Message.MailFlags, value, 32);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0002D196 File Offset: 0x0002B396
		public string GetMessageClass(Context context)
		{
			return (string)this.GetPropertyValue(context, PropTag.Message.MessageClass);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0002D1A9 File Offset: 0x0002B3A9
		public void SetMessageClass(Context context, string value)
		{
			this.SetProperty(context, PropTag.Message.MessageClass, value);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0002D1B9 File Offset: 0x0002B3B9
		public short? GetBodyType(Context context)
		{
			return (short?)this.GetPropertyValue(context, PropTag.Message.NativeBodyType);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0002D1CC File Offset: 0x0002B3CC
		public void SetBodyType(Context context, short? value)
		{
			this.SetProperty(context, PropTag.Message.NativeBodyType, value);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0002D1E4 File Offset: 0x0002B3E4
		public Stream GetReadNativeBody(Context context)
		{
			Stream result;
			ErrorCode first = this.OpenPropertyReadStream(context, PropTag.Message.NativeBody, out result);
			if (!(first != ErrorCode.NoError))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0002D210 File Offset: 0x0002B410
		public Stream GetWriteNativeBody(Context context)
		{
			Stream result;
			ErrorCode first = this.OpenPropertyWriteStream(context, PropTag.Message.NativeBody, out result);
			if (!(first != ErrorCode.NoError))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0002D23C File Offset: 0x0002B43C
		public bool AdjustMessageFlags(Context context, MessageFlags flagsToSet, MessageFlags flagsToClear)
		{
			return PropertyBagHelpers.AdjustPropertyFlags(context, this, PropTag.Message.MessageFlags, (int)flagsToSet, (int)flagsToClear);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0002D24C File Offset: 0x0002B44C
		public bool AdjustUncomputedMessageFlags(Context context, MessageFlags flagsToSet, MessageFlags flagsToClear)
		{
			return PropertyBagHelpers.AdjustPropertyFlags(context, this, PropTag.Message.MessageFlagsActual, (int)flagsToSet, (int)flagsToClear);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0002D25C File Offset: 0x0002B45C
		public string GetSubject(Context context)
		{
			return (string)this.GetPropertyValue(context, PropTag.Message.NormalizedSubject);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0002D26F File Offset: 0x0002B46F
		public void SetSubject(Context context, string value)
		{
			this.SetProperty(context, PropTag.Message.NormalizedSubject, value);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0002D27F File Offset: 0x0002B47F
		public void SetSubjectPrefix(Context context, string value)
		{
			this.SetProperty(context, PropTag.Message.SubjectPrefix, value);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0002D28F File Offset: 0x0002B48F
		public string GetDisplayNameTo(Context context)
		{
			this.RefreshDisplayNameTo(context);
			return (string)this.GetPropertyValue(context, PropTag.Message.DisplayTo);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0002D2A9 File Offset: 0x0002B4A9
		public string GetDisplayNameCc(Context context)
		{
			this.RefreshDisplayNameCc(context);
			return (string)this.GetPropertyValue(context, PropTag.Message.DisplayCc);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0002D2C3 File Offset: 0x0002B4C3
		public string GetDisplayNameBcc(Context context)
		{
			this.RefreshDisplayNameBcc(context);
			return (string)this.GetPropertyValue(context, PropTag.Message.DisplayBcc);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0002D2DD File Offset: 0x0002B4DD
		public override void Flush(Context context, bool flushLargeDirtyStreams)
		{
			if (this.IsDirty)
			{
				this.UpdateRecipients(context);
				base.Flush(context, flushLargeDirtyStreams);
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0002D2F6 File Offset: 0x0002B4F6
		public override bool SaveChanges(Context context)
		{
			if (this.IsEmbedded)
			{
				this.SetProperty(context, PropTag.Message.IMAPId, null);
				this.SetProperty(context, PropTag.Message.InternetArticleNumber, null);
			}
			return base.SaveChanges(context);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0002D328 File Offset: 0x0002B528
		internal override void SaveChild(Context context, ISubobject child)
		{
			base.SaveChild(context, child);
			this.SetProperty(context, PropTag.Message.HasAttach, 0 != this.AttachCount);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0002D350 File Offset: 0x0002B550
		internal override void DeleteChild(Context context, ISubobject child)
		{
			base.DeleteChild(context, child);
			this.SetProperty(context, PropTag.Message.HasAttach, 0 != this.AttachCount);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0002D378 File Offset: 0x0002B578
		public override void Scrub(Context context)
		{
			base.Scrub(context);
			this.SetProperty(context, PropTag.Message.HasAttach, false);
			this.recipientList = new RecipientCollection(this);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0002D3A0 File Offset: 0x0002B5A0
		protected void UpdateRecipients(Context context)
		{
			if (this.recipientList != null && this.recipientList.Changed)
			{
				this.RefreshDisplayNameTo(context);
				this.RefreshDisplayNameCc(context);
				this.RefreshDisplayNameBcc(context);
				byte[][] value = this.recipientList.ToBinary(context);
				this.SetProperty(context, PropTag.Message.MessageRecipientsMVBin, value);
				this.recipientList.Changed = false;
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0002D3FE File Offset: 0x0002B5FE
		private void RefreshDisplayNameTo(Context context)
		{
			if (!this.IsComputedPropertyValid(PropTag.Message.DisplayTo))
			{
				this.ResetDisplayName(context, PropTag.Message.DisplayTo, RecipientType.To);
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0002D41A File Offset: 0x0002B61A
		private void RefreshDisplayNameCc(Context context)
		{
			if (!this.IsComputedPropertyValid(PropTag.Message.DisplayCc))
			{
				this.ResetDisplayName(context, PropTag.Message.DisplayCc, RecipientType.Cc);
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0002D436 File Offset: 0x0002B636
		private void RefreshDisplayNameBcc(Context context)
		{
			if (!this.IsComputedPropertyValid(PropTag.Message.DisplayBcc))
			{
				this.ResetDisplayName(context, PropTag.Message.DisplayBcc, RecipientType.Bcc);
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0002D454 File Offset: 0x0002B654
		private void ResetDisplayName(Context context, StorePropTag propTag, RecipientType rt)
		{
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in this.GetRecipients(context))
			{
				Recipient recipient = (Recipient)obj;
				if (recipient.RecipientType == rt)
				{
					if (flag)
					{
						stringBuilder.Append("; ");
					}
					else
					{
						flag = true;
					}
					stringBuilder.Append(recipient.Name);
				}
			}
			this.SetProperty(context, propTag, stringBuilder.ToString());
			this.MarkComputedPropertyAsValid(propTag);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0002D4F4 File Offset: 0x0002B6F4
		protected bool IsComputedPropertyValid(StorePropTag propTag)
		{
			return this.computedPropInvalid == null || !this.computedPropInvalid.Contains(propTag);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0002D50F File Offset: 0x0002B70F
		public void MarkComputedPropertyAsInvalid(StorePropTag propTag)
		{
			if (this.computedPropInvalid == null)
			{
				this.computedPropInvalid = new HashSet<StorePropTag>();
			}
			this.computedPropInvalid.Add(propTag);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0002D531 File Offset: 0x0002B731
		protected void MarkComputedPropertyAsValid(StorePropTag propTag)
		{
			if (this.computedPropInvalid != null)
			{
				this.computedPropInvalid.Remove(propTag);
			}
		}

		// Token: 0x04000227 RID: 551
		public const MessageFlags ComputedMessageFlags = MessageFlags.Read | MessageFlags.HasAttachment | MessageFlags.Associated | MessageFlags.ReadNotificationPending | MessageFlags.NonReadNotificationPending | MessageFlags.EverRead;

		// Token: 0x04000228 RID: 552
		private RecipientCollection recipientList;

		// Token: 0x04000229 RID: 553
		private HashSet<StorePropTag> computedPropInvalid;
	}
}
