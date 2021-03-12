using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000281 RID: 641
	internal class FindInPersonalContactsRequestBehavior : PeopleSearchRequestBehavior
	{
		// Token: 0x06001312 RID: 4882 RVA: 0x00055355 File Offset: 0x00053555
		public FindInPersonalContactsRequestBehavior(Guid id, CultureInfo culture, Guid userObjectGuid, Guid tenantGuid) : base(id, culture, userObjectGuid, tenantGuid)
		{
			MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Entering FindInPersonalContactsRequestBehavior constructor", new object[0]);
			this.recipient = base.GetADRecipient();
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x00055385 File Offset: 0x00053585
		public override SpeechRecognitionEngineType EngineType
		{
			get
			{
				return SpeechRecognitionEngineType.CmdAndControl;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x00055388 File Offset: 0x00053588
		public override int MaxAlternates
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x0005538B File Offset: 0x0005358B
		public override int MaxProcessingTime
		{
			get
			{
				return 60000;
			}
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00055394 File Offset: 0x00053594
		public override List<UMGrammar> PrepareGrammars()
		{
			MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Entering FindInPersonalContactsRequestBehavior.PrepareGrammars", new object[0]);
			List<UMGrammar> list = new List<UMGrammar>();
			using (NonBlockingReader nonBlockingReader = new NonBlockingReader(new NonBlockingReader.Operation(this.PopulateGrammarFile), this, TimeSpan.FromMilliseconds(6000.0), new NonBlockingReader.TimeoutCallback(this.TimedOutPopulateGrammarFile)))
			{
				nonBlockingReader.StartAsyncOperation();
				if (nonBlockingReader.WaitForCompletion())
				{
					if (this.exceptionThrown != null)
					{
						Exception ex = new PersonalContactsSpeechGrammarErrorException(this.recipient.PrimarySmtpAddress.ToString(), this.exceptionThrown);
						MobileSpeechRecoTracer.TraceError(this, base.Id, "An exception occurred in PopulateGrammarFiler '{0}'", new object[]
						{
							ex
						});
						throw ex;
					}
					list.Add(new UMGrammar(this.grammarFile.FilePath, "MobilePeopleSearch", base.Culture, this.grammarFile.BaseUri, true));
				}
			}
			return list;
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00055494 File Offset: 0x00053694
		public override string ProcessRecoResults(List<IMobileRecognitionResult> results)
		{
			ValidateArgument.NotNull(results, "results");
			MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Entering FindInPersonalContactsRequestBehavior.ProcessRecoResults", new object[0]);
			List<string> requiredTags = new List<string>
			{
				"PersonId",
				"GALLinkID"
			};
			return base.ConvertResultsToXml(results, requiredTags);
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x000554EC File Offset: 0x000536EC
		private void PopulateGrammarFile(object state)
		{
			try
			{
				MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Entering FindInPersonalContactsRequestBehavior.PopulateGrammarFile", new object[0]);
				this.exceptionThrown = null;
				using (UMMailboxRecipient ummailboxRecipient = UMRecipient.Factory.FromADRecipient<UMMailboxRecipient>(this.recipient))
				{
					this.grammarFile = new MowaPersonalContactsGrammarFile(ummailboxRecipient, base.Culture);
				}
				MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Grammar path='{0}', Base URI='{1}'", new object[]
				{
					this.grammarFile.FilePath,
					(this.grammarFile.BaseUri != null) ? this.grammarFile.BaseUri.ToString() : "<null>"
				});
			}
			catch (Exception ex)
			{
				MobileSpeechRecoTracer.TraceError(this, base.Id, "Exception in Populate Grammar file '{0}'", new object[]
				{
					ex
				});
				this.exceptionThrown = ex;
			}
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x000555DC File Offset: 0x000537DC
		private void TimedOutPopulateGrammarFile(object state)
		{
			MobileSpeechRecoTracer.TraceDebug(this, base.Id, "Entering FindInPersonalContactsRequestBehavior.TimedOutPopulateGrammarFile. Timeout retrieving grammar for recipient='{0}'", new object[]
			{
				base.UserObjectGuid
			});
			throw new PersonalContactsSpeechGrammarTimeoutException(this.recipient.PrimarySmtpAddress.ToString());
		}

		// Token: 0x04000C4B RID: 3147
		private const int MaxAlternatesValue = 5;

		// Token: 0x04000C4C RID: 3148
		private const int MaxProcessingTimeValue = 60000;

		// Token: 0x04000C4D RID: 3149
		private const int MaxTimeoutForGrammarGeneration = 6000;

		// Token: 0x04000C4E RID: 3150
		private MowaPersonalContactsGrammarFile grammarFile;

		// Token: 0x04000C4F RID: 3151
		private ADRecipient recipient;

		// Token: 0x04000C50 RID: 3152
		private Exception exceptionThrown;
	}
}
