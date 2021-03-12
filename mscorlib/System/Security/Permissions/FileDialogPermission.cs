using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002B3 RID: 691
	[ComVisible(true)]
	[Serializable]
	public sealed class FileDialogPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x060024A7 RID: 9383 RVA: 0x000851A5 File Offset: 0x000833A5
		public FileDialogPermission(PermissionState state)
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

		// Token: 0x060024A8 RID: 9384 RVA: 0x000851D9 File Offset: 0x000833D9
		public FileDialogPermission(FileDialogPermissionAccess access)
		{
			FileDialogPermission.VerifyAccess(access);
			this.access = access;
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060024A9 RID: 9385 RVA: 0x000851EE File Offset: 0x000833EE
		// (set) Token: 0x060024AA RID: 9386 RVA: 0x000851F6 File Offset: 0x000833F6
		public FileDialogPermissionAccess Access
		{
			get
			{
				return this.access;
			}
			set
			{
				FileDialogPermission.VerifyAccess(value);
				this.access = value;
			}
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x00085205 File Offset: 0x00083405
		public override IPermission Copy()
		{
			return new FileDialogPermission(this.access);
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x00085214 File Offset: 0x00083414
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.SetUnrestricted(true);
				return;
			}
			this.access = FileDialogPermissionAccess.None;
			string text = esd.Attribute("Access");
			if (text != null)
			{
				this.access = (FileDialogPermissionAccess)Enum.Parse(typeof(FileDialogPermissionAccess), text);
			}
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x00085269 File Offset: 0x00083469
		int IBuiltInPermission.GetTokenIndex()
		{
			return FileDialogPermission.GetTokenIndex();
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x00085270 File Offset: 0x00083470
		internal static int GetTokenIndex()
		{
			return 1;
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x00085274 File Offset: 0x00083474
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
			FileDialogPermission fileDialogPermission = (FileDialogPermission)target;
			FileDialogPermissionAccess fileDialogPermissionAccess = this.access & fileDialogPermission.Access;
			if (fileDialogPermissionAccess == FileDialogPermissionAccess.None)
			{
				return null;
			}
			return new FileDialogPermission(fileDialogPermissionAccess);
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000852D4 File Offset: 0x000834D4
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.access == FileDialogPermissionAccess.None;
			}
			bool result;
			try
			{
				FileDialogPermission fileDialogPermission = (FileDialogPermission)target;
				if (fileDialogPermission.IsUnrestricted())
				{
					result = true;
				}
				else if (this.IsUnrestricted())
				{
					result = false;
				}
				else
				{
					int num = (int)(this.access & FileDialogPermissionAccess.Open);
					int num2 = (int)(this.access & FileDialogPermissionAccess.Save);
					int num3 = (int)(fileDialogPermission.Access & FileDialogPermissionAccess.Open);
					int num4 = (int)(fileDialogPermission.Access & FileDialogPermissionAccess.Save);
					result = (num <= num3 && num2 <= num4);
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

		// Token: 0x060024B1 RID: 9393 RVA: 0x00085380 File Offset: 0x00083580
		public bool IsUnrestricted()
		{
			return this.access == FileDialogPermissionAccess.OpenSave;
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x0008538B File Offset: 0x0008358B
		private void Reset()
		{
			this.access = FileDialogPermissionAccess.None;
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x00085394 File Offset: 0x00083594
		private void SetUnrestricted(bool unrestricted)
		{
			if (unrestricted)
			{
				this.access = FileDialogPermissionAccess.OpenSave;
			}
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000853A0 File Offset: 0x000835A0
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.FileDialogPermission");
			if (!this.IsUnrestricted())
			{
				if (this.access != FileDialogPermissionAccess.None)
				{
					securityElement.AddAttribute("Access", Enum.GetName(typeof(FileDialogPermissionAccess), this.access));
				}
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x00085404 File Offset: 0x00083604
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
			FileDialogPermission fileDialogPermission = (FileDialogPermission)target;
			return new FileDialogPermission(this.access | fileDialogPermission.Access);
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x00085461 File Offset: 0x00083661
		private static void VerifyAccess(FileDialogPermissionAccess access)
		{
			if ((access & ~(FileDialogPermissionAccess.Open | FileDialogPermissionAccess.Save)) != FileDialogPermissionAccess.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
				{
					(int)access
				}));
			}
		}

		// Token: 0x04000DC0 RID: 3520
		private FileDialogPermissionAccess access;
	}
}
