using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000188 RID: 392
	public class WizardPageCollection : BindingList<WizardPage>
	{
		// Token: 0x06000F50 RID: 3920 RVA: 0x0003B2E0 File Offset: 0x000394E0
		public WizardPageCollection(WizardPage parentPage)
		{
			if (parentPage == null)
			{
				throw new ArgumentNullException();
			}
			this.parentPage = parentPage;
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x0003B2F8 File Offset: 0x000394F8
		public WizardPage ParentPage
		{
			get
			{
				return this.parentPage;
			}
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0003B300 File Offset: 0x00039500
		protected override void InsertItem(int index, WizardPage item)
		{
			if (base.Contains(item))
			{
				if (index >= base.Count)
				{
					index--;
				}
				base.Remove(item);
			}
			item.ParentPage = this.parentPage;
			base.InsertItem(index, item);
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0003B338 File Offset: 0x00039538
		protected override void RemoveItem(int index)
		{
			WizardPage wizardPage = base[index];
			base.RemoveItem(index);
			wizardPage.ParentPage = null;
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0003B35B File Offset: 0x0003955B
		protected override void SetItem(int index, WizardPage item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0003B364 File Offset: 0x00039564
		protected override void ClearItems()
		{
			WizardPage[] array = new WizardPage[base.Count];
			base.CopyTo(array, 0);
			base.ClearItems();
			foreach (WizardPage wizardPage in array)
			{
				wizardPage.ParentPage = null;
			}
		}

		// Token: 0x0400060A RID: 1546
		private WizardPage parentPage;
	}
}
