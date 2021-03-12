using System;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000185 RID: 389
	[Serializable]
	public class UIValidationError
	{
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x0003B157 File Offset: 0x00039357
		public static UIValidationError[] None
		{
			get
			{
				return UIValidationError.none;
			}
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0003B15E File Offset: 0x0003935E
		public UIValidationError(LocalizedString description, Control errorProviderAnchor)
		{
			this.description = description;
			this.errorProviderAnchor = errorProviderAnchor;
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x0003B174 File Offset: 0x00039374
		public LocalizedString Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x0003B17C File Offset: 0x0003937C
		public Control ErrorProviderAnchor
		{
			get
			{
				return this.errorProviderAnchor;
			}
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0003B184 File Offset: 0x00039384
		public override bool Equals(object right)
		{
			if (right == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, right))
			{
				return true;
			}
			if (base.GetType() != right.GetType())
			{
				return false;
			}
			UIValidationError uivalidationError = right as UIValidationError;
			return string.Compare(this.Description, uivalidationError.Description) == 0 && this.ErrorProviderAnchor == uivalidationError.ErrorProviderAnchor;
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0003B1F0 File Offset: 0x000393F0
		public override int GetHashCode()
		{
			return this.Description.GetHashCode() ^ this.ErrorProviderAnchor.GetHashCode();
		}

		// Token: 0x04000606 RID: 1542
		private LocalizedString description;

		// Token: 0x04000607 RID: 1543
		private Control errorProviderAnchor;

		// Token: 0x04000608 RID: 1544
		private static readonly UIValidationError[] none = new UIValidationError[0];
	}
}
