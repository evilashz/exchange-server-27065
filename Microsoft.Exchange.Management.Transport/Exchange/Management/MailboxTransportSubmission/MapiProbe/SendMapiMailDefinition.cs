using System;
using System.Text;

namespace Microsoft.Exchange.Management.MailboxTransportSubmission.MapiProbe
{
	// Token: 0x02000058 RID: 88
	internal class SendMapiMailDefinition
	{
		// Token: 0x06000313 RID: 787 RVA: 0x0000C47A File Offset: 0x0000A67A
		private SendMapiMailDefinition()
		{
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000C482 File Offset: 0x0000A682
		// (set) Token: 0x06000315 RID: 789 RVA: 0x0000C48A File Offset: 0x0000A68A
		public string SenderEmailAddress
		{
			get
			{
				return this.senderEmailAddress;
			}
			internal set
			{
				this.senderEmailAddress = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000C493 File Offset: 0x0000A693
		// (set) Token: 0x06000317 RID: 791 RVA: 0x0000C49B File Offset: 0x0000A69B
		public Guid SenderMbxGuid
		{
			get
			{
				return this.senderMbxGuid;
			}
			internal set
			{
				this.senderMbxGuid = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		// (set) Token: 0x06000319 RID: 793 RVA: 0x0000C4AC File Offset: 0x0000A6AC
		public Guid SenderMdbGuid
		{
			get
			{
				return this.senderMdbGuid;
			}
			internal set
			{
				this.senderMdbGuid = value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000C4B5 File Offset: 0x0000A6B5
		// (set) Token: 0x0600031B RID: 795 RVA: 0x0000C4BD File Offset: 0x0000A6BD
		public string RecipientEmailAddress
		{
			get
			{
				return this.recipientEmailAddress;
			}
			internal set
			{
				this.recipientEmailAddress = value;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000C4C6 File Offset: 0x0000A6C6
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0000C4CE File Offset: 0x0000A6CE
		public string MessageSubject
		{
			get
			{
				return this.messageSubject;
			}
			private set
			{
				this.messageSubject = value;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000C4D7 File Offset: 0x0000A6D7
		// (set) Token: 0x0600031F RID: 799 RVA: 0x0000C4DF File Offset: 0x0000A6DF
		public string MessageClass
		{
			get
			{
				return this.messageClass;
			}
			private set
			{
				this.messageClass = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000C4E8 File Offset: 0x0000A6E8
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		public string MessageBody
		{
			get
			{
				return this.messageBody;
			}
			private set
			{
				this.messageBody = value;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000C4F9 File Offset: 0x0000A6F9
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0000C501 File Offset: 0x0000A701
		public bool DoNotDeliver
		{
			get
			{
				return this.doNotDeliver;
			}
			private set
			{
				this.doNotDeliver = value;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000C50A File Offset: 0x0000A70A
		// (set) Token: 0x06000325 RID: 805 RVA: 0x0000C512 File Offset: 0x0000A712
		public bool DropMessageInHub
		{
			get
			{
				return this.dropMessageInHub;
			}
			private set
			{
				this.dropMessageInHub = value;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000C51B File Offset: 0x0000A71B
		// (set) Token: 0x06000327 RID: 807 RVA: 0x0000C523 File Offset: 0x0000A723
		public bool DeleteAfterSubmit
		{
			get
			{
				return this.deleteAfterSubmit;
			}
			private set
			{
				this.deleteAfterSubmit = value;
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000C52C File Offset: 0x0000A72C
		public static SendMapiMailDefinition CreateInstance(string subject, string body, string messageClass, bool doNotDeliver, bool dropMessageInHub, bool deleteAfterSubmit, string senderEmail, Guid senderMbx, Guid senderMdb, string recipientEmail, bool skipSenderValidation)
		{
			if (string.IsNullOrEmpty(subject))
			{
				throw new ArgumentNullException("subject");
			}
			if (string.IsNullOrEmpty(body))
			{
				throw new ArgumentNullException("body");
			}
			if (string.IsNullOrEmpty(messageClass))
			{
				throw new ArgumentNullException("messageClass");
			}
			if (!skipSenderValidation)
			{
				if (string.IsNullOrEmpty(senderEmail))
				{
					throw new ArgumentNullException("senderEmail");
				}
				if (string.IsNullOrEmpty(recipientEmail))
				{
					throw new ArgumentNullException("recipientEmail");
				}
			}
			return new SendMapiMailDefinition
			{
				MessageSubject = subject,
				MessageBody = body,
				MessageClass = messageClass,
				DoNotDeliver = doNotDeliver,
				DropMessageInHub = dropMessageInHub,
				DeleteAfterSubmit = deleteAfterSubmit,
				SenderEmailAddress = senderEmail,
				SenderMbxGuid = senderMbx,
				SenderMdbGuid = senderMdb,
				RecipientEmailAddress = recipientEmail
			};
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000C5F4 File Offset: 0x0000A7F4
		public static SendMapiMailDefinition CreateInstance(string subject, string body, string messageClass, bool doNotDeliver, bool deleteAfterSubmit, string senderEmail, string recipientEmail)
		{
			return SendMapiMailDefinition.CreateInstance(subject, body, messageClass, doNotDeliver, false, deleteAfterSubmit, senderEmail, Guid.Empty, Guid.Empty, recipientEmail, false);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000C61C File Offset: 0x0000A81C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Message Subject: " + this.MessageSubject);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Message Body: " + this.MessageBody);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Message Class: " + this.MessageClass);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Do Not Deliver Message: " + this.DoNotDeliver);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Drop Message in Hub: " + this.DropMessageInHub);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Delete Message From SentItems After Submit: " + this.DeleteAfterSubmit);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Sender Email Address: " + this.SenderEmailAddress);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Recipient Email Address: " + this.RecipientEmailAddress);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Sender Mbx Guid: " + this.SenderMbxGuid);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Sender Mdb Guid: " + this.SenderMdbGuid);
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		// Token: 0x04000126 RID: 294
		private string senderEmailAddress;

		// Token: 0x04000127 RID: 295
		private Guid senderMbxGuid;

		// Token: 0x04000128 RID: 296
		private Guid senderMdbGuid;

		// Token: 0x04000129 RID: 297
		private string recipientEmailAddress;

		// Token: 0x0400012A RID: 298
		private string messageSubject;

		// Token: 0x0400012B RID: 299
		private string messageClass;

		// Token: 0x0400012C RID: 300
		private string messageBody;

		// Token: 0x0400012D RID: 301
		private bool doNotDeliver;

		// Token: 0x0400012E RID: 302
		private bool dropMessageInHub;

		// Token: 0x0400012F RID: 303
		private bool deleteAfterSubmit;
	}
}
