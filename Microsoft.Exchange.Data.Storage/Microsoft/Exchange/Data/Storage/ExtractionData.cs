using System;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020008E1 RID: 2273
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExtractionData
	{
		// Token: 0x0600552A RID: 21802 RVA: 0x00161AAE File Offset: 0x0015FCAE
		public ExtractionData()
		{
			this.bodyFragment = null;
			this.childDisclaimer = null;
			this.childUniqueBody = null;
			this.originalTagInfo = null;
		}

		// Token: 0x0600552B RID: 21803 RVA: 0x00161AD2 File Offset: 0x0015FCD2
		public ExtractionData(BodyFragmentInfo childFragment, BodyTagInfo parentBodyTag)
		{
			childFragment.ExtractNestedBodyParts(parentBodyTag, out this.bodyFragment, out this.childUniqueBody, out this.childDisclaimer);
			this.originalTagInfo = parentBodyTag;
		}

		// Token: 0x170017BD RID: 6077
		// (get) Token: 0x0600552C RID: 21804 RVA: 0x00161AFA File Offset: 0x0015FCFA
		public bool IsFormatReliable
		{
			get
			{
				return this.BodyFragment != null && this.BodyFragment.BodyTag == this.OriginalTagInfo;
			}
		}

		// Token: 0x170017BE RID: 6078
		// (get) Token: 0x0600552D RID: 21805 RVA: 0x00161B1C File Offset: 0x0015FD1C
		public BodyFragmentInfo BodyFragment
		{
			get
			{
				return this.bodyFragment;
			}
		}

		// Token: 0x170017BF RID: 6079
		// (get) Token: 0x0600552E RID: 21806 RVA: 0x00161B24 File Offset: 0x0015FD24
		public BodyTagInfo OriginalTagInfo
		{
			get
			{
				return this.originalTagInfo;
			}
		}

		// Token: 0x170017C0 RID: 6080
		// (get) Token: 0x0600552F RID: 21807 RVA: 0x00161B2C File Offset: 0x0015FD2C
		public FragmentInfo ChildUniqueBody
		{
			get
			{
				return this.childUniqueBody;
			}
		}

		// Token: 0x170017C1 RID: 6081
		// (get) Token: 0x06005530 RID: 21808 RVA: 0x00161B34 File Offset: 0x0015FD34
		public FragmentInfo ChildDisclaimer
		{
			get
			{
				return this.childDisclaimer;
			}
		}

		// Token: 0x04002DC2 RID: 11714
		private readonly BodyFragmentInfo bodyFragment;

		// Token: 0x04002DC3 RID: 11715
		private readonly BodyTagInfo originalTagInfo;

		// Token: 0x04002DC4 RID: 11716
		private readonly FragmentInfo childUniqueBody;

		// Token: 0x04002DC5 RID: 11717
		private readonly FragmentInfo childDisclaimer;
	}
}
