using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel.Web;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000257 RID: 599
	internal class ServiceMethodInfo
	{
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x000534AB File Offset: 0x000516AB
		// (set) Token: 0x06001693 RID: 5779 RVA: 0x000534B3 File Offset: 0x000516B3
		internal string Name { get; set; }

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x000534BC File Offset: 0x000516BC
		// (set) Token: 0x06001695 RID: 5781 RVA: 0x000534C4 File Offset: 0x000516C4
		internal Type RequestType { get; set; }

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x000534CD File Offset: 0x000516CD
		// (set) Token: 0x06001697 RID: 5783 RVA: 0x000534D5 File Offset: 0x000516D5
		internal Type ResponseType { get; set; }

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x000534DE File Offset: 0x000516DE
		// (set) Token: 0x06001699 RID: 5785 RVA: 0x000534E6 File Offset: 0x000516E6
		internal Type WrappedRequestType { get; set; }

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x000534EF File Offset: 0x000516EF
		// (set) Token: 0x0600169B RID: 5787 RVA: 0x000534F7 File Offset: 0x000516F7
		internal Dictionary<string, string> WrappedRequestTypeParameterMap { get; set; }

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x00053500 File Offset: 0x00051700
		// (set) Token: 0x0600169D RID: 5789 RVA: 0x00053508 File Offset: 0x00051708
		internal bool IsAsyncPattern { get; set; }

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x00053511 File Offset: 0x00051711
		// (set) Token: 0x0600169F RID: 5791 RVA: 0x00053519 File Offset: 0x00051719
		internal bool IsAsyncAwait { get; set; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x00053522 File Offset: 0x00051722
		// (set) Token: 0x060016A1 RID: 5793 RVA: 0x0005352A File Offset: 0x0005172A
		internal bool IsWrappedRequest { get; set; }

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x00053533 File Offset: 0x00051733
		// (set) Token: 0x060016A3 RID: 5795 RVA: 0x0005353B File Offset: 0x0005173B
		internal bool IsWrappedResponse { get; set; }

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x00053544 File Offset: 0x00051744
		// (set) Token: 0x060016A5 RID: 5797 RVA: 0x0005354C File Offset: 0x0005174C
		internal bool IsStreamedResponse { get; set; }

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x00053555 File Offset: 0x00051755
		// (set) Token: 0x060016A7 RID: 5799 RVA: 0x0005355D File Offset: 0x0005175D
		internal bool ShouldAutoDisposeRequest { get; set; }

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x00053566 File Offset: 0x00051766
		// (set) Token: 0x060016A9 RID: 5801 RVA: 0x0005356E File Offset: 0x0005176E
		internal bool ShouldAutoDisposeResponse { get; set; }

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x00053577 File Offset: 0x00051777
		// (set) Token: 0x060016AB RID: 5803 RVA: 0x0005357F File Offset: 0x0005177F
		internal bool IsResponseCacheable { get; set; }

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x00053588 File Offset: 0x00051788
		// (set) Token: 0x060016AD RID: 5805 RVA: 0x00053590 File Offset: 0x00051790
		internal bool IsHttpGet { get; set; }

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x00053599 File Offset: 0x00051799
		// (set) Token: 0x060016AF RID: 5807 RVA: 0x000535A1 File Offset: 0x000517A1
		internal UriTemplate UriTemplate { get; set; }

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x000535AA File Offset: 0x000517AA
		// (set) Token: 0x060016B1 RID: 5809 RVA: 0x000535B2 File Offset: 0x000517B2
		internal MethodInfo SyncMethod { get; set; }

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x000535BB File Offset: 0x000517BB
		// (set) Token: 0x060016B3 RID: 5811 RVA: 0x000535C3 File Offset: 0x000517C3
		internal MethodInfo BeginMethod { get; set; }

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x000535CC File Offset: 0x000517CC
		// (set) Token: 0x060016B5 RID: 5813 RVA: 0x000535D4 File Offset: 0x000517D4
		internal MethodInfo EndMethod { get; set; }

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x000535DD File Offset: 0x000517DD
		// (set) Token: 0x060016B7 RID: 5815 RVA: 0x000535E5 File Offset: 0x000517E5
		internal MethodInfo GenericAsyncTaskMethod { get; set; }

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x000535EE File Offset: 0x000517EE
		// (set) Token: 0x060016B9 RID: 5817 RVA: 0x000535F6 File Offset: 0x000517F6
		internal JsonRequestFormat JsonRequestFormat { get; set; }

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x000535FF File Offset: 0x000517FF
		// (set) Token: 0x060016BB RID: 5819 RVA: 0x00053607 File Offset: 0x00051807
		internal WebMessageFormat WebMethodResponseFormat { get; set; }

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x00053610 File Offset: 0x00051810
		// (set) Token: 0x060016BD RID: 5821 RVA: 0x00053618 File Offset: 0x00051818
		internal WebMessageFormat WebMethodRequestFormat { get; set; }
	}
}
