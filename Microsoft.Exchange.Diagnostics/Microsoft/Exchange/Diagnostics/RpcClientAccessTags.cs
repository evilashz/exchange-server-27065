using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002A8 RID: 680
	public struct RpcClientAccessTags
	{
		// Token: 0x0400126C RID: 4716
		public const int RpcRawBuffer = 0;

		// Token: 0x0400126D RID: 4717
		public const int FailedRop = 1;

		// Token: 0x0400126E RID: 4718
		public const int RopLevelException = 2;

		// Token: 0x0400126F RID: 4719
		public const int NotImplemented = 3;

		// Token: 0x04001270 RID: 4720
		public const int NotificationHandler = 4;

		// Token: 0x04001271 RID: 4721
		public const int NotificationDelivery = 5;

		// Token: 0x04001272 RID: 4722
		public const int Attachment = 6;

		// Token: 0x04001273 RID: 4723
		public const int Message = 7;

		// Token: 0x04001274 RID: 4724
		public const int FailedRpc = 8;

		// Token: 0x04001275 RID: 4725
		public const int ClientThrottled = 9;

		// Token: 0x04001276 RID: 4726
		public const int ConnectRpc = 10;

		// Token: 0x04001277 RID: 4727
		public const int FaultInjection = 11;

		// Token: 0x04001278 RID: 4728
		public const int UnhandledException = 12;

		// Token: 0x04001279 RID: 4729
		public const int AsyncRpc = 13;

		// Token: 0x0400127A RID: 4730
		public const int AccessControl = 14;

		// Token: 0x0400127B RID: 4731
		public const int AsyncRopHandler = 15;

		// Token: 0x0400127C RID: 4732
		public const int ConnectXrop = 16;

		// Token: 0x0400127D RID: 4733
		public const int FailedXrop = 17;

		// Token: 0x0400127E RID: 4734
		public const int Availability = 18;

		// Token: 0x0400127F RID: 4735
		public const int Logon = 19;

		// Token: 0x04001280 RID: 4736
		public const int Folder = 20;

		// Token: 0x04001281 RID: 4737
		public const int ExchangeAsyncDispatch = 21;

		// Token: 0x04001282 RID: 4738
		public const int ExchangeDispatch = 22;

		// Token: 0x04001283 RID: 4739
		public const int DispatchTask = 23;

		// Token: 0x04001284 RID: 4740
		public const int RpcHttpConnectionRegistrationAsyncDispatch = 24;

		// Token: 0x04001285 RID: 4741
		public static Guid guid = new Guid("E5EC0B19-2F45-4b2f-8B2B-4B0473209669");
	}
}
