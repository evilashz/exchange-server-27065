using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200012E RID: 302
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class AttendeeType
	{
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00022537 File Offset: 0x00020737
		// (set) Token: 0x06000DEC RID: 3564 RVA: 0x0002253F File Offset: 0x0002073F
		public EmailAddressType Mailbox
		{
			get
			{
				return this.mailboxField;
			}
			set
			{
				this.mailboxField = value;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x00022548 File Offset: 0x00020748
		// (set) Token: 0x06000DEE RID: 3566 RVA: 0x00022550 File Offset: 0x00020750
		public ResponseTypeType ResponseType
		{
			get
			{
				return this.responseTypeField;
			}
			set
			{
				this.responseTypeField = value;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x00022559 File Offset: 0x00020759
		// (set) Token: 0x06000DF0 RID: 3568 RVA: 0x00022561 File Offset: 0x00020761
		[XmlIgnore]
		public bool ResponseTypeSpecified
		{
			get
			{
				return this.responseTypeFieldSpecified;
			}
			set
			{
				this.responseTypeFieldSpecified = value;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x0002256A File Offset: 0x0002076A
		// (set) Token: 0x06000DF2 RID: 3570 RVA: 0x00022572 File Offset: 0x00020772
		public DateTime LastResponseTime
		{
			get
			{
				return this.lastResponseTimeField;
			}
			set
			{
				this.lastResponseTimeField = value;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x0002257B File Offset: 0x0002077B
		// (set) Token: 0x06000DF4 RID: 3572 RVA: 0x00022583 File Offset: 0x00020783
		[XmlIgnore]
		public bool LastResponseTimeSpecified
		{
			get
			{
				return this.lastResponseTimeFieldSpecified;
			}
			set
			{
				this.lastResponseTimeFieldSpecified = value;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0002258C File Offset: 0x0002078C
		// (set) Token: 0x06000DF6 RID: 3574 RVA: 0x00022594 File Offset: 0x00020794
		public DateTime ProposedStart
		{
			get
			{
				return this.proposedStartField;
			}
			set
			{
				this.proposedStartField = value;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x0002259D File Offset: 0x0002079D
		// (set) Token: 0x06000DF8 RID: 3576 RVA: 0x000225A5 File Offset: 0x000207A5
		[XmlIgnore]
		public bool ProposedStartSpecified
		{
			get
			{
				return this.proposedStartFieldSpecified;
			}
			set
			{
				this.proposedStartFieldSpecified = value;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x000225AE File Offset: 0x000207AE
		// (set) Token: 0x06000DFA RID: 3578 RVA: 0x000225B6 File Offset: 0x000207B6
		public DateTime ProposedEnd
		{
			get
			{
				return this.proposedEndField;
			}
			set
			{
				this.proposedEndField = value;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x000225BF File Offset: 0x000207BF
		// (set) Token: 0x06000DFC RID: 3580 RVA: 0x000225C7 File Offset: 0x000207C7
		[XmlIgnore]
		public bool ProposedEndSpecified
		{
			get
			{
				return this.proposedEndFieldSpecified;
			}
			set
			{
				this.proposedEndFieldSpecified = value;
			}
		}

		// Token: 0x04000996 RID: 2454
		private EmailAddressType mailboxField;

		// Token: 0x04000997 RID: 2455
		private ResponseTypeType responseTypeField;

		// Token: 0x04000998 RID: 2456
		private bool responseTypeFieldSpecified;

		// Token: 0x04000999 RID: 2457
		private DateTime lastResponseTimeField;

		// Token: 0x0400099A RID: 2458
		private bool lastResponseTimeFieldSpecified;

		// Token: 0x0400099B RID: 2459
		private DateTime proposedStartField;

		// Token: 0x0400099C RID: 2460
		private bool proposedStartFieldSpecified;

		// Token: 0x0400099D RID: 2461
		private DateTime proposedEndField;

		// Token: 0x0400099E RID: 2462
		private bool proposedEndFieldSpecified;
	}
}
