using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.UM.Rpc;

namespace Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess
{
	// Token: 0x02000086 RID: 134
	internal interface IUMUserMailboxStorage : IDisposeTrackable, IDisposable
	{
		// Token: 0x06000493 RID: 1171
		void InitUMMailbox();

		// Token: 0x06000494 RID: 1172
		void ResetUMMailbox(bool keepProperties);

		// Token: 0x06000495 RID: 1173
		PINInfo ValidateUMPin(string pin, Guid userUMMailboxPolicyGuid);

		// Token: 0x06000496 RID: 1174
		void SaveUMPin(PINInfo pin, Guid userUMMailboxPolicyGuid);

		// Token: 0x06000497 RID: 1175
		PINInfo GetUMPin();

		// Token: 0x06000498 RID: 1176
		void SendEmail(string recipientMailAddress, string messageSubject, string messageBody);

		// Token: 0x06000499 RID: 1177
		PersonaType GetPersonaFromEmail(string emailAddress);

		// Token: 0x0600049A RID: 1178
		UMSubscriberCallAnsweringData GetUMSubscriberCallAnsweringData(UMSubscriber subscriber, TimeSpan timeout);
	}
}
