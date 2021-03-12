using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200055A RID: 1370
	internal sealed class ResolverPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003F06 RID: 16134 RVA: 0x0010E0CC File Offset: 0x0010C2CC
		internal ResolverPerfCountersInstance(string instanceName, ResolverPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeTransport Resolver")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.MessagesRetriedTotal = new ExPerformanceCounter(base.CategoryName, "Messages Retried", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesRetriedTotal);
				this.MessagesCreatedTotal = new ExPerformanceCounter(base.CategoryName, "Messages Created", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesCreatedTotal);
				this.MessagesChippedTotal = new ExPerformanceCounter(base.CategoryName, "Messages Chipped", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesChippedTotal);
				this.FailedRecipientsTotal = new ExPerformanceCounter(base.CategoryName, "Failed Recipients", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailedRecipientsTotal);
				this.UnresolvedOrgRecipientsTotal = new ExPerformanceCounter(base.CategoryName, "Unresolved Org Recipients", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.UnresolvedOrgRecipientsTotal);
				this.AmbiguousRecipientsTotal = new ExPerformanceCounter(base.CategoryName, "Ambiguous Recipients", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AmbiguousRecipientsTotal);
				this.LoopRecipientsTotal = new ExPerformanceCounter(base.CategoryName, "Loop Recipients", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.LoopRecipientsTotal);
				this.UnresolvedOrgSendersTotal = new ExPerformanceCounter(base.CategoryName, "Unresolved Org Senders", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.UnresolvedOrgSendersTotal);
				this.AmbiguousSendersTotal = new ExPerformanceCounter(base.CategoryName, "Ambiguous Senders", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AmbiguousSendersTotal);
				this.CatchAllRecipientsTotal = new ExPerformanceCounter(base.CategoryName, "Message directed to catch-all recipient.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CatchAllRecipientsTotal);
				long num = this.MessagesRetriedTotal.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x0010E328 File Offset: 0x0010C528
		internal ResolverPerfCountersInstance(string instanceName) : base(instanceName, "MSExchangeTransport Resolver")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.MessagesRetriedTotal = new ExPerformanceCounter(base.CategoryName, "Messages Retried", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesRetriedTotal);
				this.MessagesCreatedTotal = new ExPerformanceCounter(base.CategoryName, "Messages Created", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesCreatedTotal);
				this.MessagesChippedTotal = new ExPerformanceCounter(base.CategoryName, "Messages Chipped", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesChippedTotal);
				this.FailedRecipientsTotal = new ExPerformanceCounter(base.CategoryName, "Failed Recipients", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailedRecipientsTotal);
				this.UnresolvedOrgRecipientsTotal = new ExPerformanceCounter(base.CategoryName, "Unresolved Org Recipients", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.UnresolvedOrgRecipientsTotal);
				this.AmbiguousRecipientsTotal = new ExPerformanceCounter(base.CategoryName, "Ambiguous Recipients", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AmbiguousRecipientsTotal);
				this.LoopRecipientsTotal = new ExPerformanceCounter(base.CategoryName, "Loop Recipients", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.LoopRecipientsTotal);
				this.UnresolvedOrgSendersTotal = new ExPerformanceCounter(base.CategoryName, "Unresolved Org Senders", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.UnresolvedOrgSendersTotal);
				this.AmbiguousSendersTotal = new ExPerformanceCounter(base.CategoryName, "Ambiguous Senders", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AmbiguousSendersTotal);
				this.CatchAllRecipientsTotal = new ExPerformanceCounter(base.CategoryName, "Message directed to catch-all recipient.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CatchAllRecipientsTotal);
				long num = this.MessagesRetriedTotal.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06003F08 RID: 16136 RVA: 0x0010E584 File Offset: 0x0010C784
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x04002313 RID: 8979
		public readonly ExPerformanceCounter MessagesRetriedTotal;

		// Token: 0x04002314 RID: 8980
		public readonly ExPerformanceCounter MessagesCreatedTotal;

		// Token: 0x04002315 RID: 8981
		public readonly ExPerformanceCounter MessagesChippedTotal;

		// Token: 0x04002316 RID: 8982
		public readonly ExPerformanceCounter FailedRecipientsTotal;

		// Token: 0x04002317 RID: 8983
		public readonly ExPerformanceCounter UnresolvedOrgRecipientsTotal;

		// Token: 0x04002318 RID: 8984
		public readonly ExPerformanceCounter AmbiguousRecipientsTotal;

		// Token: 0x04002319 RID: 8985
		public readonly ExPerformanceCounter LoopRecipientsTotal;

		// Token: 0x0400231A RID: 8986
		public readonly ExPerformanceCounter UnresolvedOrgSendersTotal;

		// Token: 0x0400231B RID: 8987
		public readonly ExPerformanceCounter AmbiguousSendersTotal;

		// Token: 0x0400231C RID: 8988
		public readonly ExPerformanceCounter CatchAllRecipientsTotal;
	}
}
