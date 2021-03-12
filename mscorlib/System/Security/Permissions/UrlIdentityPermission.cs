using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002E2 RID: 738
	[ComVisible(true)]
	[Serializable]
	public sealed class UrlIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x06002666 RID: 9830 RVA: 0x0008AFE0 File Offset: 0x000891E0
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_serializedPermission != null)
			{
				this.FromXml(SecurityElement.FromString(this.m_serializedPermission));
				this.m_serializedPermission = null;
				return;
			}
			if (this.m_url != null)
			{
				this.m_unrestricted = false;
				this.m_urls = new URLString[1];
				this.m_urls[0] = this.m_url;
				this.m_url = null;
			}
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x0008B040 File Offset: 0x00089240
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_serializedPermission = this.ToXml().ToString();
				if (this.m_urls != null && this.m_urls.Length == 1)
				{
					this.m_url = this.m_urls[0];
				}
			}
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x0008B08E File Offset: 0x0008928E
		[OnSerialized]
		private void OnSerialized(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_serializedPermission = null;
				this.m_url = null;
			}
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x0008B0AD File Offset: 0x000892AD
		public UrlIdentityPermission(PermissionState state)
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

		// Token: 0x0600266A RID: 9834 RVA: 0x0008B0DB File Offset: 0x000892DB
		public UrlIdentityPermission(string site)
		{
			if (site == null)
			{
				throw new ArgumentNullException("site");
			}
			this.Url = site;
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x0008B0F8 File Offset: 0x000892F8
		internal UrlIdentityPermission(URLString site)
		{
			this.m_unrestricted = false;
			this.m_urls = new URLString[1];
			this.m_urls[0] = site;
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x0008B11C File Offset: 0x0008931C
		internal void AppendOrigin(ArrayList originList)
		{
			if (this.m_urls == null)
			{
				originList.Add("");
				return;
			}
			for (int i = 0; i < this.m_urls.Length; i++)
			{
				originList.Add(this.m_urls[i].ToString());
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x0600266E RID: 9838 RVA: 0x0008B19B File Offset: 0x0008939B
		// (set) Token: 0x0600266D RID: 9837 RVA: 0x0008B165 File Offset: 0x00089365
		public string Url
		{
			get
			{
				if (this.m_urls == null)
				{
					return "";
				}
				if (this.m_urls.Length == 1)
				{
					return this.m_urls[0].ToString();
				}
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
			}
			set
			{
				this.m_unrestricted = false;
				if (value == null || value.Length == 0)
				{
					this.m_urls = null;
					return;
				}
				this.m_urls = new URLString[1];
				this.m_urls[0] = new URLString(value);
			}
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x0008B1D4 File Offset: 0x000893D4
		public override IPermission Copy()
		{
			UrlIdentityPermission urlIdentityPermission = new UrlIdentityPermission(PermissionState.None);
			urlIdentityPermission.m_unrestricted = this.m_unrestricted;
			if (this.m_urls != null)
			{
				urlIdentityPermission.m_urls = new URLString[this.m_urls.Length];
				for (int i = 0; i < this.m_urls.Length; i++)
				{
					urlIdentityPermission.m_urls[i] = (URLString)this.m_urls[i].Copy();
				}
			}
			return urlIdentityPermission;
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x0008B240 File Offset: 0x00089440
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !this.m_unrestricted && (this.m_urls == null || this.m_urls.Length == 0);
			}
			UrlIdentityPermission urlIdentityPermission = target as UrlIdentityPermission;
			if (urlIdentityPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			if (urlIdentityPermission.m_unrestricted)
			{
				return true;
			}
			if (this.m_unrestricted)
			{
				return false;
			}
			if (this.m_urls != null)
			{
				foreach (URLString urlstring in this.m_urls)
				{
					bool flag = false;
					if (urlIdentityPermission.m_urls != null)
					{
						foreach (URLString operand in urlIdentityPermission.m_urls)
						{
							if (urlstring.IsSubsetOf(operand))
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

		// Token: 0x06002671 RID: 9841 RVA: 0x0008B318 File Offset: 0x00089518
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			UrlIdentityPermission urlIdentityPermission = target as UrlIdentityPermission;
			if (urlIdentityPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			if (this.m_unrestricted && urlIdentityPermission.m_unrestricted)
			{
				return new UrlIdentityPermission(PermissionState.None)
				{
					m_unrestricted = true
				};
			}
			if (this.m_unrestricted)
			{
				return urlIdentityPermission.Copy();
			}
			if (urlIdentityPermission.m_unrestricted)
			{
				return this.Copy();
			}
			if (this.m_urls == null || urlIdentityPermission.m_urls == null || this.m_urls.Length == 0 || urlIdentityPermission.m_urls.Length == 0)
			{
				return null;
			}
			List<URLString> list = new List<URLString>();
			foreach (URLString urlstring in this.m_urls)
			{
				foreach (URLString operand in urlIdentityPermission.m_urls)
				{
					URLString urlstring2 = (URLString)urlstring.Intersect(operand);
					if (urlstring2 != null)
					{
						list.Add(urlstring2);
					}
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return new UrlIdentityPermission(PermissionState.None)
			{
				m_urls = list.ToArray()
			};
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x0008B440 File Offset: 0x00089640
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				if ((this.m_urls == null || this.m_urls.Length == 0) && !this.m_unrestricted)
				{
					return null;
				}
				return this.Copy();
			}
			else
			{
				UrlIdentityPermission urlIdentityPermission = target as UrlIdentityPermission;
				if (urlIdentityPermission == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
					{
						base.GetType().FullName
					}));
				}
				if (this.m_unrestricted || urlIdentityPermission.m_unrestricted)
				{
					return new UrlIdentityPermission(PermissionState.None)
					{
						m_unrestricted = true
					};
				}
				if (this.m_urls == null || this.m_urls.Length == 0)
				{
					if (urlIdentityPermission.m_urls == null || urlIdentityPermission.m_urls.Length == 0)
					{
						return null;
					}
					return urlIdentityPermission.Copy();
				}
				else
				{
					if (urlIdentityPermission.m_urls == null || urlIdentityPermission.m_urls.Length == 0)
					{
						return this.Copy();
					}
					List<URLString> list = new List<URLString>();
					foreach (URLString item in this.m_urls)
					{
						list.Add(item);
					}
					foreach (URLString urlstring in urlIdentityPermission.m_urls)
					{
						bool flag = false;
						foreach (URLString url in list)
						{
							if (urlstring.Equals(url))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							list.Add(urlstring);
						}
					}
					return new UrlIdentityPermission(PermissionState.None)
					{
						m_urls = list.ToArray()
					};
				}
			}
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x0008B5C4 File Offset: 0x000897C4
		public override void FromXml(SecurityElement esd)
		{
			this.m_unrestricted = false;
			this.m_urls = null;
			CodeAccessPermission.ValidateElement(esd, this);
			string text = esd.Attribute("Unrestricted");
			if (text != null && string.Compare(text, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_unrestricted = true;
				return;
			}
			string text2 = esd.Attribute("Url");
			List<URLString> list = new List<URLString>();
			if (text2 != null)
			{
				list.Add(new URLString(text2, true));
			}
			ArrayList children = esd.Children;
			if (children != null)
			{
				foreach (object obj in children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					text2 = securityElement.Attribute("Url");
					if (text2 != null)
					{
						list.Add(new URLString(text2, true));
					}
				}
			}
			if (list.Count != 0)
			{
				this.m_urls = list.ToArray();
			}
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x0008B6B0 File Offset: 0x000898B0
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.UrlIdentityPermission");
			if (this.m_unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else if (this.m_urls != null)
			{
				if (this.m_urls.Length == 1)
				{
					securityElement.AddAttribute("Url", this.m_urls[0].ToString());
				}
				else
				{
					for (int i = 0; i < this.m_urls.Length; i++)
					{
						SecurityElement securityElement2 = new SecurityElement("Url");
						securityElement2.AddAttribute("Url", this.m_urls[i].ToString());
						securityElement.AddChild(securityElement2);
					}
				}
			}
			return securityElement;
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x0008B74E File Offset: 0x0008994E
		int IBuiltInPermission.GetTokenIndex()
		{
			return UrlIdentityPermission.GetTokenIndex();
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x0008B755 File Offset: 0x00089955
		internal static int GetTokenIndex()
		{
			return 13;
		}

		// Token: 0x04000E9A RID: 3738
		[OptionalField(VersionAdded = 2)]
		private bool m_unrestricted;

		// Token: 0x04000E9B RID: 3739
		[OptionalField(VersionAdded = 2)]
		private URLString[] m_urls;

		// Token: 0x04000E9C RID: 3740
		[OptionalField(VersionAdded = 2)]
		private string m_serializedPermission;

		// Token: 0x04000E9D RID: 3741
		private URLString m_url;
	}
}
