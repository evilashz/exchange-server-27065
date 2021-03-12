using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008C1 RID: 2241
	[DesignerCategory("code")]
	[XmlType(Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[Serializable]
	public class OptionsType
	{
		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06003003 RID: 12291 RVA: 0x0006CA6F File Offset: 0x0006AC6F
		// (set) Token: 0x06003004 RID: 12292 RVA: 0x0006CA77 File Offset: 0x0006AC77
		public byte ConfirmSent
		{
			get
			{
				return this.confirmSentField;
			}
			set
			{
				this.confirmSentField = value;
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06003005 RID: 12293 RVA: 0x0006CA80 File Offset: 0x0006AC80
		// (set) Token: 0x06003006 RID: 12294 RVA: 0x0006CA88 File Offset: 0x0006AC88
		public HeaderDisplayType HeaderDisplay
		{
			get
			{
				return this.headerDisplayField;
			}
			set
			{
				this.headerDisplayField = value;
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06003007 RID: 12295 RVA: 0x0006CA91 File Offset: 0x0006AC91
		// (set) Token: 0x06003008 RID: 12296 RVA: 0x0006CA99 File Offset: 0x0006AC99
		public IncludeOriginalInReplyType IncludeOriginalInReply
		{
			get
			{
				return this.includeOriginalInReplyField;
			}
			set
			{
				this.includeOriginalInReplyField = value;
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06003009 RID: 12297 RVA: 0x0006CAA2 File Offset: 0x0006ACA2
		// (set) Token: 0x0600300A RID: 12298 RVA: 0x0006CAAA File Offset: 0x0006ACAA
		public JunkLevelType JunkLevel
		{
			get
			{
				return this.junkLevelField;
			}
			set
			{
				this.junkLevelField = value;
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x0600300B RID: 12299 RVA: 0x0006CAB3 File Offset: 0x0006ACB3
		// (set) Token: 0x0600300C RID: 12300 RVA: 0x0006CABB File Offset: 0x0006ACBB
		public JunkMailDestinationType JunkMailDestination
		{
			get
			{
				return this.junkMailDestinationField;
			}
			set
			{
				this.junkMailDestinationField = value;
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x0600300D RID: 12301 RVA: 0x0006CAC4 File Offset: 0x0006ACC4
		// (set) Token: 0x0600300E RID: 12302 RVA: 0x0006CACC File Offset: 0x0006ACCC
		public ReplyIndicatorType ReplyIndicator
		{
			get
			{
				return this.replyIndicatorField;
			}
			set
			{
				this.replyIndicatorField = value;
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x0600300F RID: 12303 RVA: 0x0006CAD5 File Offset: 0x0006ACD5
		// (set) Token: 0x06003010 RID: 12304 RVA: 0x0006CADD File Offset: 0x0006ACDD
		public string ReplyToAddress
		{
			get
			{
				return this.replyToAddressField;
			}
			set
			{
				this.replyToAddressField = value;
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06003011 RID: 12305 RVA: 0x0006CAE6 File Offset: 0x0006ACE6
		// (set) Token: 0x06003012 RID: 12306 RVA: 0x0006CAEE File Offset: 0x0006ACEE
		public byte SaveSentMail
		{
			get
			{
				return this.saveSentMailField;
			}
			set
			{
				this.saveSentMailField = value;
			}
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06003013 RID: 12307 RVA: 0x0006CAF7 File Offset: 0x0006ACF7
		// (set) Token: 0x06003014 RID: 12308 RVA: 0x0006CAFF File Offset: 0x0006ACFF
		public byte UseReplyToAddress
		{
			get
			{
				return this.useReplyToAddressField;
			}
			set
			{
				this.useReplyToAddressField = value;
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06003015 RID: 12309 RVA: 0x0006CB08 File Offset: 0x0006AD08
		// (set) Token: 0x06003016 RID: 12310 RVA: 0x0006CB10 File Offset: 0x0006AD10
		public OptionsTypeVacationResponse VacationResponse
		{
			get
			{
				return this.vacationResponseField;
			}
			set
			{
				this.vacationResponseField = value;
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06003017 RID: 12311 RVA: 0x0006CB19 File Offset: 0x0006AD19
		// (set) Token: 0x06003018 RID: 12312 RVA: 0x0006CB21 File Offset: 0x0006AD21
		public OptionsTypeMailForwarding MailForwarding
		{
			get
			{
				return this.mailForwardingField;
			}
			set
			{
				this.mailForwardingField = value;
			}
		}

		// Token: 0x04002979 RID: 10617
		private byte confirmSentField;

		// Token: 0x0400297A RID: 10618
		private HeaderDisplayType headerDisplayField;

		// Token: 0x0400297B RID: 10619
		private IncludeOriginalInReplyType includeOriginalInReplyField;

		// Token: 0x0400297C RID: 10620
		private JunkLevelType junkLevelField;

		// Token: 0x0400297D RID: 10621
		private JunkMailDestinationType junkMailDestinationField;

		// Token: 0x0400297E RID: 10622
		private ReplyIndicatorType replyIndicatorField;

		// Token: 0x0400297F RID: 10623
		private string replyToAddressField;

		// Token: 0x04002980 RID: 10624
		private byte saveSentMailField;

		// Token: 0x04002981 RID: 10625
		private byte useReplyToAddressField;

		// Token: 0x04002982 RID: 10626
		private OptionsTypeVacationResponse vacationResponseField;

		// Token: 0x04002983 RID: 10627
		private OptionsTypeMailForwarding mailForwardingField;
	}
}
