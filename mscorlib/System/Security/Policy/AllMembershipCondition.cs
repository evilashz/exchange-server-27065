using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x02000312 RID: 786
	[ComVisible(true)]
	[Serializable]
	public sealed class AllMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
	{
		// Token: 0x0600283C RID: 10300 RVA: 0x000943E4 File Offset: 0x000925E4
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x000943FC File Offset: 0x000925FC
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			return true;
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x00094402 File Offset: 0x00092602
		public IMembershipCondition Copy()
		{
			return new AllMembershipCondition();
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x00094409 File Offset: 0x00092609
		public override string ToString()
		{
			return Environment.GetResourceString("All_ToString");
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x00094415 File Offset: 0x00092615
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x0009441E File Offset: 0x0009261E
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x00094428 File Offset: 0x00092628
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), "System.Security.Policy.AllMembershipCondition");
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x00094462 File Offset: 0x00092662
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (!e.Tag.Equals("IMembershipCondition"))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"));
			}
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x00094494 File Offset: 0x00092694
		public override bool Equals(object o)
		{
			return o is AllMembershipCondition;
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x0009449F File Offset: 0x0009269F
		public override int GetHashCode()
		{
			return typeof(AllMembershipCondition).GetHashCode();
		}
	}
}
