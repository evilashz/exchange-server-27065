using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200003B RID: 59
	[Serializable]
	internal class TextFilter : ContentFilter
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x00007C66 File Offset: 0x00005E66
		public TextFilter(PropertyDefinition property, string text, MatchOptions matchOptions, MatchFlags matchFlags) : base(property, matchOptions, matchFlags)
		{
			this.text = text;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00007C79 File Offset: 0x00005E79
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00007C81 File Offset: 0x00005E81
		public override SinglePropertyFilter CloneWithAnotherProperty(PropertyDefinition property)
		{
			base.CheckClonable(property);
			return new TextFilter(property, this.text, base.MatchOptions, base.MatchFlags);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00007CA4 File Offset: 0x00005EA4
		public override bool Equals(object obj)
		{
			TextFilter textFilter = obj as TextFilter;
			return textFilter != null && base.MatchFlags == textFilter.MatchFlags && base.MatchOptions == textFilter.MatchOptions && this.text == textFilter.text && base.Equals(obj);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00007CF3 File Offset: 0x00005EF3
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.text.GetHashCode();
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00007D07 File Offset: 0x00005F07
		public override string PropertyName
		{
			get
			{
				if (base.Property != null)
				{
					return QueryFilter.ConvertPropertyName(base.Property.Name);
				}
				return base.PropertyName;
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00007D28 File Offset: 0x00005F28
		public override IEnumerable<string> Keywords()
		{
			return new string[]
			{
				this.StringValue
			};
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00007D46 File Offset: 0x00005F46
		protected override string StringValue
		{
			get
			{
				if (this.text != null)
				{
					return this.text;
				}
				return "<null>";
			}
		}

		// Token: 0x0400009B RID: 155
		private readonly string text;
	}
}
