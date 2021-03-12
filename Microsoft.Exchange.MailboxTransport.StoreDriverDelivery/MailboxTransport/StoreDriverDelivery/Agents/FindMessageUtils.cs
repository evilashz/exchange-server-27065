using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000066 RID: 102
	internal static class FindMessageUtils
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00010CDF File Offset: 0x0000EEDF
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00010CE6 File Offset: 0x0000EEE6
		internal static char MessageIdDomainPartDivider
		{
			get
			{
				return FindMessageUtils.messageIdDomainPartDivider;
			}
			set
			{
				FindMessageUtils.messageIdDomainPartDivider = value;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00010CF0 File Offset: 0x0000EEF0
		internal static bool TryParseMessageId(string messageId, out string local, out string domain)
		{
			local = string.Empty;
			domain = string.Empty;
			if (string.IsNullOrEmpty(messageId))
			{
				FindMessageUtils.diag.TraceDebug(0L, "no message id");
				return false;
			}
			if (messageId.Length > 512)
			{
				FindMessageUtils.diag.TraceDebug<string>(0L, "message id '{0}' is longer than supported max.", messageId);
				return false;
			}
			int num = messageId.IndexOf(FindMessageUtils.MessageIdDomainPartDivider);
			if (num >= 0)
			{
				local = messageId.Substring(0, num);
				if (num + 1 < messageId.Length)
				{
					domain = messageId.Substring(num + 1, messageId.Length - num - 1);
				}
			}
			else
			{
				local = messageId;
			}
			return true;
		}

		// Token: 0x04000204 RID: 516
		private static readonly Trace diag = ExTraceGlobals.ApprovalAgentTracer;

		// Token: 0x04000205 RID: 517
		private static readonly PropertyDefinition[] FindByMessageIdPropertyDefinition = new PropertyDefinition[]
		{
			ItemSchema.InternetMessageId,
			ItemSchema.Id
		};

		// Token: 0x04000206 RID: 518
		private static char messageIdDomainPartDivider = '@';
	}
}
