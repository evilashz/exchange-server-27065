using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002D6 RID: 726
	[ComVisible(true)]
	[Serializable]
	public sealed class ReflectionPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x060025EB RID: 9707 RVA: 0x00088754 File Offset: 0x00086954
		public ReflectionPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.SetUnrestricted(true);
				return;
			}
			if (state == PermissionState.None)
			{
				this.SetUnrestricted(false);
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x00088782 File Offset: 0x00086982
		public ReflectionPermission(ReflectionPermissionFlag flag)
		{
			this.VerifyAccess(flag);
			this.SetUnrestricted(false);
			this.m_flags = flag;
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x0008879F File Offset: 0x0008699F
		private void SetUnrestricted(bool unrestricted)
		{
			if (unrestricted)
			{
				this.m_flags = (ReflectionPermissionFlag.TypeInformation | ReflectionPermissionFlag.MemberAccess | ReflectionPermissionFlag.ReflectionEmit | ReflectionPermissionFlag.RestrictedMemberAccess);
				return;
			}
			this.Reset();
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x000887B3 File Offset: 0x000869B3
		private void Reset()
		{
			this.m_flags = ReflectionPermissionFlag.NoFlags;
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060025F0 RID: 9712 RVA: 0x000887CC File Offset: 0x000869CC
		// (set) Token: 0x060025EF RID: 9711 RVA: 0x000887BC File Offset: 0x000869BC
		public ReflectionPermissionFlag Flags
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

		// Token: 0x060025F1 RID: 9713 RVA: 0x000887D4 File Offset: 0x000869D4
		public bool IsUnrestricted()
		{
			return this.m_flags == (ReflectionPermissionFlag.TypeInformation | ReflectionPermissionFlag.MemberAccess | ReflectionPermissionFlag.ReflectionEmit | ReflectionPermissionFlag.RestrictedMemberAccess);
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x000887E0 File Offset: 0x000869E0
		public override IPermission Union(IPermission other)
		{
			if (other == null)
			{
				return this.Copy();
			}
			if (!base.VerifyType(other))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			ReflectionPermission reflectionPermission = (ReflectionPermission)other;
			if (this.IsUnrestricted() || reflectionPermission.IsUnrestricted())
			{
				return new ReflectionPermission(PermissionState.Unrestricted);
			}
			ReflectionPermissionFlag flag = this.m_flags | reflectionPermission.m_flags;
			return new ReflectionPermission(flag);
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x00088858 File Offset: 0x00086A58
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_flags == ReflectionPermissionFlag.NoFlags;
			}
			bool result;
			try
			{
				ReflectionPermission reflectionPermission = (ReflectionPermission)target;
				if (reflectionPermission.IsUnrestricted())
				{
					result = true;
				}
				else if (this.IsUnrestricted())
				{
					result = false;
				}
				else
				{
					result = ((this.m_flags & ~reflectionPermission.m_flags) == ReflectionPermissionFlag.NoFlags);
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			return result;
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x000888DC File Offset: 0x00086ADC
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
			ReflectionPermission reflectionPermission = (ReflectionPermission)target;
			ReflectionPermissionFlag reflectionPermissionFlag = reflectionPermission.m_flags & this.m_flags;
			if (reflectionPermissionFlag == ReflectionPermissionFlag.NoFlags)
			{
				return null;
			}
			return new ReflectionPermission(reflectionPermissionFlag);
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x0008893B File Offset: 0x00086B3B
		public override IPermission Copy()
		{
			if (this.IsUnrestricted())
			{
				return new ReflectionPermission(PermissionState.Unrestricted);
			}
			return new ReflectionPermission(this.m_flags);
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x00088957 File Offset: 0x00086B57
		private void VerifyAccess(ReflectionPermissionFlag type)
		{
			if ((type & ~(ReflectionPermissionFlag.TypeInformation | ReflectionPermissionFlag.MemberAccess | ReflectionPermissionFlag.ReflectionEmit | ReflectionPermissionFlag.RestrictedMemberAccess)) != ReflectionPermissionFlag.NoFlags)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
				{
					(int)type
				}));
			}
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x00088980 File Offset: 0x00086B80
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.ReflectionPermission");
			if (!this.IsUnrestricted())
			{
				securityElement.AddAttribute("Flags", XMLUtil.BitFieldEnumToString(typeof(ReflectionPermissionFlag), this.m_flags));
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x000889DC File Offset: 0x00086BDC
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.m_flags = (ReflectionPermissionFlag.TypeInformation | ReflectionPermissionFlag.MemberAccess | ReflectionPermissionFlag.ReflectionEmit | ReflectionPermissionFlag.RestrictedMemberAccess);
				return;
			}
			this.Reset();
			this.SetUnrestricted(false);
			string text = esd.Attribute("Flags");
			if (text != null)
			{
				this.m_flags = (ReflectionPermissionFlag)Enum.Parse(typeof(ReflectionPermissionFlag), text);
			}
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x00088A38 File Offset: 0x00086C38
		int IBuiltInPermission.GetTokenIndex()
		{
			return ReflectionPermission.GetTokenIndex();
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x00088A3F File Offset: 0x00086C3F
		internal static int GetTokenIndex()
		{
			return 4;
		}

		// Token: 0x04000E61 RID: 3681
		internal const ReflectionPermissionFlag AllFlagsAndMore = ReflectionPermissionFlag.TypeInformation | ReflectionPermissionFlag.MemberAccess | ReflectionPermissionFlag.ReflectionEmit | ReflectionPermissionFlag.RestrictedMemberAccess;

		// Token: 0x04000E62 RID: 3682
		private ReflectionPermissionFlag m_flags;
	}
}
