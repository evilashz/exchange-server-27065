using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008B5 RID: 2229
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class FiltersRequestTypeFilter
	{
		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06002FD7 RID: 12247 RVA: 0x0006C8FD File Offset: 0x0006AAFD
		// (set) Token: 0x06002FD8 RID: 12248 RVA: 0x0006C905 File Offset: 0x0006AB05
		public int ExecutionOrder
		{
			get
			{
				return this.executionOrderField;
			}
			set
			{
				this.executionOrderField = value;
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06002FD9 RID: 12249 RVA: 0x0006C90E File Offset: 0x0006AB0E
		// (set) Token: 0x06002FDA RID: 12250 RVA: 0x0006C916 File Offset: 0x0006AB16
		public byte Enabled
		{
			get
			{
				return this.enabledField;
			}
			set
			{
				this.enabledField = value;
			}
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06002FDB RID: 12251 RVA: 0x0006C91F File Offset: 0x0006AB1F
		// (set) Token: 0x06002FDC RID: 12252 RVA: 0x0006C927 File Offset: 0x0006AB27
		public RunWhenType RunWhen
		{
			get
			{
				return this.runWhenField;
			}
			set
			{
				this.runWhenField = value;
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06002FDD RID: 12253 RVA: 0x0006C930 File Offset: 0x0006AB30
		// (set) Token: 0x06002FDE RID: 12254 RVA: 0x0006C938 File Offset: 0x0006AB38
		public FiltersRequestTypeFilterCondition Condition
		{
			get
			{
				return this.conditionField;
			}
			set
			{
				this.conditionField = value;
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06002FDF RID: 12255 RVA: 0x0006C941 File Offset: 0x0006AB41
		// (set) Token: 0x06002FE0 RID: 12256 RVA: 0x0006C949 File Offset: 0x0006AB49
		public FiltersRequestTypeFilterActions Actions
		{
			get
			{
				return this.actionsField;
			}
			set
			{
				this.actionsField = value;
			}
		}

		// Token: 0x04002954 RID: 10580
		private int executionOrderField;

		// Token: 0x04002955 RID: 10581
		private byte enabledField;

		// Token: 0x04002956 RID: 10582
		private RunWhenType runWhenField;

		// Token: 0x04002957 RID: 10583
		private FiltersRequestTypeFilterCondition conditionField;

		// Token: 0x04002958 RID: 10584
		private FiltersRequestTypeFilterActions actionsField;
	}
}
