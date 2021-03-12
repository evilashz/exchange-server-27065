using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200085A RID: 2138
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RecipientId : IEquatable<RecipientId>
	{
		// Token: 0x0600506A RID: 20586 RVA: 0x0014E33F File Offset: 0x0014C53F
		internal RecipientId(byte[] id)
		{
			this.id = id;
		}

		// Token: 0x0600506B RID: 20587 RVA: 0x0014E350 File Offset: 0x0014C550
		public override bool Equals(object obj)
		{
			RecipientId recipId = obj as RecipientId;
			return this.Equals(recipId);
		}

		// Token: 0x0600506C RID: 20588 RVA: 0x0014E36B File Offset: 0x0014C56B
		public bool Equals(RecipientId recipId)
		{
			return recipId != null && ArrayComparer<byte>.Comparer.Equals(this.id, recipId.id);
		}

		// Token: 0x0600506D RID: 20589 RVA: 0x0014E388 File Offset: 0x0014C588
		public override int GetHashCode()
		{
			return this.id.GetHashCode();
		}

		// Token: 0x0600506E RID: 20590 RVA: 0x0014E395 File Offset: 0x0014C595
		public string GetBase64()
		{
			return Convert.ToBase64String(this.id);
		}

		// Token: 0x0600506F RID: 20591 RVA: 0x0014E3A2 File Offset: 0x0014C5A2
		public byte[] GetBytes()
		{
			return (byte[])this.id.Clone();
		}

		// Token: 0x04002C20 RID: 11296
		private byte[] id;
	}
}
