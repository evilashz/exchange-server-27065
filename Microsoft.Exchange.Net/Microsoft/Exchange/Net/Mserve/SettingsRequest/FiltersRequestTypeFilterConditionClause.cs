using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008B8 RID: 2232
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[DebuggerStepThrough]
	[Serializable]
	public class FiltersRequestTypeFilterConditionClause
	{
		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06002FE5 RID: 12261 RVA: 0x0006C973 File Offset: 0x0006AB73
		// (set) Token: 0x06002FE6 RID: 12262 RVA: 0x0006C97B File Offset: 0x0006AB7B
		public FilterKeyType Field
		{
			get
			{
				return this.fieldField;
			}
			set
			{
				this.fieldField = value;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06002FE7 RID: 12263 RVA: 0x0006C984 File Offset: 0x0006AB84
		// (set) Token: 0x06002FE8 RID: 12264 RVA: 0x0006C98C File Offset: 0x0006AB8C
		public FilterOperatorType Operator
		{
			get
			{
				return this.operatorField;
			}
			set
			{
				this.operatorField = value;
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06002FE9 RID: 12265 RVA: 0x0006C995 File Offset: 0x0006AB95
		// (set) Token: 0x06002FEA RID: 12266 RVA: 0x0006C99D File Offset: 0x0006AB9D
		public StringWithCharSetType Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x0400295D RID: 10589
		private FilterKeyType fieldField;

		// Token: 0x0400295E RID: 10590
		private FilterOperatorType operatorField;

		// Token: 0x0400295F RID: 10591
		private StringWithCharSetType valueField;
	}
}
