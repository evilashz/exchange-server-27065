using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200026A RID: 618
	internal class ShadowRoutingContext : ProxyRoutingContext
	{
		// Token: 0x06001B13 RID: 6931 RVA: 0x0006F0EA File Offset: 0x0006D2EA
		public ShadowRoutingContext(RoutingTables routingTables, RoutingContextCore contextCore, ShadowRoutingConfiguration shadowRoutingConfiguration) : base(routingTables, contextCore)
		{
			this.shadowRoutingConfig = shadowRoutingConfiguration;
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001B14 RID: 6932 RVA: 0x0006F0FB File Offset: 0x0006D2FB
		public override int MaxRemoteSiteHubCount
		{
			get
			{
				return this.shadowRoutingConfig.RemoteShadowCount;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x0006F108 File Offset: 0x0006D308
		public override int MaxLocalSiteHubCount
		{
			get
			{
				return this.shadowRoutingConfig.LocalShadowCount;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001B16 RID: 6934 RVA: 0x0006F115 File Offset: 0x0006D315
		public override int MaxTotalHubCount
		{
			get
			{
				return this.shadowRoutingConfig.LocalShadowCount + this.shadowRoutingConfig.RemoteShadowCount;
			}
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x0006F130 File Offset: 0x0006D330
		public bool EvaluateDeliveryGroup(DeliveryGroup deliveryGroup)
		{
			bool flag = false;
			bool flag2 = false;
			if (deliveryGroup == null)
			{
				return false;
			}
			foreach (RoutingServerInfo serverInfo in deliveryGroup.AllServersNoFallback)
			{
				if (base.RoutingTables.ServerMap.IsInLocalSite(serverInfo))
				{
					flag2 = true;
				}
				else
				{
					flag = true;
				}
				if (flag2 && flag)
				{
					break;
				}
			}
			switch (this.shadowRoutingConfig.ShadowMessagePreference)
			{
			case ShadowMessagePreference.PreferRemote:
				return flag || flag2;
			case ShadowMessagePreference.LocalOnly:
				return flag2;
			case ShadowMessagePreference.RemoteOnly:
				return flag;
			default:
				return false;
			}
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x0006F1CC File Offset: 0x0006D3CC
		public override IEnumerable<RoutingServerInfo> GetDeliveryGroupServers(DeliveryGroup deliveryGroup, ProxyRoutingEnumeratorContext enumeratorContext)
		{
			RoutingUtils.ThrowIfNull(deliveryGroup, "deliveryGroup");
			RoutingUtils.ThrowIfNull(enumeratorContext, "enumeratorContext");
			return deliveryGroup.GetServersForShadowTarget(enumeratorContext, this.shadowRoutingConfig);
		}

		// Token: 0x04000CCB RID: 3275
		private ShadowRoutingConfiguration shadowRoutingConfig;
	}
}
