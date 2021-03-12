using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x02000347 RID: 839
	[ComVisible(true)]
	[Serializable]
	public sealed class GacMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
	{
		// Token: 0x06002AA4 RID: 10916 RVA: 0x0009E4D4 File Offset: 0x0009C6D4
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x0009E4EC File Offset: 0x0009C6EC
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			return evidence != null && evidence.GetHostEvidence<GacInstalled>() != null;
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x0009E4FF File Offset: 0x0009C6FF
		public IMembershipCondition Copy()
		{
			return new GacMembershipCondition();
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x0009E506 File Offset: 0x0009C706
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x0009E50F File Offset: 0x0009C70F
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x0009E51C File Offset: 0x0009C71C
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), base.GetType().FullName);
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x0009E55C File Offset: 0x0009C75C
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

		// Token: 0x06002AAB RID: 10923 RVA: 0x0009E590 File Offset: 0x0009C790
		public override bool Equals(object o)
		{
			return o is GacMembershipCondition;
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x0009E5AA File Offset: 0x0009C7AA
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x0009E5AD File Offset: 0x0009C7AD
		public override string ToString()
		{
			return Environment.GetResourceString("GAC_ToString");
		}
	}
}
