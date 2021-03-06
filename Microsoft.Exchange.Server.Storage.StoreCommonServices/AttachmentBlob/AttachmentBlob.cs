using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;

namespace Microsoft.Exchange.Server.Storage.AttachmentBlob
{
	// Token: 0x02000010 RID: 16
	public static class AttachmentBlob
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000C2DB File Offset: 0x0000A4DB
		public static int MaxSupportedDescendantCountRead
		{
			get
			{
				return ConfigurationSchema.AttachmentBlobMaxSupportedDescendantCountRead.Value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000C2E7 File Offset: 0x0000A4E7
		public static int MaxSupportedDescendantCountWrite
		{
			get
			{
				return ConfigurationSchema.AttachmentBlobMaxSupportedDescendantCountWrite.Value;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000C2F4 File Offset: 0x0000A4F4
		public static int GetCount(byte[] blob, bool childrenOnly)
		{
			int num = 0;
			int num2;
			int num3;
			int num4;
			if (!AttachmentBlob.ParseHeader(blob, ref num, out num2, out num3, out num4))
			{
				return 0;
			}
			if (!childrenOnly)
			{
				return num3 + num4;
			}
			return num3;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000C320 File Offset: 0x0000A520
		public static IEnumerable<KeyValuePair<int, long>> Deserialize(byte[] blob, bool childrenOnly)
		{
			int offset = 0;
			int reserved;
			int childrenCount;
			int indirectCount;
			if (!AttachmentBlob.ParseHeader(blob, ref offset, out reserved, out childrenCount, out indirectCount))
			{
				return Enumerable.Empty<KeyValuePair<int, long>>();
			}
			return AttachmentBlob.Deserialize(blob, offset, reserved, childrenCount, indirectCount, childrenOnly);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000C544 File Offset: 0x0000A744
		private static IEnumerable<KeyValuePair<int, long>> Deserialize(byte[] blob, int offset, int reserved, int childrenCount, int indirectCount, bool childrenOnly)
		{
			for (int i = 0; i < childrenCount; i++)
			{
				int childNumber = SerializedValue.ParseInt32(blob, ref offset);
				long inid = SerializedValue.ParseInt64(blob, ref offset);
				if (childNumber < 0 || childNumber == 2147483647)
				{
					throw new InvalidAttachmentBlobException("blob parsing error");
				}
				yield return new KeyValuePair<int, long>(childNumber, inid);
			}
			if (!childrenOnly)
			{
				for (int j = 0; j < indirectCount; j++)
				{
					long inid2 = SerializedValue.ParseInt64(blob, ref offset);
					yield return new KeyValuePair<int, long>(int.MinValue, inid2);
				}
			}
			yield break;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000C580 File Offset: 0x0000A780
		private static bool ParseHeader(byte[] blob, ref int offset, out int reserved, out int childrenCount, out int indirectCount)
		{
			if (blob == null)
			{
				reserved = 0;
				childrenCount = 0;
				indirectCount = 0;
				return false;
			}
			reserved = SerializedValue.ParseInt32(blob, ref offset);
			childrenCount = SerializedValue.ParseInt32(blob, ref offset);
			if (childrenCount <= 0 || childrenCount > AttachmentBlob.MaxSupportedDescendantCountRead)
			{
				throw new InvalidAttachmentBlobException("blob parsing error");
			}
			indirectCount = SerializedValue.ParseInt32(blob, ref offset);
			if (indirectCount < 0 || indirectCount > AttachmentBlob.MaxSupportedDescendantCountRead || childrenCount + indirectCount > AttachmentBlob.MaxSupportedDescendantCountRead)
			{
				throw new InvalidAttachmentBlobException("blob parsing error");
			}
			return true;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000C5FA File Offset: 0x0000A7FA
		[Conditional("DEBUG")]
		private static void Assert(bool condition, string message)
		{
			if (!condition)
			{
				throw new Exception(message);
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000C670 File Offset: 0x0000A870
		public static byte[] Serialize(IEnumerable<KeyValuePair<int, long>> subobjects, bool renumberChildren)
		{
			if (subobjects == null)
			{
				return null;
			}
			List<KeyValuePair<int, long>> list = new List<KeyValuePair<int, long>>(subobjects);
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Count > AttachmentBlob.MaxSupportedDescendantCountWrite)
			{
				throw new StoreException((LID)53088U, ErrorCodeValue.MaxAttachmentExceeded);
			}
			if (list.Count > 1)
			{
				list.Sort(delegate(KeyValuePair<int, long> x, KeyValuePair<int, long> y)
				{
					int num6 = (x.Key < 0) ? int.MinValue : x.Key;
					int num7 = (y.Key < 0) ? int.MinValue : y.Key;
					if (num6 != num7)
					{
						return (num6 - 1).CompareTo(num7 - 1);
					}
					return x.Value.CompareTo(y.Value);
				});
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Key >= 0)
				{
					num++;
					num3 += SerializedValue.SerializeInt32(renumberChildren ? num4 : list[i].Key, null, 0);
					num3 += SerializedValue.SerializeInt64(list[i].Value, null, 0);
					num4++;
				}
				else
				{
					num2++;
					num3 += SerializedValue.SerializeInt64(list[i].Value, null, 0);
				}
			}
			num3 += SerializedValue.SerializeInt32(0, null, 0);
			num3 += SerializedValue.SerializeInt32(num, null, 0);
			num3 += SerializedValue.SerializeInt32(num2, null, 0);
			if (num == 0)
			{
				return null;
			}
			byte[] array = new byte[num3];
			int num5 = 0;
			num4 = 0;
			num5 += SerializedValue.SerializeInt32(0, array, num5);
			num5 += SerializedValue.SerializeInt32(num, array, num5);
			num5 += SerializedValue.SerializeInt32(num2, array, num5);
			for (int j = 0; j < num; j++)
			{
				num5 += SerializedValue.SerializeInt32(renumberChildren ? num4 : list[j].Key, array, num5);
				num5 += SerializedValue.SerializeInt64(list[j].Value, array, num5);
				num4++;
			}
			for (int k = 0; k < num2; k++)
			{
				num5 += SerializedValue.SerializeInt64(list[num + k].Value, array, num5);
			}
			return array;
		}
	}
}
