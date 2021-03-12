using System;
using Microsoft.Exchange.Data;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000224 RID: 548
	public class SmtpDomainWithSubdomainsToDomainNameConverter : ValueToDisplayObjectConverter
	{
		// Token: 0x06001924 RID: 6436 RVA: 0x0006D7BC File Offset: 0x0006B9BC
		public object Convert(object valueObject)
		{
			SmtpDomainWithSubdomains smtpDomainWithSubdomains = (SmtpDomainWithSubdomains)valueObject;
			if (!SmtpDomainWithSubdomains.StarDomain.Equals(smtpDomainWithSubdomains))
			{
				return smtpDomainWithSubdomains.Domain;
			}
			return string.Empty;
		}
	}
}
