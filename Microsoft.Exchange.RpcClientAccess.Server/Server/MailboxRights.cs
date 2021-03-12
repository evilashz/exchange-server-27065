using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000027 RID: 39
	internal static class MailboxRights
	{
		// Token: 0x0400009A RID: 154
		public const uint Owner = 1U;

		// Token: 0x0400009B RID: 155
		private const uint SendAs = 2U;

		// Token: 0x0400009C RID: 156
		private const uint PrimaryUser = 4U;

		// Token: 0x0400009D RID: 157
		public static readonly Guid SendAsExtendedRightGuid = WellKnownGuid.SendAsExtendedRightGuid;

		// Token: 0x0400009E RID: 158
		public static readonly Guid ReceiveAsExtendedRightGuid = WellKnownGuid.ReceiveAsExtendedRightGuid;

		// Token: 0x0400009F RID: 159
		public static readonly Guid UserObjectType = new Guid("bf967aba-0de6-11d0-a285-00aa003049e2");

		// Token: 0x040000A0 RID: 160
		public static readonly NativeMethods.GENERIC_MAPPING GenericRightsMapping = new NativeMethods.GENERIC_MAPPING
		{
			GenericRead = 131072U,
			GenericWrite = 196608U,
			GenericExecute = 131072U,
			GenericAll = 2031623U
		};
	}
}
