using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000141 RID: 321
	[ToolboxItemFilter("System.Windows.Forms")]
	[ProvideProperty("Enabled", typeof(BindingSource))]
	public class InputValidationProvider : Component, IExtenderProvider
	{
		// Token: 0x06000CC4 RID: 3268 RVA: 0x0002E6D8 File Offset: 0x0002C8D8
		public InputValidationProvider()
		{
			this.components = new Container();
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002E743 File Offset: 0x0002C943
		public InputValidationProvider(IContainer container) : this()
		{
			container.Add(this);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002E752 File Offset: 0x0002C952
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ContainerControl = null;
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0002E770 File Offset: 0x0002C970
		bool IExtenderProvider.CanExtend(object extendee)
		{
			return extendee is BindingSource;
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0002E77B File Offset: 0x0002C97B
		[DefaultValue(false)]
		public bool GetEnabled(BindingSource bindingSource)
		{
			return this.enabledBindingSources.ContainsKey(bindingSource);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0002E78C File Offset: 0x0002C98C
		public void SetEnabled(BindingSource bindingSource, bool enabled)
		{
			if (enabled != this.GetEnabled(bindingSource))
			{
				if (enabled)
				{
					ExchangeErrorProvider exchangeErrorProvider = new ExchangeErrorProvider(this.components);
					((ISupportInitialize)exchangeErrorProvider).BeginInit();
					exchangeErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
					exchangeErrorProvider.ContainerControl = this.ContainerControl;
					((ISupportInitialize)exchangeErrorProvider).EndInit();
					this.enabledBindingSources.Add(bindingSource, exchangeErrorProvider);
					if (this.ContainerBindingContext != null)
					{
						this.BindToBindingSource(bindingSource);
						return;
					}
				}
				else
				{
					ExchangeErrorProvider exchangeErrorProvider2 = this.enabledBindingSources[bindingSource];
					this.components.Remove(exchangeErrorProvider2);
					exchangeErrorProvider2.Dispose();
					this.enabledBindingSources.Remove(bindingSource);
					if (this.ContainerBindingContext != null)
					{
						this.UnbindFromBindingSource(bindingSource);
					}
				}
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x0002E82C File Offset: 0x0002CA2C
		// (set) Token: 0x06000CCB RID: 3275 RVA: 0x0002E834 File Offset: 0x0002CA34
		[DefaultValue(null)]
		public ContainerControl ContainerControl
		{
			get
			{
				return this.containerControl;
			}
			set
			{
				if (this.ContainerControl != value)
				{
					if (this.ContainerControl != null)
					{
						this.ContainerControl.BindingContextChanged -= this.ContainerControl_BindingContextChanged;
						this.ContainerBindingContext = null;
					}
					this.containerControl = value;
					if (this.ContainerControl != null)
					{
						this.ContainerControl.AutoValidate = AutoValidate.EnableAllowFocusChange;
						this.ContainerControl.BindingContextChanged += this.ContainerControl_BindingContextChanged;
						this.ContainerBindingContext = this.ContainerControl.BindingContext;
					}
					foreach (ExchangeErrorProvider exchangeErrorProvider in this.enabledBindingSources.Values)
					{
						exchangeErrorProvider.ContainerControl = value;
					}
				}
			}
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0002E904 File Offset: 0x0002CB04
		private void ContainerControl_BindingContextChanged(object sender, EventArgs e)
		{
			this.ContainerBindingContext = this.ContainerControl.BindingContext;
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x0002E917 File Offset: 0x0002CB17
		// (set) Token: 0x06000CCE RID: 3278 RVA: 0x0002E920 File Offset: 0x0002CB20
		private BindingContext ContainerBindingContext
		{
			get
			{
				return this.containerBindingContext;
			}
			set
			{
				if (this.ContainerBindingContext != value)
				{
					if (this.ContainerBindingContext != null)
					{
						foreach (BindingSource bindingSource in this.enabledBindingSources.Keys)
						{
							this.UnbindFromBindingSource(bindingSource);
						}
					}
					this.containerBindingContext = value;
					if (this.ContainerBindingContext != null)
					{
						foreach (BindingSource bindingSource2 in this.enabledBindingSources.Keys)
						{
							this.BindToBindingSource(bindingSource2);
						}
					}
				}
			}
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0002E9E4 File Offset: 0x0002CBE4
		private void BindToBindingSource(BindingSource bindingSource)
		{
			BindingManagerBase bindingManagerBase = this.ContainerBindingContext[bindingSource];
			bindingManagerBase.CurrentChanged += this.bindingManager_CurrentChanged;
			bindingManagerBase.BindingComplete += this.BindingCompleted;
			bindingManagerBase.Bindings.CollectionChanged += this.Bindings_CollectionChanged;
			this.removedBindings[bindingSource] = new Dictionary<Control, List<Binding>>();
			bindingSource.ListChanged += this.bindingSource_ListChanged;
			foreach (object obj in bindingManagerBase.Bindings)
			{
				Binding element = (Binding)obj;
				this.Bindings_CollectionChanged(bindingManagerBase.Bindings, new CollectionChangeEventArgs(CollectionChangeAction.Add, element));
			}
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0002EAB4 File Offset: 0x0002CCB4
		private void UnbindFromBindingSource(BindingSource bindingSource)
		{
			BindingManagerBase bindingManagerBase = this.ContainerBindingContext[bindingSource];
			bindingManagerBase.CurrentChanged -= this.bindingManager_CurrentChanged;
			bindingManagerBase.BindingComplete -= this.BindingCompleted;
			bindingManagerBase.Bindings.CollectionChanged -= this.Bindings_CollectionChanged;
			this.removedBindings.Remove(bindingSource);
			bindingSource.ListChanged -= this.bindingSource_ListChanged;
			foreach (object obj in bindingManagerBase.Bindings)
			{
				Binding element = (Binding)obj;
				this.Bindings_CollectionChanged(bindingManagerBase.Bindings, new CollectionChangeEventArgs(CollectionChangeAction.Remove, element));
			}
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0002EB80 File Offset: 0x0002CD80
		private void bindingSource_ListChanged(object sender, ListChangedEventArgs e)
		{
			BindingSource bindingSource = (BindingSource)sender;
			IVersionable versionable = bindingSource.DataSource as IVersionable;
			if (versionable != null)
			{
				if (!this.datasourceVersions.ContainsKey(bindingSource))
				{
					this.datasourceVersions[bindingSource] = versionable.ExchangeVersion;
					this.DisableControlsByVersion(bindingSource, versionable);
					return;
				}
				ExchangeObjectVersion exchangeObjectVersion = this.datasourceVersions[bindingSource];
				if (!exchangeObjectVersion.IsSameVersion(versionable.ExchangeVersion))
				{
					this.datasourceVersions[bindingSource] = versionable.ExchangeVersion;
					this.EnableControlsByVersion(bindingSource, versionable);
					return;
				}
			}
			else
			{
				DataTable dataTable = bindingSource.DataSource as DataTable;
				if (dataTable != null)
				{
					DataObjectStore dataObjectStore = dataTable.ExtendedProperties["DataSourceStore"] as DataObjectStore;
					if (dataObjectStore != null)
					{
						foreach (string text in dataObjectStore.GetKeys())
						{
							IVersionable versionable2 = dataObjectStore.GetDataObject(text) as IVersionable;
							if (versionable2 != null)
							{
								if (this.dataSourceInTableVersions.ContainsKey(text))
								{
									ExchangeObjectVersion exchangeObjectVersion2 = this.dataSourceInTableVersions[text];
									if (!exchangeObjectVersion2.IsSameVersion(versionable2.ExchangeVersion))
									{
										this.dataSourceInTableVersions[text] = versionable2.ExchangeVersion;
										this.EnableControlsByVersion(bindingSource, versionable2);
									}
								}
								else
								{
									this.dataSourceInTableVersions[text] = versionable2.ExchangeVersion;
									this.DisableControlsByVersion(bindingSource, versionable2);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0002ECFC File Offset: 0x0002CEFC
		private void DisableControlsByVersion(BindingSource bindingSource, IVersionable dataSource)
		{
			BindingManagerBase bindingManagerBase = this.ContainerBindingContext[bindingSource];
			for (int i = bindingManagerBase.Bindings.Count - 1; i >= 0; i--)
			{
				Binding binding = bindingManagerBase.Bindings[i];
				ExchangeObjectVersion propertyDefinitionVersion = PropertyConstraintProvider.GetPropertyDefinitionVersion(dataSource, binding.BindingMemberInfo.BindingMember);
				if (dataSource.ExchangeVersion.IsOlderThan(propertyDefinitionVersion))
				{
					if (!this.removedBindings[bindingSource].ContainsKey(binding.Control))
					{
						this.removedBindings[bindingSource][binding.Control] = new List<Binding>();
					}
					this.removedBindings[bindingSource][binding.Control].Add(binding);
					ISpecifyPropertyState specifyPropertyState = binding.Control as ISpecifyPropertyState;
					if (specifyPropertyState != null)
					{
						specifyPropertyState.SetPropertyState(binding.PropertyName, PropertyState.UnsupportedVersion, Strings.FeatureVersionMismatchDescription(propertyDefinitionVersion.ExchangeBuild));
					}
					else
					{
						binding.Control.Enabled = false;
					}
					binding.Control.DataBindings.Remove(binding);
				}
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0002EE08 File Offset: 0x0002D008
		private void EnableControlsByVersion(BindingSource bindingSource, IVersionable dataSource)
		{
			List<Control> list = new List<Control>();
			foreach (Control item in this.removedBindings[bindingSource].Keys)
			{
				list.Add(item);
			}
			foreach (Control control in list)
			{
				List<Binding> list2 = new List<Binding>(this.removedBindings[bindingSource][control]);
				foreach (Binding binding in list2)
				{
					ExchangeObjectVersion propertyDefinitionVersion = PropertyConstraintProvider.GetPropertyDefinitionVersion(dataSource, binding.BindingMemberInfo.BindingMember);
					if (!dataSource.ExchangeVersion.IsOlderThan(propertyDefinitionVersion))
					{
						control.DataBindings.Add(binding);
						this.removedBindings[bindingSource][control].Remove(binding);
						if (this.removedBindings[bindingSource][control].Count == 0)
						{
							ISpecifyPropertyState specifyPropertyState = control as ISpecifyPropertyState;
							if (specifyPropertyState != null)
							{
								specifyPropertyState.SetPropertyState(binding.PropertyName, PropertyState.Normal, string.Empty);
							}
							else
							{
								control.Enabled = true;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0002EF90 File Offset: 0x0002D190
		private void Bindings_CollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			if (CollectionChangeAction.Refresh != e.Action)
			{
				Binding binding = (Binding)e.Element;
				if (binding.BindableComponent != null)
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(binding.BindableComponent)[binding.PropertyName];
					EventHandler handler = new EventHandler(this.RaiseDataBoundPropertyChanged);
					switch (e.Action)
					{
					case CollectionChangeAction.Add:
						binding.FormattingEnabled = true;
						propertyDescriptor.AddValueChanged(binding.BindableComponent, handler);
						this.AddRemoveEnabledChangedHandler(binding, true);
						if (binding.Control is ExchangeUserControl)
						{
							this.SetUIValidationEnabled(binding.Control as ExchangeUserControl, true);
							return;
						}
						break;
					case CollectionChangeAction.Remove:
					{
						propertyDescriptor.RemoveValueChanged(binding.BindableComponent, handler);
						this.AddRemoveEnabledChangedHandler(binding, false);
						ExchangeUserControl exchangeUserControl = binding.Control as ExchangeUserControl;
						if (exchangeUserControl != null && exchangeUserControl.DataBindings.Count == 1)
						{
							this.SetUIValidationEnabled(exchangeUserControl, false);
						}
						break;
					}
					default:
						return;
					}
				}
			}
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0002F074 File Offset: 0x0002D274
		private void AddRemoveEnabledChangedHandler(Binding newBinding, bool add)
		{
			if (newBinding.Control != null)
			{
				newBinding.Control.EnabledChanged -= this.Control_EnabledChanged;
				if (add)
				{
					newBinding.Control.EnabledChanged += this.Control_EnabledChanged;
				}
			}
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0002F0B0 File Offset: 0x0002D2B0
		private void Control_EnabledChanged(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			if (!control.Enabled)
			{
				foreach (object obj in control.DataBindings)
				{
					Binding binding = (Binding)obj;
					BindingSource bindingSource = binding.DataSource as BindingSource;
					if (bindingSource != null)
					{
						ExchangeErrorProvider errorProvider = this.GetErrorProvider(bindingSource);
						if (errorProvider != null)
						{
							Control errorProviderAnchor = InputValidationProvider.GetErrorProviderAnchor(control);
							errorProvider.SetError(errorProviderAnchor, string.Empty);
						}
					}
				}
			}
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0002F148 File Offset: 0x0002D348
		private void UpdateUserControlUIErrorProvider(object sender, UIValidationEventArgs e)
		{
			this.SetUserControlErrorProvider(sender, e.Errors);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0002F158 File Offset: 0x0002D358
		private void SetUserControlErrorProvider(object sender, ICollection<UIValidationError> errors)
		{
			ExchangeUserControl exchangeUserControl = (ExchangeUserControl)sender;
			ICollection<UIValidationError> collection = exchangeUserControl.ValidationErrors ?? ((ICollection<UIValidationError>)UIValidationError.None);
			foreach (UIValidationError uivalidationError in collection)
			{
				bool flag = true;
				foreach (UIValidationError uivalidationError2 in errors)
				{
					if (uivalidationError.ErrorProviderAnchor == uivalidationError2.ErrorProviderAnchor)
					{
						this.validatingControls[exchangeUserControl].SetError(uivalidationError2.ErrorProviderAnchor, uivalidationError2.Description);
						flag = false;
						break;
					}
				}
				if (flag)
				{
					this.validatingControls[exchangeUserControl].SetError(uivalidationError.ErrorProviderAnchor, string.Empty);
				}
			}
			foreach (UIValidationError uivalidationError3 in errors)
			{
				bool flag2 = true;
				foreach (UIValidationError uivalidationError4 in collection)
				{
					if (uivalidationError4.ErrorProviderAnchor == uivalidationError3.ErrorProviderAnchor)
					{
						flag2 = false;
						break;
					}
				}
				if (flag2)
				{
					this.validatingControls[exchangeUserControl].SetError(uivalidationError3.ErrorProviderAnchor, uivalidationError3.Description);
				}
			}
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0002F2FC File Offset: 0x0002D4FC
		private void BindingCompleted(object sender, BindingCompleteEventArgs e)
		{
			Control errorProviderAnchor = InputValidationProvider.GetErrorProviderAnchor(e.Binding.Control);
			if (e.BindingCompleteState != BindingCompleteState.Success)
			{
				if (e.BindingCompleteContext == BindingCompleteContext.DataSourceUpdate)
				{
					StrongTypeException ex = e.Exception as StrongTypeException;
					if ((ex == null || ex.IsTargetProperty) && e.Binding.Control.Enabled)
					{
						this.GetErrorProvider((BindingSource)e.Binding.DataSource).SetError(errorProviderAnchor, e.ErrorText);
						this.AddErrorBindingForControl(e.Binding);
					}
					e.Binding.ControlUpdateMode = ControlUpdateMode.Never;
				}
			}
			else
			{
				e.Binding.ControlUpdateMode = ControlUpdateMode.OnPropertyChanged;
				if (this.RemoveSuccessfulBindingForControl(e.Binding))
				{
					this.GetErrorProvider((BindingSource)e.Binding.DataSource).SetError(errorProviderAnchor, string.Empty);
				}
			}
			e.Cancel = false;
			this.SetModifiedBindingMembers(e);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0002F3DC File Offset: 0x0002D5DC
		private static Control GetErrorProviderAnchor(Control control)
		{
			if (!(control is ExchangeUserControl))
			{
				return control;
			}
			return ((ExchangeUserControl)control).ErrorProviderAnchor;
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0002F3F3 File Offset: 0x0002D5F3
		// (set) Token: 0x06000CDC RID: 3292 RVA: 0x0002F3FB File Offset: 0x0002D5FB
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool SuspendRecordingModifiedBindingMembers { get; set; }

		// Token: 0x06000CDD RID: 3293 RVA: 0x0002F404 File Offset: 0x0002D604
		private void SetModifiedBindingMembers(BindingCompleteEventArgs e)
		{
			if (!this.SuspendRecordingModifiedBindingMembers && e.BindingCompleteContext.Equals(BindingCompleteContext.DataSourceUpdate))
			{
				object dataSource = e.Binding.DataSource;
				string bindingField = e.Binding.BindingMemberInfo.BindingField;
				object key;
				if (dataSource is BindingSource)
				{
					key = (dataSource as BindingSource).DataSource;
				}
				else
				{
					key = dataSource;
				}
				if (this.ModifiedBindingMembers.ContainsKey(key))
				{
					if (!this.ModifiedBindingMembers[key].Contains(bindingField))
					{
						this.ModifiedBindingMembers[key].Add(bindingField);
						return;
					}
				}
				else
				{
					List<string> list = new List<string>();
					list.Add(bindingField);
					this.ModifiedBindingMembers.Add(key, list);
				}
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0002F4BF File Offset: 0x0002D6BF
		internal Dictionary<object, List<string>> ModifiedBindingMembers
		{
			get
			{
				return this.modifiedBindingMembers;
			}
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0002F4C8 File Offset: 0x0002D6C8
		private void AddErrorBindingForControl(Binding errorBinding)
		{
			Control control = errorBinding.Control;
			if (!this.bindingErrorControls.ContainsKey(control))
			{
				this.bindingErrorControls[control] = new List<Binding>();
			}
			if (!this.bindingErrorControls[control].Contains(errorBinding))
			{
				this.bindingErrorControls[control].Add(errorBinding);
			}
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0002F524 File Offset: 0x0002D724
		private bool RemoveSuccessfulBindingForControl(Binding successfulBinding)
		{
			Control control = successfulBinding.Control;
			if (this.bindingErrorControls.ContainsKey(control) && this.bindingErrorControls[control].Contains(successfulBinding))
			{
				this.bindingErrorControls[control].Remove(successfulBinding);
			}
			return !this.bindingErrorControls.ContainsKey(control) || this.bindingErrorControls[control].Count == 0;
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0002F594 File Offset: 0x0002D794
		private void bindingManager_CurrentChanged(object sender, EventArgs e)
		{
			BindingManagerBase bindingManagerBase = (BindingManagerBase)sender;
			foreach (object obj in bindingManagerBase.Bindings)
			{
				Binding binding = (Binding)obj;
				binding.ControlUpdateMode = ControlUpdateMode.OnPropertyChanged;
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0002F5F4 File Offset: 0x0002D7F4
		private void RaiseDataBoundPropertyChanged(object sender, EventArgs e)
		{
			Control control = sender as Control;
			if (control != null)
			{
				foreach (ExchangeErrorProvider exchangeErrorProvider in this.enabledBindingSources.Values)
				{
					Control errorProviderAnchor = InputValidationProvider.GetErrorProviderAnchor(control);
					exchangeErrorProvider.SetError(errorProviderAnchor, "");
				}
			}
			EventHandler eventHandler = (EventHandler)base.Events[InputValidationProvider.EventDataBoundPropertyChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06000CE3 RID: 3299 RVA: 0x0002F684 File Offset: 0x0002D884
		// (remove) Token: 0x06000CE4 RID: 3300 RVA: 0x0002F697 File Offset: 0x0002D897
		public event EventHandler DataBoundPropertyChanged
		{
			add
			{
				SynchronizedDelegate.Combine(base.Events, InputValidationProvider.EventDataBoundPropertyChanged, value);
			}
			remove
			{
				SynchronizedDelegate.Remove(base.Events, InputValidationProvider.EventDataBoundPropertyChanged, value);
			}
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0002F6AC File Offset: 0x0002D8AC
		public string GetErrorProviderMessages(Control topControl, ref Control firstErrorControl)
		{
			if (topControl == null)
			{
				throw new ArgumentNullException("topControl");
			}
			StringBuilder stringBuilder = new StringBuilder();
			List<Control> list = new List<Control>();
			List<ExchangeErrorProvider> list2 = new List<ExchangeErrorProvider>(this.validatingControls.Values);
			list2.AddRange(this.enabledBindingSources.Values);
			ChildrenControlCollector.GetAllControlsInActualTabOrder(topControl, list, true);
			foreach (ExchangeErrorProvider exchangeErrorProvider in list2)
			{
				foreach (Control control in list)
				{
					string error = exchangeErrorProvider.GetError(control);
					if (!string.IsNullOrEmpty(error))
					{
						stringBuilder.AppendLine(error);
						if (firstErrorControl == null)
						{
							firstErrorControl = control;
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002F79C File Offset: 0x0002D99C
		public void SetUIValidationEnabled(ExchangeUserControl userControl, bool enabled)
		{
			if (enabled)
			{
				if (!this.validatingControls.ContainsKey(userControl))
				{
					ExchangeErrorProvider exchangeErrorProvider = new ExchangeErrorProvider(this.components);
					((ISupportInitialize)exchangeErrorProvider).BeginInit();
					exchangeErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
					exchangeErrorProvider.ContainerControl = this.ContainerControl;
					((ISupportInitialize)exchangeErrorProvider).EndInit();
					this.validatingControls.Add(userControl, exchangeErrorProvider);
					userControl.ValidationErrorsChanging += this.UpdateUserControlUIErrorProvider;
					return;
				}
			}
			else if (this.validatingControls.ContainsKey(userControl))
			{
				this.validatingControls.Remove(userControl);
				userControl.ValidationErrorsChanging -= this.UpdateUserControlUIErrorProvider;
			}
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0002F834 File Offset: 0x0002DA34
		public ExchangeErrorProvider GetErrorProvider(BindingSource bindingSource)
		{
			ExchangeErrorProvider result = null;
			this.enabledBindingSources.TryGetValue(bindingSource, out result);
			return result;
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0002F854 File Offset: 0x0002DA54
		public void WriteBindings()
		{
			foreach (BindingSource dataSource in this.enabledBindingSources.Keys)
			{
				BindingManagerBase bindingManagerBase = this.ContainerBindingContext[dataSource];
				foreach (object obj in bindingManagerBase.Bindings)
				{
					Binding binding = (Binding)obj;
					if (binding.DataSourceUpdateMode != DataSourceUpdateMode.Never && binding.Control.Enabled && binding.Control.Visible)
					{
						binding.WriteValue();
					}
				}
			}
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0002F928 File Offset: 0x0002DB28
		private Binding GetBindingGivenPropertyName(string propertyName)
		{
			Binding binding = null;
			foreach (BindingSource dataSource in this.enabledBindingSources.Keys)
			{
				BindingManagerBase bindingManagerBase = this.ContainerBindingContext[dataSource];
				foreach (object obj in bindingManagerBase.Bindings)
				{
					Binding binding2 = (Binding)obj;
					if (binding2.DataSourceUpdateMode != DataSourceUpdateMode.Never && binding2.Control.Enabled && binding2.Control.Visible && binding2.BindingMemberInfo.BindingMember.CompareTo(propertyName) == 0)
					{
						binding = binding2;
						break;
					}
				}
				if (binding != null)
				{
					break;
				}
			}
			return binding;
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0002FA1C File Offset: 0x0002DC1C
		public bool IsBoundToProperty(string propertyName)
		{
			return null != this.GetBindingGivenPropertyName(propertyName);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0002FA2C File Offset: 0x0002DC2C
		public bool UpdateErrorProviderTextForProperty(string errorMessage, string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ArgumentNullException("propertyName");
			}
			if (string.IsNullOrEmpty(errorMessage))
			{
				throw new ArgumentNullException("errorMessage");
			}
			bool result = false;
			Binding bindingGivenPropertyName = this.GetBindingGivenPropertyName(propertyName);
			if (bindingGivenPropertyName != null)
			{
				ExchangeErrorProvider exchangeErrorProvider = this.enabledBindingSources[(BindingSource)bindingGivenPropertyName.DataSource];
				StringBuilder stringBuilder = new StringBuilder(exchangeErrorProvider.GetError(bindingGivenPropertyName.Control));
				if (stringBuilder.Length > 0)
				{
					stringBuilder.AppendLine();
				}
				stringBuilder.Append(errorMessage);
				Control errorProviderAnchor = InputValidationProvider.GetErrorProviderAnchor(bindingGivenPropertyName.Control);
				exchangeErrorProvider.SetError(errorProviderAnchor, stringBuilder.ToString());
				result = true;
			}
			return result;
		}

		// Token: 0x04000520 RID: 1312
		private static readonly object EventDataBoundPropertyChanged = new object();

		// Token: 0x04000521 RID: 1313
		private IContainer components;

		// Token: 0x04000522 RID: 1314
		private ContainerControl containerControl;

		// Token: 0x04000523 RID: 1315
		private BindingContext containerBindingContext;

		// Token: 0x04000524 RID: 1316
		private Dictionary<BindingSource, ExchangeErrorProvider> enabledBindingSources = new Dictionary<BindingSource, ExchangeErrorProvider>();

		// Token: 0x04000525 RID: 1317
		private Dictionary<BindingSource, ExchangeObjectVersion> datasourceVersions = new Dictionary<BindingSource, ExchangeObjectVersion>();

		// Token: 0x04000526 RID: 1318
		private Dictionary<string, ExchangeObjectVersion> dataSourceInTableVersions = new Dictionary<string, ExchangeObjectVersion>();

		// Token: 0x04000527 RID: 1319
		private Dictionary<BindingSource, Dictionary<Control, List<Binding>>> removedBindings = new Dictionary<BindingSource, Dictionary<Control, List<Binding>>>();

		// Token: 0x04000528 RID: 1320
		private Dictionary<ExchangeUserControl, ExchangeErrorProvider> validatingControls = new Dictionary<ExchangeUserControl, ExchangeErrorProvider>();

		// Token: 0x04000529 RID: 1321
		private Dictionary<Control, List<Binding>> bindingErrorControls = new Dictionary<Control, List<Binding>>();

		// Token: 0x0400052A RID: 1322
		private Dictionary<object, List<string>> modifiedBindingMembers = new Dictionary<object, List<string>>();
	}
}
