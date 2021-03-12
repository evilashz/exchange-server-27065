using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x02000023 RID: 35
	public class SecureTextBox : TextBox
	{
		// Token: 0x06000185 RID: 389 RVA: 0x00008A21 File Offset: 0x00006C21
		public SecureTextBox()
		{
			this.InitializeComponent();
			base.PasswordChar = '*';
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00008A42 File Offset: 0x00006C42
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00008A4A File Offset: 0x00006C4A
		public SecureString SecureText
		{
			get
			{
				return this.secureString;
			}
			set
			{
				this.secureString = value;
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00008A53 File Offset: 0x00006C53
		protected override bool ProcessKeyMessage(ref Message m)
		{
			if (this.displayChar)
			{
				return base.ProcessKeyMessage(ref m);
			}
			this.displayChar = true;
			return true;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00008A70 File Offset: 0x00006C70
		protected override bool IsInputChar(char charCode)
		{
			int num = base.SelectionStart;
			bool flag = base.IsInputChar(charCode);
			if (flag)
			{
				if (!char.IsControl(charCode) && !char.IsHighSurrogate(charCode) && !char.IsLowSurrogate(charCode))
				{
					if (this.SelectionLength > 0)
					{
						for (int i = 0; i < this.SelectionLength; i++)
						{
							this.secureString.RemoveAt(base.SelectionStart);
						}
					}
					if (num == this.secureString.Length)
					{
						this.secureString.AppendChar(charCode);
					}
					else
					{
						this.secureString.InsertAt(num, charCode);
					}
					if (this.secureString.Length > 0)
					{
						this.Text = new string('*', this.secureString.Length);
					}
					base.SelectionStart = this.secureString.Length;
					this.displayChar = false;
					num++;
				}
				else if (charCode == '\b')
				{
					if (this.SelectionLength == 0 && num > 0)
					{
						num--;
						this.secureString.RemoveAt(num);
						this.Text = new string('*', this.secureString.Length);
						base.SelectionStart = num;
					}
					else if (this.SelectionLength > 0)
					{
						if (this.SelectionLength == this.secureString.Length)
						{
							this.secureString.Clear();
						}
						else
						{
							for (int j = 0; j < this.SelectionLength; j++)
							{
								this.secureString.RemoveAt(base.SelectionStart);
							}
						}
						if (this.secureString.Length > 0)
						{
							this.Text = new string('*', this.secureString.Length);
							base.SelectionStart = num;
						}
						else
						{
							this.Text = string.Empty;
						}
					}
					this.displayChar = false;
				}
			}
			else
			{
				this.displayChar = true;
			}
			return flag;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00008C2C File Offset: 0x00006E2C
		protected override bool IsInputKey(Keys keyData)
		{
			bool result = true;
			if ((keyData & Keys.Delete) == Keys.Delete)
			{
				if (this.SelectionLength == this.secureString.Length)
				{
					this.secureString.Clear();
				}
				if (this.SelectionLength > 0)
				{
					for (int i = 0; i < this.SelectionLength; i++)
					{
						this.secureString.RemoveAt(base.SelectionStart);
					}
				}
				else if (base.SelectionStart < this.Text.Length)
				{
					this.secureString.RemoveAt(base.SelectionStart);
				}
			}
			return result;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008CB3 File Offset: 0x00006EB3
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00008CD4 File Offset: 0x00006ED4
		internal char[] GetCharacterData()
		{
			char[] array = new char[this.secureString.Length];
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.SecureStringToBSTR(this.secureString);
				array = new char[this.secureString.Length];
				Marshal.Copy(intPtr, array, 0, this.secureString.Length);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeBSTR(intPtr);
				}
			}
			return array;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008D50 File Offset: 0x00006F50
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		// Token: 0x040000D2 RID: 210
		private IContainer components;

		// Token: 0x040000D3 RID: 211
		private bool displayChar;

		// Token: 0x040000D4 RID: 212
		private SecureString secureString = new SecureString();
	}
}
