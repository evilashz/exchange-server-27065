using System;
using System.Net;
using System.Net.Sockets;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x0200026F RID: 623
	internal abstract class PagedObjectSchema : ObjectSchema
	{
		// Token: 0x060014D3 RID: 5331
		internal abstract int GetFieldIndex(string fieldName);

		// Token: 0x060014D4 RID: 5332
		internal abstract bool TryGetFieldIndex(string fieldName, out int index);

		// Token: 0x060014D5 RID: 5333
		internal abstract string GetFieldName(int field);

		// Token: 0x060014D6 RID: 5334
		internal abstract ProviderPropertyDefinition GetFieldByName(string fieldName);

		// Token: 0x060014D7 RID: 5335
		internal abstract bool IsBasicField(int field);

		// Token: 0x060014D8 RID: 5336
		internal abstract Type GetFieldType(int field);

		// Token: 0x060014D9 RID: 5337
		internal abstract bool MatchField(int field, PagedDataObject pagedDataObject, object matchPattern, MatchOptions matchOptions);

		// Token: 0x060014DA RID: 5338
		internal abstract int CompareField(int field, PagedDataObject pagedDataObject, object value);

		// Token: 0x060014DB RID: 5339
		internal abstract int CompareField(int field, PagedDataObject object1, PagedDataObject object2);

		// Token: 0x060014DC RID: 5340 RVA: 0x00042CEA File Offset: 0x00040EEA
		protected static int CompareString(string v1, string v2)
		{
			return string.Compare(v1, v2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00042CF4 File Offset: 0x00040EF4
		internal static bool MatchString(string sourceText, string matchText, MatchOptions matchOptions)
		{
			if (sourceText == null)
			{
				sourceText = string.Empty;
			}
			switch (matchOptions)
			{
			case MatchOptions.FullString:
				return sourceText.Equals(matchText, StringComparison.OrdinalIgnoreCase);
			case MatchOptions.SubString:
				return sourceText.IndexOf(matchText, StringComparison.OrdinalIgnoreCase) != -1;
			case MatchOptions.Prefix:
				return sourceText.StartsWith(matchText, StringComparison.OrdinalIgnoreCase);
			case MatchOptions.Suffix:
				return sourceText.EndsWith(matchText, StringComparison.OrdinalIgnoreCase);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x00042D54 File Offset: 0x00040F54
		protected static int CompareDateTimeNullable(DateTime? v1, DateTime? v2)
		{
			if (v1 == v2)
			{
				return 0;
			}
			if (v1 != null && v2 == null)
			{
				return 1;
			}
			if (v1 == null && v2 != null)
			{
				return -1;
			}
			return v1.Value.CompareTo(v2.Value);
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x00042DB0 File Offset: 0x00040FB0
		protected static int CompareIPAddress(IPAddress v1, IPAddress v2)
		{
			if (v1 == null)
			{
				throw new ArgumentNullException("Null IPAddress", "v1");
			}
			if (v2 == null)
			{
				throw new ArgumentNullException("Null IPAddress", "v2");
			}
			if (v1.AddressFamily == AddressFamily.InterNetwork && v2.AddressFamily == AddressFamily.InterNetworkV6)
			{
				return -1;
			}
			if (v1.AddressFamily == AddressFamily.InterNetworkV6 && v2.AddressFamily == AddressFamily.InterNetwork)
			{
				return 1;
			}
			byte[] addressBytes = v1.GetAddressBytes();
			byte[] addressBytes2 = v2.GetAddressBytes();
			for (int i = 0; i < addressBytes.Length; i++)
			{
				if (addressBytes[i] < addressBytes2[i])
				{
					return -1;
				}
				if (addressBytes[i] > addressBytes2[i])
				{
					return 1;
				}
			}
			return 0;
		}
	}
}
