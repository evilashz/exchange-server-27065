using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000222 RID: 546
	internal class OverBudgetExceptionMapping : ExceptionMappingBase
	{
		// Token: 0x06000E20 RID: 3616 RVA: 0x000453C9 File Offset: 0x000435C9
		public OverBudgetExceptionMapping() : base(typeof(OverBudgetException), ExceptionMappingBase.Attributes.StopsBatchProcessing)
		{
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x000453DC File Offset: 0x000435DC
		protected override IDictionary<string, string> GetConstantValues(LocalizedException exception)
		{
			OverBudgetException ex = exception as OverBudgetException;
			EwsBudget.LogOverBudgetToIIS(ex);
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			if (this.IsMaxConcurrency(exception))
			{
				dictionary.Add("Policy", ex.PolicyPart);
				dictionary.Add("MaxConcurrencyLimit", ex.PolicyValue);
				dictionary.Add("ErrorMessage", ex.Message);
			}
			else if (ex.BackoffTime >= 0)
			{
				dictionary.Add("BackOffMilliseconds", ex.BackoffTime.ToString());
			}
			return dictionary;
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0004545D File Offset: 0x0004365D
		protected override ResponseCodeType GetResponseCode(LocalizedException exception)
		{
			if (this.UseMaxConcurrencyError(exception))
			{
				return ResponseCodeType.ErrorExceededConnectionCount;
			}
			return ResponseCodeType.ErrorServerBusy;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00045470 File Offset: 0x00043670
		protected override ExchangeVersion GetEffectiveVersion(LocalizedException exception)
		{
			if (this.GetResponseCode(exception) != ResponseCodeType.ErrorServerBusy)
			{
				return ExchangeVersion.Exchange2010;
			}
			return ExchangeVersion.Exchange2007;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0004548B File Offset: 0x0004368B
		protected override CoreResources.IDs GetResourceId(LocalizedException exception)
		{
			if (this.UseMaxConcurrencyError(exception))
			{
				return CoreResources.IDs.ErrorExceededConnectionCount;
			}
			return (CoreResources.IDs)3655513582U;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x000454A1 File Offset: 0x000436A1
		private bool UseMaxConcurrencyError(LocalizedException exception)
		{
			return this.IsMaxConcurrency(exception) && ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010);
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x000454C0 File Offset: 0x000436C0
		private bool IsMaxConcurrency(LocalizedException exception)
		{
			OverBudgetException ex = base.VerifyExceptionType<OverBudgetException>(exception);
			return ex.PolicyPart == "MaxConcurrency" || ex.PolicyPart == "MaxStreamingConcurrency";
		}

		// Token: 0x04000AED RID: 2797
		private const string BackOffMilliseconds = "BackOffMilliseconds";

		// Token: 0x04000AEE RID: 2798
		private const string MaxConcurrencyLimit = "MaxConcurrencyLimit";

		// Token: 0x04000AEF RID: 2799
		private const string Policy = "Policy";

		// Token: 0x04000AF0 RID: 2800
		private const string ErrorMessage = "ErrorMessage";
	}
}
