using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.Security
{
	// Token: 0x020001D0 RID: 464
	internal static class CodeAccessSecurityEngine
	{
		// Token: 0x06001C50 RID: 7248
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SpecialDemand(PermissionType whatPermission, ref StackCrawlMark stackMark);

		// Token: 0x06001C51 RID: 7249 RVA: 0x000612BE File Offset: 0x0005F4BE
		[SecurityCritical]
		[Conditional("_DEBUG")]
		private static void DEBUG_OUT(string str)
		{
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x000612DC File Offset: 0x0005F4DC
		[SecurityCritical]
		private static void ThrowSecurityException(RuntimeAssembly asm, PermissionSet granted, PermissionSet refused, RuntimeMethodHandleInternal rmh, SecurityAction action, object demand, IPermission permThatFailed)
		{
			AssemblyName asmName = null;
			Evidence asmEvidence = null;
			if (asm != null)
			{
				PermissionSet.s_fullTrust.Assert();
				asmName = asm.GetName();
				if (asm != Assembly.GetExecutingAssembly())
				{
					asmEvidence = asm.Evidence;
				}
			}
			throw SecurityException.MakeSecurityException(asmName, asmEvidence, granted, refused, rmh, action, demand, permThatFailed);
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x0006132C File Offset: 0x0005F52C
		[SecurityCritical]
		private static void ThrowSecurityException(object assemblyOrString, PermissionSet granted, PermissionSet refused, RuntimeMethodHandleInternal rmh, SecurityAction action, object demand, IPermission permThatFailed)
		{
			if (assemblyOrString == null || assemblyOrString is RuntimeAssembly)
			{
				CodeAccessSecurityEngine.ThrowSecurityException((RuntimeAssembly)assemblyOrString, granted, refused, rmh, action, demand, permThatFailed);
				return;
			}
			AssemblyName asmName = new AssemblyName((string)assemblyOrString);
			throw SecurityException.MakeSecurityException(asmName, null, granted, refused, rmh, action, demand, permThatFailed);
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x00061375 File Offset: 0x0005F575
		[SecurityCritical]
		internal static void CheckSetHelper(CompressedStack cs, PermissionSet grants, PermissionSet refused, PermissionSet demands, RuntimeMethodHandleInternal rmh, RuntimeAssembly asm, SecurityAction action)
		{
			if (cs != null)
			{
				cs.CheckSetDemand(demands, rmh);
				return;
			}
			CodeAccessSecurityEngine.CheckSetHelper(grants, refused, demands, rmh, asm, action, true);
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x00061398 File Offset: 0x0005F598
		[SecurityCritical]
		internal static bool CheckSetHelper(PermissionSet grants, PermissionSet refused, PermissionSet demands, RuntimeMethodHandleInternal rmh, object assemblyOrString, SecurityAction action, bool throwException)
		{
			IPermission permThatFailed = null;
			if (grants != null)
			{
				grants.CheckDecoded(demands);
			}
			if (refused != null)
			{
				refused.CheckDecoded(demands);
			}
			bool flag = SecurityManager._SetThreadSecurity(false);
			try
			{
				if (!demands.CheckDemand(grants, out permThatFailed))
				{
					if (!throwException)
					{
						return false;
					}
					CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grants, refused, rmh, action, demands, permThatFailed);
				}
				if (!demands.CheckDeny(refused, out permThatFailed))
				{
					if (!throwException)
					{
						return false;
					}
					CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grants, refused, rmh, action, demands, permThatFailed);
				}
			}
			catch (SecurityException)
			{
				throw;
			}
			catch (Exception)
			{
				if (!throwException)
				{
					return false;
				}
				CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grants, refused, rmh, action, demands, permThatFailed);
			}
			finally
			{
				if (flag)
				{
					SecurityManager._SetThreadSecurity(true);
				}
			}
			return true;
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x00061460 File Offset: 0x0005F660
		[SecurityCritical]
		internal static void CheckHelper(CompressedStack cs, PermissionSet grantedSet, PermissionSet refusedSet, CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandleInternal rmh, RuntimeAssembly asm, SecurityAction action)
		{
			if (cs != null)
			{
				cs.CheckDemand(demand, permToken, rmh);
				return;
			}
			CodeAccessSecurityEngine.CheckHelper(grantedSet, refusedSet, demand, permToken, rmh, asm, action, true);
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x00061484 File Offset: 0x0005F684
		[SecurityCritical]
		internal static bool CheckHelper(PermissionSet grantedSet, PermissionSet refusedSet, CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandleInternal rmh, object assemblyOrString, SecurityAction action, bool throwException)
		{
			if (permToken == null)
			{
				permToken = PermissionToken.GetToken(demand);
			}
			if (grantedSet != null)
			{
				grantedSet.CheckDecoded(permToken.m_index);
			}
			if (refusedSet != null)
			{
				refusedSet.CheckDecoded(permToken.m_index);
			}
			bool flag = SecurityManager._SetThreadSecurity(false);
			try
			{
				if (grantedSet == null)
				{
					if (!throwException)
					{
						return false;
					}
					CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grantedSet, refusedSet, rmh, action, demand, demand);
				}
				else if (!grantedSet.IsUnrestricted())
				{
					CodeAccessPermission grant = (CodeAccessPermission)grantedSet.GetPermission(permToken);
					if (!demand.CheckDemand(grant))
					{
						if (!throwException)
						{
							return false;
						}
						CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grantedSet, refusedSet, rmh, action, demand, demand);
					}
				}
				if (refusedSet != null)
				{
					CodeAccessPermission codeAccessPermission = (CodeAccessPermission)refusedSet.GetPermission(permToken);
					if (codeAccessPermission != null && !codeAccessPermission.CheckDeny(demand))
					{
						if (!throwException)
						{
							return false;
						}
						CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grantedSet, refusedSet, rmh, action, demand, demand);
					}
					if (refusedSet.IsUnrestricted())
					{
						if (!throwException)
						{
							return false;
						}
						CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grantedSet, refusedSet, rmh, action, demand, demand);
					}
				}
			}
			catch (SecurityException)
			{
				throw;
			}
			catch (Exception)
			{
				if (!throwException)
				{
					return false;
				}
				CodeAccessSecurityEngine.ThrowSecurityException(assemblyOrString, grantedSet, refusedSet, rmh, action, demand, demand);
			}
			finally
			{
				if (flag)
				{
					SecurityManager._SetThreadSecurity(true);
				}
			}
			return true;
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x000615C8 File Offset: 0x0005F7C8
		[SecurityCritical]
		private static void CheckGrantSetHelper(PermissionSet grantSet)
		{
			grantSet.CopyWithNoIdentityPermissions().Demand();
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x000615D5 File Offset: 0x0005F7D5
		[SecurityCritical]
		internal static void ReflectionTargetDemandHelper(PermissionType permission, PermissionSet targetGrant)
		{
			CodeAccessSecurityEngine.ReflectionTargetDemandHelper((int)permission, targetGrant);
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x000615E0 File Offset: 0x0005F7E0
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ReflectionTargetDemandHelper(int permission, PermissionSet targetGrant)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			CompressedStack compressedStack = CompressedStack.GetCompressedStack(ref stackCrawlMark);
			CodeAccessSecurityEngine.ReflectionTargetDemandHelper(permission, targetGrant, compressedStack);
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x000615FF File Offset: 0x0005F7FF
		[SecurityCritical]
		private static void ReflectionTargetDemandHelper(int permission, PermissionSet targetGrant, Resolver accessContext)
		{
			CodeAccessSecurityEngine.ReflectionTargetDemandHelper(permission, targetGrant, accessContext.GetSecurityContext());
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x00061610 File Offset: 0x0005F810
		[SecurityCritical]
		private static void ReflectionTargetDemandHelper(int permission, PermissionSet targetGrant, CompressedStack securityContext)
		{
			PermissionSet permissionSet;
			if (targetGrant == null)
			{
				permissionSet = new PermissionSet(PermissionState.Unrestricted);
			}
			else
			{
				permissionSet = targetGrant.CopyWithNoIdentityPermissions();
				permissionSet.AddPermission(new ReflectionPermission(ReflectionPermissionFlag.RestrictedMemberAccess));
			}
			securityContext.DemandFlagsOrGrantSet(1 << permission, permissionSet);
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0006164C File Offset: 0x0005F84C
		[SecurityCritical]
		internal static void GetZoneAndOriginHelper(CompressedStack cs, PermissionSet grantSet, PermissionSet refusedSet, ArrayList zoneList, ArrayList originList)
		{
			if (cs != null)
			{
				cs.GetZoneAndOrigin(zoneList, originList, PermissionToken.GetToken(typeof(ZoneIdentityPermission)), PermissionToken.GetToken(typeof(UrlIdentityPermission)));
				return;
			}
			ZoneIdentityPermission zoneIdentityPermission = (ZoneIdentityPermission)grantSet.GetPermission(typeof(ZoneIdentityPermission));
			UrlIdentityPermission urlIdentityPermission = (UrlIdentityPermission)grantSet.GetPermission(typeof(UrlIdentityPermission));
			if (zoneIdentityPermission != null)
			{
				zoneList.Add(zoneIdentityPermission.SecurityZone);
			}
			if (urlIdentityPermission != null)
			{
				originList.Add(urlIdentityPermission.Url);
			}
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x000616D6 File Offset: 0x0005F8D6
		[SecurityCritical]
		internal static void GetZoneAndOrigin(ref StackCrawlMark mark, out ArrayList zone, out ArrayList origin)
		{
			zone = new ArrayList();
			origin = new ArrayList();
			CodeAccessSecurityEngine.GetZoneAndOriginInternal(zone, origin, ref mark);
		}

		// Token: 0x06001C60 RID: 7264
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetZoneAndOriginInternal(ArrayList zoneList, ArrayList originList, ref StackCrawlMark stackMark);

		// Token: 0x06001C61 RID: 7265 RVA: 0x000616F0 File Offset: 0x0005F8F0
		[SecurityCritical]
		internal static void CheckAssembly(RuntimeAssembly asm, CodeAccessPermission demand)
		{
			PermissionSet grantedSet;
			PermissionSet refusedSet;
			asm.GetGrantSet(out grantedSet, out refusedSet);
			CodeAccessSecurityEngine.CheckHelper(grantedSet, refusedSet, demand, PermissionToken.GetToken(demand), RuntimeMethodHandleInternal.EmptyHandle, asm, SecurityAction.Demand, true);
		}

		// Token: 0x06001C62 RID: 7266
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Check(object demand, ref StackCrawlMark stackMark, bool isPermSet);

		// Token: 0x06001C63 RID: 7267
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool QuickCheckForAllDemands();

		// Token: 0x06001C64 RID: 7268
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool AllDomainsHomogeneousWithNoStackModifiers();

		// Token: 0x06001C65 RID: 7269 RVA: 0x0006171E File Offset: 0x0005F91E
		[SecurityCritical]
		internal static void Check(CodeAccessPermission cap, ref StackCrawlMark stackMark)
		{
			CodeAccessSecurityEngine.Check(cap, ref stackMark, false);
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x00061728 File Offset: 0x0005F928
		[SecurityCritical]
		internal static void Check(PermissionSet permSet, ref StackCrawlMark stackMark)
		{
			CodeAccessSecurityEngine.Check(permSet, ref stackMark, true);
		}

		// Token: 0x06001C67 RID: 7271
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern FrameSecurityDescriptor CheckNReturnSO(PermissionToken permToken, CodeAccessPermission demand, ref StackCrawlMark stackMark, int create);

		// Token: 0x06001C68 RID: 7272 RVA: 0x00061734 File Offset: 0x0005F934
		[SecurityCritical]
		internal static void Assert(CodeAccessPermission cap, ref StackCrawlMark stackMark)
		{
			FrameSecurityDescriptor frameSecurityDescriptor = CodeAccessSecurityEngine.CheckNReturnSO(CodeAccessSecurityEngine.AssertPermissionToken, CodeAccessSecurityEngine.AssertPermission, ref stackMark, 1);
			if (frameSecurityDescriptor == null)
			{
				Environment.FailFast(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
				return;
			}
			if (frameSecurityDescriptor.HasImperativeAsserts())
			{
				throw new SecurityException(Environment.GetResourceString("Security_MustRevertOverride"));
			}
			frameSecurityDescriptor.SetAssert(cap);
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x00061788 File Offset: 0x0005F988
		[SecurityCritical]
		internal static void Deny(CodeAccessPermission cap, ref StackCrawlMark stackMark)
		{
			if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_CasDeny"));
			}
			FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, true);
			if (securityObjectForFrame == null)
			{
				Environment.FailFast(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
				return;
			}
			if (securityObjectForFrame.HasImperativeDenials())
			{
				throw new SecurityException(Environment.GetResourceString("Security_MustRevertOverride"));
			}
			securityObjectForFrame.SetDeny(cap);
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x000617EC File Offset: 0x0005F9EC
		[SecurityCritical]
		internal static void PermitOnly(CodeAccessPermission cap, ref StackCrawlMark stackMark)
		{
			FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, true);
			if (securityObjectForFrame == null)
			{
				Environment.FailFast(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
				return;
			}
			if (securityObjectForFrame.HasImperativeRestrictions())
			{
				throw new SecurityException(Environment.GetResourceString("Security_MustRevertOverride"));
			}
			securityObjectForFrame.SetPermitOnly(cap);
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00061834 File Offset: 0x0005FA34
		private static void PreResolve(out bool isFullyTrusted, out bool isHomogeneous)
		{
			ApplicationTrust applicationTrust = AppDomain.CurrentDomain.SetupInformation.ApplicationTrust;
			if (applicationTrust != null)
			{
				isFullyTrusted = applicationTrust.DefaultGrantSet.PermissionSet.IsUnrestricted();
				isHomogeneous = true;
				return;
			}
			if (CompatibilitySwitches.IsNetFx40LegacySecurityPolicy || AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				isFullyTrusted = false;
				isHomogeneous = false;
				return;
			}
			isFullyTrusted = true;
			isHomogeneous = true;
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x0006188C File Offset: 0x0005FA8C
		private static PermissionSet ResolveGrantSet(Evidence evidence, out int specialFlags, bool checkExecutionPermission)
		{
			PermissionSet permissionSet = null;
			if (!CodeAccessSecurityEngine.TryResolveGrantSet(evidence, out permissionSet))
			{
				permissionSet = new PermissionSet(PermissionState.Unrestricted);
			}
			if (checkExecutionPermission)
			{
				SecurityPermission perm = new SecurityPermission(SecurityPermissionFlag.Execution);
				if (!permissionSet.Contains(perm))
				{
					throw new PolicyException(Environment.GetResourceString("Policy_NoExecutionPermission"), -2146233320);
				}
			}
			specialFlags = SecurityManager.GetSpecialFlags(permissionSet, null);
			return permissionSet;
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x000618E0 File Offset: 0x0005FAE0
		[SecuritySafeCritical]
		internal static bool TryResolveGrantSet(Evidence evidence, out PermissionSet grantSet)
		{
			HostSecurityManager hostSecurityManager = AppDomain.CurrentDomain.HostSecurityManager;
			if (evidence.GetHostEvidence<GacInstalled>() != null)
			{
				grantSet = new PermissionSet(PermissionState.Unrestricted);
				return true;
			}
			if ((hostSecurityManager.Flags & HostSecurityManagerOptions.HostResolvePolicy) == HostSecurityManagerOptions.HostResolvePolicy)
			{
				PermissionSet permissionSet = hostSecurityManager.ResolvePolicy(evidence);
				if (permissionSet == null)
				{
					throw new PolicyException(Environment.GetResourceString("Policy_NullHostGrantSet", new object[]
					{
						hostSecurityManager.GetType().FullName
					}));
				}
				if (AppDomain.CurrentDomain.IsHomogenous)
				{
					if (permissionSet.IsEmpty())
					{
						throw new PolicyException(Environment.GetResourceString("Policy_NoExecutionPermission"));
					}
					PermissionSet permissionSet2 = AppDomain.CurrentDomain.ApplicationTrust.DefaultGrantSet.PermissionSet;
					if (!permissionSet.IsUnrestricted() && (!permissionSet.IsSubsetOf(permissionSet2) || !permissionSet2.IsSubsetOf(permissionSet)))
					{
						throw new PolicyException(Environment.GetResourceString("Policy_GrantSetDoesNotMatchDomain", new object[]
						{
							hostSecurityManager.GetType().FullName
						}));
					}
				}
				grantSet = permissionSet;
				return true;
			}
			else
			{
				if (AppDomain.CurrentDomain.IsHomogenous)
				{
					grantSet = AppDomain.CurrentDomain.GetHomogenousGrantSet(evidence);
					return true;
				}
				grantSet = null;
				return false;
			}
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x000619EC File Offset: 0x0005FBEC
		[SecurityCritical]
		private static PermissionListSet UpdateAppDomainPLS(PermissionListSet adPLS, PermissionSet grantedPerms, PermissionSet refusedPerms)
		{
			if (adPLS == null)
			{
				adPLS = new PermissionListSet();
				adPLS.UpdateDomainPLS(grantedPerms, refusedPerms);
				return adPLS;
			}
			PermissionListSet permissionListSet = new PermissionListSet();
			permissionListSet.UpdateDomainPLS(adPLS);
			permissionListSet.UpdateDomainPLS(grantedPerms, refusedPerms);
			return permissionListSet;
		}

		// Token: 0x040009EE RID: 2542
		internal static SecurityPermission AssertPermission = new SecurityPermission(SecurityPermissionFlag.Assertion);

		// Token: 0x040009EF RID: 2543
		internal static PermissionToken AssertPermissionToken = PermissionToken.GetToken(CodeAccessSecurityEngine.AssertPermission);
	}
}
