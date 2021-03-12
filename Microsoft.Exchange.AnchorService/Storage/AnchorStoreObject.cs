using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AnchorService.Storage
{
	// Token: 0x02000030 RID: 48
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AnchorStoreObject : DisposeTrackableBase, IAnchorStoreObject, IDisposable, IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00007A88 File Offset: 0x00005C88
		public StoreObjectId Id
		{
			get
			{
				return this.StoreObject.StoreObjectId;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00007A95 File Offset: 0x00005C95
		public ExDateTime CreationTime
		{
			get
			{
				return this.StoreObject.CreationTime;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000209 RID: 521
		public abstract string Name { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00007AA2 File Offset: 0x00005CA2
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00007AAA File Offset: 0x00005CAA
		protected AnchorContext AnchorContext { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600020C RID: 524
		protected abstract StoreObject StoreObject { get; }

		// Token: 0x17000087 RID: 135
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

		// Token: 0x0600020F RID: 527 RVA: 0x00007ACB File Offset: 0x00005CCB
		public virtual void OpenAsReadWrite()
		{
		}

		// Token: 0x06000210 RID: 528
		public abstract void Save(SaveMode saveMode);

		// Token: 0x06000211 RID: 529 RVA: 0x00007ACD File Offset: 0x00005CCD
		public virtual void LoadMessageIdProperties()
		{
			this.Load(AnchorStoreObject.IdPropertyDefinition);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007ADA File Offset: 0x00005CDA
		public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
		{
			return this.StoreObject.GetProperties(propertyDefinitionArray);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007AE8 File Offset: 0x00005CE8
		public void SetProperties(ICollection<PropertyDefinition> propertyDefinitionArray, object[] propertyValuesArray)
		{
			this.StoreObject.SetProperties(propertyDefinitionArray, propertyValuesArray);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007AF7 File Offset: 0x00005CF7
		public void Delete(PropertyDefinition propertyDefinition)
		{
			this.StoreObject.Delete(propertyDefinition);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00007B08 File Offset: 0x00005D08
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

		// Token: 0x06000216 RID: 534 RVA: 0x00007B55 File Offset: 0x00005D55
		public void Load(ICollection<PropertyDefinition> properties)
		{
			this.StoreObject.Load(properties);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007B64 File Offset: 0x00005D64
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

		// Token: 0x06000218 RID: 536 RVA: 0x00007BB4 File Offset: 0x00005DB4
		private object TryGetProperty(PropertyDefinition propertyDefinition)
		{
			object obj = this.StoreObject.TryGetProperty(propertyDefinition);
			if (propertyDefinition.Type == typeof(string) && PropertyError.IsPropertyValueTooBig(obj))
			{
				this.AnchorContext.Logger.Log(MigrationEventType.Verbose, "AnchorStoreObject.TryGetProperty: reading {0} as a stream", new object[]
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

		// Token: 0x04000092 RID: 146
		private const int MaxPropertySize = 131072;

		// Token: 0x04000093 RID: 147
		internal static readonly PropertyDefinition[] IdPropertyDefinition = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.CreationTime
		};
	}
}
