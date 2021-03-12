using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200084A RID: 2122
	[ClassAccessLevel(AccessLevel.Consumer)]
	internal class ForwardReplyHeaderOptions
	{
		// Token: 0x17001661 RID: 5729
		// (get) Token: 0x06004F16 RID: 20246 RVA: 0x0014B78E File Offset: 0x0014998E
		public int ComposeFontSize
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17001662 RID: 5730
		// (get) Token: 0x06004F17 RID: 20247 RVA: 0x0014B791 File Offset: 0x00149991
		public string ComposeFontColor
		{
			get
			{
				return "#000000";
			}
		}

		// Token: 0x17001663 RID: 5731
		// (get) Token: 0x06004F18 RID: 20248 RVA: 0x0014B798 File Offset: 0x00149998
		public string ComposeFontName
		{
			get
			{
				return "Tahoma";
			}
		}

		// Token: 0x17001664 RID: 5732
		// (get) Token: 0x06004F19 RID: 20249 RVA: 0x0014B79F File Offset: 0x0014999F
		public bool ComposeFontBold
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001665 RID: 5733
		// (get) Token: 0x06004F1A RID: 20250 RVA: 0x0014B7A2 File Offset: 0x001499A2
		public bool ComposeFontUnderline
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001666 RID: 5734
		// (get) Token: 0x06004F1B RID: 20251 RVA: 0x0014B7A5 File Offset: 0x001499A5
		public bool ComposeFontItalics
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001667 RID: 5735
		// (get) Token: 0x06004F1C RID: 20252 RVA: 0x0014B7A8 File Offset: 0x001499A8
		public bool AutoAddSignature
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001668 RID: 5736
		// (get) Token: 0x06004F1D RID: 20253 RVA: 0x0014B7AB File Offset: 0x001499AB
		public string SignatureText
		{
			get
			{
				return "";
			}
		}

		// Token: 0x04002B22 RID: 11042
		private const string ComposeFontNameValue = "Tahoma";

		// Token: 0x04002B23 RID: 11043
		private const int ComposeFontSizeValue = 2;

		// Token: 0x04002B24 RID: 11044
		private const string ComposeFontColorValue = "#000000";

		// Token: 0x04002B25 RID: 11045
		private const bool ComposeFontBoldValue = true;

		// Token: 0x04002B26 RID: 11046
		private const bool ComposeFontUnderlineValue = false;

		// Token: 0x04002B27 RID: 11047
		private const bool ComposeFontItalicsValue = false;

		// Token: 0x04002B28 RID: 11048
		private const bool AutoAddSignatureValue = false;

		// Token: 0x04002B29 RID: 11049
		private const string SignatureTextValue = "";
	}
}
