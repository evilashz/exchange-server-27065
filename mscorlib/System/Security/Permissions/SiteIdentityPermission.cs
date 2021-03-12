using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002DB RID: 731
	[ComVisible(true)]
	[Serializable]
	public sealed class SiteIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x06002624 RID: 9764 RVA: 0x00089958 File Offset: 0x00087B58
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_serializedPermission != null)
			{
				this.FromXml(SecurityElement.FromString(this.m_serializedPermission));
				this.m_serializedPermission = null;
				return;
			}
			if (this.m_site != null)
			{
				this.m_unrestricted = false;
				this.m_sites = new SiteString[1];
				this.m_sites[0] = this.m_site;
				this.m_site = null;
			}
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x000899B8 File Offset: 0x00087BB8
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_serializedPermission = this.ToXml().ToString();
				if (this.m_sites != null && this.m_sites.Length == 1)
				{
					this.m_site = this.m_sites[0];
				}
			}
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x00089A06 File Offset: 0x00087C06
		[OnSerialized]
		private void OnSerialized(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_serializedPermission = null;
				this.m_site = null;
			}
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x00089A25 File Offset: 0x00087C25
		public SiteIdentityPermission(PermissionState state)
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

		// Token: 0x06002628 RID: 9768 RVA: 0x00089A53 File Offset: 0x00087C53
		public SiteIdentityPermission(string site)
		{
			this.Site = site;
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600262A RID: 9770 RVA: 0x00089A85 File Offset: 0x00087C85
		// (set) Token: 0x06002629 RID: 9769 RVA: 0x00089A62 File Offset: 0x00087C62
		public string Site
		{
			get
			{
				if (this.m_sites == null)
				{
					return "";
				}
				if (this.m_sites.Length == 1)
				{
					return this.m_sites[0].ToString();
				}
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
			}
			set
			{
				this.m_unrestricted = false;
				this.m_sites = new SiteString[1];
				this.m_sites[0] = new SiteString(value);
			}
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x00089AC0 File Offset: 0x00087CC0
		public override IPermission Copy()
		{
			SiteIdentityPermission siteIdentityPermission = new SiteIdentityPermission(PermissionState.None);
			siteIdentityPermission.m_unrestricted = this.m_unrestricted;
			if (this.m_sites != null)
			{
				siteIdentityPermission.m_sites = new SiteString[this.m_sites.Length];
				for (int i = 0; i < this.m_sites.Length; i++)
				{
					siteIdentityPermission.m_sites[i] = this.m_sites[i].Copy();
				}
			}
			return siteIdentityPermission;
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x00089B24 File Offset: 0x00087D24
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !this.m_unrestricted && (this.m_sites == null || this.m_sites.Length == 0);
			}
			SiteIdentityPermission siteIdentityPermission = target as SiteIdentityPermission;
			if (siteIdentityPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			if (siteIdentityPermission.m_unrestricted)
			{
				return true;
			}
			if (this.m_unrestricted)
			{
				return false;
			}
			if (this.m_sites != null)
			{
				foreach (SiteString siteString in this.m_sites)
				{
					bool flag = false;
					if (siteIdentityPermission.m_sites != null)
					{
						foreach (SiteString operand in siteIdentityPermission.m_sites)
						{
							if (siteString.IsSubsetOf(operand))
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

		// Token: 0x0600262D RID: 9773 RVA: 0x00089BFC File Offset: 0x00087DFC
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SiteIdentityPermission siteIdentityPermission = target as SiteIdentityPermission;
			if (siteIdentityPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			if (this.m_unrestricted && siteIdentityPermission.m_unrestricted)
			{
				return new SiteIdentityPermission(PermissionState.None)
				{
					m_unrestricted = true
				};
			}
			if (this.m_unrestricted)
			{
				return siteIdentityPermission.Copy();
			}
			if (siteIdentityPermission.m_unrestricted)
			{
				return this.Copy();
			}
			if (this.m_sites == null || siteIdentityPermission.m_sites == null || this.m_sites.Length == 0 || siteIdentityPermission.m_sites.Length == 0)
			{
				return null;
			}
			List<SiteString> list = new List<SiteString>();
			foreach (SiteString siteString in this.m_sites)
			{
				foreach (SiteString operand in siteIdentityPermission.m_sites)
				{
					SiteString siteString2 = siteString.Intersect(operand);
					if (siteString2 != null)
					{
						list.Add(siteString2);
					}
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return new SiteIdentityPermission(PermissionState.None)
			{
				m_sites = list.ToArray()
			};
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x00089D20 File Offset: 0x00087F20
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				if ((this.m_sites == null || this.m_sites.Length == 0) && !this.m_unrestricted)
				{
					return null;
				}
				return this.Copy();
			}
			else
			{
				SiteIdentityPermission siteIdentityPermission = target as SiteIdentityPermission;
				if (siteIdentityPermission == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
					{
						base.GetType().FullName
					}));
				}
				if (this.m_unrestricted || siteIdentityPermission.m_unrestricted)
				{
					return new SiteIdentityPermission(PermissionState.None)
					{
						m_unrestricted = true
					};
				}
				if (this.m_sites == null || this.m_sites.Length == 0)
				{
					if (siteIdentityPermission.m_sites == null || siteIdentityPermission.m_sites.Length == 0)
					{
						return null;
					}
					return siteIdentityPermission.Copy();
				}
				else
				{
					if (siteIdentityPermission.m_sites == null || siteIdentityPermission.m_sites.Length == 0)
					{
						return this.Copy();
					}
					List<SiteString> list = new List<SiteString>();
					foreach (SiteString item in this.m_sites)
					{
						list.Add(item);
					}
					foreach (SiteString siteString in siteIdentityPermission.m_sites)
					{
						bool flag = false;
						foreach (SiteString obj in list)
						{
							if (siteString.Equals(obj))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							list.Add(siteString);
						}
					}
					return new SiteIdentityPermission(PermissionState.None)
					{
						m_sites = list.ToArray()
					};
				}
			}
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x00089EA4 File Offset: 0x000880A4
		public override void FromXml(SecurityElement esd)
		{
			this.m_unrestricted = false;
			this.m_sites = null;
			CodeAccessPermission.ValidateElement(esd, this);
			string text = esd.Attribute("Unrestricted");
			if (text != null && string.Compare(text, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_unrestricted = true;
				return;
			}
			string text2 = esd.Attribute("Site");
			List<SiteString> list = new List<SiteString>();
			if (text2 != null)
			{
				list.Add(new SiteString(text2));
			}
			ArrayList children = esd.Children;
			if (children != null)
			{
				foreach (object obj in children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					text2 = securityElement.Attribute("Site");
					if (text2 != null)
					{
						list.Add(new SiteString(text2));
					}
				}
			}
			if (list.Count != 0)
			{
				this.m_sites = list.ToArray();
			}
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x00089F90 File Offset: 0x00088190
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.SiteIdentityPermission");
			if (this.m_unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else if (this.m_sites != null)
			{
				if (this.m_sites.Length == 1)
				{
					securityElement.AddAttribute("Site", this.m_sites[0].ToString());
				}
				else
				{
					for (int i = 0; i < this.m_sites.Length; i++)
					{
						SecurityElement securityElement2 = new SecurityElement("Site");
						securityElement2.AddAttribute("Site", this.m_sites[i].ToString());
						securityElement.AddChild(securityElement2);
					}
				}
			}
			return securityElement;
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x0008A02E File Offset: 0x0008822E
		int IBuiltInPermission.GetTokenIndex()
		{
			return SiteIdentityPermission.GetTokenIndex();
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x0008A035 File Offset: 0x00088235
		internal static int GetTokenIndex()
		{
			return 11;
		}

		// Token: 0x04000E85 RID: 3717
		[OptionalField(VersionAdded = 2)]
		private bool m_unrestricted;

		// Token: 0x04000E86 RID: 3718
		[OptionalField(VersionAdded = 2)]
		private SiteString[] m_sites;

		// Token: 0x04000E87 RID: 3719
		[OptionalField(VersionAdded = 2)]
		private string m_serializedPermission;

		// Token: 0x04000E88 RID: 3720
		private SiteString m_site;
	}
}
