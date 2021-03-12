using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200003F RID: 63
	internal class FilterNode : Component, ICustomTypeDescriptor, IPropertyConstraintProvider
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000903E File Offset: 0x0000723E
		// (set) Token: 0x0600025C RID: 604 RVA: 0x00009046 File Offset: 0x00007246
		internal string ValueParsingError
		{
			get
			{
				return this.valueParsingError;
			}
			set
			{
				this.valueParsingError = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00009057 File Offset: 0x00007257
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DisplayName
		{
			get
			{
				if (this.FilterablePropertyDescription != null)
				{
					return this.FilterablePropertyDescription.DisplayName;
				}
				return string.Empty;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00009072 File Offset: 0x00007272
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		public ProviderPropertyDefinition PropertyDefinition
		{
			get
			{
				if (this.FilterablePropertyDescription != null)
				{
					return this.FilterablePropertyDescription.PropertyDefinition;
				}
				return null;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00009089 File Offset: 0x00007289
		// (set) Token: 0x06000261 RID: 609 RVA: 0x00009091 File Offset: 0x00007291
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public FilterablePropertyDescription FilterablePropertyDescription
		{
			get
			{
				return this.propDesc;
			}
			set
			{
				if (this.propDesc != value)
				{
					this.propDesc = value;
					this.OnFilterablePropertyDescriptionChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000090B0 File Offset: 0x000072B0
		protected virtual void OnFilterablePropertyDescriptionChanged(EventArgs e)
		{
			this.ResetValue();
			EventHandler eventHandler = (EventHandler)base.Events[FilterNode.EventFilterablePropertyDescriptionChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000263 RID: 611 RVA: 0x000090E4 File Offset: 0x000072E4
		// (remove) Token: 0x06000264 RID: 612 RVA: 0x000090F7 File Offset: 0x000072F7
		public event EventHandler FilterablePropertyDescriptionChanged
		{
			add
			{
				base.Events.AddHandler(FilterNode.EventFilterablePropertyDescriptionChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(FilterNode.EventFilterablePropertyDescriptionChanged, value);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000910A File Offset: 0x0000730A
		// (set) Token: 0x06000266 RID: 614 RVA: 0x00009112 File Offset: 0x00007312
		[DefaultValue(PropertyFilterOperator.Equal)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PropertyFilterOperator Operator
		{
			get
			{
				return this.op;
			}
			set
			{
				if (this.op != value)
				{
					this.op = value;
					this.OnOperatorChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00009130 File Offset: 0x00007330
		protected virtual void OnOperatorChanged(EventArgs e)
		{
			this.ResetValue();
			EventHandler eventHandler = (EventHandler)base.Events[FilterNode.EventOperatorChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000268 RID: 616 RVA: 0x00009164 File Offset: 0x00007364
		// (remove) Token: 0x06000269 RID: 617 RVA: 0x00009177 File Offset: 0x00007377
		public event EventHandler OperatorChanged
		{
			add
			{
				base.Events.AddHandler(FilterNode.EventOperatorChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(FilterNode.EventOperatorChanged, value);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000918A File Offset: 0x0000738A
		// (set) Token: 0x0600026B RID: 619 RVA: 0x00009192 File Offset: 0x00007392
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (!object.Equals(this.value, value))
				{
					this.value = value;
					this.OnValueChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000091B4 File Offset: 0x000073B4
		protected virtual void OnValueChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[FilterNode.EventValueChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x0600026D RID: 621 RVA: 0x000091E2 File Offset: 0x000073E2
		// (remove) Token: 0x0600026E RID: 622 RVA: 0x000091F5 File Offset: 0x000073F5
		public event EventHandler ValueChanged
		{
			add
			{
				base.Events.AddHandler(FilterNode.EventValueChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(FilterNode.EventValueChanged, value);
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00009208 File Offset: 0x00007408
		private void ResetValue()
		{
			if (this.PropertyDefinition != null && (typeof(DateTime).IsAssignableFrom(this.PropertyDefinition.Type) || typeof(DateTime?).IsAssignableFrom(this.PropertyDefinition.Type)))
			{
				this.Value = (DateTime)ExDateTime.Now;
				return;
			}
			this.Value = null;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00009274 File Offset: 0x00007474
		public bool IsComplete
		{
			get
			{
				bool result;
				if (this.Operator == PropertyFilterOperator.Present || this.Operator == PropertyFilterOperator.NotPresent)
				{
					result = (null != this.PropertyDefinition);
				}
				else
				{
					string text = this.Value as string;
					if (text == null)
					{
						result = (this.PropertyDefinition != null && null != this.Value);
					}
					else
					{
						result = (this.PropertyDefinition != null && !string.IsNullOrEmpty(text));
					}
				}
				return result;
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000092E8 File Offset: 0x000074E8
		public string Validate()
		{
			if (this.ValueParsingError != null)
			{
				return string.Format("{0}: '{1}'", this.DisplayName, this.ValueParsingError);
			}
			string text = null;
			if (this.op == PropertyFilterOperator.Equal || this.op == PropertyFilterOperator.NotEqual || this.op == PropertyFilterOperator.GreaterThan || this.op == PropertyFilterOperator.LessThan || this.op == PropertyFilterOperator.LessThanOrEqual || this.op == PropertyFilterOperator.GreaterThanOrEqual)
			{
				Type type = this.Value.GetType();
				if (type != this.PropertyDefinition.Type)
				{
					try
					{
						string valueToConvert;
						if (this.Value is DateTime)
						{
							valueToConvert = ((DateTime)this.Value).ToString("s");
						}
						else
						{
							valueToConvert = this.Value.ToString();
						}
						MonadFilter.ConvertValueFromString(valueToConvert, this.PropertyDefinition.Type);
					}
					catch (PSInvalidCastException ex)
					{
						Exception ex2 = ex;
						while (ex2.InnerException != null && !(ex2.InnerException is FormatException))
						{
							ex2 = ex2.InnerException;
						}
						text = string.Format("{0}: '{1}'", this.DisplayName, (ex2.InnerException != null) ? ex2.InnerException.Message : ex.Message);
						if (this.PropertyDefinition.Type == typeof(Version))
						{
							text = string.Format("{0}. {1}", text, Strings.ValidVersionExample);
						}
					}
				}
			}
			return text;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00009458 File Offset: 0x00007658
		public QueryFilter QueryFilter
		{
			get
			{
				QueryFilter queryFilter = null;
				if (typeof(Enum).IsAssignableFrom(this.PropertyDefinition.Type))
				{
					queryFilter = this.GetQueryFilterForEnum();
				}
				else if (typeof(bool).IsAssignableFrom(this.PropertyDefinition.Type))
				{
					queryFilter = new ComparisonFilter((ComparisonOperator)this.Operator, this.PropertyDefinition, true);
					if (this.Value.Equals(false))
					{
						queryFilter = new NotFilter(queryFilter);
					}
				}
				else
				{
					switch (this.Operator)
					{
					case PropertyFilterOperator.Equal:
					case PropertyFilterOperator.NotEqual:
					case PropertyFilterOperator.LessThan:
					case PropertyFilterOperator.LessThanOrEqual:
					case PropertyFilterOperator.GreaterThan:
					case PropertyFilterOperator.GreaterThanOrEqual:
						if (this.Value is ADObjectId)
						{
							queryFilter = new ComparisonFilter((ComparisonOperator)this.op, this.PropertyDefinition, ((ADObjectId)this.Value).ToDNString());
						}
						else if (this.Value is DateTime)
						{
							queryFilter = new ComparisonFilter((ComparisonOperator)this.op, this.PropertyDefinition, ((DateTime)this.Value).ToString("s"));
						}
						else
						{
							queryFilter = new ComparisonFilter((ComparisonOperator)this.op, this.PropertyDefinition, this.Value);
						}
						break;
					case PropertyFilterOperator.StartsWith:
						queryFilter = new TextFilter(this.PropertyDefinition, this.Value.ToString(), MatchOptions.Prefix, MatchFlags.IgnoreCase);
						break;
					case PropertyFilterOperator.EndsWith:
						queryFilter = new TextFilter(this.PropertyDefinition, this.Value.ToString(), MatchOptions.Suffix, MatchFlags.IgnoreCase);
						break;
					case PropertyFilterOperator.Contains:
					case PropertyFilterOperator.NotContains:
						queryFilter = this.GetQueryFilterForLike(this.Operator);
						break;
					case PropertyFilterOperator.Present:
						queryFilter = new ExistsFilter(this.PropertyDefinition);
						break;
					case PropertyFilterOperator.NotPresent:
						queryFilter = new NotFilter(new ExistsFilter(this.PropertyDefinition));
						break;
					}
				}
				return queryFilter;
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00009614 File Offset: 0x00007814
		private QueryFilter GetQueryFilterForEnum()
		{
			QueryFilter result = null;
			if (this.PropertyDefinition.Type.GetCustomAttributes(typeof(FlagsAttribute), false).Length == 0)
			{
				Type underlyingType = Enum.GetUnderlyingType(this.PropertyDefinition.Type);
				result = new ComparisonFilter((ComparisonOperator)this.op, this.PropertyDefinition, Convert.ChangeType(this.Value, underlyingType));
			}
			else
			{
				switch (this.Operator)
				{
				case PropertyFilterOperator.Equal:
					result = this.GetQueryFilterForLike(PropertyFilterOperator.Contains);
					break;
				case PropertyFilterOperator.NotEqual:
					result = this.GetQueryFilterForLike(PropertyFilterOperator.NotContains);
					break;
				}
			}
			return result;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000096A0 File Offset: 0x000078A0
		private QueryFilter GetQueryFilterForLike(PropertyFilterOperator op)
		{
			QueryFilter result = null;
			switch (op)
			{
			case PropertyFilterOperator.Contains:
				result = new TextFilter(this.PropertyDefinition, this.Value.ToString(), MatchOptions.SubString, MatchFlags.IgnoreCase);
				break;
			case PropertyFilterOperator.NotContains:
				result = new NotFilter(new TextFilter(this.PropertyDefinition, this.Value.ToString(), MatchOptions.SubString, MatchFlags.IgnoreCase));
				break;
			}
			return result;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00009700 File Offset: 0x00007900
		public static List<FilterNode> GetNodesFromSerializedQueryFilter(byte[] serializedQueryFilter, IList<FilterablePropertyDescription> filterableProperties, ObjectSchema schema)
		{
			List<FilterNode> list = new List<FilterNode>();
			if (serializedQueryFilter != null)
			{
				Hashtable hashtable = new Hashtable(filterableProperties.Count);
				foreach (FilterablePropertyDescription filterablePropertyDescription in filterableProperties)
				{
					hashtable.Add(filterablePropertyDescription.PropertyDefinition.Name, filterablePropertyDescription);
				}
				FilterNode.GetNodesFromExpressionTree((QueryFilter)WinformsHelper.DeSerialize(serializedQueryFilter), hashtable, list);
			}
			return list;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000977C File Offset: 0x0000797C
		internal static List<FilterNode> GetNodesFromExpressionString(string expression, IList<FilterablePropertyDescription> filterableProperties, ObjectSchema schema)
		{
			return FilterNode.GetNodesFromSerializedQueryFilter(FilterNode.ConvertExpressionStringToByteArray(expression, schema), filterableProperties, schema);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000978C File Offset: 0x0000798C
		internal static byte[] ConvertExpressionStringToByteArray(string expression, ObjectSchema schema)
		{
			QueryParser.ConvertValueFromStringDelegate convertDelegate = new QueryParser.ConvertValueFromStringDelegate(MonadFilter.ConvertValueFromString);
			QueryParser.EvaluateVariableDelegate evalDelegate = new QueryParser.EvaluateVariableDelegate(FilterNode.VarConverter);
			QueryParser queryParser = new QueryParser(expression, schema, QueryParser.Capabilities.All, evalDelegate, convertDelegate);
			return WinformsHelper.Serialize(queryParser.ParseTree);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000097CD File Offset: 0x000079CD
		private static object VarConverter(string varName)
		{
			return null;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000097D0 File Offset: 0x000079D0
		private static void GetNodesFromExpressionTree(QueryFilter queryFilter, Hashtable allowedProperties, List<FilterNode> filterNodes)
		{
			if (queryFilter != null)
			{
				CompositeFilter compositeFilter = queryFilter as CompositeFilter;
				TextFilter textFilter = queryFilter as TextFilter;
				ComparisonFilter comparisonFilter = queryFilter as ComparisonFilter;
				ExistsFilter existsFilter = queryFilter as ExistsFilter;
				NotFilter notFilter = queryFilter as NotFilter;
				if (compositeFilter != null)
				{
					using (ReadOnlyCollection<QueryFilter>.Enumerator enumerator = compositeFilter.Filters.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							QueryFilter queryFilter2 = enumerator.Current;
							FilterNode.GetNodesFromExpressionTree(queryFilter2, allowedProperties, filterNodes);
						}
						return;
					}
				}
				if (notFilter != null)
				{
					textFilter = (notFilter.Filter as TextFilter);
					existsFilter = (notFilter.Filter as ExistsFilter);
					comparisonFilter = (notFilter.Filter as ComparisonFilter);
					if ((textFilter == null && existsFilter == null && comparisonFilter == null) || (comparisonFilter != null && !typeof(bool).IsAssignableFrom(comparisonFilter.Property.Type)))
					{
						throw new InvalidOperationException(Strings.InvalidNotFilter.ToString());
					}
				}
				if (textFilter != null)
				{
					FilterNode.GetNodeFromTextFilter(textFilter, allowedProperties, filterNodes, notFilter != null);
					return;
				}
				if (comparisonFilter != null)
				{
					FilterNode.GetNodeFromComparisonFilter(comparisonFilter, allowedProperties, filterNodes, notFilter != null);
					return;
				}
				if (existsFilter != null)
				{
					FilterNode.GetNodeFromExistsFilter(existsFilter, allowedProperties, filterNodes, null != notFilter);
					return;
				}
				throw new InvalidOperationException(Strings.UnsuportedFilterType(queryFilter.GetType()).ToString());
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00009920 File Offset: 0x00007B20
		private static void GetNodeFromExistsFilter(ExistsFilter existsFilter, Hashtable allowedProperties, List<FilterNode> filterNodes, bool isNotFilter)
		{
			if (allowedProperties.ContainsKey(existsFilter.Property.Name))
			{
				FilterNode filterNode = new FilterNode();
				filterNode.FilterablePropertyDescription = (FilterablePropertyDescription)allowedProperties[existsFilter.Property.Name];
				if (isNotFilter)
				{
					filterNode.Operator = PropertyFilterOperator.NotPresent;
				}
				else
				{
					filterNode.Operator = PropertyFilterOperator.Present;
				}
				filterNodes.Add(filterNode);
				return;
			}
			throw new InvalidOperationException(Strings.UnknownFilterableProperty(existsFilter.Property.Name).ToString());
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000099A8 File Offset: 0x00007BA8
		private static void GetNodeFromComparisonFilter(ComparisonFilter comparisonFilter, Hashtable allowedProperties, List<FilterNode> filterNodes, bool isNotFilter)
		{
			if (allowedProperties.ContainsKey(comparisonFilter.Property.Name))
			{
				FilterNode filterNode = new FilterNode();
				filterNode.FilterablePropertyDescription = (FilterablePropertyDescription)allowedProperties[comparisonFilter.Property.Name];
				filterNode.Operator = (PropertyFilterOperator)comparisonFilter.ComparisonOperator;
				if (typeof(Enum).IsAssignableFrom(comparisonFilter.Property.Type))
				{
					filterNode.Value = Enum.Parse(comparisonFilter.Property.Type, comparisonFilter.PropertyValue.ToString(), true);
				}
				else if (typeof(bool).IsAssignableFrom(comparisonFilter.Property.Type))
				{
					filterNode.Value = !isNotFilter;
				}
				else if (comparisonFilter.Property.Type == typeof(MultiValuedProperty<string>))
				{
					filterNode.Value = comparisonFilter.PropertyValue.ToUserFriendText(CultureInfo.CurrentUICulture.TextInfo.ListSeparator, (object input) => false);
				}
				else
				{
					filterNode.Value = MonadFilter.ConvertValueFromString(comparisonFilter.PropertyValue, comparisonFilter.Property.Type);
				}
				filterNodes.Add(filterNode);
				return;
			}
			throw new InvalidOperationException(Strings.UnknownFilterableProperty(comparisonFilter.Property.Name).ToString());
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00009B0C File Offset: 0x00007D0C
		private static void GetNodeFromTextFilter(TextFilter textFilter, Hashtable allowedProperties, List<FilterNode> filterNodes, bool isNotFilter)
		{
			if (allowedProperties.ContainsKey(textFilter.Property.Name))
			{
				FilterNode filterNode = new FilterNode();
				filterNode.FilterablePropertyDescription = (FilterablePropertyDescription)allowedProperties[textFilter.Property.Name];
				switch (textFilter.MatchOptions)
				{
				case MatchOptions.SubString:
					if (isNotFilter)
					{
						filterNode.Operator = PropertyFilterOperator.NotContains;
					}
					else
					{
						filterNode.Operator = PropertyFilterOperator.Contains;
					}
					break;
				case MatchOptions.Prefix:
					filterNode.Operator = PropertyFilterOperator.StartsWith;
					break;
				case MatchOptions.Suffix:
					filterNode.Operator = PropertyFilterOperator.EndsWith;
					break;
				default:
					throw new InvalidOperationException(Strings.UnsupportedTextFilter(textFilter.Property.Name, textFilter.MatchOptions.ToString(), textFilter.Text).ToString());
				}
				if (typeof(Enum).IsAssignableFrom(filterNode.PropertyDefinition.Type) && filterNode.PropertyDefinition.Type.GetCustomAttributes(typeof(FlagsAttribute), false).Length != 0)
				{
					if (isNotFilter)
					{
						filterNode.Operator = PropertyFilterOperator.NotEqual;
					}
					else
					{
						filterNode.Operator = PropertyFilterOperator.Equal;
					}
					filterNode.Value = Enum.Parse(filterNode.PropertyDefinition.Type, textFilter.Text, true);
				}
				else
				{
					filterNode.Value = textFilter.Text;
				}
				if (isNotFilter)
				{
					if (!typeof(Enum).IsAssignableFrom(filterNode.PropertyDefinition.Type) && filterNode.Operator != PropertyFilterOperator.NotContains)
					{
						throw new InvalidOperationException(Strings.InvalidTextFilterForNonEnums(filterNode.Operator.ToString()).ToString());
					}
					if (typeof(Enum).IsAssignableFrom(filterNode.PropertyDefinition.Type) && filterNode.Operator != PropertyFilterOperator.NotEqual)
					{
						throw new InvalidOperationException(Strings.InvalidTextFilterForEnums(filterNode.Operator.ToString()).ToString());
					}
				}
				filterNodes.Add(filterNode);
				return;
			}
			throw new InvalidOperationException(Strings.UnknownFilterableProperty(textFilter.Property.Name).ToString());
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00009D19 File Offset: 0x00007F19
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00009D22 File Offset: 0x00007F22
		string ICustomTypeDescriptor.GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00009D2B File Offset: 0x00007F2B
		string ICustomTypeDescriptor.GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00009D34 File Offset: 0x00007F34
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00009D3D File Offset: 0x00007F3D
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00009D46 File Offset: 0x00007F46
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00009D4F File Offset: 0x00007F4F
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00009D59 File Offset: 0x00007F59
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00009D62 File Offset: 0x00007F62
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00009D6C File Offset: 0x00007F6C
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00009D78 File Offset: 0x00007F78
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
			foreach (object obj in TypeDescriptor.GetProperties(this, attributes, true))
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Name == "Value")
				{
					propertyDescriptorCollection.Add(new FilterValuePropertyDescriptor(this, propertyDescriptor));
				}
				else
				{
					propertyDescriptorCollection.Add(propertyDescriptor);
				}
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00009E00 File Offset: 0x00008000
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00009E04 File Offset: 0x00008004
		public PropertyDefinitionConstraint[] GetPropertyDefinitionConstraints(string propertyName)
		{
			PropertyDefinitionConstraint[] array = new PropertyDefinitionConstraint[0];
			if (propertyName == "Value" && this.PropertyDefinition != null)
			{
				array = new PropertyDefinitionConstraint[this.PropertyDefinition.AllConstraints.Count];
				this.PropertyDefinition.AllConstraints.CopyTo(array, 0);
			}
			return array;
		}

		// Token: 0x040000A8 RID: 168
		private FilterablePropertyDescription propDesc;

		// Token: 0x040000A9 RID: 169
		private PropertyFilterOperator op;

		// Token: 0x040000AA RID: 170
		private object value;

		// Token: 0x040000AB RID: 171
		private string valueParsingError;

		// Token: 0x040000AC RID: 172
		private static readonly object EventFilterablePropertyDescriptionChanged = new object();

		// Token: 0x040000AD RID: 173
		private static readonly object EventOperatorChanged = new object();

		// Token: 0x040000AE RID: 174
		private static readonly object EventValueChanged = new object();
	}
}
