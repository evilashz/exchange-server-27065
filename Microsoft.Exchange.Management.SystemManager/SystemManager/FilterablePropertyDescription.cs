using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200003B RID: 59
	internal class FilterablePropertyDescription : IComparable<FilterablePropertyDescription>
	{
		// Token: 0x06000234 RID: 564 RVA: 0x00008CB4 File Offset: 0x00006EB4
		internal FilterablePropertyDescription(ProviderPropertyDefinition propdef, string displayName, PropertyFilterOperator[] operators)
		{
			this.propertyDefinition = propdef;
			this.displayName = displayName;
			this.supportedOperators = new EnumListSource<PropertyFilterOperator>(operators);
			this.SurfaceFilterablePropertyDescription = this;
			this.SurfaceFilterNodeSynchronizer = FilterablePropertyDescription.defaultFilterNodeSynchronizer;
			this.UnderlyingFilterablePropertyDescription = this;
			this.UnderlyingFilterNodeSynchronizer = FilterablePropertyDescription.defaultFilterNodeSynchronizer;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00008D10 File Offset: 0x00006F10
		public FilterablePropertyDescription(ProviderPropertyDefinition propdef, string displayName, PropertyFilterOperator[] operators, string pickerProfile, string objectPickerValueMember) : this(propdef, displayName, operators)
		{
			this.PickerProfileName = pickerProfile;
			this.ObjectPickerValueMember = objectPickerValueMember;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00008D2B File Offset: 0x00006F2B
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00008D33 File Offset: 0x00006F33
		public string PickerProfileName { get; internal set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00008D3C File Offset: 0x00006F3C
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00008D6A File Offset: 0x00006F6A
		public ObjectPicker ObjectPicker
		{
			get
			{
				if (this.objectPicker == null && !string.IsNullOrEmpty(this.PickerProfileName))
				{
					this.objectPicker = new AutomatedObjectPicker(this.PickerProfileName);
				}
				return this.objectPicker;
			}
			internal set
			{
				this.objectPicker = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00008D73 File Offset: 0x00006F73
		public ProviderPropertyDefinition PropertyDefinition
		{
			get
			{
				return this.propertyDefinition;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00008D7B File Offset: 0x00006F7B
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00008D83 File Offset: 0x00006F83
		public EnumListSource<PropertyFilterOperator> SupportedOperators
		{
			get
			{
				return this.supportedOperators;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00008D8B File Offset: 0x00006F8B
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00008D93 File Offset: 0x00006F93
		public Type ColumnType
		{
			get
			{
				return this.columnType;
			}
			set
			{
				this.columnType = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00008D9C File Offset: 0x00006F9C
		internal Type ValueType
		{
			get
			{
				return this.ColumnType ?? this.PropertyDefinition.Type;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00008DB4 File Offset: 0x00006FB4
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00008E06 File Offset: 0x00007006
		public ObjectListSource FilterableListSource
		{
			get
			{
				ObjectListSource objectListSource = this.filterableListSource;
				if (objectListSource == null)
				{
					Type valueType = this.ValueType;
					if (typeof(Enum).IsAssignableFrom(valueType))
					{
						objectListSource = new EnumListSource(valueType);
					}
					else if (typeof(bool).IsAssignableFrom(valueType))
					{
						objectListSource = new BoolListSource();
					}
				}
				return objectListSource;
			}
			set
			{
				this.filterableListSource = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00008E0F File Offset: 0x0000700F
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00008E17 File Offset: 0x00007017
		public DisplayFormatMode FormatMode
		{
			get
			{
				return this.formatMode;
			}
			set
			{
				this.formatMode = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00008E20 File Offset: 0x00007020
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00008E28 File Offset: 0x00007028
		public string ObjectPickerValueMember
		{
			get
			{
				return this.objectPickerValueMember;
			}
			set
			{
				this.objectPickerValueMember = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00008E31 File Offset: 0x00007031
		// (set) Token: 0x06000247 RID: 583 RVA: 0x00008E39 File Offset: 0x00007039
		public ADPropertyDefinition ObjectPickerValueMemberPropertyDefinition
		{
			get
			{
				return this.objectPickerValueMemberPropertyDefinition;
			}
			set
			{
				this.objectPickerValueMemberPropertyDefinition = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00008E42 File Offset: 0x00007042
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00008E4A File Offset: 0x0000704A
		public string ObjectPickerDisplayMember
		{
			get
			{
				return this.objectPickerDisplayMember;
			}
			set
			{
				this.objectPickerDisplayMember = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00008E53 File Offset: 0x00007053
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00008E5B File Offset: 0x0000705B
		public FilterablePropertyDescription SurfaceFilterablePropertyDescription
		{
			get
			{
				return this.surfaceFilterablePropertyDescription;
			}
			set
			{
				if (this.SurfaceFilterablePropertyDescription != value)
				{
					this.surfaceFilterablePropertyDescription = (value ?? this);
					this.SurfaceFilterablePropertyDescription.UnderlyingFilterablePropertyDescription = this;
				}
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00008E7E File Offset: 0x0000707E
		// (set) Token: 0x0600024D RID: 589 RVA: 0x00008E86 File Offset: 0x00007086
		public FilterablePropertyDescription UnderlyingFilterablePropertyDescription
		{
			get
			{
				return this.underlyingFilterablePropertyDescription;
			}
			set
			{
				if (this.UnderlyingFilterablePropertyDescription != value)
				{
					this.underlyingFilterablePropertyDescription = (value ?? this);
					this.UnderlyingFilterablePropertyDescription.SurfaceFilterablePropertyDescription = this;
				}
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00008EA9 File Offset: 0x000070A9
		// (set) Token: 0x0600024F RID: 591 RVA: 0x00008EB1 File Offset: 0x000070B1
		public FilterNodeSynchronizer SurfaceFilterNodeSynchronizer
		{
			get
			{
				return this.surfaceFilterNodeSynchronizer;
			}
			set
			{
				if (this.SurfaceFilterNodeSynchronizer != value)
				{
					this.surfaceFilterNodeSynchronizer = (value ?? FilterablePropertyDescription.defaultFilterNodeSynchronizer);
				}
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00008ECC File Offset: 0x000070CC
		// (set) Token: 0x06000251 RID: 593 RVA: 0x00008ED4 File Offset: 0x000070D4
		public FilterNodeSynchronizer UnderlyingFilterNodeSynchronizer
		{
			get
			{
				return this.underlyingFilterNodeSynchronizer;
			}
			set
			{
				if (this.UnderlyingFilterNodeSynchronizer != value)
				{
					this.underlyingFilterNodeSynchronizer = (value ?? FilterablePropertyDescription.defaultFilterNodeSynchronizer);
				}
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00008EEF File Offset: 0x000070EF
		public void SetFilterablePropertyValueEditor(PropertyFilterOperator filterOperator, FilterablePropertyValueEditor editorType)
		{
			if (this.editorTypePerOperator.ContainsKey(filterOperator))
			{
				this.editorTypePerOperator[filterOperator] = editorType;
				return;
			}
			this.editorTypePerOperator.Add(filterOperator, editorType);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00008F1C File Offset: 0x0000711C
		internal FilterablePropertyValueEditor GetPropertyFilterEditor(PropertyFilterOperator filterOperator)
		{
			FilterablePropertyValueEditor result = FilterablePropertyValueEditor.TextBox;
			if (this.editorTypePerOperator.ContainsKey(filterOperator))
			{
				result = this.editorTypePerOperator[filterOperator];
			}
			else
			{
				Type valueType = this.ValueType;
				if (filterOperator == PropertyFilterOperator.NotPresent || filterOperator == PropertyFilterOperator.Present)
				{
					result = FilterablePropertyValueEditor.DisabledTextBox;
				}
				else if (valueType == typeof(DateTime) || valueType == typeof(DateTime?))
				{
					result = FilterablePropertyValueEditor.DateTimePicker;
				}
				else if (filterOperator == PropertyFilterOperator.Equal || filterOperator == PropertyFilterOperator.NotEqual)
				{
					if (this.FilterableListSource != null)
					{
						result = FilterablePropertyValueEditor.ComboBox;
					}
					else if (!string.IsNullOrEmpty(this.PickerProfileName) || this.objectPicker != null)
					{
						result = FilterablePropertyValueEditor.PickerLauncherTextBox;
					}
				}
				else if ((filterOperator == PropertyFilterOperator.Contains || filterOperator == PropertyFilterOperator.NotContains) && this.PropertyDefinition.IsMultivalued && (!string.IsNullOrEmpty(this.PickerProfileName) || this.objectPicker != null))
				{
					result = FilterablePropertyValueEditor.PickerLauncherTextBox;
				}
			}
			return result;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00008FE5 File Offset: 0x000071E5
		public int CompareTo(FilterablePropertyDescription other)
		{
			return this.DisplayName.CompareTo(other.DisplayName);
		}

		// Token: 0x04000097 RID: 151
		private static FilterablePropertyDescription.DefaultFilterNodeSynchronizer defaultFilterNodeSynchronizer = new FilterablePropertyDescription.DefaultFilterNodeSynchronizer();

		// Token: 0x04000098 RID: 152
		private ObjectPicker objectPicker;

		// Token: 0x04000099 RID: 153
		private ProviderPropertyDefinition propertyDefinition;

		// Token: 0x0400009A RID: 154
		private string displayName;

		// Token: 0x0400009B RID: 155
		private EnumListSource<PropertyFilterOperator> supportedOperators;

		// Token: 0x0400009C RID: 156
		private Type columnType;

		// Token: 0x0400009D RID: 157
		private ObjectListSource filterableListSource;

		// Token: 0x0400009E RID: 158
		private DisplayFormatMode formatMode;

		// Token: 0x0400009F RID: 159
		private string objectPickerValueMember;

		// Token: 0x040000A0 RID: 160
		private ADPropertyDefinition objectPickerValueMemberPropertyDefinition;

		// Token: 0x040000A1 RID: 161
		private string objectPickerDisplayMember;

		// Token: 0x040000A2 RID: 162
		private FilterablePropertyDescription surfaceFilterablePropertyDescription;

		// Token: 0x040000A3 RID: 163
		public FilterablePropertyDescription underlyingFilterablePropertyDescription;

		// Token: 0x040000A4 RID: 164
		private FilterNodeSynchronizer surfaceFilterNodeSynchronizer;

		// Token: 0x040000A5 RID: 165
		private FilterNodeSynchronizer underlyingFilterNodeSynchronizer;

		// Token: 0x040000A6 RID: 166
		private Dictionary<PropertyFilterOperator, FilterablePropertyValueEditor> editorTypePerOperator = new Dictionary<PropertyFilterOperator, FilterablePropertyValueEditor>();

		// Token: 0x0200003D RID: 61
		private class DefaultFilterNodeSynchronizer : FilterNodeSynchronizer
		{
			// Token: 0x06000258 RID: 600 RVA: 0x0000900C File Offset: 0x0000720C
			public override void Synchronize(FilterNode sourceNode, FilterNode targetNode)
			{
				if (sourceNode != targetNode)
				{
					targetNode.FilterablePropertyDescription = sourceNode.FilterablePropertyDescription;
					targetNode.Operator = sourceNode.Operator;
					targetNode.Value = sourceNode.Value;
				}
			}
		}
	}
}
