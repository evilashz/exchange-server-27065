using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ResourceBooking;
using Microsoft.Exchange.InfoWorker.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ResourceBooking
{
	// Token: 0x02000128 RID: 296
	internal static class BookingPolicy
	{
		// Token: 0x06000BCD RID: 3021 RVA: 0x0004DDF0 File Offset: 0x0004BFF0
		public static BookingRoles QueryRole(IRecipientSession rs, ADRecipient recipient, CalendarConfiguration mailboxConfig, out bool reqInPolicy, out bool bookInPolicy)
		{
			BookingRoles bookingRoles = BookingRoles.NoRole;
			reqInPolicy = false;
			bookInPolicy = false;
			BookingPolicy.Tracer.TraceDebug((long)((recipient != null) ? recipient.GetHashCode() : 0), "{0}: Evaluating RequestInPolicy", new object[]
			{
				TraceContext.Get()
			});
			if (mailboxConfig.AllRequestInPolicy || BookingPolicy.InRole(rs, recipient, mailboxConfig.RequestInPolicy, mailboxConfig.RequestInPolicyLegacy))
			{
				bookingRoles = BookingRoles.RequestInPolicy;
				reqInPolicy = true;
			}
			BookingPolicy.Tracer.TraceDebug((long)((recipient != null) ? recipient.GetHashCode() : 0), "{0}: Evaluating BookInPolicy", new object[]
			{
				TraceContext.Get()
			});
			if (mailboxConfig.AllBookInPolicy || BookingPolicy.InRole(rs, recipient, mailboxConfig.BookInPolicy, mailboxConfig.BookInPolicyLegacy))
			{
				bookingRoles = BookingRoles.BookInPolicy;
				bookInPolicy = true;
			}
			BookingPolicy.Tracer.TraceDebug((long)((recipient != null) ? recipient.GetHashCode() : 0), "{0}: Evaluating RequestOutOfPolicy", new object[]
			{
				TraceContext.Get()
			});
			if (mailboxConfig.AllRequestOutOfPolicy || BookingPolicy.InRole(rs, recipient, mailboxConfig.RequestOutOfPolicy, mailboxConfig.RequestOutOfPolicyLegacy))
			{
				bookingRoles = BookingRoles.RequestOutOfPolicy;
			}
			if (bookingRoles == BookingRoles.NoRole)
			{
				BookingPolicy.Tracer.TraceDebug((long)((recipient != null) ? recipient.GetHashCode() : 0), "{0}: No Role", new object[]
				{
					TraceContext.Get()
				});
			}
			return bookingRoles;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0004DF24 File Offset: 0x0004C124
		internal static bool InRole(IRecipientSession rs, ADRecipient recipient, MultiValuedProperty<string> roleMembersLegDN, MultiValuedProperty<ADObjectId> roleMemberslegacy)
		{
			if (recipient == null)
			{
				return false;
			}
			if (roleMemberslegacy == null && roleMembersLegDN == null)
			{
				return false;
			}
			if (roleMembersLegDN != null && roleMembersLegDN.Count != 0)
			{
				foreach (string text in roleMembersLegDN)
				{
					if (string.Equals(text, recipient.LegacyExchangeDN, StringComparison.InvariantCultureIgnoreCase))
					{
						return true;
					}
					ADRecipient adrecipient = rs.FindByLegacyExchangeDN(text);
					if (adrecipient != null && adrecipient is ADGroup && recipient.IsMemberOf(adrecipient.Id, false))
					{
						BookingPolicy.Tracer.TraceDebug((long)recipient.GetHashCode(), "{0}: Group match", new object[]
						{
							TraceContext.Get()
						});
						return true;
					}
				}
			}
			if (roleMemberslegacy != null && roleMemberslegacy.Count != 0)
			{
				foreach (ADObjectId adobjectId in roleMemberslegacy)
				{
					if (ADObjectId.Equals(adobjectId, recipient.Id))
					{
						return true;
					}
					ADRecipient adrecipient2 = rs.Read(adobjectId);
					if (adrecipient2 != null && adrecipient2 is ADGroup && recipient.IsMemberOf(adrecipient2.Id, false))
					{
						BookingPolicy.Tracer.TraceDebug((long)recipient.GetHashCode(), "{0}: Group match", new object[]
						{
							TraceContext.Get()
						});
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000753 RID: 1875
		private static readonly Trace Tracer = ExTraceGlobals.BookingPolicyTracer;
	}
}
