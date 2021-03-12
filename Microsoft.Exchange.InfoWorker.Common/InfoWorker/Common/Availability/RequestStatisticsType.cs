using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000C6 RID: 198
	internal struct RequestStatisticsType
	{
		// Token: 0x060004FD RID: 1277 RVA: 0x00016416 File Offset: 0x00014616
		private RequestStatisticsType(string name)
		{
			this.name = name;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0001641F File Offset: 0x0001461F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040002E5 RID: 741
		public static readonly RequestStatisticsType LocalElapsedTimeLongPole = new RequestStatisticsType("LLPE");

		// Token: 0x040002E6 RID: 742
		public static readonly RequestStatisticsType LocalFirstThreadExecute = new RequestStatisticsType("LFTE");

		// Token: 0x040002E7 RID: 743
		public static readonly RequestStatisticsType RequestCPUMain = new RequestStatisticsType("RCM");

		// Token: 0x040002E8 RID: 744
		public static readonly RequestStatisticsType AD = new RequestStatisticsType("AD");

		// Token: 0x040002E9 RID: 745
		public static readonly RequestStatisticsType MailboxRPC = new RequestStatisticsType("MRPC");

		// Token: 0x040002EA RID: 746
		public static readonly RequestStatisticsType ThreadCPULongPole = new RequestStatisticsType("TCLP");

		// Token: 0x040002EB RID: 747
		public static readonly RequestStatisticsType Local = new RequestStatisticsType("LLP");

		// Token: 0x040002EC RID: 748
		public static readonly RequestStatisticsType TotalLocal = new RequestStatisticsType("LT");

		// Token: 0x040002ED RID: 749
		public static readonly RequestStatisticsType FederatedToken = new RequestStatisticsType("FTCL");

		// Token: 0x040002EE RID: 750
		public static readonly RequestStatisticsType AutoDiscoverRequest = new RequestStatisticsType("ADLPR");

		// Token: 0x040002EF RID: 751
		public static readonly RequestStatisticsType IntraSiteProxy = new RequestStatisticsType("ISLP");

		// Token: 0x040002F0 RID: 752
		public static readonly RequestStatisticsType CrossSiteProxy = new RequestStatisticsType("CSLP");

		// Token: 0x040002F1 RID: 753
		public static readonly RequestStatisticsType CrossForestProxy = new RequestStatisticsType("CFLP");

		// Token: 0x040002F2 RID: 754
		public static readonly RequestStatisticsType FederatedProxy = new RequestStatisticsType("FCFLP");

		// Token: 0x040002F3 RID: 755
		public static readonly RequestStatisticsType OAuthProxy = new RequestStatisticsType("OCFLP");

		// Token: 0x040002F4 RID: 756
		private string name;
	}
}
