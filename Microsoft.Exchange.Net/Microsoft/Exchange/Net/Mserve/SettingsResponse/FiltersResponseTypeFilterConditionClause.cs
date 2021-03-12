using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008F6 RID: 2294
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[DebuggerStepThrough]
	[Serializable]
	public class FiltersResponseTypeFilterConditionClause
	{
		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06003173 RID: 12659 RVA: 0x00073627 File Offset: 0x00071827
		// (set) Token: 0x06003174 RID: 12660 RVA: 0x0007362F File Offset: 0x0007182F
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

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06003175 RID: 12661 RVA: 0x00073638 File Offset: 0x00071838
		// (set) Token: 0x06003176 RID: 12662 RVA: 0x00073640 File Offset: 0x00071840
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

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06003177 RID: 12663 RVA: 0x00073649 File Offset: 0x00071849
		// (set) Token: 0x06003178 RID: 12664 RVA: 0x00073651 File Offset: 0x00071851
		public StringWithVersionType Value
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

		// Token: 0x04002A8A RID: 10890
		private FilterKeyType fieldField;

		// Token: 0x04002A8B RID: 10891
		private FilterOperatorType operatorField;

		// Token: 0x04002A8C RID: 10892
		private StringWithVersionType valueField;
	}
}
