using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000BA RID: 186
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class OccurrencesRangeType
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x0001FE39 File Offset: 0x0001E039
		// (set) Token: 0x06000950 RID: 2384 RVA: 0x0001FE41 File Offset: 0x0001E041
		[XmlAttribute]
		public DateTime Start
		{
			get
			{
				return this.startField;
			}
			set
			{
				this.startField = value;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x0001FE4A File Offset: 0x0001E04A
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x0001FE52 File Offset: 0x0001E052
		[XmlIgnore]
		public bool StartSpecified
		{
			get
			{
				return this.startFieldSpecified;
			}
			set
			{
				this.startFieldSpecified = value;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0001FE5B File Offset: 0x0001E05B
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x0001FE63 File Offset: 0x0001E063
		[XmlAttribute]
		public DateTime End
		{
			get
			{
				return this.endField;
			}
			set
			{
				this.endField = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x0001FE6C File Offset: 0x0001E06C
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x0001FE74 File Offset: 0x0001E074
		[XmlIgnore]
		public bool EndSpecified
		{
			get
			{
				return this.endFieldSpecified;
			}
			set
			{
				this.endFieldSpecified = value;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0001FE7D File Offset: 0x0001E07D
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x0001FE85 File Offset: 0x0001E085
		[XmlAttribute]
		public int Count
		{
			get
			{
				return this.countField;
			}
			set
			{
				this.countField = value;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x0001FE8E File Offset: 0x0001E08E
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x0001FE96 File Offset: 0x0001E096
		[XmlIgnore]
		public bool CountSpecified
		{
			get
			{
				return this.countFieldSpecified;
			}
			set
			{
				this.countFieldSpecified = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x0001FE9F File Offset: 0x0001E09F
		// (set) Token: 0x0600095C RID: 2396 RVA: 0x0001FEA7 File Offset: 0x0001E0A7
		[XmlAttribute]
		public bool CompareOriginalStartTime
		{
			get
			{
				return this.compareOriginalStartTimeField;
			}
			set
			{
				this.compareOriginalStartTimeField = value;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0001FEB0 File Offset: 0x0001E0B0
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x0001FEB8 File Offset: 0x0001E0B8
		[XmlIgnore]
		public bool CompareOriginalStartTimeSpecified
		{
			get
			{
				return this.compareOriginalStartTimeFieldSpecified;
			}
			set
			{
				this.compareOriginalStartTimeFieldSpecified = value;
			}
		}

		// Token: 0x0400055D RID: 1373
		private DateTime startField;

		// Token: 0x0400055E RID: 1374
		private bool startFieldSpecified;

		// Token: 0x0400055F RID: 1375
		private DateTime endField;

		// Token: 0x04000560 RID: 1376
		private bool endFieldSpecified;

		// Token: 0x04000561 RID: 1377
		private int countField;

		// Token: 0x04000562 RID: 1378
		private bool countFieldSpecified;

		// Token: 0x04000563 RID: 1379
		private bool compareOriginalStartTimeField;

		// Token: 0x04000564 RID: 1380
		private bool compareOriginalStartTimeFieldSpecified;
	}
}
