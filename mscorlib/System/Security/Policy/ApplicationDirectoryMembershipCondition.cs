using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x02000315 RID: 789
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationDirectoryMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
	{
		// Token: 0x06002854 RID: 10324 RVA: 0x00094644 File Offset: 0x00092844
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x0009465C File Offset: 0x0009285C
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			if (evidence == null)
			{
				return false;
			}
			ApplicationDirectory hostEvidence = evidence.GetHostEvidence<ApplicationDirectory>();
			Url hostEvidence2 = evidence.GetHostEvidence<Url>();
			if (hostEvidence != null && hostEvidence2 != null)
			{
				string text = hostEvidence.Directory;
				if (text != null && text.Length > 1)
				{
					if (text[text.Length - 1] == '/')
					{
						text += "*";
					}
					else
					{
						text += "/*";
					}
					URLString operand = new URLString(text);
					if (hostEvidence2.GetURLString().IsSubsetOf(operand))
					{
						usedEvidence = hostEvidence;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x000946DF File Offset: 0x000928DF
		public IMembershipCondition Copy()
		{
			return new ApplicationDirectoryMembershipCondition();
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x000946E6 File Offset: 0x000928E6
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x000946EF File Offset: 0x000928EF
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x000946FC File Offset: 0x000928FC
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), "System.Security.Policy.ApplicationDirectoryMembershipCondition");
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x00094736 File Offset: 0x00092936
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

		// Token: 0x0600285B RID: 10331 RVA: 0x00094768 File Offset: 0x00092968
		public override bool Equals(object o)
		{
			return o is ApplicationDirectoryMembershipCondition;
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x00094773 File Offset: 0x00092973
		public override int GetHashCode()
		{
			return typeof(ApplicationDirectoryMembershipCondition).GetHashCode();
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x00094784 File Offset: 0x00092984
		public override string ToString()
		{
			return Environment.GetResourceString("ApplicationDirectory_ToString");
		}
	}
}
