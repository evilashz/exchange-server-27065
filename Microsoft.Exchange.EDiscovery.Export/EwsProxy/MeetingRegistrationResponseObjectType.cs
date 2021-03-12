using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200011B RID: 283
	[XmlInclude(typeof(DeclineItemType))]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(AcceptItemType))]
	[XmlInclude(typeof(TentativelyAcceptItemType))]
	[Serializable]
	public class MeetingRegistrationResponseObjectType : WellKnownResponseObjectType
	{
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00021C8B File Offset: 0x0001FE8B
		// (set) Token: 0x06000CE6 RID: 3302 RVA: 0x00021C93 File Offset: 0x0001FE93
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

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x00021C9C File Offset: 0x0001FE9C
		// (set) Token: 0x06000CE8 RID: 3304 RVA: 0x00021CA4 File Offset: 0x0001FEA4
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

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00021CAD File Offset: 0x0001FEAD
		// (set) Token: 0x06000CEA RID: 3306 RVA: 0x00021CB5 File Offset: 0x0001FEB5
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

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x00021CBE File Offset: 0x0001FEBE
		// (set) Token: 0x06000CEC RID: 3308 RVA: 0x00021CC6 File Offset: 0x0001FEC6
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

		// Token: 0x040008FE RID: 2302
		private DateTime proposedStartField;

		// Token: 0x040008FF RID: 2303
		private bool proposedStartFieldSpecified;

		// Token: 0x04000900 RID: 2304
		private DateTime proposedEndField;

		// Token: 0x04000901 RID: 2305
		private bool proposedEndFieldSpecified;
	}
}
