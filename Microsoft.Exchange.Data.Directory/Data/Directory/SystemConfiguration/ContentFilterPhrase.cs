using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004BF RID: 1215
	[Serializable]
	public class ContentFilterPhrase : ConfigurableObject
	{
		// Token: 0x06003741 RID: 14145 RVA: 0x000D8123 File Offset: 0x000D6323
		public ContentFilterPhrase() : base(new SimpleProviderPropertyBag())
		{
			this.Phrase = string.Empty;
			this.Influence = Influence.GoodWord;
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x000D8146 File Offset: 0x000D6346
		private ContentFilterPhrase(string phrase, Influence influence) : base(new SimpleProviderPropertyBag())
		{
			this.Phrase = phrase;
			this.Influence = influence;
		}

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x06003743 RID: 14147 RVA: 0x000D8161 File Offset: 0x000D6361
		// (set) Token: 0x06003744 RID: 14148 RVA: 0x000D8178 File Offset: 0x000D6378
		public Influence Influence
		{
			get
			{
				return (Influence)this.propertyBag[ContentFilterPhraseSchema.Influence];
			}
			internal set
			{
				this.propertyBag[ContentFilterPhraseSchema.Influence] = value;
			}
		}

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x06003745 RID: 14149 RVA: 0x000D8190 File Offset: 0x000D6390
		// (set) Token: 0x06003746 RID: 14150 RVA: 0x000D81A7 File Offset: 0x000D63A7
		public string Phrase
		{
			get
			{
				return (string)this.propertyBag[ContentFilterPhraseSchema.Phrase];
			}
			internal set
			{
				this.propertyBag[ContentFilterPhraseSchema.Phrase] = value;
			}
		}

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x06003747 RID: 14151 RVA: 0x000D81BA File Offset: 0x000D63BA
		public override ObjectId Identity
		{
			get
			{
				return new ContentFilterPhraseIdentity(this.Phrase);
			}
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x000D81C7 File Offset: 0x000D63C7
		public override string ToString()
		{
			return this.Phrase;
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x000D81D0 File Offset: 0x000D63D0
		protected override void ValidateRead(List<ValidationError> errors)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(this.Phrase))
			{
				this.Phrase = this.Phrase.Trim();
				num = this.Phrase.Length;
			}
			if (!Enum.IsDefined(typeof(Influence), this.Influence))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.InvalidInfluence(this.Influence), null, this.Influence));
				return;
			}
			if (num < 1 || num > 256)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.InvalidPhrase((this.Influence == Influence.BadWord) ? "blocking" : "non-blocking", 256), null, this.Phrase));
			}
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x000D828C File Offset: 0x000D648C
		internal static ContentFilterPhrase Decode(string encoded)
		{
			int num = encoded.IndexOf(';');
			if (num < 0)
			{
				throw new FormatException("Encoded string is invalid: " + encoded);
			}
			int num2 = int.Parse(encoded.Substring(0, num), CultureInfo.InvariantCulture);
			if (!Enum.IsDefined(typeof(Influence), num2))
			{
				throw new FormatException("Encoded string is invalid: " + encoded);
			}
			return new ContentFilterPhrase(encoded.Substring(num + 1), (Influence)num2);
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x000D8304 File Offset: 0x000D6504
		internal string Encode()
		{
			return ((int)this.Influence).ToString(CultureInfo.InvariantCulture) + ';' + this.Phrase;
		}

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x0600374C RID: 14156 RVA: 0x000D8336 File Offset: 0x000D6536
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ContentFilterPhrase.schema;
			}
		}

		// Token: 0x04002564 RID: 9572
		private const char Delimiter = ';';

		// Token: 0x04002565 RID: 9573
		private const int MaxLength = 256;

		// Token: 0x04002566 RID: 9574
		private static ObjectSchema schema = ObjectSchema.GetInstance<ContentFilterPhraseSchema>();
	}
}
