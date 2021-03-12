using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x02000326 RID: 806
	[ComVisible(true)]
	[Serializable]
	public sealed class FileCodeGroup : CodeGroup, IUnionSemanticCodeGroup
	{
		// Token: 0x06002936 RID: 10550 RVA: 0x00098219 File Offset: 0x00096419
		internal FileCodeGroup()
		{
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x00098221 File Offset: 0x00096421
		public FileCodeGroup(IMembershipCondition membershipCondition, FileIOPermissionAccess access) : base(membershipCondition, null)
		{
			this.m_access = access;
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x00098234 File Offset: 0x00096434
		[SecuritySafeCritical]
		public override PolicyStatement Resolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			object obj = null;
			if (PolicyManager.CheckMembershipCondition(base.MembershipCondition, evidence, out obj))
			{
				PolicyStatement policyStatement = this.CalculateAssemblyPolicy(evidence);
				IDelayEvaluatedEvidence delayEvaluatedEvidence = obj as IDelayEvaluatedEvidence;
				bool flag = delayEvaluatedEvidence != null && !delayEvaluatedEvidence.IsVerified;
				if (flag)
				{
					policyStatement.AddDependentEvidence(delayEvaluatedEvidence);
				}
				bool flag2 = false;
				IEnumerator enumerator = base.Children.GetEnumerator();
				while (enumerator.MoveNext() && !flag2)
				{
					PolicyStatement policyStatement2 = PolicyManager.ResolveCodeGroup(enumerator.Current as CodeGroup, evidence);
					if (policyStatement2 != null)
					{
						policyStatement.InplaceUnion(policyStatement2);
						if ((policyStatement2.Attributes & PolicyStatementAttribute.Exclusive) == PolicyStatementAttribute.Exclusive)
						{
							flag2 = true;
						}
					}
				}
				return policyStatement;
			}
			return null;
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x000982DB File Offset: 0x000964DB
		PolicyStatement IUnionSemanticCodeGroup.InternalResolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (base.MembershipCondition.Check(evidence))
			{
				return this.CalculateAssemblyPolicy(evidence);
			}
			return null;
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x00098304 File Offset: 0x00096504
		public override CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (base.MembershipCondition.Check(evidence))
			{
				CodeGroup codeGroup = this.Copy();
				codeGroup.Children = new ArrayList();
				foreach (object obj in base.Children)
				{
					CodeGroup codeGroup2 = ((CodeGroup)obj).ResolveMatchingCodeGroups(evidence);
					if (codeGroup2 != null)
					{
						codeGroup.AddChild(codeGroup2);
					}
				}
				return codeGroup;
			}
			return null;
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x00098374 File Offset: 0x00096574
		internal PolicyStatement CalculatePolicy(Url url)
		{
			URLString urlstring = url.GetURLString();
			if (string.Compare(urlstring.Scheme, "file", StringComparison.OrdinalIgnoreCase) != 0)
			{
				return null;
			}
			string directoryName = urlstring.GetDirectoryName();
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.SetPermission(new FileIOPermission(this.m_access, Path.GetFullPath(directoryName)));
			return new PolicyStatement(permissionSet, PolicyStatementAttribute.Nothing);
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000983CC File Offset: 0x000965CC
		private PolicyStatement CalculateAssemblyPolicy(Evidence evidence)
		{
			PolicyStatement policyStatement = null;
			Url hostEvidence = evidence.GetHostEvidence<Url>();
			if (hostEvidence != null)
			{
				policyStatement = this.CalculatePolicy(hostEvidence);
			}
			if (policyStatement == null)
			{
				policyStatement = new PolicyStatement(new PermissionSet(false), PolicyStatementAttribute.Nothing);
			}
			return policyStatement;
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x00098400 File Offset: 0x00096600
		public override CodeGroup Copy()
		{
			FileCodeGroup fileCodeGroup = new FileCodeGroup(base.MembershipCondition, this.m_access);
			fileCodeGroup.Name = base.Name;
			fileCodeGroup.Description = base.Description;
			foreach (object obj in base.Children)
			{
				fileCodeGroup.AddChild((CodeGroup)obj);
			}
			return fileCodeGroup;
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600293E RID: 10558 RVA: 0x0009845F File Offset: 0x0009665F
		public override string MergeLogic
		{
			get
			{
				return Environment.GetResourceString("MergeLogic_Union");
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600293F RID: 10559 RVA: 0x0009846B File Offset: 0x0009666B
		public override string PermissionSetName
		{
			get
			{
				return Environment.GetResourceString("FileCodeGroup_PermissionSet", new object[]
				{
					XMLUtil.BitFieldEnumToString(typeof(FileIOPermissionAccess), this.m_access)
				});
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06002940 RID: 10560 RVA: 0x0009849A File Offset: 0x0009669A
		public override string AttributeString
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x0009849D File Offset: 0x0009669D
		protected override void CreateXml(SecurityElement element, PolicyLevel level)
		{
			element.AddAttribute("Access", XMLUtil.BitFieldEnumToString(typeof(FileIOPermissionAccess), this.m_access));
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x000984C4 File Offset: 0x000966C4
		protected override void ParseXml(SecurityElement e, PolicyLevel level)
		{
			string text = e.Attribute("Access");
			if (text != null)
			{
				this.m_access = (FileIOPermissionAccess)Enum.Parse(typeof(FileIOPermissionAccess), text);
				return;
			}
			this.m_access = FileIOPermissionAccess.NoAccess;
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x00098504 File Offset: 0x00096704
		public override bool Equals(object o)
		{
			FileCodeGroup fileCodeGroup = o as FileCodeGroup;
			return fileCodeGroup != null && base.Equals(fileCodeGroup) && this.m_access == fileCodeGroup.m_access;
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x00098535 File Offset: 0x00096735
		public override int GetHashCode()
		{
			return base.GetHashCode() + this.m_access.GetHashCode();
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x0009854F File Offset: 0x0009674F
		internal override string GetTypeName()
		{
			return "System.Security.Policy.FileCodeGroup";
		}

		// Token: 0x04001087 RID: 4231
		private FileIOPermissionAccess m_access;
	}
}
