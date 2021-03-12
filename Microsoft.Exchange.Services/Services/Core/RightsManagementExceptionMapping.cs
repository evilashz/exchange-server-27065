using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000227 RID: 551
	internal class RightsManagementExceptionMapping : ExceptionMappingBase
	{
		// Token: 0x06000E2F RID: 3631 RVA: 0x0004573B File Offset: 0x0004393B
		public RightsManagementExceptionMapping(Type exceptionType, ExchangeVersion effectiveVersion, ResponseCodeType responseCode) : base(exceptionType, ExceptionMappingBase.Attributes.None)
		{
			this.effectiveVersion = effectiveVersion;
			this.responseCode = responseCode;
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00045753 File Offset: 0x00043953
		public override LocalizedString GetLocalizedMessage(LocalizedException exception)
		{
			return exception.LocalizedString;
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0004575B File Offset: 0x0004395B
		protected override ResponseCodeType GetResponseCode(LocalizedException exception)
		{
			return this.responseCode;
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00045763 File Offset: 0x00043963
		protected override ExchangeVersion GetEffectiveVersion(LocalizedException exception)
		{
			return this.effectiveVersion;
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0004576B File Offset: 0x0004396B
		protected override CoreResources.IDs GetResourceId(LocalizedException exception)
		{
			return CoreResources.IDs.ErrorRightsManagementException;
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00045774 File Offset: 0x00043974
		protected override IDictionary<string, string> GetConstantValues(LocalizedException exception)
		{
			IDictionary<string, string> dictionary = null;
			Exception ex = null;
			if (exception != null)
			{
				ex = exception.InnerException;
			}
			if (ex != null)
			{
				ex = ex.InnerException;
			}
			if (ex != null)
			{
				COMException ex2 = ex as COMException;
				if (ex2 != null)
				{
					dictionary = new Dictionary<string, string>();
					dictionary.Add("HResult", ex2.HResult.ToString());
				}
			}
			return dictionary;
		}

		// Token: 0x04000AFB RID: 2811
		private const string HResult = "HResult";

		// Token: 0x04000AFC RID: 2812
		private readonly ExchangeVersion effectiveVersion;

		// Token: 0x04000AFD RID: 2813
		private readonly ResponseCodeType responseCode;
	}
}
