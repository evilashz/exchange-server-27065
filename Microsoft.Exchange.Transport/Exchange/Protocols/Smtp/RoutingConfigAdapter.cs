using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004D0 RID: 1232
	internal class RoutingConfigAdapter : IRoutingConfigProvider
	{
		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x060038CE RID: 14542 RVA: 0x000E862E File Offset: 0x000E682E
		public bool CheckDagSelectorHeader
		{
			get
			{
				return this.appConfig.Routing.CheckDagSelectorHeader;
			}
		}

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x060038CF RID: 14543 RVA: 0x000E8640 File Offset: 0x000E6840
		public bool LocalLoopDetectionEnabled
		{
			get
			{
				return this.appConfig.Routing.LocalLoopDetectionEnabled;
			}
		}

		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x060038D0 RID: 14544 RVA: 0x000E8652 File Offset: 0x000E6852
		public int LocalLoopDetectionSubDomainLeftToRightOffsetForPerfCounter
		{
			get
			{
				return this.appConfig.Routing.LocalLoopDetectionSubDomainLeftToRightOffsetForPerfCounter;
			}
		}

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x060038D1 RID: 14545 RVA: 0x000E8664 File Offset: 0x000E6864
		public List<int> LocalLoopMessageDeferralIntervals
		{
			get
			{
				return this.appConfig.Routing.LocalLoopMessageDeferralIntervals.ToList<int>();
			}
		}

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x060038D2 RID: 14546 RVA: 0x000E867B File Offset: 0x000E687B
		public int LocalLoopSubdomainDepth
		{
			get
			{
				return this.appConfig.Routing.LocalLoopSubdomainDepth;
			}
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x060038D3 RID: 14547 RVA: 0x000E868D File Offset: 0x000E688D
		public int LoopDetectionNumberOfTransits
		{
			get
			{
				return this.appConfig.Routing.LoopDetectionNumberOfTransits;
			}
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x000E869F File Offset: 0x000E689F
		public static IRoutingConfigProvider Create(ITransportAppConfig appConfig)
		{
			ArgumentValidator.ThrowIfNull("appConfig", appConfig);
			return new RoutingConfigAdapter(appConfig);
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x000E86B2 File Offset: 0x000E68B2
		private RoutingConfigAdapter(ITransportAppConfig appConfig)
		{
			this.appConfig = appConfig;
		}

		// Token: 0x04001CF7 RID: 7415
		private readonly ITransportAppConfig appConfig;
	}
}
