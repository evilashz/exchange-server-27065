using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Compliance.Audit;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x02000095 RID: 149
	internal class AuditHealthHandler : ExchangeDiagnosableWrapper<AuditHealthInfo>
	{
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00017EB7 File Offset: 0x000160B7
		public static AuditHealthHandler Instance
		{
			get
			{
				return Singleton<AuditHealthHandler>.Instance;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x00017EBE File Offset: 0x000160BE
		public static AuditHealthInfo Health
		{
			get
			{
				return AuditHealthInfo.Instance;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00017EC5 File Offset: 0x000160C5
		protected override string ComponentName
		{
			get
			{
				return "AuditHealthInfo";
			}
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00017ECC File Offset: 0x000160CC
		internal override AuditHealthInfo GetExchangeDiagnosticsInfoData(DiagnosableParameters arguments)
		{
			return AuditHealthHandler.Health;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00017ED3 File Offset: 0x000160D3
		public static void Register()
		{
			if (!AuditHealthHandler.isRegistered)
			{
				AuditHealthHandler.isRegistered = true;
				ExchangeDiagnosticsHelper.RegisterDiagnosticsComponents(AuditHealthHandler.Instance);
			}
		}

		// Token: 0x040002DD RID: 733
		private static bool isRegistered;
	}
}
