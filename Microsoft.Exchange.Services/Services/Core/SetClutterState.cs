using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Clutter;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000373 RID: 883
	internal sealed class SetClutterState : SingleStepServiceCommand<SetClutterStateRequest, SetClutterStateResponse>
	{
		// Token: 0x060018C4 RID: 6340 RVA: 0x0008889E File Offset: 0x00086A9E
		public SetClutterState(CallContext callContext, SetClutterStateRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x000888A8 File Offset: 0x00086AA8
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return base.Result.Value;
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x000888B8 File Offset: 0x00086AB8
		internal override ServiceResult<SetClutterStateResponse> Execute()
		{
			MailboxSession mailboxSession = base.GetMailboxSession(base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
			switch (base.Request.Command)
			{
			case SetClutterStateCommand.EnableClutter:
				ClutterUtilities.OptUserIn(mailboxSession);
				break;
			case SetClutterStateCommand.DisableClutter:
				ClutterUtilities.OptUserOut(mailboxSession);
				break;
			default:
				throw new ArgumentException("Unsupported clutter request command: {0}".FormatWith(new object[]
				{
					base.Request.Command
				}));
			}
			SetClutterStateResponse value = new SetClutterStateResponse(ServiceResultCode.Success, null)
			{
				ClutterState = Util.GetMailboxClutterState(mailboxSession)
			};
			return new ServiceResult<SetClutterStateResponse>(value);
		}
	}
}
