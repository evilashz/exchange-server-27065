using System;
using Microsoft.Exchange.Data.Serialization;

namespace Microsoft.Exchange.Data.MailboxSignature
{
	// Token: 0x02000236 RID: 566
	internal class MailboxSignatureSectionMetadata
	{
		// Token: 0x06001388 RID: 5000 RVA: 0x0003BCD0 File Offset: 0x00039ED0
		internal MailboxSignatureSectionMetadata(MailboxSignatureSectionType type, short version, int elementsNumber, int length)
		{
			this.type = type;
			this.version = version;
			this.elementsNumber = elementsNumber;
			this.length = length;
			this.Validate();
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x0003BCFB File Offset: 0x00039EFB
		internal MailboxSignatureSectionType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x0003BD03 File Offset: 0x00039F03
		internal short Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x0003BD0B File Offset: 0x00039F0B
		internal int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x0003BD13 File Offset: 0x00039F13
		internal int ElementsNumber
		{
			get
			{
				return this.elementsNumber;
			}
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0003BD1C File Offset: 0x00039F1C
		internal static MailboxSignatureSectionMetadata Parse(byte[] buffer, ref int offset)
		{
			if (offset > buffer.Length || buffer.Length - offset < 12)
			{
				throw new ArgumentException("Parse: insufficient buffer.");
			}
			return new MailboxSignatureSectionMetadata((MailboxSignatureSectionType)Serialization.DeserializeUInt16(buffer, ref offset), (short)Serialization.DeserializeUInt16(buffer, ref offset), (int)Serialization.DeserializeUInt32(buffer, ref offset), (int)Serialization.DeserializeUInt32(buffer, ref offset));
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0003BD68 File Offset: 0x00039F68
		internal int Serialize(byte[] buffer, int offset)
		{
			if (buffer == null)
			{
				return 12;
			}
			if (offset > buffer.Length || buffer.Length - offset < 12)
			{
				throw new ArgumentException("Serialize: insufficient buffer.");
			}
			Serialization.SerializeUInt16(buffer, ref offset, (ushort)this.type);
			Serialization.SerializeUInt16(buffer, ref offset, (ushort)this.version);
			Serialization.SerializeUInt32(buffer, ref offset, (uint)this.elementsNumber);
			Serialization.SerializeUInt32(buffer, ref offset, (uint)this.length);
			return 12;
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0003BDD4 File Offset: 0x00039FD4
		private void Validate()
		{
			if (this.elementsNumber < 0)
			{
				throw new ArgumentException("Invalid number of section elements.");
			}
			if (this.length < 0)
			{
				throw new ArgumentException("Invalid section length.");
			}
			if (this.length != 0 && this.elementsNumber == 0)
			{
				throw new ArgumentException("Zero elements serialized to non-zero length.");
			}
		}

		// Token: 0x04000B71 RID: 2929
		public const int SerializedSize = 12;

		// Token: 0x04000B72 RID: 2930
		private readonly MailboxSignatureSectionType type;

		// Token: 0x04000B73 RID: 2931
		private readonly short version;

		// Token: 0x04000B74 RID: 2932
		private readonly int elementsNumber;

		// Token: 0x04000B75 RID: 2933
		private readonly int length;
	}
}
