using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009F3 RID: 2547
	internal sealed class ADServerMetrics
	{
		// Token: 0x06007629 RID: 30249 RVA: 0x0018568F File Offset: 0x0018388F
		public ADServerMetrics(ADServer dc)
		{
			this.ServerId = dc.Id;
			this.DnsHostName = dc.DnsHostName;
		}

		// Token: 0x0600762A RID: 30250 RVA: 0x001856AF File Offset: 0x001838AF
		public ADServerMetrics(string dnsHostName)
		{
			this.DnsHostName = dnsHostName;
		}

		// Token: 0x17002A50 RID: 10832
		// (get) Token: 0x0600762B RID: 30251 RVA: 0x001856BE File Offset: 0x001838BE
		// (set) Token: 0x0600762C RID: 30252 RVA: 0x001856C6 File Offset: 0x001838C6
		public ADObjectId ServerId { get; private set; }

		// Token: 0x17002A51 RID: 10833
		// (get) Token: 0x0600762D RID: 30253 RVA: 0x001856CF File Offset: 0x001838CF
		// (set) Token: 0x0600762E RID: 30254 RVA: 0x001856D7 File Offset: 0x001838D7
		public string DnsHostName { get; private set; }

		// Token: 0x17002A52 RID: 10834
		// (get) Token: 0x0600762F RID: 30255 RVA: 0x001856E0 File Offset: 0x001838E0
		// (set) Token: 0x06007630 RID: 30256 RVA: 0x001856E8 File Offset: 0x001838E8
		public bool IsSuitable { get; internal set; }

		// Token: 0x17002A53 RID: 10835
		// (get) Token: 0x06007631 RID: 30257 RVA: 0x001856F1 File Offset: 0x001838F1
		// (set) Token: 0x06007632 RID: 30258 RVA: 0x001856F9 File Offset: 0x001838F9
		public LocalizedString ErrorMessage { get; internal set; }

		// Token: 0x17002A54 RID: 10836
		// (get) Token: 0x06007633 RID: 30259 RVA: 0x00185702 File Offset: 0x00183902
		// (set) Token: 0x06007634 RID: 30260 RVA: 0x0018570A File Offset: 0x0018390A
		public bool IsSynchronized { get; internal set; }

		// Token: 0x17002A55 RID: 10837
		// (get) Token: 0x06007635 RID: 30261 RVA: 0x00185713 File Offset: 0x00183913
		// (set) Token: 0x06007636 RID: 30262 RVA: 0x0018571B File Offset: 0x0018391B
		public ExDateTime UpdateTime { get; private set; }

		// Token: 0x17002A56 RID: 10838
		// (get) Token: 0x06007637 RID: 30263 RVA: 0x00185724 File Offset: 0x00183924
		// (set) Token: 0x06007638 RID: 30264 RVA: 0x0018572C File Offset: 0x0018392C
		public long HighestUsn { get; private set; }

		// Token: 0x17002A57 RID: 10839
		// (get) Token: 0x06007639 RID: 30265 RVA: 0x00185735 File Offset: 0x00183935
		// (set) Token: 0x0600763A RID: 30266 RVA: 0x0018573D File Offset: 0x0018393D
		public double InjectionRate { get; private set; }

		// Token: 0x17002A58 RID: 10840
		// (get) Token: 0x0600763B RID: 30267 RVA: 0x00185746 File Offset: 0x00183946
		// (set) Token: 0x0600763C RID: 30268 RVA: 0x0018574E File Offset: 0x0018394E
		public long IncomingDebt { get; set; }

		// Token: 0x17002A59 RID: 10841
		// (get) Token: 0x0600763D RID: 30269 RVA: 0x00185757 File Offset: 0x00183957
		// (set) Token: 0x0600763E RID: 30270 RVA: 0x0018575F File Offset: 0x0018395F
		public long OutgoingDebt { get; set; }

		// Token: 0x17002A5A RID: 10842
		// (get) Token: 0x0600763F RID: 30271 RVA: 0x00185768 File Offset: 0x00183968
		// (set) Token: 0x06007640 RID: 30272 RVA: 0x00185770 File Offset: 0x00183970
		public int IncomingHealth { get; set; }

		// Token: 0x17002A5B RID: 10843
		// (get) Token: 0x06007641 RID: 30273 RVA: 0x00185779 File Offset: 0x00183979
		// (set) Token: 0x06007642 RID: 30274 RVA: 0x00185781 File Offset: 0x00183981
		public int OutgoingHealth { get; set; }

		// Token: 0x17002A5C RID: 10844
		// (get) Token: 0x06007643 RID: 30275 RVA: 0x0018578A File Offset: 0x0018398A
		// (set) Token: 0x06007644 RID: 30276 RVA: 0x00185792 File Offset: 0x00183992
		public ICollection<ADReplicationLinkMetrics> DomainReplicationMetrics { get; private set; }

		// Token: 0x17002A5D RID: 10845
		// (get) Token: 0x06007645 RID: 30277 RVA: 0x0018579B File Offset: 0x0018399B
		// (set) Token: 0x06007646 RID: 30278 RVA: 0x001857A3 File Offset: 0x001839A3
		public ICollection<ADReplicationLinkMetrics> ConfigReplicationMetrics { get; private set; }

		// Token: 0x06007647 RID: 30279 RVA: 0x001857AC File Offset: 0x001839AC
		internal void Initialize(ExDateTime updateTime, long highestUsn, ICollection<ADReplicationLinkMetrics> configReplicationMetrics, ICollection<ADReplicationLinkMetrics> domainReplicationMetrics)
		{
			if (configReplicationMetrics == null)
			{
				throw new ArgumentNullException("configReplicationMetrics");
			}
			if (domainReplicationMetrics == null)
			{
				throw new ArgumentNullException("domainReplicationMetrics");
			}
			if (highestUsn <= 0L)
			{
				throw new ArgumentException(null, "highestUsn");
			}
			this.UpdateTime = updateTime;
			this.HighestUsn = highestUsn;
			this.ConfigReplicationMetrics = configReplicationMetrics;
			this.DomainReplicationMetrics = domainReplicationMetrics;
		}

		// Token: 0x06007648 RID: 30280 RVA: 0x00185804 File Offset: 0x00183A04
		internal void SetInjectionRate(ADServerMetrics previousMetrics)
		{
			if (!this.IsSuitable)
			{
				throw new InvalidOperationException();
			}
			if (previousMetrics != null && previousMetrics.IsSuitable)
			{
				this.InjectionRate = (double)(this.HighestUsn - previousMetrics.HighestUsn) / (this.UpdateTime - previousMetrics.UpdateTime).TotalSeconds;
			}
			else
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "[ADServerMetrics::SetInjectionRate] Could not calculate actual injection rate for {0} because there no previous data for this DC.", this.DnsHostName);
				this.InjectionRate = (double)this.HighestUsn;
			}
			ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<string, double>((long)this.GetHashCode(), "[ADServerMetrics::SetInjectionRate] {0}: InjectionRate={1}.", this.DnsHostName, this.InjectionRate);
		}
	}
}
