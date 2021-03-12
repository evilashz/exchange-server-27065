using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x0200033F RID: 831
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongName : EvidenceBase, IIdentityPermissionFactory, IDelayEvaluatedEvidence
	{
		// Token: 0x06002A2E RID: 10798 RVA: 0x0009CCF8 File Offset: 0x0009AEF8
		internal StrongName()
		{
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x0009CD00 File Offset: 0x0009AF00
		public StrongName(StrongNamePublicKeyBlob blob, string name, Version version) : this(blob, name, version, null)
		{
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x0009CD0C File Offset: 0x0009AF0C
		internal StrongName(StrongNamePublicKeyBlob blob, string name, Version version, Assembly assembly)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyStrongName"));
			}
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			RuntimeAssembly runtimeAssembly = assembly as RuntimeAssembly;
			if (assembly != null && runtimeAssembly == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), "assembly");
			}
			this.m_publicKeyBlob = blob;
			this.m_name = name;
			this.m_version = version;
			this.m_assembly = runtimeAssembly;
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06002A31 RID: 10801 RVA: 0x0009CDB3 File Offset: 0x0009AFB3
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				return this.m_publicKeyBlob;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06002A32 RID: 10802 RVA: 0x0009CDBB File Offset: 0x0009AFBB
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06002A33 RID: 10803 RVA: 0x0009CDC3 File Offset: 0x0009AFC3
		public Version Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06002A34 RID: 10804 RVA: 0x0009CDCB File Offset: 0x0009AFCB
		bool IDelayEvaluatedEvidence.IsVerified
		{
			[SecurityCritical]
			get
			{
				return !(this.m_assembly != null) || this.m_assembly.IsStrongNameVerified;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06002A35 RID: 10805 RVA: 0x0009CDE8 File Offset: 0x0009AFE8
		bool IDelayEvaluatedEvidence.WasUsed
		{
			get
			{
				return this.m_wasUsed;
			}
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x0009CDF0 File Offset: 0x0009AFF0
		void IDelayEvaluatedEvidence.MarkUsed()
		{
			this.m_wasUsed = true;
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x0009CDFC File Offset: 0x0009AFFC
		internal static bool CompareNames(string asmName, string mcName)
		{
			if (mcName.Length > 0 && mcName[mcName.Length - 1] == '*' && mcName.Length - 1 <= asmName.Length)
			{
				return string.Compare(mcName, 0, asmName, 0, mcName.Length - 1, StringComparison.OrdinalIgnoreCase) == 0;
			}
			return string.Compare(mcName, asmName, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x0009CE55 File Offset: 0x0009B055
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new StrongNameIdentityPermission(this.m_publicKeyBlob, this.m_name, this.m_version);
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x0009CE6E File Offset: 0x0009B06E
		public override EvidenceBase Clone()
		{
			return new StrongName(this.m_publicKeyBlob, this.m_name, this.m_version);
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x0009CE87 File Offset: 0x0009B087
		public object Copy()
		{
			return this.Clone();
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x0009CE90 File Offset: 0x0009B090
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("StrongName");
			securityElement.AddAttribute("version", "1");
			if (this.m_publicKeyBlob != null)
			{
				securityElement.AddAttribute("Key", Hex.EncodeHexString(this.m_publicKeyBlob.PublicKey));
			}
			if (this.m_name != null)
			{
				securityElement.AddAttribute("Name", this.m_name);
			}
			if (this.m_version != null)
			{
				securityElement.AddAttribute("Version", this.m_version.ToString());
			}
			return securityElement;
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x0009CF1C File Offset: 0x0009B11C
		internal void FromXml(SecurityElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (string.Compare(element.Tag, "StrongName", StringComparison.Ordinal) != 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
			}
			this.m_publicKeyBlob = null;
			this.m_version = null;
			string text = element.Attribute("Key");
			if (text != null)
			{
				this.m_publicKeyBlob = new StrongNamePublicKeyBlob(Hex.DecodeHexString(text));
			}
			this.m_name = element.Attribute("Name");
			string text2 = element.Attribute("Version");
			if (text2 != null)
			{
				this.m_version = new Version(text2);
			}
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x0009CFB4 File Offset: 0x0009B1B4
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x0009CFC4 File Offset: 0x0009B1C4
		public override bool Equals(object o)
		{
			StrongName strongName = o as StrongName;
			return strongName != null && object.Equals(this.m_publicKeyBlob, strongName.m_publicKeyBlob) && object.Equals(this.m_name, strongName.m_name) && object.Equals(this.m_version, strongName.m_version);
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x0009D014 File Offset: 0x0009B214
		public override int GetHashCode()
		{
			if (this.m_publicKeyBlob != null)
			{
				return this.m_publicKeyBlob.GetHashCode();
			}
			if (this.m_name != null || this.m_version != null)
			{
				return ((this.m_name == null) ? 0 : this.m_name.GetHashCode()) + ((this.m_version == null) ? 0 : this.m_version.GetHashCode());
			}
			return typeof(StrongName).GetHashCode();
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x0009D090 File Offset: 0x0009B290
		internal object Normalize()
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(this.m_publicKeyBlob.PublicKey);
			binaryWriter.Write(this.m_version.Major);
			binaryWriter.Write(this.m_name);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x040010E0 RID: 4320
		private StrongNamePublicKeyBlob m_publicKeyBlob;

		// Token: 0x040010E1 RID: 4321
		private string m_name;

		// Token: 0x040010E2 RID: 4322
		private Version m_version;

		// Token: 0x040010E3 RID: 4323
		[NonSerialized]
		private RuntimeAssembly m_assembly;

		// Token: 0x040010E4 RID: 4324
		[NonSerialized]
		private bool m_wasUsed;
	}
}
