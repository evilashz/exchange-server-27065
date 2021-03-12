using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000144 RID: 324
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class OccurrenceInfoType
	{
		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x00022778 File Offset: 0x00020978
		// (set) Token: 0x06000E31 RID: 3633 RVA: 0x00022780 File Offset: 0x00020980
		public ItemIdType ItemId
		{
			get
			{
				return this.itemIdField;
			}
			set
			{
				this.itemIdField = value;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x00022789 File Offset: 0x00020989
		// (set) Token: 0x06000E33 RID: 3635 RVA: 0x00022791 File Offset: 0x00020991
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

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x0002279A File Offset: 0x0002099A
		// (set) Token: 0x06000E35 RID: 3637 RVA: 0x000227A2 File Offset: 0x000209A2
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

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x000227AB File Offset: 0x000209AB
		// (set) Token: 0x06000E37 RID: 3639 RVA: 0x000227B3 File Offset: 0x000209B3
		public DateTime OriginalStart
		{
			get
			{
				return this.originalStartField;
			}
			set
			{
				this.originalStartField = value;
			}
		}

		// Token: 0x040009CD RID: 2509
		private ItemIdType itemIdField;

		// Token: 0x040009CE RID: 2510
		private DateTime startField;

		// Token: 0x040009CF RID: 2511
		private DateTime endField;

		// Token: 0x040009D0 RID: 2512
		private DateTime originalStartField;
	}
}
