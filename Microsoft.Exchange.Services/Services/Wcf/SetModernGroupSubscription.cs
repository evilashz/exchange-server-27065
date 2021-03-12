using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000939 RID: 2361
	internal class SetModernGroupSubscription : ServiceCommand<bool>
	{
		// Token: 0x06004464 RID: 17508 RVA: 0x000EADA8 File Offset: 0x000E8FA8
		public SetModernGroupSubscription(CallContext context) : base(context)
		{
			this.session = context.SessionCache.GetMailboxIdentityMailboxSession();
		}

		// Token: 0x06004465 RID: 17509 RVA: 0x000EADC4 File Offset: 0x000E8FC4
		protected override bool InternalExecute()
		{
			ExTraceGlobals.ModernGroupsTracer.TraceDebug<SmtpAddress>((long)this.GetHashCode(), "SetModernGroupSubscription.InternalExecute: Setting subscription for user {0}.", base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress);
			bool result;
			try
			{
				IRecipientSession adrecipientSession = base.CallContext.ADRecipientSessionContext.GetADRecipientSession();
				UserMailboxLocator userMailboxLocator = UserMailboxLocator.Instantiate(adrecipientSession, base.CallContext.AccessingADUser);
				ModernGroupNotificationLocator modernGroupNotificationLocator = new ModernGroupNotificationLocator(adrecipientSession);
				modernGroupNotificationLocator.UpdateMemberSubscription(this.session, userMailboxLocator);
				result = true;
			}
			catch (TransientException arg)
			{
				ExTraceGlobals.ModernGroupsTracer.TraceError<TransientException>((long)this.GetHashCode(), "GetModernGroupUnseenItems.InternalExecute: TransientException while querying for unseen conversation items. {0}.", arg);
				throw;
			}
			catch (DataSourceOperationException arg2)
			{
				ExTraceGlobals.ModernGroupsTracer.TraceError<DataSourceOperationException>((long)this.GetHashCode(), "GetModernGroupUnseenItems.InternalExecute: DataSourceOperationException while querying for unseen conversation items. {0}.", arg2);
				throw;
			}
			return result;
		}

		// Token: 0x040027D9 RID: 10201
		private readonly MailboxSession session;
	}
}
