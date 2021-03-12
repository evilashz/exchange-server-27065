using System;
using System.Security.Policy;

namespace System.Security.Permissions
{
	// Token: 0x020002DC RID: 732
	[Serializable]
	internal sealed class StrongName2
	{
		// Token: 0x06002633 RID: 9779 RVA: 0x0008A039 File Offset: 0x00088239
		public StrongName2(StrongNamePublicKeyBlob publicKeyBlob, string name, Version version)
		{
			this.m_publicKeyBlob = publicKeyBlob;
			this.m_name = name;
			this.m_version = version;
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x0008A056 File Offset: 0x00088256
		public StrongName2 Copy()
		{
			return new StrongName2(this.m_publicKeyBlob, this.m_name, this.m_version);
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x0008A070 File Offset: 0x00088270
		public bool IsSubsetOf(StrongName2 target)
		{
			return this.m_publicKeyBlob == null || (this.m_publicKeyBlob.Equals(target.m_publicKeyBlob) && (this.m_name == null || (target.m_name != null && StrongName.CompareNames(target.m_name, this.m_name))) && (this.m_version == null || (target.m_version != null && target.m_version.CompareTo(this.m_version) == 0)));
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x0008A0E7 File Offset: 0x000882E7
		public StrongName2 Intersect(StrongName2 target)
		{
			if (target.IsSubsetOf(this))
			{
				return target.Copy();
			}
			if (this.IsSubsetOf(target))
			{
				return this.Copy();
			}
			return null;
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x0008A10A File Offset: 0x0008830A
		public bool Equals(StrongName2 target)
		{
			return target.IsSubsetOf(this) && this.IsSubsetOf(target);
		}

		// Token: 0x04000E89 RID: 3721
		public StrongNamePublicKeyBlob m_publicKeyBlob;

		// Token: 0x04000E8A RID: 3722
		public string m_name;

		// Token: 0x04000E8B RID: 3723
		public Version m_version;
	}
}
