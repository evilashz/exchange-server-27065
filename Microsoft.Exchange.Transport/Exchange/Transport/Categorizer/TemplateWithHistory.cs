using System;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001FB RID: 507
	internal class TemplateWithHistory : IEquatable<TemplateWithHistory>, IComparable<TemplateWithHistory>
	{
		// Token: 0x06001693 RID: 5779 RVA: 0x0005C2DA File Offset: 0x0005A4DA
		public TemplateWithHistory()
		{
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0005C2E2 File Offset: 0x0005A4E2
		public TemplateWithHistory(MessageTemplate template, History history)
		{
			this.template = template;
			this.history = history;
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x0005C2F8 File Offset: 0x0005A4F8
		// (set) Token: 0x06001696 RID: 5782 RVA: 0x0005C300 File Offset: 0x0005A500
		public MessageTemplate Template
		{
			get
			{
				return this.template;
			}
			set
			{
				this.template = value;
			}
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0005C30C File Offset: 0x0005A50C
		public static TemplateWithHistory ReadFrom(MailRecipient recipient)
		{
			MessageTemplate messageTemplate = MessageTemplate.ReadFrom(recipient);
			History history = History.ReadFrom(recipient);
			if (messageTemplate.TransmitHistory)
			{
				return new TemplateWithHistory(messageTemplate, history);
			}
			if (history == null)
			{
				return new TemplateWithHistory(messageTemplate, null);
			}
			if (history.RecipientType == RecipientP2Type.Bcc)
			{
				return new TemplateWithHistory(messageTemplate, history);
			}
			return new TemplateWithHistory(messageTemplate, null);
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0005C360 File Offset: 0x0005A560
		public void Normalize(ResolverMessage message)
		{
			this.template.Normalize(message);
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0005C36E File Offset: 0x0005A56E
		public bool Equals(TemplateWithHistory other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0005C37C File Offset: 0x0005A57C
		public int CompareTo(TemplateWithHistory other)
		{
			int num = this.template.CompareTo(other.template);
			if (num != 0)
			{
				return num;
			}
			if (!(this.history == null))
			{
				return this.history.CompareTo(other.history);
			}
			if (!(other.history == null))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0005C3D4 File Offset: 0x0005A5D4
		public void ApplyTo(TransportMailItem mailItem)
		{
			ResolverMessage message = new ResolverMessage(mailItem.Message, mailItem.MimeSize);
			this.template.ApplyTo(mailItem, message);
			if (this.history != null)
			{
				this.history.WriteTo(mailItem.RootPart.Headers);
			}
		}

		// Token: 0x04000B56 RID: 2902
		public static readonly TemplateWithHistory Default = new TemplateWithHistory(MessageTemplate.Default, null);

		// Token: 0x04000B57 RID: 2903
		private MessageTemplate template;

		// Token: 0x04000B58 RID: 2904
		private History history;
	}
}
