using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Security
{
	// Token: 0x020001E4 RID: 484
	[Serializable]
	internal sealed class PermissionListSet
	{
		// Token: 0x06001D4D RID: 7501 RVA: 0x00065D6B File Offset: 0x00063F6B
		internal PermissionListSet()
		{
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x00065D73 File Offset: 0x00063F73
		private void EnsureTriplesListCreated()
		{
			if (this.m_permSetTriples == null)
			{
				this.m_permSetTriples = new ArrayList();
				if (this.m_firstPermSetTriple != null)
				{
					this.m_permSetTriples.Add(this.m_firstPermSetTriple);
					this.m_firstPermSetTriple = null;
				}
			}
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x00065DA9 File Offset: 0x00063FA9
		[SecurityCritical]
		internal void UpdateDomainPLS(PermissionListSet adPLS)
		{
			if (adPLS != null && adPLS.m_firstPermSetTriple != null)
			{
				this.UpdateDomainPLS(adPLS.m_firstPermSetTriple.GrantSet, adPLS.m_firstPermSetTriple.RefusedSet);
			}
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x00065DD2 File Offset: 0x00063FD2
		[SecurityCritical]
		internal void UpdateDomainPLS(PermissionSet grantSet, PermissionSet deniedSet)
		{
			if (this.m_firstPermSetTriple == null)
			{
				this.m_firstPermSetTriple = new PermissionSetTriple();
			}
			this.m_firstPermSetTriple.UpdateGrant(grantSet);
			this.m_firstPermSetTriple.UpdateRefused(deniedSet);
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x00065DFF File Offset: 0x00063FFF
		private void Terminate(PermissionSetTriple currentTriple)
		{
			this.UpdateTripleListAndCreateNewTriple(currentTriple, null);
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x00065E09 File Offset: 0x00064009
		[SecurityCritical]
		private void Terminate(PermissionSetTriple currentTriple, PermissionListSet pls)
		{
			this.UpdateZoneAndOrigin(pls);
			this.UpdatePermissions(currentTriple, pls);
			this.UpdateTripleListAndCreateNewTriple(currentTriple, null);
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x00065E23 File Offset: 0x00064023
		[SecurityCritical]
		private bool Update(PermissionSetTriple currentTriple, PermissionListSet pls)
		{
			this.UpdateZoneAndOrigin(pls);
			return this.UpdatePermissions(currentTriple, pls);
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x00065E34 File Offset: 0x00064034
		[SecurityCritical]
		private bool Update(PermissionSetTriple currentTriple, FrameSecurityDescriptor fsd)
		{
			FrameSecurityDescriptorWithResolver frameSecurityDescriptorWithResolver = fsd as FrameSecurityDescriptorWithResolver;
			if (frameSecurityDescriptorWithResolver != null)
			{
				return this.Update2(currentTriple, frameSecurityDescriptorWithResolver);
			}
			bool flag = this.Update2(currentTriple, fsd, false);
			if (!flag)
			{
				flag = this.Update2(currentTriple, fsd, true);
			}
			return flag;
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x00065E6C File Offset: 0x0006406C
		[SecurityCritical]
		private bool Update2(PermissionSetTriple currentTriple, FrameSecurityDescriptorWithResolver fsdWithResolver)
		{
			DynamicResolver resolver = fsdWithResolver.Resolver;
			CompressedStack securityContext = resolver.GetSecurityContext();
			securityContext.CompleteConstruction(null);
			return this.Update(currentTriple, securityContext.PLS);
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x00065E9C File Offset: 0x0006409C
		[SecurityCritical]
		private bool Update2(PermissionSetTriple currentTriple, FrameSecurityDescriptor fsd, bool fDeclarative)
		{
			PermissionSet denials = fsd.GetDenials(fDeclarative);
			if (denials != null)
			{
				currentTriple.UpdateRefused(denials);
			}
			PermissionSet permitOnly = fsd.GetPermitOnly(fDeclarative);
			if (permitOnly != null)
			{
				currentTriple.UpdateGrant(permitOnly);
			}
			if (fsd.GetAssertAllPossible())
			{
				if (currentTriple.GrantSet == null)
				{
					currentTriple.GrantSet = PermissionSet.s_fullTrust;
				}
				this.UpdateTripleListAndCreateNewTriple(currentTriple, this.m_permSetTriples);
				currentTriple.GrantSet = PermissionSet.s_fullTrust;
				currentTriple.UpdateAssert(fsd.GetAssertions(fDeclarative));
				return true;
			}
			PermissionSet assertions = fsd.GetAssertions(fDeclarative);
			if (assertions != null)
			{
				if (assertions.IsUnrestricted())
				{
					if (currentTriple.GrantSet == null)
					{
						currentTriple.GrantSet = PermissionSet.s_fullTrust;
					}
					this.UpdateTripleListAndCreateNewTriple(currentTriple, this.m_permSetTriples);
					currentTriple.GrantSet = PermissionSet.s_fullTrust;
					currentTriple.UpdateAssert(assertions);
					return true;
				}
				PermissionSetTriple permissionSetTriple = currentTriple.UpdateAssert(assertions);
				if (permissionSetTriple != null)
				{
					this.EnsureTriplesListCreated();
					this.m_permSetTriples.Add(permissionSetTriple);
				}
			}
			return false;
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x00065F78 File Offset: 0x00064178
		[SecurityCritical]
		private void Update(PermissionSetTriple currentTriple, PermissionSet in_g, PermissionSet in_r)
		{
			ZoneIdentityPermission z;
			UrlIdentityPermission u;
			currentTriple.UpdateGrant(in_g, out z, out u);
			currentTriple.UpdateRefused(in_r);
			this.AppendZoneOrigin(z, u);
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x00065F9F File Offset: 0x0006419F
		[SecurityCritical]
		private void Update(PermissionSet in_g)
		{
			if (this.m_firstPermSetTriple == null)
			{
				this.m_firstPermSetTriple = new PermissionSetTriple();
			}
			this.Update(this.m_firstPermSetTriple, in_g, null);
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x00065FC4 File Offset: 0x000641C4
		private void UpdateZoneAndOrigin(PermissionListSet pls)
		{
			if (pls != null)
			{
				if (this.m_zoneList == null && pls.m_zoneList != null && pls.m_zoneList.Count > 0)
				{
					this.m_zoneList = new ArrayList();
				}
				PermissionListSet.UpdateArrayList(this.m_zoneList, pls.m_zoneList);
				if (this.m_originList == null && pls.m_originList != null && pls.m_originList.Count > 0)
				{
					this.m_originList = new ArrayList();
				}
				PermissionListSet.UpdateArrayList(this.m_originList, pls.m_originList);
			}
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x00066048 File Offset: 0x00064248
		[SecurityCritical]
		private bool UpdatePermissions(PermissionSetTriple currentTriple, PermissionListSet pls)
		{
			if (pls != null)
			{
				if (pls.m_permSetTriples != null)
				{
					this.UpdateTripleListAndCreateNewTriple(currentTriple, pls.m_permSetTriples);
				}
				else
				{
					PermissionSetTriple firstPermSetTriple = pls.m_firstPermSetTriple;
					PermissionSetTriple permissionSetTriple;
					if (currentTriple.Update(firstPermSetTriple, out permissionSetTriple))
					{
						return true;
					}
					if (permissionSetTriple != null)
					{
						this.EnsureTriplesListCreated();
						this.m_permSetTriples.Add(permissionSetTriple);
					}
				}
			}
			else
			{
				this.UpdateTripleListAndCreateNewTriple(currentTriple, null);
			}
			return false;
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x000660A4 File Offset: 0x000642A4
		private void UpdateTripleListAndCreateNewTriple(PermissionSetTriple currentTriple, ArrayList tripleList)
		{
			if (!currentTriple.IsEmpty())
			{
				if (this.m_firstPermSetTriple == null && this.m_permSetTriples == null)
				{
					this.m_firstPermSetTriple = new PermissionSetTriple(currentTriple);
				}
				else
				{
					this.EnsureTriplesListCreated();
					this.m_permSetTriples.Add(new PermissionSetTriple(currentTriple));
				}
				currentTriple.Reset();
			}
			if (tripleList != null)
			{
				this.EnsureTriplesListCreated();
				this.m_permSetTriples.AddRange(tripleList);
			}
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x0006610C File Offset: 0x0006430C
		private static void UpdateArrayList(ArrayList current, ArrayList newList)
		{
			if (newList == null)
			{
				return;
			}
			for (int i = 0; i < newList.Count; i++)
			{
				if (!current.Contains(newList[i]))
				{
					current.Add(newList[i]);
				}
			}
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x0006614C File Offset: 0x0006434C
		private void AppendZoneOrigin(ZoneIdentityPermission z, UrlIdentityPermission u)
		{
			if (z != null)
			{
				if (this.m_zoneList == null)
				{
					this.m_zoneList = new ArrayList();
				}
				z.AppendZones(this.m_zoneList);
			}
			if (u != null)
			{
				if (this.m_originList == null)
				{
					this.m_originList = new ArrayList();
				}
				u.AppendOrigin(this.m_originList);
			}
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x000661A0 File Offset: 0x000643A0
		[SecurityCritical]
		[ComVisible(true)]
		internal static PermissionListSet CreateCompressedState(CompressedStack cs, CompressedStack innerCS)
		{
			bool flag = false;
			if (cs.CompressedStackHandle == null)
			{
				return null;
			}
			PermissionListSet permissionListSet = new PermissionListSet();
			PermissionSetTriple currentTriple = new PermissionSetTriple();
			int dcscount = CompressedStack.GetDCSCount(cs.CompressedStackHandle);
			int num = dcscount - 1;
			while (num >= 0 && !flag)
			{
				DomainCompressedStack domainCompressedStack = CompressedStack.GetDomainCompressedStack(cs.CompressedStackHandle, num);
				if (domainCompressedStack != null)
				{
					if (domainCompressedStack.PLS == null)
					{
						throw new SecurityException(string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Security_Generic"), Array.Empty<object>()));
					}
					permissionListSet.UpdateZoneAndOrigin(domainCompressedStack.PLS);
					permissionListSet.Update(currentTriple, domainCompressedStack.PLS);
					flag = domainCompressedStack.ConstructionHalted;
				}
				num--;
			}
			if (!flag)
			{
				PermissionListSet pls = null;
				if (innerCS != null)
				{
					innerCS.CompleteConstruction(null);
					pls = innerCS.PLS;
				}
				permissionListSet.Terminate(currentTriple, pls);
			}
			else
			{
				permissionListSet.Terminate(currentTriple);
			}
			return permissionListSet;
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x00066270 File Offset: 0x00064470
		[SecurityCritical]
		internal static PermissionListSet CreateCompressedState(IntPtr unmanagedDCS, out bool bHaltConstruction)
		{
			PermissionListSet permissionListSet = new PermissionListSet();
			PermissionSetTriple currentTriple = new PermissionSetTriple();
			int descCount = DomainCompressedStack.GetDescCount(unmanagedDCS);
			bHaltConstruction = false;
			int num = 0;
			while (num < descCount && !bHaltConstruction)
			{
				PermissionSet in_g;
				PermissionSet in_r;
				Assembly assembly;
				FrameSecurityDescriptor fsd;
				if (DomainCompressedStack.GetDescriptorInfo(unmanagedDCS, num, out in_g, out in_r, out assembly, out fsd))
				{
					bHaltConstruction = permissionListSet.Update(currentTriple, fsd);
				}
				else
				{
					permissionListSet.Update(currentTriple, in_g, in_r);
				}
				num++;
			}
			if (!bHaltConstruction && !DomainCompressedStack.IgnoreDomain(unmanagedDCS))
			{
				PermissionSet in_g;
				PermissionSet in_r;
				DomainCompressedStack.GetDomainPermissionSets(unmanagedDCS, out in_g, out in_r);
				permissionListSet.Update(currentTriple, in_g, in_r);
			}
			permissionListSet.Terminate(currentTriple);
			return permissionListSet;
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x000662F8 File Offset: 0x000644F8
		[SecurityCritical]
		internal static PermissionListSet CreateCompressedState_HG()
		{
			PermissionListSet permissionListSet = new PermissionListSet();
			CompressedStack.GetHomogeneousPLS(permissionListSet);
			return permissionListSet;
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x00066314 File Offset: 0x00064514
		[SecurityCritical]
		internal bool CheckDemandNoThrow(CodeAccessPermission demand)
		{
			PermissionToken permToken = null;
			if (demand != null)
			{
				permToken = PermissionToken.GetToken(demand);
			}
			return this.m_firstPermSetTriple.CheckDemandNoThrow(demand, permToken);
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x0006633A File Offset: 0x0006453A
		[SecurityCritical]
		internal bool CheckSetDemandNoThrow(PermissionSet pSet)
		{
			return this.m_firstPermSetTriple.CheckSetDemandNoThrow(pSet);
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x00066348 File Offset: 0x00064548
		[SecurityCritical]
		internal bool CheckDemand(CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandleInternal rmh)
		{
			bool flag = true;
			if (this.m_permSetTriples != null)
			{
				int num = 0;
				while (num < this.m_permSetTriples.Count && flag)
				{
					PermissionSetTriple permissionSetTriple = (PermissionSetTriple)this.m_permSetTriples[num];
					flag = permissionSetTriple.CheckDemand(demand, permToken, rmh);
					num++;
				}
			}
			else if (this.m_firstPermSetTriple != null)
			{
				flag = this.m_firstPermSetTriple.CheckDemand(demand, permToken, rmh);
			}
			return flag;
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x000663B0 File Offset: 0x000645B0
		[SecurityCritical]
		internal bool CheckSetDemand(PermissionSet pset, RuntimeMethodHandleInternal rmh)
		{
			PermissionSet permissionSet;
			this.CheckSetDemandWithModification(pset, out permissionSet, rmh);
			return false;
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x000663CC File Offset: 0x000645CC
		[SecurityCritical]
		internal bool CheckSetDemandWithModification(PermissionSet pset, out PermissionSet alteredDemandSet, RuntimeMethodHandleInternal rmh)
		{
			bool flag = true;
			PermissionSet demandSet = pset;
			alteredDemandSet = null;
			if (this.m_permSetTriples != null)
			{
				int num = 0;
				while (num < this.m_permSetTriples.Count && flag)
				{
					PermissionSetTriple permissionSetTriple = (PermissionSetTriple)this.m_permSetTriples[num];
					flag = permissionSetTriple.CheckSetDemand(demandSet, out alteredDemandSet, rmh);
					if (alteredDemandSet != null)
					{
						demandSet = alteredDemandSet;
					}
					num++;
				}
			}
			else if (this.m_firstPermSetTriple != null)
			{
				flag = this.m_firstPermSetTriple.CheckSetDemand(demandSet, out alteredDemandSet, rmh);
			}
			return flag;
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x00066440 File Offset: 0x00064640
		[SecurityCritical]
		private bool CheckFlags(int flags)
		{
			bool flag = true;
			if (this.m_permSetTriples != null)
			{
				int num = 0;
				while (num < this.m_permSetTriples.Count && flag)
				{
					if (flags == 0)
					{
						break;
					}
					flag &= ((PermissionSetTriple)this.m_permSetTriples[num]).CheckFlags(ref flags);
					num++;
				}
			}
			else if (this.m_firstPermSetTriple != null)
			{
				flag = this.m_firstPermSetTriple.CheckFlags(ref flags);
			}
			return flag;
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x000664A8 File Offset: 0x000646A8
		[SecurityCritical]
		internal void DemandFlagsOrGrantSet(int flags, PermissionSet grantSet)
		{
			if (this.CheckFlags(flags))
			{
				return;
			}
			this.CheckSetDemand(grantSet, RuntimeMethodHandleInternal.EmptyHandle);
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x000664C1 File Offset: 0x000646C1
		internal void GetZoneAndOrigin(ArrayList zoneList, ArrayList originList, PermissionToken zoneToken, PermissionToken originToken)
		{
			if (this.m_zoneList != null)
			{
				zoneList.AddRange(this.m_zoneList);
			}
			if (this.m_originList != null)
			{
				originList.AddRange(this.m_originList);
			}
		}

		// Token: 0x04000A40 RID: 2624
		private PermissionSetTriple m_firstPermSetTriple;

		// Token: 0x04000A41 RID: 2625
		private ArrayList m_permSetTriples;

		// Token: 0x04000A42 RID: 2626
		private ArrayList m_zoneList;

		// Token: 0x04000A43 RID: 2627
		private ArrayList m_originList;
	}
}
