using System;
using System.ServiceModel;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001EC RID: 492
	internal class OwaAnonymousApplication : BaseApplication
	{
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x00042DF2 File Offset: 0x00040FF2
		public override int MaxBreadcrumbs
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x00042DF5 File Offset: 0x00040FF5
		public override bool LogVerboseNotifications
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x00042DF8 File Offset: 0x00040FF8
		public override int ActivityBasedPresenceDuration
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x00042DFB File Offset: 0x00040FFB
		public override HttpClientCredentialType ServiceAuthenticationType
		{
			get
			{
				return HttpClientCredentialType.None;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x00042DFE File Offset: 0x00040FFE
		public override TroubleshootingContext TroubleshootingContext
		{
			get
			{
				return this.troubleshootingContext;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x00042E06 File Offset: 0x00041006
		public override bool LogErrorDetails
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x00042E09 File Offset: 0x00041009
		public override bool LogErrorTraces
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00042E0C File Offset: 0x0004100C
		internal override void UpdateErrorTracingConfiguration()
		{
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00042E0E File Offset: 0x0004100E
		internal override void Initialize()
		{
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00042E10 File Offset: 0x00041010
		protected override void InternalDispose()
		{
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00042E12 File Offset: 0x00041012
		protected override void ExecuteApplicationSpecificStart()
		{
			ErrorHandlerUtilities.RegisterForUnhandledExceptions();
		}

		// Token: 0x04000A48 RID: 2632
		private TroubleshootingContext troubleshootingContext = new TroubleshootingContext("OwaAnonymousServer");
	}
}
