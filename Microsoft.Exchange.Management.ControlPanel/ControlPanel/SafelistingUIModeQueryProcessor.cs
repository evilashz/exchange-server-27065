using System;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200037A RID: 890
	internal abstract class SafelistingUIModeQueryProcessor : EcpCmdletQueryProcessor
	{
		// Token: 0x17001F2E RID: 7982
		// (get) Token: 0x06003048 RID: 12360
		protected abstract SafelistingUIMode SafelistingUIMode { get; }

		// Token: 0x17001F2F RID: 7983
		// (get) Token: 0x06003049 RID: 12361
		protected abstract string RbacRoleName { get; }

		// Token: 0x0600304A RID: 12362 RVA: 0x00093424 File Offset: 0x00091624
		internal override bool? IsInRoleCmdlet(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			IpSafeListings ipSafeListings = new IpSafeListings();
			PowerShellResults<IpSafeListing> @object = ipSafeListings.GetObject(null);
			if (@object.SucceededWithValue)
			{
				foreach (IpSafeListing ipSafeListing in @object.Output)
				{
					if (ipSafeListing.SafelistingUIMode == this.SafelistingUIMode)
					{
						return new bool?(true);
					}
				}
				return new bool?(false);
			}
			base.LogCmdletError(@object, this.RbacRoleName);
			return null;
		}
	}
}
