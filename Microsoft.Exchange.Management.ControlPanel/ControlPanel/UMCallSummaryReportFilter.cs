using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004B7 RID: 1207
	[DataContract]
	public class UMCallSummaryReportFilter : WebServiceParameters
	{
		// Token: 0x17002392 RID: 9106
		// (get) Token: 0x06003BB0 RID: 15280 RVA: 0x000B41EC File Offset: 0x000B23EC
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-UMCallSummaryReport";
			}
		}

		// Token: 0x17002393 RID: 9107
		// (get) Token: 0x06003BB1 RID: 15281 RVA: 0x000B41F3 File Offset: 0x000B23F3
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x17002394 RID: 9108
		// (get) Token: 0x06003BB2 RID: 15282 RVA: 0x000B41FA File Offset: 0x000B23FA
		// (set) Token: 0x06003BB3 RID: 15283 RVA: 0x000B420C File Offset: 0x000B240C
		[DataMember]
		public string GroupBy
		{
			get
			{
				return (string)base["GroupBy"];
			}
			set
			{
				base["GroupBy"] = value;
			}
		}

		// Token: 0x17002395 RID: 9109
		// (get) Token: 0x06003BB4 RID: 15284 RVA: 0x000B421A File Offset: 0x000B241A
		// (set) Token: 0x06003BB5 RID: 15285 RVA: 0x000B422C File Offset: 0x000B242C
		[DataMember]
		public string UMDialPlan
		{
			get
			{
				return (string)base["UMDialPlan"];
			}
			set
			{
				base["UMDialPlan"] = value;
			}
		}

		// Token: 0x17002396 RID: 9110
		// (get) Token: 0x06003BB6 RID: 15286 RVA: 0x000B423A File Offset: 0x000B243A
		// (set) Token: 0x06003BB7 RID: 15287 RVA: 0x000B424C File Offset: 0x000B244C
		[DataMember]
		public string UMIPGateway
		{
			get
			{
				return (string)base["UMIPGateway"];
			}
			set
			{
				base["UMIPGateway"] = value;
			}
		}

		// Token: 0x0400277A RID: 10106
		public const string RbacParameters = "?GroupBy";
	}
}
