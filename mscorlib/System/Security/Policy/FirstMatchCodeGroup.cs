using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x02000327 RID: 807
	[ComVisible(true)]
	[Obsolete("This type is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
	[Serializable]
	public sealed class FirstMatchCodeGroup : CodeGroup
	{
		// Token: 0x06002946 RID: 10566 RVA: 0x00098556 File Offset: 0x00096756
		internal FirstMatchCodeGroup()
		{
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x0009855E File Offset: 0x0009675E
		public FirstMatchCodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy) : base(membershipCondition, policy)
		{
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x00098568 File Offset: 0x00096768
		[SecuritySafeCritical]
		public override PolicyStatement Resolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			object obj = null;
			if (!PolicyManager.CheckMembershipCondition(base.MembershipCondition, evidence, out obj))
			{
				return null;
			}
			PolicyStatement policyStatement = null;
			foreach (object obj2 in base.Children)
			{
				policyStatement = PolicyManager.ResolveCodeGroup(obj2 as CodeGroup, evidence);
				if (policyStatement != null)
				{
					break;
				}
			}
			IDelayEvaluatedEvidence delayEvaluatedEvidence = obj as IDelayEvaluatedEvidence;
			bool flag = delayEvaluatedEvidence != null && !delayEvaluatedEvidence.IsVerified;
			PolicyStatement policyStatement2 = base.PolicyStatement;
			if (policyStatement2 == null)
			{
				if (flag)
				{
					policyStatement = policyStatement.Copy();
					policyStatement.AddDependentEvidence(delayEvaluatedEvidence);
				}
				return policyStatement;
			}
			if (policyStatement != null)
			{
				PolicyStatement policyStatement3 = policyStatement2.Copy();
				if (flag)
				{
					policyStatement3.AddDependentEvidence(delayEvaluatedEvidence);
				}
				policyStatement3.InplaceUnion(policyStatement);
				return policyStatement3;
			}
			if (flag)
			{
				policyStatement2.AddDependentEvidence(delayEvaluatedEvidence);
			}
			return policyStatement2;
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x00098630 File Offset: 0x00096830
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
						break;
					}
				}
				return codeGroup;
			}
			return null;
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x000986A4 File Offset: 0x000968A4
		public override CodeGroup Copy()
		{
			FirstMatchCodeGroup firstMatchCodeGroup = new FirstMatchCodeGroup();
			firstMatchCodeGroup.MembershipCondition = base.MembershipCondition;
			firstMatchCodeGroup.PolicyStatement = base.PolicyStatement;
			firstMatchCodeGroup.Name = base.Name;
			firstMatchCodeGroup.Description = base.Description;
			foreach (object obj in base.Children)
			{
				firstMatchCodeGroup.AddChild((CodeGroup)obj);
			}
			return firstMatchCodeGroup;
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x0600294B RID: 10571 RVA: 0x0009870F File Offset: 0x0009690F
		public override string MergeLogic
		{
			get
			{
				return Environment.GetResourceString("MergeLogic_FirstMatch");
			}
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x0009871B File Offset: 0x0009691B
		internal override string GetTypeName()
		{
			return "System.Security.Policy.FirstMatchCodeGroup";
		}
	}
}
