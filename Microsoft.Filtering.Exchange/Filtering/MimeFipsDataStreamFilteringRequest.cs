using System;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.UnifiedContent;
using Microsoft.Exchange.UnifiedContent.Exchange;

namespace Microsoft.Filtering
{
	// Token: 0x02000012 RID: 18
	internal sealed class MimeFipsDataStreamFilteringRequest : FipsDataStreamFilteringRequest
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002BAA File Offset: 0x00000DAA
		private MimeFipsDataStreamFilteringRequest(MailItem mailItem, string id, ContentManager contentManager) : base(id, contentManager)
		{
			this.MailItem = mailItem;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002BBB File Offset: 0x00000DBB
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002BC3 File Offset: 0x00000DC3
		public MailItem MailItem { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002BCC File Offset: 0x00000DCC
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002C10 File Offset: 0x00000E10
		public override RecoveryOptions RecoveryOptions
		{
			get
			{
				Header header = this.MailItem.MimeDocument.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-Transport-Rules-Fips-Result");
				RecoveryOptions result;
				if (header != null && Enum.TryParse<RecoveryOptions>(header.Value, out result))
				{
					return result;
				}
				return RecoveryOptions.None;
			}
			set
			{
				Header header = this.MailItem.MimeDocument.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-Transport-Rules-Fips-Result");
				if (header == null)
				{
					header = Header.Create("X-MS-Exchange-Organization-Transport-Rules-Fips-Result");
					this.MailItem.MimeDocument.RootPart.Headers.AppendChild(header);
				}
				header.Value = value.ToString();
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002C78 File Offset: 0x00000E78
		public static MimeFipsDataStreamFilteringRequest CreateInstance(MailItem mailItem)
		{
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			string messageId = mailItem.Message.MessageId;
			ContentManager contentManager = (ContentManager)ContentManagerFactory.ExtractContentManager(mailItem.Message);
			return new MimeFipsDataStreamFilteringRequest(mailItem, messageId, contentManager);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002CB8 File Offset: 0x00000EB8
		protected override void Serialize(UnifiedContentSerializer unifiedContentSerializer, bool bypassBodyTextTruncation = true)
		{
			this.MailItem.Message.Serialize(unifiedContentSerializer, bypassBodyTextTruncation);
		}
	}
}
