using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Css
{
	// Token: 0x020001AB RID: 427
	internal struct CssProperty
	{
		// Token: 0x0600122B RID: 4651 RVA: 0x0008309A File Offset: 0x0008129A
		internal CssProperty(CssToken token)
		{
			this.token = token;
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x000830A3 File Offset: 0x000812A3
		public int Index
		{
			get
			{
				return this.token.CurrentProperty;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x000830B0 File Offset: 0x000812B0
		public bool IsCompleteProperty
		{
			get
			{
				return this.token.PropertyList[this.token.CurrentProperty].IsCompleteProperty;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x000830D2 File Offset: 0x000812D2
		public bool IsPropertyBegin
		{
			get
			{
				return this.token.PropertyList[this.token.CurrentProperty].IsPropertyBegin;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x000830F4 File Offset: 0x000812F4
		public bool IsPropertyEnd
		{
			get
			{
				return this.token.PropertyList[this.token.CurrentProperty].IsPropertyEnd;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x00083116 File Offset: 0x00081316
		public bool IsPropertyNameEnd
		{
			get
			{
				return this.token.PropertyList[this.token.CurrentProperty].IsPropertyNameEnd;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x00083138 File Offset: 0x00081338
		public bool IsDeleted
		{
			get
			{
				return this.token.PropertyList[this.token.CurrentProperty].IsPropertyDeleted;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x0008315A File Offset: 0x0008135A
		public bool IsPropertyValueQuoted
		{
			get
			{
				return this.token.PropertyList[this.token.CurrentProperty].IsPropertyValueQuoted;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x0008317C File Offset: 0x0008137C
		public CssNameIndex NameId
		{
			get
			{
				return this.token.PropertyList[this.token.CurrentProperty].NameId;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x0008319E File Offset: 0x0008139E
		public char QuoteChar
		{
			get
			{
				return (char)this.token.PropertyList[this.token.CurrentProperty].QuoteChar;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x000831C0 File Offset: 0x000813C0
		public bool HasNameFragment
		{
			get
			{
				return !this.token.PropertyList[this.token.CurrentProperty].Name.IsEmpty;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x000831EA File Offset: 0x000813EA
		public CssToken.PropertyNameTextReader Name
		{
			get
			{
				return new CssToken.PropertyNameTextReader(this.token);
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x000831F7 File Offset: 0x000813F7
		public bool HasValueFragment
		{
			get
			{
				return !this.token.PropertyList[this.token.CurrentProperty].Value.IsEmpty;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x00083221 File Offset: 0x00081421
		public CssToken.PropertyValueTextReader Value
		{
			get
			{
				return new CssToken.PropertyValueTextReader(this.token);
			}
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0008322E File Offset: 0x0008142E
		public void SetMinorPart(CssToken.PropertyPartMinor newMinorPart)
		{
			this.token.PropertyList[this.token.CurrentProperty].MinorPart = newMinorPart;
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00083251 File Offset: 0x00081451
		[Conditional("DEBUG")]
		private void AssertCurrent()
		{
		}

		// Token: 0x040012CF RID: 4815
		private CssToken token;
	}
}
