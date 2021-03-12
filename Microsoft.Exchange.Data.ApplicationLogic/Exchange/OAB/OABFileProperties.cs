using System;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x0200015D RID: 349
	internal sealed class OABFileProperties : IWriteToBinaryWriter
	{
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x0003A894 File Offset: 0x00038A94
		// (set) Token: 0x06000DFA RID: 3578 RVA: 0x0003A89C File Offset: 0x00038A9C
		public OABPropertyDescriptor[] HeaderProperties { get; set; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x0003A8A5 File Offset: 0x00038AA5
		// (set) Token: 0x06000DFC RID: 3580 RVA: 0x0003A8AD File Offset: 0x00038AAD
		public OABPropertyDescriptor[] DetailProperties { get; set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x0003A8B8 File Offset: 0x00038AB8
		public int Size
		{
			get
			{
				int num = (this.HeaderProperties != null) ? this.HeaderProperties.Length : 0;
				int num2 = (this.DetailProperties != null) ? this.DetailProperties.Length : 0;
				return 8 + num * OABPropertyDescriptor.Size + 4 + num2 * OABPropertyDescriptor.Size;
			}
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0003A900 File Offset: 0x00038B00
		public static OABFileProperties ReadFrom(BinaryReader reader, string elementName)
		{
			long position = reader.BaseStream.Position;
			int num = (int)reader.ReadUInt32(elementName + ".size");
			OABPropertyDescriptor[] headerProperties = OABFileProperties.ReadProperties(reader, elementName + ".headerProperties");
			OABPropertyDescriptor[] detailProperties = OABFileProperties.ReadProperties(reader, elementName + ".detailProperties");
			long position2 = reader.BaseStream.Position;
			int num2 = (int)(position2 - position);
			if (num2 != num)
			{
				throw new InvalidDataException(string.Format("Unable to read element '{0}' at position {1} because number of bytes read from stream doesn't match in size in header. Size in header: {2}, bytes read from stream: {3}", new object[]
				{
					elementName,
					position,
					num,
					num2
				}));
			}
			return new OABFileProperties
			{
				HeaderProperties = headerProperties,
				DetailProperties = detailProperties
			};
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0003A9BF File Offset: 0x00038BBF
		public void WriteTo(BinaryWriter writer)
		{
			writer.Write((uint)this.Size);
			OABFileProperties.WriteProperties(writer, this.HeaderProperties);
			OABFileProperties.WriteProperties(writer, this.DetailProperties);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0003A9E8 File Offset: 0x00038BE8
		private static OABPropertyDescriptor[] ReadProperties(BinaryReader reader, string elementName)
		{
			int num = (int)reader.ReadUInt32(elementName + ".count");
			OABPropertyDescriptor[] array = new OABPropertyDescriptor[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = OABPropertyDescriptor.ReadFrom(reader, string.Concat(new object[]
				{
					elementName,
					"[",
					i,
					"]"
				}));
			}
			return array;
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0003AA50 File Offset: 0x00038C50
		private static void WriteProperties(BinaryWriter writer, OABPropertyDescriptor[] properties)
		{
			if (properties != null)
			{
				writer.Write((uint)properties.Length);
				foreach (OABPropertyDescriptor oabpropertyDescriptor in properties)
				{
					oabpropertyDescriptor.WriteTo(writer);
				}
				return;
			}
			writer.Write(0U);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0003AA8C File Offset: 0x00038C8C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(200);
			if (this.HeaderProperties != null)
			{
				stringBuilder.AppendLine("HeaderProperties:");
				foreach (OABPropertyDescriptor oabpropertyDescriptor in this.HeaderProperties)
				{
					stringBuilder.Append("  ");
					stringBuilder.AppendLine(oabpropertyDescriptor.ToString());
				}
			}
			if (this.DetailProperties != null)
			{
				stringBuilder.AppendLine("DetailProperties:");
				foreach (OABPropertyDescriptor oabpropertyDescriptor2 in this.DetailProperties)
				{
					stringBuilder.Append("  ");
					stringBuilder.AppendLine(oabpropertyDescriptor2.ToString());
				}
			}
			return stringBuilder.ToString();
		}
	}
}
