using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000021 RID: 33
	public class MessageBoxHelper
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00008846 File Offset: 0x00006A46
		// (set) Token: 0x06000175 RID: 373 RVA: 0x0000884D File Offset: 0x00006A4D
		public static Dictionary<MsgBoxButtons, string> ButtonTexts
		{
			get
			{
				return MessageBoxHelper.buttonTexts;
			}
			set
			{
				MessageBoxHelper.buttonTexts = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00008855 File Offset: 0x00006A55
		// (set) Token: 0x06000177 RID: 375 RVA: 0x0000885C File Offset: 0x00006A5C
		public static Dictionary<MsgBoxIcon, string> CaptureTexts
		{
			get
			{
				return MessageBoxHelper.captureTexts;
			}
			set
			{
				MessageBoxHelper.captureTexts = value;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000088DD File Offset: 0x00006ADD
		public static DialogResult ShowError(string text)
		{
			return MessageBoxHelper.ShowError(null, text);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000088E6 File Offset: 0x00006AE6
		public static DialogResult ShowError(IWin32Window owner, string text)
		{
			return MessageBoxHelper.Show(owner, text, MessageBoxButtons.OK, MsgBoxIcon.Error);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000088F1 File Offset: 0x00006AF1
		public static DialogResult ShowWarning(string text)
		{
			return MessageBoxHelper.ShowWarning(null, text);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000088FA File Offset: 0x00006AFA
		public static DialogResult ShowWarning(IWin32Window owner, string text)
		{
			return MessageBoxHelper.Show(owner, text, MessageBoxButtons.OK, MsgBoxIcon.Warning);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00008905 File Offset: 0x00006B05
		public static DialogResult ShowCancel(string text)
		{
			return MessageBoxHelper.ShowCancel(null, text);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000890E File Offset: 0x00006B0E
		public static DialogResult ShowCancel(IWin32Window owner, string text)
		{
			return MessageBoxHelper.Show(owner, text, MessageBoxButtons.YesNo, MsgBoxIcon.Cancel);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00008919 File Offset: 0x00006B19
		public static DialogResult ShowInformation(string text)
		{
			return MessageBoxHelper.ShowInformation(null, text);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008922 File Offset: 0x00006B22
		public static DialogResult ShowInformation(IWin32Window owner, string text)
		{
			return MessageBoxHelper.Show(owner, text, MessageBoxButtons.OK, MsgBoxIcon.None);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00008930 File Offset: 0x00006B30
		public static DialogResult Show(IWin32Window owner, string text, MessageBoxButtons buttons, MsgBoxIcon icon)
		{
			DialogResult result;
			using (CustomMessageBox customMessageBox = new CustomMessageBox(MessageBoxHelper.buttonTexts, MessageBoxHelper.captureTexts))
			{
				customMessageBox.Owner = (Form)owner;
				if (customMessageBox.Owner == null)
				{
					customMessageBox.StartPosition = FormStartPosition.CenterScreen;
				}
				else
				{
					customMessageBox.StartPosition = FormStartPosition.CenterParent;
				}
				customMessageBox.MessageIcon = icon;
				customMessageBox.Buttons = buttons;
				customMessageBox.MessageText = text;
				customMessageBox.TopMost = true;
				customMessageBox.BringToFront();
				result = customMessageBox.ShowDialog();
				customMessageBox.TopMost = false;
			}
			return result;
		}

		// Token: 0x040000D0 RID: 208
		private static Dictionary<MsgBoxButtons, string> buttonTexts = new Dictionary<MsgBoxButtons, string>
		{
			{
				MsgBoxButtons.OK,
				string.Empty
			},
			{
				MsgBoxButtons.Yes,
				string.Empty
			},
			{
				MsgBoxButtons.No,
				string.Empty
			}
		};

		// Token: 0x040000D1 RID: 209
		private static Dictionary<MsgBoxIcon, string> captureTexts = new Dictionary<MsgBoxIcon, string>
		{
			{
				MsgBoxIcon.None,
				string.Empty
			},
			{
				MsgBoxIcon.Error,
				string.Empty
			},
			{
				MsgBoxIcon.Cancel,
				string.Empty
			},
			{
				MsgBoxIcon.Warning,
				string.Empty
			}
		};
	}
}
