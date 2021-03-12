using System;
using System.Collections;

namespace AjaxControlToolkit
{
	// Token: 0x02000016 RID: 22
	public class ProfilePropertyBindingCollection : CollectionBase
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000095 RID: 149 RVA: 0x00003208 File Offset: 0x00001408
		// (remove) Token: 0x06000096 RID: 150 RVA: 0x00003240 File Offset: 0x00001440
		internal event EventHandler CollectionChanged;

		// Token: 0x06000097 RID: 151 RVA: 0x00003275 File Offset: 0x00001475
		internal ProfilePropertyBindingCollection()
		{
		}

		// Token: 0x1700001C RID: 28
		public ProfilePropertyBinding this[int index]
		{
			get
			{
				return (ProfilePropertyBinding)base.InnerList[index];
			}
			set
			{
				base.InnerList[index] = value;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000329F File Offset: 0x0000149F
		public void Add(ProfilePropertyBinding binding)
		{
			base.InnerList.Add(binding);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000032AE File Offset: 0x000014AE
		public void Insert(int index, ProfilePropertyBinding binding)
		{
			base.InnerList.Insert(index, binding);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000032BD File Offset: 0x000014BD
		protected virtual void OnCollectionChanged(EventArgs e)
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, e);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000032D4 File Offset: 0x000014D4
		public void Remove(ProfilePropertyBinding binding)
		{
			base.InnerList.Remove(binding);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000032E2 File Offset: 0x000014E2
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete(index, value);
			this.OnCollectionChanged(EventArgs.Empty);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000032F7 File Offset: 0x000014F7
		protected override void OnSetComplete(int index, object oldValue, object newValue)
		{
			base.OnSetComplete(index, oldValue, newValue);
			this.OnCollectionChanged(EventArgs.Empty);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000330D File Offset: 0x0000150D
		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete(index, value);
			this.OnCollectionChanged(EventArgs.Empty);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003322 File Offset: 0x00001522
		protected override void OnClearComplete()
		{
			base.OnClearComplete();
			this.OnCollectionChanged(EventArgs.Empty);
		}
	}
}
