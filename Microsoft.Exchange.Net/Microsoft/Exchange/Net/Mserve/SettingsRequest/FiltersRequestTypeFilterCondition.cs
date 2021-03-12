using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008B7 RID: 2231
	[DebuggerStepThrough]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[Serializable]
	public class FiltersRequestTypeFilterCondition
	{
		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06002FE2 RID: 12258 RVA: 0x0006C95A File Offset: 0x0006AB5A
		// (set) Token: 0x06002FE3 RID: 12259 RVA: 0x0006C962 File Offset: 0x0006AB62
		public FiltersRequestTypeFilterConditionClause Clause
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

		// Token: 0x0400295C RID: 10588
		private FiltersRequestTypeFilterConditionClause clauseField;
	}
}
