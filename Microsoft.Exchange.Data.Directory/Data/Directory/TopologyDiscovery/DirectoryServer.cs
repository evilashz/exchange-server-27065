using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService;

namespace Microsoft.Exchange.Data.Directory.TopologyDiscovery
{
	// Token: 0x020006C2 RID: 1730
	[DebuggerDisplay("{DnsName}-{suitableRoles}")]
	internal class DirectoryServer
	{
		// Token: 0x06004FCE RID: 20430 RVA: 0x00126680 File Offset: 0x00124880
		public DirectoryServer(ADServer server, NtdsDsa ntdsdsa)
		{
			if (server == null)
			{
				throw new ArgumentNullException("server");
			}
			if (ntdsdsa == null)
			{
				throw new ArgumentNullException("ntdsdsa");
			}
			if (!ntdsdsa.Id.Parent.Equals(server.Id))
			{
				throw new ArgumentException("ntdsdsa mismatch with server");
			}
			if (string.IsNullOrEmpty(server.DnsHostName))
			{
				throw new ArgumentException("server.DnsHostName null or empty");
			}
			this.server = server;
			this.isGC = (NtdsdsaOptions.IsGC == ntdsdsa.Options);
			this.SuitabilityResult = new SuitabilityCheckResult();
			this.suitableRoles = (ADServerRole.DomainController | ADServerRole.ConfigurationDomainController);
			if (this.IsGC)
			{
				this.suitableRoles |= ADServerRole.GlobalCatalog;
			}
			this.SuitabilityResult.IsEnabled = true;
			ADObjectId adobjectId = ntdsdsa.MasterNCs.Find((ADObjectId x) => x.DescendantDN(0).Equals(x));
			this.writableDomainNC = adobjectId;
		}

		// Token: 0x17001A39 RID: 6713
		// (get) Token: 0x06004FCF RID: 20431 RVA: 0x00126764 File Offset: 0x00124964
		public string DnsName
		{
			get
			{
				return this.server.DnsHostName;
			}
		}

		// Token: 0x17001A3A RID: 6714
		// (get) Token: 0x06004FD0 RID: 20432 RVA: 0x00126771 File Offset: 0x00124971
		public bool IsGC
		{
			get
			{
				return this.isGC;
			}
		}

		// Token: 0x17001A3B RID: 6715
		// (get) Token: 0x06004FD1 RID: 20433 RVA: 0x00126779 File Offset: 0x00124979
		public ADObjectId WritableDomainNC
		{
			get
			{
				return this.writableDomainNC;
			}
		}

		// Token: 0x17001A3C RID: 6716
		// (get) Token: 0x06004FD2 RID: 20434 RVA: 0x00126781 File Offset: 0x00124981
		public string Site
		{
			get
			{
				return this.server.Site.Name;
			}
		}

		// Token: 0x17001A3D RID: 6717
		// (get) Token: 0x06004FD3 RID: 20435 RVA: 0x00126793 File Offset: 0x00124993
		// (set) Token: 0x06004FD4 RID: 20436 RVA: 0x0012679B File Offset: 0x0012499B
		public SuitabilityCheckResult SuitabilityResult { get; private set; }

		// Token: 0x06004FD5 RID: 20437 RVA: 0x001267A4 File Offset: 0x001249A4
		public bool IsSuitableForRole(ADServerRole role)
		{
			bool flag = (this.suitableRoles & role) == role && this.SuitabilityResult.IsSuitable(role);
			ExTraceGlobals.TopologyTracer.TraceInformation<string, ADServerRole, bool>(this.GetHashCode(), (long)this.GetHashCode(), "{0} - IsSuitableForRole {1} returns {2}", this.DnsName, role, flag);
			return flag;
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x001267F4 File Offset: 0x001249F4
		public bool TryGetServerInfoForRole(ADServerRole role, out ServerInfo serverInfo, bool forestWideAffinityRequested = false)
		{
			serverInfo = null;
			bool flag = this.IsSuitableForRole(role);
			if (!forestWideAffinityRequested && !flag)
			{
				return false;
			}
			serverInfo = new ServerInfo(this.server.DnsHostName, this.server.Id.GetPartitionId().ForestFQDN, (role == ADServerRole.GlobalCatalog) ? 3268 : 389)
			{
				WritableNC = this.writableDomainNC.DistinguishedName,
				SiteName = this.server.Site.Name,
				ConfigNC = this.SuitabilityResult.ConfigNC,
				RootDomainNC = this.SuitabilityResult.RootNC,
				SchemaNC = this.SuitabilityResult.SchemaNC,
				IsServerSuitable = flag
			};
			return true;
		}

		// Token: 0x06004FD7 RID: 20439 RVA: 0x001268B8 File Offset: 0x00124AB8
		public void SetSuitabilityForRole(ADServerRole role, bool isSuitable)
		{
			ExTraceGlobals.TopologyTracer.TraceInformation<string, ADServerRole, bool>(this.GetHashCode(), (long)this.GetHashCode(), "{0} - SetSuitabilityForRole {1} {2}", this.DnsName, role, isSuitable);
			ADServerRole adserverRole;
			switch (role)
			{
			case ADServerRole.GlobalCatalog:
				if (!this.IsGC && isSuitable)
				{
					throw new NotSupportedException("Directory Server is not a GC, unable to set suitability role for GC.");
				}
				adserverRole = (isSuitable ? ADServerRole.GlobalCatalog : (ADServerRole.DomainController | ADServerRole.ConfigurationDomainController));
				goto IL_77;
			case ADServerRole.DomainController:
			case ADServerRole.ConfigurationDomainController:
				adserverRole = (isSuitable ? (ADServerRole.DomainController | ADServerRole.ConfigurationDomainController) : ADServerRole.GlobalCatalog);
				goto IL_77;
			}
			throw new NotSupportedException("Invalid Role Type");
			IL_77:
			if (isSuitable)
			{
				this.suitableRoles |= adserverRole;
				this.SuitabilityResult.IsReachableByTCPConnection |= adserverRole;
				return;
			}
			this.suitableRoles &= adserverRole;
			this.SuitabilityResult.IsReachableByTCPConnection &= adserverRole;
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x00126982 File Offset: 0x00124B82
		public bool HasAnySuitableRole()
		{
			return this.IsSuitableForRole(ADServerRole.DomainController) || this.IsSuitableForRole(ADServerRole.ConfigurationDomainController) || this.IsSuitableForRole(ADServerRole.GlobalCatalog);
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x001269A0 File Offset: 0x00124BA0
		public override string ToString()
		{
			string arg = string.Format("{0}{1}{2}", ((this.suitableRoles & ADServerRole.ConfigurationDomainController) == ADServerRole.ConfigurationDomainController) ? "C" : "-", ((this.suitableRoles & ADServerRole.DomainController) == ADServerRole.DomainController) ? "D" : "-", ((this.suitableRoles & ADServerRole.GlobalCatalog) == ADServerRole.GlobalCatalog) ? "G" : "-");
			return string.Format("{0}\t{1} {2}", this.DnsName, arg, this.SuitabilityResult.ToString());
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x00126A1C File Offset: 0x00124C1C
		internal void RefreshCounters(bool isDCInLocalSite)
		{
			ADProviderPerf.AddDCInstance(this.DnsName);
			ADProviderPerf.UpdateDCCounter(this.DnsName, Counter.DCLocalSite, UpdateType.Update, (uint)Convert.ToUInt16(isDCInLocalSite));
			ADProviderPerf.UpdateDCCounter(this.DnsName, Counter.DCStateReachability, UpdateType.Update, (uint)Convert.ToUInt16(this.SuitabilityResult.IsReachableByTCPConnection));
			ADProviderPerf.UpdateDCCounter(this.DnsName, Counter.DCStateSynchronized, UpdateType.Update, (uint)Convert.ToUInt16(this.SuitabilityResult.IsSynchronized));
			ADProviderPerf.UpdateDCCounter(this.DnsName, Counter.DCStateGCCapable, UpdateType.Update, (uint)Convert.ToUInt16(this.IsGC));
			ADProviderPerf.UpdateDCCounter(this.DnsName, Counter.DCStateIsPdc, UpdateType.Update, (uint)Convert.ToUInt16(this.SuitabilityResult.IsPDC));
			ADProviderPerf.UpdateDCCounter(this.DnsName, Counter.DCStateSaclRight, UpdateType.Update, (uint)Convert.ToUInt16(this.SuitabilityResult.IsSACLRightAvailable));
			ADProviderPerf.UpdateDCCounter(this.DnsName, Counter.DCStateCriticalData, UpdateType.Update, (uint)Convert.ToUInt16(this.SuitabilityResult.IsCriticalDataAvailable));
			ADProviderPerf.UpdateDCCounter(this.DnsName, Counter.DCStateNetlogon, UpdateType.Update, (uint)Convert.ToUInt16(this.SuitabilityResult.IsNetlogonAllowed));
			ADProviderPerf.UpdateDCCounter(this.DnsName, Counter.DCStateOsversion, UpdateType.Update, (uint)Convert.ToUInt16(this.SuitabilityResult.IsOSVersionSuitable));
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x00126B60 File Offset: 0x00124D60
		[Conditional("DEBUG")]
		private void CheckProcess()
		{
			string processName = Globals.ProcessName;
			if (!processName.Equals("Microsoft.Exchange.Directory.TopologyService.exe", StringComparison.OrdinalIgnoreCase) && !processName.Equals("PerseusStudio.exe", StringComparison.OrdinalIgnoreCase) && !processName.Equals("Internal.Exchange.TopologyDiscovery.exe", StringComparison.OrdinalIgnoreCase))
			{
				processName.Equals("PerseusHarnessRuntime.exe", StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x0400366C RID: 13932
		private readonly bool isGC;

		// Token: 0x0400366D RID: 13933
		private readonly ADObjectId writableDomainNC;

		// Token: 0x0400366E RID: 13934
		private ADServer server;

		// Token: 0x0400366F RID: 13935
		private ADServerRole suitableRoles;
	}
}
