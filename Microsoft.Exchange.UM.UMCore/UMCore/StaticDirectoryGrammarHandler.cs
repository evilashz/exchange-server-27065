using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000274 RID: 628
	internal class StaticDirectoryGrammarHandler : DirectoryGrammarHandler
	{
		// Token: 0x0600129E RID: 4766 RVA: 0x00052F20 File Offset: 0x00051120
		public StaticDirectoryGrammarHandler(OrganizationId orgId) : base(orgId)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "Creating static directory grammar handler for org '{0}'", new object[]
			{
				base.OrgId
			});
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x00052F55 File Offset: 0x00051155
		public override bool DeleteFileAfterUse
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00052F58 File Offset: 0x00051158
		public override string ToString()
		{
			if (this.grammarIdentifier != null)
			{
				return this.grammarIdentifier.ToString();
			}
			return base.OrgId.ToString();
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00052F7C File Offset: 0x0005117C
		public override void PrepareGrammarAsync(CallContext callContext, DirectoryGrammarHandler.GrammarType grammarType)
		{
			ValidateArgument.NotNull(callContext, "callContext");
			this.grammarType = grammarType;
			switch (grammarType)
			{
			case DirectoryGrammarHandler.GrammarType.User:
				this.grammarIdentifier = GalGrammarFile.GetGrammarIdentifier(callContext);
				break;
			case DirectoryGrammarHandler.GrammarType.DL:
				this.grammarIdentifier = DistributionListGrammarFile.GetGrammarIdentifier(callContext);
				break;
			default:
				ExAssert.RetailAssert(false, "Unknown grammar type {0}", new object[]
				{
					grammarType
				});
				break;
			}
			this.grammarFetcher = new LargeGrammarFetcher(this.grammarIdentifier);
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00052FF8 File Offset: 0x000511F8
		public override void PrepareGrammarAsync(ADRecipient recipient, CultureInfo culture)
		{
			ValidateArgument.NotNull(recipient, "recipient");
			ValidateArgument.NotNull(culture, "culture");
			this.grammarType = DirectoryGrammarHandler.GrammarType.User;
			this.grammarIdentifier = new GrammarIdentifier(base.OrgId, culture, GrammarFileNames.GetFileNameForGALUser());
			this.grammarFetcher = new LargeGrammarFetcher(this.grammarIdentifier);
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0005304C File Offset: 0x0005124C
		public override SearchGrammarFile WaitForPrepareGrammarCompletion()
		{
			SearchGrammarFile result = null;
			switch (this.grammarType)
			{
			case DirectoryGrammarHandler.GrammarType.User:
			{
				LargeGrammarFetcher.FetchResult fetchResult = this.grammarFetcher.Wait(Constants.GalGrammarFetchTimeout);
				if (fetchResult.Status == LargeGrammarFetcher.FetchStatus.Success)
				{
					result = new GalGrammarFile(this.grammarIdentifier.Culture, fetchResult.FilePath);
				}
				break;
			}
			case DirectoryGrammarHandler.GrammarType.DL:
			{
				LargeGrammarFetcher.FetchResult fetchResult = this.grammarFetcher.Wait(Constants.DLGramamrFetchTimeout);
				if (fetchResult.Status == LargeGrammarFetcher.FetchStatus.Success)
				{
					result = new DistributionListGrammarFile(this.grammarIdentifier.Culture, fetchResult.FilePath);
				}
				break;
			}
			default:
				ExAssert.RetailAssert(false, "Unknown grammar type {0}", new object[]
				{
					this.grammarType
				});
				break;
			}
			return result;
		}

		// Token: 0x04000C2D RID: 3117
		private DirectoryGrammarHandler.GrammarType grammarType;

		// Token: 0x04000C2E RID: 3118
		private GrammarIdentifier grammarIdentifier;

		// Token: 0x04000C2F RID: 3119
		private LargeGrammarFetcher grammarFetcher;
	}
}
