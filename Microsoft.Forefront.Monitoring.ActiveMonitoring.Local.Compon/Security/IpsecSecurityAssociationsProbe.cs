using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Hygiene.Deployment.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Security
{
	// Token: 0x0200020C RID: 524
	public class IpsecSecurityAssociationsProbe : ProbeWorkItem
	{
		// Token: 0x06000FFF RID: 4095 RVA: 0x0002B5F8 File Offset: 0x000297F8
		protected override void DoWork(CancellationToken cancellationToken)
		{
			this.logger = new ProbeWorkItemLogger(this, false, true);
			this.logger.LogMessage("IpsecSecurityAssociationsProbe started");
			this.logger.LogMessage("Verifying quick mode");
			this.VerifyAssociations("VerifyQuickModeSecurityAssociations", "QuickModeSecurityAssociationsMinCount", NetHelpers.GetQuickModeSecurityAssociations());
			this.logger.LogMessage("Verifying main mode");
			this.VerifyAssociations("VerifyMainModeSecurityAssociations", "MainModeSecurityAssociationsMinCount", NetHelpers.GetMainModeSecurityAssociations());
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0002B670 File Offset: 0x00029870
		private void VerifyAssociations(string attributeShouldVerify, string attributeMinCount, List<Dictionary<string, string>> associations)
		{
			bool flag = bool.Parse(ProbeHelper.GetExtensionAttribute(this.logger, this, attributeShouldVerify));
			int num = int.Parse(ProbeHelper.GetExtensionAttribute(this.logger, this, attributeMinCount));
			this.logger.LogMessage(string.Format("associations.Count:{0}", associations.Count));
			if (!flag)
			{
				this.logger.LogMessage("Skipping SA verification");
				return;
			}
			if (associations.Count < num)
			{
				throw new Exception(string.Format("SA count is {0} but should be at least {1}", associations.Count, num));
			}
			this.logger.LogMessage(string.Format("SA count is {0} which is greater than min {1}", associations.Count, num));
		}

		// Token: 0x040007B2 RID: 1970
		private IHygieneLogger logger = new NullHygieneLogger();
	}
}
