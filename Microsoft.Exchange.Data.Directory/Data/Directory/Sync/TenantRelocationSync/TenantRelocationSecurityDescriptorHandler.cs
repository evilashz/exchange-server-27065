using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x020007F4 RID: 2036
	internal class TenantRelocationSecurityDescriptorHandler
	{
		// Token: 0x060064A0 RID: 25760 RVA: 0x0015CE78 File Offset: 0x0015B078
		public TenantRelocationSecurityDescriptorHandler(TenantRelocationSyncData syncConfigData, ITopologyConfigurationSession sourceSession, ITopologyConfigurationSession targetSession)
		{
			this.syncConfigData = syncConfigData;
			this.sourceSession = sourceSession;
			this.targetSession = targetSession;
			this.sourceConfigNC = this.sourceSession.GetConfigurationNamingContext();
			this.targetConfigNC = this.targetSession.GetConfigurationNamingContext();
			this.Initialize();
		}

		// Token: 0x060064A1 RID: 25761 RVA: 0x0015CEF0 File Offset: 0x0015B0F0
		public void ProcessSecurityDescriptor(ADObjectId sourceId, ADObjectId targetId, bool forceResetTargetSD)
		{
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "ProcessSecurityDescriptor: process object {0}.", sourceId.DistinguishedName);
			RawSecurityDescriptor rsd = TenantRelocationSecurityDescriptorHandler.ReadSecurityDescriptorWrapper(this.sourceSession, sourceId, sourceId.IsDescendantOf(this.sourceConfigNC));
			List<GenericAce> customizedAces = this.GetCustomizedAces(rsd);
			if (!forceResetTargetSD && customizedAces.Count == 0)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "ProcessSecurityDescriptor: no customized ACEs found on source found {0}, skip update.", sourceId.DistinguishedName);
				return;
			}
			bool useConfigNC = targetId.IsDescendantOf(this.targetConfigNC);
			RawSecurityDescriptor targetSd = TenantRelocationSecurityDescriptorHandler.ReadSecurityDescriptorWrapper(this.targetSession, targetId, useConfigNC);
			RawSecurityDescriptor sd = this.ApplyAcesToTargetSecurityDescriptor(targetSd, customizedAces);
			bool useConfigNC2 = this.targetSession.UseConfigNC;
			this.targetSession.UseConfigNC = useConfigNC;
			try
			{
				this.targetSession.SaveSecurityDescriptor(targetId, sd);
			}
			finally
			{
				this.targetSession.UseConfigNC = useConfigNC2;
			}
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "ProcessSecurityDescriptor: process done object {0}.", sourceId.DistinguishedName);
		}

		// Token: 0x060064A2 RID: 25762 RVA: 0x0015CFEC File Offset: 0x0015B1EC
		private static int GetRidFromSecurityIdentifier(SecurityIdentifier sid)
		{
			string text = sid.ToString();
			int num = text.LastIndexOf('-');
			return int.Parse(text.Substring(num + 1));
		}

		// Token: 0x060064A3 RID: 25763 RVA: 0x0015D01C File Offset: 0x0015B21C
		private static SecurityIdentifier GetSidFromAce(GenericAce ace)
		{
			KnownAce knownAce = ace as KnownAce;
			if (knownAce != null)
			{
				return knownAce.SecurityIdentifier;
			}
			return null;
		}

		// Token: 0x060064A4 RID: 25764 RVA: 0x0015D044 File Offset: 0x0015B244
		private static RawSecurityDescriptor ReadSecurityDescriptorWrapper(ITopologyConfigurationSession session, ADObjectId id, bool useConfigNC)
		{
			bool useConfigNC2 = session.UseConfigNC;
			session.UseConfigNC = useConfigNC;
			RawSecurityDescriptor result;
			try
			{
				result = session.ReadSecurityDescriptor(id);
			}
			finally
			{
				session.UseConfigNC = useConfigNC2;
			}
			return result;
		}

		// Token: 0x060064A5 RID: 25765 RVA: 0x0015D084 File Offset: 0x0015B284
		private List<GenericAce> GetCustomizedAces(RawSecurityDescriptor rsd)
		{
			List<GenericAce> list = new List<GenericAce>();
			foreach (GenericAce genericAce in rsd.DiscretionaryAcl)
			{
				if (!genericAce.IsInherited)
				{
					SecurityIdentifier sidFromAce = TenantRelocationSecurityDescriptorHandler.GetSidFromAce(genericAce);
					if (!(sidFromAce == null))
					{
						if (this.IsKnownGlobalPrincipal(sidFromAce))
						{
							ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "GetCustomizedAces: wellknown SID skipped {0}.", sidFromAce.ToString());
						}
						else
						{
							ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "GetCustomizedAces: customized SID found {0}.", sidFromAce.ToString());
							list.Add(genericAce);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060064A6 RID: 25766 RVA: 0x0015D118 File Offset: 0x0015B318
		private void Initialize()
		{
			ADDomain addomain = this.targetSession.Read<ADDomain>(this.syncConfigData.Target.PartitionRoot);
			this.targetDomainSid = addomain.Sid;
			this.wellKnownExchangeSecurityPrincipals = new HashSet<SecurityIdentifier>();
			ADObjectId childId = this.syncConfigData.Source.PartitionRoot.GetChildId("OU", "Microsoft Exchange Security Groups");
			ADRawEntry[] array = this.sourceSession.Find(childId, QueryScope.OneLevel, this.groupFilter, null, 0, this.groupProperties);
			foreach (ADRawEntry adrawEntry in array)
			{
				this.wellKnownExchangeSecurityPrincipals.Add((SecurityIdentifier)adrawEntry[ADMailboxRecipientSchema.Sid]);
			}
		}

		// Token: 0x060064A7 RID: 25767 RVA: 0x0015D1CC File Offset: 0x0015B3CC
		private RawSecurityDescriptor ApplyAcesToTargetSecurityDescriptor(RawSecurityDescriptor targetSd, List<GenericAce> sourceAces)
		{
			List<GenericAce> list = new List<GenericAce>();
			foreach (GenericAce genericAce in targetSd.DiscretionaryAcl)
			{
				SecurityIdentifier sidFromAce = TenantRelocationSecurityDescriptorHandler.GetSidFromAce(genericAce);
				if (!(sidFromAce == null))
				{
					SecurityIdentifier accountDomainSid = sidFromAce.AccountDomainSid;
					if (sidFromAce.IsAccountSid() && !accountDomainSid.Equals(this.targetDomainSid))
					{
						ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "ApplyAcesToTargetSecurityDescriptor: customized SID found {0} on target object, removed.", sidFromAce.ToString());
					}
					else
					{
						list.Add(genericAce);
					}
				}
			}
			RawAcl rawAcl = new RawAcl(targetSd.DiscretionaryAcl.Revision, list.Count + sourceAces.Count);
			int num = 0;
			foreach (GenericAce ace in list)
			{
				rawAcl.InsertAce(num++, ace);
			}
			foreach (GenericAce ace2 in sourceAces)
			{
				rawAcl.InsertAce(num++, ace2);
			}
			targetSd.DiscretionaryAcl = rawAcl;
			return targetSd;
		}

		// Token: 0x060064A8 RID: 25768 RVA: 0x0015D310 File Offset: 0x0015B510
		private bool IsKnownGlobalPrincipal(SecurityIdentifier sid)
		{
			int ridFromSecurityIdentifier = TenantRelocationSecurityDescriptorHandler.GetRidFromSecurityIdentifier(sid);
			return ridFromSecurityIdentifier <= 1000 || this.wellKnownExchangeSecurityPrincipals.Contains(sid);
		}

		// Token: 0x040042E1 RID: 17121
		private const int MaxRidOfAdDefaultPrincipals = 1000;

		// Token: 0x040042E2 RID: 17122
		private const string ExchangeGlobalGroupContainer = "Microsoft Exchange Security Groups";

		// Token: 0x040042E3 RID: 17123
		private SecurityIdentifier targetDomainSid;

		// Token: 0x040042E4 RID: 17124
		private ITopologyConfigurationSession sourceSession;

		// Token: 0x040042E5 RID: 17125
		private ITopologyConfigurationSession targetSession;

		// Token: 0x040042E6 RID: 17126
		private ADObjectId sourceConfigNC;

		// Token: 0x040042E7 RID: 17127
		private ADObjectId targetConfigNC;

		// Token: 0x040042E8 RID: 17128
		private HashSet<SecurityIdentifier> wellKnownExchangeSecurityPrincipals;

		// Token: 0x040042E9 RID: 17129
		private TenantRelocationSyncData syncConfigData;

		// Token: 0x040042EA RID: 17130
		private readonly QueryFilter groupFilter = ADObject.ObjectClassFilter(ADGroup.MostDerivedClass);

		// Token: 0x040042EB RID: 17131
		private PropertyDefinition[] groupProperties = new PropertyDefinition[]
		{
			ADMailboxRecipientSchema.Sid
		};
	}
}
