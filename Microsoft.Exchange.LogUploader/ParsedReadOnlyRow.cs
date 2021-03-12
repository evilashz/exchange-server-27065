using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000020 RID: 32
	internal class ParsedReadOnlyRow
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x00008F59 File Offset: 0x00007159
		public ParsedReadOnlyRow(ReadOnlyRow unparsedRow)
		{
			this.table = unparsedRow.Schema;
			this.unparsedRow = unparsedRow;
			this.parsedRow = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00008F84 File Offset: 0x00007184
		public CsvTable Schema
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00008F8C File Offset: 0x0000718C
		public ReadOnlyRow UnParsedRow
		{
			get
			{
				return this.unparsedRow;
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008F94 File Offset: 0x00007194
		public static object ConvertToTargetObjectType(string propertyName, object obj, Type targetType)
		{
			if (obj == null)
			{
				return null;
			}
			if (targetType.IsAssignableFrom(obj.GetType()))
			{
				return obj;
			}
			object result;
			try
			{
				if (targetType.IsEnum)
				{
					string value = obj.ToString();
					object obj2 = Enum.Parse(targetType, value);
					if (!Enum.IsDefined(targetType, obj2))
					{
						throw new InvalidCastException(string.Format("Cannot convert object type {0} to {1}, for field:{2}", obj.GetType().Name, targetType.Name, propertyName));
					}
					result = obj2;
				}
				else if (targetType.IsAssignableFrom(typeof(Guid)))
				{
					result = Guid.Parse(obj.ToString());
				}
				else
				{
					result = Convert.ChangeType(obj, targetType);
				}
			}
			catch (InvalidCastException)
			{
				throw;
			}
			catch (Exception innerException)
			{
				throw new InvalidCastException(string.Format("Cannot convert object type {0} to {1}, for field:{2}", obj.GetType().Name, targetType.Name, propertyName), innerException);
			}
			return result;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00009070 File Offset: 0x00007270
		public bool HasCustomField(string customfieldName)
		{
			if (this.CustomFieldHasData(customfieldName))
			{
				return true;
			}
			this.ParseField(customfieldName, typeof(KeyValuePair<string, object>[]));
			return this.CustomFieldHasData(customfieldName);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009098 File Offset: 0x00007298
		public T GetField<T>(string fieldName)
		{
			T result;
			try
			{
				result = (T)((object)this.GetField(fieldName, typeof(T)));
			}
			catch (InvalidLogLineException)
			{
				throw;
			}
			catch (Exception inner)
			{
				throw new InvalidLogLineException(Strings.FailedToCastToRequestedType(typeof(T), fieldName), inner);
			}
			return result;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000090F8 File Offset: 0x000072F8
		public object GetField(string fieldName, Type targetType)
		{
			object obj;
			if (this.parsedRow.ContainsKey(fieldName))
			{
				obj = this.parsedRow[fieldName];
			}
			else
			{
				Type fieldType;
				if (!this.unparsedRow.Schema.TryGetTypeByName(fieldName, out fieldType))
				{
					throw new InvalidLogLineException(Strings.UnknownField(fieldName));
				}
				obj = this.ParseField(fieldName, fieldType);
			}
			return ParsedReadOnlyRow.ConvertToTargetObjectType(fieldName, obj, targetType);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00009156 File Offset: 0x00007356
		public KeyValuePair<string, object>[] GetCustomFieldData(string customfieldName)
		{
			if (!this.HasCustomField(customfieldName))
			{
				throw new MissingPropertyException(Strings.RequestedCustomDataFieldMissing(customfieldName));
			}
			return (KeyValuePair<string, object>[])this.parsedRow[customfieldName];
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000919C File Offset: 0x0000739C
		public object GetPropertyValueFromCustomField(string customfieldName, string propertyName)
		{
			if (string.IsNullOrWhiteSpace(customfieldName))
			{
				throw new ArgumentException("customfieldName cannot be null or empty");
			}
			if (string.IsNullOrWhiteSpace(propertyName))
			{
				throw new ArgumentException("propertyName cannot be null or empty");
			}
			object result = null;
			KeyValuePair<string, object>[] field = this.GetField<KeyValuePair<string, object>[]>(customfieldName);
			if (field != null)
			{
				KeyValuePair<string, object> keyValuePair = field.FirstOrDefault((KeyValuePair<string, object> kvp) => kvp.Key.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));
				if (keyValuePair.Key != null && keyValuePair.Value != null)
				{
					result = keyValuePair.Value;
				}
			}
			return result;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00009228 File Offset: 0x00007428
		public void ParseAllFields()
		{
			for (int i = 0; i < this.unparsedRow.Schema.Fields.Length; i++)
			{
				CsvField csvField = this.unparsedRow.Schema.Fields[i];
				Type type = csvField.Type;
				string name = csvField.Name;
				this.ParseField(name, type);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000927C File Offset: 0x0000747C
		private object ParseField(string fieldName, Type fieldType)
		{
			object result;
			try
			{
				if (fieldType == typeof(string))
				{
					this.parsedRow[fieldName] = this.unparsedRow.GetField<string>(fieldName);
				}
				else if (fieldType == typeof(DateTime))
				{
					this.parsedRow[fieldName] = this.unparsedRow.GetField<DateTime>(fieldName);
				}
				else if (fieldType == typeof(int))
				{
					this.parsedRow[fieldName] = this.unparsedRow.GetField<int>(fieldName);
				}
				else if (fieldType == typeof(KeyValuePair<string, object>[]))
				{
					this.parsedRow[fieldName] = this.unparsedRow.GetField<KeyValuePair<string, object>[]>(fieldName);
				}
				else if (fieldType == typeof(string[]))
				{
					this.parsedRow[fieldName] = this.unparsedRow.GetField<string[]>(fieldName);
				}
				else
				{
					if (!(fieldType == typeof(int[])))
					{
						throw new NotSupportedException(string.Format("Parsing CsvField Type:{0} is not supported, for column name:{1}", fieldType, fieldName));
					}
					this.parsedRow[fieldName] = this.unparsedRow.GetField<int[]>(fieldName);
				}
				result = this.parsedRow[fieldName];
			}
			catch (Exception inner)
			{
				throw new InvalidLogLineException(Strings.FailedToParseField(fieldName, fieldType), inner);
			}
			return result;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000093F4 File Offset: 0x000075F4
		private bool CustomFieldHasData(string customFieldName)
		{
			return this.parsedRow.ContainsKey(customFieldName) && this.parsedRow[customFieldName] != null && ((KeyValuePair<string, object>[])this.parsedRow[customFieldName]).Length != 0;
		}

		// Token: 0x040000E4 RID: 228
		private readonly CsvTable table;

		// Token: 0x040000E5 RID: 229
		private readonly Dictionary<string, object> parsedRow;

		// Token: 0x040000E6 RID: 230
		private readonly ReadOnlyRow unparsedRow;
	}
}
