using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000EC RID: 236
	public struct Property
	{
		// Token: 0x06000959 RID: 2393 RVA: 0x0002BB65 File Offset: 0x00029D65
		public Property(StorePropTag tag, object value)
		{
			this.tag = tag;
			this.value = value;
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0002BB78 File Offset: 0x00029D78
		public bool IsError
		{
			get
			{
				return this.tag.PropType == PropertyType.Error;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x0002BB97 File Offset: 0x00029D97
		public StorePropTag Tag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0002BB9F File Offset: 0x00029D9F
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0002BBA7 File Offset: 0x00029DA7
		public static Property NotFoundError(StorePropTag tag)
		{
			return new Property(tag.ConvertToError(), Property.BoxedErrorCodeNotFound);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0002BBBA File Offset: 0x00029DBA
		public static Property NotEnoughMemoryError(StorePropTag tag)
		{
			return new Property(tag.ConvertToError(), Property.BoxedErrorCodeNotEnoughMemory);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0002BBCD File Offset: 0x00029DCD
		public void AppendToString(StringBuilder sb)
		{
			sb.Append("tag:[");
			sb.AppendAsString(this.Tag);
			sb.Append("] value:[");
			sb.AppendAsString(this.Value);
			sb.Append("]");
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0002BC0C File Offset: 0x00029E0C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(30);
			this.AppendToString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x04000549 RID: 1353
		private static readonly object BoxedErrorCodeNotFound = ErrorCodeValue.NotFound;

		// Token: 0x0400054A RID: 1354
		private static readonly object BoxedErrorCodeNotEnoughMemory = ErrorCodeValue.NotEnoughMemory;

		// Token: 0x0400054B RID: 1355
		private readonly StorePropTag tag;

		// Token: 0x0400054C RID: 1356
		private readonly object value;
	}
}
