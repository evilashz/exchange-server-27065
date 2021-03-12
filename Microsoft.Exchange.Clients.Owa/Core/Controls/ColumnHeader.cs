using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002B6 RID: 694
	public class ColumnHeader
	{
		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001B2E RID: 6958 RVA: 0x0009B931 File Offset: 0x00099B31
		// (set) Token: 0x06001B2F RID: 6959 RVA: 0x0009B939 File Offset: 0x00099B39
		public Strings.IDs TextID
		{
			get
			{
				return this.textID;
			}
			set
			{
				this.textID = value;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001B30 RID: 6960 RVA: 0x0009B942 File Offset: 0x00099B42
		// (set) Token: 0x06001B31 RID: 6961 RVA: 0x0009B94A File Offset: 0x00099B4A
		public ThemeFileId Image
		{
			get
			{
				return this.image;
			}
			set
			{
				this.image = value;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x0009B953 File Offset: 0x00099B53
		public bool IsImageHeader
		{
			get
			{
				return this.image != ThemeFileId.None;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x0009B961 File Offset: 0x00099B61
		public bool IsCheckBoxHeader
		{
			get
			{
				return this.isCheckBoxHeader;
			}
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0009B969 File Offset: 0x00099B69
		private ColumnHeader(Strings.IDs textID, ThemeFileId image, bool isCheckbox)
		{
			this.textID = textID;
			if (isCheckbox)
			{
				this.image = ThemeFileId.None;
				this.isCheckBoxHeader = true;
				return;
			}
			this.image = image;
			this.isCheckBoxHeader = false;
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x0009B9A3 File Offset: 0x00099BA3
		public ColumnHeader(Strings.IDs textID) : this(textID, ThemeFileId.None, false)
		{
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x0009B9AE File Offset: 0x00099BAE
		public ColumnHeader(bool isCheckbox) : this(-1018465893, ThemeFileId.None, true)
		{
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0009B9BD File Offset: 0x00099BBD
		public ColumnHeader(ThemeFileId image) : this(-1018465893, image, false)
		{
		}

		// Token: 0x04001335 RID: 4917
		private Strings.IDs textID = -1018465893;

		// Token: 0x04001336 RID: 4918
		private ThemeFileId image;

		// Token: 0x04001337 RID: 4919
		private bool isCheckBoxHeader;
	}
}
