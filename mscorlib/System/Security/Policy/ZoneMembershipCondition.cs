using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x02000345 RID: 837
	[ComVisible(true)]
	[Serializable]
	public sealed class ZoneMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
	{
		// Token: 0x06002A8A RID: 10890 RVA: 0x0009E0E7 File Offset: 0x0009C2E7
		internal ZoneMembershipCondition()
		{
			this.m_zone = SecurityZone.NoZone;
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x0009E0F6 File Offset: 0x0009C2F6
		public ZoneMembershipCondition(SecurityZone zone)
		{
			ZoneMembershipCondition.VerifyZone(zone);
			this.SecurityZone = zone;
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06002A8D RID: 10893 RVA: 0x0009E11A File Offset: 0x0009C31A
		// (set) Token: 0x06002A8C RID: 10892 RVA: 0x0009E10B File Offset: 0x0009C30B
		public SecurityZone SecurityZone
		{
			get
			{
				if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
				{
					this.ParseZone();
				}
				return this.m_zone;
			}
			set
			{
				ZoneMembershipCondition.VerifyZone(value);
				this.m_zone = value;
			}
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x0009E139 File Offset: 0x0009C339
		private static void VerifyZone(SecurityZone zone)
		{
			if (zone < SecurityZone.MyComputer || zone > SecurityZone.Untrusted)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IllegalZone"));
			}
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x0009E154 File Offset: 0x0009C354
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x0009E16C File Offset: 0x0009C36C
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			if (evidence == null)
			{
				return false;
			}
			Zone hostEvidence = evidence.GetHostEvidence<Zone>();
			if (hostEvidence != null)
			{
				if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
				{
					this.ParseZone();
				}
				if (hostEvidence.SecurityZone == this.m_zone)
				{
					usedEvidence = hostEvidence;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x0009E1B6 File Offset: 0x0009C3B6
		public IMembershipCondition Copy()
		{
			if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
			{
				this.ParseZone();
			}
			return new ZoneMembershipCondition(this.m_zone);
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x0009E1DA File Offset: 0x0009C3DA
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x0009E1E3 File Offset: 0x0009C3E3
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x0009E1F0 File Offset: 0x0009C3F0
		public SecurityElement ToXml(PolicyLevel level)
		{
			if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
			{
				this.ParseZone();
			}
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), "System.Security.Policy.ZoneMembershipCondition");
			securityElement.AddAttribute("version", "1");
			if (this.m_zone != SecurityZone.NoZone)
			{
				securityElement.AddAttribute("Zone", Enum.GetName(typeof(SecurityZone), this.m_zone));
			}
			return securityElement;
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x0009E270 File Offset: 0x0009C470
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
			lock (this)
			{
				this.m_zone = SecurityZone.NoZone;
				this.m_element = e;
			}
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x0009E2E4 File Offset: 0x0009C4E4
		private void ParseZone()
		{
			lock (this)
			{
				if (this.m_element != null)
				{
					string text = this.m_element.Attribute("Zone");
					this.m_zone = SecurityZone.NoZone;
					if (text == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_ZoneCannotBeNull"));
					}
					this.m_zone = (SecurityZone)Enum.Parse(typeof(SecurityZone), text);
					ZoneMembershipCondition.VerifyZone(this.m_zone);
					this.m_element = null;
				}
			}
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x0009E380 File Offset: 0x0009C580
		public override bool Equals(object o)
		{
			ZoneMembershipCondition zoneMembershipCondition = o as ZoneMembershipCondition;
			if (zoneMembershipCondition != null)
			{
				if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
				{
					this.ParseZone();
				}
				if (zoneMembershipCondition.m_zone == SecurityZone.NoZone && zoneMembershipCondition.m_element != null)
				{
					zoneMembershipCondition.ParseZone();
				}
				if (this.m_zone == zoneMembershipCondition.m_zone)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x0009E3D6 File Offset: 0x0009C5D6
		public override int GetHashCode()
		{
			if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
			{
				this.ParseZone();
			}
			return (int)this.m_zone;
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x0009E3F5 File Offset: 0x0009C5F5
		public override string ToString()
		{
			if (this.m_zone == SecurityZone.NoZone && this.m_element != null)
			{
				this.ParseZone();
			}
			return string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Zone_ToString"), ZoneMembershipCondition.s_names[(int)this.m_zone]);
		}

		// Token: 0x040010F2 RID: 4338
		private static readonly string[] s_names = new string[]
		{
			"MyComputer",
			"Intranet",
			"Trusted",
			"Internet",
			"Untrusted"
		};

		// Token: 0x040010F3 RID: 4339
		private SecurityZone m_zone;

		// Token: 0x040010F4 RID: 4340
		private SecurityElement m_element;
	}
}
