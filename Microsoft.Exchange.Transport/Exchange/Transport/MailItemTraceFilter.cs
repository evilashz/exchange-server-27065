using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200007B RID: 123
	internal struct MailItemTraceFilter : IDisposable
	{
		// Token: 0x06000389 RID: 905 RVA: 0x0000FBE0 File Offset: 0x0000DDE0
		public MailItemTraceFilter(TransportMailItem mailItem)
		{
			this.filteredMailItem = null;
			this.traceEnabled = false;
			this.traceConfig = null;
			this.SetMailItem(mailItem);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000FC00 File Offset: 0x0000DE00
		public void SetMailItem(TransportMailItem mailItem)
		{
			if (this.filteredMailItem != null)
			{
				throw new InvalidOperationException("Transport MailItem should be set only once!");
			}
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			this.traceConfig = TraceConfigurationSingleton<TransportTraceConfiguration>.Instance;
			this.filteredMailItem = mailItem;
			if (!this.traceConfig.FilteredTracingEnabled)
			{
				return;
			}
			this.traceEnabled = false;
			if (!this.filteredMailItem.ExtendedProperties.TryGetValue<bool>("Microsoft.Exchange.Transport.MailItemTracing", out this.traceEnabled) || !this.traceEnabled)
			{
				this.traceEnabled = (this.IsSenderFiltered(mailItem.From.ToString()) || this.IsSubjectFiltered(mailItem.Subject) || this.IsAnyRecipientFiltered(mailItem.Recipients.AllUnprocessed));
				if (this.traceEnabled)
				{
					this.filteredMailItem.ExtendedProperties.SetValue<bool>("Microsoft.Exchange.Transport.MailItemTracing", true);
				}
			}
			if (this.traceEnabled)
			{
				BaseTrace.CurrentThreadSettings.EnableTracing();
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000FCEE File Offset: 0x0000DEEE
		public void Dispose()
		{
			if (this.traceEnabled)
			{
				BaseTrace.CurrentThreadSettings.DisableTracing();
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000FD04 File Offset: 0x0000DF04
		private static bool MatchFilters(List<string> filters, string stringToMatch)
		{
			if (string.IsNullOrEmpty(stringToMatch))
			{
				return false;
			}
			foreach (string value in filters)
			{
				if (stringToMatch.IndexOf(value, StringComparison.OrdinalIgnoreCase) != -1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000FD68 File Offset: 0x0000DF68
		private bool IsSenderFiltered(string sender)
		{
			return MailItemTraceFilter.MatchFilters(this.traceConfig.FilteredSenders, sender) || MailItemTraceFilter.MatchFilters(this.traceConfig.FilteredUsers, sender);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000FD90 File Offset: 0x0000DF90
		private bool IsAnyRecipientFiltered(IEnumerable<MailRecipient> recipients)
		{
			foreach (MailRecipient mailRecipient in recipients)
			{
				if (MailItemTraceFilter.MatchFilters(this.traceConfig.FilteredRecipients, mailRecipient.Email.ToString()) || MailItemTraceFilter.MatchFilters(this.traceConfig.FilteredUsers, mailRecipient.Email.ToString()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000FE28 File Offset: 0x0000E028
		private bool IsSubjectFiltered(string subject)
		{
			return MailItemTraceFilter.MatchFilters(this.traceConfig.FilteredSubjects, subject);
		}

		// Token: 0x040001FD RID: 509
		public const string MailItemTracingPropName = "Microsoft.Exchange.Transport.MailItemTracing";

		// Token: 0x040001FE RID: 510
		private bool traceEnabled;

		// Token: 0x040001FF RID: 511
		private TransportMailItem filteredMailItem;

		// Token: 0x04000200 RID: 512
		private TransportTraceConfiguration traceConfig;
	}
}
