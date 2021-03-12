using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200026C RID: 620
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ExchangePrincipalExtensions
	{
		// Token: 0x0600196E RID: 6510 RVA: 0x00079A6A File Offset: 0x00077C6A
		public static IMailboxInfo GetPrimaryMailbox(this IExchangePrincipal exchangePrincipal)
		{
			return exchangePrincipal.AllMailboxes.FirstOrDefault((IMailboxInfo mailbox) => !mailbox.IsArchive && !mailbox.IsAggregated);
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x00079A9C File Offset: 0x00077C9C
		public static IMailboxInfo GetArchiveMailbox(this IExchangePrincipal exchangePrincipal)
		{
			return exchangePrincipal.AllMailboxes.FirstOrDefault((IMailboxInfo mailbox) => mailbox.IsArchive);
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x00079AEC File Offset: 0x00077CEC
		public static IMailboxInfo GetAggregatedMailbox(this IExchangePrincipal exchangePrincipal, Guid aggregatedMailboxGuid)
		{
			ArgumentValidator.ThrowIfEmpty("aggregatedMailboxGuid", aggregatedMailboxGuid);
			return exchangePrincipal.AllMailboxes.FirstOrDefault((IMailboxInfo mailbox) => mailbox.IsAggregated && mailbox.MailboxGuid == aggregatedMailboxGuid);
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x00079B44 File Offset: 0x00077D44
		public static ExchangePrincipal GetAggregatedExchangePrincipal(this ExchangePrincipal exchangePrincipal, Guid aggregatedMailboxGuid)
		{
			if (exchangePrincipal.MailboxInfo.IsAggregated)
			{
				throw new InvalidOperationException("Cannot get aggregated mailbox of an aggregated ExchangePrincipal");
			}
			if (exchangePrincipal.MailboxInfo.IsArchive)
			{
				throw new InvalidOperationException("Cannot get aggregated mailbox of an archive ExchangePrincipal");
			}
			if (exchangePrincipal.AggregatedMailboxGuids.All((Guid mailboxGuid) => aggregatedMailboxGuid != mailboxGuid))
			{
				throw new InvalidOperationException("Invalid aggregated mailbox Guid used");
			}
			return ExchangePrincipalExtensions.CloneExchangePrincipal(exchangePrincipal, false, new Guid?(aggregatedMailboxGuid), null);
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x00079BD0 File Offset: 0x00077DD0
		public static ExchangePrincipal GetArchiveExchangePrincipal(this ExchangePrincipal exchangePrincipal)
		{
			return ExchangePrincipalExtensions.GetArchiveExchangePrincipalInternal(exchangePrincipal, null);
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x00079BEC File Offset: 0x00077DEC
		public static ExchangePrincipal GetArchiveExchangePrincipal(this ExchangePrincipal exchangePrincipal, RemotingOptions remotingOptions)
		{
			EnumValidator.ThrowIfInvalid<RemotingOptions>(remotingOptions, "remotingOptions");
			return ExchangePrincipalExtensions.GetArchiveExchangePrincipalInternal(exchangePrincipal, new RemotingOptions?(remotingOptions));
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x00079C08 File Offset: 0x00077E08
		public static ICollection<string> GetAllEmailAddresses(this IExchangePrincipal exchangePrincipal)
		{
			Util.ThrowOnNullArgument(exchangePrincipal, "exchangePrincipal");
			int num = exchangePrincipal.MailboxInfo.EmailAddresses.Count<ProxyAddress>();
			if (exchangePrincipal.MailboxInfo.EmailAddresses == null || num == 0)
			{
				return new string[]
				{
					exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString()
				};
			}
			string[] array = new string[1 + num];
			array[0] = exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
			int num2 = 1;
			foreach (ProxyAddress proxyAddress in exchangePrincipal.MailboxInfo.EmailAddresses)
			{
				array[num2] = proxyAddress.ToString();
				num2++;
			}
			return array;
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x00079CE4 File Offset: 0x00077EE4
		private static ExchangePrincipal GetArchiveExchangePrincipalInternal(ExchangePrincipal exchangePrincipal, RemotingOptions? remotingOptions)
		{
			if (exchangePrincipal.MailboxInfo.IsAggregated)
			{
				throw new InvalidOperationException("Cannot get archive mailbox of an aggregated ExchangePrincipal");
			}
			if (exchangePrincipal.MailboxInfo.IsArchive)
			{
				throw new InvalidOperationException("Cannot get archive mailbox of an archive ExchangePrincipal");
			}
			return ExchangePrincipalExtensions.CloneExchangePrincipal(exchangePrincipal, true, null, remotingOptions);
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x00079D34 File Offset: 0x00077F34
		private static ExchangePrincipal CloneExchangePrincipal(ExchangePrincipal sourceExchangePrincipal, bool asArchive, Guid? aggregatedMailboxGuid, RemotingOptions? remotingOptions)
		{
			IMailboxInfo selectedMailboxInfo;
			if (asArchive)
			{
				selectedMailboxInfo = sourceExchangePrincipal.GetArchiveMailbox();
			}
			else if (aggregatedMailboxGuid != null && aggregatedMailboxGuid != Guid.Empty)
			{
				selectedMailboxInfo = sourceExchangePrincipal.GetAggregatedMailbox(aggregatedMailboxGuid.Value);
			}
			else
			{
				selectedMailboxInfo = sourceExchangePrincipal.MailboxInfo;
			}
			return sourceExchangePrincipal.WithSelectedMailbox(selectedMailboxInfo, remotingOptions);
		}
	}
}
