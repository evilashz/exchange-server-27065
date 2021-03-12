using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000024 RID: 36
	public class DatabasesSnapshot
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000099C1 File Offset: 0x00007BC1
		// (set) Token: 0x06000184 RID: 388 RVA: 0x000099C9 File Offset: 0x00007BC9
		public string LastSnapshotUtc { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000099D2 File Offset: 0x00007BD2
		// (set) Token: 0x06000186 RID: 390 RVA: 0x000099DA File Offset: 0x00007BDA
		[XmlArrayItem("Database")]
		[XmlArray("Databases")]
		public List<DatabaseSnapshot> Databases { get; set; }

		// Token: 0x06000187 RID: 391 RVA: 0x00009A64 File Offset: 0x00007C64
		public static DatabasesSnapshot Create(string emailAddressFilter, Guid mbxGuidFilter)
		{
			Func<BrokerSubscription, bool> subscriptionFilter = null;
			if (emailAddressFilter != null)
			{
				string pattern = string.Format("^{0}$", Regex.Escape(emailAddressFilter).Replace("\\*", ".*").Replace("\\?", "."));
				Regex regex = new Regex(pattern);
				subscriptionFilter = ((BrokerSubscription sub) => regex.Match(sub.Receiver.MailboxSmtp).Success || regex.Match(sub.Sender.MailboxSmtp).Success);
			}
			else if (mbxGuidFilter != Guid.Empty)
			{
				subscriptionFilter = ((BrokerSubscription sub) => sub.Sender.MailboxGuid == mbxGuidFilter || sub.Receiver.MailboxGuid == mbxGuidFilter);
			}
			return BrokerSubscriptionStore.Instance.GetDiagnostics(subscriptionFilter);
		}
	}
}
