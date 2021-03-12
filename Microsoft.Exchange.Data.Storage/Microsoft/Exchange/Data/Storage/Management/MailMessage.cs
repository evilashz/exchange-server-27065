using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A7D RID: 2685
	[Serializable]
	public sealed class MailMessage : XsoMailboxConfigurationObject
	{
		// Token: 0x17001B32 RID: 6962
		// (get) Token: 0x06006243 RID: 25155 RVA: 0x0019F4B1 File Offset: 0x0019D6B1
		internal override XsoMailboxConfigurationObjectSchema Schema
		{
			get
			{
				return MailMessage.schema;
			}
		}

		// Token: 0x17001B33 RID: 6963
		// (get) Token: 0x06006244 RID: 25156 RVA: 0x0019F4B8 File Offset: 0x0019D6B8
		// (set) Token: 0x06006245 RID: 25157 RVA: 0x0019F4CA File Offset: 0x0019D6CA
		public ADRecipientOrAddress[] Bcc
		{
			get
			{
				return (ADRecipientOrAddress[])this[MailMessageSchema.Bcc];
			}
			private set
			{
				this[MailMessageSchema.Bcc] = value;
			}
		}

		// Token: 0x17001B34 RID: 6964
		// (get) Token: 0x06006246 RID: 25158 RVA: 0x0019F4D8 File Offset: 0x0019D6D8
		// (set) Token: 0x06006247 RID: 25159 RVA: 0x0019F4EA File Offset: 0x0019D6EA
		public ADRecipientOrAddress[] Cc
		{
			get
			{
				return (ADRecipientOrAddress[])this[MailMessageSchema.Cc];
			}
			private set
			{
				this[MailMessageSchema.Cc] = value;
			}
		}

		// Token: 0x17001B35 RID: 6965
		// (get) Token: 0x06006248 RID: 25160 RVA: 0x0019F4F8 File Offset: 0x0019D6F8
		public ADRecipientOrAddress From
		{
			get
			{
				return (ADRecipientOrAddress)this[MailMessageSchema.From];
			}
		}

		// Token: 0x17001B36 RID: 6966
		// (get) Token: 0x06006249 RID: 25161 RVA: 0x0019F50A File Offset: 0x0019D70A
		public ADRecipientOrAddress Sender
		{
			get
			{
				return (ADRecipientOrAddress)this[MailMessageSchema.Sender];
			}
		}

		// Token: 0x17001B37 RID: 6967
		// (get) Token: 0x0600624A RID: 25162 RVA: 0x0019F51C File Offset: 0x0019D71C
		public string Subject
		{
			get
			{
				return (string)this[MailMessageSchema.Subject];
			}
		}

		// Token: 0x17001B38 RID: 6968
		// (get) Token: 0x0600624B RID: 25163 RVA: 0x0019F52E File Offset: 0x0019D72E
		// (set) Token: 0x0600624C RID: 25164 RVA: 0x0019F540 File Offset: 0x0019D740
		public ADRecipientOrAddress[] To
		{
			get
			{
				return (ADRecipientOrAddress[])this[MailMessageSchema.To];
			}
			private set
			{
				this[MailMessageSchema.To] = value;
			}
		}

		// Token: 0x17001B39 RID: 6969
		// (get) Token: 0x0600624D RID: 25165 RVA: 0x0019F54E File Offset: 0x0019D74E
		public override ObjectId Identity
		{
			get
			{
				return (MailboxStoreObjectId)this[MailMessageSchema.Identity];
			}
		}

		// Token: 0x17001B3A RID: 6970
		// (get) Token: 0x0600624E RID: 25166 RVA: 0x0019F560 File Offset: 0x0019D760
		internal VersionedId InternalMessageIdentity
		{
			get
			{
				return (VersionedId)this[MailMessageSchema.InternalMessageIdentity];
			}
		}

		// Token: 0x0600624F RID: 25167 RVA: 0x0019F574 File Offset: 0x0019D774
		private static ADRecipientOrAddress[] GetSpecifiedRecipients(RecipientCollection recipients, RecipientItemType recipientType)
		{
			if (recipients == null || recipients.Count <= 0)
			{
				return null;
			}
			List<ADRecipientOrAddress> list = new List<ADRecipientOrAddress>(recipients.Count);
			for (int i = 0; i < recipients.Count; i++)
			{
				if (recipients[i].RecipientItemType == recipientType)
				{
					list.Add(new ADRecipientOrAddress(recipients[i].Participant));
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list.ToArray();
		}

		// Token: 0x06006250 RID: 25168 RVA: 0x0019F5E4 File Offset: 0x0019D7E4
		internal static object FromGetter(IPropertyBag propertyBag)
		{
			Participant participant = (Participant)propertyBag[MailMessageSchema.RawFrom];
			if (null != participant)
			{
				return new ADRecipientOrAddress(participant);
			}
			return null;
		}

		// Token: 0x06006251 RID: 25169 RVA: 0x0019F614 File Offset: 0x0019D814
		internal static object IdentityGetter(IPropertyBag propertyBag)
		{
			ADObjectId mailboxOwnerId = (ADObjectId)propertyBag[XsoMailboxConfigurationObjectSchema.MailboxOwnerId];
			VersionedId versionedId = (VersionedId)propertyBag[MailMessageSchema.InternalMessageIdentity];
			if (versionedId != null)
			{
				return new MailboxStoreObjectId(mailboxOwnerId, (versionedId == null) ? null : versionedId.ObjectId);
			}
			return null;
		}

		// Token: 0x06006252 RID: 25170 RVA: 0x0019F65C File Offset: 0x0019D85C
		internal static object SenderGetter(IPropertyBag propertyBag)
		{
			Participant participant = (Participant)propertyBag[MailMessageSchema.RawSender];
			if (null != participant)
			{
				return new ADRecipientOrAddress(participant);
			}
			return null;
		}

		// Token: 0x06006253 RID: 25171 RVA: 0x0019F68B File Offset: 0x0019D88B
		internal void SetRecipients(RecipientCollection recipients)
		{
			this.Bcc = MailMessage.GetSpecifiedRecipients(recipients, RecipientItemType.Bcc);
			this.Cc = MailMessage.GetSpecifiedRecipients(recipients, RecipientItemType.Cc);
			this.To = MailMessage.GetSpecifiedRecipients(recipients, RecipientItemType.To);
		}

		// Token: 0x06006254 RID: 25172 RVA: 0x0019F6B4 File Offset: 0x0019D8B4
		public override string ToString()
		{
			if (this.Subject != null)
			{
				return this.Subject;
			}
			if (this.Identity != null)
			{
				return this.Identity.ToString();
			}
			return base.ToString();
		}

		// Token: 0x040037B9 RID: 14265
		private static MailMessageSchema schema = new MailMessageSchema();
	}
}
