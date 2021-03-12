using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Transport.Sync.Manager.Throttling
{
	// Token: 0x02000046 RID: 70
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DispatchEntry : EventArgs
	{
		// Token: 0x06000380 RID: 896 RVA: 0x00017207 File Offset: 0x00015407
		public DispatchEntry(MiniSubscriptionInformation miniSubscriptionInformation, WorkType workType, ExDateTime dispatchAttemptTime, ExDateTime enqueueTime)
		{
			this.miniSubscriptionInformation = miniSubscriptionInformation;
			this.workType = workType;
			this.dispatchAttemptTime = dispatchAttemptTime;
			this.dispatchEnqueueTime = enqueueTime;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0001722C File Offset: 0x0001542C
		internal MiniSubscriptionInformation MiniSubscriptionInformation
		{
			get
			{
				return this.miniSubscriptionInformation;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00017234 File Offset: 0x00015434
		internal WorkType WorkType
		{
			get
			{
				return this.workType;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0001723C File Offset: 0x0001543C
		internal ExDateTime DispatchAttemptTime
		{
			get
			{
				return this.dispatchAttemptTime;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00017244 File Offset: 0x00015444
		internal ExDateTime DispatchEnqueuedTime
		{
			get
			{
				return this.dispatchEnqueueTime;
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001724C File Offset: 0x0001544C
		public void SetDispatchAttemptTime(ExDateTime dispatchAttemptTime)
		{
			this.dispatchAttemptTime = dispatchAttemptTime;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00017258 File Offset: 0x00015458
		public XElement GetDiagnosticInfo()
		{
			XElement xelement = new XElement("DispatchEntry");
			xelement.Add(new XElement("subscriptionGuid", this.MiniSubscriptionInformation.SubscriptionGuid));
			xelement.Add(new XElement("workType", this.workType.ToString()));
			xelement.Add(new XElement("dispatchAttemptTime", this.dispatchAttemptTime.ToString("o")));
			return xelement;
		}

		// Token: 0x040001E8 RID: 488
		private MiniSubscriptionInformation miniSubscriptionInformation;

		// Token: 0x040001E9 RID: 489
		private WorkType workType;

		// Token: 0x040001EA RID: 490
		private ExDateTime dispatchAttemptTime;

		// Token: 0x040001EB RID: 491
		private ExDateTime dispatchEnqueueTime;
	}
}
