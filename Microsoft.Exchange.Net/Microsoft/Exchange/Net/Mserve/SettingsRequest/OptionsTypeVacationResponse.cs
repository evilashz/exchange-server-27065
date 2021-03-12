using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008C7 RID: 2247
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[Serializable]
	public class OptionsTypeVacationResponse
	{
		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x0600301A RID: 12314 RVA: 0x0006CB32 File Offset: 0x0006AD32
		// (set) Token: 0x0600301B RID: 12315 RVA: 0x0006CB3A File Offset: 0x0006AD3A
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

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x0600301C RID: 12316 RVA: 0x0006CB43 File Offset: 0x0006AD43
		// (set) Token: 0x0600301D RID: 12317 RVA: 0x0006CB4B File Offset: 0x0006AD4B
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

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x0600301E RID: 12318 RVA: 0x0006CB54 File Offset: 0x0006AD54
		// (set) Token: 0x0600301F RID: 12319 RVA: 0x0006CB5C File Offset: 0x0006AD5C
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

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06003020 RID: 12320 RVA: 0x0006CB65 File Offset: 0x0006AD65
		// (set) Token: 0x06003021 RID: 12321 RVA: 0x0006CB6D File Offset: 0x0006AD6D
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

		// Token: 0x04002998 RID: 10648
		private VacationResponseMode modeField;

		// Token: 0x04002999 RID: 10649
		private string startTimeField;

		// Token: 0x0400299A RID: 10650
		private string endTimeField;

		// Token: 0x0400299B RID: 10651
		private string messageField;
	}
}
