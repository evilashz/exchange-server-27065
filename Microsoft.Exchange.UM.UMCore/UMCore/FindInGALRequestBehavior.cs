using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000280 RID: 640
	internal class FindInGALRequestBehavior : PeopleSearchRequestBehavior
	{
		// Token: 0x0600130C RID: 4876 RVA: 0x000551CA File Offset: 0x000533CA
		public FindInGALRequestBehavior(Guid id, CultureInfo culture, Guid userObjectGuid, Guid tenantGuid) : base(id, culture, userObjectGuid, tenantGuid)
		{
			MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Entering FindInGALRequestBehavior constructor", new object[0]);
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x000551EE File Offset: 0x000533EE
		public override SpeechRecognitionEngineType EngineType
		{
			get
			{
				return SpeechRecognitionEngineType.CmdAndControl;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x000551F1 File Offset: 0x000533F1
		public override int MaxAlternates
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x000551F4 File Offset: 0x000533F4
		public override int MaxProcessingTime
		{
			get
			{
				return 60000;
			}
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x000551FC File Offset: 0x000533FC
		public override List<UMGrammar> PrepareGrammars()
		{
			MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Entering GALRequestBehavior.PrepareGrammars", new object[0]);
			List<UMGrammar> list = new List<UMGrammar>();
			ADRecipient adrecipient = base.GetADRecipient();
			DirectoryGrammarHandler directoryGrammarHandler = DirectoryGrammarHandler.CreateHandler(adrecipient.OrganizationId);
			directoryGrammarHandler.PrepareGrammarAsync(adrecipient, base.Culture);
			SearchGrammarFile searchGrammarFile = directoryGrammarHandler.WaitForPrepareGrammarCompletion();
			if (searchGrammarFile == null)
			{
				PIIMessage pii = PIIMessage.Create(PIIType._User, adrecipient.DistinguishedName);
				MobileSpeechRecoTracer.TraceDebug(this, base.Id, pii, "Error retrieving grammar for recipient='_User', grammar='{0}'", new object[]
				{
					directoryGrammarHandler
				});
				throw new SpeechGrammarFetchErrorException(directoryGrammarHandler.ToString());
			}
			MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Grammar path='{0}', Base URI='{1}'", new object[]
			{
				searchGrammarFile.FilePath,
				(searchGrammarFile.BaseUri != null) ? searchGrammarFile.BaseUri.ToString() : "<null>"
			});
			list.Add(new UMGrammar(searchGrammarFile.FilePath, "MobilePeopleSearch", base.Culture, searchGrammarFile.BaseUri, directoryGrammarHandler.DeleteFileAfterUse));
			return list;
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00055300 File Offset: 0x00053500
		public override string ProcessRecoResults(List<IMobileRecognitionResult> results)
		{
			ValidateArgument.NotNull(results, "results");
			MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Entering GALRequestBehavior.ProcessRecoResults", new object[0]);
			List<string> requiredTags = new List<string>
			{
				"SMTP",
				"ObjectGuid"
			};
			return base.ConvertResultsToXml(results, requiredTags);
		}

		// Token: 0x04000C49 RID: 3145
		private const int MaxAlternatesValue = 5;

		// Token: 0x04000C4A RID: 3146
		private const int MaxProcessingTimeValue = 60000;
	}
}
