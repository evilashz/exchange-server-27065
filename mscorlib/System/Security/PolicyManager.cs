﻿using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Util;
using System.Text;
using System.Threading;

namespace System.Security
{
	// Token: 0x020001E5 RID: 485
	internal class PolicyManager
	{
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001D69 RID: 7529 RVA: 0x000664EC File Offset: 0x000646EC
		private IList PolicyLevels
		{
			[SecurityCritical]
			get
			{
				if (this.m_policyLevels == null)
				{
					ArrayList arrayList = new ArrayList();
					string locationFromType = PolicyLevel.GetLocationFromType(PolicyLevelType.Enterprise);
					arrayList.Add(new PolicyLevel(PolicyLevelType.Enterprise, locationFromType, ConfigId.EnterprisePolicyLevel));
					string locationFromType2 = PolicyLevel.GetLocationFromType(PolicyLevelType.Machine);
					arrayList.Add(new PolicyLevel(PolicyLevelType.Machine, locationFromType2, ConfigId.MachinePolicyLevel));
					if (Config.UserDirectory != null)
					{
						string locationFromType3 = PolicyLevel.GetLocationFromType(PolicyLevelType.User);
						arrayList.Add(new PolicyLevel(PolicyLevelType.User, locationFromType3, ConfigId.UserPolicyLevel));
					}
					Interlocked.CompareExchange(ref this.m_policyLevels, arrayList, null);
				}
				return this.m_policyLevels as ArrayList;
			}
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x00066569 File Offset: 0x00064769
		internal PolicyManager()
		{
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x00066571 File Offset: 0x00064771
		[SecurityCritical]
		internal void AddLevel(PolicyLevel level)
		{
			this.PolicyLevels.Add(level);
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x00066580 File Offset: 0x00064780
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
		internal IEnumerator PolicyHierarchy()
		{
			return this.PolicyLevels.GetEnumerator();
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x00066590 File Offset: 0x00064790
		[SecurityCritical]
		internal PermissionSet Resolve(Evidence evidence)
		{
			PermissionSet result = null;
			if (CodeAccessSecurityEngine.TryResolveGrantSet(evidence, out result))
			{
				return result;
			}
			return this.CodeGroupResolve(evidence, false);
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x000665B4 File Offset: 0x000647B4
		[SecurityCritical]
		internal PermissionSet CodeGroupResolve(Evidence evidence, bool systemPolicy)
		{
			PermissionSet permissionSet = null;
			IEnumerator enumerator = this.PolicyLevels.GetEnumerator();
			evidence.GetHostEvidence<Zone>();
			evidence.GetHostEvidence<StrongName>();
			evidence.GetHostEvidence<Url>();
			byte[] serializedEvidence = evidence.RawSerialize();
			int rawCount = evidence.RawCount;
			bool flag = AppDomain.CurrentDomain.GetData("IgnoreSystemPolicy") != null;
			bool flag2 = false;
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				PolicyLevel policyLevel = (PolicyLevel)obj;
				if (systemPolicy)
				{
					if (policyLevel.Type == PolicyLevelType.AppDomain)
					{
						continue;
					}
				}
				else if (flag && policyLevel.Type != PolicyLevelType.AppDomain)
				{
					continue;
				}
				PolicyStatement policyStatement = policyLevel.Resolve(evidence, rawCount, serializedEvidence);
				if (permissionSet == null)
				{
					permissionSet = policyStatement.PermissionSet;
				}
				else
				{
					permissionSet.InplaceIntersect(policyStatement.GetPermissionSetNoCopy());
				}
				if (permissionSet == null || permissionSet.FastIsEmpty())
				{
					break;
				}
				if ((policyStatement.Attributes & PolicyStatementAttribute.LevelFinal) == PolicyStatementAttribute.LevelFinal)
				{
					if (policyLevel.Type != PolicyLevelType.AppDomain)
					{
						flag2 = true;
						break;
					}
					break;
				}
			}
			if (permissionSet != null && flag2)
			{
				PolicyLevel policyLevel2 = null;
				for (int i = this.PolicyLevels.Count - 1; i >= 0; i--)
				{
					PolicyLevel policyLevel = (PolicyLevel)this.PolicyLevels[i];
					if (policyLevel.Type == PolicyLevelType.AppDomain)
					{
						policyLevel2 = policyLevel;
						break;
					}
				}
				if (policyLevel2 != null)
				{
					PolicyStatement policyStatement = policyLevel2.Resolve(evidence, rawCount, serializedEvidence);
					permissionSet.InplaceIntersect(policyStatement.GetPermissionSetNoCopy());
				}
			}
			if (permissionSet == null)
			{
				permissionSet = new PermissionSet(PermissionState.None);
			}
			if (!permissionSet.IsUnrestricted())
			{
				IEnumerator hostEnumerator = evidence.GetHostEnumerator();
				while (hostEnumerator.MoveNext())
				{
					object obj2 = hostEnumerator.Current;
					IIdentityPermissionFactory identityPermissionFactory = obj2 as IIdentityPermissionFactory;
					if (identityPermissionFactory != null)
					{
						IPermission permission = identityPermissionFactory.CreateIdentityPermission(evidence);
						if (permission != null)
						{
							permissionSet.AddPermission(permission);
						}
					}
				}
			}
			permissionSet.IgnoreTypeLoadFailures = true;
			return permissionSet;
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x00066746 File Offset: 0x00064946
		internal static bool IsGacAssembly(Evidence evidence)
		{
			return new GacMembershipCondition().Check(evidence);
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x00066754 File Offset: 0x00064954
		[SecurityCritical]
		internal IEnumerator ResolveCodeGroups(Evidence evidence)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.PolicyLevels)
			{
				CodeGroup codeGroup = ((PolicyLevel)obj).ResolveMatchingCodeGroups(evidence);
				if (codeGroup != null)
				{
					arrayList.Add(codeGroup);
				}
			}
			return arrayList.GetEnumerator(0, arrayList.Count);
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x000667A7 File Offset: 0x000649A7
		internal static PolicyStatement ResolveCodeGroup(CodeGroup codeGroup, Evidence evidence)
		{
			if (codeGroup.GetType().Assembly != typeof(UnionCodeGroup).Assembly)
			{
				evidence.MarkAllEvidenceAsUsed();
			}
			return codeGroup.Resolve(evidence);
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x000667D8 File Offset: 0x000649D8
		internal static bool CheckMembershipCondition(IMembershipCondition membershipCondition, Evidence evidence, out object usedEvidence)
		{
			IReportMatchMembershipCondition reportMatchMembershipCondition = membershipCondition as IReportMatchMembershipCondition;
			if (reportMatchMembershipCondition != null)
			{
				return reportMatchMembershipCondition.Check(evidence, out usedEvidence);
			}
			usedEvidence = null;
			evidence.MarkAllEvidenceAsUsed();
			return membershipCondition.Check(evidence);
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x00066808 File Offset: 0x00064A08
		[SecurityCritical]
		internal void Save()
		{
			this.EncodeLevel(Environment.GetResourceString("Policy_PL_Enterprise"));
			this.EncodeLevel(Environment.GetResourceString("Policy_PL_Machine"));
			this.EncodeLevel(Environment.GetResourceString("Policy_PL_User"));
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x0006683C File Offset: 0x00064A3C
		[SecurityCritical]
		private void EncodeLevel(string label)
		{
			for (int i = 0; i < this.PolicyLevels.Count; i++)
			{
				PolicyLevel policyLevel = (PolicyLevel)this.PolicyLevels[i];
				if (policyLevel.Label.Equals(label))
				{
					PolicyManager.EncodeLevel(policyLevel);
					return;
				}
			}
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x00066888 File Offset: 0x00064A88
		[SecurityCritical]
		internal static void EncodeLevel(PolicyLevel level)
		{
			if (level.Path == null)
			{
				string resourceString = Environment.GetResourceString("Policy_UnableToSave", new object[]
				{
					level.Label,
					Environment.GetResourceString("Policy_SaveNotFileBased")
				});
				throw new PolicyException(resourceString);
			}
			SecurityElement securityElement = new SecurityElement("configuration");
			SecurityElement securityElement2 = new SecurityElement("mscorlib");
			SecurityElement securityElement3 = new SecurityElement("security");
			SecurityElement securityElement4 = new SecurityElement("policy");
			securityElement.AddChild(securityElement2);
			securityElement2.AddChild(securityElement3);
			securityElement3.AddChild(securityElement4);
			securityElement4.AddChild(level.ToXml());
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				Encoding utf = Encoding.UTF8;
				SecurityElement securityElement5 = new SecurityElement("xml");
				securityElement5.m_type = SecurityElementType.Format;
				securityElement5.AddAttribute("version", "1.0");
				securityElement5.AddAttribute("encoding", utf.WebName);
				stringBuilder.Append(securityElement5.ToString());
				stringBuilder.Append(securityElement.ToString());
				byte[] bytes = utf.GetBytes(stringBuilder.ToString());
				int errorCode = Config.SaveDataByte(level.Path, bytes, bytes.Length);
				Exception exceptionForHR = Marshal.GetExceptionForHR(errorCode);
				if (exceptionForHR != null)
				{
					string text = (exceptionForHR != null) ? exceptionForHR.Message : string.Empty;
					throw new PolicyException(Environment.GetResourceString("Policy_UnableToSave", new object[]
					{
						level.Label,
						text
					}), exceptionForHR);
				}
			}
			catch (Exception ex)
			{
				if (ex is PolicyException)
				{
					throw ex;
				}
				throw new PolicyException(Environment.GetResourceString("Policy_UnableToSave", new object[]
				{
					level.Label,
					ex.Message
				}), ex);
			}
			Config.ResetCacheData(level.ConfigId);
			if (PolicyManager.CanUseQuickCache(level.RootCodeGroup))
			{
				Config.SetQuickCache(level.ConfigId, PolicyManager.GenerateQuickCache(level));
			}
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x00066A5C File Offset: 0x00064C5C
		internal static bool CanUseQuickCache(CodeGroup group)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(group);
			for (int i = 0; i < arrayList.Count; i++)
			{
				group = (CodeGroup)arrayList[i];
				IUnionSemanticCodeGroup unionSemanticCodeGroup = group as IUnionSemanticCodeGroup;
				if (unionSemanticCodeGroup == null)
				{
					return false;
				}
				if (!PolicyManager.TestPolicyStatement(group.PolicyStatement))
				{
					return false;
				}
				IMembershipCondition membershipCondition = group.MembershipCondition;
				if (membershipCondition != null && !(membershipCondition is IConstantMembershipCondition))
				{
					return false;
				}
				IList children = group.Children;
				if (children != null && children.Count > 0)
				{
					foreach (object value in children)
					{
						arrayList.Add(value);
					}
				}
			}
			return true;
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x00066B01 File Offset: 0x00064D01
		private static bool TestPolicyStatement(PolicyStatement policy)
		{
			return policy == null || (policy.Attributes & PolicyStatementAttribute.Exclusive) == PolicyStatementAttribute.Nothing;
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x00066B14 File Offset: 0x00064D14
		private static QuickCacheEntryType GenerateQuickCache(PolicyLevel level)
		{
			if (PolicyManager.FullTrustMap == null)
			{
				PolicyManager.FullTrustMap = new QuickCacheEntryType[]
				{
					QuickCacheEntryType.FullTrustZoneMyComputer,
					QuickCacheEntryType.FullTrustZoneIntranet,
					QuickCacheEntryType.FullTrustZoneTrusted,
					QuickCacheEntryType.FullTrustZoneInternet,
					QuickCacheEntryType.FullTrustZoneUntrusted
				};
			}
			QuickCacheEntryType quickCacheEntryType = (QuickCacheEntryType)0;
			Evidence evidence = new Evidence();
			try
			{
				PermissionSet permissionSet = level.Resolve(evidence).PermissionSet;
				if (permissionSet.IsUnrestricted())
				{
					quickCacheEntryType |= QuickCacheEntryType.FullTrustAll;
				}
			}
			catch (PolicyException)
			{
			}
			foreach (object obj in Enum.GetValues(typeof(SecurityZone)))
			{
				SecurityZone securityZone = (SecurityZone)obj;
				if (securityZone != SecurityZone.NoZone)
				{
					Evidence evidence2 = new Evidence();
					evidence2.AddHostEvidence<Zone>(new Zone(securityZone));
					try
					{
						PermissionSet permissionSet2 = level.Resolve(evidence2).PermissionSet;
						if (permissionSet2.IsUnrestricted())
						{
							quickCacheEntryType |= PolicyManager.FullTrustMap[(int)securityZone];
						}
					}
					catch (PolicyException)
					{
					}
				}
			}
			return quickCacheEntryType;
		}

		// Token: 0x04000A44 RID: 2628
		private object m_policyLevels;

		// Token: 0x04000A45 RID: 2629
		private static volatile QuickCacheEntryType[] FullTrustMap;
	}
}
