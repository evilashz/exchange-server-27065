using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Net.Claim;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Security.X509CertAuth
{
	// Token: 0x0200009A RID: 154
	internal sealed class X509CertUser
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x0002A07B File Offset: 0x0002827B
		private X509CertUser(X509Identifier identifier)
		{
			this.identifier = identifier;
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0002A095 File Offset: 0x00028295
		public string UserPrincipalName
		{
			get
			{
				return this.userPrincipalName;
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0002A0A0 File Offset: 0x000282A0
		public static bool TryCreateX509CertUser(ReadOnlyCollection<ClaimSet> claimSets, out X509CertUser certUser)
		{
			certUser = null;
			int count = claimSets.Count;
			if (count != 1)
			{
				ExTraceGlobals.X509CertAuthTracer.TraceDebug<int>(0L, "[X509CertUser::TryCreateX509CertUser] the given claim sets has {0} members, expect only 1", count);
				return false;
			}
			ClaimSet claimSet = claimSets[0];
			X500DistinguishedName x500DistinguishedName = null;
			foreach (Claim claim in claimSet)
			{
				if (claim.Match(ClaimTypes.X500DistinguishedName, Rights.PossessProperty))
				{
					claim.HaveProperResource(out x500DistinguishedName);
					break;
				}
			}
			if (x500DistinguishedName == null)
			{
				ExTraceGlobals.X509CertAuthTracer.TraceDebug<int>(0L, "[X509CertUser::TryCreateX509CertUser] unable to find the subject's X500DistinguishedName in the claim set.", count);
				return false;
			}
			X500DistinguishedName x500DistinguishedName2 = null;
			foreach (Claim claim2 in claimSet.Issuer)
			{
				if (claim2.Match(ClaimTypes.X500DistinguishedName, Rights.PossessProperty))
				{
					claim2.HaveProperResource(out x500DistinguishedName2);
					break;
				}
			}
			if (x500DistinguishedName2 == null)
			{
				ExTraceGlobals.X509CertAuthTracer.TraceDebug<int>(0L, "[X509CertUser::TryCreateX509CertUser] unable to find the issuer's X500DistinguishedName in the claim set.", count);
				return false;
			}
			string name = x500DistinguishedName.Name;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(2634427709U, ref name);
			X509Identifier arg = new X509Identifier(x500DistinguishedName2.Name, name);
			ExTraceGlobals.X509CertAuthTracer.TraceDebug<X509Identifier>(0L, "[X509CertUser::TryCreateX509CertUser] the X509Identifier from this claim set is {0}.", arg);
			certUser = new X509CertUser(arg);
			return true;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0002A204 File Offset: 0x00028404
		public bool TryGetWindowsIdentity(out OrganizationId organizationId, out WindowsIdentity windowsIdentity, out string errorReason)
		{
			organizationId = null;
			windowsIdentity = null;
			errorReason = null;
			string text = null;
			X509CertUserCache.ResolvedX509CertUser resolvedX509CertUser = X509CertUserCache.Singleton.Get(this.identifier);
			if (string.IsNullOrEmpty(resolvedX509CertUser.ErrorString))
			{
				try
				{
					organizationId = resolvedX509CertUser.OrganizationId;
					this.userPrincipalName = resolvedX509CertUser.UserPrincipalName;
					text = resolvedX509CertUser.ImplicitUserPrincipalName;
					windowsIdentity = new WindowsIdentity(text);
					return true;
				}
				catch (SecurityException ex)
				{
					ExTraceGlobals.X509CertAuthTracer.TraceError<X509Identifier, string, string>(0L, "[X509CertUser::TryGetWindowsPrincipal] unable to get WindowsIdentity for X509Identifier '{0}', implicit upn {1}, exception: {2}", this.identifier, text, ex.Message);
					return false;
				}
			}
			errorReason = resolvedX509CertUser.ErrorString;
			ExTraceGlobals.X509CertAuthTracer.TraceDebug<X509Identifier, string>(0L, "[X509CertUser::TryGetWindowsPrincipal] unable to get ADUser for X509Identifier '{0}', reason: {1}", this.identifier, errorReason);
			return false;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0002A2B8 File Offset: 0x000284B8
		public bool TryGetSidBasedIdentity(out OrganizationId organizationId, out SidBasedIdentity sidBasedIdentity, out string errorReason)
		{
			organizationId = null;
			sidBasedIdentity = null;
			errorReason = null;
			X509CertUserCache.ResolvedX509CertUser resolvedX509CertUser = X509CertUserCache.Singleton.Get(this.identifier);
			if (string.IsNullOrEmpty(resolvedX509CertUser.ErrorString))
			{
				sidBasedIdentity = new SidBasedIdentity(resolvedX509CertUser.UserPrincipalName, resolvedX509CertUser.Sid.ToString(), resolvedX509CertUser.UserPrincipalName);
				this.userPrincipalName = resolvedX509CertUser.UserPrincipalName;
				sidBasedIdentity.UserOrganizationId = resolvedX509CertUser.OrganizationId;
				return true;
			}
			errorReason = resolvedX509CertUser.ErrorString;
			ExTraceGlobals.X509CertAuthTracer.TraceDebug<X509Identifier, string>(0L, "[X509CertUser::TryGetSidBasedIdentity] unable to get ADUser for X509Identifier '{0}', reason: {1}", this.identifier, errorReason);
			return false;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0002A347 File Offset: 0x00028547
		public override string ToString()
		{
			return string.Format("{0}({1})", this.identifier, this.userPrincipalName);
		}

		// Token: 0x04000571 RID: 1393
		private readonly X509Identifier identifier;

		// Token: 0x04000572 RID: 1394
		private string userPrincipalName = "<unknown>";
	}
}
