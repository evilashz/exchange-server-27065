using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
	// Token: 0x020003A3 RID: 931
	[ComVisible(true)]
	[Serializable]
	public class SortKey
	{
		// Token: 0x0600303E RID: 12350 RVA: 0x000B8FAC File Offset: 0x000B71AC
		internal SortKey(string localeName, string str, CompareOptions options, byte[] keyData)
		{
			this.m_KeyData = keyData;
			this.localeName = localeName;
			this.options = options;
			this.m_String = str;
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x000B8FD1 File Offset: 0x000B71D1
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			if (this.win32LCID == 0)
			{
				this.win32LCID = CultureInfo.GetCultureInfo(this.localeName).LCID;
			}
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x000B8FF1 File Offset: 0x000B71F1
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (string.IsNullOrEmpty(this.localeName) && this.win32LCID != 0)
			{
				this.localeName = CultureInfo.GetCultureInfo(this.win32LCID).Name;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06003041 RID: 12353 RVA: 0x000B901E File Offset: 0x000B721E
		public virtual string OriginalString
		{
			get
			{
				return this.m_String;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06003042 RID: 12354 RVA: 0x000B9026 File Offset: 0x000B7226
		public virtual byte[] KeyData
		{
			get
			{
				return (byte[])this.m_KeyData.Clone();
			}
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x000B9038 File Offset: 0x000B7238
		public static int Compare(SortKey sortkey1, SortKey sortkey2)
		{
			if (sortkey1 == null || sortkey2 == null)
			{
				throw new ArgumentNullException((sortkey1 == null) ? "sortkey1" : "sortkey2");
			}
			byte[] keyData = sortkey1.m_KeyData;
			byte[] keyData2 = sortkey2.m_KeyData;
			if (keyData.Length == 0)
			{
				if (keyData2.Length == 0)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (keyData2.Length == 0)
				{
					return 1;
				}
				int num = (keyData.Length < keyData2.Length) ? keyData.Length : keyData2.Length;
				for (int i = 0; i < num; i++)
				{
					if (keyData[i] > keyData2[i])
					{
						return 1;
					}
					if (keyData[i] < keyData2[i])
					{
						return -1;
					}
				}
				return 0;
			}
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x000B90B4 File Offset: 0x000B72B4
		public override bool Equals(object value)
		{
			SortKey sortKey = value as SortKey;
			return sortKey != null && SortKey.Compare(this, sortKey) == 0;
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000B90D7 File Offset: 0x000B72D7
		public override int GetHashCode()
		{
			return CompareInfo.GetCompareInfo(this.localeName).GetHashCodeOfString(this.m_String, this.options);
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x000B90F8 File Offset: 0x000B72F8
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"SortKey - ",
				this.localeName,
				", ",
				this.options,
				", ",
				this.m_String
			});
		}

		// Token: 0x0400145E RID: 5214
		[OptionalField(VersionAdded = 3)]
		internal string localeName;

		// Token: 0x0400145F RID: 5215
		[OptionalField(VersionAdded = 1)]
		internal int win32LCID;

		// Token: 0x04001460 RID: 5216
		internal CompareOptions options;

		// Token: 0x04001461 RID: 5217
		internal string m_String;

		// Token: 0x04001462 RID: 5218
		internal byte[] m_KeyData;
	}
}
