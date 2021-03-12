using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000292 RID: 658
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ValidatingCondition
	{
		// Token: 0x060017C9 RID: 6089 RVA: 0x00064805 File Offset: 0x00062A05
		public ValidatingCondition(ValidationDelegate validate, LocalizedString description, bool abortValidationIfFailed)
		{
			this.validate = validate;
			this.description = description;
			this.abortValidationIfFailed = abortValidationIfFailed;
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x00064822 File Offset: 0x00062A22
		public ValidationDelegate Validate
		{
			get
			{
				return this.validate;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x0006482A File Offset: 0x00062A2A
		public LocalizedString Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x00064832 File Offset: 0x00062A32
		public bool AbortValidationIfFailed
		{
			get
			{
				return this.abortValidationIfFailed;
			}
		}

		// Token: 0x04000A07 RID: 2567
		private ValidationDelegate validate;

		// Token: 0x04000A08 RID: 2568
		private LocalizedString description;

		// Token: 0x04000A09 RID: 2569
		private readonly bool abortValidationIfFailed;
	}
}
