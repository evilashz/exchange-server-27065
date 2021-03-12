using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x02000340 RID: 832
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNameMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
	{
		// Token: 0x06002A41 RID: 10817 RVA: 0x0009D0E1 File Offset: 0x0009B2E1
		internal StrongNameMembershipCondition()
		{
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x0009D0EC File Offset: 0x0009B2EC
		public StrongNameMembershipCondition(StrongNamePublicKeyBlob blob, string name, Version version)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (name != null && name.Equals(""))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyStrongName"));
			}
			this.m_publicKeyBlob = blob;
			this.m_name = name;
			this.m_version = version;
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06002A44 RID: 10820 RVA: 0x0009D159 File Offset: 0x0009B359
		// (set) Token: 0x06002A43 RID: 10819 RVA: 0x0009D142 File Offset: 0x0009B342
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				if (this.m_publicKeyBlob == null && this.m_element != null)
				{
					this.ParseKeyBlob();
				}
				return this.m_publicKeyBlob;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PublicKey");
				}
				this.m_publicKeyBlob = value;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06002A46 RID: 10822 RVA: 0x0009D1DC File Offset: 0x0009B3DC
		// (set) Token: 0x06002A45 RID: 10821 RVA: 0x0009D178 File Offset: 0x0009B378
		public string Name
		{
			get
			{
				if (this.m_name == null && this.m_element != null)
				{
					this.ParseName();
				}
				return this.m_name;
			}
			set
			{
				if (value == null)
				{
					if (this.m_publicKeyBlob == null && this.m_element != null)
					{
						this.ParseKeyBlob();
					}
					if (this.m_version == null && this.m_element != null)
					{
						this.ParseVersion();
					}
					this.m_element = null;
				}
				else if (value.Length == 0)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"));
				}
				this.m_name = value;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06002A48 RID: 10824 RVA: 0x0009D24C File Offset: 0x0009B44C
		// (set) Token: 0x06002A47 RID: 10823 RVA: 0x0009D1FC File Offset: 0x0009B3FC
		public Version Version
		{
			get
			{
				if (this.m_version == null && this.m_element != null)
				{
					this.ParseVersion();
				}
				return this.m_version;
			}
			set
			{
				if (value == null)
				{
					if (this.m_name == null && this.m_element != null)
					{
						this.ParseName();
					}
					if (this.m_publicKeyBlob == null && this.m_element != null)
					{
						this.ParseKeyBlob();
					}
					this.m_element = null;
				}
				this.m_version = value;
			}
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x0009D26C File Offset: 0x0009B46C
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x0009D284 File Offset: 0x0009B484
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			if (evidence == null)
			{
				return false;
			}
			StrongName delayEvaluatedHostEvidence = evidence.GetDelayEvaluatedHostEvidence<StrongName>();
			if (delayEvaluatedHostEvidence != null)
			{
				bool flag = this.PublicKey != null && this.PublicKey.Equals(delayEvaluatedHostEvidence.PublicKey);
				bool flag2 = this.Name == null || (delayEvaluatedHostEvidence.Name != null && StrongName.CompareNames(delayEvaluatedHostEvidence.Name, this.Name));
				bool flag3 = this.Version == null || (delayEvaluatedHostEvidence.Version != null && delayEvaluatedHostEvidence.Version.CompareTo(this.Version) == 0);
				if (flag && flag2 && flag3)
				{
					usedEvidence = delayEvaluatedHostEvidence;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x0009D320 File Offset: 0x0009B520
		public IMembershipCondition Copy()
		{
			return new StrongNameMembershipCondition(this.PublicKey, this.Name, this.Version);
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x0009D339 File Offset: 0x0009B539
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x0009D342 File Offset: 0x0009B542
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x0009D34C File Offset: 0x0009B54C
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), "System.Security.Policy.StrongNameMembershipCondition");
			securityElement.AddAttribute("version", "1");
			if (this.PublicKey != null)
			{
				securityElement.AddAttribute("PublicKeyBlob", Hex.EncodeHexString(this.PublicKey.PublicKey));
			}
			if (this.Name != null)
			{
				securityElement.AddAttribute("Name", this.Name);
			}
			if (this.Version != null)
			{
				securityElement.AddAttribute("AssemblyVersion", this.Version.ToString());
			}
			return securityElement;
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x0009D3E0 File Offset: 0x0009B5E0
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
				this.m_name = null;
				this.m_publicKeyBlob = null;
				this.m_version = null;
				this.m_element = e;
			}
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x0009D464 File Offset: 0x0009B664
		private void ParseName()
		{
			lock (this)
			{
				if (this.m_element != null)
				{
					string text = this.m_element.Attribute("Name");
					this.m_name = ((text == null) ? null : text);
					if (this.m_version != null && this.m_name != null && this.m_publicKeyBlob != null)
					{
						this.m_element = null;
					}
				}
			}
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x0009D4E0 File Offset: 0x0009B6E0
		private void ParseKeyBlob()
		{
			lock (this)
			{
				if (this.m_element != null)
				{
					string text = this.m_element.Attribute("PublicKeyBlob");
					StrongNamePublicKeyBlob strongNamePublicKeyBlob = new StrongNamePublicKeyBlob();
					if (text == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_BlobCannotBeNull"));
					}
					strongNamePublicKeyBlob.PublicKey = Hex.DecodeHexString(text);
					this.m_publicKeyBlob = strongNamePublicKeyBlob;
					if (this.m_version != null && this.m_name != null && this.m_publicKeyBlob != null)
					{
						this.m_element = null;
					}
				}
			}
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x0009D580 File Offset: 0x0009B780
		private void ParseVersion()
		{
			lock (this)
			{
				if (this.m_element != null)
				{
					string text = this.m_element.Attribute("AssemblyVersion");
					this.m_version = ((text == null) ? null : new Version(text));
					if (this.m_version != null && this.m_name != null && this.m_publicKeyBlob != null)
					{
						this.m_element = null;
					}
				}
			}
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x0009D604 File Offset: 0x0009B804
		public override string ToString()
		{
			string arg = "";
			string arg2 = "";
			if (this.Name != null)
			{
				arg = " " + string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("StrongName_Name"), this.Name);
			}
			if (this.Version != null)
			{
				arg2 = " " + string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("StrongName_Version"), this.Version);
			}
			return string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("StrongName_ToString"), Hex.EncodeHexString(this.PublicKey.PublicKey), arg, arg2);
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x0009D6A0 File Offset: 0x0009B8A0
		public override bool Equals(object o)
		{
			StrongNameMembershipCondition strongNameMembershipCondition = o as StrongNameMembershipCondition;
			if (strongNameMembershipCondition != null)
			{
				if (this.m_publicKeyBlob == null && this.m_element != null)
				{
					this.ParseKeyBlob();
				}
				if (strongNameMembershipCondition.m_publicKeyBlob == null && strongNameMembershipCondition.m_element != null)
				{
					strongNameMembershipCondition.ParseKeyBlob();
				}
				if (object.Equals(this.m_publicKeyBlob, strongNameMembershipCondition.m_publicKeyBlob))
				{
					if (this.m_name == null && this.m_element != null)
					{
						this.ParseName();
					}
					if (strongNameMembershipCondition.m_name == null && strongNameMembershipCondition.m_element != null)
					{
						strongNameMembershipCondition.ParseName();
					}
					if (object.Equals(this.m_name, strongNameMembershipCondition.m_name))
					{
						if (this.m_version == null && this.m_element != null)
						{
							this.ParseVersion();
						}
						if (strongNameMembershipCondition.m_version == null && strongNameMembershipCondition.m_element != null)
						{
							strongNameMembershipCondition.ParseVersion();
						}
						if (object.Equals(this.m_version, strongNameMembershipCondition.m_version))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x0009D78C File Offset: 0x0009B98C
		public override int GetHashCode()
		{
			if (this.m_publicKeyBlob == null && this.m_element != null)
			{
				this.ParseKeyBlob();
			}
			if (this.m_publicKeyBlob != null)
			{
				return this.m_publicKeyBlob.GetHashCode();
			}
			if (this.m_name == null && this.m_element != null)
			{
				this.ParseName();
			}
			if (this.m_version == null && this.m_element != null)
			{
				this.ParseVersion();
			}
			if (this.m_name != null || this.m_version != null)
			{
				return ((this.m_name == null) ? 0 : this.m_name.GetHashCode()) + ((this.m_version == null) ? 0 : this.m_version.GetHashCode());
			}
			return typeof(StrongNameMembershipCondition).GetHashCode();
		}

		// Token: 0x040010E5 RID: 4325
		private StrongNamePublicKeyBlob m_publicKeyBlob;

		// Token: 0x040010E6 RID: 4326
		private string m_name;

		// Token: 0x040010E7 RID: 4327
		private Version m_version;

		// Token: 0x040010E8 RID: 4328
		private SecurityElement m_element;

		// Token: 0x040010E9 RID: 4329
		private const string s_tagName = "Name";

		// Token: 0x040010EA RID: 4330
		private const string s_tagVersion = "AssemblyVersion";

		// Token: 0x040010EB RID: 4331
		private const string s_tagPublicKeyBlob = "PublicKeyBlob";
	}
}
