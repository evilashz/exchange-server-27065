using System;
using System.Collections;
using System.ComponentModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000147 RID: 327
	public class ObjectChangeTrackListEditor : ObjectListEditor
	{
		// Token: 0x06000D2A RID: 3370 RVA: 0x0003068F File Offset: 0x0002E88F
		public ObjectChangeTrackListEditor()
		{
			base.Name = "ObjectChangeTrackListEditor";
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x000306B0 File Offset: 0x0002E8B0
		protected override void TrackResolvedObjects(ICollection identities)
		{
			if (identities != null)
			{
				this.resolvedObjects.Clear();
				foreach (object obj in identities)
				{
					ADObjectId item = (ADObjectId)obj;
					this.resolvedObjects.Add(item);
				}
				this.resolvedObjects.ResetChangeTracking();
			}
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00030724 File Offset: 0x0002E924
		protected override void AddToIdentityList(ICollection identities)
		{
			this.UpdateIdentityList(identities, false);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0003072E File Offset: 0x0002E92E
		protected override void RemoveFromIdentityList(ICollection identities)
		{
			this.UpdateIdentityList(identities, true);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00030738 File Offset: 0x0002E938
		private void UpdateIdentityList(ICollection identities, bool removing)
		{
			if (identities != null && this.resolvedObjects != null)
			{
				int num = this.resolvedObjects.Added.Length;
				int num2 = this.resolvedObjects.Removed.Length;
				foreach (object obj in identities)
				{
					ADObjectId item = (ADObjectId)obj;
					try
					{
						if (removing)
						{
							this.resolvedObjects.Remove(item);
						}
						else
						{
							this.resolvedObjects.Add(item);
						}
					}
					catch (InvalidOperationException)
					{
					}
				}
				if (num != this.resolvedObjects.Added.Length)
				{
					this.OnAddedIdentityListChanged(EventArgs.Empty);
				}
				if (num2 != this.resolvedObjects.Removed.Length)
				{
					this.OnRemovedIdentityListChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x0003081C File Offset: 0x0002EA1C
		// (set) Token: 0x06000D30 RID: 3376 RVA: 0x00030829 File Offset: 0x0002EA29
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		public object[] AddedIdentityList
		{
			get
			{
				return this.resolvedObjects.Added;
			}
			set
			{
			}
		}

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06000D31 RID: 3377 RVA: 0x0003082C File Offset: 0x0002EA2C
		// (remove) Token: 0x06000D32 RID: 3378 RVA: 0x00030864 File Offset: 0x0002EA64
		public event EventHandler AddedIdentityListChanged;

		// Token: 0x06000D33 RID: 3379 RVA: 0x00030899 File Offset: 0x0002EA99
		protected virtual void OnAddedIdentityListChanged(EventArgs e)
		{
			if (this.AddedIdentityListChanged != null)
			{
				this.AddedIdentityListChanged(this, e);
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x000308B0 File Offset: 0x0002EAB0
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x000308BD File Offset: 0x0002EABD
		[DefaultValue(null)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object[] RemovedIdentityList
		{
			get
			{
				return this.resolvedObjects.Removed;
			}
			set
			{
			}
		}

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06000D36 RID: 3382 RVA: 0x000308C0 File Offset: 0x0002EAC0
		// (remove) Token: 0x06000D37 RID: 3383 RVA: 0x000308F8 File Offset: 0x0002EAF8
		public event EventHandler RemovedIdentityListChanged;

		// Token: 0x06000D38 RID: 3384 RVA: 0x0003092D File Offset: 0x0002EB2D
		protected virtual void OnRemovedIdentityListChanged(EventArgs e)
		{
			if (this.RemovedIdentityListChanged != null)
			{
				this.RemovedIdentityListChanged(this, e);
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x00030944 File Offset: 0x0002EB44
		// (set) Token: 0x06000D3A RID: 3386 RVA: 0x0003094B File Offset: 0x0002EB4B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		public new MultiValuedProperty<ADObjectId> IdentityList
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x04000540 RID: 1344
		private MultiValuedProperty<ADObjectId> resolvedObjects = new MultiValuedProperty<ADObjectId>();
	}
}
