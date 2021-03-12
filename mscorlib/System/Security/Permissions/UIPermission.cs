using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002E1 RID: 737
	[ComVisible(true)]
	[Serializable]
	public sealed class UIPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x06002651 RID: 9809 RVA: 0x0008ABAE File Offset: 0x00088DAE
		public UIPermission(PermissionState state)
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

		// Token: 0x06002652 RID: 9810 RVA: 0x0008ABE2 File Offset: 0x00088DE2
		public UIPermission(UIPermissionWindow windowFlag, UIPermissionClipboard clipboardFlag)
		{
			UIPermission.VerifyWindowFlag(windowFlag);
			UIPermission.VerifyClipboardFlag(clipboardFlag);
			this.m_windowFlag = windowFlag;
			this.m_clipboardFlag = clipboardFlag;
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x0008AC04 File Offset: 0x00088E04
		public UIPermission(UIPermissionWindow windowFlag)
		{
			UIPermission.VerifyWindowFlag(windowFlag);
			this.m_windowFlag = windowFlag;
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x0008AC19 File Offset: 0x00088E19
		public UIPermission(UIPermissionClipboard clipboardFlag)
		{
			UIPermission.VerifyClipboardFlag(clipboardFlag);
			this.m_clipboardFlag = clipboardFlag;
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06002656 RID: 9814 RVA: 0x0008AC3D File Offset: 0x00088E3D
		// (set) Token: 0x06002655 RID: 9813 RVA: 0x0008AC2E File Offset: 0x00088E2E
		public UIPermissionWindow Window
		{
			get
			{
				return this.m_windowFlag;
			}
			set
			{
				UIPermission.VerifyWindowFlag(value);
				this.m_windowFlag = value;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06002658 RID: 9816 RVA: 0x0008AC54 File Offset: 0x00088E54
		// (set) Token: 0x06002657 RID: 9815 RVA: 0x0008AC45 File Offset: 0x00088E45
		public UIPermissionClipboard Clipboard
		{
			get
			{
				return this.m_clipboardFlag;
			}
			set
			{
				UIPermission.VerifyClipboardFlag(value);
				this.m_clipboardFlag = value;
			}
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x0008AC5C File Offset: 0x00088E5C
		private static void VerifyWindowFlag(UIPermissionWindow flag)
		{
			if (flag < UIPermissionWindow.NoWindows || flag > UIPermissionWindow.AllWindows)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
				{
					(int)flag
				}));
			}
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x0008AC85 File Offset: 0x00088E85
		private static void VerifyClipboardFlag(UIPermissionClipboard flag)
		{
			if (flag < UIPermissionClipboard.NoClipboard || flag > UIPermissionClipboard.AllClipboard)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
				{
					(int)flag
				}));
			}
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x0008ACAE File Offset: 0x00088EAE
		private void Reset()
		{
			this.m_windowFlag = UIPermissionWindow.NoWindows;
			this.m_clipboardFlag = UIPermissionClipboard.NoClipboard;
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x0008ACBE File Offset: 0x00088EBE
		private void SetUnrestricted(bool unrestricted)
		{
			if (unrestricted)
			{
				this.m_windowFlag = UIPermissionWindow.AllWindows;
				this.m_clipboardFlag = UIPermissionClipboard.AllClipboard;
			}
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x0008ACD1 File Offset: 0x00088ED1
		public bool IsUnrestricted()
		{
			return this.m_windowFlag == UIPermissionWindow.AllWindows && this.m_clipboardFlag == UIPermissionClipboard.AllClipboard;
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x0008ACE8 File Offset: 0x00088EE8
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_windowFlag == UIPermissionWindow.NoWindows && this.m_clipboardFlag == UIPermissionClipboard.NoClipboard;
			}
			bool result;
			try
			{
				UIPermission uipermission = (UIPermission)target;
				if (uipermission.IsUnrestricted())
				{
					result = true;
				}
				else if (this.IsUnrestricted())
				{
					result = false;
				}
				else
				{
					result = (this.m_windowFlag <= uipermission.m_windowFlag && this.m_clipboardFlag <= uipermission.m_clipboardFlag);
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

		// Token: 0x0600265F RID: 9823 RVA: 0x0008AD88 File Offset: 0x00088F88
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
			UIPermission uipermission = (UIPermission)target;
			UIPermissionWindow uipermissionWindow = (this.m_windowFlag < uipermission.m_windowFlag) ? this.m_windowFlag : uipermission.m_windowFlag;
			UIPermissionClipboard uipermissionClipboard = (this.m_clipboardFlag < uipermission.m_clipboardFlag) ? this.m_clipboardFlag : uipermission.m_clipboardFlag;
			if (uipermissionWindow == UIPermissionWindow.NoWindows && uipermissionClipboard == UIPermissionClipboard.NoClipboard)
			{
				return null;
			}
			return new UIPermission(uipermissionWindow, uipermissionClipboard);
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x0008AE18 File Offset: 0x00089018
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
			UIPermission uipermission = (UIPermission)target;
			UIPermissionWindow uipermissionWindow = (this.m_windowFlag > uipermission.m_windowFlag) ? this.m_windowFlag : uipermission.m_windowFlag;
			UIPermissionClipboard uipermissionClipboard = (this.m_clipboardFlag > uipermission.m_clipboardFlag) ? this.m_clipboardFlag : uipermission.m_clipboardFlag;
			if (uipermissionWindow == UIPermissionWindow.NoWindows && uipermissionClipboard == UIPermissionClipboard.NoClipboard)
			{
				return null;
			}
			return new UIPermission(uipermissionWindow, uipermissionClipboard);
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x0008AEAC File Offset: 0x000890AC
		public override IPermission Copy()
		{
			return new UIPermission(this.m_windowFlag, this.m_clipboardFlag);
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x0008AEC0 File Offset: 0x000890C0
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.UIPermission");
			if (!this.IsUnrestricted())
			{
				if (this.m_windowFlag != UIPermissionWindow.NoWindows)
				{
					securityElement.AddAttribute("Window", Enum.GetName(typeof(UIPermissionWindow), this.m_windowFlag));
				}
				if (this.m_clipboardFlag != UIPermissionClipboard.NoClipboard)
				{
					securityElement.AddAttribute("Clipboard", Enum.GetName(typeof(UIPermissionClipboard), this.m_clipboardFlag));
				}
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x0008AF50 File Offset: 0x00089150
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.SetUnrestricted(true);
				return;
			}
			this.m_windowFlag = UIPermissionWindow.NoWindows;
			this.m_clipboardFlag = UIPermissionClipboard.NoClipboard;
			string text = esd.Attribute("Window");
			if (text != null)
			{
				this.m_windowFlag = (UIPermissionWindow)Enum.Parse(typeof(UIPermissionWindow), text);
			}
			string text2 = esd.Attribute("Clipboard");
			if (text2 != null)
			{
				this.m_clipboardFlag = (UIPermissionClipboard)Enum.Parse(typeof(UIPermissionClipboard), text2);
			}
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x0008AFD6 File Offset: 0x000891D6
		int IBuiltInPermission.GetTokenIndex()
		{
			return UIPermission.GetTokenIndex();
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x0008AFDD File Offset: 0x000891DD
		internal static int GetTokenIndex()
		{
			return 7;
		}

		// Token: 0x04000E98 RID: 3736
		private UIPermissionWindow m_windowFlag;

		// Token: 0x04000E99 RID: 3737
		private UIPermissionClipboard m_clipboardFlag;
	}
}
