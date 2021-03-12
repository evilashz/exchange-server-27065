using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002DD RID: 733
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNameIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x06002638 RID: 9784 RVA: 0x0008A123 File Offset: 0x00088323
		public StrongNameIdentityPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.m_unrestricted = true;
				return;
			}
			if (state == PermissionState.None)
			{
				this.m_unrestricted = false;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x0008A154 File Offset: 0x00088354
		public StrongNameIdentityPermission(StrongNamePublicKeyBlob blob, string name, Version version)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (name != null && name.Equals(""))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyStrongName"));
			}
			this.m_unrestricted = false;
			this.m_strongNames = new StrongName2[1];
			this.m_strongNames[0] = new StrongName2(blob, name, version);
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x0008A220 File Offset: 0x00088420
		// (set) Token: 0x0600263A RID: 9786 RVA: 0x0008A1B8 File Offset: 0x000883B8
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				if (this.m_strongNames == null || this.m_strongNames.Length == 0)
				{
					return null;
				}
				if (this.m_strongNames.Length > 1)
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
				}
				return this.m_strongNames[0].m_publicKeyBlob;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PublicKey");
				}
				this.m_unrestricted = false;
				if (this.m_strongNames != null && this.m_strongNames.Length == 1)
				{
					this.m_strongNames[0].m_publicKeyBlob = value;
					return;
				}
				this.m_strongNames = new StrongName2[1];
				this.m_strongNames[0] = new StrongName2(value, "", new Version());
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600263D RID: 9789 RVA: 0x0008A2D4 File Offset: 0x000884D4
		// (set) Token: 0x0600263C RID: 9788 RVA: 0x0008A260 File Offset: 0x00088460
		public string Name
		{
			get
			{
				if (this.m_strongNames == null || this.m_strongNames.Length == 0)
				{
					return "";
				}
				if (this.m_strongNames.Length > 1)
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
				}
				return this.m_strongNames[0].m_name;
			}
			set
			{
				if (value != null && value.Length == 0)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"));
				}
				this.m_unrestricted = false;
				if (this.m_strongNames != null && this.m_strongNames.Length == 1)
				{
					this.m_strongNames[0].m_name = value;
					return;
				}
				this.m_strongNames = new StrongName2[1];
				this.m_strongNames[0] = new StrongName2(null, value, new Version());
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600263F RID: 9791 RVA: 0x0008A378 File Offset: 0x00088578
		// (set) Token: 0x0600263E RID: 9790 RVA: 0x0008A320 File Offset: 0x00088520
		public Version Version
		{
			get
			{
				if (this.m_strongNames == null || this.m_strongNames.Length == 0)
				{
					return new Version();
				}
				if (this.m_strongNames.Length > 1)
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
				}
				return this.m_strongNames[0].m_version;
			}
			set
			{
				this.m_unrestricted = false;
				if (this.m_strongNames != null && this.m_strongNames.Length == 1)
				{
					this.m_strongNames[0].m_version = value;
					return;
				}
				this.m_strongNames = new StrongName2[1];
				this.m_strongNames[0] = new StrongName2(null, "", value);
			}
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x0008A3C4 File Offset: 0x000885C4
		public override IPermission Copy()
		{
			StrongNameIdentityPermission strongNameIdentityPermission = new StrongNameIdentityPermission(PermissionState.None);
			strongNameIdentityPermission.m_unrestricted = this.m_unrestricted;
			if (this.m_strongNames != null)
			{
				strongNameIdentityPermission.m_strongNames = new StrongName2[this.m_strongNames.Length];
				for (int i = 0; i < this.m_strongNames.Length; i++)
				{
					strongNameIdentityPermission.m_strongNames[i] = this.m_strongNames[i].Copy();
				}
			}
			return strongNameIdentityPermission;
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x0008A428 File Offset: 0x00088628
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !this.m_unrestricted && (this.m_strongNames == null || this.m_strongNames.Length == 0);
			}
			StrongNameIdentityPermission strongNameIdentityPermission = target as StrongNameIdentityPermission;
			if (strongNameIdentityPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			if (strongNameIdentityPermission.m_unrestricted)
			{
				return true;
			}
			if (this.m_unrestricted)
			{
				return false;
			}
			if (this.m_strongNames != null)
			{
				foreach (StrongName2 strongName in this.m_strongNames)
				{
					bool flag = false;
					if (strongNameIdentityPermission.m_strongNames != null)
					{
						foreach (StrongName2 target2 in strongNameIdentityPermission.m_strongNames)
						{
							if (strongName.IsSubsetOf(target2))
							{
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x0008A500 File Offset: 0x00088700
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			StrongNameIdentityPermission strongNameIdentityPermission = target as StrongNameIdentityPermission;
			if (strongNameIdentityPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			if (this.m_unrestricted && strongNameIdentityPermission.m_unrestricted)
			{
				return new StrongNameIdentityPermission(PermissionState.None)
				{
					m_unrestricted = true
				};
			}
			if (this.m_unrestricted)
			{
				return strongNameIdentityPermission.Copy();
			}
			if (strongNameIdentityPermission.m_unrestricted)
			{
				return this.Copy();
			}
			if (this.m_strongNames == null || strongNameIdentityPermission.m_strongNames == null || this.m_strongNames.Length == 0 || strongNameIdentityPermission.m_strongNames.Length == 0)
			{
				return null;
			}
			List<StrongName2> list = new List<StrongName2>();
			foreach (StrongName2 strongName in this.m_strongNames)
			{
				foreach (StrongName2 target2 in strongNameIdentityPermission.m_strongNames)
				{
					StrongName2 strongName2 = strongName.Intersect(target2);
					if (strongName2 != null)
					{
						list.Add(strongName2);
					}
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return new StrongNameIdentityPermission(PermissionState.None)
			{
				m_strongNames = list.ToArray()
			};
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x0008A624 File Offset: 0x00088824
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				if ((this.m_strongNames == null || this.m_strongNames.Length == 0) && !this.m_unrestricted)
				{
					return null;
				}
				return this.Copy();
			}
			else
			{
				StrongNameIdentityPermission strongNameIdentityPermission = target as StrongNameIdentityPermission;
				if (strongNameIdentityPermission == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
					{
						base.GetType().FullName
					}));
				}
				if (this.m_unrestricted || strongNameIdentityPermission.m_unrestricted)
				{
					return new StrongNameIdentityPermission(PermissionState.None)
					{
						m_unrestricted = true
					};
				}
				if (this.m_strongNames == null || this.m_strongNames.Length == 0)
				{
					if (strongNameIdentityPermission.m_strongNames == null || strongNameIdentityPermission.m_strongNames.Length == 0)
					{
						return null;
					}
					return strongNameIdentityPermission.Copy();
				}
				else
				{
					if (strongNameIdentityPermission.m_strongNames == null || strongNameIdentityPermission.m_strongNames.Length == 0)
					{
						return this.Copy();
					}
					List<StrongName2> list = new List<StrongName2>();
					foreach (StrongName2 item in this.m_strongNames)
					{
						list.Add(item);
					}
					foreach (StrongName2 strongName in strongNameIdentityPermission.m_strongNames)
					{
						bool flag = false;
						foreach (StrongName2 target2 in list)
						{
							if (strongName.Equals(target2))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							list.Add(strongName);
						}
					}
					return new StrongNameIdentityPermission(PermissionState.None)
					{
						m_strongNames = list.ToArray()
					};
				}
			}
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x0008A7A8 File Offset: 0x000889A8
		public override void FromXml(SecurityElement e)
		{
			this.m_unrestricted = false;
			this.m_strongNames = null;
			CodeAccessPermission.ValidateElement(e, this);
			string text = e.Attribute("Unrestricted");
			if (text != null && string.Compare(text, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_unrestricted = true;
				return;
			}
			string text2 = e.Attribute("PublicKeyBlob");
			string text3 = e.Attribute("Name");
			string text4 = e.Attribute("AssemblyVersion");
			List<StrongName2> list = new List<StrongName2>();
			if (text2 != null || text3 != null || text4 != null)
			{
				StrongName2 item = new StrongName2((text2 == null) ? null : new StrongNamePublicKeyBlob(text2), text3, (text4 == null) ? null : new Version(text4));
				list.Add(item);
			}
			ArrayList children = e.Children;
			if (children != null)
			{
				foreach (object obj in children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					text2 = securityElement.Attribute("PublicKeyBlob");
					text3 = securityElement.Attribute("Name");
					text4 = securityElement.Attribute("AssemblyVersion");
					if (text2 != null || text3 != null || text4 != null)
					{
						StrongName2 item = new StrongName2((text2 == null) ? null : new StrongNamePublicKeyBlob(text2), text3, (text4 == null) ? null : new Version(text4));
						list.Add(item);
					}
				}
			}
			if (list.Count != 0)
			{
				this.m_strongNames = list.ToArray();
			}
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x0008A914 File Offset: 0x00088B14
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.StrongNameIdentityPermission");
			if (this.m_unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else if (this.m_strongNames != null)
			{
				if (this.m_strongNames.Length == 1)
				{
					if (this.m_strongNames[0].m_publicKeyBlob != null)
					{
						securityElement.AddAttribute("PublicKeyBlob", Hex.EncodeHexString(this.m_strongNames[0].m_publicKeyBlob.PublicKey));
					}
					if (this.m_strongNames[0].m_name != null)
					{
						securityElement.AddAttribute("Name", this.m_strongNames[0].m_name);
					}
					if (this.m_strongNames[0].m_version != null)
					{
						securityElement.AddAttribute("AssemblyVersion", this.m_strongNames[0].m_version.ToString());
					}
				}
				else
				{
					for (int i = 0; i < this.m_strongNames.Length; i++)
					{
						SecurityElement securityElement2 = new SecurityElement("StrongName");
						if (this.m_strongNames[i].m_publicKeyBlob != null)
						{
							securityElement2.AddAttribute("PublicKeyBlob", Hex.EncodeHexString(this.m_strongNames[i].m_publicKeyBlob.PublicKey));
						}
						if (this.m_strongNames[i].m_name != null)
						{
							securityElement2.AddAttribute("Name", this.m_strongNames[i].m_name);
						}
						if (this.m_strongNames[i].m_version != null)
						{
							securityElement2.AddAttribute("AssemblyVersion", this.m_strongNames[i].m_version.ToString());
						}
						securityElement.AddChild(securityElement2);
					}
				}
			}
			return securityElement;
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x0008AA9F File Offset: 0x00088C9F
		int IBuiltInPermission.GetTokenIndex()
		{
			return StrongNameIdentityPermission.GetTokenIndex();
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x0008AAA6 File Offset: 0x00088CA6
		internal static int GetTokenIndex()
		{
			return 12;
		}

		// Token: 0x04000E8C RID: 3724
		private bool m_unrestricted;

		// Token: 0x04000E8D RID: 3725
		private StrongName2[] m_strongNames;
	}
}
