using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000330 RID: 816
	[Serializable]
	public class ADDomain : ADNonExchangeObject
	{
		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x060025DB RID: 9691 RVA: 0x000A03B9 File Offset: 0x0009E5B9
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADDomain.schema;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x060025DC RID: 9692 RVA: 0x000A03C0 File Offset: 0x0009E5C0
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADDomain.MostDerivedClass;
			}
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x000A03D0 File Offset: 0x0009E5D0
		internal static object FqdnGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
			return NativeHelpers.CanonicalNameFromDistinguishedName(adobjectId.DistinguishedName);
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x060025DF RID: 9695 RVA: 0x000A03F9 File Offset: 0x0009E5F9
		public string Fqdn
		{
			get
			{
				return (string)this[ADDomainSchema.Fqdn];
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x060025E0 RID: 9696 RVA: 0x000A040B File Offset: 0x0009E60B
		public SecurityIdentifier Sid
		{
			get
			{
				return (SecurityIdentifier)this[ADDomainSchema.Sid];
			}
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000A0420 File Offset: 0x0009E620
		internal static object MaximumPasswordAgeGetter(IPropertyBag propertyBag)
		{
			long? num = (long?)propertyBag[ADDomainSchema.MaximumPasswordAgeRaw];
			if (num == null)
			{
				return null;
			}
			if (num.Value == -9223372036854775808L)
			{
				return EnhancedTimeSpan.Zero;
			}
			return new EnhancedTimeSpan?(EnhancedTimeSpan.FromTicks(-num.Value));
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x060025E2 RID: 9698 RVA: 0x000A048A File Offset: 0x0009E68A
		public long? MaximumPasswordAgeRaw
		{
			get
			{
				return (long?)this[ADDomainSchema.MaximumPasswordAgeRaw];
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060025E3 RID: 9699 RVA: 0x000A049C File Offset: 0x0009E69C
		public EnhancedTimeSpan? MaximumPasswordAge
		{
			get
			{
				return (EnhancedTimeSpan?)this[ADDomainSchema.MaximumPasswordAge];
			}
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x000A04B0 File Offset: 0x0009E6B0
		internal ReadOnlyCollection<ADServer> FindAllDomainControllers(bool includingRodc)
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.Session.DomainController, base.Session.ReadOnly, base.Session.ConsistencyMode, base.Session.NetworkCredential, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(Datacenter.IsMicrosoftHostedOnly(true) ? base.Id.GetPartitionId() : PartitionId.LocalForest), 215, "FindAllDomainControllers", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ADDomain.cs");
			topologyConfigurationSession.UseConfigNC = true;
			return topologyConfigurationSession.FindServerWithNtdsdsa(base.DistinguishedName, false, includingRodc);
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x000A0537 File Offset: 0x0009E737
		internal ReadOnlyCollection<ADServer> FindAllDomainControllers()
		{
			return this.FindAllDomainControllers(true);
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x000A06EC File Offset: 0x0009E8EC
		internal IEnumerable<ADServer> FindDomainControllersInSite(ADObjectId siteId)
		{
			foreach (ADServer adServer in this.FindAllDomainControllers())
			{
				if (adServer.Site.Equals(siteId))
				{
					yield return adServer;
				}
			}
			yield break;
		}

		// Token: 0x04001715 RID: 5909
		private static ADDomainSchema schema = ObjectSchema.GetInstance<ADDomainSchema>();

		// Token: 0x04001716 RID: 5910
		internal static string MostDerivedClass = "domainDNS";
	}
}
