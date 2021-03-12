using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000216 RID: 534
	internal class UMIPGatewayOutboundTargetPicker : UMIPGatewayTargetPickerBase
	{
		// Token: 0x06000F90 RID: 3984 RVA: 0x0004624A File Offset: 0x0004444A
		private UMIPGatewayOutboundTargetPicker()
		{
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x00046252 File Offset: 0x00044452
		public static UMIPGatewayOutboundTargetPicker Instance
		{
			get
			{
				return UMIPGatewayOutboundTargetPicker.staticInstance;
			}
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00046259 File Offset: 0x00044459
		protected override bool InternalIsValid(UMIPGateway gateway)
		{
			return gateway.OutcallsAllowed;
		}

		// Token: 0x04000B4C RID: 2892
		private static UMIPGatewayOutboundTargetPicker staticInstance = new UMIPGatewayOutboundTargetPicker();
	}
}
