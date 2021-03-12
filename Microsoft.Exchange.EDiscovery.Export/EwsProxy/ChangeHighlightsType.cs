using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200015F RID: 351
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ChangeHighlightsType
	{
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x0002352F File Offset: 0x0002172F
		// (set) Token: 0x06000FCF RID: 4047 RVA: 0x00023537 File Offset: 0x00021737
		public bool HasLocationChanged
		{
			get
			{
				return this.hasLocationChangedField;
			}
			set
			{
				this.hasLocationChangedField = value;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x00023540 File Offset: 0x00021740
		// (set) Token: 0x06000FD1 RID: 4049 RVA: 0x00023548 File Offset: 0x00021748
		[XmlIgnore]
		public bool HasLocationChangedSpecified
		{
			get
			{
				return this.hasLocationChangedFieldSpecified;
			}
			set
			{
				this.hasLocationChangedFieldSpecified = value;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x00023551 File Offset: 0x00021751
		// (set) Token: 0x06000FD3 RID: 4051 RVA: 0x00023559 File Offset: 0x00021759
		public string Location
		{
			get
			{
				return this.locationField;
			}
			set
			{
				this.locationField = value;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00023562 File Offset: 0x00021762
		// (set) Token: 0x06000FD5 RID: 4053 RVA: 0x0002356A File Offset: 0x0002176A
		public bool HasStartTimeChanged
		{
			get
			{
				return this.hasStartTimeChangedField;
			}
			set
			{
				this.hasStartTimeChangedField = value;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00023573 File Offset: 0x00021773
		// (set) Token: 0x06000FD7 RID: 4055 RVA: 0x0002357B File Offset: 0x0002177B
		[XmlIgnore]
		public bool HasStartTimeChangedSpecified
		{
			get
			{
				return this.hasStartTimeChangedFieldSpecified;
			}
			set
			{
				this.hasStartTimeChangedFieldSpecified = value;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00023584 File Offset: 0x00021784
		// (set) Token: 0x06000FD9 RID: 4057 RVA: 0x0002358C File Offset: 0x0002178C
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

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00023595 File Offset: 0x00021795
		// (set) Token: 0x06000FDB RID: 4059 RVA: 0x0002359D File Offset: 0x0002179D
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

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x000235A6 File Offset: 0x000217A6
		// (set) Token: 0x06000FDD RID: 4061 RVA: 0x000235AE File Offset: 0x000217AE
		public bool HasEndTimeChanged
		{
			get
			{
				return this.hasEndTimeChangedField;
			}
			set
			{
				this.hasEndTimeChangedField = value;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x000235B7 File Offset: 0x000217B7
		// (set) Token: 0x06000FDF RID: 4063 RVA: 0x000235BF File Offset: 0x000217BF
		[XmlIgnore]
		public bool HasEndTimeChangedSpecified
		{
			get
			{
				return this.hasEndTimeChangedFieldSpecified;
			}
			set
			{
				this.hasEndTimeChangedFieldSpecified = value;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x000235C8 File Offset: 0x000217C8
		// (set) Token: 0x06000FE1 RID: 4065 RVA: 0x000235D0 File Offset: 0x000217D0
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

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x000235D9 File Offset: 0x000217D9
		// (set) Token: 0x06000FE3 RID: 4067 RVA: 0x000235E1 File Offset: 0x000217E1
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

		// Token: 0x04000AE3 RID: 2787
		private bool hasLocationChangedField;

		// Token: 0x04000AE4 RID: 2788
		private bool hasLocationChangedFieldSpecified;

		// Token: 0x04000AE5 RID: 2789
		private string locationField;

		// Token: 0x04000AE6 RID: 2790
		private bool hasStartTimeChangedField;

		// Token: 0x04000AE7 RID: 2791
		private bool hasStartTimeChangedFieldSpecified;

		// Token: 0x04000AE8 RID: 2792
		private DateTime startField;

		// Token: 0x04000AE9 RID: 2793
		private bool startFieldSpecified;

		// Token: 0x04000AEA RID: 2794
		private bool hasEndTimeChangedField;

		// Token: 0x04000AEB RID: 2795
		private bool hasEndTimeChangedFieldSpecified;

		// Token: 0x04000AEC RID: 2796
		private DateTime endField;

		// Token: 0x04000AED RID: 2797
		private bool endFieldSpecified;
	}
}
