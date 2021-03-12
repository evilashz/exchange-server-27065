using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000751 RID: 1873
	internal static class IOUtil
	{
		// Token: 0x0600529A RID: 21146 RVA: 0x00122271 File Offset: 0x00120471
		internal static bool FlagTest(MessageEnum flag, MessageEnum target)
		{
			return (flag & target) == target;
		}

		// Token: 0x0600529B RID: 21147 RVA: 0x0012227C File Offset: 0x0012047C
		internal static void WriteStringWithCode(string value, __BinaryWriter sout)
		{
			if (value == null)
			{
				sout.WriteByte(17);
				return;
			}
			sout.WriteByte(18);
			sout.WriteString(value);
		}

		// Token: 0x0600529C RID: 21148 RVA: 0x0012229C File Offset: 0x0012049C
		internal static void WriteWithCode(Type type, object value, __BinaryWriter sout)
		{
			if (type == null)
			{
				sout.WriteByte(17);
				return;
			}
			if (type == Converter.typeofString)
			{
				IOUtil.WriteStringWithCode((string)value, sout);
				return;
			}
			InternalPrimitiveTypeE internalPrimitiveTypeE = Converter.ToCode(type);
			sout.WriteByte((byte)internalPrimitiveTypeE);
			sout.WriteValue(internalPrimitiveTypeE, value);
		}

		// Token: 0x0600529D RID: 21149 RVA: 0x001222E4 File Offset: 0x001204E4
		internal static object ReadWithCode(__BinaryParser input)
		{
			InternalPrimitiveTypeE internalPrimitiveTypeE = (InternalPrimitiveTypeE)input.ReadByte();
			if (internalPrimitiveTypeE == InternalPrimitiveTypeE.Null)
			{
				return null;
			}
			if (internalPrimitiveTypeE == InternalPrimitiveTypeE.String)
			{
				return input.ReadString();
			}
			return input.ReadValue(internalPrimitiveTypeE);
		}

		// Token: 0x0600529E RID: 21150 RVA: 0x00122314 File Offset: 0x00120514
		internal static object[] ReadArgs(__BinaryParser input)
		{
			int num = input.ReadInt32();
			object[] array = new object[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = IOUtil.ReadWithCode(input);
			}
			return array;
		}
	}
}
