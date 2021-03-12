using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000090 RID: 144
	[DataContract]
	public class EMailSignatureConfiguration : MessagingConfigurationBase
	{
		// Token: 0x06001BAE RID: 7086 RVA: 0x000576DF File Offset: 0x000558DF
		public EMailSignatureConfiguration(MailboxMessageConfiguration mailboxMessageConfiguration) : base(mailboxMessageConfiguration)
		{
		}

		// Token: 0x170018AC RID: 6316
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x000576E8 File Offset: 0x000558E8
		// (set) Token: 0x06001BB0 RID: 7088 RVA: 0x000576FA File Offset: 0x000558FA
		[DataMember]
		public string SignatureHtml
		{
			get
			{
				return TextConverterHelper.SanitizeHtml(base.MailboxMessageConfiguration.SignatureHtml);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018AD RID: 6317
		// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x00057701 File Offset: 0x00055901
		// (set) Token: 0x06001BB2 RID: 7090 RVA: 0x0005770E File Offset: 0x0005590E
		[DataMember]
		public bool AutoAddSignature
		{
			get
			{
				return base.MailboxMessageConfiguration.AutoAddSignature;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
