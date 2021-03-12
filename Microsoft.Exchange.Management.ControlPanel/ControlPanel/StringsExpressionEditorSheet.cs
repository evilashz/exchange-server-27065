using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI.Design;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000669 RID: 1641
	internal class StringsExpressionEditorSheet : ExpressionEditorSheet
	{
		// Token: 0x0600473F RID: 18239 RVA: 0x000D8168 File Offset: 0x000D6368
		internal static void AddExpressionGroup(string prefix, Type stringsType, Type idsType)
		{
			if (StringsExpressionEditorSheet.stringsExpressionGroups.ContainsKey(prefix))
			{
				throw new ArgumentException("String expression group is already provided for :" + prefix);
			}
			StringsExpressionEditorSheet.stringsExpressionGroups[prefix] = new StringsExpressionGroup(stringsType, idsType);
		}

		// Token: 0x06004740 RID: 18240 RVA: 0x000D819C File Offset: 0x000D639C
		public StringsExpressionEditorSheet(string expression, IServiceProvider serviceProvider) : base(serviceProvider)
		{
			string[] array = expression.Split(new char[]
			{
				'.'
			});
			if (array != null && array.Length == 2 && !string.IsNullOrEmpty(array[0]) && !string.IsNullOrEmpty(array[1]))
			{
				StringsExpressionGroup stringsExpressionGroup = StringsExpressionEditorSheet.stringsExpressionGroups[array[0]];
				if (stringsExpressionGroup == null)
				{
					throw new Exception("Unsupported String Expression Group: " + array[0]);
				}
				this.Group = stringsExpressionGroup;
				this.StringID = array[1];
			}
			if (string.IsNullOrEmpty(this.StringID))
			{
				this.StringID = expression;
			}
		}

		// Token: 0x17002762 RID: 10082
		// (get) Token: 0x06004741 RID: 18241 RVA: 0x000D8238 File Offset: 0x000D6438
		public override bool IsValid
		{
			get
			{
				bool result;
				try
				{
					Enum.Parse(this.Group.IdsType, this.StringID);
					result = true;
				}
				catch (ArgumentException)
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x06004742 RID: 18242 RVA: 0x000D8278 File Offset: 0x000D6478
		public override string GetExpression()
		{
			return this.stringID;
		}

		// Token: 0x17002763 RID: 10083
		// (get) Token: 0x06004743 RID: 18243 RVA: 0x000D8280 File Offset: 0x000D6480
		// (set) Token: 0x06004744 RID: 18244 RVA: 0x000D8288 File Offset: 0x000D6488
		[DefaultValue("")]
		[DisplayName("String Name")]
		[TypeConverter(typeof(StringsExpressionEditorSheet.StringIDTypeConverter))]
		[Description("The ID of the string to show. The string cannot have parameters.")]
		public string StringID
		{
			get
			{
				return this.stringID;
			}
			set
			{
				this.stringID = (value ?? string.Empty);
			}
		}

		// Token: 0x17002764 RID: 10084
		// (get) Token: 0x06004745 RID: 18245 RVA: 0x000D829A File Offset: 0x000D649A
		// (set) Token: 0x06004746 RID: 18246 RVA: 0x000D82AB File Offset: 0x000D64AB
		public StringsExpressionGroup Group
		{
			get
			{
				return this.group ?? StringsExpressionEditorSheet.defaultGroup;
			}
			set
			{
				this.group = value;
			}
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x000D82B4 File Offset: 0x000D64B4
		public string Evaluate()
		{
			MethodInfo method = this.Group.StringsType.GetMethod("GetLocalizedString", BindingFlags.Static | BindingFlags.Public);
			if (method != null)
			{
				return method.Invoke(null, new object[]
				{
					Enum.Parse(this.Group.IdsType, this.StringID)
				}) as string;
			}
			return string.Empty;
		}

		// Token: 0x04002FF7 RID: 12279
		private static readonly StringsExpressionGroup defaultGroup = new StringsExpressionGroup(typeof(Strings), typeof(Strings.IDs));

		// Token: 0x04002FF8 RID: 12280
		private static Dictionary<string, StringsExpressionGroup> stringsExpressionGroups = new Dictionary<string, StringsExpressionGroup>
		{
			{
				string.Empty,
				new StringsExpressionGroup(typeof(Strings), typeof(Strings.IDs))
			},
			{
				"Client",
				new StringsExpressionGroup(typeof(ClientStrings), typeof(ClientStrings.IDs))
			},
			{
				"OwaOption",
				new StringsExpressionGroup(typeof(OwaOptionStrings), typeof(OwaOptionStrings.IDs))
			},
			{
				"OwaOptionClient",
				new StringsExpressionGroup(typeof(OwaOptionClientStrings), typeof(OwaOptionClientStrings.IDs))
			}
		};

		// Token: 0x04002FF9 RID: 12281
		private string stringID = string.Empty;

		// Token: 0x04002FFA RID: 12282
		private StringsExpressionGroup group;

		// Token: 0x0200066A RID: 1642
		private class StringIDTypeConverter : TypeConverter
		{
			// Token: 0x06004749 RID: 18249 RVA: 0x000D83DF File Offset: 0x000D65DF
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			// Token: 0x0600474A RID: 18250 RVA: 0x000D83E4 File Offset: 0x000D65E4
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				if (StringsExpressionEditorSheet.StringIDTypeConverter.standardValues == null)
				{
					ArrayList arrayList = new ArrayList();
					foreach (KeyValuePair<string, StringsExpressionGroup> keyValuePair in StringsExpressionEditorSheet.stringsExpressionGroups)
					{
						string key = keyValuePair.Key;
						string[] names = Enum.GetNames(keyValuePair.Value.IdsType);
						if (string.Empty == key)
						{
							arrayList.AddRange(names);
						}
						else
						{
							foreach (string arg in names)
							{
								arrayList.Add(key + '.' + arg);
							}
						}
					}
					StringsExpressionEditorSheet.StringIDTypeConverter.standardValues = new TypeConverter.StandardValuesCollection(arrayList.ToArray());
				}
				return StringsExpressionEditorSheet.StringIDTypeConverter.standardValues;
			}

			// Token: 0x04002FFB RID: 12283
			private static TypeConverter.StandardValuesCollection standardValues;
		}
	}
}
