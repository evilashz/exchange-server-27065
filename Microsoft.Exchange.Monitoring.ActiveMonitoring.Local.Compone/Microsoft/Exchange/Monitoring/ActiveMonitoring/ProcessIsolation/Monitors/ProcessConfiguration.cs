using System;
using System.Collections.Generic;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ProcessIsolation.Monitors
{
	// Token: 0x02000293 RID: 659
	internal class ProcessConfiguration
	{
		// Token: 0x060012A1 RID: 4769 RVA: 0x000814FB File Offset: 0x0007F6FB
		internal ProcessConfiguration(Component component, ProcessType processType, ProcessResponderConfiguration responders) : this(component, processType, responders, new Dictionary<string, object>())
		{
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0008150B File Offset: 0x0007F70B
		internal ProcessConfiguration(Component component, ProcessType processType, ProcessResponderConfiguration responders, Dictionary<string, object> parameters) : this(component, processType, responders, parameters, null)
		{
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00081519 File Offset: 0x0007F719
		internal ProcessConfiguration(Component component, ProcessType processType, ProcessResponderConfiguration responders, Dictionary<string, object> parameters, Func<bool> shouldRunOnLocalServer)
		{
			this.Component = component;
			this.ProcessType = processType;
			this.Responders = responders;
			this.Parameters = parameters;
			this.ShouldRunOnLocalServer = shouldRunOnLocalServer;
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x00081546 File Offset: 0x0007F746
		// (set) Token: 0x060012A5 RID: 4773 RVA: 0x0008154E File Offset: 0x0007F74E
		internal Component Component { get; private set; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x00081557 File Offset: 0x0007F757
		// (set) Token: 0x060012A7 RID: 4775 RVA: 0x0008155F File Offset: 0x0007F75F
		internal ProcessType ProcessType { get; private set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x00081568 File Offset: 0x0007F768
		// (set) Token: 0x060012A9 RID: 4777 RVA: 0x00081570 File Offset: 0x0007F770
		internal ProcessResponderConfiguration Responders { get; private set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x00081579 File Offset: 0x0007F779
		// (set) Token: 0x060012AB RID: 4779 RVA: 0x00081581 File Offset: 0x0007F781
		internal Dictionary<string, object> Parameters { get; private set; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x0008158A File Offset: 0x0007F78A
		// (set) Token: 0x060012AD RID: 4781 RVA: 0x00081592 File Offset: 0x0007F792
		internal Func<bool> ShouldRunOnLocalServer { get; private set; }
	}
}
