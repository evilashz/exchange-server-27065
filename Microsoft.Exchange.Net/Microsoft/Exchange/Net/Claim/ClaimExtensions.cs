using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net.Claim
{
	// Token: 0x02000687 RID: 1671
	internal static class ClaimExtensions
	{
		// Token: 0x06001E40 RID: 7744 RVA: 0x000379C0 File Offset: 0x00035BC0
		public static bool PossessClaimType(this ReadOnlyCollection<ClaimSet> claimSets, string desiredClaimType)
		{
			foreach (ClaimSet claimSet in claimSets)
			{
				foreach (Claim claim in claimSet)
				{
					if (claim.Match(desiredClaimType, Rights.PossessProperty))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x00037A4C File Offset: 0x00035C4C
		public static bool Match(this Claim claim, string claimTypeToTest, string rightToTest)
		{
			return claim.ClaimType == claimTypeToTest && claim.Right == rightToTest;
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x00037A6C File Offset: 0x00035C6C
		public static bool HaveProperResource<T>(this Claim claim, out T resource) where T : class
		{
			resource = default(T);
			if (claim.Resource == null)
			{
				ExTraceGlobals.AuthenticationTracer.TraceDebug<string, string>(0L, "[ClaimHelper::HaveProperResource] {0}/{1} claim had a null Resource", claim.ClaimType, claim.Right);
				return false;
			}
			resource = (claim.Resource as T);
			if (resource == null)
			{
				ExTraceGlobals.ClaimTracer.TraceDebug(0L, "[ClaimHelper::HaveProperResource] {0}/{1} claim had a claim of type {2}, not of type {3}", new object[]
				{
					claim.ClaimType,
					claim.Right,
					claim.Resource.GetType().FullName,
					typeof(T).FullName
				});
				return false;
			}
			return true;
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x00037B1D File Offset: 0x00035D1D
		public static void TraceClaimSets(this ReadOnlyCollection<ClaimSet> claimSets)
		{
			if (!ExTraceGlobals.ClaimTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				return;
			}
			ExTraceGlobals.ClaimTracer.TraceDebug<string>(0L, "ClaimSets: {0}", claimSets.GetTraceString());
		}
	}
}
