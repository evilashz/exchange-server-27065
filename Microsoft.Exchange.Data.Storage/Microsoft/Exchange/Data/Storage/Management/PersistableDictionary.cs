using System;
using System.Collections;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A48 RID: 2632
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PersistableDictionary : ConfigurationDictionary
	{
		// Token: 0x17001A81 RID: 6785
		public object this[object key]
		{
			get
			{
				return base.Dictionary[key];
			}
			set
			{
				base.Dictionary[key] = value;
			}
		}

		// Token: 0x17001A82 RID: 6786
		// (get) Token: 0x06006042 RID: 24642 RVA: 0x00196155 File Offset: 0x00194355
		public ICollection Values
		{
			get
			{
				return base.Dictionary.Values;
			}
		}

		// Token: 0x06006043 RID: 24643 RVA: 0x00196162 File Offset: 0x00194362
		public static PersistableDictionary Create(string rawXml)
		{
			return MigrationXmlSerializer.Deserialize<PersistableDictionary>(rawXml);
		}

		// Token: 0x06006044 RID: 24644 RVA: 0x0019616C File Offset: 0x0019436C
		public T GetRequired<T>(object key)
		{
			object obj = this[key];
			if (obj == null)
			{
				throw new MigrationDataCorruptionException("expected to find key " + key);
			}
			return PersistableDictionary.DeserializeProperty<T>(obj);
		}

		// Token: 0x06006045 RID: 24645 RVA: 0x0019619B File Offset: 0x0019439B
		public T Get<T>(object key)
		{
			return PersistableDictionary.DeserializeProperty<T>(this[key]);
		}

		// Token: 0x06006046 RID: 24646 RVA: 0x001961AC File Offset: 0x001943AC
		public bool TryGetValue<T>(object key, out T value)
		{
			value = default(T);
			if (!this.Contains(key))
			{
				return false;
			}
			bool result;
			try
			{
				value = this.Get<T>(key);
				result = true;
			}
			catch (NotInBagPropertyErrorException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06006047 RID: 24647 RVA: 0x001961F4 File Offset: 0x001943F4
		public T Get<T>(string key, T defaultValue)
		{
			object obj = this[key];
			if (obj == null)
			{
				return defaultValue;
			}
			return PersistableDictionary.DeserializeProperty<T>(obj);
		}

		// Token: 0x06006048 RID: 24648 RVA: 0x00196214 File Offset: 0x00194414
		public void Set<T>(string key, T value)
		{
			this[key] = PersistableDictionary.SerializeProperty<T>(value);
		}

		// Token: 0x06006049 RID: 24649 RVA: 0x00196228 File Offset: 0x00194428
		public void Remove(string key)
		{
			base.Dictionary.Remove(key);
		}

		// Token: 0x0600604A RID: 24650 RVA: 0x00196236 File Offset: 0x00194436
		public bool Contains(object key)
		{
			return base.Dictionary.Contains(key);
		}

		// Token: 0x0600604B RID: 24651 RVA: 0x00196244 File Offset: 0x00194444
		public string Serialize()
		{
			return MigrationXmlSerializer.Serialize(this);
		}

		// Token: 0x0600604C RID: 24652 RVA: 0x0019624C File Offset: 0x0019444C
		public void Add(object key, object value)
		{
			base.Dictionary.Add(key, value);
		}

		// Token: 0x0600604D RID: 24653 RVA: 0x0019625B File Offset: 0x0019445B
		public void SetMultiValuedProperty(string key, MultiValuedProperty<string> value)
		{
			this[key] = MigrationXmlSerializer.Serialize(value);
		}

		// Token: 0x0600604E RID: 24654 RVA: 0x0019626C File Offset: 0x0019446C
		public MultiValuedProperty<string> GetMultiValuedStringProperty(string key)
		{
			if (this.Contains(key))
			{
				string required = this.GetRequired<string>(key);
				return (MultiValuedProperty<string>)MigrationXmlSerializer.Deserialize(required, typeof(MultiValuedProperty<string>));
			}
			return null;
		}

		// Token: 0x0600604F RID: 24655 RVA: 0x001962A4 File Offset: 0x001944A4
		private static T DeserializeProperty<T>(object val)
		{
			if (val != null)
			{
				Type typeFromHandle = typeof(T);
				if (typeFromHandle.IsEnum)
				{
					try
					{
						val = Enum.ToObject(typeFromHandle, (int)val);
						goto IL_B6;
					}
					catch (ArgumentException innerException)
					{
						throw new MigrationDataCorruptionException("can't convert serialized version", innerException);
					}
				}
				if (typeFromHandle == typeof(TimeSpan?) || typeFromHandle == typeof(TimeSpan))
				{
					val = new TimeSpan((long)val);
				}
				else
				{
					if (!(typeFromHandle == typeof(Guid?)))
					{
						if (!(typeFromHandle == typeof(Guid)))
						{
							goto IL_B6;
						}
					}
					try
					{
						val = new Guid((byte[])val);
					}
					catch (ArgumentException innerException2)
					{
						throw new MigrationDataCorruptionException("couldn't deserialize guid", innerException2);
					}
				}
			}
			IL_B6:
			return (T)((object)val);
		}

		// Token: 0x06006050 RID: 24656 RVA: 0x0019638C File Offset: 0x0019458C
		private static object SerializeProperty<T>(object val)
		{
			if (val != null)
			{
				Type typeFromHandle = typeof(T);
				if (typeFromHandle.IsEnum)
				{
					return (int)val;
				}
				if (typeFromHandle == typeof(TimeSpan))
				{
					return ((TimeSpan)val).Ticks;
				}
				if (typeFromHandle == typeof(TimeSpan?))
				{
					return ((TimeSpan?)val).Value.Ticks;
				}
				if (typeFromHandle == typeof(Guid?) || typeFromHandle == typeof(Guid))
				{
					return ((Guid)val).ToByteArray();
				}
			}
			return val;
		}
	}
}
