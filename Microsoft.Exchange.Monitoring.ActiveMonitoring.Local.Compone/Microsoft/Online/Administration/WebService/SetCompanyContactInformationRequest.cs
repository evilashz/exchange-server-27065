using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002FD RID: 765
	[DataContract(Name = "SetCompanyContactInformationRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class SetCompanyContactInformationRequest : Request
	{
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x0008B2CF File Offset: 0x000894CF
		// (set) Token: 0x060014D2 RID: 5330 RVA: 0x0008B2D7 File Offset: 0x000894D7
		[DataMember]
		public string[] MarketingNotificationEmails
		{
			get
			{
				return this.MarketingNotificationEmailsField;
			}
			set
			{
				this.MarketingNotificationEmailsField = value;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x0008B2E0 File Offset: 0x000894E0
		// (set) Token: 0x060014D4 RID: 5332 RVA: 0x0008B2E8 File Offset: 0x000894E8
		[DataMember]
		public string[] TechnicalNotificationEmails
		{
			get
			{
				return this.TechnicalNotificationEmailsField;
			}
			set
			{
				this.TechnicalNotificationEmailsField = value;
			}
		}

		// Token: 0x04000F85 RID: 3973
		private string[] MarketingNotificationEmailsField;

		// Token: 0x04000F86 RID: 3974
		private string[] TechnicalNotificationEmailsField;
	}
}
