using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200021F RID: 543
	internal class MailboxFailoverExceptionMapping : StaticExceptionMapping
	{
		// Token: 0x06000E17 RID: 3607 RVA: 0x00045259 File Offset: 0x00043459
		public MailboxFailoverExceptionMapping(Type exceptionType) : base(exceptionType, ExceptionMappingBase.Attributes.StopsBatchProcessing, ResponseCodeType.ErrorMailboxStoreUnavailable, CoreResources.IDs.ErrorMailboxStoreUnavailable)
		{
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0004526D File Offset: 0x0004346D
		protected override void DoServiceErrorPostProcessing(LocalizedException exception, ServiceError error)
		{
			MailboxFailoverExceptionMapping.WriteFailoverHeaders(exception);
			if (exception is MailboxInSiteFailoverException || exception is MailboxOfflineException)
			{
				error.IsTransient = true;
			}
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0004528C File Offset: 0x0004348C
		internal static void WriteFailoverHeaders(LocalizedException localizedException)
		{
			if (Global.WriteFailoverTypeHeader)
			{
				MailboxOfflineException ex = localizedException as MailboxOfflineException;
				if (ex != null)
				{
					EWSSettings.FailoverType = "Offline";
					return;
				}
				MailboxInSiteFailoverException ex2 = localizedException as MailboxInSiteFailoverException;
				if (ex2 != null)
				{
					EWSSettings.FailoverType = "In-Site";
					return;
				}
				MailboxCrossSiteFailoverException ex3 = localizedException as MailboxCrossSiteFailoverException;
				if (ex3 != null)
				{
					EWSSettings.FailoverType = "Cross-Site@" + ex3.DatabaseLocationInfo.ServerFqdn;
				}
			}
		}
	}
}
