using System;

namespace Microsoft.Exchange.Transport.MessageDepot
{
	// Token: 0x02000003 RID: 3
	internal sealed class AcquireToken : IEquatable<AcquireToken>
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002118 File Offset: 0x00000318
		public static bool operator ==(AcquireToken obj1, AcquireToken obj2)
		{
			return object.ReferenceEquals(obj1, obj2) || (!object.ReferenceEquals(obj1, null) && !object.ReferenceEquals(obj2, null) && obj1.tokenId.Equals(obj2.tokenId));
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002158 File Offset: 0x00000358
		public static bool operator !=(AcquireToken obj1, AcquireToken obj2)
		{
			return !(obj1 == obj2);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002164 File Offset: 0x00000364
		public bool Equals(AcquireToken other)
		{
			return this == other;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000216D File Offset: 0x0000036D
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AcquireToken);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000217C File Offset: 0x0000037C
		public override string ToString()
		{
			return this.tokenId.ToString();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021A0 File Offset: 0x000003A0
		public override int GetHashCode()
		{
			return this.tokenId.GetHashCode();
		}

		// Token: 0x04000003 RID: 3
		private readonly Guid tokenId = Guid.NewGuid();
	}
}
