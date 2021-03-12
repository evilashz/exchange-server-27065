using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CB8 RID: 3256
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class ReplicaListProperty : SmartPropertyDefinition
	{
		// Token: 0x0600714E RID: 29006 RVA: 0x001F6B29 File Offset: 0x001F4D29
		internal ReplicaListProperty() : base("ReplicaListProperty", typeof(string[]), PropertyFlags.None, PropertyDefinitionConstraint.None, ReplicaListProperty.dependentProps)
		{
		}

		// Token: 0x0600714F RID: 29007 RVA: 0x001F6B4C File Offset: 0x001F4D4C
		public static string[] GetStringArrayFromBytes(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			List<string> list = new List<string>();
			int num = 0;
			int num2 = 0;
			while (bytes.Length > num2)
			{
				if (bytes[num2] == 0)
				{
					list.Add(CTSGlobals.AsciiEncoding.GetString(bytes, num, num2 - num));
					num = 1 + num2;
				}
				num2++;
			}
			return list.ToArray();
		}

		// Token: 0x06007150 RID: 29008 RVA: 0x001F6BA4 File Offset: 0x001F4DA4
		public static byte[] GetBytesFromStringArray(string[] strings)
		{
			if (strings == null)
			{
				throw new ArgumentNullException("strings");
			}
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(1000))
			{
				foreach (string s in strings)
				{
					byte[] bytes = CTSGlobals.AsciiEncoding.GetBytes(s);
					memoryStream.Write(bytes, 0, bytes.Length);
					memoryStream.WriteByte(0);
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06007151 RID: 29009 RVA: 0x001F6C28 File Offset: 0x001F4E28
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			byte[] array = propertyBag.GetValue(InternalSchema.ReplicaListBinary) as byte[];
			if (array == null)
			{
				return new PropertyError(this, PropertyErrorCode.NotFound);
			}
			return ReplicaListProperty.GetStringArrayFromBytes(array);
		}

		// Token: 0x06007152 RID: 29010 RVA: 0x001F6C58 File Offset: 0x001F4E58
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			string[] array = value as string[];
			if (array != null)
			{
				propertyBag.SetValue(InternalSchema.ReplicaListBinary, ReplicaListProperty.GetBytesFromStringArray(array));
			}
		}

		// Token: 0x04004EA5 RID: 20133
		private static PropertyDependency[] dependentProps = new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.ReplicaListBinary, PropertyDependencyType.NeedToReadForWrite)
		};
	}
}
