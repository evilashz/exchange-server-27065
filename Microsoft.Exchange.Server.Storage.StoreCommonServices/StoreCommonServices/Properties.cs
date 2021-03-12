using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000ED RID: 237
	public struct Properties : IEnumerable<Property>, IEnumerable
	{
		// Token: 0x06000962 RID: 2402 RVA: 0x0002BC4E File Offset: 0x00029E4E
		public Properties(int initialCapacity)
		{
			this.properties = new List<Property>(initialCapacity);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0002BC5C File Offset: 0x00029E5C
		public Properties(Properties properties)
		{
			this.properties = new List<Property>(properties.Count);
			foreach (Property prop in properties)
			{
				this.Add(prop);
			}
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0002BCC0 File Offset: 0x00029EC0
		public Properties(IList<StorePropTag> tags, IList<object> values)
		{
			if (tags == null)
			{
				this.properties = new List<Property>(10);
				return;
			}
			this.properties = new List<Property>(tags.Count);
			for (int i = 0; i < tags.Count; i++)
			{
				this.Add(tags[i], (values != null) ? values[i] : null);
			}
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0002BD1C File Offset: 0x00029F1C
		public Properties(IEnumerable<Property> properties)
		{
			this.properties = new List<Property>(10);
			foreach (Property prop in properties)
			{
				this.Add(prop);
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x0002BD74 File Offset: 0x00029F74
		public int Count
		{
			get
			{
				if (this.properties != null)
				{
					return this.properties.Count;
				}
				return 0;
			}
		}

		// Token: 0x17000268 RID: 616
		public Property this[int i]
		{
			get
			{
				return this.properties[i];
			}
			set
			{
				this.properties[i] = value;
			}
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0002BDA8 File Offset: 0x00029FA8
		public static void RemoveFrom(List<Property> list, StorePropTag tag)
		{
			if (list != null && list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].Tag == tag)
					{
						list.RemoveAt(i);
						return;
					}
				}
			}
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0002BDF4 File Offset: 0x00029FF4
		public static bool Contains(List<Property> list, StorePropTag propTag)
		{
			if (list != null && list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].Tag == propTag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0002BE38 File Offset: 0x0002A038
		public void Add(StorePropTag tag, object value)
		{
			this.properties.Add(new Property(tag, value));
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0002BE4C File Offset: 0x0002A04C
		public void Add(Property prop)
		{
			this.properties.Add(prop);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0002BE5C File Offset: 0x0002A05C
		public void AddOrReplace(StorePropTag tag, object value)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (tag == this.properties[i].Tag)
				{
					this.properties[i] = new Property(tag, value);
					return;
				}
			}
			this.Add(tag, value);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0002BEB4 File Offset: 0x0002A0B4
		public void AddOrReplace(Property prop)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (prop.Tag == this.properties[i].Tag)
				{
					this.properties[i] = prop;
					return;
				}
			}
			this.Add(prop);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0002BF0C File Offset: 0x0002A10C
		public void AddOrReplace(IEnumerable<Property> properties)
		{
			foreach (Property prop in properties)
			{
				this.AddOrReplace(prop);
			}
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0002BF54 File Offset: 0x0002A154
		public void Remove(StorePropTag tag)
		{
			Properties.RemoveFrom(this.properties, tag);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0002BF64 File Offset: 0x0002A164
		public void Remove(IEnumerable<StorePropTag> tags)
		{
			foreach (StorePropTag tag in tags)
			{
				this.Remove(tag);
			}
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0002BFAC File Offset: 0x0002A1AC
		public void RemoveAt(int index)
		{
			this.properties.RemoveAt(index);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0002BFBA File Offset: 0x0002A1BA
		public void Clear()
		{
			this.properties.Clear();
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0002BFC7 File Offset: 0x0002A1C7
		public bool Contains(StorePropTag propTag)
		{
			return Properties.Contains(this.properties, propTag);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0002BFD8 File Offset: 0x0002A1D8
		public Property GetProperty(StorePropTag propTag)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this.properties[i].Tag == propTag)
				{
					return this.properties[i];
				}
			}
			return Property.NotFoundError(propTag);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0002C028 File Offset: 0x0002A228
		public object GetValue(StorePropTag propTag)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this.properties[i].Tag == propTag)
				{
					return this.properties[i].Value;
				}
			}
			return null;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0002C078 File Offset: 0x0002A278
		public StorePropTag[] GetPropTags()
		{
			StorePropTag[] array = new StorePropTag[this.Count];
			for (int i = 0; i < this.Count; i++)
			{
				array[i] = this.properties[i].Tag;
			}
			return array;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0002C0C3 File Offset: 0x0002A2C3
		public List<Property>.Enumerator GetEnumerator()
		{
			if (this.properties == null)
			{
				return Properties.emptyProperties.GetEnumerator();
			}
			return this.properties.GetEnumerator();
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0002C0E3 File Offset: 0x0002A2E3
		IEnumerator<Property> IEnumerable<Property>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0002C0F0 File Offset: 0x0002A2F0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0002C100 File Offset: 0x0002A300
		public void AppendToString(StringBuilder sb)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (i != 0)
				{
					sb.Append(" ");
				}
				this.properties[i].AppendToString(sb);
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0002C144 File Offset: 0x0002A344
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.Count * 30);
			this.AppendToString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0400054D RID: 1357
		private const int DefaultCapacity = 10;

		// Token: 0x0400054E RID: 1358
		public static readonly Properties Empty = default(Properties);

		// Token: 0x0400054F RID: 1359
		private static List<Property> emptyProperties = new List<Property>(0);

		// Token: 0x04000550 RID: 1360
		private List<Property> properties;
	}
}
