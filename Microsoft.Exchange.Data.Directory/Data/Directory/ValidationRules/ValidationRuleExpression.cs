using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.ValidationRules
{
	// Token: 0x02000A34 RID: 2612
	internal abstract class ValidationRuleExpression
	{
		// Token: 0x06007810 RID: 30736 RVA: 0x0018B935 File Offset: 0x00189B35
		protected ValidationRuleExpression(string queryFilter, ObjectSchema schema, List<Type> applicableObjects)
		{
			if (string.IsNullOrEmpty(queryFilter))
			{
				throw new ArgumentNullException("queryFilter");
			}
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			this.QueryString = queryFilter;
			this.Schema = schema;
			this.ApplicableObjects = applicableObjects;
		}

		// Token: 0x17002ADA RID: 10970
		// (get) Token: 0x06007811 RID: 30737 RVA: 0x0018B973 File Offset: 0x00189B73
		// (set) Token: 0x06007812 RID: 30738 RVA: 0x0018B97B File Offset: 0x00189B7B
		public string QueryString { get; private set; }

		// Token: 0x17002ADB RID: 10971
		// (get) Token: 0x06007813 RID: 30739 RVA: 0x0018B984 File Offset: 0x00189B84
		// (set) Token: 0x06007814 RID: 30740 RVA: 0x0018B98C File Offset: 0x00189B8C
		public ObjectSchema Schema { get; private set; }

		// Token: 0x17002ADC RID: 10972
		// (get) Token: 0x06007815 RID: 30741 RVA: 0x0018B995 File Offset: 0x00189B95
		// (set) Token: 0x06007816 RID: 30742 RVA: 0x0018B99D File Offset: 0x00189B9D
		public ICollection<Type> ApplicableObjects { get; private set; }

		// Token: 0x17002ADD RID: 10973
		// (get) Token: 0x06007817 RID: 30743 RVA: 0x0018B9A6 File Offset: 0x00189BA6
		// (set) Token: 0x06007818 RID: 30744 RVA: 0x0018B9AE File Offset: 0x00189BAE
		public QueryFilter QueryFilter { get; internal set; }
	}
}
