using System;

namespace Microsoft.Exchange.Server.Storage.Diagnostics.Generated
{
	// Token: 0x0200003E RID: 62
	public struct Token
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000D627 File Offset: 0x0000B827
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0000D62F File Offset: 0x0000B82F
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000D638 File Offset: 0x0000B838
		public string ValueAsString
		{
			get
			{
				return this.value as string;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000D645 File Offset: 0x0000B845
		public string ValueAsIdentifier
		{
			get
			{
				if (this.ValueAsString != null && this.ValueAsString.StartsWith("[") && this.ValueAsString.EndsWith("]"))
				{
					return this.TrimOuter();
				}
				return this.ValueAsString;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000D680 File Offset: 0x0000B880
		public string ValueAsSubtractor
		{
			get
			{
				string text = this.ValueAsString;
				if (text == null)
				{
					return null;
				}
				if (text.Length < 1 || !text.StartsWith("-"))
				{
					return text;
				}
				text = text.Substring(1);
				if (text.StartsWith("[") && text.EndsWith("]"))
				{
					return text.Substring(1, text.Length - 2);
				}
				return text;
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000D6E4 File Offset: 0x0000B8E4
		public string TrimOuter()
		{
			if (this.ValueAsString != null && this.ValueAsString.Length > 1)
			{
				return this.ValueAsString.Substring(1, this.ValueAsString.Length - 2);
			}
			return this.ValueAsString;
		}

		// Token: 0x04000118 RID: 280
		private const string IdentifierBegin = "[";

		// Token: 0x04000119 RID: 281
		private const string IdentifierFinish = "]";

		// Token: 0x0400011A RID: 282
		private const string SubtractorPrefix = "-";

		// Token: 0x0400011B RID: 283
		private object value;
	}
}
