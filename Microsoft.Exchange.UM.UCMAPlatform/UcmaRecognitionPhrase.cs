using System;
using System.Collections.ObjectModel;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Speech.Recognition;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x0200005A RID: 90
	internal class UcmaRecognitionPhrase : UcmaRecognitionPhraseBase
	{
		// Token: 0x060003F6 RID: 1014 RVA: 0x0001227A File Offset: 0x0001047A
		internal UcmaRecognitionPhrase(RecognizedPhrase recognitionPhrase)
		{
			ValidateArgument.NotNull(recognitionPhrase, "recognitionPhrase");
			this.recognitionPhrase = recognitionPhrase;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00012294 File Offset: 0x00010494
		public override float Confidence
		{
			get
			{
				return this.recognitionPhrase.Confidence;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x000122A1 File Offset: 0x000104A1
		public override string Text
		{
			get
			{
				return this.recognitionPhrase.Text;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x000122AE File Offset: 0x000104AE
		public override int HomophoneGroupId
		{
			get
			{
				return this.recognitionPhrase.HomophoneGroupId;
			}
		}

		// Token: 0x170000D6 RID: 214
		public override object this[string key]
		{
			get
			{
				if (this.recognitionPhrase.Semantics.ContainsKey(key))
				{
					return this.recognitionPhrase.Semantics[key].Value ?? string.Empty;
				}
				return null;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x000122F1 File Offset: 0x000104F1
		protected override ReadOnlyCollection<RecognizedWordUnit> WordUnits
		{
			get
			{
				return this.recognitionPhrase.Words;
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000122FE File Offset: 0x000104FE
		public override string ToSml()
		{
			if (this.recognitionPhrase == null)
			{
				return string.Empty;
			}
			return this.recognitionPhrase.ConstructSmlFromSemantics().CreateNavigator().OuterXml;
		}

		// Token: 0x0400013D RID: 317
		private RecognizedPhrase recognitionPhrase;
	}
}
