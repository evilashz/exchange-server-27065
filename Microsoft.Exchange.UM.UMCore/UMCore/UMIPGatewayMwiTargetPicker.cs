using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000214 RID: 532
	internal class UMIPGatewayMwiTargetPicker : UMIPGatewayTargetPickerBase
	{
		// Token: 0x06000F8A RID: 3978 RVA: 0x000461E1 File Offset: 0x000443E1
		private UMIPGatewayMwiTargetPicker()
		{
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x000461E9 File Offset: 0x000443E9
		public static UMIPGatewayMwiTargetPicker Instance
		{
			get
			{
				return UMIPGatewayMwiTargetPicker.staticInstance;
			}
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x000461F0 File Offset: 0x000443F0
		protected override bool InternalIsValid(UMIPGateway candidate)
		{
			return candidate.MessageWaitingIndicatorAllowed;
		}

		// Token: 0x04000B4B RID: 2891
		private static UMIPGatewayMwiTargetPicker staticInstance = new UMIPGatewayMwiTargetPicker();
	}
}
