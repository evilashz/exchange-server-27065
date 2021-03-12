using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002D9 RID: 729
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class MasterMailboxType
	{
		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x060018AA RID: 6314 RVA: 0x00027FDA File Offset: 0x000261DA
		// (set) Token: 0x060018AB RID: 6315 RVA: 0x00027FE2 File Offset: 0x000261E2
		public string MailboxType
		{
			get
			{
				return this.mailboxTypeField;
			}
			set
			{
				this.mailboxTypeField = value;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x00027FEB File Offset: 0x000261EB
		// (set) Token: 0x060018AD RID: 6317 RVA: 0x00027FF3 File Offset: 0x000261F3
		public string Alias
		{
			get
			{
				return this.aliasField;
			}
			set
			{
				this.aliasField = value;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x00027FFC File Offset: 0x000261FC
		// (set) Token: 0x060018AF RID: 6319 RVA: 0x00028004 File Offset: 0x00026204
		public string DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x0002800D File Offset: 0x0002620D
		// (set) Token: 0x060018B1 RID: 6321 RVA: 0x00028015 File Offset: 0x00026215
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddressField;
			}
			set
			{
				this.smtpAddressField = value;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x0002801E File Offset: 0x0002621E
		// (set) Token: 0x060018B3 RID: 6323 RVA: 0x00028026 File Offset: 0x00026226
		public ModernGroupTypeType GroupType
		{
			get
			{
				return this.groupTypeField;
			}
			set
			{
				this.groupTypeField = value;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x0002802F File Offset: 0x0002622F
		// (set) Token: 0x060018B5 RID: 6325 RVA: 0x00028037 File Offset: 0x00026237
		[XmlIgnore]
		public bool GroupTypeSpecified
		{
			get
			{
				return this.groupTypeFieldSpecified;
			}
			set
			{
				this.groupTypeFieldSpecified = value;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x00028040 File Offset: 0x00026240
		// (set) Token: 0x060018B7 RID: 6327 RVA: 0x00028048 File Offset: 0x00026248
		public string Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x00028051 File Offset: 0x00026251
		// (set) Token: 0x060018B9 RID: 6329 RVA: 0x00028059 File Offset: 0x00026259
		public string Photo
		{
			get
			{
				return this.photoField;
			}
			set
			{
				this.photoField = value;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x00028062 File Offset: 0x00026262
		// (set) Token: 0x060018BB RID: 6331 RVA: 0x0002806A File Offset: 0x0002626A
		public string SharePointUrl
		{
			get
			{
				return this.sharePointUrlField;
			}
			set
			{
				this.sharePointUrlField = value;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x00028073 File Offset: 0x00026273
		// (set) Token: 0x060018BD RID: 6333 RVA: 0x0002807B File Offset: 0x0002627B
		public string InboxUrl
		{
			get
			{
				return this.inboxUrlField;
			}
			set
			{
				this.inboxUrlField = value;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x00028084 File Offset: 0x00026284
		// (set) Token: 0x060018BF RID: 6335 RVA: 0x0002808C File Offset: 0x0002628C
		public string CalendarUrl
		{
			get
			{
				return this.calendarUrlField;
			}
			set
			{
				this.calendarUrlField = value;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x00028095 File Offset: 0x00026295
		// (set) Token: 0x060018C1 RID: 6337 RVA: 0x0002809D File Offset: 0x0002629D
		public string DomainController
		{
			get
			{
				return this.domainControllerField;
			}
			set
			{
				this.domainControllerField = value;
			}
		}

		// Token: 0x040010B4 RID: 4276
		private string mailboxTypeField;

		// Token: 0x040010B5 RID: 4277
		private string aliasField;

		// Token: 0x040010B6 RID: 4278
		private string displayNameField;

		// Token: 0x040010B7 RID: 4279
		private string smtpAddressField;

		// Token: 0x040010B8 RID: 4280
		private ModernGroupTypeType groupTypeField;

		// Token: 0x040010B9 RID: 4281
		private bool groupTypeFieldSpecified;

		// Token: 0x040010BA RID: 4282
		private string descriptionField;

		// Token: 0x040010BB RID: 4283
		private string photoField;

		// Token: 0x040010BC RID: 4284
		private string sharePointUrlField;

		// Token: 0x040010BD RID: 4285
		private string inboxUrlField;

		// Token: 0x040010BE RID: 4286
		private string calendarUrlField;

		// Token: 0x040010BF RID: 4287
		private string domainControllerField;
	}
}
