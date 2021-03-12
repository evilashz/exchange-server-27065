using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000525 RID: 1317
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class PersonId : StoreId, IEquatable<PersonId>
	{
		// Token: 0x060038C1 RID: 14529 RVA: 0x000E8DB0 File Offset: 0x000E6FB0
		private PersonId(byte[] bytes)
		{
			Util.ThrowOnNullArgument(bytes, "bytes");
			Util.ThrowOnArgumentOutOfRangeOnLessThan(bytes.Length, 16, "bytes");
			Util.ThrowOnArgumentInvalidOnGreaterThan(bytes.Length, 16, "bytes");
			this.bytes = bytes;
			this.hashCode = PersonId.ComputeHashCode(bytes);
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x000E8E00 File Offset: 0x000E7000
		public static PersonId CreateNew()
		{
			return new PersonId(Guid.NewGuid().ToByteArray());
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x000E8E1F File Offset: 0x000E701F
		public static PersonId Create(string base64String)
		{
			return new PersonId(StoreId.Base64ToByteArray(base64String));
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x000E8E2C File Offset: 0x000E702C
		public static PersonId Create(byte[] bytes)
		{
			return new PersonId(PersonId.CloneBytes(bytes));
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x000E8E39 File Offset: 0x000E7039
		public static long TraceId(PersonId personId)
		{
			return (long)((personId == null) ? 0 : personId.GetHashCode());
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x000E8E48 File Offset: 0x000E7048
		public override string ToBase64String()
		{
			return Convert.ToBase64String(this.bytes);
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x000E8E55 File Offset: 0x000E7055
		public override byte[] GetBytes()
		{
			return PersonId.CloneBytes(this.bytes);
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x000E8E62 File Offset: 0x000E7062
		public override bool Equals(object other)
		{
			return this.Equals(other as PersonId);
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x000E8E70 File Offset: 0x000E7070
		public override bool Equals(StoreId other)
		{
			return this.Equals(other as PersonId);
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x000E8E7E File Offset: 0x000E707E
		public bool Equals(PersonId other)
		{
			return other != null && (object.ReferenceEquals(this, other) || ArrayComparer<byte>.Comparer.Equals(this.bytes, other.bytes));
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x000E8EA6 File Offset: 0x000E70A6
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x000E8EAE File Offset: 0x000E70AE
		public override string ToString()
		{
			return this.ToBase64String();
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x000E8EB8 File Offset: 0x000E70B8
		private static int ComputeHashCode(byte[] bytes)
		{
			int num = 0;
			for (int i = 0; i < bytes.Length; i++)
			{
				num ^= (int)bytes[i] << 8 * (i % 4);
			}
			return num;
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x000E8EE8 File Offset: 0x000E70E8
		private static byte[] CloneBytes(byte[] bytes)
		{
			byte[] array = new byte[bytes.Length];
			bytes.CopyTo(array, 0);
			return array;
		}

		// Token: 0x04001E18 RID: 7704
		private const int BytesInPersonId = 16;

		// Token: 0x04001E19 RID: 7705
		private readonly byte[] bytes;

		// Token: 0x04001E1A RID: 7706
		private readonly int hashCode;
	}
}
