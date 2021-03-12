using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x02000206 RID: 518
	internal class HtmlToken : Token
	{
		// Token: 0x0600154A RID: 5450 RVA: 0x000A5C13 File Offset: 0x000A3E13
		public HtmlToken()
		{
			this.Reset();
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x000A5C21 File Offset: 0x000A3E21
		// (set) Token: 0x0600154C RID: 5452 RVA: 0x000A5C29 File Offset: 0x000A3E29
		public HtmlTokenId HtmlTokenId
		{
			get
			{
				return (HtmlTokenId)base.TokenId;
			}
			set
			{
				base.TokenId = (TokenId)value;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x000A5C32 File Offset: 0x000A3E32
		// (set) Token: 0x0600154E RID: 5454 RVA: 0x000A5C3A File Offset: 0x000A3E3A
		public HtmlToken.TagFlags Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x000A5C43 File Offset: 0x000A3E43
		public bool IsEndTag
		{
			get
			{
				return 0 != (byte)(this.flags & HtmlToken.TagFlags.EndTag);
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x000A5C55 File Offset: 0x000A3E55
		public bool IsEmptyScope
		{
			get
			{
				return 0 != (byte)(this.flags & HtmlToken.TagFlags.EmptyScope);
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x000A5C67 File Offset: 0x000A3E67
		public HtmlToken.TagPartMajor MajorPart
		{
			get
			{
				return this.PartMajor;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x000A5C6F File Offset: 0x000A3E6F
		public HtmlToken.TagPartMinor MinorPart
		{
			get
			{
				return this.PartMinor;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x000A5C77 File Offset: 0x000A3E77
		public bool IsTagComplete
		{
			get
			{
				return this.PartMajor == HtmlToken.TagPartMajor.Complete;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x000A5C82 File Offset: 0x000A3E82
		public bool IsTagBegin
		{
			get
			{
				return (byte)(this.PartMajor & HtmlToken.TagPartMajor.Begin) == 3;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x000A5C90 File Offset: 0x000A3E90
		public bool IsTagEnd
		{
			get
			{
				return (byte)(this.PartMajor & HtmlToken.TagPartMajor.End) == 6;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x000A5C9E File Offset: 0x000A3E9E
		public bool IsTagNameEmpty
		{
			get
			{
				return 0 != (byte)(this.flags & HtmlToken.TagFlags.EmptyTagName);
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x000A5CAF File Offset: 0x000A3EAF
		public bool IsTagNameBegin
		{
			get
			{
				return (byte)(this.PartMinor & HtmlToken.TagPartMinor.BeginName) == 3;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x000A5CBD File Offset: 0x000A3EBD
		public bool IsTagNameEnd
		{
			get
			{
				return (byte)(this.PartMinor & HtmlToken.TagPartMinor.EndName) == 6;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x000A5CCB File Offset: 0x000A3ECB
		public bool HasNameFragment
		{
			get
			{
				return !base.IsFragmentEmpty(this.NameInternal);
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x000A5CDC File Offset: 0x000A3EDC
		public HtmlToken.TagNameTextReader Name
		{
			get
			{
				return new HtmlToken.TagNameTextReader(this);
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x000A5CE4 File Offset: 0x000A3EE4
		public HtmlToken.TagUnstructuredContentTextReader UnstructuredContent
		{
			get
			{
				return new HtmlToken.TagUnstructuredContentTextReader(this);
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x000A5CEC File Offset: 0x000A3EEC
		public HtmlTagIndex OriginalTagId
		{
			get
			{
				return this.OriginalTagIndex;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x000A5CF4 File Offset: 0x000A3EF4
		public bool IsAllowWspLeft
		{
			get
			{
				return (byte)(this.flags & HtmlToken.TagFlags.AllowWspLeft) == 64;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x000A5D04 File Offset: 0x000A3F04
		public bool IsAllowWspRight
		{
			get
			{
				return (byte)(this.flags & HtmlToken.TagFlags.AllowWspRight) == 128;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x000A5D1A File Offset: 0x000A3F1A
		public HtmlToken.AttributeEnumerator Attributes
		{
			get
			{
				return new HtmlToken.AttributeEnumerator(this);
			}
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x000A5D24 File Offset: 0x000A3F24
		internal new void Reset()
		{
			this.TagIndex = (this.OriginalTagIndex = HtmlTagIndex._NULL);
			this.NameIndex = HtmlNameIndex._NOTANAME;
			this.flags = HtmlToken.TagFlags.None;
			this.PartMajor = HtmlToken.TagPartMajor.None;
			this.PartMinor = HtmlToken.TagPartMinor.Empty;
			this.NameInternal.Reset();
			this.Unstructured.Reset();
			this.NamePosition.Reset();
			this.UnstructuredPosition.Reset();
			this.AttributeTail = 0;
			this.CurrentAttribute = -1;
			this.AttrNamePosition.Reset();
			this.AttrValuePosition.Reset();
		}

		// Token: 0x04001807 RID: 6151
		private HtmlToken.TagFlags flags;

		// Token: 0x04001808 RID: 6152
		protected internal HtmlTagIndex TagIndex;

		// Token: 0x04001809 RID: 6153
		protected internal HtmlTagIndex OriginalTagIndex;

		// Token: 0x0400180A RID: 6154
		protected internal HtmlNameIndex NameIndex;

		// Token: 0x0400180B RID: 6155
		protected internal HtmlToken.TagPartMajor PartMajor;

		// Token: 0x0400180C RID: 6156
		protected internal HtmlToken.TagPartMinor PartMinor;

		// Token: 0x0400180D RID: 6157
		protected internal Token.LexicalUnit Unstructured;

		// Token: 0x0400180E RID: 6158
		protected internal Token.FragmentPosition UnstructuredPosition;

		// Token: 0x0400180F RID: 6159
		protected internal Token.LexicalUnit NameInternal;

		// Token: 0x04001810 RID: 6160
		protected internal Token.LexicalUnit LocalName;

		// Token: 0x04001811 RID: 6161
		protected internal Token.FragmentPosition NamePosition;

		// Token: 0x04001812 RID: 6162
		protected internal HtmlToken.AttributeEntry[] AttributeList;

		// Token: 0x04001813 RID: 6163
		protected internal int AttributeTail;

		// Token: 0x04001814 RID: 6164
		protected internal int CurrentAttribute;

		// Token: 0x04001815 RID: 6165
		protected internal Token.FragmentPosition AttrNamePosition;

		// Token: 0x04001816 RID: 6166
		protected internal Token.FragmentPosition AttrValuePosition;

		// Token: 0x02000207 RID: 519
		[Flags]
		public enum TagFlags : byte
		{
			// Token: 0x04001818 RID: 6168
			None = 0,
			// Token: 0x04001819 RID: 6169
			EmptyTagName = 8,
			// Token: 0x0400181A RID: 6170
			EndTag = 16,
			// Token: 0x0400181B RID: 6171
			EmptyScope = 32,
			// Token: 0x0400181C RID: 6172
			AllowWspLeft = 64,
			// Token: 0x0400181D RID: 6173
			AllowWspRight = 128
		}

		// Token: 0x02000208 RID: 520
		public enum TagPartMajor : byte
		{
			// Token: 0x0400181F RID: 6175
			None,
			// Token: 0x04001820 RID: 6176
			Begin = 3,
			// Token: 0x04001821 RID: 6177
			Continue = 2,
			// Token: 0x04001822 RID: 6178
			End = 6,
			// Token: 0x04001823 RID: 6179
			Complete
		}

		// Token: 0x02000209 RID: 521
		public enum TagPartMinor : byte
		{
			// Token: 0x04001825 RID: 6181
			Empty,
			// Token: 0x04001826 RID: 6182
			BeginName = 3,
			// Token: 0x04001827 RID: 6183
			ContinueName = 2,
			// Token: 0x04001828 RID: 6184
			EndName = 6,
			// Token: 0x04001829 RID: 6185
			EndNameWithAttributes = 134,
			// Token: 0x0400182A RID: 6186
			CompleteName = 7,
			// Token: 0x0400182B RID: 6187
			CompleteNameWithAttributes = 135,
			// Token: 0x0400182C RID: 6188
			BeginAttribute = 24,
			// Token: 0x0400182D RID: 6189
			ContinueAttribute = 16,
			// Token: 0x0400182E RID: 6190
			EndAttribute = 48,
			// Token: 0x0400182F RID: 6191
			EndAttributeWithOtherAttributes = 176,
			// Token: 0x04001830 RID: 6192
			AttributePartMask = 56,
			// Token: 0x04001831 RID: 6193
			Attributes = 128
		}

		// Token: 0x0200020A RID: 522
		public enum AttrPartMajor : byte
		{
			// Token: 0x04001833 RID: 6195
			None,
			// Token: 0x04001834 RID: 6196
			Begin = 24,
			// Token: 0x04001835 RID: 6197
			Continue = 16,
			// Token: 0x04001836 RID: 6198
			End = 48,
			// Token: 0x04001837 RID: 6199
			Complete = 56,
			// Token: 0x04001838 RID: 6200
			EmptyName = 1,
			// Token: 0x04001839 RID: 6201
			ValueQuoted = 64,
			// Token: 0x0400183A RID: 6202
			Deleted = 128,
			// Token: 0x0400183B RID: 6203
			MaskOffFlags = 56
		}

		// Token: 0x0200020B RID: 523
		public enum AttrPartMinor : byte
		{
			// Token: 0x0400183D RID: 6205
			Empty,
			// Token: 0x0400183E RID: 6206
			BeginName = 3,
			// Token: 0x0400183F RID: 6207
			ContinueName = 2,
			// Token: 0x04001840 RID: 6208
			EndName = 6,
			// Token: 0x04001841 RID: 6209
			EndNameWithBeginValue = 30,
			// Token: 0x04001842 RID: 6210
			EndNameWithCompleteValue = 62,
			// Token: 0x04001843 RID: 6211
			CompleteName = 7,
			// Token: 0x04001844 RID: 6212
			CompleteNameWithBeginValue = 31,
			// Token: 0x04001845 RID: 6213
			CompleteNameWithCompleteValue = 63,
			// Token: 0x04001846 RID: 6214
			BeginValue = 24,
			// Token: 0x04001847 RID: 6215
			ContinueValue = 16,
			// Token: 0x04001848 RID: 6216
			EndValue = 48,
			// Token: 0x04001849 RID: 6217
			CompleteValue = 56
		}

		// Token: 0x0200020C RID: 524
		public struct AttributeEnumerator
		{
			// Token: 0x06001561 RID: 5473 RVA: 0x000A5DAD File Offset: 0x000A3FAD
			internal AttributeEnumerator(HtmlToken token)
			{
				this.token = token;
			}

			// Token: 0x1700057A RID: 1402
			// (get) Token: 0x06001562 RID: 5474 RVA: 0x000A5DB6 File Offset: 0x000A3FB6
			public int Count
			{
				get
				{
					return this.token.AttributeTail;
				}
			}

			// Token: 0x1700057B RID: 1403
			// (get) Token: 0x06001563 RID: 5475 RVA: 0x000A5DC3 File Offset: 0x000A3FC3
			public HtmlAttribute Current
			{
				get
				{
					return new HtmlAttribute(this.token);
				}
			}

			// Token: 0x1700057C RID: 1404
			// (get) Token: 0x06001564 RID: 5476 RVA: 0x000A5DD0 File Offset: 0x000A3FD0
			public int CurrentIndex
			{
				get
				{
					return this.token.CurrentAttribute;
				}
			}

			// Token: 0x1700057D RID: 1405
			public HtmlAttribute this[int i]
			{
				get
				{
					if (i != this.token.CurrentAttribute)
					{
						this.token.AttrNamePosition.Rewind(this.token.AttributeList[i].Name);
						this.token.AttrValuePosition.Rewind(this.token.AttributeList[i].Value);
					}
					this.token.CurrentAttribute = i;
					return new HtmlAttribute(this.token);
				}
			}

			// Token: 0x06001566 RID: 5478 RVA: 0x000A5E60 File Offset: 0x000A4060
			public bool MoveNext()
			{
				if (this.token.CurrentAttribute != this.token.AttributeTail)
				{
					this.token.CurrentAttribute++;
					if (this.token.CurrentAttribute != this.token.AttributeTail)
					{
						this.token.AttrNamePosition.Rewind(this.token.AttributeList[this.token.CurrentAttribute].Name);
						this.token.AttrValuePosition.Rewind(this.token.AttributeList[this.token.CurrentAttribute].Value);
					}
				}
				return this.token.CurrentAttribute != this.token.AttributeTail;
			}

			// Token: 0x06001567 RID: 5479 RVA: 0x000A5F2E File Offset: 0x000A412E
			public void Rewind()
			{
				this.token.CurrentAttribute = -1;
			}

			// Token: 0x06001568 RID: 5480 RVA: 0x000A5F3C File Offset: 0x000A413C
			public HtmlToken.AttributeEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x06001569 RID: 5481 RVA: 0x000A5F44 File Offset: 0x000A4144
			public bool Find(HtmlNameIndex nameIndex)
			{
				for (int i = 0; i < this.token.AttributeTail; i++)
				{
					if (this.token.AttributeList[i].NameIndex == nameIndex)
					{
						this.token.CurrentAttribute = i;
						this.token.AttrNamePosition.Rewind(this.token.AttributeList[i].Name);
						this.token.AttrValuePosition.Rewind(this.token.AttributeList[i].Value);
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600156A RID: 5482 RVA: 0x000A5FDE File Offset: 0x000A41DE
			[Conditional("DEBUG")]
			private void AssertCurrent()
			{
			}

			// Token: 0x0400184A RID: 6218
			private HtmlToken token;
		}

		// Token: 0x0200020D RID: 525
		public struct TagUnstructuredContentTextReader
		{
			// Token: 0x0600156B RID: 5483 RVA: 0x000A5FE0 File Offset: 0x000A41E0
			internal TagUnstructuredContentTextReader(HtmlToken token)
			{
				this.token = token;
			}

			// Token: 0x0600156C RID: 5484 RVA: 0x000A5FE9 File Offset: 0x000A41E9
			public void WriteTo(ITextSink sink)
			{
				this.token.WriteTo(this.token.Unstructured, sink);
			}

			// Token: 0x0600156D RID: 5485 RVA: 0x000A6002 File Offset: 0x000A4202
			public string GetString(int maxSize)
			{
				return this.token.GetString(this.token.Unstructured, maxSize);
			}

			// Token: 0x0600156E RID: 5486 RVA: 0x000A601B File Offset: 0x000A421B
			[Conditional("DEBUG")]
			private void AssertCurrent()
			{
			}

			// Token: 0x0400184B RID: 6219
			private HtmlToken token;
		}

		// Token: 0x0200020E RID: 526
		public struct TagNameTextReader
		{
			// Token: 0x0600156F RID: 5487 RVA: 0x000A601D File Offset: 0x000A421D
			internal TagNameTextReader(HtmlToken token)
			{
				this.token = token;
			}

			// Token: 0x1700057E RID: 1406
			// (get) Token: 0x06001570 RID: 5488 RVA: 0x000A6026 File Offset: 0x000A4226
			public int Length
			{
				get
				{
					return this.token.GetLength(this.token.NameInternal);
				}
			}

			// Token: 0x06001571 RID: 5489 RVA: 0x000A6040 File Offset: 0x000A4240
			public int Read(char[] buffer, int offset, int count)
			{
				return this.token.Read(this.token.NameInternal, ref this.token.NamePosition, buffer, offset, count);
			}

			// Token: 0x06001572 RID: 5490 RVA: 0x000A6073 File Offset: 0x000A4273
			public void Rewind()
			{
				this.token.NamePosition.Rewind(this.token.NameInternal);
			}

			// Token: 0x06001573 RID: 5491 RVA: 0x000A6090 File Offset: 0x000A4290
			public void WriteTo(ITextSink sink)
			{
				this.token.WriteTo(this.token.NameInternal, sink);
			}

			// Token: 0x06001574 RID: 5492 RVA: 0x000A60A9 File Offset: 0x000A42A9
			public string GetString(int maxSize)
			{
				return this.token.GetString(this.token.NameInternal, maxSize);
			}

			// Token: 0x06001575 RID: 5493 RVA: 0x000A60C2 File Offset: 0x000A42C2
			public void MakeEmpty()
			{
				this.token.NameInternal.Reset();
				this.Rewind();
			}

			// Token: 0x06001576 RID: 5494 RVA: 0x000A60DA File Offset: 0x000A42DA
			[Conditional("DEBUG")]
			private void AssertCurrent()
			{
			}

			// Token: 0x0400184C RID: 6220
			private HtmlToken token;
		}

		// Token: 0x0200020F RID: 527
		public struct AttributeNameTextReader
		{
			// Token: 0x06001577 RID: 5495 RVA: 0x000A60DC File Offset: 0x000A42DC
			internal AttributeNameTextReader(HtmlToken token)
			{
				this.token = token;
			}

			// Token: 0x1700057F RID: 1407
			// (get) Token: 0x06001578 RID: 5496 RVA: 0x000A60E5 File Offset: 0x000A42E5
			public int Length
			{
				get
				{
					return this.token.GetLength(this.token.AttributeList[this.token.CurrentAttribute].Name);
				}
			}

			// Token: 0x06001579 RID: 5497 RVA: 0x000A6114 File Offset: 0x000A4314
			public int Read(char[] buffer, int offset, int count)
			{
				return this.token.Read(this.token.AttributeList[this.token.CurrentAttribute].Name, ref this.token.AttrNamePosition, buffer, offset, count);
			}

			// Token: 0x0600157A RID: 5498 RVA: 0x000A615C File Offset: 0x000A435C
			public void Rewind()
			{
				this.token.AttrNamePosition.Rewind(this.token.AttributeList[this.token.CurrentAttribute].Name);
			}

			// Token: 0x0600157B RID: 5499 RVA: 0x000A618E File Offset: 0x000A438E
			public void WriteTo(ITextSink sink)
			{
				this.token.WriteTo(this.token.AttributeList[this.token.CurrentAttribute].Name, sink);
			}

			// Token: 0x0600157C RID: 5500 RVA: 0x000A61BC File Offset: 0x000A43BC
			public string GetString(int maxSize)
			{
				return this.token.GetString(this.token.AttributeList[this.token.CurrentAttribute].Name, maxSize);
			}

			// Token: 0x0600157D RID: 5501 RVA: 0x000A61EC File Offset: 0x000A43EC
			public void MakeEmpty()
			{
				this.token.AttributeList[this.token.CurrentAttribute].Name.Reset();
				this.token.AttrNamePosition.Rewind(this.token.AttributeList[this.token.CurrentAttribute].Name);
			}

			// Token: 0x0600157E RID: 5502 RVA: 0x000A624E File Offset: 0x000A444E
			[Conditional("DEBUG")]
			private void AssertCurrent()
			{
			}

			// Token: 0x0400184D RID: 6221
			private HtmlToken token;
		}

		// Token: 0x02000210 RID: 528
		public struct AttributeValueTextReader
		{
			// Token: 0x0600157F RID: 5503 RVA: 0x000A6250 File Offset: 0x000A4450
			internal AttributeValueTextReader(HtmlToken token)
			{
				this.token = token;
			}

			// Token: 0x17000580 RID: 1408
			// (get) Token: 0x06001580 RID: 5504 RVA: 0x000A6259 File Offset: 0x000A4459
			public int Length
			{
				get
				{
					return this.token.GetLength(this.token.AttributeList[this.token.CurrentAttribute].Value);
				}
			}

			// Token: 0x17000581 RID: 1409
			// (get) Token: 0x06001581 RID: 5505 RVA: 0x000A6286 File Offset: 0x000A4486
			public bool IsEmpty
			{
				get
				{
					return this.token.IsFragmentEmpty(this.token.AttributeList[this.token.CurrentAttribute].Value);
				}
			}

			// Token: 0x17000582 RID: 1410
			// (get) Token: 0x06001582 RID: 5506 RVA: 0x000A62B3 File Offset: 0x000A44B3
			public bool IsContiguous
			{
				get
				{
					return this.token.IsContiguous(this.token.AttributeList[this.token.CurrentAttribute].Value);
				}
			}

			// Token: 0x17000583 RID: 1411
			// (get) Token: 0x06001583 RID: 5507 RVA: 0x000A62E0 File Offset: 0x000A44E0
			public BufferString ContiguousBufferString
			{
				get
				{
					return new BufferString(this.token.Buffer, this.token.AttributeList[this.token.CurrentAttribute].Value.HeadOffset, this.token.RunList[this.token.AttributeList[this.token.CurrentAttribute].Value.Head].Length);
				}
			}

			// Token: 0x06001584 RID: 5508 RVA: 0x000A635C File Offset: 0x000A455C
			public int Read(char[] buffer, int offset, int count)
			{
				return this.token.Read(this.token.AttributeList[this.token.CurrentAttribute].Value, ref this.token.AttrValuePosition, buffer, offset, count);
			}

			// Token: 0x06001585 RID: 5509 RVA: 0x000A63A4 File Offset: 0x000A45A4
			public void Rewind()
			{
				this.token.AttrValuePosition.Rewind(this.token.AttributeList[this.token.CurrentAttribute].Value);
			}

			// Token: 0x06001586 RID: 5510 RVA: 0x000A63D6 File Offset: 0x000A45D6
			public void WriteTo(ITextSink sink)
			{
				this.token.WriteTo(this.token.AttributeList[this.token.CurrentAttribute].Value, sink);
			}

			// Token: 0x06001587 RID: 5511 RVA: 0x000A6404 File Offset: 0x000A4604
			public string GetString(int maxSize)
			{
				return this.token.GetString(this.token.AttributeList[this.token.CurrentAttribute].Value, maxSize);
			}

			// Token: 0x06001588 RID: 5512 RVA: 0x000A6432 File Offset: 0x000A4632
			public bool CaseInsensitiveCompareEqual(string str)
			{
				return this.token.CaseInsensitiveCompareEqual(this.token.AttributeList[this.token.CurrentAttribute].Value, str);
			}

			// Token: 0x06001589 RID: 5513 RVA: 0x000A6460 File Offset: 0x000A4660
			public bool CaseInsensitiveContainsSubstring(string str)
			{
				return this.token.CaseInsensitiveContainsSubstring(this.token.AttributeList[this.token.CurrentAttribute].Value, str);
			}

			// Token: 0x0600158A RID: 5514 RVA: 0x000A648E File Offset: 0x000A468E
			public bool SkipLeadingWhitespace()
			{
				return this.token.SkipLeadingWhitespace(this.token.AttributeList[this.token.CurrentAttribute].Value, ref this.token.AttrValuePosition);
			}

			// Token: 0x0600158B RID: 5515 RVA: 0x000A64C6 File Offset: 0x000A46C6
			public void MakeEmpty()
			{
				this.token.AttributeList[this.token.CurrentAttribute].Value.Reset();
				this.Rewind();
			}

			// Token: 0x0600158C RID: 5516 RVA: 0x000A64F3 File Offset: 0x000A46F3
			[Conditional("DEBUG")]
			private void AssertCurrent()
			{
			}

			// Token: 0x0400184E RID: 6222
			private HtmlToken token;
		}

		// Token: 0x02000211 RID: 529
		protected internal struct AttributeEntry
		{
			// Token: 0x17000584 RID: 1412
			// (get) Token: 0x0600158D RID: 5517 RVA: 0x000A64F5 File Offset: 0x000A46F5
			public bool IsCompleteAttr
			{
				get
				{
					return this.MajorPart == HtmlToken.AttrPartMajor.Complete;
				}
			}

			// Token: 0x17000585 RID: 1413
			// (get) Token: 0x0600158E RID: 5518 RVA: 0x000A6501 File Offset: 0x000A4701
			public bool IsAttrBegin
			{
				get
				{
					return (byte)(this.PartMajor & HtmlToken.AttrPartMajor.Begin) == 24;
				}
			}

			// Token: 0x17000586 RID: 1414
			// (get) Token: 0x0600158F RID: 5519 RVA: 0x000A6511 File Offset: 0x000A4711
			public bool IsAttrEnd
			{
				get
				{
					return (byte)(this.PartMajor & HtmlToken.AttrPartMajor.End) == 48;
				}
			}

			// Token: 0x17000587 RID: 1415
			// (get) Token: 0x06001590 RID: 5520 RVA: 0x000A6521 File Offset: 0x000A4721
			public bool IsAttrEmptyName
			{
				get
				{
					return (byte)(this.PartMajor & HtmlToken.AttrPartMajor.EmptyName) == 1;
				}
			}

			// Token: 0x17000588 RID: 1416
			// (get) Token: 0x06001591 RID: 5521 RVA: 0x000A652F File Offset: 0x000A472F
			public bool IsAttrNameEnd
			{
				get
				{
					return (byte)(this.PartMinor & HtmlToken.AttrPartMinor.EndName) == 6;
				}
			}

			// Token: 0x17000589 RID: 1417
			// (get) Token: 0x06001592 RID: 5522 RVA: 0x000A653D File Offset: 0x000A473D
			public bool IsAttrValueBegin
			{
				get
				{
					return (byte)(this.PartMinor & HtmlToken.AttrPartMinor.BeginValue) == 24;
				}
			}

			// Token: 0x1700058A RID: 1418
			// (get) Token: 0x06001593 RID: 5523 RVA: 0x000A654D File Offset: 0x000A474D
			public HtmlToken.AttrPartMajor MajorPart
			{
				get
				{
					return this.PartMajor & HtmlToken.AttrPartMajor.Complete;
				}
			}

			// Token: 0x1700058B RID: 1419
			// (get) Token: 0x06001594 RID: 5524 RVA: 0x000A6559 File Offset: 0x000A4759
			// (set) Token: 0x06001595 RID: 5525 RVA: 0x000A6561 File Offset: 0x000A4761
			public HtmlToken.AttrPartMinor MinorPart
			{
				get
				{
					return this.PartMinor;
				}
				set
				{
					this.PartMinor = value;
				}
			}

			// Token: 0x1700058C RID: 1420
			// (get) Token: 0x06001596 RID: 5526 RVA: 0x000A656A File Offset: 0x000A476A
			// (set) Token: 0x06001597 RID: 5527 RVA: 0x000A657A File Offset: 0x000A477A
			public bool IsAttrValueQuoted
			{
				get
				{
					return (byte)(this.PartMajor & HtmlToken.AttrPartMajor.ValueQuoted) == 64;
				}
				set
				{
					this.PartMajor = (value ? (this.PartMajor | HtmlToken.AttrPartMajor.ValueQuoted) : (this.PartMajor & (HtmlToken.AttrPartMajor)191));
				}
			}

			// Token: 0x1700058D RID: 1421
			// (get) Token: 0x06001598 RID: 5528 RVA: 0x000A659E File Offset: 0x000A479E
			// (set) Token: 0x06001599 RID: 5529 RVA: 0x000A65B4 File Offset: 0x000A47B4
			public bool IsAttrDeleted
			{
				get
				{
					return (byte)(this.PartMajor & HtmlToken.AttrPartMajor.Deleted) == 128;
				}
				set
				{
					this.PartMajor = (value ? (this.PartMajor | HtmlToken.AttrPartMajor.Deleted) : (this.PartMajor & (HtmlToken.AttrPartMajor)127));
				}
			}

			// Token: 0x0400184F RID: 6223
			public HtmlNameIndex NameIndex;

			// Token: 0x04001850 RID: 6224
			public byte QuoteChar;

			// Token: 0x04001851 RID: 6225
			public byte DangerousCharacters;

			// Token: 0x04001852 RID: 6226
			public HtmlToken.AttrPartMajor PartMajor;

			// Token: 0x04001853 RID: 6227
			public HtmlToken.AttrPartMinor PartMinor;

			// Token: 0x04001854 RID: 6228
			public Token.LexicalUnit Name;

			// Token: 0x04001855 RID: 6229
			public Token.LexicalUnit LocalName;

			// Token: 0x04001856 RID: 6230
			public Token.LexicalUnit Value;
		}
	}
}
