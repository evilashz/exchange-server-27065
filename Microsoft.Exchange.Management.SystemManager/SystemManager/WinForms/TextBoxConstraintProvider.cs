using System;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200016D RID: 365
	public class TextBoxConstraintProvider
	{
		// Token: 0x06000EE8 RID: 3816 RVA: 0x000394A9 File Offset: 0x000376A9
		public TextBoxConstraintProvider(IFormatModeProvider owner, Control target) : this(owner, "Text", target)
		{
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x000394B8 File Offset: 0x000376B8
		public TextBoxConstraintProvider(IFormatModeProvider owner, string ownerBindingPropertyName, Control target)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			if (string.IsNullOrEmpty(ownerBindingPropertyName))
			{
				throw new ArgumentNullException("ownerBindingPropertyName");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			this.ownerControl = owner;
			this.ownerBindingPropertyName = ownerBindingPropertyName;
			this.targetTextBox = target;
			this.targetControlMaxLengthPropertyInfo = this.targetTextBox.GetType().GetProperty("MaxLength");
			this.targetControlMaskExpressionPropertyInfo = this.targetTextBox.GetType().GetProperty("MaskExpression");
			this.ownerControl.DataBindings.CollectionChanged += this.Owner_DataBindingsCollectionChanged;
			this.ownerControl.FormatModeChanged += this.Owner_FormatChanged;
			this.ownerControl.BindingContextChanged += this.Owner_BindingContextChanged;
			this.Owner_DataBindingsCollectionChanged(this, new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
			this.Owner_FormatChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x000395A9 File Offset: 0x000377A9
		private void Owner_BindingContextChanged(object sender, EventArgs e)
		{
			this.Owner_DataBindingsCollectionChanged(null, new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x000395BC File Offset: 0x000377BC
		private void Owner_FormatChanged(object sender, EventArgs e)
		{
			Binding binding = this.ownerControl.DataBindings[this.ownerBindingPropertyName];
			if (!this.IsFormatModeAndBindingCompatible(this.ownerControl.FormatMode, binding))
			{
				throw new InvalidOperationException();
			}
			switch (this.ownerControl.FormatMode)
			{
			case 0:
				this.converter = DisplayFormats.Default;
				return;
			case 1:
				this.converter = DisplayFormats.EnhancedTimeSpanAsDays;
				return;
			case 2:
				this.converter = DisplayFormats.EnhancedTimeSpanAsHours;
				return;
			case 3:
				this.converter = DisplayFormats.EnhancedTimeSpanAsMinutes;
				return;
			case 4:
				this.converter = DisplayFormats.EnhancedTimeSpanAsSeconds;
				return;
			case 5:
				this.converter = DisplayFormats.EnhancedTimeSpanAsDetailedFormat;
				return;
			case 6:
				this.converter = DisplayFormats.ByteQuantifiedSizeAsKb;
				return;
			case 7:
				this.converter = DisplayFormats.ByteQuantifiedSizeAsMb;
				return;
			case 8:
				this.converter = DisplayFormats.ByteQuantifiedSizeAsDetailedFormat;
				return;
			case 9:
				this.converter = DisplayFormats.BooleanAsStatus;
				return;
			case 10:
				this.converter = DisplayFormats.BooleanAsMountStatus;
				return;
			case 11:
				this.converter = DisplayFormats.BooleanAsYesNo;
				return;
			case 12:
				this.converter = DisplayFormats.AdObjectIdAsName;
				return;
			case 13:
				this.converter = DisplayFormats.NullableDateTimeAsLogTime;
				return;
			case 14:
				this.converter = DisplayFormats.IntegerAsPercentage;
				return;
			case 15:
				this.converter = DisplayFormats.SmtpDomainWithSubdomainsListAsString;
				return;
			default:
				throw new NotSupportedException(this.ownerControl.FormatMode.ToString());
			}
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0003972B File Offset: 0x0003792B
		private bool IsTextBoxParseFormatCompatible(Binding binding)
		{
			return binding.PropertyName == "Text";
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00039740 File Offset: 0x00037940
		private void Owner_DataBindingsCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			Binding binding = (Binding)e.Element;
			switch (e.Action)
			{
			case CollectionChangeAction.Add:
				if (binding.PropertyName == this.ownerBindingPropertyName)
				{
					this.SetConstraintsFromBinding(binding);
					if (this.IsTextBoxParseFormatCompatible(binding))
					{
						binding.Parse += this.TextBoxValue_Parse;
						binding.Format += this.TextBoxValue_Format;
					}
				}
				break;
			case CollectionChangeAction.Remove:
				if (binding.PropertyName == this.ownerBindingPropertyName)
				{
					this.ResetTextBoxMask();
					if (this.IsTextBoxParseFormatCompatible(binding))
					{
						binding.Parse -= this.TextBoxValue_Parse;
						binding.Format -= this.TextBoxValue_Format;
					}
				}
				break;
			case CollectionChangeAction.Refresh:
				this.ResetTextBoxMask();
				binding = this.ownerControl.DataBindings[this.ownerBindingPropertyName];
				if (binding != null)
				{
					if (this.IsTextBoxParseFormatCompatible(binding))
					{
						binding.Parse -= this.TextBoxValue_Parse;
						binding.Format -= this.TextBoxValue_Format;
					}
					this.SetConstraintsFromBinding(binding);
					if (this.IsTextBoxParseFormatCompatible(binding))
					{
						binding.Parse += this.TextBoxValue_Parse;
						binding.Format += this.TextBoxValue_Format;
					}
				}
				break;
			}
			this.SetConstaintsFromDataSource();
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0003989D File Offset: 0x00037A9D
		private void ResetTextBoxMask()
		{
			this.SetMaskExpression("");
			this.SetMaxLength(32767);
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x000398B5 File Offset: 0x00037AB5
		private bool IsMaxLengthEnable()
		{
			return this.targetControlMaxLengthPropertyInfo != null && this.targetControlMaxLengthPropertyInfo.CanRead && this.targetControlMaxLengthPropertyInfo.CanWrite;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x000398DF File Offset: 0x00037ADF
		protected void SetMaxLength(int value)
		{
			if (this.IsMaxLengthEnable())
			{
				this.targetControlMaxLengthPropertyInfo.SetValue(this.targetTextBox, value, null);
			}
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00039901 File Offset: 0x00037B01
		private bool IsMaskExpressionEnable()
		{
			return this.targetControlMaskExpressionPropertyInfo != null && this.targetControlMaskExpressionPropertyInfo.CanRead && this.targetControlMaskExpressionPropertyInfo.CanWrite;
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0003992B File Offset: 0x00037B2B
		private void SetMaskExpression(string value)
		{
			if (this.IsMaskExpressionEnable())
			{
				this.targetControlMaskExpressionPropertyInfo.SetValue(this.targetTextBox, value, null);
			}
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00039948 File Offset: 0x00037B48
		private string GetMaskExpression()
		{
			string result = null;
			if (this.targetControlMaskExpressionPropertyInfo != null && this.targetControlMaskExpressionPropertyInfo.CanRead)
			{
				result = (this.targetControlMaskExpressionPropertyInfo.GetValue(this.targetTextBox, null) as string);
			}
			return result;
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0003998C File Offset: 0x00037B8C
		private void SetConstaintsFromDataSource()
		{
			if (this.ownerControl.DataBindings[this.ownerBindingPropertyName] == null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.ownerControl)[this.ownerBindingPropertyName];
				if (propertyDescriptor != null)
				{
					object value = propertyDescriptor.GetValue(this.ownerControl);
					Type listItemType = ListBindingHelper.GetListItemType(value);
					if (null != listItemType)
					{
						this.SetConstraintsFromType(listItemType, new PropertyDefinitionConstraint[0]);
					}
				}
			}
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x000399F8 File Offset: 0x00037BF8
		private void SetConstraintsFromBinding(Binding binding)
		{
			if (binding.BindingManagerBase != null)
			{
				if (!this.IsFormatModeAndBindingCompatible(this.ownerControl.FormatMode, binding))
				{
					throw new InvalidOperationException();
				}
				PropertyDescriptorCollection itemProperties = binding.BindingManagerBase.GetItemProperties();
				PropertyDescriptor propertyDescriptor = itemProperties.Find(binding.BindingMemberInfo.BindingField, true);
				if (propertyDescriptor != null)
				{
					FilterValuePropertyDescriptor filterValuePropertyDescriptor = propertyDescriptor as FilterValuePropertyDescriptor;
					Type propertyType;
					if (filterValuePropertyDescriptor != null)
					{
						propertyType = filterValuePropertyDescriptor.ValuePropertyType;
					}
					else
					{
						propertyType = propertyDescriptor.PropertyType;
					}
					object obj = (binding.DataSource is BindingSource) ? ((BindingSource)binding.DataSource).DataSource : binding.DataSource;
					DataTable dataTable = obj as DataTable;
					if (dataTable != null)
					{
						DataObjectStore dataObjectStore = dataTable.ExtendedProperties["DataSourceStore"] as DataObjectStore;
						if (dataObjectStore != null)
						{
							DataColumn dataColumn = dataTable.Columns[binding.BindingMemberInfo.BindingField];
							ColumnProfile columnProfile = dataColumn.ExtendedProperties["ColumnProfile"] as ColumnProfile;
							if (!string.IsNullOrEmpty(columnProfile.DataObjectName))
							{
								Type dataObjectType = dataObjectStore.GetDataObjectType(columnProfile.DataObjectName);
								if (null != dataObjectType)
								{
									obj = dataObjectType;
									propertyType = dataObjectType.GetProperty(columnProfile.MappingProperty).PropertyType;
								}
							}
						}
					}
					PropertyDefinitionConstraint[] propertyDefinitionConstraints = PropertyConstraintProvider.GetPropertyDefinitionConstraints(obj, binding.BindingMemberInfo.BindingField);
					this.SetConstraintsFromType(propertyType, propertyDefinitionConstraints);
				}
			}
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00039B58 File Offset: 0x00037D58
		private bool IsInvalidChar(char c)
		{
			string maskExpression = this.GetMaskExpression();
			return !string.IsNullOrEmpty(maskExpression) && !Regex.IsMatch(c.ToString(), maskExpression);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00039B88 File Offset: 0x00037D88
		private void SetConstraintsFromType(Type propertyType, PropertyDefinitionConstraint[] constraints)
		{
			if (propertyType.IsGenericType)
			{
				if (propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
				{
					propertyType = propertyType.GetGenericArguments()[0];
				}
				Type[] genericArguments = propertyType.GetGenericArguments();
				if (genericArguments.Length == 1 && !genericArguments[0].IsGenericParameter)
				{
					propertyType = genericArguments[0];
				}
			}
			if (this.IsMaskExpressionEnable())
			{
				if (typeof(short) == propertyType || typeof(int) == propertyType || typeof(long) == propertyType)
				{
					this.SetMaskExpression(this.CreateMaskExpressionForSignedNumericField(constraints));
				}
				else if (typeof(ushort) == propertyType || typeof(uint) == propertyType || typeof(ulong) == propertyType || typeof(ByteQuantifiedSize) == propertyType || typeof(EnhancedTimeSpan) == propertyType)
				{
					this.SetMaskExpression("[0-9]");
				}
				FieldInfo field = propertyType.GetField("AllowedCharacters");
				if (null != field && field.FieldType == typeof(string))
				{
					this.SetMaskExpression((string)field.GetValue(null));
				}
				if (string.IsNullOrEmpty(this.GetMaskExpression()))
				{
					StringBuilder stringBuilder = new StringBuilder("[^");
					foreach (PropertyDefinitionConstraint propertyDefinitionConstraint in constraints)
					{
						CharacterConstraint characterConstraint = propertyDefinitionConstraint as CharacterConstraint;
						if (characterConstraint != null)
						{
							if (characterConstraint.ShowAsValid)
							{
								this.SetMaskExpression(characterConstraint.Pattern);
								break;
							}
							stringBuilder.Append(characterConstraint.Pattern.Substring(2, characterConstraint.Pattern.Length - 3));
						}
					}
					if (string.IsNullOrEmpty(this.GetMaskExpression()) && stringBuilder.Length > 2)
					{
						this.SetMaskExpression(stringBuilder.Append(']').ToString());
					}
				}
			}
			if (this.IsMaxLengthEnable())
			{
				int num = this.IsInvalidChar('-') ? -1 : 0;
				if (typeof(short) == propertyType || typeof(ushort) == propertyType)
				{
					this.SetMaxLength(short.MinValue.ToString().Length + num);
				}
				else if (typeof(int) == propertyType || typeof(uint) == propertyType)
				{
					this.SetMaxLength(int.MinValue.ToString().Length + num);
				}
				else if (typeof(long) == propertyType || typeof(ulong) == propertyType || typeof(ByteQuantifiedSize) == propertyType || typeof(EnhancedTimeSpan) == propertyType)
				{
					this.SetMaxLength(long.MinValue.ToString().Length + num);
				}
				FieldInfo fieldInfo = null;
				Type type = propertyType;
				while (null != type && null == fieldInfo)
				{
					fieldInfo = type.GetField("MaxLength");
					type = type.BaseType;
				}
				if (null != fieldInfo && fieldInfo.FieldType == typeof(int))
				{
					this.SetMaxLength((int)fieldInfo.GetValue(null));
				}
				foreach (PropertyDefinitionConstraint propertyDefinitionConstraint2 in constraints)
				{
					UIImpactStringLengthConstraint uiimpactStringLengthConstraint = propertyDefinitionConstraint2 as UIImpactStringLengthConstraint;
					if (uiimpactStringLengthConstraint != null)
					{
						this.SetMaxLength(uiimpactStringLengthConstraint.MaxLength);
						return;
					}
					StringLengthConstraint stringLengthConstraint = propertyDefinitionConstraint2 as StringLengthConstraint;
					if (stringLengthConstraint != null)
					{
						this.SetMaxLength(stringLengthConstraint.MaxLength);
						return;
					}
				}
			}
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00039F38 File Offset: 0x00038138
		private string CreateMaskExpressionForSignedNumericField(PropertyDefinitionConstraint[] constraints)
		{
			bool flag = false;
			foreach (PropertyDefinitionConstraint propertyDefinitionConstraint in constraints)
			{
				RangedValueConstraint<int> rangedValueConstraint = propertyDefinitionConstraint as RangedValueConstraint<int>;
				if (rangedValueConstraint != null && rangedValueConstraint.MinimumValue >= 0)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return "[-0-9]";
			}
			return "[0-9]";
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x00039F88 File Offset: 0x00038188
		private bool IsFormatModeAndBindingCompatible(DisplayFormatMode format, Binding binding)
		{
			if (binding == null || binding.BindingManagerBase == null)
			{
				return true;
			}
			Type type = null;
			PropertyDescriptorCollection itemProperties = binding.BindingManagerBase.GetItemProperties();
			PropertyDescriptor propertyDescriptor = itemProperties.Find(binding.BindingMemberInfo.BindingField, true);
			if (propertyDescriptor != null)
			{
				FilterValuePropertyDescriptor filterValuePropertyDescriptor = propertyDescriptor as FilterValuePropertyDescriptor;
				if (filterValuePropertyDescriptor != null)
				{
					type = filterValuePropertyDescriptor.ValuePropertyType;
				}
				else
				{
					type = propertyDescriptor.PropertyType;
				}
			}
			if (type == null)
			{
				return true;
			}
			Type type2 = null;
			Type left = null;
			bool flag = false;
			Type left2;
			switch (format)
			{
			case 1:
				left2 = typeof(EnhancedTimeSpan);
				break;
			case 2:
				left2 = typeof(EnhancedTimeSpan);
				break;
			case 3:
				left2 = typeof(EnhancedTimeSpan);
				break;
			case 4:
				left2 = typeof(EnhancedTimeSpan);
				break;
			case 5:
				left2 = typeof(EnhancedTimeSpan);
				flag = true;
				break;
			case 6:
				left2 = typeof(ByteQuantifiedSize);
				break;
			case 7:
				left2 = typeof(ByteQuantifiedSize);
				break;
			case 8:
				left2 = typeof(ByteQuantifiedSize);
				flag = true;
				break;
			case 9:
				left2 = typeof(bool);
				flag = true;
				break;
			case 10:
				left2 = typeof(bool);
				flag = true;
				break;
			case 11:
				left2 = typeof(bool);
				flag = true;
				break;
			case 12:
				left2 = typeof(ADObjectId);
				if (typeof(MultiValuedProperty<ADObjectId>).IsAssignableFrom(type))
				{
					flag = true;
				}
				break;
			case 13:
				left2 = typeof(DateTime);
				flag = true;
				break;
			case 14:
				left2 = typeof(int);
				flag = true;
				break;
			case 15:
				left2 = typeof(SmtpDomainWithSubdomains);
				break;
			default:
				left2 = null;
				break;
			}
			if (flag && binding.DataSourceUpdateMode != DataSourceUpdateMode.Never)
			{
				return false;
			}
			Type type3 = type;
			if (type2 != null)
			{
				return type.GetInterface(type2.ToString()) != null;
			}
			if (type3.IsGenericType)
			{
				if (type3.GetGenericTypeDefinition() == typeof(Nullable<>))
				{
					type3 = type3.GetGenericArguments()[0];
				}
				Type[] genericArguments = type3.GetGenericArguments();
				if (genericArguments.Length == 1 && !genericArguments[0].IsGenericParameter)
				{
					type3 = genericArguments[0];
				}
			}
			if (type3 == typeof(EnhancedTimeSpan) || type3 == typeof(ByteQuantifiedSize) || type3 == typeof(ADObjectId))
			{
				left = type3;
			}
			if (left2 != null || left != null)
			{
				return left2 == type3;
			}
			return format == 0;
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0003A23C File Offset: 0x0003843C
		private void TextBoxValue_Parse(object sender, ConvertEventArgs e)
		{
			try
			{
				if (e.Value is string && (!string.IsNullOrEmpty((string)e.Value) || this.ownerControl.FormatMode == 15))
				{
					object value = this.converter.Parse(e.DesiredType, (string)e.Value, null);
					this.GoThroughRangedConstraint(new TextBoxConstraintProvider.DealWithRangedConstraint(this.ValidateByRangedValueConstraint), value);
					e.Value = value;
				}
			}
			catch (TypeConversionException ex)
			{
				this.GoThroughRangedConstraint(new TextBoxConstraintProvider.DealWithRangedConstraint(this.ThrowRangeConstraintException), null);
				if (ex.InnerException != null)
				{
					throw ex.InnerException;
				}
				throw;
			}
			catch (OverflowException)
			{
				this.GoThroughRangedConstraint(new TextBoxConstraintProvider.DealWithRangedConstraint(this.ThrowRangeConstraintException), null);
				throw;
			}
			catch (FormatException)
			{
				this.GoThroughRangedConstraint(new TextBoxConstraintProvider.DealWithRangedConstraint(this.ThrowRangeConstraintException), null);
				throw;
			}
			catch (DataValidationException)
			{
				this.GoThroughRangedConstraint(new TextBoxConstraintProvider.DealWithRangedConstraint(this.ThrowRangeConstraintException), null);
				throw;
			}
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0003A34C File Offset: 0x0003854C
		private void TextBoxValue_Format(object sender, ConvertEventArgs e)
		{
			e.Value = this.converter.Format(null, e.Value, null);
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0003A368 File Offset: 0x00038568
		private void ValidateByRangedValueConstraint(Type constraintType, PropertyDefinitionConstraint constraint, object constraintSource, object value)
		{
			PropertyDefinition propertyDefinition = PropertyConstraintProvider.GetPropertyDefinition(constraintSource.GetType(), this.ownerControl.DataBindings[this.ownerBindingPropertyName].BindingMemberInfo.BindingField);
			if (propertyDefinition != null)
			{
				PropertyConstraintViolationError propertyConstraintViolationError = constraint.Validate(value, propertyDefinition, null);
				if (propertyConstraintViolationError != null)
				{
					this.ThrowRangeConstraintException(constraintType, constraint, constraintSource, value);
				}
			}
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0003A3C0 File Offset: 0x000385C0
		private void ThrowRangeConstraintException(Type constraintType, PropertyDefinitionConstraint constraint, object constraintSource, object value)
		{
			object value2 = constraintType.GetProperty("MinimumValue").GetValue(constraint, null);
			object value3 = constraintType.GetProperty("MaximumValue").GetValue(constraint, null);
			string minValue = this.Ceiling(this.converter.Format(null, value2, null)).ToString();
			string maxValue = this.Flooring(this.converter.Format(null, value3, null)).ToString();
			if (string.IsNullOrEmpty(this.ownerControl.Text))
			{
				throw new ArgumentException(Strings.EmptyRangeConstraintViolation(this.ownerControl.DataBindings[this.ownerBindingPropertyName].BindingMemberInfo.BindingMember, minValue, maxValue));
			}
			throw new ArgumentException(Strings.RangeConstraintViolation(this.ownerControl.DataBindings[this.ownerBindingPropertyName].BindingMemberInfo.BindingMember, this.ownerControl.Text, minValue, maxValue));
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0003A4B8 File Offset: 0x000386B8
		private long Ceiling(string value)
		{
			double a = Convert.ToDouble(value);
			return long.Parse(Math.Ceiling(a).ToString());
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0003A4DF File Offset: 0x000386DF
		private long Flooring(string value)
		{
			return Convert.ToInt64(Convert.ToDouble(value));
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0003A4EC File Offset: 0x000386EC
		private void GoThroughRangedConstraint(TextBoxConstraintProvider.DealWithRangedConstraint doWork, object value)
		{
			object dataSource = this.ownerControl.DataBindings[this.ownerBindingPropertyName].DataSource;
			while (dataSource is BindingSource)
			{
				dataSource = ((BindingSource)dataSource).DataSource;
			}
			PropertyDefinitionConstraint[] propertyDefinitionConstraints = PropertyConstraintProvider.GetPropertyDefinitionConstraints(dataSource, this.ownerControl.DataBindings[this.ownerBindingPropertyName].BindingMemberInfo.BindingField);
			foreach (PropertyDefinitionConstraint propertyDefinitionConstraint in propertyDefinitionConstraints)
			{
				Type type = propertyDefinitionConstraint.GetType();
				if (type.IsGenericType)
				{
					Type genericTypeDefinition = type.GetGenericTypeDefinition();
					if (typeof(RangedValueConstraint<>) == genericTypeDefinition || typeof(RangedNullableUnlimitedConstraint<>) == genericTypeDefinition || typeof(RangedNullableValueConstraint<>) == genericTypeDefinition || typeof(RangedUnlimitedConstraint<>) == genericTypeDefinition)
					{
						doWork(type, propertyDefinitionConstraint, dataSource, value);
					}
				}
			}
		}

		// Token: 0x040005FB RID: 1531
		private const string DefaultOwnerBindingPropertyName = "Text";

		// Token: 0x040005FC RID: 1532
		private IFormatModeProvider ownerControl;

		// Token: 0x040005FD RID: 1533
		private Control targetTextBox;

		// Token: 0x040005FE RID: 1534
		private ICustomTextConverter converter;

		// Token: 0x040005FF RID: 1535
		private string ownerBindingPropertyName;

		// Token: 0x04000600 RID: 1536
		private PropertyInfo targetControlMaxLengthPropertyInfo;

		// Token: 0x04000601 RID: 1537
		private PropertyInfo targetControlMaskExpressionPropertyInfo;

		// Token: 0x0200016E RID: 366
		// (Invoke) Token: 0x06000F02 RID: 3842
		internal delegate void DealWithRangedConstraint(Type type, PropertyDefinitionConstraint constraint, object constraintSource, object value);
	}
}
