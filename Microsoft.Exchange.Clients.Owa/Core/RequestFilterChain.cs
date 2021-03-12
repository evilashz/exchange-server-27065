using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000CB RID: 203
	public abstract class RequestFilterChain
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x00038242 File Offset: 0x00036442
		// (set) Token: 0x06000739 RID: 1849 RVA: 0x0003824A File Offset: 0x0003644A
		internal RequestFilterChain Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x0600073A RID: 1850
		internal abstract bool FilterRequest(object source, EventArgs e, RequestEventType eventType);

		// Token: 0x0600073B RID: 1851 RVA: 0x00038254 File Offset: 0x00036454
		internal bool ExecuteRequestFilterChain(object source, EventArgs e, RequestEventType eventType)
		{
			bool flag = this.FilterRequest(source, e, eventType);
			if (!flag && this.Next != null)
			{
				flag = this.Next.ExecuteRequestFilterChain(source, e, eventType);
			}
			return flag;
		}

		// Token: 0x04000508 RID: 1288
		private RequestFilterChain next;
	}
}
