using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001C3 RID: 451
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class MailTipsServiceConfiguration : ServiceConfiguration
	{
		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x00025333 File Offset: 0x00023533
		// (set) Token: 0x0600135C RID: 4956 RVA: 0x0002533B File Offset: 0x0002353B
		public bool MailTipsEnabled
		{
			get
			{
				return this.mailTipsEnabledField;
			}
			set
			{
				this.mailTipsEnabledField = value;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x00025344 File Offset: 0x00023544
		// (set) Token: 0x0600135E RID: 4958 RVA: 0x0002534C File Offset: 0x0002354C
		public int MaxRecipientsPerGetMailTipsRequest
		{
			get
			{
				return this.maxRecipientsPerGetMailTipsRequestField;
			}
			set
			{
				this.maxRecipientsPerGetMailTipsRequestField = value;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x00025355 File Offset: 0x00023555
		// (set) Token: 0x06001360 RID: 4960 RVA: 0x0002535D File Offset: 0x0002355D
		public int MaxMessageSize
		{
			get
			{
				return this.maxMessageSizeField;
			}
			set
			{
				this.maxMessageSizeField = value;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x00025366 File Offset: 0x00023566
		// (set) Token: 0x06001362 RID: 4962 RVA: 0x0002536E File Offset: 0x0002356E
		public int LargeAudienceThreshold
		{
			get
			{
				return this.largeAudienceThresholdField;
			}
			set
			{
				this.largeAudienceThresholdField = value;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x00025377 File Offset: 0x00023577
		// (set) Token: 0x06001364 RID: 4964 RVA: 0x0002537F File Offset: 0x0002357F
		public bool ShowExternalRecipientCount
		{
			get
			{
				return this.showExternalRecipientCountField;
			}
			set
			{
				this.showExternalRecipientCountField = value;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x00025388 File Offset: 0x00023588
		// (set) Token: 0x06001366 RID: 4966 RVA: 0x00025390 File Offset: 0x00023590
		[XmlArrayItem("Domain", IsNullable = false)]
		public SmtpDomain[] InternalDomains
		{
			get
			{
				return this.internalDomainsField;
			}
			set
			{
				this.internalDomainsField = value;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x00025399 File Offset: 0x00023599
		// (set) Token: 0x06001368 RID: 4968 RVA: 0x000253A1 File Offset: 0x000235A1
		public bool PolicyTipsEnabled
		{
			get
			{
				return this.policyTipsEnabledField;
			}
			set
			{
				this.policyTipsEnabledField = value;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x000253AA File Offset: 0x000235AA
		// (set) Token: 0x0600136A RID: 4970 RVA: 0x000253B2 File Offset: 0x000235B2
		public int LargeAudienceCap
		{
			get
			{
				return this.largeAudienceCapField;
			}
			set
			{
				this.largeAudienceCapField = value;
			}
		}

		// Token: 0x04000D6A RID: 3434
		private bool mailTipsEnabledField;

		// Token: 0x04000D6B RID: 3435
		private int maxRecipientsPerGetMailTipsRequestField;

		// Token: 0x04000D6C RID: 3436
		private int maxMessageSizeField;

		// Token: 0x04000D6D RID: 3437
		private int largeAudienceThresholdField;

		// Token: 0x04000D6E RID: 3438
		private bool showExternalRecipientCountField;

		// Token: 0x04000D6F RID: 3439
		private SmtpDomain[] internalDomainsField;

		// Token: 0x04000D70 RID: 3440
		private bool policyTipsEnabledField;

		// Token: 0x04000D71 RID: 3441
		private int largeAudienceCapField;
	}
}
