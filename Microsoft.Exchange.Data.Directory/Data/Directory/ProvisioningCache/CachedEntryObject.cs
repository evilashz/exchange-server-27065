using System;
using System.Collections;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.ProvisioningCache
{
	// Token: 0x0200079E RID: 1950
	public class CachedEntryObject
	{
		// Token: 0x06006111 RID: 24849 RVA: 0x00149E75 File Offset: 0x00148075
		public CachedEntryObject(Guid key, object value) : this(key, Guid.Empty, value)
		{
		}

		// Token: 0x06006112 RID: 24850 RVA: 0x00149E84 File Offset: 0x00148084
		public CachedEntryObject(Guid key, Guid orgId, object value)
		{
			this.CacheKey = key;
			this.OrganizationId = orgId;
			this.RawValue = value;
		}

		// Token: 0x170022B9 RID: 8889
		// (get) Token: 0x06006113 RID: 24851 RVA: 0x00149EAC File Offset: 0x001480AC
		// (set) Token: 0x06006114 RID: 24852 RVA: 0x00149EB4 File Offset: 0x001480B4
		public Guid CacheKey
		{
			get
			{
				return this.cacheKey;
			}
			private set
			{
				this.cacheKey = value;
			}
		}

		// Token: 0x170022BA RID: 8890
		// (get) Token: 0x06006115 RID: 24853 RVA: 0x00149EBD File Offset: 0x001480BD
		// (set) Token: 0x06006116 RID: 24854 RVA: 0x00149EC5 File Offset: 0x001480C5
		public Guid OrganizationId
		{
			get
			{
				return this.organizationId;
			}
			private set
			{
				this.organizationId = value;
			}
		}

		// Token: 0x170022BB RID: 8891
		// (get) Token: 0x06006117 RID: 24855 RVA: 0x00149ECE File Offset: 0x001480CE
		// (set) Token: 0x06006118 RID: 24856 RVA: 0x00149ED6 File Offset: 0x001480D6
		public object RawValue
		{
			get
			{
				return this.rawValue;
			}
			private set
			{
				this.rawValue = value;
			}
		}

		// Token: 0x06006119 RID: 24857 RVA: 0x00149EE0 File Offset: 0x001480E0
		public byte[] ToSendMessage()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.CacheKey.ToString()).Append('\n');
			stringBuilder.Append(this.OrganizationId.ToString()).Append('\n');
			string value = CachedEntryObject.RawValueToString(this.CacheKey, this.rawValue);
			if (!string.IsNullOrWhiteSpace(value))
			{
				stringBuilder.Append(value);
			}
			else
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			return Encoding.UTF8.GetBytes(stringBuilder.ToString());
		}

		// Token: 0x0600611A RID: 24858 RVA: 0x00149F7B File Offset: 0x0014817B
		private static string RawValueToString(Guid cacheKey, object value)
		{
			if (value == null)
			{
				return null;
			}
			if (value is IDictionary)
			{
				return CachedEntryObject.BuildStringFromDictionary(cacheKey, (IDictionary)value);
			}
			if (value is IEnumerable)
			{
				return CachedEntryObject.BuildStringFromCollection(cacheKey, (IEnumerable)value);
			}
			return CachedEntryObject.BuildStringFromSingleValue(cacheKey, value);
		}

		// Token: 0x0600611B RID: 24859 RVA: 0x00149FB4 File Offset: 0x001481B4
		private static string BuildStringFromDictionary(Guid key, IDictionary dictionary)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in dictionary.Keys)
			{
				string value = CachedEntryObject.BuildStringFromSingleValue(key, obj);
				string value2 = CachedEntryObject.RawValueToString(key, dictionary[obj]);
				stringBuilder.Append(value).Append('=');
				if (!string.IsNullOrWhiteSpace(value2))
				{
					stringBuilder.Append(value2).Append(';');
				}
				else
				{
					stringBuilder.Append("Null").Append(';');
				}
			}
			return stringBuilder.ToString().TrimEnd(new char[]
			{
				';'
			});
		}

		// Token: 0x0600611C RID: 24860 RVA: 0x0014A07C File Offset: 0x0014827C
		private static string BuildStringFromCollection(Guid key, IEnumerable collection)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object value in collection)
			{
				string value2 = CachedEntryObject.RawValueToString(key, value);
				if (!string.IsNullOrWhiteSpace(value2))
				{
					stringBuilder.Append(value2).Append(';');
				}
				else
				{
					stringBuilder.Append("Null").Append(';');
				}
			}
			return stringBuilder.ToString().TrimEnd(new char[]
			{
				';'
			});
		}

		// Token: 0x0600611D RID: 24861 RVA: 0x0014A11C File Offset: 0x0014831C
		private static string BuildStringFromSingleValue(Guid key, object value)
		{
			if (value == null)
			{
				return null;
			}
			if (value is ADObjectId)
			{
				return ((ADObjectId)value).DistinguishedName;
			}
			if (value is ADObject)
			{
				return ((ADObject)value).DistinguishedName;
			}
			return value.ToString();
		}

		// Token: 0x04004114 RID: 16660
		private const char Separator = ';';

		// Token: 0x04004115 RID: 16661
		private const char Equator = '=';

		// Token: 0x04004116 RID: 16662
		private const char Delimiter = '\n';

		// Token: 0x04004117 RID: 16663
		private Guid cacheKey;

		// Token: 0x04004118 RID: 16664
		private Guid organizationId = Guid.Empty;

		// Token: 0x04004119 RID: 16665
		private object rawValue;
	}
}
