using System;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002B9 RID: 697
	[Serializable]
	internal sealed class HostProtectionPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x06002510 RID: 9488 RVA: 0x00086F89 File Offset: 0x00085189
		public HostProtectionPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.Resources = HostProtectionResource.All;
				return;
			}
			if (state == PermissionState.None)
			{
				this.Resources = HostProtectionResource.None;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x00086FBB File Offset: 0x000851BB
		public HostProtectionPermission(HostProtectionResource resources)
		{
			this.Resources = resources;
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x00086FCA File Offset: 0x000851CA
		public bool IsUnrestricted()
		{
			return this.Resources == HostProtectionResource.All;
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06002514 RID: 9492 RVA: 0x0008700D File Offset: 0x0008520D
		// (set) Token: 0x06002513 RID: 9491 RVA: 0x00086FD9 File Offset: 0x000851D9
		public HostProtectionResource Resources
		{
			get
			{
				return this.m_resources;
			}
			set
			{
				if (value < HostProtectionResource.None || value > HostProtectionResource.All)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
					{
						(int)value
					}));
				}
				this.m_resources = value;
			}
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x00087018 File Offset: 0x00085218
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_resources == HostProtectionResource.None;
			}
			if (base.GetType() != target.GetType())
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			return (this.m_resources & ((HostProtectionPermission)target).m_resources) == this.m_resources;
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x00087084 File Offset: 0x00085284
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (base.GetType() != target.GetType())
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			HostProtectionResource resources = this.m_resources | ((HostProtectionPermission)target).m_resources;
			return new HostProtectionPermission(resources);
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x000870EC File Offset: 0x000852EC
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (base.GetType() != target.GetType())
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			HostProtectionResource hostProtectionResource = this.m_resources & ((HostProtectionPermission)target).m_resources;
			if (hostProtectionResource == HostProtectionResource.None)
			{
				return null;
			}
			return new HostProtectionPermission(hostProtectionResource);
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x00087153 File Offset: 0x00085353
		public override IPermission Copy()
		{
			return new HostProtectionPermission(this.m_resources);
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x00087160 File Offset: 0x00085360
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, base.GetType().FullName);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				securityElement.AddAttribute("Resources", XMLUtil.BitFieldEnumToString(typeof(HostProtectionResource), this.Resources));
			}
			return securityElement;
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x000871C0 File Offset: 0x000853C0
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.Resources = HostProtectionResource.All;
				return;
			}
			string text = esd.Attribute("Resources");
			if (text == null)
			{
				this.Resources = HostProtectionResource.None;
				return;
			}
			this.Resources = (HostProtectionResource)Enum.Parse(typeof(HostProtectionResource), text);
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x0008721A File Offset: 0x0008541A
		int IBuiltInPermission.GetTokenIndex()
		{
			return HostProtectionPermission.GetTokenIndex();
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x00087221 File Offset: 0x00085421
		internal static int GetTokenIndex()
		{
			return 9;
		}

		// Token: 0x04000DE3 RID: 3555
		internal static volatile HostProtectionResource protectedResources;

		// Token: 0x04000DE4 RID: 3556
		private HostProtectionResource m_resources;
	}
}
