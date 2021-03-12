using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000166 RID: 358
	public abstract class DDIValidateAttribute : DDIAttribute, IDDIValidator
	{
		// Token: 0x06002200 RID: 8704 RVA: 0x00066767 File Offset: 0x00064967
		public DDIValidateAttribute(string description) : base(description)
		{
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x00066770 File Offset: 0x00064970
		public virtual List<string> Validate(object target, Service profile)
		{
			return new List<string>();
		}

		// Token: 0x04001D57 RID: 7511
		public const string ArgumentKey_CodeBehind = "CodeBehind";

		// Token: 0x04001D58 RID: 7512
		public const string ArgumentKey_Xaml = "Xaml";

		// Token: 0x04001D59 RID: 7513
		public const string ArgumentKey_SchemaName = "SchemaName";
	}
}
