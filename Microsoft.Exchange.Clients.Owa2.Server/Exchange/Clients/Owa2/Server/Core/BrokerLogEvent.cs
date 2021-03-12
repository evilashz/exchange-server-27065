using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000095 RID: 149
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class BrokerLogEvent : ILogEvent
	{
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x000109D0 File Offset: 0x0000EBD0
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x000109D8 File Offset: 0x0000EBD8
		public ExchangePrincipal Principal { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x000109E1 File Offset: 0x0000EBE1
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x000109E9 File Offset: 0x0000EBE9
		public string UserContextKey { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x000109F2 File Offset: 0x0000EBF2
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x000109FA File Offset: 0x0000EBFA
		public Exception HandledException { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00010A03 File Offset: 0x0000EC03
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x00010A0B File Offset: 0x0000EC0B
		public string SubscriptionId { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x00010A14 File Offset: 0x0000EC14
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x00010A1C File Offset: 0x0000EC1C
		public Guid BrokerSubscriptionId { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00010A25 File Offset: 0x0000EC25
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x00010A2D File Offset: 0x0000EC2D
		public string EventName { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00010A36 File Offset: 0x0000EC36
		public string EventId
		{
			get
			{
				return "BrokerNotification";
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00010A40 File Offset: 0x0000EC40
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
			list.Add(new KeyValuePair<string, object>("MG", (this.Principal == null || this.Principal.MailboxInfo == null) ? string.Empty : this.Principal.MailboxInfo.MailboxGuid.ToString()));
			List<KeyValuePair<string, object>> list2 = list;
			string key = "PSA";
			object value;
			if (this.Principal != null && this.Principal.MailboxInfo != null)
			{
				SmtpAddress primarySmtpAddress = this.Principal.MailboxInfo.PrimarySmtpAddress;
				value = this.Principal.MailboxInfo.PrimarySmtpAddress.ToString();
			}
			else
			{
				value = string.Empty;
			}
			list2.Add(new KeyValuePair<string, object>(key, value));
			list.Add(new KeyValuePair<string, object>("UCK", this.UserContextKey ?? string.Empty));
			list.Add(new KeyValuePair<string, object>("SID", this.SubscriptionId ?? string.Empty));
			list.Add(new KeyValuePair<string, object>("BSID", this.BrokerSubscriptionId));
			list.Add(new KeyValuePair<string, object>("EN", this.EventName ?? string.Empty));
			list.Add(new KeyValuePair<string, object>("EX", (this.HandledException == null) ? string.Empty : this.HandledException.ToString()));
			return list;
		}
	}
}
