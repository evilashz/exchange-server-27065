using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.UM.UMPhoneSession;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D25 RID: 3365
	[Cmdlet("Get", "UMPhoneSession", SupportsShouldProcess = false, DefaultParameterSetName = "Identity")]
	public sealed class GetUMPhoneSession : GetTenantADObjectWithIdentityTaskBase<UMPhoneSessionIdentityParameter, UMPhoneSession>
	{
		// Token: 0x1700280A RID: 10250
		// (get) Token: 0x0600811A RID: 33050 RVA: 0x002105E0 File Offset: 0x0020E7E0
		// (set) Token: 0x0600811B RID: 33051 RVA: 0x002105F7 File Offset: 0x0020E7F7
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public new UMPhoneSessionIdentityParameter Identity
		{
			get
			{
				return (UMPhoneSessionIdentityParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x0600811C RID: 33052 RVA: 0x0021060A File Offset: 0x0020E80A
		protected override IConfigDataProvider CreateSession()
		{
			return new UMPlayOnPhoneDataProvider(null, TypeOfPlayOnPhoneGreetingCall.Unknown);
		}
	}
}
