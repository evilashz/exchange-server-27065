using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008E2 RID: 2274
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class OptionsTypeVacationResponse
	{
		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x060030E2 RID: 12514 RVA: 0x0007315F File Offset: 0x0007135F
		// (set) Token: 0x060030E3 RID: 12515 RVA: 0x00073167 File Offset: 0x00071367
		public VacationResponseMode Mode
		{
			get
			{
				return this.modeField;
			}
			set
			{
				this.modeField = value;
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x060030E4 RID: 12516 RVA: 0x00073170 File Offset: 0x00071370
		// (set) Token: 0x060030E5 RID: 12517 RVA: 0x00073178 File Offset: 0x00071378
		public string StartTime
		{
			get
			{
				return this.startTimeField;
			}
			set
			{
				this.startTimeField = value;
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x060030E6 RID: 12518 RVA: 0x00073181 File Offset: 0x00071381
		// (set) Token: 0x060030E7 RID: 12519 RVA: 0x00073189 File Offset: 0x00071389
		public string EndTime
		{
			get
			{
				return this.endTimeField;
			}
			set
			{
				this.endTimeField = value;
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x060030E8 RID: 12520 RVA: 0x00073192 File Offset: 0x00071392
		// (set) Token: 0x060030E9 RID: 12521 RVA: 0x0007319A File Offset: 0x0007139A
		public string Message
		{
			get
			{
				return this.messageField;
			}
			set
			{
				this.messageField = value;
			}
		}

		// Token: 0x04002A3F RID: 10815
		private VacationResponseMode modeField;

		// Token: 0x04002A40 RID: 10816
		private string startTimeField;

		// Token: 0x04002A41 RID: 10817
		private string endTimeField;

		// Token: 0x04002A42 RID: 10818
		private string messageField;
	}
}
