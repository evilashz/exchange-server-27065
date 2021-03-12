using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002D1 RID: 721
	public struct TopologyServiceTags
	{
		// Token: 0x04001334 RID: 4916
		public const int Service = 0;

		// Token: 0x04001335 RID: 4917
		public const int Client = 1;

		// Token: 0x04001336 RID: 4918
		public const int WCFServiceEndpoint = 2;

		// Token: 0x04001337 RID: 4919
		public const int WCFClientEndpoint = 3;

		// Token: 0x04001338 RID: 4920
		public const int Topology = 4;

		// Token: 0x04001339 RID: 4921
		public const int SuitabilityVerifier = 5;

		// Token: 0x0400133A RID: 4922
		public const int Discovery = 6;

		// Token: 0x0400133B RID: 4923
		public const int DnsTroubleshooter = 7;

		// Token: 0x0400133C RID: 4924
		public const int FaultInjection = 8;

		// Token: 0x0400133D RID: 4925
		public static Guid guid = new Guid("23c20436-ba78-481d-99c3-5c523ff23024");
	}
}
