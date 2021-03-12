using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000268 RID: 616
	public class ProvisioningValidationError : ValidationError
	{
		// Token: 0x060014A0 RID: 5280 RVA: 0x0004236F File Offset: 0x0004056F
		public ProvisioningValidationError(LocalizedString description, ExchangeErrorCategory errorCategory, Exception exception) : base(description)
		{
			this.errorCategory = errorCategory;
			this.exception = exception;
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x00042386 File Offset: 0x00040586
		public ProvisioningValidationError(LocalizedString description, ExchangeErrorCategory errorCategory) : this(description, errorCategory, null)
		{
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x00042391 File Offset: 0x00040591
		public ProvisioningValidationError(LocalizedString description, Exception exception) : this(description, ExchangeErrorCategory.Client, exception)
		{
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x000423A0 File Offset: 0x000405A0
		public ProvisioningValidationError(LocalizedString description) : this(description, null)
		{
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x000423AA File Offset: 0x000405AA
		// (set) Token: 0x060014A5 RID: 5285 RVA: 0x000423B2 File Offset: 0x000405B2
		internal string AgentName
		{
			get
			{
				return this.agentName;
			}
			set
			{
				this.agentName = value;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x000423BB File Offset: 0x000405BB
		internal ExchangeErrorCategory ErrorCategory
		{
			get
			{
				return this.errorCategory;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x000423C3 File Offset: 0x000405C3
		internal Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x04000C0D RID: 3085
		private string agentName;

		// Token: 0x04000C0E RID: 3086
		private ExchangeErrorCategory errorCategory;

		// Token: 0x04000C0F RID: 3087
		private Exception exception;
	}
}
