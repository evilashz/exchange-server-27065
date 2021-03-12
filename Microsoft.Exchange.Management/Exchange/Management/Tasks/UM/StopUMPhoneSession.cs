using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMPhoneSession;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D48 RID: 3400
	[Cmdlet("Stop", "UMPhoneSession", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class StopUMPhoneSession : RemoveTenantADTaskBase<UMPhoneSessionIdentityParameter, UMPhoneSession>
	{
		// Token: 0x17002883 RID: 10371
		// (get) Token: 0x0600825F RID: 33375 RVA: 0x00215424 File Offset: 0x00213624
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageStopUMPhoneSession;
			}
		}

		// Token: 0x06008260 RID: 33376 RVA: 0x0021542B File Offset: 0x0021362B
		protected override IConfigDataProvider CreateSession()
		{
			return new UMPlayOnPhoneDataProvider(null, TypeOfPlayOnPhoneGreetingCall.Unknown);
		}
	}
}
