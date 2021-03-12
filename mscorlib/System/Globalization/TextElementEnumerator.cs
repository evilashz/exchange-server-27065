using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
	// Token: 0x020003A6 RID: 934
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class TextElementEnumerator : IEnumerator
	{
		// Token: 0x06003075 RID: 12405 RVA: 0x000B9800 File Offset: 0x000B7A00
		internal TextElementEnumerator(string str, int startIndex, int strLen)
		{
			this.str = str;
			this.startIndex = startIndex;
			this.strLen = strLen;
			this.Reset();
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x000B9823 File Offset: 0x000B7A23
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.charLen = -1;
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x000B982C File Offset: 0x000B7A2C
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.strLen = this.endIndex + 1;
			this.currTextElementLen = this.nextTextElementLen;
			if (this.charLen == -1)
			{
				this.uc = CharUnicodeInfo.InternalGetUnicodeCategory(this.str, this.index, out this.charLen);
			}
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000B9879 File Offset: 0x000B7A79
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.endIndex = this.strLen - 1;
			this.nextTextElementLen = this.currTextElementLen;
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x000B9898 File Offset: 0x000B7A98
		[__DynamicallyInvokable]
		public bool MoveNext()
		{
			if (this.index >= this.strLen)
			{
				this.index = this.strLen + 1;
				return false;
			}
			this.currTextElementLen = StringInfo.GetCurrentTextElementLen(this.str, this.index, this.strLen, ref this.uc, ref this.charLen);
			this.index += this.currTextElementLen;
			return true;
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x0600307A RID: 12410 RVA: 0x000B9900 File Offset: 0x000B7B00
		[__DynamicallyInvokable]
		public object Current
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetTextElement();
			}
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x000B9908 File Offset: 0x000B7B08
		[__DynamicallyInvokable]
		public string GetTextElement()
		{
			if (this.index == this.startIndex)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
			}
			if (this.index > this.strLen)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
			}
			return this.str.Substring(this.index - this.currTextElementLen, this.currTextElementLen);
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x0600307C RID: 12412 RVA: 0x000B996F File Offset: 0x000B7B6F
		[__DynamicallyInvokable]
		public int ElementIndex
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.index == this.startIndex)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
				}
				return this.index - this.currTextElementLen;
			}
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x000B999C File Offset: 0x000B7B9C
		[__DynamicallyInvokable]
		public void Reset()
		{
			this.index = this.startIndex;
			if (this.index < this.strLen)
			{
				this.uc = CharUnicodeInfo.InternalGetUnicodeCategory(this.str, this.index, out this.charLen);
			}
		}

		// Token: 0x0400146A RID: 5226
		private string str;

		// Token: 0x0400146B RID: 5227
		private int index;

		// Token: 0x0400146C RID: 5228
		private int startIndex;

		// Token: 0x0400146D RID: 5229
		[NonSerialized]
		private int strLen;

		// Token: 0x0400146E RID: 5230
		[NonSerialized]
		private int currTextElementLen;

		// Token: 0x0400146F RID: 5231
		[OptionalField(VersionAdded = 2)]
		private UnicodeCategory uc;

		// Token: 0x04001470 RID: 5232
		[OptionalField(VersionAdded = 2)]
		private int charLen;

		// Token: 0x04001471 RID: 5233
		private int endIndex;

		// Token: 0x04001472 RID: 5234
		private int nextTextElementLen;
	}
}
