using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000C6 RID: 198
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MigrationStoreObject : DisposeTrackableBase, IMigrationStoreObject, IDisposable, IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0002BFB1 File Offset: 0x0002A1B1
		public StoreObjectId Id
		{
			get
			{
				return this.StoreObject.StoreObjectId;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0002BFBE File Offset: 0x0002A1BE
		public ExDateTime CreationTime
		{
			get
			{
				return this.StoreObject.CreationTime;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000A82 RID: 2690
		public abstract string Name { get; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000A83 RID: 2691
		protected abstract StoreObject StoreObject { get; }

		// Token: 0x17000360 RID: 864
		public object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				return this.GetProperty(propertyDefinition);
			}
			set
			{
				this.StoreObject[propertyDefinition] = value;
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0002BFE3 File Offset: 0x0002A1E3
		public virtual void OpenAsReadWrite()
		{
		}

		// Token: 0x06000A87 RID: 2695
		public abstract void Save(SaveMode saveMode);

		// Token: 0x06000A88 RID: 2696 RVA: 0x0002BFE5 File Offset: 0x0002A1E5
		public virtual void LoadMessageIdProperties()
		{
			this.Load(MigrationStoreObject.IdPropertyDefinition);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0002BFF2 File Offset: 0x0002A1F2
		public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
		{
			return this.StoreObject.GetProperties(propertyDefinitionArray);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0002C000 File Offset: 0x0002A200
		public void SetProperties(ICollection<PropertyDefinition> propertyDefinitionArray, object[] propertyValuesArray)
		{
			this.StoreObject.SetProperties(propertyDefinitionArray, propertyValuesArray);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0002C00F File Offset: 0x0002A20F
		public void Delete(PropertyDefinition propertyDefinition)
		{
			this.StoreObject.Delete(propertyDefinition);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002C020 File Offset: 0x0002A220
		public T GetValueOrDefault<T>(PropertyDefinition propertyDefinition, T defaultValue)
		{
			object obj = this.TryGetProperty(propertyDefinition);
			PropertyError propertyError = obj as PropertyError;
			if (obj == null || (propertyError != null && propertyError.PropertyErrorCode == PropertyErrorCode.NotFound))
			{
				return defaultValue;
			}
			if (propertyError != null)
			{
				throw PropertyError.ToException(new PropertyError[]
				{
					(PropertyError)obj
				});
			}
			return (T)((object)obj);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002C06D File Offset: 0x0002A26D
		public void Load(ICollection<PropertyDefinition> properties)
		{
			this.StoreObject.Load(properties);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002C07C File Offset: 0x0002A27C
		public virtual XElement GetDiagnosticInfo(ICollection<PropertyDefinition> properties, MigrationDiagnosticArgument argument)
		{
			XElement xelement = new XElement("StoreObject", new object[]
			{
				new XAttribute("id", this.Id.ToBase64String()),
				new XAttribute("creationTime", this.CreationTime),
				new XAttribute("name", this.Name)
			});
			foreach (PropertyDefinition propertyDefinition in properties)
			{
				try
				{
					object valueOrDefault = this.GetValueOrDefault<object>(propertyDefinition, null);
					XElement xelement2 = new XElement("Property", new object[]
					{
						new XAttribute("name", propertyDefinition.Name),
						new XAttribute("type", propertyDefinition.Type)
					});
					if (valueOrDefault != null)
					{
						if (propertyDefinition.Type == typeof(byte[]))
						{
							xelement2.Add(Convert.ToBase64String((byte[])valueOrDefault));
						}
						else if (propertyDefinition.Type.IsArray)
						{
							StringBuilder stringBuilder = new StringBuilder();
							foreach (object value in ((Array)valueOrDefault))
							{
								stringBuilder.Append(value);
								stringBuilder.Append(',');
							}
							xelement2.Add(stringBuilder.ToString());
						}
						else
						{
							xelement2.Add(valueOrDefault.ToString());
						}
					}
					else
					{
						xelement2.Add(new XAttribute("isnull", "true"));
					}
					xelement.Add(xelement2);
				}
				catch (NotInBagPropertyErrorException)
				{
					XElement content = new XElement("Property", new XAttribute("Status", "Not found"));
					xelement.Add(content);
				}
			}
			return xelement;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0002C2CC File Offset: 0x0002A4CC
		private object GetProperty(PropertyDefinition propertyDefinition)
		{
			object obj = this.TryGetProperty(propertyDefinition);
			if (obj == null)
			{
				throw PropertyError.ToException(new PropertyError[]
				{
					new PropertyError(propertyDefinition, PropertyErrorCode.NullValue)
				});
			}
			if (PropertyError.IsPropertyError(obj))
			{
				throw PropertyError.ToException(new PropertyError[]
				{
					(PropertyError)obj
				});
			}
			return obj;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002C31C File Offset: 0x0002A51C
		private object TryGetProperty(PropertyDefinition propertyDefinition)
		{
			object obj = this.StoreObject.TryGetProperty(propertyDefinition);
			if (propertyDefinition.Type == typeof(string) && PropertyError.IsPropertyValueTooBig(obj))
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "MigrationStoreObject.TryGetProperty: reading {0} as a stream", new object[]
				{
					propertyDefinition
				});
				using (Stream stream = this.StoreObject.OpenPropertyStream(propertyDefinition, PropertyOpenMode.ReadOnly))
				{
					if (stream.Length > 131072L)
					{
						throw new MigrationDataCorruptionException(string.Format("size of property {0} too large {1}", propertyDefinition, stream.Length));
					}
					int num = (int)stream.Length;
					byte[] array = new byte[num];
					int i = 0;
					while (i < num)
					{
						int num2 = stream.Read(array, i, num - i);
						i += num2;
						if (num2 <= 0)
						{
							break;
						}
					}
					if (i != num)
					{
						throw new MigrationDataCorruptionException(string.Format("size of property {0} inconsistent, expected {1}, found {2}", propertyDefinition, num, i));
					}
					UnicodeEncoding unicodeEncoding = new UnicodeEncoding(false, true, true);
					try
					{
						obj = unicodeEncoding.GetString(array);
					}
					catch (ArgumentException innerException)
					{
						throw new MigrationDataCorruptionException(string.Format("couldn't decode bytes to utf16 for property {0}", propertyDefinition), innerException);
					}
				}
			}
			return obj;
		}

		// Token: 0x04000414 RID: 1044
		private const int MaxPropertySize = 131072;

		// Token: 0x04000415 RID: 1045
		internal static readonly PropertyDefinition[] IdPropertyDefinition = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.CreationTime
		};
	}
}
