using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000923 RID: 2339
	internal class GetModernGroupUnseenItems : ServiceCommand<GetModernGroupUnseenItemsResponse>
	{
		// Token: 0x060043CF RID: 17359 RVA: 0x000E65DD File Offset: 0x000E47DD
		public GetModernGroupUnseenItems(CallContext context) : base(context)
		{
			this.session = context.SessionCache.GetMailboxIdentityMailboxSession();
		}

		// Token: 0x060043D0 RID: 17360 RVA: 0x000E65F7 File Offset: 0x000E47F7
		public static bool RequestShouldUseSharedContext(string methodName)
		{
			return methodName == "GetModernGroupUnseenItems";
		}

		// Token: 0x060043D1 RID: 17361 RVA: 0x000E6604 File Offset: 0x000E4804
		protected override GetModernGroupUnseenItemsResponse InternalExecute()
		{
			ExTraceGlobals.ModernGroupsTracer.TraceDebug<SmtpAddress>((long)this.GetHashCode(), "GetModernGroupUnseenItems.InternalExecute: Getting unseen items for user {0}.", base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress);
			GetModernGroupUnseenItemsResponse result;
			try
			{
				IRecipientSession adrecipientSession = base.CallContext.ADRecipientSessionContext.GetADRecipientSession();
				UserMailboxLocator userMailboxLocator = UserMailboxLocator.Instantiate(adrecipientSession, base.CallContext.AccessingADUser);
				UnseenDataType unseenData;
				using (IUnseenItemsReader unseenItemsReader = UnseenItemsReader.Create(this.session))
				{
					ModernGroupNotificationLocator modernGroupNotificationLocator = new ModernGroupNotificationLocator(adrecipientSession);
					IMemberSubscriptionItem memberSubscription = modernGroupNotificationLocator.GetMemberSubscription(this.session, userMailboxLocator);
					unseenItemsReader.LoadLastNItemReceiveDates(this.session);
					ExDateTime lastUpdateTimeUTC = memberSubscription.LastUpdateTimeUTC;
					int unseenItemCount = unseenItemsReader.GetUnseenItemCount(lastUpdateTimeUTC);
					unseenData = new UnseenDataType(unseenItemCount, ExDateTimeConverter.ToUtcXsdDateTime(lastUpdateTimeUTC));
				}
				result = new GetModernGroupUnseenItemsResponse
				{
					UnseenData = unseenData
				};
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

		// Token: 0x0400278C RID: 10124
		private readonly MailboxSession session;
	}
}
