using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.ClientAccess
{
	// Token: 0x0200002B RID: 43
	internal class UMServerManager
	{
		// Token: 0x0600027A RID: 634 RVA: 0x00009FE8 File Offset: 0x000081E8
		internal static UMServerProxy GetServerByDialplan(ADObjectId dialPlanId)
		{
			Guid context = (dialPlanId != null) ? dialPlanId.ObjectGuid : Guid.Empty;
			UMServerProxy umserverProxy = (UMServerProxy)UMServerManager.PlayOnPhoneUMServerPicker.Instance.PickNextServer(context);
			if (umserverProxy == null)
			{
				throw new UMServerNotFoundDialPlanException(context.ToString());
			}
			return umserverProxy;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000A02E File Offset: 0x0000822E
		internal static UMServerProxy GetServer()
		{
			return UMServerManager.GetServerByDialplan(null);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A036 File Offset: 0x00008236
		internal static UMServerProxy GetServer(string fqdn)
		{
			return new UMServerProxy(fqdn);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000A03E File Offset: 0x0000823E
		internal static void ClearServerCache()
		{
			UMServerManager.PlayOnPhoneUMServerPicker.Instance.ClearServerCache();
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000A04C File Offset: 0x0000824C
		internal static bool IsAuthorizedUMServer(UMServerProxy umServerProxy)
		{
			bool result = false;
			List<IVersionedRpcTarget> registeredUMServers = UMServerManager.PlayOnPhoneUMServerPicker.Instance.GetRegisteredUMServers();
			foreach (IVersionedRpcTarget versionedRpcTarget in registeredUMServers)
			{
				UMServerProxy umserverProxy = (UMServerProxy)versionedRpcTarget;
				if (umserverProxy != null && umserverProxy.Fqdn != null && umserverProxy.Fqdn.Equals(umServerProxy.Fqdn))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x02000032 RID: 50
		private class PlayOnPhoneUMServerPicker : UMServerRpcTargetPickerBase<IVersionedRpcTarget>
		{
			// Token: 0x060002B1 RID: 689 RVA: 0x0000AAF8 File Offset: 0x00008CF8
			public void ClearServerCache()
			{
				base.RefreshServers();
			}

			// Token: 0x060002B2 RID: 690 RVA: 0x0000AB00 File Offset: 0x00008D00
			protected override IVersionedRpcTarget CreateTarget(Server server)
			{
				return new UMServerProxy(server);
			}

			// Token: 0x040000D7 RID: 215
			public static readonly UMServerManager.PlayOnPhoneUMServerPicker Instance = new UMServerManager.PlayOnPhoneUMServerPicker();
		}
	}
}
