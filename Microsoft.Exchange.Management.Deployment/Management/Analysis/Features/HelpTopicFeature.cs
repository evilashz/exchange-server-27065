using System;

namespace Microsoft.Exchange.Management.Analysis.Features
{
	// Token: 0x02000061 RID: 97
	internal class HelpTopicFeature : Feature
	{
		// Token: 0x0600024D RID: 589 RVA: 0x00008420 File Offset: 0x00006620
		public HelpTopicFeature(Guid topicGuid) : base(false, false)
		{
			this.TopicGuid = topicGuid;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00008431 File Offset: 0x00006631
		// (set) Token: 0x0600024F RID: 591 RVA: 0x00008439 File Offset: 0x00006639
		public Guid TopicGuid { get; private set; }

		// Token: 0x06000250 RID: 592 RVA: 0x00008444 File Offset: 0x00006644
		public override string ToString()
		{
			return string.Format("{0}({1})", base.ToString(), this.TopicGuid.ToString());
		}
	}
}
