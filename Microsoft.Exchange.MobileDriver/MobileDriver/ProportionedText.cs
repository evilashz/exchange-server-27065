using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200003A RID: 58
	internal class ProportionedText
	{
		// Token: 0x0600012E RID: 302 RVA: 0x0000735E File Offset: 0x0000555E
		public ProportionedText(string text) : this(text, false)
		{
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007368 File Offset: 0x00005568
		public ProportionedText(string text, bool isOptional) : this(text, string.IsNullOrEmpty(text) ? 0 : text.Length, string.IsNullOrEmpty(text) ? 0 : text.Length, 0)
		{
			this.IsOptional = isOptional;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000739C File Offset: 0x0000559C
		public ProportionedText(string text, int lengthReservedAtLeast, int lengthKeptAtMost, int weight)
		{
			if (0 > lengthReservedAtLeast)
			{
				throw new ArgumentOutOfRangeException("lengthReservedAtLeast");
			}
			if (0 > lengthKeptAtMost)
			{
				throw new ArgumentOutOfRangeException("lengthKeptAtMost");
			}
			if (0 > weight)
			{
				throw new ArgumentOutOfRangeException("weight");
			}
			if (lengthReservedAtLeast > lengthKeptAtMost)
			{
				throw new ArgumentOutOfRangeException("lengthReservedAtLeast");
			}
			this.Text = (text ?? string.Empty);
			this.LengthReservedAtLeast = Math.Min(this.Text.Length, lengthReservedAtLeast);
			this.LengthKeptAtMost = Math.Min(this.Text.Length, lengthKeptAtMost);
			this.Weight = weight;
			this.IsOptional = false;
			if (this.LengthReservedAtLeast == this.Text.Length)
			{
				this.Type = ProportionedText.ProportionedTextType.OriginalCopy;
				return;
			}
			if (this.LengthReservedAtLeast == this.LengthKeptAtMost)
			{
				this.Type = ProportionedText.ProportionedTextType.FixedTruncation;
				this.Text = this.Text.Substring(0, this.LengthKeptAtMost);
				return;
			}
			this.Type = ProportionedText.ProportionedTextType.WeightedProportion;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00007489 File Offset: 0x00005689
		private ProportionedText(string text, ProportionedText.ProportionedTextType type) : this(text, false)
		{
			this.Type = type;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000749A File Offset: 0x0000569A
		// (set) Token: 0x06000133 RID: 307 RVA: 0x000074A2 File Offset: 0x000056A2
		public ProportionedText.ProportionedTextType Type { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000074AB File Offset: 0x000056AB
		// (set) Token: 0x06000135 RID: 309 RVA: 0x000074B3 File Offset: 0x000056B3
		public int Weight { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000074BC File Offset: 0x000056BC
		// (set) Token: 0x06000137 RID: 311 RVA: 0x000074C4 File Offset: 0x000056C4
		public int LengthReservedAtLeast { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000074CD File Offset: 0x000056CD
		// (set) Token: 0x06000139 RID: 313 RVA: 0x000074D5 File Offset: 0x000056D5
		public int LengthKeptAtMost { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600013A RID: 314 RVA: 0x000074DE File Offset: 0x000056DE
		// (set) Token: 0x0600013B RID: 315 RVA: 0x000074E6 File Offset: 0x000056E6
		public string Text { get; private set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000074EF File Offset: 0x000056EF
		// (set) Token: 0x0600013D RID: 317 RVA: 0x000074F7 File Offset: 0x000056F7
		public bool IsOptional { get; private set; }

		// Token: 0x0600013E RID: 318 RVA: 0x00007500 File Offset: 0x00005700
		public ProportionedText.PresentationText ToPresentationText(int lengthKept, int groupId)
		{
			if (ProportionedText.ProportionedTextType.WeightedProportion != this.Type)
			{
				return new ProportionedText.PresentationText(this.Text, ProportionedText.ProportionedTextType.FixedTruncation == this.Type, groupId);
			}
			lengthKept = Math.Max(lengthKept, this.LengthReservedAtLeast);
			lengthKept = Math.Min(lengthKept, this.LengthKeptAtMost);
			return new ProportionedText.PresentationText(this.Text.Substring(0, lengthKept), lengthKept < this.Text.Length, groupId);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000756C File Offset: 0x0000576C
		public ProportionedText.PresentationText ToPresentationText(int totalWeight, int capacityForWeightedText, int groupId)
		{
			if (ProportionedText.ProportionedTextType.WeightedProportion != this.Type)
			{
				return new ProportionedText.PresentationText(this.Text, ProportionedText.ProportionedTextType.FixedTruncation == this.Type, groupId);
			}
			if (0 > totalWeight || this.Weight > totalWeight)
			{
				throw new ArgumentOutOfRangeException("totalWeight");
			}
			if (0 > capacityForWeightedText)
			{
				throw new ArgumentOutOfRangeException("capacityForWeightedText");
			}
			if (totalWeight == 0)
			{
				return new ProportionedText.PresentationText(string.Empty, false, groupId);
			}
			return this.ToPresentationText(capacityForWeightedText * this.Weight / totalWeight, groupId);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000075E0 File Offset: 0x000057E0
		public override string ToString()
		{
			return this.Text;
		}

		// Token: 0x040000BC RID: 188
		public static readonly ProportionedText Delimiter = new ProportionedText(" ", ProportionedText.ProportionedTextType.Delimiter);

		// Token: 0x0200003B RID: 59
		internal enum ProportionedTextType
		{
			// Token: 0x040000C4 RID: 196
			OriginalCopy,
			// Token: 0x040000C5 RID: 197
			FixedTruncation,
			// Token: 0x040000C6 RID: 198
			WeightedProportion,
			// Token: 0x040000C7 RID: 199
			Delimiter
		}

		// Token: 0x0200003C RID: 60
		public class PresentationText
		{
			// Token: 0x06000142 RID: 322 RVA: 0x000075FA File Offset: 0x000057FA
			public PresentationText(string text, bool needEllipsisTrail, int groupId)
			{
				this.Text = text;
				this.NeedEllipsisTrail = needEllipsisTrail;
				this.GroupId = groupId;
			}

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x06000143 RID: 323 RVA: 0x00007617 File Offset: 0x00005817
			// (set) Token: 0x06000144 RID: 324 RVA: 0x0000761F File Offset: 0x0000581F
			public string Text { get; private set; }

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x06000145 RID: 325 RVA: 0x00007628 File Offset: 0x00005828
			// (set) Token: 0x06000146 RID: 326 RVA: 0x00007630 File Offset: 0x00005830
			public bool NeedEllipsisTrail { get; private set; }

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x06000147 RID: 327 RVA: 0x00007639 File Offset: 0x00005839
			// (set) Token: 0x06000148 RID: 328 RVA: 0x00007641 File Offset: 0x00005841
			public int GroupId { get; private set; }

			// Token: 0x06000149 RID: 329 RVA: 0x0000764A File Offset: 0x0000584A
			public override string ToString()
			{
				return this.ToString(CodingScheme.Unicode);
			}

			// Token: 0x0600014A RID: 330 RVA: 0x00007653 File Offset: 0x00005853
			public string ToString(CodingScheme codingScheme)
			{
				if (codingScheme == CodingScheme.Neutral)
				{
					throw new ArgumentOutOfRangeException("codingScheme");
				}
				if (!this.NeedEllipsisTrail)
				{
					return this.Text;
				}
				return new EllipsisTrailer(codingScheme).Trail(this.Text);
			}
		}

		// Token: 0x0200003D RID: 61
		public class PresentationContent
		{
			// Token: 0x17000078 RID: 120
			// (get) Token: 0x0600014B RID: 331 RVA: 0x00007683 File Offset: 0x00005883
			// (set) Token: 0x0600014C RID: 332 RVA: 0x0000768B File Offset: 0x0000588B
			public bool IsGrouped { get; private set; }

			// Token: 0x0600014D RID: 333 RVA: 0x00007694 File Offset: 0x00005894
			public PresentationContent(IList<ProportionedText.PresentationText> presentationText)
			{
				if (presentationText == null)
				{
					throw new ArgumentNullException("texts");
				}
				this.PresentationTexts = presentationText;
				this.IsGrouped = false;
				for (int i = 0; i < presentationText.Count; i++)
				{
					if (presentationText[i].GroupId > 0)
					{
						this.IsGrouped = true;
						return;
					}
				}
			}

			// Token: 0x0600014E RID: 334 RVA: 0x000076EC File Offset: 0x000058EC
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(this.PresentationTexts.Count);
				int num = 0;
				int num2 = 0;
				while (this.PresentationTexts.Count > num2)
				{
					if (this.PresentationTexts[num2].GroupId != num)
					{
						num = this.PresentationTexts[num2].GroupId;
					}
					else
					{
						stringBuilder.Append(this.PresentationTexts[num2].Text);
					}
					num2++;
				}
				return stringBuilder.ToString();
			}

			// Token: 0x040000CB RID: 203
			public IList<ProportionedText.PresentationText> PresentationTexts;
		}
	}
}
