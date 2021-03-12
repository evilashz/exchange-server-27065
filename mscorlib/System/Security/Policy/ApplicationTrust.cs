using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x02000319 RID: 793
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationTrust : EvidenceBase, ISecurityEncodable
	{
		// Token: 0x06002871 RID: 10353 RVA: 0x00094EA3 File Offset: 0x000930A3
		public ApplicationTrust(ApplicationIdentity applicationIdentity) : this()
		{
			this.ApplicationIdentity = applicationIdentity;
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x00094EB2 File Offset: 0x000930B2
		public ApplicationTrust() : this(new PermissionSet(PermissionState.None))
		{
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x00094EC0 File Offset: 0x000930C0
		internal ApplicationTrust(PermissionSet defaultGrantSet)
		{
			this.InitDefaultGrantSet(defaultGrantSet);
			this.m_fullTrustAssemblies = new List<StrongName>().AsReadOnly();
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x00094EE0 File Offset: 0x000930E0
		public ApplicationTrust(PermissionSet defaultGrantSet, IEnumerable<StrongName> fullTrustAssemblies)
		{
			if (fullTrustAssemblies == null)
			{
				throw new ArgumentNullException("fullTrustAssemblies");
			}
			this.InitDefaultGrantSet(defaultGrantSet);
			List<StrongName> list = new List<StrongName>();
			foreach (StrongName strongName in fullTrustAssemblies)
			{
				if (strongName == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_NullFullTrustAssembly"));
				}
				list.Add(new StrongName(strongName.PublicKey, strongName.Name, strongName.Version));
			}
			this.m_fullTrustAssemblies = list.AsReadOnly();
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x00094F80 File Offset: 0x00093180
		private void InitDefaultGrantSet(PermissionSet defaultGrantSet)
		{
			if (defaultGrantSet == null)
			{
				throw new ArgumentNullException("defaultGrantSet");
			}
			this.DefaultGrantSet = new PolicyStatement(defaultGrantSet);
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06002876 RID: 10358 RVA: 0x00094F9C File Offset: 0x0009319C
		// (set) Token: 0x06002877 RID: 10359 RVA: 0x00094FA4 File Offset: 0x000931A4
		public ApplicationIdentity ApplicationIdentity
		{
			get
			{
				return this.m_appId;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(Environment.GetResourceString("Argument_InvalidAppId"));
				}
				this.m_appId = value;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06002878 RID: 10360 RVA: 0x00094FC0 File Offset: 0x000931C0
		// (set) Token: 0x06002879 RID: 10361 RVA: 0x00094FDC File Offset: 0x000931DC
		public PolicyStatement DefaultGrantSet
		{
			get
			{
				if (this.m_psDefaultGrant == null)
				{
					return new PolicyStatement(new PermissionSet(PermissionState.None));
				}
				return this.m_psDefaultGrant;
			}
			set
			{
				if (value == null)
				{
					this.m_psDefaultGrant = null;
					this.m_grantSetSpecialFlags = 0;
					return;
				}
				this.m_psDefaultGrant = value;
				this.m_grantSetSpecialFlags = SecurityManager.GetSpecialFlags(this.m_psDefaultGrant.PermissionSet, null);
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600287A RID: 10362 RVA: 0x0009500E File Offset: 0x0009320E
		public IList<StrongName> FullTrustAssemblies
		{
			get
			{
				return this.m_fullTrustAssemblies;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600287B RID: 10363 RVA: 0x00095016 File Offset: 0x00093216
		// (set) Token: 0x0600287C RID: 10364 RVA: 0x0009501E File Offset: 0x0009321E
		public bool IsApplicationTrustedToRun
		{
			get
			{
				return this.m_appTrustedToRun;
			}
			set
			{
				this.m_appTrustedToRun = value;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600287D RID: 10365 RVA: 0x00095027 File Offset: 0x00093227
		// (set) Token: 0x0600287E RID: 10366 RVA: 0x0009502F File Offset: 0x0009322F
		public bool Persist
		{
			get
			{
				return this.m_persist;
			}
			set
			{
				this.m_persist = value;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600287F RID: 10367 RVA: 0x00095038 File Offset: 0x00093238
		// (set) Token: 0x06002880 RID: 10368 RVA: 0x00095060 File Offset: 0x00093260
		public object ExtraInfo
		{
			get
			{
				if (this.m_elExtraInfo != null)
				{
					this.m_extraInfo = ApplicationTrust.ObjectFromXml(this.m_elExtraInfo);
					this.m_elExtraInfo = null;
				}
				return this.m_extraInfo;
			}
			set
			{
				this.m_elExtraInfo = null;
				this.m_extraInfo = value;
			}
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x00095070 File Offset: 0x00093270
		public SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("ApplicationTrust");
			securityElement.AddAttribute("version", "1");
			if (this.m_appId != null)
			{
				securityElement.AddAttribute("FullName", SecurityElement.Escape(this.m_appId.FullName));
			}
			if (this.m_appTrustedToRun)
			{
				securityElement.AddAttribute("TrustedToRun", "true");
			}
			if (this.m_persist)
			{
				securityElement.AddAttribute("Persist", "true");
			}
			if (this.m_psDefaultGrant != null)
			{
				SecurityElement securityElement2 = new SecurityElement("DefaultGrant");
				securityElement2.AddChild(this.m_psDefaultGrant.ToXml());
				securityElement.AddChild(securityElement2);
			}
			if (this.m_fullTrustAssemblies.Count > 0)
			{
				SecurityElement securityElement3 = new SecurityElement("FullTrustAssemblies");
				foreach (StrongName strongName in this.m_fullTrustAssemblies)
				{
					securityElement3.AddChild(strongName.ToXml());
				}
				securityElement.AddChild(securityElement3);
			}
			if (this.ExtraInfo != null)
			{
				securityElement.AddChild(ApplicationTrust.ObjectToXml("ExtraInfo", this.ExtraInfo));
			}
			return securityElement;
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x0009519C File Offset: 0x0009339C
		public void FromXml(SecurityElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (string.Compare(element.Tag, "ApplicationTrust", StringComparison.Ordinal) != 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
			}
			this.m_appTrustedToRun = false;
			string text = element.Attribute("TrustedToRun");
			if (text != null && string.Compare(text, "true", StringComparison.Ordinal) == 0)
			{
				this.m_appTrustedToRun = true;
			}
			this.m_persist = false;
			string text2 = element.Attribute("Persist");
			if (text2 != null && string.Compare(text2, "true", StringComparison.Ordinal) == 0)
			{
				this.m_persist = true;
			}
			this.m_appId = null;
			string text3 = element.Attribute("FullName");
			if (text3 != null && text3.Length > 0)
			{
				this.m_appId = new ApplicationIdentity(text3);
			}
			this.m_psDefaultGrant = null;
			this.m_grantSetSpecialFlags = 0;
			SecurityElement securityElement = element.SearchForChildByTag("DefaultGrant");
			if (securityElement != null)
			{
				SecurityElement securityElement2 = securityElement.SearchForChildByTag("PolicyStatement");
				if (securityElement2 != null)
				{
					PolicyStatement policyStatement = new PolicyStatement(null);
					policyStatement.FromXml(securityElement2);
					this.m_psDefaultGrant = policyStatement;
					this.m_grantSetSpecialFlags = SecurityManager.GetSpecialFlags(policyStatement.PermissionSet, null);
				}
			}
			List<StrongName> list = new List<StrongName>();
			SecurityElement securityElement3 = element.SearchForChildByTag("FullTrustAssemblies");
			if (securityElement3 != null && securityElement3.InternalChildren != null)
			{
				IEnumerator enumerator = securityElement3.Children.GetEnumerator();
				while (enumerator.MoveNext())
				{
					StrongName strongName = new StrongName();
					strongName.FromXml(enumerator.Current as SecurityElement);
					list.Add(strongName);
				}
			}
			this.m_fullTrustAssemblies = list.AsReadOnly();
			this.m_elExtraInfo = element.SearchForChildByTag("ExtraInfo");
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x00095330 File Offset: 0x00093530
		private static SecurityElement ObjectToXml(string tag, object obj)
		{
			ISecurityEncodable securityEncodable = obj as ISecurityEncodable;
			SecurityElement securityElement;
			if (securityEncodable != null)
			{
				securityElement = securityEncodable.ToXml();
				if (!securityElement.Tag.Equals(tag))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
				}
			}
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, obj);
			byte[] sArray = memoryStream.ToArray();
			securityElement = new SecurityElement(tag);
			securityElement.AddAttribute("Data", Hex.EncodeHexString(sArray));
			return securityElement;
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000953A4 File Offset: 0x000935A4
		private static object ObjectFromXml(SecurityElement elObject)
		{
			if (elObject.Attribute("class") != null)
			{
				ISecurityEncodable securityEncodable = XMLUtil.CreateCodeGroup(elObject) as ISecurityEncodable;
				if (securityEncodable != null)
				{
					securityEncodable.FromXml(elObject);
					return securityEncodable;
				}
			}
			string hexString = elObject.Attribute("Data");
			MemoryStream serializationStream = new MemoryStream(Hex.DecodeHexString(hexString));
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			return binaryFormatter.Deserialize(serializationStream);
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x000953FB File Offset: 0x000935FB
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override EvidenceBase Clone()
		{
			return base.Clone();
		}

		// Token: 0x04001059 RID: 4185
		private ApplicationIdentity m_appId;

		// Token: 0x0400105A RID: 4186
		private bool m_appTrustedToRun;

		// Token: 0x0400105B RID: 4187
		private bool m_persist;

		// Token: 0x0400105C RID: 4188
		private object m_extraInfo;

		// Token: 0x0400105D RID: 4189
		private SecurityElement m_elExtraInfo;

		// Token: 0x0400105E RID: 4190
		private PolicyStatement m_psDefaultGrant;

		// Token: 0x0400105F RID: 4191
		private IList<StrongName> m_fullTrustAssemblies;

		// Token: 0x04001060 RID: 4192
		[NonSerialized]
		private int m_grantSetSpecialFlags;
	}
}
