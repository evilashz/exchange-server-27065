using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x0200012A RID: 298
	internal class GlsCallerId
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x000382EC File Offset: 0x000364EC
		internal static GlsEnvironmentType GLSEnvironment
		{
			get
			{
				if (GlsCallerId.glsEnvironmentType == GlsEnvironmentType.NotDefined)
				{
					if (DatacenterRegistry.IsForefrontForOffice())
					{
						string forefrontRegion = DatacenterRegistry.GetForefrontRegion();
						if (forefrontRegion.StartsWith("CN", true, CultureInfo.InvariantCulture))
						{
							GlsCallerId.glsEnvironmentType = GlsEnvironmentType.Gallatin;
						}
						else
						{
							GlsCallerId.glsEnvironmentType = GlsEnvironmentType.Prod;
							if (forefrontRegion.StartsWith("EMEASIP", true, CultureInfo.InvariantCulture))
							{
								GlsCallerId.glsEnvironmentType = GlsEnvironmentType.FFO_PROD_EMEASIP;
							}
						}
					}
					else
					{
						GlsCallerId.glsEnvironmentType = RegistrySettings.ExchangeServerCurrentVersion.GlsEnvironmentType;
					}
				}
				return GlsCallerId.glsEnvironmentType;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00038358 File Offset: 0x00036558
		public static GlsCallerId ForwardSyncService
		{
			get
			{
				switch (GlsCallerId.GLSEnvironment)
				{
				case GlsEnvironmentType.Prod:
					return GlsCallerId.forwardSync_Prod;
				case GlsEnvironmentType.Gallatin:
					return GlsCallerId.forwardSync_CN;
				case GlsEnvironmentType.FFO_PROD_EMEASIP:
					return GlsCallerId.forwardSync_EMEASIP;
				default:
					throw new ArgumentException("GLS callerID not initialized because of invalid value in GlsCallerId.GLSEnvironment");
				}
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x000383A0 File Offset: 0x000365A0
		public static GlsCallerId Transport
		{
			get
			{
				switch (GlsCallerId.GLSEnvironment)
				{
				case GlsEnvironmentType.Prod:
				case GlsEnvironmentType.FFO_PROD_EMEASIP:
					return GlsCallerId.transport_Prod;
				case GlsEnvironmentType.Gallatin:
					return GlsCallerId.transport_CN;
				default:
					throw new ArgumentException("GLS callerID not initialized because of invalid value in GlsCallerId.GLSEnvironment");
				}
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x000383E0 File Offset: 0x000365E0
		public static GlsCallerId Exchange
		{
			get
			{
				switch (GlsCallerId.GLSEnvironment)
				{
				case GlsEnvironmentType.Prod:
				case GlsEnvironmentType.FFO_PROD_EMEASIP:
					return GlsCallerId.exchange_Prod;
				case GlsEnvironmentType.Gallatin:
					return GlsCallerId.exchange_CN;
				default:
					throw new ArgumentException("GLS callerID not initialized because of invalid value in GlsCallerId.GLSEnvironment");
				}
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x00038420 File Offset: 0x00036620
		public static GlsCallerId FFOAdminUI
		{
			get
			{
				switch (GlsCallerId.GLSEnvironment)
				{
				case GlsEnvironmentType.Prod:
				case GlsEnvironmentType.FFO_PROD_EMEASIP:
					return GlsCallerId.ffoAdminUI_Prod;
				case GlsEnvironmentType.Gallatin:
					return GlsCallerId.ffoAdminUI_CN;
				default:
					throw new ArgumentException("GLS callerID not initialized because of invalid value in GlsCallerId.GLSEnvironment");
				}
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00038460 File Offset: 0x00036660
		public static GlsCallerId MessageTrace
		{
			get
			{
				switch (GlsCallerId.GLSEnvironment)
				{
				case GlsEnvironmentType.Prod:
				case GlsEnvironmentType.FFO_PROD_EMEASIP:
					return GlsCallerId.messageTrace_Prod;
				case GlsEnvironmentType.Gallatin:
					return GlsCallerId.messageTrace_CN;
				default:
					throw new ArgumentException("GLS callerID not initialized because of invalid value in GlsCallerId.GLSEnvironment");
				}
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x000384A0 File Offset: 0x000366A0
		public static GlsCallerId FFOVersioning
		{
			get
			{
				switch (GlsCallerId.GLSEnvironment)
				{
				case GlsEnvironmentType.Prod:
					return GlsCallerId.ffoVersioning_Prod;
				case GlsEnvironmentType.Gallatin:
					return GlsCallerId.ffoVersioning_CN;
				case GlsEnvironmentType.FFO_PROD_EMEASIP:
					return GlsCallerId.ffoVersioning_EMEASIP;
				default:
					throw new ArgumentException("GLS callerID not initialized because of invalid value in GlsCallerId.GLSEnvironment");
				}
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x000384E8 File Offset: 0x000366E8
		public static GlsCallerId O365CE
		{
			get
			{
				GlsEnvironmentType glsenvironment = GlsCallerId.GLSEnvironment;
				if (glsenvironment == GlsEnvironmentType.Prod)
				{
					return GlsCallerId.o365ce;
				}
				throw new ArgumentException("GLS callerID not initialized because of invalid value in GlsCallerId.GLSEnvironment");
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0003850F File Offset: 0x0003670F
		public static GlsCallerId TestOnly
		{
			get
			{
				return GlsCallerId.testOnly;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x00038516 File Offset: 0x00036716
		public string CallerIdString
		{
			get
			{
				return this.callerIdString;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0003851E File Offset: 0x0003671E
		public Guid TrackingGuid
		{
			get
			{
				return this.trackingGuid;
			}
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00038526 File Offset: 0x00036726
		public static bool IsForwardSyncCallerID(GlsCallerId callerID)
		{
			return callerID == GlsCallerId.forwardSync_Prod || callerID == GlsCallerId.forwardSync_EMEASIP || callerID == GlsCallerId.forwardSync_CN;
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00038542 File Offset: 0x00036742
		private GlsCallerId(string callerIdString, Guid trackingGuid, Namespace defaultNamespace)
		{
			this.callerIdString = callerIdString;
			this.trackingGuid = trackingGuid;
			this.defaultNamespace = defaultNamespace;
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0003855F File Offset: 0x0003675F
		public Namespace DefaultNamespace
		{
			get
			{
				return this.defaultNamespace;
			}
		}

		// Token: 0x04000662 RID: 1634
		private const string FFOEMEASIP = "EMEASIP";

		// Token: 0x04000663 RID: 1635
		private static GlsEnvironmentType glsEnvironmentType = GlsEnvironmentType.NotDefined;

		// Token: 0x04000664 RID: 1636
		private static readonly GlsCallerId forwardSync_Prod = new GlsCallerId("ffoFSS1", new Guid("54b2df1c-9cc0-4933-99e7-5f6ab597bc86"), Namespace.Ffo);

		// Token: 0x04000665 RID: 1637
		private static readonly GlsCallerId forwardSync_CN = new GlsCallerId("ffoFSS1-CN", new Guid("54b2df1c-9cc0-4933-99e7-5f6ab597bc86"), Namespace.Ffo);

		// Token: 0x04000666 RID: 1638
		private static readonly GlsCallerId forwardSync_EMEASIP = new GlsCallerId("ffoFSS1-EMEASIP", new Guid("54b2df1c-9cc0-4933-99e7-5f6ab597bc86"), Namespace.Ffo);

		// Token: 0x04000667 RID: 1639
		private static readonly GlsCallerId ffoVersioning_Prod = new GlsCallerId("FFOVersioning", new Guid("ff8c4923-2a5f-49ed-b586-3a0960379326"), Namespace.Ffo);

		// Token: 0x04000668 RID: 1640
		private static readonly GlsCallerId ffoVersioning_CN = new GlsCallerId("FFOVersioning-CN", new Guid("ff8c4923-2a5f-49ed-b586-3a0960379326"), Namespace.Ffo);

		// Token: 0x04000669 RID: 1641
		private static readonly GlsCallerId ffoVersioning_EMEASIP = new GlsCallerId("FFOVersioning-EMEASIP", new Guid("ff8c4923-2a5f-49ed-b586-3a0960379326"), Namespace.Ffo);

		// Token: 0x0400066A RID: 1642
		private static readonly GlsCallerId transport_Prod = new GlsCallerId("Transport", new Guid("1b1c89c1-44c1-447a-b7dc-ad48a8b8e495"), Namespace.Ffo);

		// Token: 0x0400066B RID: 1643
		private static readonly GlsCallerId transport_CN = new GlsCallerId("Transport-CN", new Guid("1b1c89c1-44c1-447a-b7dc-ad48a8b8e495"), Namespace.Ffo);

		// Token: 0x0400066C RID: 1644
		private static readonly GlsCallerId exchange_Prod = new GlsCallerId("EXO", new Guid("423b8095-fe9f-4193-89e9-d08341d951ef"), Namespace.Exo);

		// Token: 0x0400066D RID: 1645
		private static readonly GlsCallerId exchange_CN = new GlsCallerId("EXO-CN", new Guid("423b8095-fe9f-4193-89e9-d08341d951ef"), Namespace.Exo);

		// Token: 0x0400066E RID: 1646
		private static readonly GlsCallerId messageTrace_Prod = new GlsCallerId("MTRT", new Guid("4EDA220A-88C2-42f4-ABD8-5CB6F1A826D8"), Namespace.Ffo);

		// Token: 0x0400066F RID: 1647
		private static readonly GlsCallerId messageTrace_CN = new GlsCallerId("MTRT-CN", new Guid("4EDA220A-88C2-42f4-ABD8-5CB6F1A826D8"), Namespace.Ffo);

		// Token: 0x04000670 RID: 1648
		private static readonly GlsCallerId ffoAdminUI_Prod = new GlsCallerId("FFOAdminUI", new Guid("72f9956a-cb04-4a16-a149-0f382eb044e4"), Namespace.Ffo);

		// Token: 0x04000671 RID: 1649
		private static readonly GlsCallerId ffoAdminUI_CN = new GlsCallerId("FFOAdminUI-CN", new Guid("72f9956a-cb04-4a16-a149-0f382eb044e4"), Namespace.Ffo);

		// Token: 0x04000672 RID: 1650
		private static readonly GlsCallerId testOnly = new GlsCallerId("Lin Caller", new Guid("4d5b5b22-200e-4582-a4fe-3e44f5276b82"), Namespace.Ffo);

		// Token: 0x04000673 RID: 1651
		private static readonly GlsCallerId o365ce = new GlsCallerId("O365CE", new Guid("fa662ad4-faa2-4f7e-b3ab-2802549b88b8"), Namespace.Ffo);

		// Token: 0x04000674 RID: 1652
		private readonly string callerIdString;

		// Token: 0x04000675 RID: 1653
		private readonly Namespace defaultNamespace;

		// Token: 0x04000676 RID: 1654
		private readonly Guid trackingGuid;
	}
}
