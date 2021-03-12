using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000D1 RID: 209
	internal abstract class ScheduledAction<T> : ScheduledActionBase
	{
		// Token: 0x060008F0 RID: 2288 RVA: 0x0003C7C5 File Offset: 0x0003A9C5
		public ScheduledAction(ExDateTime expectedTime, T context) : base(expectedTime)
		{
			this.Context = context;
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0003C7D5 File Offset: 0x0003A9D5
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x0003C7DD File Offset: 0x0003A9DD
		public T Context { get; private set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0003C7E8 File Offset: 0x0003A9E8
		protected override string SourceDescription
		{
			get
			{
				if (!object.ReferenceEquals(null, this.Context))
				{
					T context = this.Context;
					return context.ToString();
				}
				return string.Empty;
			}
		}
	}
}
