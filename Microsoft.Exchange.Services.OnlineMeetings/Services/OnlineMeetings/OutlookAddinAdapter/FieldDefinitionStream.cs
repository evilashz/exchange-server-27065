using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000BB RID: 187
	internal class FieldDefinitionStream
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x0000AD1C File Offset: 0x00008F1C
		internal FieldDefinitionStream(string fieldName)
		{
			this.NmidName = fieldName;
			this.NmidNameLength = (ushort)fieldName.Length;
			this.NameAnsi = fieldName;
			this.SkipBlocks.Add(fieldName);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000ADA4 File Offset: 0x00008FA4
		private FieldDefinitionStream()
		{
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000AE04 File Offset: 0x00009004
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x0000AE0C File Offset: 0x0000900C
		internal FieldDefinitionStreamFlags Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000AE15 File Offset: 0x00009015
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x0000AE1D File Offset: 0x0000901D
		internal VarEnum Vt
		{
			get
			{
				return this.vt;
			}
			set
			{
				this.vt = value;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000AE26 File Offset: 0x00009026
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x0000AE2E File Offset: 0x0000902E
		internal uint DispId
		{
			get
			{
				return this.dispId;
			}
			set
			{
				this.dispId = value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000AE37 File Offset: 0x00009037
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x0000AE3F File Offset: 0x0000903F
		internal ushort NmidNameLength
		{
			get
			{
				return this.nmidNameLength;
			}
			set
			{
				this.nmidNameLength = value;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000AE48 File Offset: 0x00009048
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x0000AE50 File Offset: 0x00009050
		internal string NmidName
		{
			get
			{
				return this.nmidName;
			}
			set
			{
				this.nmidName = value;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000AE59 File Offset: 0x00009059
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x0000AE61 File Offset: 0x00009061
		internal string NameAnsi
		{
			get
			{
				return this.nameAnsi;
			}
			set
			{
				this.nameAnsi = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000AE6A File Offset: 0x0000906A
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x0000AE72 File Offset: 0x00009072
		internal string FormulaAnsi
		{
			get
			{
				return this.formulaAnsi;
			}
			set
			{
				this.formulaAnsi = value;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000AE7B File Offset: 0x0000907B
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x0000AE83 File Offset: 0x00009083
		internal string ValidationRuleAnsi
		{
			get
			{
				return this.validationRuleAnsi;
			}
			set
			{
				this.validationRuleAnsi = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000AE8C File Offset: 0x0000908C
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x0000AE94 File Offset: 0x00009094
		internal string ValidationTextAnsi
		{
			get
			{
				return this.validationTextAnsi;
			}
			set
			{
				this.validationTextAnsi = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000AE9D File Offset: 0x0000909D
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x0000AEA5 File Offset: 0x000090A5
		internal string ErrorAnsi
		{
			get
			{
				return this.errorAnsi;
			}
			set
			{
				this.errorAnsi = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0000AEAE File Offset: 0x000090AE
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x0000AEB6 File Offset: 0x000090B6
		internal int InternalType
		{
			get
			{
				return this.internalType;
			}
			set
			{
				this.internalType = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000AEBF File Offset: 0x000090BF
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x0000AEC7 File Offset: 0x000090C7
		internal List<string> SkipBlocks
		{
			get
			{
				return this.skipBlocks;
			}
			private set
			{
				this.skipBlocks = value;
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000AED0 File Offset: 0x000090D0
		internal static FieldDefinitionStream Read(BinaryReader reader, PropertyDefinitionStreamVersion version)
		{
			if (version != PropertyDefinitionStreamVersion.PropDefV1)
			{
				return FieldDefinitionStream.ReadVersion103(reader);
			}
			return FieldDefinitionStream.ReadVersion102(reader);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000AEE8 File Offset: 0x000090E8
		internal void Write(BinaryWriter writer)
		{
			writer.Write((uint)this.Flags);
			writer.Write((ushort)this.Vt);
			writer.Write(this.DispId);
			writer.Write(this.NmidNameLength);
			writer.Write(FieldDefinitionStream.UnicodeEncoder.GetBytes(this.NmidName));
			FieldDefinitionStream.WriteString(writer, this.NameAnsi, false);
			FieldDefinitionStream.WriteString(writer, this.FormulaAnsi, false);
			FieldDefinitionStream.WriteString(writer, this.ValidationRuleAnsi, false);
			FieldDefinitionStream.WriteString(writer, this.ValidationTextAnsi, false);
			FieldDefinitionStream.WriteString(writer, this.ErrorAnsi, false);
			writer.Write(this.InternalType);
			FieldDefinitionStream.WriteSkipBlocks(writer, this.SkipBlocks);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000AF98 File Offset: 0x00009198
		private static FieldDefinitionStream ReadVersion102(BinaryReader reader)
		{
			FieldDefinitionStream fieldDefinitionStream = new FieldDefinitionStream();
			fieldDefinitionStream.Flags = (FieldDefinitionStreamFlags)reader.ReadUInt32();
			fieldDefinitionStream.Vt = (VarEnum)reader.ReadInt16();
			fieldDefinitionStream.DispId = reader.ReadUInt32();
			fieldDefinitionStream.NmidNameLength = reader.ReadUInt16();
			byte[] array = reader.ReadBytes((int)(fieldDefinitionStream.NmidNameLength * 2));
			fieldDefinitionStream.NmidName = FieldDefinitionStream.UnicodeEncoder.GetString(array, 0, array.Length);
			fieldDefinitionStream.NameAnsi = FieldDefinitionStream.ReadString(reader, false);
			fieldDefinitionStream.FormulaAnsi = FieldDefinitionStream.ReadString(reader, false);
			fieldDefinitionStream.ValidationRuleAnsi = FieldDefinitionStream.ReadString(reader, false);
			fieldDefinitionStream.ValidationTextAnsi = FieldDefinitionStream.ReadString(reader, false);
			fieldDefinitionStream.ErrorAnsi = FieldDefinitionStream.ReadString(reader, false);
			return fieldDefinitionStream;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000B044 File Offset: 0x00009244
		private static FieldDefinitionStream ReadVersion103(BinaryReader reader)
		{
			FieldDefinitionStream fieldDefinitionStream = FieldDefinitionStream.ReadVersion102(reader);
			fieldDefinitionStream.InternalType = reader.ReadInt32();
			fieldDefinitionStream.SkipBlocks = FieldDefinitionStream.ReadSkipBlocks(reader);
			return fieldDefinitionStream;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000B074 File Offset: 0x00009274
		private static string ReadString(BinaryReader reader, bool isUnicode)
		{
			ushort num = (ushort)reader.ReadByte();
			if (num == 255)
			{
				num = reader.ReadUInt16();
			}
			if (isUnicode)
			{
				byte[] array = reader.ReadBytes((int)(num * 2));
				return FieldDefinitionStream.UnicodeEncoder.GetString(array, 0, array.Length);
			}
			byte[] array2 = reader.ReadBytes((int)num);
			return FieldDefinitionStream.AsciiEncoder.GetString(array2, 0, array2.Length);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000B0CC File Offset: 0x000092CC
		private static void WriteString(BinaryWriter writer, string str, bool isUnicode)
		{
			if (string.IsNullOrEmpty(str))
			{
				writer.Write(0);
				return;
			}
			if (str.Length >= 255)
			{
				writer.Write(byte.MaxValue);
				writer.Write((ushort)str.Length);
			}
			else
			{
				writer.Write((byte)str.Length);
			}
			if (isUnicode)
			{
				writer.Write(FieldDefinitionStream.UnicodeEncoder.GetBytes(str));
				return;
			}
			writer.Write(FieldDefinitionStream.AsciiEncoder.GetBytes(str));
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000B144 File Offset: 0x00009344
		private static List<string> ReadSkipBlocks(BinaryReader reader)
		{
			int num = reader.ReadInt32();
			List<string> list = new List<string>();
			while (num != 0)
			{
				list.Add(FieldDefinitionStream.ReadString(reader, true));
				num = reader.ReadInt32();
			}
			return list;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000B178 File Offset: 0x00009378
		private static void WriteSkipBlocks(BinaryWriter writer, List<string> skipBlocks)
		{
			foreach (string text in skipBlocks)
			{
				int value = text.Length * 2 + ((text.Length > 255) ? 2 : 1);
				writer.Write(value);
				FieldDefinitionStream.WriteString(writer, text, true);
			}
			writer.Write(0);
		}

		// Token: 0x040002F2 RID: 754
		private static readonly UnicodeEncoding UnicodeEncoder = new UnicodeEncoding();

		// Token: 0x040002F3 RID: 755
		private static readonly Encoding AsciiEncoder = CTSGlobals.AsciiEncoding;

		// Token: 0x040002F4 RID: 756
		private FieldDefinitionStreamFlags flags;

		// Token: 0x040002F5 RID: 757
		private VarEnum vt;

		// Token: 0x040002F6 RID: 758
		private uint dispId;

		// Token: 0x040002F7 RID: 759
		private ushort nmidNameLength;

		// Token: 0x040002F8 RID: 760
		private string nmidName = string.Empty;

		// Token: 0x040002F9 RID: 761
		private string nameAnsi = string.Empty;

		// Token: 0x040002FA RID: 762
		private string formulaAnsi = string.Empty;

		// Token: 0x040002FB RID: 763
		private string validationRuleAnsi = string.Empty;

		// Token: 0x040002FC RID: 764
		private string validationTextAnsi = string.Empty;

		// Token: 0x040002FD RID: 765
		private string errorAnsi = string.Empty;

		// Token: 0x040002FE RID: 766
		private int internalType;

		// Token: 0x040002FF RID: 767
		private List<string> skipBlocks = new List<string>();
	}
}
