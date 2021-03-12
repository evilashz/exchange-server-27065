using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000223 RID: 547
	internal sealed class SuccessfulAddressTypesResult : RopResult
	{
		// Token: 0x06000BF8 RID: 3064 RVA: 0x00026696 File Offset: 0x00024896
		internal SuccessfulAddressTypesResult(string[] addressTypes) : base(RopId.AddressTypes, ErrorCode.None, null)
		{
			if (addressTypes == null)
			{
				throw new ArgumentNullException("addressTypes");
			}
			this.addressTypes = addressTypes;
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x000266B8 File Offset: 0x000248B8
		internal SuccessfulAddressTypesResult(Reader reader) : base(reader)
		{
			ushort num = reader.ReadUInt16();
			ushort estimateCount = reader.ReadUInt16();
			reader.CheckBoundary((uint)num, 1U);
			reader.CheckBoundary((uint)estimateCount, 1U);
			this.addressTypes = new string[(int)num];
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				this.addressTypes[(int)num2] = reader.ReadAsciiString(StringFlags.IncludeNull);
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x00026712 File Offset: 0x00024912
		internal string[] AddressTypes
		{
			get
			{
				return this.addressTypes;
			}
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0002671A File Offset: 0x0002491A
		internal static SuccessfulAddressTypesResult Parse(Reader reader)
		{
			return new SuccessfulAddressTypesResult(reader);
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00026724 File Offset: 0x00024924
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt16((ushort)this.addressTypes.Length);
			long position = writer.Position;
			writer.WriteUInt16(0);
			long position2 = writer.Position;
			foreach (string value in this.addressTypes)
			{
				writer.WriteAsciiString(value, StringFlags.IncludeNull);
			}
			long position3 = writer.Position;
			writer.Position = position;
			writer.WriteUInt16((ushort)(position3 - position2));
			writer.Position = position3;
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x000267A4 File Offset: 0x000249A4
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Types=[");
			Util.AppendToString<string>(stringBuilder, this.addressTypes);
			stringBuilder.Append("]");
		}

		// Token: 0x040006B1 RID: 1713
		private string[] addressTypes;
	}
}
