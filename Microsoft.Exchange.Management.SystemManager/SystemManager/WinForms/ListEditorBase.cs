using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.Commands;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000145 RID: 325
	public abstract class ListEditorBase<TObject> : DataListControl
	{
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0002FAE7 File Offset: 0x0002DCE7
		protected Dictionary<TObject, string> ChangedObjects
		{
			get
			{
				return this.changedObjects;
			}
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0002FAF0 File Offset: 0x0002DCF0
		public ListEditorBase()
		{
			this.InitializeComponent();
			base.DataListView.VirtualMode = true;
			this.addObjectCommand = new Command();
			this.addObjectCommand.Name = "addObject";
			this.addObjectCommand.Description = Strings.AddObjectDescription;
			this.addObjectCommand.Execute += this.addObjectCommand_Execute;
			this.addObjectCommand.Icon = Icons.Add;
			base.DataListView.RemoveCommand.Executing += this.RemoveCommand_Executing;
			base.DataListView.DataSource = null;
			this.addObjectToolStripItem = new CommandToolStripButton(this.addObjectCommand);
			this.addObjectToolStripItem.Visible = false;
			this.removeObjectToolStripItem = new CommandToolStripButton(base.DataListView.RemoveCommand);
			this.removeObjectToolStripItem.Visible = false;
			base.ToolStripItems.Add(this.addObjectToolStripItem);
			base.ToolStripItems.Add(this.removeObjectToolStripItem);
			this.deleteContextmenu = new CommandMenuItem(base.DataListView.RemoveCommand, base.Components);
			this.deleteContextmenu.Visible = false;
			base.DataListView.ContextMenu.MenuItems.Add(this.deleteContextmenu);
			base.DataListView.AutoGenerateColumns = false;
			base.DataListView.AllowRemove = true;
			base.DataListView.IconLibrary = ObjectPicker.ObjectClassIconLibrary;
			this.AddButtonText = Strings.AddObject;
			this.RemoveButtonText = Strings.ListEditRemove;
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0002FCAB File Offset: 0x0002DEAB
		private void RemoveCommand_Executing(object sender, CancelEventArgs e)
		{
			this.ExtractChangedObjects(base.DataListView.SelectedObjects);
			this.RemoveFromIdentityList(this.changedObjects.Keys);
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0002FCD0 File Offset: 0x0002DED0
		private void addObjectCommand_Execute(object sender, EventArgs e)
		{
			ObjectPicker objectPicker = this.ObjectPickerForEdit ?? this.ObjectPicker;
			if (objectPicker.ShowDialog() == DialogResult.OK && !this.IsResolving)
			{
				if (base.DataListView.DataSource is DataTable)
				{
					DataTable dataTable = (DataTable)base.DataListView.DataSource;
					dataTable.Merge(objectPicker.SelectedObjects);
				}
				else
				{
					base.DataListView.DataSource = objectPicker.SelectedObjects.Copy();
				}
				this.ExtractChangedObjects(objectPicker.SelectedObjects.Rows);
				this.AddToIdentityList(this.changedObjects.Keys);
			}
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0002FD68 File Offset: 0x0002DF68
		private void ExtractChangedObjects(ICollection changedRowList)
		{
			this.changedObjects.Clear();
			foreach (object obj in changedRowList)
			{
				if (obj is DataRow || obj is DataRowView)
				{
					DataRow dataRow = (obj is DataRow) ? ((DataRow)obj) : ((DataRowView)obj).Row;
					this.InsertChangedObject(dataRow);
				}
			}
		}

		// Token: 0x06000CF5 RID: 3317
		protected abstract void InsertChangedObject(DataRow dataRow);

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0002FDF0 File Offset: 0x0002DFF0
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x0002FDFD File Offset: 0x0002DFFD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string AddButtonText
		{
			get
			{
				return this.addObjectCommand.Text;
			}
			set
			{
				this.addObjectCommand.Text = value;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0002FE0B File Offset: 0x0002E00B
		// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x0002FE1D File Offset: 0x0002E01D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string RemoveButtonText
		{
			get
			{
				return base.DataListView.RemoveCommand.Text;
			}
			set
			{
				base.DataListView.RemoveCommand.Text = value;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0002FE30 File Offset: 0x0002E030
		// (set) Token: 0x06000CFB RID: 3323 RVA: 0x0002FE38 File Offset: 0x0002E038
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		[Browsable(false)]
		public ObjectPicker ObjectPicker
		{
			get
			{
				return this.objectPicker;
			}
			set
			{
				if (this.objectPicker != value)
				{
					this.objectPicker = value;
					if (this.objectPicker != null)
					{
						this.objectPicker.AllowMultiSelect = true;
					}
					this.addObjectToolStripItem.Enabled = (this.objectPicker != null);
					if (this.objectResolver != null)
					{
						this.objectResolver.ResolveObjectIdsCompleted -= this.objectResolver_ResolveObjectIdsCompleted;
						this.objectResolver.IsResolvingChanged -= this.objectResolver_IsResolvingChanged;
					}
					this.objectResolver = new ObjectResolver(this.objectPicker)
					{
						PrefillBeforeResolving = true
					};
					this.objectResolver.ResolveObjectIdsCompleted += this.objectResolver_ResolveObjectIdsCompleted;
					this.objectResolver.IsResolvingChanged += this.objectResolver_IsResolvingChanged;
					base.DataListView.DataSourceRefresher = this.objectResolver.Refresher;
					base.DataListView.IdentityProperty = ((this.ObjectPicker != null) ? this.ObjectPicker.IdentityProperty : null);
					base.DataListView.ImagePropertyName = ((this.ObjectPicker != null) ? this.ObjectPicker.ImageProperty : null);
					base.DataListView.DataSource = this.objectResolver.ResolvedObjects;
					if (string.IsNullOrEmpty(base.DataListView.SortProperty) && this.ObjectPicker != null)
					{
						base.DataListView.SortProperty = this.ObjectPicker.DefaultSortProperty;
					}
				}
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0002FF9F File Offset: 0x0002E19F
		private void objectResolver_IsResolvingChanged(object sender, EventArgs e)
		{
			this.UpdateToolStripButtonStatus();
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x0002FFA7 File Offset: 0x0002E1A7
		// (set) Token: 0x06000CFE RID: 3326 RVA: 0x0002FFAF File Offset: 0x0002E1AF
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[DefaultValue(null)]
		public ObjectPicker ObjectPickerForEdit
		{
			get
			{
				return this.secondPickerForEdit;
			}
			set
			{
				this.secondPickerForEdit = value;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x0002FFB8 File Offset: 0x0002E1B8
		// (set) Token: 0x06000D00 RID: 3328 RVA: 0x0002FFC0 File Offset: 0x0002E1C0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(false)]
		public bool Editable
		{
			get
			{
				return this.isEditable;
			}
			set
			{
				if (value != this.isEditable)
				{
					this.isEditable = value;
					this.UpdateToolStripButtonStatus();
				}
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x0002FFD8 File Offset: 0x0002E1D8
		// (set) Token: 0x06000D02 RID: 3330 RVA: 0x0002FFE0 File Offset: 0x0002E1E0
		[DefaultValue(false)]
		public bool ReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				if (value != this.isReadOnly)
				{
					this.isReadOnly = value;
					this.UpdateToolStripButtonStatus();
				}
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x0002FFF8 File Offset: 0x0002E1F8
		// (set) Token: 0x06000D04 RID: 3332 RVA: 0x00030000 File Offset: 0x0002E200
		[DefaultValue(true)]
		public bool ImageShown
		{
			get
			{
				return this.imageShown;
			}
			set
			{
				if (value != this.imageShown)
				{
					this.imageShown = value;
					base.DataListView.IconLibrary = (value ? ObjectPicker.ObjectClassIconLibrary : null);
				}
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00030028 File Offset: 0x0002E228
		// (set) Token: 0x06000D06 RID: 3334 RVA: 0x00030030 File Offset: 0x0002E230
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal ADPropertyDefinition ObjectFilterProperty
		{
			get
			{
				return this.objectFilterProperty;
			}
			set
			{
				this.objectFilterProperty = value;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00030039 File Offset: 0x0002E239
		// (set) Token: 0x06000D08 RID: 3336 RVA: 0x00030041 File Offset: 0x0002E241
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		[Browsable(false)]
		public object ObjectFilterTarget
		{
			get
			{
				return this.objectFilterTarget;
			}
			set
			{
				if (this.ObjectFilterTarget != value)
				{
					this.objectFilterTarget = value;
					this.OnObjectFilterTargetChanged();
					this.ResolveObjectList();
				}
			}
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00030060 File Offset: 0x0002E260
		private void OnObjectFilterTargetChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListEditorBase<TObject>.EventObjectFilterTargetChanged];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06000D0A RID: 3338 RVA: 0x00030092 File Offset: 0x0002E292
		// (remove) Token: 0x06000D0B RID: 3339 RVA: 0x000300A5 File Offset: 0x0002E2A5
		public event EventHandler ObjectFilterTargetChanged
		{
			add
			{
				base.Events.AddHandler(ListEditorBase<TObject>.EventObjectFilterTargetChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListEditorBase<TObject>.EventObjectFilterTargetChanged, value);
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x000300B8 File Offset: 0x0002E2B8
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x000300C0 File Offset: 0x0002E2C0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public MultiValuedProperty<TObject> IdentityList
		{
			get
			{
				return this.identityMvpList;
			}
			set
			{
				this.identityMvpList = value;
			}
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x000300C9 File Offset: 0x0002E2C9
		protected virtual void AddToIdentityList(ICollection identities)
		{
			this.UpdateIdentityList(identities, false);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x000300D3 File Offset: 0x0002E2D3
		protected virtual void RemoveFromIdentityList(ICollection identities)
		{
			this.UpdateIdentityList(identities, true);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x000300E0 File Offset: 0x0002E2E0
		private void UpdateIdentityList(ICollection identities, bool remove)
		{
			if (identities != null && identities.Count > 0)
			{
				if (this.IdentityList == null)
				{
					this.IdentityList = new MultiValuedProperty<TObject>();
				}
				base.NotifyExposedPropertyIsModified();
				foreach (object obj in identities)
				{
					TObject item = (TObject)((object)obj);
					try
					{
						if (remove)
						{
							this.IdentityList.Remove(item);
						}
						else
						{
							this.IdentityList.Add(item);
						}
					}
					catch (InvalidOperationException)
					{
					}
				}
				this.OnIdentityListChanged(new IdentityListChangedEventArgs<TObject>(remove ? 1 : 0, this.ChangedObjects));
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x000301A0 File Offset: 0x0002E3A0
		protected virtual void OnIdentityListChanged(IdentityListChangedEventArgs<TObject> e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListEditorBase<TObject>.EventIdentityListChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06000D12 RID: 3346 RVA: 0x000301CE File Offset: 0x0002E3CE
		// (remove) Token: 0x06000D13 RID: 3347 RVA: 0x000301E1 File Offset: 0x0002E3E1
		public event EventHandler IdentityListChanged
		{
			add
			{
				base.Events.AddHandler(ListEditorBase<TObject>.EventIdentityListChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListEditorBase<TObject>.EventIdentityListChanged, value);
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x000301F4 File Offset: 0x0002E3F4
		private void ResolveObjectList()
		{
			if (this.ObjectFilterTarget != null && DBNull.Value != this.ObjectFilterTarget && !this.IsResolving)
			{
				ICollection collection = null;
				if (this.ObjectFilterTarget is TObject)
				{
					collection = new TObject[]
					{
						(TObject)((object)this.ObjectFilterTarget)
					};
				}
				else if (this.ObjectFilterTarget is MultiValuedProperty<TObject>)
				{
					MultiValuedProperty<TObject> multiValuedProperty = (MultiValuedProperty<TObject>)this.ObjectFilterTarget;
					if (multiValuedProperty.Count > 0)
					{
						collection = multiValuedProperty;
					}
				}
				if (collection != null)
				{
					if (base.DataListView.AvailableColumns.Count > 1)
					{
						base.DataListView.StatusPropertyName = "LoadStatusColumn";
					}
					else if (!this.FastResolving)
					{
						this.objectResolver.PrefillBeforeResolving = false;
					}
					this.objectResolver.ResolvedObjects.Rows.Clear();
					this.objectResolver.ResolveObjectIds(this.ObjectFilterProperty, collection);
					base.DataListView.DataSource = this.objectResolver.ResolvedObjects;
					return;
				}
				base.DataListView.DataSource = null;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x000302FD File Offset: 0x0002E4FD
		// (set) Token: 0x06000D16 RID: 3350 RVA: 0x0003030A File Offset: 0x0002E50A
		public bool PrefillBeforeResolving
		{
			get
			{
				return this.objectResolver.PrefillBeforeResolving;
			}
			set
			{
				this.objectResolver.PrefillBeforeResolving = value;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x00030318 File Offset: 0x0002E518
		// (set) Token: 0x06000D18 RID: 3352 RVA: 0x00030325 File Offset: 0x0002E525
		public bool FastResolving
		{
			get
			{
				return this.objectResolver.FastResolving;
			}
			set
			{
				this.objectResolver.FastResolving = value;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x00030333 File Offset: 0x0002E533
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsResolving
		{
			get
			{
				return this.objectResolver != null && this.objectResolver.IsResolving;
			}
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0003034A File Offset: 0x0002E54A
		public void LoadObjects(ADObjectId containerId, QueryFilter filter)
		{
			if (!this.IsResolving)
			{
				this.objectResolver.ResolvedObjects.Rows.Clear();
				this.objectResolver.ResolveObjects(containerId, filter);
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00030376 File Offset: 0x0002E576
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.UpdateToolStripButtonStatus();
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00030388 File Offset: 0x0002E588
		private void UpdateToolStripButtonStatus()
		{
			if (base.IsHandleCreated)
			{
				base.DataListView.AllowRemove = (this.Editable && !this.ReadOnly && !this.IsResolving);
				if (this.addObjectToolStripItem != null)
				{
					this.addObjectToolStripItem.Visible = this.Editable;
					this.addObjectToolStripItem.Enabled = (!this.ReadOnly && !this.IsResolving);
				}
				if (this.removeObjectToolStripItem != null)
				{
					this.removeObjectToolStripItem.Visible = this.Editable;
					this.removeObjectToolStripItem.Enabled = (!this.ReadOnly && !this.IsResolving && base.DataListView.RemoveCommand.Enabled);
				}
				if (this.deleteContextmenu != null)
				{
					this.deleteContextmenu.Visible = this.Editable;
					this.deleteContextmenu.Enabled = this.removeObjectToolStripItem.Enabled;
				}
			}
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00030478 File Offset: 0x0002E678
		private void objectResolver_ResolveObjectIdsCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				base.ShowError(Strings.ErrorNotAllObjectsLoaded(e.Error.Message));
			}
			this.ExtractChangedObjects(this.objectResolver.ResolvedObjects.Rows);
			this.TrackResolvedObjects(this.ChangedObjects.Keys);
			base.DataListView.BackupItemsStates();
			base.DataListView.RestoreItemsStates(false);
			this.OnResolveCompleted(EventArgs.Empty);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x000304F1 File Offset: 0x0002E6F1
		protected virtual void TrackResolvedObjects(ICollection identities)
		{
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000304F4 File Offset: 0x0002E6F4
		protected virtual void OnResolveCompleted(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListEditorBase<TObject>.EventResolveCompleted];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06000D20 RID: 3360 RVA: 0x00030522 File Offset: 0x0002E722
		// (remove) Token: 0x06000D21 RID: 3361 RVA: 0x00030535 File Offset: 0x0002E735
		public event EventHandler ResolveCompleted
		{
			add
			{
				base.Events.AddHandler(ListEditorBase<TObject>.EventResolveCompleted, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListEditorBase<TObject>.EventResolveCompleted, value);
			}
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00030548 File Offset: 0x0002E748
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(8f, 16f);
			base.Name = "ListEditorBase";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00030580 File Offset: 0x0002E780
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.objectResolver != null)
			{
				this.objectResolver.ResolveObjectIdsCompleted -= this.objectResolver_ResolveObjectIdsCompleted;
				this.objectResolver.IsResolvingChanged -= this.objectResolver_IsResolvingChanged;
				this.objectResolver.Refresher.CancelRefresh();
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x000305DD File Offset: 0x0002E7DD
		internal DataTable ResolvedObjects
		{
			get
			{
				return this.objectResolver.ResolvedObjects;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x000305EC File Offset: 0x0002E7EC
		public override Dictionary<string, HashSet<Control>> ExposedPropertyRelatedControls
		{
			get
			{
				Dictionary<string, HashSet<Control>> exposedPropertyRelatedControls = base.ExposedPropertyRelatedControls;
				if (!exposedPropertyRelatedControls.ContainsKey("ObjectFilterTarget"))
				{
					exposedPropertyRelatedControls.Add("ObjectFilterTarget", base.GetChildControls());
				}
				return exposedPropertyRelatedControls;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x0003061F File Offset: 0x0002E81F
		protected override string ExposedPropertyName
		{
			get
			{
				return "IdentityList";
			}
		}

		// Token: 0x0400052F RID: 1327
		private Command addObjectCommand;

		// Token: 0x04000530 RID: 1328
		private ObjectPicker objectPicker;

		// Token: 0x04000531 RID: 1329
		private ObjectPicker secondPickerForEdit;

		// Token: 0x04000532 RID: 1330
		private ObjectResolver objectResolver;

		// Token: 0x04000533 RID: 1331
		private CommandToolStripButton addObjectToolStripItem;

		// Token: 0x04000534 RID: 1332
		private CommandToolStripButton removeObjectToolStripItem;

		// Token: 0x04000535 RID: 1333
		private CommandMenuItem deleteContextmenu;

		// Token: 0x04000536 RID: 1334
		private Dictionary<TObject, string> changedObjects = new Dictionary<TObject, string>();

		// Token: 0x04000537 RID: 1335
		private bool isEditable;

		// Token: 0x04000538 RID: 1336
		private bool isReadOnly;

		// Token: 0x04000539 RID: 1337
		private bool imageShown = true;

		// Token: 0x0400053A RID: 1338
		private ADPropertyDefinition objectFilterProperty = ADObjectSchema.Id;

		// Token: 0x0400053B RID: 1339
		private object objectFilterTarget;

		// Token: 0x0400053C RID: 1340
		private static readonly object EventObjectFilterTargetChanged = new object();

		// Token: 0x0400053D RID: 1341
		private MultiValuedProperty<TObject> identityMvpList = new MultiValuedProperty<TObject>();

		// Token: 0x0400053E RID: 1342
		private static readonly object EventIdentityListChanged = new object();

		// Token: 0x0400053F RID: 1343
		private static readonly object EventResolveCompleted = new object();
	}
}
