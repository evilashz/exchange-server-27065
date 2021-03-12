using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000EA RID: 234
	public abstract class RecipientDataSourceService : DataSourceService
	{
		// Token: 0x06001E75 RID: 7797 RVA: 0x0005BB13 File Offset: 0x00059D13
		protected new PowerShellResults<O> GetObject<O>(string getCmdlet, Identity identity)
		{
			return this.GetObject<O>(getCmdlet, identity, true);
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x0005BB1E File Offset: 0x00059D1E
		protected PowerShellResults<O> GetObject<O>(string getCmdlet, Identity identity, bool readFromDomainController)
		{
			if (readFromDomainController)
			{
				return base.GetObject<O>(new PSCommand().AddCommand(getCmdlet).AddParameter("ReadFromDomainController"), identity);
			}
			return base.GetObject<O>(new PSCommand().AddCommand(getCmdlet), identity);
		}
	}
}
