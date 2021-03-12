using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CD8 RID: 3288
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class SubjectProperty : SmartPropertyDefinition
	{
		// Token: 0x060071D7 RID: 29143 RVA: 0x001F83F8 File Offset: 0x001F65F8
		internal SubjectProperty() : base("SubjectProperty", typeof(string), PropertyFlags.None, Array<PropertyDefinitionConstraint>.Empty, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.MapiSubject, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.NormalizedSubjectInternal, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.SubjectPrefixInternal, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.ItemClass, PropertyDependencyType.NeedToReadForWrite),
			new PropertyDependency(InternalSchema.ReplyForwardStatus, PropertyDependencyType.NeedToReadForWrite)
		})
		{
		}

		// Token: 0x060071D8 RID: 29144 RVA: 0x001F8470 File Offset: 0x001F6670
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			string valueOrDefault = propertyBag.GetValueOrDefault<string>(InternalSchema.SubjectPrefixInternal);
			string valueOrDefault2 = propertyBag.GetValueOrDefault<string>(InternalSchema.NormalizedSubjectInternal);
			if (valueOrDefault == null && valueOrDefault2 == null)
			{
				return propertyBag.GetValue(InternalSchema.MapiSubject);
			}
			return valueOrDefault + valueOrDefault2;
		}

		// Token: 0x060071D9 RID: 29145 RVA: 0x001F84B4 File Offset: 0x001F66B4
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			string text = (string)value;
			string text2 = propertyBag.GetValue(InternalSchema.SubjectPrefixInternal) as string;
			string text3 = propertyBag.GetValue(InternalSchema.NormalizedSubjectInternal) as string;
			if (text2 != null && text3 != null && text == text2 + text3)
			{
				return;
			}
			string propertyValue;
			string text4;
			SubjectProperty.ComputeSubjectPrefix(text, out propertyValue, out text4);
			propertyBag.SetValueWithFixup(InternalSchema.SubjectPrefixInternal, propertyValue);
			propertyBag.SetValueWithFixup(InternalSchema.NormalizedSubjectInternal, text4);
			propertyBag.SetValueWithFixup(InternalSchema.MapiSubject, text);
			if (text4 != text3)
			{
				MessageItem messageItem = propertyBag.Context.StoreObject as MessageItem;
				if (messageItem != null)
				{
					string itemClass = propertyBag.GetValue(InternalSchema.ItemClass) as string;
					if (!ObjectClass.IsPost(itemClass))
					{
						messageItem.ConversationTopic = text4;
						if (!string.IsNullOrEmpty(text3))
						{
							messageItem.ConversationIndex = ConversationIndex.CreateNew().ToByteArray();
							if (messageItem.MessageResponseType == MessageResponseType.None)
							{
								SubjectProperty.ClearReplyForwardProperties(messageItem);
								return;
							}
						}
						else if (messageItem.GetValueOrDefault<byte[]>(InternalSchema.ConversationIndex) == null)
						{
							messageItem.ConversationIndex = ConversationIndex.CreateNew().ToByteArray();
						}
					}
				}
			}
		}

		// Token: 0x060071DA RID: 29146 RVA: 0x001F85DC File Offset: 0x001F67DC
		protected override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			propertyBag.Delete(InternalSchema.SubjectPrefixInternal);
			propertyBag.Delete(InternalSchema.NormalizedSubjectInternal);
			propertyBag.Delete(InternalSchema.MapiSubject);
		}

		// Token: 0x060071DB RID: 29147 RVA: 0x001F8604 File Offset: 0x001F6804
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			TextFilter textFilter = filter as TextFilter;
			if (comparisonFilter != null)
			{
				return new ComparisonFilter(comparisonFilter.ComparisonOperator, InternalSchema.MapiSubject, (string)comparisonFilter.PropertyValue);
			}
			if (textFilter != null)
			{
				return new TextFilter(InternalSchema.MapiSubject, textFilter.Text, textFilter.MatchOptions, textFilter.MatchFlags);
			}
			if (filter is ExistsFilter)
			{
				return new ExistsFilter(InternalSchema.MapiSubject);
			}
			return base.SmartFilterToNativeFilter(filter);
		}

		// Token: 0x060071DC RID: 29148 RVA: 0x001F8678 File Offset: 0x001F6878
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			SinglePropertyFilter singlePropertyFilter = filter as SinglePropertyFilter;
			if (singlePropertyFilter != null && singlePropertyFilter.Property.Equals(InternalSchema.MapiSubject))
			{
				ComparisonFilter comparisonFilter = filter as ComparisonFilter;
				TextFilter textFilter = filter as TextFilter;
				if (comparisonFilter != null)
				{
					return new ComparisonFilter(comparisonFilter.ComparisonOperator, this, (string)comparisonFilter.PropertyValue);
				}
				if (textFilter != null)
				{
					return new TextFilter(this, textFilter.Text, textFilter.MatchOptions, textFilter.MatchFlags);
				}
				if (filter is ExistsFilter)
				{
					return new ExistsFilter(this);
				}
			}
			return null;
		}

		// Token: 0x060071DD RID: 29149 RVA: 0x001F86F6 File Offset: 0x001F68F6
		internal override void RegisterFilterTranslation()
		{
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ComparisonFilter));
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(TextFilter));
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ExistsFilter));
		}

		// Token: 0x17001E69 RID: 7785
		// (get) Token: 0x060071DE RID: 29150 RVA: 0x001F8728 File Offset: 0x001F6928
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x060071DF RID: 29151 RVA: 0x001F872B File Offset: 0x001F692B
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return InternalSchema.NormalizedSubjectInternal;
		}

		// Token: 0x060071E0 RID: 29152 RVA: 0x001F8734 File Offset: 0x001F6934
		internal static void ComputeSubjectPrefix(string subject, out string prefix, out string normalized)
		{
			int num = Math.Min(subject.Length, 4);
			int num2 = -1;
			for (int i = 0; i < num; i++)
			{
				if (subject[i] == ':' && i + 1 < subject.Length && subject[i + 1] == ' ')
				{
					num2 = i + 1;
					break;
				}
				if (!char.IsLetter(subject[i]))
				{
					break;
				}
			}
			if (num2 > 0)
			{
				prefix = subject.Substring(0, num2 + 1);
				normalized = subject.Substring(num2 + 1);
				return;
			}
			prefix = string.Empty;
			normalized = subject;
		}

		// Token: 0x060071E1 RID: 29153 RVA: 0x001F87B9 File Offset: 0x001F69B9
		internal static string ExtractPrefixUsingNormalizedSubject(string mapiSubject, string normalizedSubject)
		{
			if (mapiSubject.EndsWith(normalizedSubject, StringComparison.Ordinal))
			{
				return mapiSubject.Substring(0, mapiSubject.Length - normalizedSubject.Length);
			}
			return null;
		}

		// Token: 0x060071E2 RID: 29154 RVA: 0x001F87DB File Offset: 0x001F69DB
		internal static void ModifySubjectProperty(Item item, NativeStorePropertyDefinition property, string value)
		{
			SubjectProperty.ModifySubjectProperty(item.PropertyBag, property, value);
		}

		// Token: 0x060071E3 RID: 29155 RVA: 0x001F87EA File Offset: 0x001F69EA
		internal static void ModifySubjectProperty(PropertyBag propertyBag, NativeStorePropertyDefinition property, string value)
		{
			SubjectProperty.ModifySubjectProperty((PropertyBag.BasicPropertyStore)propertyBag, property, value);
		}

		// Token: 0x060071E4 RID: 29156 RVA: 0x001F87FC File Offset: 0x001F69FC
		internal static void ModifySubjectProperty(PropertyBag.BasicPropertyStore item, NativeStorePropertyDefinition property, string value)
		{
			string text = item.GetValue(InternalSchema.SubjectPrefixInternal) as string;
			string text2 = item.GetValue(InternalSchema.NormalizedSubjectInternal) as string;
			string text3 = item.GetValue(InternalSchema.MapiSubject) as string;
			if (property == InternalSchema.NormalizedSubjectInternal)
			{
				text2 = value;
				if (text3 != null)
				{
					string text4 = SubjectProperty.ExtractPrefixUsingNormalizedSubject(text3, text2);
					if (text4 != null)
					{
						text = text4;
					}
				}
				if (text == null)
				{
					text = string.Empty;
				}
			}
			else if (property == InternalSchema.SubjectPrefixInternal)
			{
				text = value;
				if (text3 != null && text3.StartsWith(text, StringComparison.Ordinal))
				{
					text2 = text3.Substring(text.Length);
				}
				if (text2 == null)
				{
					text2 = string.Empty;
				}
			}
			else
			{
				if (property != InternalSchema.MapiSubject)
				{
					throw new ArgumentException("Not a supported subject property", "property");
				}
				if (!string.IsNullOrEmpty(text) && value.StartsWith(text, StringComparison.Ordinal))
				{
					text2 = value.Substring(text.Length);
				}
				else if (!string.IsNullOrEmpty(text2))
				{
					string text5 = SubjectProperty.ExtractPrefixUsingNormalizedSubject(value, text2);
					if (text5 != null)
					{
						text = text5;
					}
					else
					{
						SubjectProperty.ComputeSubjectPrefix(value, out text, out text2);
					}
				}
				else
				{
					SubjectProperty.ComputeSubjectPrefix(value, out text, out text2);
				}
			}
			text3 = text + text2;
			item.SetValueWithFixup(InternalSchema.SubjectPrefixInternal, text);
			item.SetValueWithFixup(InternalSchema.NormalizedSubjectInternal, text2);
			item.SetValueWithFixup(InternalSchema.MapiSubject, text3);
			string itemClass = item.GetValue(InternalSchema.ItemClass) as string;
			if (!ObjectClass.IsPost(itemClass))
			{
				item.SetValueWithFixup(InternalSchema.ConversationTopic, text2);
			}
		}

		// Token: 0x060071E5 RID: 29157 RVA: 0x001F895E File Offset: 0x001F6B5E
		internal static void TruncateSubject(Item item, int limit)
		{
			SubjectProperty.TruncateSubject(item.PropertyBag, limit);
		}

		// Token: 0x060071E6 RID: 29158 RVA: 0x001F8970 File Offset: 0x001F6B70
		internal static bool TruncateSubject(PropertyBag propertyBag, int limit)
		{
			bool result = false;
			string text = propertyBag.TryGetProperty(InternalSchema.Subject) as string;
			if (text != null && SubjectProperty.TruncateSubject(ref text, limit))
			{
				SubjectProperty.ModifySubjectProperty((PropertyBag.BasicPropertyStore)propertyBag, InternalSchema.MapiSubject, text);
				result = true;
			}
			return result;
		}

		// Token: 0x060071E7 RID: 29159 RVA: 0x001F89B4 File Offset: 0x001F6BB4
		internal static bool TruncateSubject(ref string subject, int limit)
		{
			if (subject.Length > limit)
			{
				int num = limit - "...".Length;
				bool flag = true;
				if (num < 0)
				{
					num = limit;
					flag = false;
				}
				if (char.IsHighSurrogate(subject[num - 1]))
				{
					num--;
				}
				subject = subject.Substring(0, num);
				if (flag)
				{
					subject += "...";
				}
				return true;
			}
			return false;
		}

		// Token: 0x060071E8 RID: 29160 RVA: 0x001F8A16 File Offset: 0x001F6C16
		private static void ClearReplyForwardProperties(MessageItem message)
		{
			message.MessageResponseType = MessageResponseType.None;
			message.ParentMessageId = null;
			message[InternalSchema.InReplyTo] = string.Empty;
			message[InternalSchema.InternetReferences] = string.Empty;
			message[InternalSchema.ReplyForwardStatus] = string.Empty;
		}

		// Token: 0x04004F11 RID: 20241
		private const string TruncationString = "...";

		// Token: 0x04004F12 RID: 20242
		private const int MaxPrefixLength = 4;
	}
}
