using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008DC RID: 2268
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "HMSETTINGS:")]
	[Serializable]
	public class OptionsType
	{
		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x060030CB RID: 12491 RVA: 0x0007309C File Offset: 0x0007129C
		// (set) Token: 0x060030CC RID: 12492 RVA: 0x000730A4 File Offset: 0x000712A4
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

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x060030CD RID: 12493 RVA: 0x000730AD File Offset: 0x000712AD
		// (set) Token: 0x060030CE RID: 12494 RVA: 0x000730B5 File Offset: 0x000712B5
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

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x060030CF RID: 12495 RVA: 0x000730BE File Offset: 0x000712BE
		// (set) Token: 0x060030D0 RID: 12496 RVA: 0x000730C6 File Offset: 0x000712C6
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

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x060030D1 RID: 12497 RVA: 0x000730CF File Offset: 0x000712CF
		// (set) Token: 0x060030D2 RID: 12498 RVA: 0x000730D7 File Offset: 0x000712D7
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

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x060030D3 RID: 12499 RVA: 0x000730E0 File Offset: 0x000712E0
		// (set) Token: 0x060030D4 RID: 12500 RVA: 0x000730E8 File Offset: 0x000712E8
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

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x060030D5 RID: 12501 RVA: 0x000730F1 File Offset: 0x000712F1
		// (set) Token: 0x060030D6 RID: 12502 RVA: 0x000730F9 File Offset: 0x000712F9
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

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x060030D7 RID: 12503 RVA: 0x00073102 File Offset: 0x00071302
		// (set) Token: 0x060030D8 RID: 12504 RVA: 0x0007310A File Offset: 0x0007130A
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

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x060030D9 RID: 12505 RVA: 0x00073113 File Offset: 0x00071313
		// (set) Token: 0x060030DA RID: 12506 RVA: 0x0007311B File Offset: 0x0007131B
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

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x060030DB RID: 12507 RVA: 0x00073124 File Offset: 0x00071324
		// (set) Token: 0x060030DC RID: 12508 RVA: 0x0007312C File Offset: 0x0007132C
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

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x060030DD RID: 12509 RVA: 0x00073135 File Offset: 0x00071335
		// (set) Token: 0x060030DE RID: 12510 RVA: 0x0007313D File Offset: 0x0007133D
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

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x060030DF RID: 12511 RVA: 0x00073146 File Offset: 0x00071346
		// (set) Token: 0x060030E0 RID: 12512 RVA: 0x0007314E File Offset: 0x0007134E
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

		// Token: 0x04002A20 RID: 10784
		private byte confirmSentField;

		// Token: 0x04002A21 RID: 10785
		private HeaderDisplayType headerDisplayField;

		// Token: 0x04002A22 RID: 10786
		private IncludeOriginalInReplyType includeOriginalInReplyField;

		// Token: 0x04002A23 RID: 10787
		private JunkLevelType junkLevelField;

		// Token: 0x04002A24 RID: 10788
		private JunkMailDestinationType junkMailDestinationField;

		// Token: 0x04002A25 RID: 10789
		private ReplyIndicatorType replyIndicatorField;

		// Token: 0x04002A26 RID: 10790
		private string replyToAddressField;

		// Token: 0x04002A27 RID: 10791
		private byte saveSentMailField;

		// Token: 0x04002A28 RID: 10792
		private byte useReplyToAddressField;

		// Token: 0x04002A29 RID: 10793
		private OptionsTypeVacationResponse vacationResponseField;

		// Token: 0x04002A2A RID: 10794
		private OptionsTypeMailForwarding mailForwardingField;
	}
}
