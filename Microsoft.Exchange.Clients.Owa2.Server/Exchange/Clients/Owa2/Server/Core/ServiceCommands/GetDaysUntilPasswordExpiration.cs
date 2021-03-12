using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x02000311 RID: 785
	internal class GetDaysUntilPasswordExpiration : ServiceCommand<int>
	{
		// Token: 0x06001A08 RID: 6664 RVA: 0x0005E0E4 File Offset: 0x0005C2E4
		public GetDaysUntilPasswordExpiration(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x0005E0F0 File Offset: 0x0005C2F0
		protected override int InternalExecute()
		{
			UserContext userContext = UserContextManager.GetUserContext(base.CallContext.HttpContext, base.CallContext.EffectiveCaller, true);
			return this.CalculateDaysUntilPasswordExpiration(userContext);
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x0005E124 File Offset: 0x0005C324
		protected int CalculateDaysUntilPasswordExpiration(UserContext userContext)
		{
			ExDateTime exDateTime = ExDateTime.MaxValue;
			ExDateTime exDateTime2 = ExDateTime.MaxValue;
			try
			{
				userContext.LockAndReconnectMailboxSession(30000);
				IExchangePrincipal mailboxOwner = userContext.MailboxSession.MailboxOwner;
				ConfigurationContext configurationContext = new ConfigurationContext(userContext);
				if (userContext.IsExplicitLogon || (configurationContext.SegmentationFlags & 67108864UL) != 67108864UL || userContext.MailboxIdentity.IsCrossForest(mailboxOwner.MasterAccountSid) || userContext.IsBposUser)
				{
					return -1;
				}
				exDateTime2 = DirectoryHelper.GetPasswordExpirationDate(mailboxOwner.ObjectId, userContext.MailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid));
				if (ExDateTime.MaxValue == exDateTime2)
				{
					return -1;
				}
				exDateTime = userContext.MailboxSession.ExTimeZone.ConvertDateTime(exDateTime2);
			}
			catch (Exception)
			{
				ExTraceGlobals.UserOptionsTracer.TraceError(0L, "Could not acquire lock for UserContext to get time zone.");
				exDateTime = exDateTime2;
			}
			finally
			{
				userContext.UnlockAndDisconnectMailboxSession();
			}
			ExDateTime exDateTime3 = ExDateTime.Now;
			if (exDateTime3.CompareTo(exDateTime) > 0)
			{
				return 0;
			}
			if (ExTraceGlobals.ChangePasswordTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.ChangePasswordTracer.TraceDebug(0L, "Password expiration for {0}: Expiration UTC date: {1} / Expiration local user date {2} / User current Local date: {3} ", new object[]
				{
					userContext.LogonIdentity.SafeGetRenderableName(),
					(DateTime)exDateTime2.ToUtc(),
					(DateTime)exDateTime,
					exDateTime3
				});
			}
			exDateTime3 = exDateTime3.Date;
			exDateTime = exDateTime.Date;
			int days = exDateTime.Subtract(exDateTime3).Days;
			if (days < 14)
			{
				return days;
			}
			return -1;
		}

		// Token: 0x04000E6C RID: 3692
		internal const int MailboxSessionLockTimeout = 30000;

		// Token: 0x04000E6D RID: 3693
		private const int PasswordExpirationNotificationDays = 14;
	}
}
