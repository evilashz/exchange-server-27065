using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000039 RID: 57
	internal class ProportionedContent
	{
		// Token: 0x06000126 RID: 294 RVA: 0x00007130 File Offset: 0x00005330
		public ProportionedContent(IList<ProportionedText> texts)
		{
			if (texts == null)
			{
				throw new ArgumentNullException("texts");
			}
			this.nodes = new List<ProportionedText>(texts);
			this.nodes.RemoveAll((ProportionedText node) => string.IsNullOrEmpty(node.Text));
			List<int> list = new List<int>(this.nodes.Count);
			int num = 0;
			while (this.nodes.Count > num)
			{
				if (ProportionedText.ProportionedTextType.WeightedProportion == this.nodes[num].Type)
				{
					list.Add(num);
					this.TotalWeight += this.nodes[num].Weight;
				}
				num++;
			}
			if (list.Count == 0)
			{
				return;
			}
			foreach (int index in list)
			{
				int num2 = Math.Min(this.nodes[index].LengthKeptAtMost, this.nodes[index].Text.Length) * this.TotalWeight / this.nodes[index].Weight;
				if (this.IdealWeightedTextCapacity < num2)
				{
					this.IdealWeightedTextCapacity = num2;
				}
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00007280 File Offset: 0x00005480
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00007288 File Offset: 0x00005488
		public int IdealWeightedTextCapacity { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00007291 File Offset: 0x00005491
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00007299 File Offset: 0x00005499
		public int TotalWeight { get; private set; }

		// Token: 0x0600012B RID: 299 RVA: 0x000072A4 File Offset: 0x000054A4
		public ProportionedText.PresentationContent GetPresentationContent(int weightedTextCapacity, bool withOptional, bool withGroup)
		{
			List<ProportionedText.PresentationText> list = new List<ProportionedText.PresentationText>(this.nodes.Count);
			int num = 0;
			int num2 = 0;
			while (this.nodes.Count > num2)
			{
				if (withOptional || !this.nodes[num2].IsOptional)
				{
					if (this.nodes[num2].Type == ProportionedText.ProportionedTextType.Delimiter)
					{
						if (!withGroup)
						{
							goto IL_87;
						}
						num++;
					}
					ProportionedText.PresentationText presentationText = this.nodes[num2].ToPresentationText(this.TotalWeight, Math.Min(weightedTextCapacity, this.IdealWeightedTextCapacity), num);
					if (!string.IsNullOrEmpty(presentationText.Text))
					{
						list.Insert(list.Count, presentationText);
					}
				}
				IL_87:
				num2++;
			}
			return new ProportionedText.PresentationContent(list);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007353 File Offset: 0x00005553
		public ProportionedText.PresentationContent GetPresentationContent(int weightedTextCapacity, bool withOptional)
		{
			return this.GetPresentationContent(weightedTextCapacity, withOptional, true);
		}

		// Token: 0x040000B8 RID: 184
		private List<ProportionedText> nodes;
	}
}
