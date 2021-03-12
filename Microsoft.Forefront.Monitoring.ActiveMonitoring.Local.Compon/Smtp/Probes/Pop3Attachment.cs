using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000235 RID: 565
	public class Pop3Attachment
	{
		// Token: 0x060012B9 RID: 4793 RVA: 0x00037060 File Offset: 0x00035260
		public Pop3Attachment(string contentType, string name, byte[] content)
		{
			this.ContentType = contentType;
			this.Name = name;
			this.Content = content;
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x0003707D File Offset: 0x0003527D
		// (set) Token: 0x060012BB RID: 4795 RVA: 0x00037085 File Offset: 0x00035285
		public string ContentType { get; private set; }

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x0003708E File Offset: 0x0003528E
		// (set) Token: 0x060012BD RID: 4797 RVA: 0x00037096 File Offset: 0x00035296
		public string Name { get; private set; }

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x0003709F File Offset: 0x0003529F
		// (set) Token: 0x060012BF RID: 4799 RVA: 0x000370A7 File Offset: 0x000352A7
		public byte[] Content { get; private set; }
	}
}
