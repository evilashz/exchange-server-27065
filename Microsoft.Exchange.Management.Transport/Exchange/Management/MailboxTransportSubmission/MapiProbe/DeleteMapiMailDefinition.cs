using System;
using System.Text;

namespace Microsoft.Exchange.Management.MailboxTransportSubmission.MapiProbe
{
	// Token: 0x0200004F RID: 79
	internal class DeleteMapiMailDefinition
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x0000BBE5 File Offset: 0x00009DE5
		private DeleteMapiMailDefinition()
		{
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000BBED File Offset: 0x00009DED
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x0000BBF5 File Offset: 0x00009DF5
		public string SenderEmailAddress { get; internal set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000BBFE File Offset: 0x00009DFE
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x0000BC06 File Offset: 0x00009E06
		public string MessageClass { get; private set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000BC0F File Offset: 0x00009E0F
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000BC17 File Offset: 0x00009E17
		public string InternetMessageId { get; private set; }

		// Token: 0x060002EA RID: 746 RVA: 0x0000BC20 File Offset: 0x00009E20
		public static DeleteMapiMailDefinition CreateInstance(string messageClass, string senderEmail, string internetMessageId)
		{
			if (string.IsNullOrEmpty(messageClass))
			{
				throw new ArgumentException("messageClass is null or empty");
			}
			if (string.IsNullOrEmpty(senderEmail))
			{
				throw new ArgumentException("senderEmail is null or empty");
			}
			if (string.IsNullOrEmpty(internetMessageId))
			{
				throw new ArgumentException("internetMessageId is null or empty");
			}
			return new DeleteMapiMailDefinition
			{
				MessageClass = messageClass,
				SenderEmailAddress = senderEmail,
				InternetMessageId = internetMessageId
			};
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000BC84 File Offset: 0x00009E84
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Message Class: " + this.MessageClass);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Sender Email Address: " + this.SenderEmailAddress);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("InternetMessageId: " + this.InternetMessageId);
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}
	}
}
