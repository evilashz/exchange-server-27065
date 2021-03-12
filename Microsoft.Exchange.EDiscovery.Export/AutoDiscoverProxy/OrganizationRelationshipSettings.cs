using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x0200007D RID: 125
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class OrganizationRelationshipSettings
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0001F454 File Offset: 0x0001D654
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x0001F45C File Offset: 0x0001D65C
		public bool DeliveryReportEnabled
		{
			get
			{
				return this.deliveryReportEnabledField;
			}
			set
			{
				this.deliveryReportEnabledField = value;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0001F465 File Offset: 0x0001D665
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x0001F46D File Offset: 0x0001D66D
		[XmlArrayItem("Domain")]
		[XmlArray(IsNullable = true)]
		public string[] DomainNames
		{
			get
			{
				return this.domainNamesField;
			}
			set
			{
				this.domainNamesField = value;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0001F476 File Offset: 0x0001D676
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x0001F47E File Offset: 0x0001D67E
		public bool FreeBusyAccessEnabled
		{
			get
			{
				return this.freeBusyAccessEnabledField;
			}
			set
			{
				this.freeBusyAccessEnabledField = value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x0001F487 File Offset: 0x0001D687
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x0001F48F File Offset: 0x0001D68F
		[XmlElement(IsNullable = true)]
		public string FreeBusyAccessLevel
		{
			get
			{
				return this.freeBusyAccessLevelField;
			}
			set
			{
				this.freeBusyAccessLevelField = value;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x0001F498 File Offset: 0x0001D698
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x0001F4A0 File Offset: 0x0001D6A0
		public bool MailTipsAccessEnabled
		{
			get
			{
				return this.mailTipsAccessEnabledField;
			}
			set
			{
				this.mailTipsAccessEnabledField = value;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x0001F4A9 File Offset: 0x0001D6A9
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x0001F4B1 File Offset: 0x0001D6B1
		[XmlElement(IsNullable = true)]
		public string MailTipsAccessLevel
		{
			get
			{
				return this.mailTipsAccessLevelField;
			}
			set
			{
				this.mailTipsAccessLevelField = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x0001F4BA File Offset: 0x0001D6BA
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x0001F4C2 File Offset: 0x0001D6C2
		public bool MailboxMoveEnabled
		{
			get
			{
				return this.mailboxMoveEnabledField;
			}
			set
			{
				this.mailboxMoveEnabledField = value;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x0001F4CB File Offset: 0x0001D6CB
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x0001F4D3 File Offset: 0x0001D6D3
		[XmlElement(IsNullable = true)]
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x0001F4DC File Offset: 0x0001D6DC
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x0001F4E4 File Offset: 0x0001D6E4
		[XmlElement(DataType = "anyURI", IsNullable = true)]
		public string TargetApplicationUri
		{
			get
			{
				return this.targetApplicationUriField;
			}
			set
			{
				this.targetApplicationUriField = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x0001F4ED File Offset: 0x0001D6ED
		// (set) Token: 0x06000830 RID: 2096 RVA: 0x0001F4F5 File Offset: 0x0001D6F5
		[XmlElement(DataType = "anyURI", IsNullable = true)]
		public string TargetAutodiscoverEpr
		{
			get
			{
				return this.targetAutodiscoverEprField;
			}
			set
			{
				this.targetAutodiscoverEprField = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x0001F4FE File Offset: 0x0001D6FE
		// (set) Token: 0x06000832 RID: 2098 RVA: 0x0001F506 File Offset: 0x0001D706
		[XmlElement(DataType = "anyURI", IsNullable = true)]
		public string TargetSharingEpr
		{
			get
			{
				return this.targetSharingEprField;
			}
			set
			{
				this.targetSharingEprField = value;
			}
		}

		// Token: 0x040002FE RID: 766
		private bool deliveryReportEnabledField;

		// Token: 0x040002FF RID: 767
		private string[] domainNamesField;

		// Token: 0x04000300 RID: 768
		private bool freeBusyAccessEnabledField;

		// Token: 0x04000301 RID: 769
		private string freeBusyAccessLevelField;

		// Token: 0x04000302 RID: 770
		private bool mailTipsAccessEnabledField;

		// Token: 0x04000303 RID: 771
		private string mailTipsAccessLevelField;

		// Token: 0x04000304 RID: 772
		private bool mailboxMoveEnabledField;

		// Token: 0x04000305 RID: 773
		private string nameField;

		// Token: 0x04000306 RID: 774
		private string targetApplicationUriField;

		// Token: 0x04000307 RID: 775
		private string targetAutodiscoverEprField;

		// Token: 0x04000308 RID: 776
		private string targetSharingEprField;
	}
}
