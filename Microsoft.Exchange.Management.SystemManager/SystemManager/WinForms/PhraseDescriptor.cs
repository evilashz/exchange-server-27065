using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001CB RID: 459
	public class PhraseDescriptor : INotifyPropertyChanged
	{
		// Token: 0x0600135B RID: 4955 RVA: 0x0004E89C File Offset: 0x0004CA9C
		public PhraseDescriptor(int index, string markupText)
		{
			if (string.IsNullOrEmpty(markupText))
			{
				throw new ArgumentNullException("markupText");
			}
			this.index = index;
			this.markupText = markupText;
			this.EditingProperties = new MarkupParser
			{
				Markup = markupText
			}.GetEditingProperties();
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600135C RID: 4956 RVA: 0x0004E908 File Offset: 0x0004CB08
		// (set) Token: 0x0600135D RID: 4957 RVA: 0x0004E910 File Offset: 0x0004CB10
		public Dictionary<string, bool> EditingProperties { get; private set; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x0004E919 File Offset: 0x0004CB19
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x0004E921 File Offset: 0x0004CB21
		public string MarkupText
		{
			get
			{
				return this.markupText;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x0004E929 File Offset: 0x0004CB29
		// (set) Token: 0x06001361 RID: 4961 RVA: 0x0004E931 File Offset: 0x0004CB31
		public TypeMapping Type2UIEditor
		{
			get
			{
				return this.type2UIEditor;
			}
			set
			{
				this.type2UIEditor = value;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x0004E93A File Offset: 0x0004CB3A
		// (set) Token: 0x06001363 RID: 4963 RVA: 0x0004E942 File Offset: 0x0004CB42
		[DefaultValue(null)]
		public object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				if (value != this.DataSource)
				{
					this.dataSource = value;
					this.used = this.HasInitializedValuesOfEditingProperties();
				}
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x0004E960 File Offset: 0x0004CB60
		// (set) Token: 0x06001365 RID: 4965 RVA: 0x0004E968 File Offset: 0x0004CB68
		[DefaultValue(false)]
		public bool Used
		{
			get
			{
				return this.used;
			}
			set
			{
				if (this.used != value)
				{
					this.used = value;
					if (value)
					{
						this.InitializeValuesOfEditingProperties();
					}
					else
					{
						this.CleanValuesOfEditingProperties();
					}
					this.NotifyPropertyChanged("Used");
				}
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x0004E996 File Offset: 0x0004CB96
		// (set) Token: 0x06001367 RID: 4967 RVA: 0x0004E99E File Offset: 0x0004CB9E
		public string ListSeparator
		{
			get
			{
				return this.listSeparator;
			}
			set
			{
				this.listSeparator = value;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x0004E9A7 File Offset: 0x0004CBA7
		// (set) Token: 0x06001369 RID: 4969 RVA: 0x0004E9AF File Offset: 0x0004CBAF
		[DefaultValue("")]
		public string PhrasePresentationPrefix { get; set; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x0004E9B8 File Offset: 0x0004CBB8
		// (set) Token: 0x0600136B RID: 4971 RVA: 0x0004E9C0 File Offset: 0x0004CBC0
		[DefaultValue(0)]
		public int GroupID { get; set; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x0004E9CC File Offset: 0x0004CBCC
		public bool IsValuesOfEditingPropertiesValid
		{
			get
			{
				ValidationError[] array = this.Validate();
				return array == null || array.Length == 0;
			}
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0004E9EC File Offset: 0x0004CBEC
		public virtual UITypeEditor CreateEditor(PropertyDescriptor property)
		{
			UITypeEditor result = null;
			if (this.Type2UIEditor != null && property != null)
			{
				Type nullableTypeArgument = WinformsHelper.GetNullableTypeArgument(property.PropertyType);
				Type type = this.Type2UIEditor[nullableTypeArgument] as Type;
				if (typeof(UITypeEditor).IsAssignableFrom(type))
				{
					result = (Activator.CreateInstance(type) as UITypeEditor);
				}
			}
			return result;
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0004EA44 File Offset: 0x0004CC44
		public virtual ValidationError[] Validate()
		{
			List<ValidationError> list = new List<ValidationError>();
			if (this.Used)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.DataSource);
				foreach (string text in this.EditingProperties.Keys)
				{
					PropertyDescriptor propertyDescriptor = properties[text];
					object propertyValue = (propertyDescriptor != null) ? propertyDescriptor.GetValue(this.DataSource) : null;
					if (WinformsHelper.IsEmptyValue(propertyValue))
					{
						list.Add(new StrongTypeValidationError(Strings.InvalidPhraseValues(text), text));
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0004EAF0 File Offset: 0x0004CCF0
		protected virtual void CleanValuesOfEditingProperties()
		{
			if (this.DataSource != null)
			{
				foreach (string text in this.EditingProperties.Keys)
				{
					object value = null;
					if (!this.EditingProperties[text])
					{
						value = false;
					}
					this.SetDataSourceProperty(text, value);
				}
			}
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0004EB68 File Offset: 0x0004CD68
		protected virtual void InitializeValuesOfEditingProperties()
		{
			if (this.DataSource != null)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.DataSource);
				foreach (string text in this.EditingProperties.Keys)
				{
					if (!this.EditingProperties[text])
					{
						this.SetDataSourceProperty(text, true);
					}
					else
					{
						PropertyDescriptor propertyDescriptor = properties[text];
						Type nullableTypeArgument = WinformsHelper.GetNullableTypeArgument(propertyDescriptor.PropertyType);
						if (null != nullableTypeArgument && nullableTypeArgument.IsValueType)
						{
							this.SetDataSourceProperty(text, Activator.CreateInstance(nullableTypeArgument));
						}
					}
				}
			}
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0004EC20 File Offset: 0x0004CE20
		protected virtual bool HasInitializedValuesOfEditingProperties()
		{
			bool result = false;
			if (this.DataSource != null)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.DataSource);
				foreach (string text in this.EditingProperties.Keys)
				{
					PropertyDescriptor propertyDescriptor = properties[text];
					object value = propertyDescriptor.GetValue(this.DataSource);
					if ((!this.EditingProperties[text] && true.Equals(value)) || (this.EditingProperties[text] && !WinformsHelper.IsEmptyValue(value)))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0004ECDC File Offset: 0x0004CEDC
		public void SetDataSourceProperty(string propertyName, object value)
		{
			if (this.DataSource != null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.DataSource)[propertyName];
				propertyDescriptor.SetValue(this.DataSource, value);
				this.OnPhraseEditingPropertyUpdated();
			}
		}

		// Token: 0x14000078 RID: 120
		// (add) Token: 0x06001373 RID: 4979 RVA: 0x0004ED18 File Offset: 0x0004CF18
		// (remove) Token: 0x06001374 RID: 4980 RVA: 0x0004ED50 File Offset: 0x0004CF50
		public event PropertyChangedEventHandler PhraseEditingPropertyUpdated;

		// Token: 0x06001375 RID: 4981 RVA: 0x0004ED88 File Offset: 0x0004CF88
		private void OnPhraseEditingPropertyUpdated()
		{
			if (this.PhraseEditingPropertyUpdated != null)
			{
				foreach (string propertyName in this.EditingProperties.Keys)
				{
					this.PhraseEditingPropertyUpdated(this, new PropertyChangedEventArgs(propertyName));
				}
			}
		}

		// Token: 0x14000079 RID: 121
		// (add) Token: 0x06001376 RID: 4982 RVA: 0x0004EDF4 File Offset: 0x0004CFF4
		// (remove) Token: 0x06001377 RID: 4983 RVA: 0x0004EE2C File Offset: 0x0004D02C
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06001378 RID: 4984 RVA: 0x0004EE61 File Offset: 0x0004D061
		private void NotifyPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x0400072B RID: 1835
		private int index;

		// Token: 0x0400072C RID: 1836
		private string markupText;

		// Token: 0x0400072D RID: 1837
		private TypeMapping type2UIEditor;

		// Token: 0x0400072E RID: 1838
		private object dataSource;

		// Token: 0x0400072F RID: 1839
		private bool used;

		// Token: 0x04000730 RID: 1840
		private string listSeparator = " " + Strings.ListSeparatorForCollection + " ";
	}
}
