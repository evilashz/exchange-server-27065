using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000AC9 RID: 2761
	internal class LsaPolicy
	{
		// Token: 0x06003B31 RID: 15153 RVA: 0x00098468 File Offset: 0x00096668
		internal static int GetDomainMembershipStatus(out bool memberOfDomain)
		{
			memberOfDomain = false;
			LsaNativeMethods.LsaObjectAttributes objectAttributes = new LsaNativeMethods.LsaObjectAttributes();
			SafeLsaPolicyHandle safeLsaPolicyHandle;
			int num = LsaNativeMethods.LsaOpenPolicy(null, objectAttributes, LsaNativeMethods.PolicyAccess.ViewLocalInformation, out safeLsaPolicyHandle);
			if (num != 0)
			{
				return LsaNativeMethods.LsaNtStatusToWinError(num);
			}
			SafeLsaMemoryHandle safeLsaMemoryHandle;
			num = LsaNativeMethods.LsaQueryInformationPolicy(safeLsaPolicyHandle, LsaNativeMethods.PolicyInformationClass.DnsDomainInformation, out safeLsaMemoryHandle);
			if (num != 0)
			{
				safeLsaPolicyHandle.Dispose();
				return LsaNativeMethods.LsaNtStatusToWinError(num);
			}
			LsaNativeMethods.PolicyDnsDomainInfo policyDnsDomainInfo = new LsaNativeMethods.PolicyDnsDomainInfo(safeLsaMemoryHandle);
			safeLsaPolicyHandle.Dispose();
			safeLsaMemoryHandle.Dispose();
			memberOfDomain = policyDnsDomainInfo.IsDomainMember;
			return 0;
		}
	}
}
