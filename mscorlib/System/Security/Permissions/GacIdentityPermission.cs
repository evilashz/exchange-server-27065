using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002E5 RID: 741
	[ComVisible(true)]
	[Serializable]
	public sealed class GacIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x0600268B RID: 9867 RVA: 0x0008BBBF File Offset: 0x00089DBF
		public GacIdentityPermission(PermissionState state)
		{
			if (state != PermissionState.Unrestricted && state != PermissionState.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
			}
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x0008BBDE File Offset: 0x00089DDE
		public GacIdentityPermission()
		{
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x0008BBE6 File Offset: 0x00089DE6
		public override IPermission Copy()
		{
			return new GacIdentityPermission();
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x0008BBED File Offset: 0x00089DED
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return false;
			}
			if (!(target is GacIdentityPermission))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			return true;
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x0008BC21 File Offset: 0x00089E21
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (!(target is GacIdentityPermission))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			return this.Copy();
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x0008BC5A File Offset: 0x00089E5A
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (!(target is GacIdentityPermission))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			return this.Copy();
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x0008BC98 File Offset: 0x00089E98
		public override SecurityElement ToXml()
		{
			return CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.GacIdentityPermission");
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x0008BCB2 File Offset: 0x00089EB2
		public override void FromXml(SecurityElement securityElement)
		{
			CodeAccessPermission.ValidateElement(securityElement, this);
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x0008BCBB File Offset: 0x00089EBB
		int IBuiltInPermission.GetTokenIndex()
		{
			return GacIdentityPermission.GetTokenIndex();
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x0008BCC2 File Offset: 0x00089EC2
		internal static int GetTokenIndex()
		{
			return 15;
		}
	}
}
