using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002DA RID: 730
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x06002614 RID: 9748 RVA: 0x00089651 File Offset: 0x00087851
		public SecurityPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.SetUnrestricted(true);
				return;
			}
			if (state == PermissionState.None)
			{
				this.SetUnrestricted(false);
				this.Reset();
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x00089685 File Offset: 0x00087885
		public SecurityPermission(SecurityPermissionFlag flag)
		{
			this.VerifyAccess(flag);
			this.SetUnrestricted(false);
			this.m_flags = flag;
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x000896A2 File Offset: 0x000878A2
		private void SetUnrestricted(bool unrestricted)
		{
			if (unrestricted)
			{
				this.m_flags = SecurityPermissionFlag.AllFlags;
			}
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x000896B2 File Offset: 0x000878B2
		private void Reset()
		{
			this.m_flags = SecurityPermissionFlag.NoFlags;
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06002619 RID: 9753 RVA: 0x000896CB File Offset: 0x000878CB
		// (set) Token: 0x06002618 RID: 9752 RVA: 0x000896BB File Offset: 0x000878BB
		public SecurityPermissionFlag Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				this.VerifyAccess(value);
				this.m_flags = value;
			}
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x000896D4 File Offset: 0x000878D4
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_flags == SecurityPermissionFlag.NoFlags;
			}
			SecurityPermission securityPermission = target as SecurityPermission;
			if (securityPermission != null)
			{
				return (this.m_flags & ~securityPermission.m_flags) == SecurityPermissionFlag.NoFlags;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
			{
				base.GetType().FullName
			}));
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x00089730 File Offset: 0x00087930
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (!base.VerifyType(target))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			SecurityPermission securityPermission = (SecurityPermission)target;
			if (securityPermission.IsUnrestricted() || this.IsUnrestricted())
			{
				return new SecurityPermission(PermissionState.Unrestricted);
			}
			SecurityPermissionFlag flag = this.m_flags | securityPermission.m_flags;
			return new SecurityPermission(flag);
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x000897A8 File Offset: 0x000879A8
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (!base.VerifyType(target))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			SecurityPermission securityPermission = (SecurityPermission)target;
			SecurityPermissionFlag securityPermissionFlag;
			if (securityPermission.IsUnrestricted())
			{
				if (this.IsUnrestricted())
				{
					return new SecurityPermission(PermissionState.Unrestricted);
				}
				securityPermissionFlag = this.m_flags;
			}
			else if (this.IsUnrestricted())
			{
				securityPermissionFlag = securityPermission.m_flags;
			}
			else
			{
				securityPermissionFlag = (this.m_flags & securityPermission.m_flags);
			}
			if (securityPermissionFlag == SecurityPermissionFlag.NoFlags)
			{
				return null;
			}
			return new SecurityPermission(securityPermissionFlag);
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x0008983A File Offset: 0x00087A3A
		public override IPermission Copy()
		{
			if (this.IsUnrestricted())
			{
				return new SecurityPermission(PermissionState.Unrestricted);
			}
			return new SecurityPermission(this.m_flags);
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x00089856 File Offset: 0x00087A56
		public bool IsUnrestricted()
		{
			return this.m_flags == SecurityPermissionFlag.AllFlags;
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x00089865 File Offset: 0x00087A65
		private void VerifyAccess(SecurityPermissionFlag type)
		{
			if ((type & ~(SecurityPermissionFlag.Assertion | SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.SkipVerification | SecurityPermissionFlag.Execution | SecurityPermissionFlag.ControlThread | SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy | SecurityPermissionFlag.SerializationFormatter | SecurityPermissionFlag.ControlDomainPolicy | SecurityPermissionFlag.ControlPrincipal | SecurityPermissionFlag.ControlAppDomain | SecurityPermissionFlag.RemotingConfiguration | SecurityPermissionFlag.Infrastructure | SecurityPermissionFlag.BindingRedirects)) != SecurityPermissionFlag.NoFlags)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
				{
					(int)type
				}));
			}
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x00089890 File Offset: 0x00087A90
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.SecurityPermission");
			if (!this.IsUnrestricted())
			{
				securityElement.AddAttribute("Flags", XMLUtil.BitFieldEnumToString(typeof(SecurityPermissionFlag), this.m_flags));
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x000898EC File Offset: 0x00087AEC
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.m_flags = SecurityPermissionFlag.AllFlags;
				return;
			}
			this.Reset();
			this.SetUnrestricted(false);
			string text = esd.Attribute("Flags");
			if (text != null)
			{
				this.m_flags = (SecurityPermissionFlag)Enum.Parse(typeof(SecurityPermissionFlag), text);
			}
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x0008994B File Offset: 0x00087B4B
		int IBuiltInPermission.GetTokenIndex()
		{
			return SecurityPermission.GetTokenIndex();
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x00089952 File Offset: 0x00087B52
		internal static int GetTokenIndex()
		{
			return 6;
		}

		// Token: 0x04000E79 RID: 3705
		private SecurityPermissionFlag m_flags;

		// Token: 0x04000E7A RID: 3706
		private const string _strHeaderAssertion = "Assertion";

		// Token: 0x04000E7B RID: 3707
		private const string _strHeaderUnmanagedCode = "UnmanagedCode";

		// Token: 0x04000E7C RID: 3708
		private const string _strHeaderExecution = "Execution";

		// Token: 0x04000E7D RID: 3709
		private const string _strHeaderSkipVerification = "SkipVerification";

		// Token: 0x04000E7E RID: 3710
		private const string _strHeaderControlThread = "ControlThread";

		// Token: 0x04000E7F RID: 3711
		private const string _strHeaderControlEvidence = "ControlEvidence";

		// Token: 0x04000E80 RID: 3712
		private const string _strHeaderControlPolicy = "ControlPolicy";

		// Token: 0x04000E81 RID: 3713
		private const string _strHeaderSerializationFormatter = "SerializationFormatter";

		// Token: 0x04000E82 RID: 3714
		private const string _strHeaderControlDomainPolicy = "ControlDomainPolicy";

		// Token: 0x04000E83 RID: 3715
		private const string _strHeaderControlPrincipal = "ControlPrincipal";

		// Token: 0x04000E84 RID: 3716
		private const string _strHeaderControlAppDomain = "ControlAppDomain";
	}
}
