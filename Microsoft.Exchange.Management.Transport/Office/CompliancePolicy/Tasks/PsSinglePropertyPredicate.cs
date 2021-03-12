using System;
using System.Collections.Generic;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000CF RID: 207
	public abstract class PsSinglePropertyPredicate<T> : PsComplianceRulePredicateBase
	{
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000884 RID: 2180
		protected abstract string PropertyNameForEnginePredicate { get; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x00024877 File Offset: 0x00022A77
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x0002487F File Offset: 0x00022A7F
		public T PropertyValue { get; protected set; }

		// Token: 0x06000887 RID: 2183 RVA: 0x00024888 File Offset: 0x00022A88
		protected PsSinglePropertyPredicate(T value)
		{
			this.PropertyValue = value;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00024898 File Offset: 0x00022A98
		internal override PredicateCondition ToEnginePredicate()
		{
			Property property = new Property(this.PropertyNameForEnginePredicate, typeof(T));
			List<string> list = new List<string>();
			List<string> list2 = list;
			T propertyValue = this.PropertyValue;
			list2.Add(propertyValue.ToString());
			return new EqualPredicate(property, list);
		}
	}
}
