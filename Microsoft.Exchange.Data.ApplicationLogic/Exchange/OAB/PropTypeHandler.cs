using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Mapi;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x02000164 RID: 356
	internal abstract class PropTypeHandler
	{
		// Token: 0x06000E2C RID: 3628 RVA: 0x0003B284 File Offset: 0x00039484
		public static PropTypeHandler GetHandler(PropType propType)
		{
			if (propType <= PropType.Binary)
			{
				if (propType <= PropType.String)
				{
					if (propType == PropType.Int)
					{
						return PropTypeHandler.intHandler;
					}
					if (propType == PropType.Boolean)
					{
						return PropTypeHandler.booleanHandler;
					}
					switch (propType)
					{
					case PropType.AnsiString:
						return PropTypeHandler.ansiStringHandler;
					case PropType.String:
						return PropTypeHandler.stringHandler;
					}
				}
				else
				{
					if (propType == PropType.SysTime)
					{
						return PropTypeHandler.sysTimeHandler;
					}
					if (propType == PropType.Guid)
					{
						return PropTypeHandler.guidHandler;
					}
					if (propType == PropType.Binary)
					{
						return PropTypeHandler.binaryHandler;
					}
				}
			}
			else if (propType <= PropType.StringArray)
			{
				if (propType == PropType.IntArray)
				{
					return PropTypeHandler.intArrayHandler;
				}
				if (propType == (PropType)4107)
				{
					return PropTypeHandler.booleanArrayHandler;
				}
				switch (propType)
				{
				case PropType.AnsiStringArray:
					return PropTypeHandler.ansiStringArrayHandler;
				case PropType.StringArray:
					return PropTypeHandler.stringArrayHandler;
				}
			}
			else
			{
				if (propType == PropType.SysTimeArray)
				{
					return PropTypeHandler.sysTimeArrayHandler;
				}
				if (propType == PropType.GuidArray)
				{
					return PropTypeHandler.guidArrayHandler;
				}
				if (propType == PropType.BinaryArray)
				{
					return PropTypeHandler.binaryArrayHandler;
				}
			}
			return PropTypeHandler.none;
		}

		// Token: 0x06000E2D RID: 3629
		public abstract object ReadFrom(BinaryReader reader, string elementName);

		// Token: 0x06000E2E RID: 3630
		public abstract void WriteTo(BinaryWriter writer, object value);

		// Token: 0x06000E2F RID: 3631
		public abstract void AppendText(StringBuilder text, object value);

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0003B383 File Offset: 0x00039583
		public virtual bool IsWritable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0003B386 File Offset: 0x00039586
		protected static void WriteBytes(BinaryWriter writer, byte[] bytes)
		{
			CompressedUIntReaderWriter.WriteTo(writer, (uint)bytes.Length);
			writer.Write(bytes);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0003B398 File Offset: 0x00039598
		protected static byte[] ReadStringBytes(BinaryReader reader, string elementName)
		{
			List<byte> list = new List<byte>(200);
			for (;;)
			{
				byte b = reader.ReadByte(elementName);
				if (b == 0)
				{
					break;
				}
				list.Add(b);
			}
			return list.ToArray();
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0003B3CC File Offset: 0x000395CC
		protected static byte[] ReadBytes(BinaryReader reader, string elementName)
		{
			int count = (int)CompressedUIntReaderWriter.ReadFrom(reader, elementName + ".count");
			return reader.ReadBytes(count, elementName);
		}

		// Token: 0x04000797 RID: 1943
		private static readonly PropTypeHandler ansiStringHandler = new PropTypeHandler.AnsiStringHandler();

		// Token: 0x04000798 RID: 1944
		private static readonly PropTypeHandler ansiStringArrayHandler = new PropTypeHandler.ArrayHandler(PropTypeHandler.ansiStringHandler);

		// Token: 0x04000799 RID: 1945
		private static readonly PropTypeHandler stringHandler = new PropTypeHandler.StringHandler();

		// Token: 0x0400079A RID: 1946
		private static readonly PropTypeHandler stringArrayHandler = new PropTypeHandler.ArrayHandler(PropTypeHandler.stringHandler);

		// Token: 0x0400079B RID: 1947
		private static readonly PropTypeHandler booleanHandler = new PropTypeHandler.BooleanHandler();

		// Token: 0x0400079C RID: 1948
		private static readonly PropTypeHandler booleanArrayHandler = new PropTypeHandler.ArrayHandler(PropTypeHandler.booleanHandler);

		// Token: 0x0400079D RID: 1949
		private static readonly PropTypeHandler intHandler = new PropTypeHandler.IntHandler();

		// Token: 0x0400079E RID: 1950
		private static readonly PropTypeHandler intArrayHandler = new PropTypeHandler.ArrayHandler(PropTypeHandler.intHandler);

		// Token: 0x0400079F RID: 1951
		private static readonly PropTypeHandler guidHandler = new PropTypeHandler.GuidHandler();

		// Token: 0x040007A0 RID: 1952
		private static readonly PropTypeHandler guidArrayHandler = new PropTypeHandler.ArrayHandler(PropTypeHandler.guidHandler);

		// Token: 0x040007A1 RID: 1953
		private static readonly PropTypeHandler binaryHandler = new PropTypeHandler.BinaryHandler();

		// Token: 0x040007A2 RID: 1954
		private static readonly PropTypeHandler binaryArrayHandler = new PropTypeHandler.ArrayHandler(PropTypeHandler.binaryHandler);

		// Token: 0x040007A3 RID: 1955
		private static readonly PropTypeHandler sysTimeHandler = new PropTypeHandler.SysTimeHandler();

		// Token: 0x040007A4 RID: 1956
		private static readonly PropTypeHandler sysTimeArrayHandler = new PropTypeHandler.ArrayHandler(PropTypeHandler.sysTimeHandler);

		// Token: 0x040007A5 RID: 1957
		private static readonly PropTypeHandler none = new PropTypeHandler.NoHandler();

		// Token: 0x02000165 RID: 357
		private sealed class ArrayHandler : PropTypeHandler
		{
			// Token: 0x06000E36 RID: 3638 RVA: 0x0003B4C2 File Offset: 0x000396C2
			public ArrayHandler(PropTypeHandler singleValueHandler)
			{
				this.singleValueHandler = singleValueHandler;
			}

			// Token: 0x06000E37 RID: 3639 RVA: 0x0003B4D4 File Offset: 0x000396D4
			public override object ReadFrom(BinaryReader reader, string elementName)
			{
				int num = (int)CompressedUIntReaderWriter.ReadFrom(reader, elementName);
				object[] array = new object[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = this.singleValueHandler.ReadFrom(reader, elementName);
				}
				return array;
			}

			// Token: 0x06000E38 RID: 3640 RVA: 0x0003B510 File Offset: 0x00039710
			public override void WriteTo(BinaryWriter writer, object value)
			{
				Array array = (Array)value;
				CompressedUIntReaderWriter.WriteTo(writer, (uint)array.Length);
				foreach (object value2 in array)
				{
					this.singleValueHandler.WriteTo(writer, value2);
				}
			}

			// Token: 0x06000E39 RID: 3641 RVA: 0x0003B578 File Offset: 0x00039778
			public override void AppendText(StringBuilder text, object value)
			{
				text.Append("{");
				bool flag = true;
				Array array = (Array)value;
				foreach (object value2 in array)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						text.Append(",");
					}
					this.singleValueHandler.AppendText(text, value2);
				}
				text.Append("}");
			}

			// Token: 0x040007A6 RID: 1958
			private PropTypeHandler singleValueHandler;
		}

		// Token: 0x02000166 RID: 358
		private sealed class AnsiStringHandler : PropTypeHandler
		{
			// Token: 0x06000E3A RID: 3642 RVA: 0x0003B604 File Offset: 0x00039804
			public override object ReadFrom(BinaryReader reader, string elementName)
			{
				return PropTypeHandler.ReadStringBytes(reader, elementName);
			}

			// Token: 0x06000E3B RID: 3643 RVA: 0x0003B60D File Offset: 0x0003980D
			public override void WriteTo(BinaryWriter writer, object value)
			{
				writer.Write((byte[])value);
				writer.Write(0);
			}

			// Token: 0x06000E3C RID: 3644 RVA: 0x0003B622 File Offset: 0x00039822
			public override void AppendText(StringBuilder text, object value)
			{
				text.Append("'");
				text.Append(Encoding.ASCII.GetString((byte[])value));
				text.Append("'");
			}
		}

		// Token: 0x02000167 RID: 359
		private sealed class StringHandler : PropTypeHandler
		{
			// Token: 0x06000E3E RID: 3646 RVA: 0x0003B65B File Offset: 0x0003985B
			public override object ReadFrom(BinaryReader reader, string elementName)
			{
				return Encoding.UTF8.GetString(PropTypeHandler.ReadStringBytes(reader, elementName));
			}

			// Token: 0x06000E3F RID: 3647 RVA: 0x0003B66E File Offset: 0x0003986E
			public override void WriteTo(BinaryWriter writer, object value)
			{
				writer.Write(Encoding.UTF8.GetBytes((string)value));
				writer.Write(0);
			}

			// Token: 0x06000E40 RID: 3648 RVA: 0x0003B68D File Offset: 0x0003988D
			public override void AppendText(StringBuilder text, object value)
			{
				text.Append("'");
				text.Append((string)value);
				text.Append("'");
			}
		}

		// Token: 0x02000168 RID: 360
		private sealed class IntHandler : PropTypeHandler
		{
			// Token: 0x06000E42 RID: 3650 RVA: 0x0003B6BC File Offset: 0x000398BC
			public override object ReadFrom(BinaryReader reader, string elementName)
			{
				return (int)CompressedUIntReaderWriter.ReadFrom(reader, elementName);
			}

			// Token: 0x06000E43 RID: 3651 RVA: 0x0003B6CA File Offset: 0x000398CA
			public override void WriteTo(BinaryWriter writer, object value)
			{
				CompressedUIntReaderWriter.WriteTo(writer, (uint)((int)value));
			}

			// Token: 0x06000E44 RID: 3652 RVA: 0x0003B6D8 File Offset: 0x000398D8
			public override void AppendText(StringBuilder text, object value)
			{
				text.Append(((int)value).ToString());
			}
		}

		// Token: 0x02000169 RID: 361
		private sealed class BooleanHandler : PropTypeHandler
		{
			// Token: 0x06000E46 RID: 3654 RVA: 0x0003B702 File Offset: 0x00039902
			public override object ReadFrom(BinaryReader reader, string elementName)
			{
				return reader.ReadByte(elementName) != 0;
			}

			// Token: 0x06000E47 RID: 3655 RVA: 0x0003B716 File Offset: 0x00039916
			public override void WriteTo(BinaryWriter writer, object value)
			{
				writer.Write(((bool)value) ? 1 : 0);
			}

			// Token: 0x06000E48 RID: 3656 RVA: 0x0003B72C File Offset: 0x0003992C
			public override void AppendText(StringBuilder text, object value)
			{
				text.Append(((bool)value).ToString());
			}
		}

		// Token: 0x0200016A RID: 362
		private sealed class BinaryHandler : PropTypeHandler
		{
			// Token: 0x06000E4A RID: 3658 RVA: 0x0003B756 File Offset: 0x00039956
			public override object ReadFrom(BinaryReader reader, string elementName)
			{
				return PropTypeHandler.ReadBytes(reader, elementName);
			}

			// Token: 0x06000E4B RID: 3659 RVA: 0x0003B75F File Offset: 0x0003995F
			public override void WriteTo(BinaryWriter writer, object value)
			{
				PropTypeHandler.WriteBytes(writer, (byte[])value);
			}

			// Token: 0x06000E4C RID: 3660 RVA: 0x0003B76D File Offset: 0x0003996D
			public override void AppendText(StringBuilder text, object value)
			{
				text.Append(BitConverter.ToString((byte[])value).Replace("-", string.Empty));
			}
		}

		// Token: 0x0200016B RID: 363
		private sealed class GuidHandler : PropTypeHandler
		{
			// Token: 0x06000E4E RID: 3662 RVA: 0x0003B798 File Offset: 0x00039998
			public override object ReadFrom(BinaryReader reader, string elementName)
			{
				long position = reader.BaseStream.Position;
				byte[] array = PropTypeHandler.ReadBytes(reader, elementName);
				if (array.Length != 16)
				{
					throw new InvalidDataException(string.Format("Unable to read element '{0}' from position {1} because byte array is different size than expected for a GUID (16 bytes). Size read was: {2}", elementName, position, array.Length));
				}
				return new Guid(array);
			}

			// Token: 0x06000E4F RID: 3663 RVA: 0x0003B7EC File Offset: 0x000399EC
			public override void WriteTo(BinaryWriter writer, object value)
			{
				PropTypeHandler.WriteBytes(writer, ((Guid)value).ToByteArray());
			}

			// Token: 0x06000E50 RID: 3664 RVA: 0x0003B810 File Offset: 0x00039A10
			public override void AppendText(StringBuilder text, object value)
			{
				text.Append(((Guid)value).ToString());
			}
		}

		// Token: 0x0200016C RID: 364
		private sealed class SysTimeHandler : PropTypeHandler
		{
			// Token: 0x06000E52 RID: 3666 RVA: 0x0003B840 File Offset: 0x00039A40
			public override object ReadFrom(BinaryReader reader, string elementName)
			{
				return DateTime.FromFileTimeUtc((long)reader.ReadUInt64(elementName));
			}

			// Token: 0x06000E53 RID: 3667 RVA: 0x0003B854 File Offset: 0x00039A54
			public override void WriteTo(BinaryWriter writer, object value)
			{
				writer.Write((ulong)((DateTime)value).ToFileTimeUtc());
			}

			// Token: 0x06000E54 RID: 3668 RVA: 0x0003B878 File Offset: 0x00039A78
			public override void AppendText(StringBuilder text, object value)
			{
				text.Append(((DateTime)value).ToString());
			}
		}

		// Token: 0x0200016D RID: 365
		private sealed class NoHandler : PropTypeHandler
		{
			// Token: 0x1700036A RID: 874
			// (get) Token: 0x06000E56 RID: 3670 RVA: 0x0003B8A8 File Offset: 0x00039AA8
			public override bool IsWritable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000E57 RID: 3671 RVA: 0x0003B8AB File Offset: 0x00039AAB
			public override object ReadFrom(BinaryReader reader, string elementName)
			{
				return null;
			}

			// Token: 0x06000E58 RID: 3672 RVA: 0x0003B8AE File Offset: 0x00039AAE
			public override void WriteTo(BinaryWriter writer, object value)
			{
			}

			// Token: 0x06000E59 RID: 3673 RVA: 0x0003B8B0 File Offset: 0x00039AB0
			public override void AppendText(StringBuilder text, object value)
			{
				text.Append("unknown");
			}
		}
	}
}
