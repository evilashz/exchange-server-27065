using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x020001CF RID: 463
	public abstract class HtmlTagContext
	{
		// Token: 0x0600144E RID: 5198 RVA: 0x00092417 File Offset: 0x00090617
		internal HtmlTagContext()
		{
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x0009241F File Offset: 0x0009061F
		public bool IsEndTag
		{
			get
			{
				this.AssertContextValid();
				return this.isEndTag;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x0009242D File Offset: 0x0009062D
		public bool IsEmptyElementTag
		{
			get
			{
				this.AssertContextValid();
				return this.isEmptyElementTag;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x0009243B File Offset: 0x0009063B
		public HtmlTagId TagId
		{
			get
			{
				this.AssertContextValid();
				return HtmlNameData.Names[(int)this.tagNameIndex].PublicTagId;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x00092458 File Offset: 0x00090658
		public string TagName
		{
			get
			{
				this.AssertContextValid();
				return this.GetTagNameImpl();
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x00092466 File Offset: 0x00090666
		public HtmlTagContext.AttributeCollection Attributes
		{
			get
			{
				this.AssertContextValid();
				return new HtmlTagContext.AttributeCollection(this);
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x00092474 File Offset: 0x00090674
		internal HtmlNameIndex TagNameIndex
		{
			get
			{
				this.AssertContextValid();
				return this.tagNameIndex;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x00092482 File Offset: 0x00090682
		internal HtmlTagParts TagParts
		{
			get
			{
				this.AssertContextValid();
				return this.tagParts;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x00092490 File Offset: 0x00090690
		internal bool IsInvokeCallbackForEndTag
		{
			get
			{
				return this.invokeCallbackForEndTag;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x00092498 File Offset: 0x00090698
		internal bool IsDeleteInnerContent
		{
			get
			{
				return this.deleteInnerContent;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x000924A0 File Offset: 0x000906A0
		internal bool IsDeleteEndTag
		{
			get
			{
				return this.deleteEndTag;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x000924A8 File Offset: 0x000906A8
		internal bool CopyPending
		{
			get
			{
				this.AssertContextValid();
				return this.GetCopyPendingStateImpl();
			}
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x000924B6 File Offset: 0x000906B6
		public void WriteTag()
		{
			this.WriteTag(false);
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x000924C0 File Offset: 0x000906C0
		public void WriteTag(bool copyInputAttributes)
		{
			this.AssertContextValid();
			if (this.writeState != HtmlTagContext.TagWriteState.Undefined)
			{
				throw new InvalidOperationException((this.writeState == HtmlTagContext.TagWriteState.Written) ? TextConvertersStrings.CallbackTagAlreadyWritten : TextConvertersStrings.CallbackTagAlreadyDeleted);
			}
			this.deleteEndTag = false;
			this.WriteTagImpl(!this.isEndTag && copyInputAttributes);
			this.writeState = HtmlTagContext.TagWriteState.Written;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x00092516 File Offset: 0x00090716
		public void DeleteTag()
		{
			this.DeleteTag(false);
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x00092520 File Offset: 0x00090720
		public void DeleteTag(bool keepEndTag)
		{
			this.AssertContextValid();
			if (this.writeState != HtmlTagContext.TagWriteState.Undefined)
			{
				throw new InvalidOperationException((this.writeState == HtmlTagContext.TagWriteState.Written) ? TextConvertersStrings.CallbackTagAlreadyWritten : TextConvertersStrings.CallbackTagAlreadyDeleted);
			}
			if (!this.isEndTag && !this.isEmptyElementTag)
			{
				this.deleteEndTag = !keepEndTag;
			}
			else
			{
				this.deleteEndTag = false;
			}
			this.DeleteTagImpl();
			this.writeState = HtmlTagContext.TagWriteState.Deleted;
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x00092586 File Offset: 0x00090786
		public void DeleteInnerContent()
		{
			this.AssertContextValid();
			if (!this.isEndTag && !this.isEmptyElementTag)
			{
				this.deleteInnerContent = true;
			}
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x000925A5 File Offset: 0x000907A5
		public void InvokeCallbackForEndTag()
		{
			this.AssertContextValid();
			if (!this.isEndTag && !this.isEmptyElementTag)
			{
				this.invokeCallbackForEndTag = true;
			}
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x000925C4 File Offset: 0x000907C4
		internal static byte ExtractCookie(int attributeIndexAndCookie)
		{
			return (byte)((uint)attributeIndexAndCookie >> 24);
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x000925CB File Offset: 0x000907CB
		internal static int ExtractIndex(int attributeIndexAndCookie)
		{
			return (attributeIndexAndCookie & 16777215) - 1;
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x000925D6 File Offset: 0x000907D6
		internal static int ComposeIndexAndCookie(byte cookie, int attributeIndex)
		{
			return ((int)cookie << 24) + (attributeIndex + 1);
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x000925E0 File Offset: 0x000907E0
		internal void InitializeTag(bool isEndTag, HtmlNameIndex tagNameIndex, bool droppedEndTag)
		{
			this.isEndTag = isEndTag;
			this.isEmptyElementTag = false;
			this.tagNameIndex = tagNameIndex;
			this.writeState = (droppedEndTag ? HtmlTagContext.TagWriteState.Deleted : HtmlTagContext.TagWriteState.Undefined);
			this.invokeCallbackForEndTag = false;
			this.deleteInnerContent = false;
			this.deleteEndTag = !this.isEndTag;
			this.cookie += 1;
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0009263B File Offset: 0x0009083B
		internal void InitializeFragment(bool isEmptyElementTag, int attributeCount, HtmlTagParts tagParts)
		{
			if (attributeCount >= 16777215)
			{
				throw new TextConvertersException();
			}
			this.isEmptyElementTag = isEmptyElementTag;
			this.tagParts = tagParts;
			this.attributeCount = attributeCount;
			this.cookie += 1;
			this.valid = true;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x00092676 File Offset: 0x00090876
		internal void UninitializeFragment()
		{
			this.valid = false;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0009267F File Offset: 0x0009087F
		internal virtual bool GetCopyPendingStateImpl()
		{
			return false;
		}

		// Token: 0x06001467 RID: 5223
		internal abstract string GetTagNameImpl();

		// Token: 0x06001468 RID: 5224
		internal abstract HtmlAttributeId GetAttributeNameIdImpl(int attributeIndex);

		// Token: 0x06001469 RID: 5225
		internal abstract HtmlAttributeParts GetAttributePartsImpl(int attributeIndex);

		// Token: 0x0600146A RID: 5226
		internal abstract string GetAttributeNameImpl(int attributeIndex);

		// Token: 0x0600146B RID: 5227
		internal abstract string GetAttributeValueImpl(int attributeIndex);

		// Token: 0x0600146C RID: 5228
		internal abstract int ReadAttributeValueImpl(int attributeIndex, char[] buffer, int offset, int count);

		// Token: 0x0600146D RID: 5229
		internal abstract void WriteTagImpl(bool writeAttributes);

		// Token: 0x0600146E RID: 5230 RVA: 0x00092682 File Offset: 0x00090882
		internal virtual void DeleteTagImpl()
		{
		}

		// Token: 0x0600146F RID: 5231
		internal abstract void WriteAttributeImpl(int attributeIndex, bool writeName, bool writeValue);

		// Token: 0x06001470 RID: 5232 RVA: 0x00092684 File Offset: 0x00090884
		internal void AssertAttributeValid(int attributeIndexAndCookie)
		{
			if (!this.valid)
			{
				throw new InvalidOperationException(TextConvertersStrings.AttributeNotValidInThisState);
			}
			if (HtmlTagContext.ExtractCookie(attributeIndexAndCookie) != this.cookie)
			{
				throw new InvalidOperationException(TextConvertersStrings.AttributeNotValidForThisContext);
			}
			int num = HtmlTagContext.ExtractIndex(attributeIndexAndCookie);
			if (num < 0 || num >= this.attributeCount)
			{
				throw new InvalidOperationException(TextConvertersStrings.AttributeNotValidForThisContext);
			}
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x000926DC File Offset: 0x000908DC
		internal void AssertContextValid()
		{
			if (!this.valid)
			{
				throw new InvalidOperationException(TextConvertersStrings.ContextNotValidInThisState);
			}
		}

		// Token: 0x040013DC RID: 5084
		private HtmlTagContext.TagWriteState writeState;

		// Token: 0x040013DD RID: 5085
		private byte cookie;

		// Token: 0x040013DE RID: 5086
		private bool valid;

		// Token: 0x040013DF RID: 5087
		private bool invokeCallbackForEndTag;

		// Token: 0x040013E0 RID: 5088
		private bool deleteInnerContent;

		// Token: 0x040013E1 RID: 5089
		private bool deleteEndTag;

		// Token: 0x040013E2 RID: 5090
		private bool isEndTag;

		// Token: 0x040013E3 RID: 5091
		private bool isEmptyElementTag;

		// Token: 0x040013E4 RID: 5092
		private HtmlNameIndex tagNameIndex;

		// Token: 0x040013E5 RID: 5093
		private HtmlTagParts tagParts;

		// Token: 0x040013E6 RID: 5094
		private int attributeCount;

		// Token: 0x020001D0 RID: 464
		internal enum TagWriteState
		{
			// Token: 0x040013E8 RID: 5096
			Undefined,
			// Token: 0x040013E9 RID: 5097
			Written,
			// Token: 0x040013EA RID: 5098
			Deleted
		}

		// Token: 0x020001D1 RID: 465
		public struct AttributeCollection : IEnumerable<HtmlTagContextAttribute>, IEnumerable
		{
			// Token: 0x06001472 RID: 5234 RVA: 0x000926F1 File Offset: 0x000908F1
			internal AttributeCollection(HtmlTagContext tagContext)
			{
				this.tagContext = tagContext;
			}

			// Token: 0x17000549 RID: 1353
			// (get) Token: 0x06001473 RID: 5235 RVA: 0x000926FA File Offset: 0x000908FA
			public int Count
			{
				get
				{
					this.AssertValid();
					return this.tagContext.attributeCount;
				}
			}

			// Token: 0x1700054A RID: 1354
			public HtmlTagContextAttribute this[int index]
			{
				get
				{
					this.AssertValid();
					if (index < 0 || index >= this.tagContext.attributeCount)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return new HtmlTagContextAttribute(this.tagContext, HtmlTagContext.ComposeIndexAndCookie(this.tagContext.cookie, index));
				}
			}

			// Token: 0x06001475 RID: 5237 RVA: 0x0009275C File Offset: 0x0009095C
			public HtmlTagContext.AttributeCollection.Enumerator GetEnumerator()
			{
				this.AssertValid();
				return new HtmlTagContext.AttributeCollection.Enumerator(this.tagContext);
			}

			// Token: 0x06001476 RID: 5238 RVA: 0x0009276F File Offset: 0x0009096F
			IEnumerator<HtmlTagContextAttribute> IEnumerable<HtmlTagContextAttribute>.GetEnumerator()
			{
				this.AssertValid();
				return new HtmlTagContext.AttributeCollection.Enumerator(this.tagContext);
			}

			// Token: 0x06001477 RID: 5239 RVA: 0x00092787 File Offset: 0x00090987
			IEnumerator IEnumerable.GetEnumerator()
			{
				this.AssertValid();
				return new HtmlTagContext.AttributeCollection.Enumerator(this.tagContext);
			}

			// Token: 0x06001478 RID: 5240 RVA: 0x0009279F File Offset: 0x0009099F
			private void AssertValid()
			{
				if (this.tagContext == null)
				{
					throw new InvalidOperationException(TextConvertersStrings.AttributeCollectionNotInitialized);
				}
			}

			// Token: 0x040013EB RID: 5099
			private HtmlTagContext tagContext;

			// Token: 0x020001D2 RID: 466
			public struct Enumerator : IEnumerator<HtmlTagContextAttribute>, IDisposable, IEnumerator
			{
				// Token: 0x06001479 RID: 5241 RVA: 0x000927B4 File Offset: 0x000909B4
				internal Enumerator(HtmlTagContext tagContext)
				{
					this.tagContext = tagContext;
					this.attributeIndexAndCookie = HtmlTagContext.ComposeIndexAndCookie(this.tagContext.cookie, -1);
				}

				// Token: 0x1700054B RID: 1355
				// (get) Token: 0x0600147A RID: 5242 RVA: 0x000927D4 File Offset: 0x000909D4
				public HtmlTagContextAttribute Current
				{
					get
					{
						return new HtmlTagContextAttribute(this.tagContext, this.attributeIndexAndCookie);
					}
				}

				// Token: 0x1700054C RID: 1356
				// (get) Token: 0x0600147B RID: 5243 RVA: 0x000927E7 File Offset: 0x000909E7
				object IEnumerator.Current
				{
					get
					{
						return new HtmlTagContextAttribute(this.tagContext, this.attributeIndexAndCookie);
					}
				}

				// Token: 0x0600147C RID: 5244 RVA: 0x000927FF File Offset: 0x000909FF
				public void Dispose()
				{
				}

				// Token: 0x0600147D RID: 5245 RVA: 0x00092804 File Offset: 0x00090A04
				public bool MoveNext()
				{
					if (HtmlTagContext.ExtractIndex(this.attributeIndexAndCookie) < this.tagContext.attributeCount)
					{
						this.attributeIndexAndCookie++;
						return HtmlTagContext.ExtractIndex(this.attributeIndexAndCookie) < this.tagContext.attributeCount;
					}
					return false;
				}

				// Token: 0x0600147E RID: 5246 RVA: 0x00092851 File Offset: 0x00090A51
				void IEnumerator.Reset()
				{
					this.attributeIndexAndCookie = HtmlTagContext.ComposeIndexAndCookie(this.tagContext.cookie, -1);
				}

				// Token: 0x040013EC RID: 5100
				private HtmlTagContext tagContext;

				// Token: 0x040013ED RID: 5101
				private int attributeIndexAndCookie;
			}
		}
	}
}
