using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000219 RID: 537
	internal abstract class Result
	{
		// Token: 0x06000BAA RID: 2986 RVA: 0x0002505D File Offset: 0x0002325D
		protected Result(RopId ropId)
		{
			this.ropId = ropId;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002506C File Offset: 0x0002326C
		protected Result(Reader reader)
		{
			this.ropId = (RopId)reader.ReadByte();
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00025080 File Offset: 0x00023280
		public RopId RopId
		{
			get
			{
				return this.ropId;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x00025088 File Offset: 0x00023288
		// (set) Token: 0x06000BAE RID: 2990 RVA: 0x00025090 File Offset: 0x00023290
		public Encoding String8Encoding
		{
			get
			{
				return this.string8Encoding;
			}
			set
			{
				this.string8Encoding = value;
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00025099 File Offset: 0x00023299
		internal virtual void Serialize(Writer writer)
		{
			if (this.string8Encoding == null)
			{
				throw new InvalidOperationException("No encoding was set on this result");
			}
			writer.WriteByte((byte)this.ropId);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x000250BC File Offset: 0x000232BC
		public override bool Equals(object obj)
		{
			string message = "Results don't support Equals.  To verify equality of Results in test code, use test EqualityComparer.";
			throw new NotSupportedException(message);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x000250D8 File Offset: 0x000232D8
		public override int GetHashCode()
		{
			string message = "Results don't support GetHashCode. Use test EqualityComparer.";
			throw new NotSupportedException(message);
		}

		// Token: 0x04000692 RID: 1682
		private readonly RopId ropId;

		// Token: 0x04000693 RID: 1683
		private Encoding string8Encoding;
	}
}
