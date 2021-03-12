using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.LogAnalyzer.Analyzers.CertificateLog;
using Microsoft.Exchange.LogAnalyzer.Extensions.CertificateLog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Triggers
{
	// Token: 0x02000034 RID: 52
	public sealed class ExchangeCertificateLogTrigger : CertificateLogAnalyzer
	{
		// Token: 0x0600011D RID: 285 RVA: 0x000093BC File Offset: 0x000075BC
		public ExchangeCertificateLogTrigger(IJob job) : base(job)
		{
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000093C5 File Offset: 0x000075C5
		protected override bool ShouldValidateCertificate(CertificateInformation info)
		{
			return info.StoreName.Equals(StoreName.My.ToString());
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000093E0 File Offset: 0x000075E0
		protected override void ValidateExpiration(CertificateInformation info)
		{
			TimeSpan t = info.ValidTo.ToUniversalTime().Subtract(DateTime.UtcNow);
			string text = string.Format("The following certificate is expiring within {0} day(s).", Math.Ceiling(t.TotalDays));
			if (t <= this.ErrorDaysBeforeExpiry)
			{
				TriggerHandler.Trigger("SSLCertificateErrorEvent", new object[]
				{
					text,
					info.ToString(),
					info.ComponentOwner,
					base.GetType().Name
				});
				return;
			}
			if (t <= this.WarningDaysBeforeExpiry)
			{
				TriggerHandler.Trigger("SSLCertificateWarningEvent", new object[]
				{
					text,
					info.ToString(),
					info.ComponentOwner,
					base.GetType().Name
				});
			}
		}
	}
}
