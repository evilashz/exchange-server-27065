using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000705 RID: 1797
	internal class ExchangeServerSchema : ADPresentationSchema
	{
		// Token: 0x0600548E RID: 21646 RVA: 0x0013206F File Offset: 0x0013026F
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ServerSchema>();
		}

		// Token: 0x040038C2 RID: 14530
		public static readonly ADPropertyDefinition DataPath = ServerSchema.DataPath;

		// Token: 0x040038C3 RID: 14531
		public static readonly ADPropertyDefinition Domain = ServerSchema.Domain;

		// Token: 0x040038C4 RID: 14532
		public static readonly ADPropertyDefinition Edition = ServerSchema.Edition;

		// Token: 0x040038C5 RID: 14533
		public static readonly ADPropertyDefinition ExchangeLegacyDN = ServerSchema.ExchangeLegacyDN;

		// Token: 0x040038C6 RID: 14534
		public static readonly ADPropertyDefinition ExchangeLegacyServerRole = ServerSchema.ExchangeLegacyServerRole;

		// Token: 0x040038C7 RID: 14535
		public static readonly ADPropertyDefinition Fqdn = ServerSchema.Fqdn;

		// Token: 0x040038C8 RID: 14536
		public static readonly ADPropertyDefinition IsHubTransportServer = ServerSchema.IsHubTransportServer;

		// Token: 0x040038C9 RID: 14537
		public static readonly ADPropertyDefinition IsClientAccessServer = ServerSchema.IsClientAccessServer;

		// Token: 0x040038CA RID: 14538
		public static readonly ADPropertyDefinition IsExchange2007OrLater = ServerSchema.IsExchange2007OrLater;

		// Token: 0x040038CB RID: 14539
		public static readonly ADPropertyDefinition IsEdgeServer = ServerSchema.IsEdgeServer;

		// Token: 0x040038CC RID: 14540
		public static readonly ADPropertyDefinition IsMailboxServer = ServerSchema.IsMailboxServer;

		// Token: 0x040038CD RID: 14541
		public static readonly ADPropertyDefinition IsProvisionedServer = ServerSchema.IsProvisionedServer;

		// Token: 0x040038CE RID: 14542
		public static readonly ADPropertyDefinition IsUnifiedMessagingServer = ServerSchema.IsUnifiedMessagingServer;

		// Token: 0x040038CF RID: 14543
		public static readonly ADPropertyDefinition IsCafeServer = ServerSchema.IsCafeServer;

		// Token: 0x040038D0 RID: 14544
		public static readonly ADPropertyDefinition IsFrontendTransportServer = ServerSchema.IsFrontendTransportServer;

		// Token: 0x040038D1 RID: 14545
		public static readonly ADPropertyDefinition NetworkAddress = ServerSchema.NetworkAddress;

		// Token: 0x040038D2 RID: 14546
		public static readonly ADPropertyDefinition OrganizationalUnit = ServerSchema.OrganizationalUnit;

		// Token: 0x040038D3 RID: 14547
		public static readonly ADPropertyDefinition CurrentServerRole = ServerSchema.CurrentServerRole;

		// Token: 0x040038D4 RID: 14548
		public static readonly ADPropertyDefinition AdminDisplayVersion = ServerSchema.AdminDisplayVersion;

		// Token: 0x040038D5 RID: 14549
		public static readonly ADPropertyDefinition ErrorReportingEnabled = ServerSchema.ErrorReportingEnabled;

		// Token: 0x040038D6 RID: 14550
		public static readonly ADPropertyDefinition StaticDomainControllers = ServerSchema.StaticDomainControllers;

		// Token: 0x040038D7 RID: 14551
		public static readonly ADPropertyDefinition StaticGlobalCatalogs = ServerSchema.StaticGlobalCatalogs;

		// Token: 0x040038D8 RID: 14552
		public static readonly ADPropertyDefinition StaticConfigDomainController = ServerSchema.StaticConfigDomainController;

		// Token: 0x040038D9 RID: 14553
		public static readonly ADPropertyDefinition StaticExcludedDomainControllers = ServerSchema.StaticExcludedDomainControllers;

		// Token: 0x040038DA RID: 14554
		public static readonly ADPropertyDefinition Site = ServerSchema.ServerSite;

		// Token: 0x040038DB RID: 14555
		public static readonly ADPropertyDefinition CurrentDomainControllers = ServerSchema.CurrentDomainControllers;

		// Token: 0x040038DC RID: 14556
		public static readonly ADPropertyDefinition CurrentGlobalCatalogs = ServerSchema.CurrentGlobalCatalogs;

		// Token: 0x040038DD RID: 14557
		public static readonly ADPropertyDefinition CurrentConfigDomainController = ServerSchema.CurrentConfigDomainController;

		// Token: 0x040038DE RID: 14558
		public static readonly ADPropertyDefinition ProductID = ServerSchema.ProductID;

		// Token: 0x040038DF RID: 14559
		public static readonly ADPropertyDefinition InternetWebProxy = ServerSchema.InternetWebProxy;

		// Token: 0x040038E0 RID: 14560
		public static readonly ADPropertyDefinition IsExchangeTrialEdition = ServerSchema.IsExchangeTrialEdition;

		// Token: 0x040038E1 RID: 14561
		public static readonly ADPropertyDefinition IsExpiredExchangeTrialEdition = ServerSchema.IsExpiredExchangeTrialEdition;

		// Token: 0x040038E2 RID: 14562
		public static readonly ADPropertyDefinition RemainingTrialPeriod = ServerSchema.RemainingTrialPeriod;

		// Token: 0x040038E3 RID: 14563
		public static readonly ADPropertyDefinition CustomerFeedbackEnabled = ServerSchema.CustomerFeedbackEnabled;

		// Token: 0x040038E4 RID: 14564
		public static readonly ADPropertyDefinition IsE14OrLater = ServerSchema.IsE14OrLater;

		// Token: 0x040038E5 RID: 14565
		public static readonly ADPropertyDefinition IsE15OrLater = ServerSchema.IsE15OrLater;

		// Token: 0x040038E6 RID: 14566
		public static readonly ADPropertyDefinition MonitoringGroup = ServerSchema.MonitoringGroup;
	}
}
