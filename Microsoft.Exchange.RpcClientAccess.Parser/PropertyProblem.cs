using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020001F6 RID: 502
	internal struct PropertyProblem
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00020AEC File Offset: 0x0001ECEC
		public ushort Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00020AF4 File Offset: 0x0001ECF4
		public PropertyTag PropertyTag
		{
			get
			{
				return this.propertyTag;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x00020AFC File Offset: 0x0001ECFC
		public ErrorCode ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00020B04 File Offset: 0x0001ED04
		public PropertyProblem(ushort index, PropertyTag propertyTag, ErrorCode errorCode)
		{
			this.index = index;
			this.propertyTag = propertyTag;
			this.errorCode = errorCode;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00020B1B File Offset: 0x0001ED1B
		public override string ToString()
		{
			return string.Format("{{Index: {0}, PropertyTag: {1}, ErrorCode: {2}}}", this.index, this.propertyTag, this.errorCode);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00020B48 File Offset: 0x0001ED48
		internal static PropertyProblem Parse(Reader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return new PropertyProblem(reader.ReadUInt16(), new PropertyTag(reader.ReadUInt32()), (ErrorCode)reader.ReadUInt32());
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00020B74 File Offset: 0x0001ED74
		internal void Serialize(Writer writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteUInt16(this.index);
			writer.WritePropertyTag(this.propertyTag);
			writer.WriteUInt32((uint)this.errorCode);
		}

		// Token: 0x0400051D RID: 1309
		internal static readonly uint MinimumSize = 10U;

		// Token: 0x0400051E RID: 1310
		private readonly ushort index;

		// Token: 0x0400051F RID: 1311
		private readonly PropertyTag propertyTag;

		// Token: 0x04000520 RID: 1312
		private readonly ErrorCode errorCode;
	}
}
