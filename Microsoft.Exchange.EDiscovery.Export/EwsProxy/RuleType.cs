using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001B2 RID: 434
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class RuleType
	{
		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x00024967 File Offset: 0x00022B67
		// (set) Token: 0x06001234 RID: 4660 RVA: 0x0002496F File Offset: 0x00022B6F
		public string RuleId
		{
			get
			{
				return this.ruleIdField;
			}
			set
			{
				this.ruleIdField = value;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x00024978 File Offset: 0x00022B78
		// (set) Token: 0x06001236 RID: 4662 RVA: 0x00024980 File Offset: 0x00022B80
		public string DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x00024989 File Offset: 0x00022B89
		// (set) Token: 0x06001238 RID: 4664 RVA: 0x00024991 File Offset: 0x00022B91
		public int Priority
		{
			get
			{
				return this.priorityField;
			}
			set
			{
				this.priorityField = value;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x0002499A File Offset: 0x00022B9A
		// (set) Token: 0x0600123A RID: 4666 RVA: 0x000249A2 File Offset: 0x00022BA2
		public bool IsEnabled
		{
			get
			{
				return this.isEnabledField;
			}
			set
			{
				this.isEnabledField = value;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x000249AB File Offset: 0x00022BAB
		// (set) Token: 0x0600123C RID: 4668 RVA: 0x000249B3 File Offset: 0x00022BB3
		public bool IsNotSupported
		{
			get
			{
				return this.isNotSupportedField;
			}
			set
			{
				this.isNotSupportedField = value;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x000249BC File Offset: 0x00022BBC
		// (set) Token: 0x0600123E RID: 4670 RVA: 0x000249C4 File Offset: 0x00022BC4
		[XmlIgnore]
		public bool IsNotSupportedSpecified
		{
			get
			{
				return this.isNotSupportedFieldSpecified;
			}
			set
			{
				this.isNotSupportedFieldSpecified = value;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x000249CD File Offset: 0x00022BCD
		// (set) Token: 0x06001240 RID: 4672 RVA: 0x000249D5 File Offset: 0x00022BD5
		public bool IsInError
		{
			get
			{
				return this.isInErrorField;
			}
			set
			{
				this.isInErrorField = value;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x000249DE File Offset: 0x00022BDE
		// (set) Token: 0x06001242 RID: 4674 RVA: 0x000249E6 File Offset: 0x00022BE6
		[XmlIgnore]
		public bool IsInErrorSpecified
		{
			get
			{
				return this.isInErrorFieldSpecified;
			}
			set
			{
				this.isInErrorFieldSpecified = value;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x000249EF File Offset: 0x00022BEF
		// (set) Token: 0x06001244 RID: 4676 RVA: 0x000249F7 File Offset: 0x00022BF7
		public RulePredicatesType Conditions
		{
			get
			{
				return this.conditionsField;
			}
			set
			{
				this.conditionsField = value;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001245 RID: 4677 RVA: 0x00024A00 File Offset: 0x00022C00
		// (set) Token: 0x06001246 RID: 4678 RVA: 0x00024A08 File Offset: 0x00022C08
		public RulePredicatesType Exceptions
		{
			get
			{
				return this.exceptionsField;
			}
			set
			{
				this.exceptionsField = value;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x00024A11 File Offset: 0x00022C11
		// (set) Token: 0x06001248 RID: 4680 RVA: 0x00024A19 File Offset: 0x00022C19
		public RuleActionsType Actions
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

		// Token: 0x04000CD2 RID: 3282
		private string ruleIdField;

		// Token: 0x04000CD3 RID: 3283
		private string displayNameField;

		// Token: 0x04000CD4 RID: 3284
		private int priorityField;

		// Token: 0x04000CD5 RID: 3285
		private bool isEnabledField;

		// Token: 0x04000CD6 RID: 3286
		private bool isNotSupportedField;

		// Token: 0x04000CD7 RID: 3287
		private bool isNotSupportedFieldSpecified;

		// Token: 0x04000CD8 RID: 3288
		private bool isInErrorField;

		// Token: 0x04000CD9 RID: 3289
		private bool isInErrorFieldSpecified;

		// Token: 0x04000CDA RID: 3290
		private RulePredicatesType conditionsField;

		// Token: 0x04000CDB RID: 3291
		private RulePredicatesType exceptionsField;

		// Token: 0x04000CDC RID: 3292
		private RuleActionsType actionsField;
	}
}
