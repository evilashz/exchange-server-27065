using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000178 RID: 376
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class FlagType
	{
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x00023A96 File Offset: 0x00021C96
		// (set) Token: 0x06001073 RID: 4211 RVA: 0x00023A9E File Offset: 0x00021C9E
		public FlagStatusType FlagStatus
		{
			get
			{
				return this.flagStatusField;
			}
			set
			{
				this.flagStatusField = value;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x00023AA7 File Offset: 0x00021CA7
		// (set) Token: 0x06001075 RID: 4213 RVA: 0x00023AAF File Offset: 0x00021CAF
		public DateTime StartDate
		{
			get
			{
				return this.startDateField;
			}
			set
			{
				this.startDateField = value;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x00023AB8 File Offset: 0x00021CB8
		// (set) Token: 0x06001077 RID: 4215 RVA: 0x00023AC0 File Offset: 0x00021CC0
		[XmlIgnore]
		public bool StartDateSpecified
		{
			get
			{
				return this.startDateFieldSpecified;
			}
			set
			{
				this.startDateFieldSpecified = value;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x00023AC9 File Offset: 0x00021CC9
		// (set) Token: 0x06001079 RID: 4217 RVA: 0x00023AD1 File Offset: 0x00021CD1
		public DateTime DueDate
		{
			get
			{
				return this.dueDateField;
			}
			set
			{
				this.dueDateField = value;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x00023ADA File Offset: 0x00021CDA
		// (set) Token: 0x0600107B RID: 4219 RVA: 0x00023AE2 File Offset: 0x00021CE2
		[XmlIgnore]
		public bool DueDateSpecified
		{
			get
			{
				return this.dueDateFieldSpecified;
			}
			set
			{
				this.dueDateFieldSpecified = value;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x00023AEB File Offset: 0x00021CEB
		// (set) Token: 0x0600107D RID: 4221 RVA: 0x00023AF3 File Offset: 0x00021CF3
		public DateTime CompleteDate
		{
			get
			{
				return this.completeDateField;
			}
			set
			{
				this.completeDateField = value;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x00023AFC File Offset: 0x00021CFC
		// (set) Token: 0x0600107F RID: 4223 RVA: 0x00023B04 File Offset: 0x00021D04
		[XmlIgnore]
		public bool CompleteDateSpecified
		{
			get
			{
				return this.completeDateFieldSpecified;
			}
			set
			{
				this.completeDateFieldSpecified = value;
			}
		}

		// Token: 0x04000B3B RID: 2875
		private FlagStatusType flagStatusField;

		// Token: 0x04000B3C RID: 2876
		private DateTime startDateField;

		// Token: 0x04000B3D RID: 2877
		private bool startDateFieldSpecified;

		// Token: 0x04000B3E RID: 2878
		private DateTime dueDateField;

		// Token: 0x04000B3F RID: 2879
		private bool dueDateFieldSpecified;

		// Token: 0x04000B40 RID: 2880
		private DateTime completeDateField;

		// Token: 0x04000B41 RID: 2881
		private bool completeDateFieldSpecified;
	}
}
