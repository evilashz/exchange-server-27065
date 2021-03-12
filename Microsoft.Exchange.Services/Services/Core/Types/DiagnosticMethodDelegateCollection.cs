using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000214 RID: 532
	internal sealed class DiagnosticMethodDelegateCollection
	{
		// Token: 0x06000DD1 RID: 3537 RVA: 0x00044900 File Offset: 0x00042B00
		private DiagnosticMethodDelegateCollection()
		{
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00044951 File Offset: 0x00042B51
		public static DiagnosticMethodDelegateCollection Singleton
		{
			get
			{
				return DiagnosticMethodDelegateCollection.singleton;
			}
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00044958 File Offset: 0x00042B58
		public DiagnosticMethodDelegate GetDelegate(string verb)
		{
			if (!this.delegateMapping.Member.ContainsKey(verb))
			{
				throw new InvalidRequestException();
			}
			DiagnosticMethodDelegate diagnosticMethodDelegate = this.delegateMapping.Member[verb];
			if (S2SRightsWrapper.AllowsTokenSerializationBy(CallContext.Current.EffectiveCaller.ClientSecurityContext))
			{
				return diagnosticMethodDelegate;
			}
			if (!this.IsMethodUsableInProduction(verb, diagnosticMethodDelegate))
			{
				throw new ServiceAccessDeniedException();
			}
			ExchangeRunspaceConfiguration exchangeRunspaceConfiguration = ExchangeRunspaceConfigurationCache.Singleton.Get(CallContext.Current.HttpContext.User.Identity, null, false);
			if (!exchangeRunspaceConfiguration.HasRoleOfType(RoleType.SupportDiagnostics))
			{
				throw new ServiceAccessDeniedException();
			}
			return diagnosticMethodDelegate;
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x000449EC File Offset: 0x00042BEC
		private bool IsMethodUsableInProduction(string verb, DiagnosticMethodDelegate diagnosticDelegate)
		{
			bool result;
			lock (this.instanceLock)
			{
				if (this.verbAuthorizedForSupportDiagnosticRoleCache.ContainsKey(verb))
				{
					result = this.verbAuthorizedForSupportDiagnosticRoleCache[verb];
				}
				else
				{
					DiagnosticStrideJustificationAttribute diagnosticStrideJustificationAttribute = Attribute.GetCustomAttribute(diagnosticDelegate.Method, typeof(DiagnosticStrideJustificationAttribute)) as DiagnosticStrideJustificationAttribute;
					bool flag2 = diagnosticStrideJustificationAttribute != null && diagnosticStrideJustificationAttribute.IsAuthorizedForSupportDiagnosticRoleUse;
					this.verbAuthorizedForSupportDiagnosticRoleCache[verb] = flag2;
					result = flag2;
				}
			}
			return result;
		}

		// Token: 0x04000AC4 RID: 2756
		private static DiagnosticMethodDelegateCollection singleton = new DiagnosticMethodDelegateCollection();

		// Token: 0x04000AC5 RID: 2757
		private object instanceLock = new object();

		// Token: 0x04000AC6 RID: 2758
		private LazyMember<Dictionary<string, DiagnosticMethodDelegate>> delegateMapping = new LazyMember<Dictionary<string, DiagnosticMethodDelegate>>(() => new Dictionary<string, DiagnosticMethodDelegate>
		{
			{
				"ClearSubscriptions",
				new DiagnosticMethodDelegate(SubscriptionDiagnosticMethods.ClearSubscriptions)
			},
			{
				"GetActiveSubscriptionIds",
				new DiagnosticMethodDelegate(SubscriptionDiagnosticMethods.GetActiveSubscriptionIds)
			},
			{
				"GetHangingSubscriptionConnections",
				new DiagnosticMethodDelegate(SubscriptionDiagnosticMethods.GetHangingSubscriptionConnections)
			},
			{
				"SetStreamingConnectionHeartbeatDefault",
				new DiagnosticMethodDelegate(SubscriptionDiagnosticMethods.SetStreamingConnectionHeartbeatDefault)
			},
			{
				"SetStreamingSubscriptionNewEventQueueSize",
				new DiagnosticMethodDelegate(SubscriptionDiagnosticMethods.SetStreamingSubscriptionNewEventQueueSize)
			},
			{
				"SetStreamingSubscriptionTimeToLiveDefault",
				new DiagnosticMethodDelegate(SubscriptionDiagnosticMethods.SetStreamingSubscriptionTimeToLiveDefault)
			},
			{
				"GetStreamingSubscriptionExpirationTime",
				new DiagnosticMethodDelegate(SubscriptionDiagnosticMethods.GetStreamingSubscriptionExpirationTime)
			},
			{
				"ClearExchangeRunspaceConfigurationCache",
				new DiagnosticMethodDelegate(CacheDiagnosticMethods.ClearExchangeRunspaceConfigurationCache)
			}
		});

		// Token: 0x04000AC7 RID: 2759
		private Dictionary<string, bool> verbAuthorizedForSupportDiagnosticRoleCache = new Dictionary<string, bool>();
	}
}
