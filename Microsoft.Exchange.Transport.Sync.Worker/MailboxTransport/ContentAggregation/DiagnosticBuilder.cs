using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x020001FD RID: 509
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DiagnosticBuilder
	{
		// Token: 0x0600111D RID: 4381 RVA: 0x00038554 File Offset: 0x00036754
		public void UpdateSubscriptionDiagnosticMessage(ISyncWorkerData subscription, SyncLogSession syncLogSession, Exception exception)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			SyncUtilities.ThrowIfArgumentNull("SubscriptionDiagnostics", subscription.Diagnostics);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			string diagnostics = string.Empty;
			if (exception != null)
			{
				string blackBoxText = syncLogSession.GetBlackBoxText();
				diagnostics = this.AppendDiagnosticMessage(subscription.Diagnostics, blackBoxText);
			}
			subscription.Diagnostics = diagnostics;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000385AC File Offset: 0x000367AC
		private string AppendDiagnosticMessage(string oldmessage, string newmessage)
		{
			StringBuilder stringBuilder = new StringBuilder(oldmessage, oldmessage.Length + newmessage.Length);
			if (stringBuilder.Length != 0)
			{
				stringBuilder.Append(Environment.NewLine);
			}
			stringBuilder.Append(newmessage);
			if (stringBuilder.Length > DiagnosticBuilder.MaximumDiagnosticSize)
			{
				int length = stringBuilder.Length - DiagnosticBuilder.MaximumDiagnosticSize + DiagnosticBuilder.MoreCharString.Length;
				stringBuilder.Remove(0, length);
				stringBuilder.Insert(0, DiagnosticBuilder.MoreCharString);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000990 RID: 2448
		internal static readonly int MaximumDiagnosticSize = 8192;

		// Token: 0x04000991 RID: 2449
		internal static readonly string MoreCharString = "...";
	}
}
