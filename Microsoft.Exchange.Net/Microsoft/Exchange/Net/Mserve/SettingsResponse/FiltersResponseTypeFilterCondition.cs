using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008F5 RID: 2293
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class FiltersResponseTypeFilterCondition
	{
		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06003170 RID: 12656 RVA: 0x0007360E File Offset: 0x0007180E
		// (set) Token: 0x06003171 RID: 12657 RVA: 0x00073616 File Offset: 0x00071816
		public FiltersResponseTypeFilterConditionClause Clause
		{
			get
			{
				return this.clauseField;
			}
			set
			{
				this.clauseField = value;
			}
		}

		// Token: 0x04002A89 RID: 10889
		private FiltersResponseTypeFilterConditionClause clauseField;
	}
}
