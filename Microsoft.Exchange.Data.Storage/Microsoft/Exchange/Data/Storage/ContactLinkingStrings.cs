using System;
using System.Collections;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004C6 RID: 1222
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class ContactLinkingStrings
	{
		// Token: 0x060035A4 RID: 13732 RVA: 0x000D7F5C File Offset: 0x000D615C
		public static string GetValueString(object value)
		{
			if (value == null)
			{
				return null;
			}
			string text = value as string;
			if (text != null)
			{
				return ContactLinkingStrings.GetValueString(text);
			}
			byte[] array = value as byte[];
			if (array != null)
			{
				return ContactLinkingStrings.GetValueString(array);
			}
			Guid? nullableGuid = value as Guid?;
			if (nullableGuid != null)
			{
				return ContactLinkingStrings.GetValueString(nullableGuid);
			}
			VersionedId versionedId = value as VersionedId;
			if (versionedId != null)
			{
				return ContactLinkingStrings.GetValueString(versionedId);
			}
			IEnumerable enumerable = value as IEnumerable;
			if (enumerable != null)
			{
				return ContactLinkingStrings.GetValueString(enumerable);
			}
			ExDateTime? nullableExDateTime = value as ExDateTime?;
			if (nullableExDateTime != null)
			{
				return ContactLinkingStrings.GetValueString(nullableExDateTime);
			}
			DateTime? nullableDateTime = value as DateTime?;
			if (nullableDateTime != null)
			{
				return ContactLinkingStrings.GetValueString(nullableDateTime);
			}
			return value.ToString();
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x000D8013 File Offset: 0x000D6213
		public static string GetValueString(string valueString)
		{
			if (string.IsNullOrEmpty(valueString))
			{
				return null;
			}
			return valueString;
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x000D8020 File Offset: 0x000D6220
		public static string GetValueString(byte[] byteArray)
		{
			if (byteArray.Length == 0)
			{
				return null;
			}
			return BitConverter.ToString(byteArray).Replace("-", string.Empty);
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x000D8040 File Offset: 0x000D6240
		public static string GetValueString(Guid? nullableGuid)
		{
			if (nullableGuid != null)
			{
				return nullableGuid.Value.ToString();
			}
			return null;
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x000D8070 File Offset: 0x000D6270
		public static string GetValueString(ExDateTime? nullableExDateTime)
		{
			if (nullableExDateTime != null)
			{
				return nullableExDateTime.Value.ToString(CultureInfo.InvariantCulture);
			}
			return null;
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x000D809C File Offset: 0x000D629C
		public static string GetValueString(DateTime? nullableDateTime)
		{
			if (nullableDateTime != null)
			{
				return nullableDateTime.Value.ToString(CultureInfo.InvariantCulture);
			}
			return null;
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x000D80C8 File Offset: 0x000D62C8
		public static string GetValueString(IEnumerable collection)
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("{");
			foreach (object value in collection)
			{
				stringBuilder.Append(ContactLinkingStrings.GetValueString(value));
				stringBuilder.Append(",");
			}
			if (stringBuilder.Length > 1)
			{
				stringBuilder[stringBuilder.Length - 1] = '}';
				return stringBuilder.ToString();
			}
			return string.Empty;
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x000D8164 File Offset: 0x000D6364
		public static string GetValueString(VersionedId versionedId)
		{
			return versionedId.ObjectId.ToString();
		}
	}
}
