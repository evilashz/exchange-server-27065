using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x0200091C RID: 2332
	internal class MRSProxyTask : SessionTask
	{
		// Token: 0x060052F1 RID: 21233 RVA: 0x00156B72 File Offset: 0x00154D72
		public MRSProxyTask() : base(HybridStrings.MRSProxyTaskName, 2)
		{
		}

		// Token: 0x060052F2 RID: 21234 RVA: 0x00156B85 File Offset: 0x00154D85
		public override bool CheckPrereqs(ITaskContext taskContext)
		{
			if (!base.CheckPrereqs(taskContext))
			{
				base.Logger.LogInformation(HybridStrings.HybridInfoTaskLogTemplate(base.Name, HybridStrings.HybridInfoBasePrereqsFailed));
				return false;
			}
			return true;
		}

		// Token: 0x060052F3 RID: 21235 RVA: 0x00156BB8 File Offset: 0x00154DB8
		public override bool NeedsConfiguration(ITaskContext taskContext)
		{
			bool flag = base.NeedsConfiguration(taskContext);
			return this.CheckOrVerifyConfiguration(taskContext, false) || flag;
		}

		// Token: 0x060052F4 RID: 21236 RVA: 0x00156BDC File Offset: 0x00154DDC
		public override bool Configure(ITaskContext taskContext)
		{
			if (!base.Configure(taskContext))
			{
				return false;
			}
			ADWebServicesVirtualDirectory[] vdirsToEnable = this.GetVdirsToEnable();
			foreach (ADWebServicesVirtualDirectory adwebServicesVirtualDirectory in vdirsToEnable)
			{
				base.OnPremisesSession.SetWebServicesVirtualDirectory(adwebServicesVirtualDirectory.DistinguishedName, true);
			}
			return true;
		}

		// Token: 0x060052F5 RID: 21237 RVA: 0x00156C22 File Offset: 0x00154E22
		public override bool ValidateConfiguration(ITaskContext taskContext)
		{
			return base.ValidateConfiguration(taskContext) && !this.CheckOrVerifyConfiguration(taskContext, true);
		}

		// Token: 0x060052F6 RID: 21238 RVA: 0x00156C3C File Offset: 0x00154E3C
		private bool CheckOrVerifyConfiguration(ITaskContext taskContext, bool fVerifyOnly)
		{
			ADWebServicesVirtualDirectory[] vdirsToEnable = this.GetVdirsToEnable();
			return vdirsToEnable.Length != 0;
		}

		// Token: 0x060052F7 RID: 21239 RVA: 0x00156C5C File Offset: 0x00154E5C
		private ADWebServicesVirtualDirectory[] GetVdirsToEnable()
		{
			ServerVersion b = new ServerVersion(14, 0, 100, 0);
			List<ADWebServicesVirtualDirectory> list = new List<ADWebServicesVirtualDirectory>(1);
			IEnumerable<ADWebServicesVirtualDirectory> webServicesVirtualDirectory = base.OnPremisesSession.GetWebServicesVirtualDirectory(null);
			foreach (ADWebServicesVirtualDirectory adwebServicesVirtualDirectory in webServicesVirtualDirectory)
			{
				if (ServerVersion.Compare(adwebServicesVirtualDirectory.AdminDisplayVersion, b) >= 0 && !adwebServicesVirtualDirectory.MRSProxyEnabled)
				{
					list.Add(adwebServicesVirtualDirectory);
				}
			}
			return list.ToArray();
		}
	}
}
