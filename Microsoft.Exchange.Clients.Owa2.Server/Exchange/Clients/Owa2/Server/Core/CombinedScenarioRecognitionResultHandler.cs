using System;
using System.Globalization;
using System.IO;
using System.Web;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000376 RID: 886
	internal class CombinedScenarioRecognitionResultHandler : IMobileSpeechRecognitionResultHandler
	{
		// Token: 0x06001C84 RID: 7300 RVA: 0x00071F20 File Offset: 0x00070120
		public CombinedScenarioRecognitionResultHandler(RequestParameters parameters, UserContext userContext, HttpContext httpContext)
		{
			this.parameters = parameters;
			this.userContext = userContext;
			this.httpContext = httpContext;
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x00071F40 File Offset: 0x00070140
		public void ProcessAndFormatSpeechRecognitionResults(string result, out string jsonResponse, out SpeechRecognitionProcessor.SpeechHttpStatus httpStatus)
		{
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<string>((long)this.GetHashCode(), "Entering CombinedScenariosRecognitionResultHandler.ProcessAndFormatSpeechRecognitionResults with results '{0}'", result);
			jsonResponse = null;
			httpStatus = SpeechRecognitionProcessor.SpeechHttpStatus.Success;
			MobileSpeechRecoResultType mobileSpeechRecoResultType = SpeechRecognitionUtils.ParseMobileScenarioXML(result);
			IMobileSpeechRecognitionResultHandler mobileSpeechRecognitionResultHandler = null;
			switch (mobileSpeechRecoResultType)
			{
			case MobileSpeechRecoResultType.DaySearch:
				mobileSpeechRecognitionResultHandler = new DaySearchRecognitionResultHandler(this.parameters.TimeZone);
				break;
			case MobileSpeechRecoResultType.AppointmentCreation:
				mobileSpeechRecognitionResultHandler = new DateTimeandDurationRecognitionResultHandler(this.parameters.TimeZone);
				break;
			case MobileSpeechRecoResultType.FindPeople:
				mobileSpeechRecognitionResultHandler = new FindPeopleSpeechRecognitionResultHandler(this.parameters, this.userContext, this.httpContext);
				break;
			case MobileSpeechRecoResultType.EmailPeople:
				mobileSpeechRecognitionResultHandler = new EmailPeopleSpeechRecognitionResultHandler(this.parameters, this.userContext, this.httpContext);
				break;
			case MobileSpeechRecoResultType.None:
				mobileSpeechRecognitionResultHandler = null;
				break;
			default:
				ExAssert.RetailAssert(false, "Invalid result type '{0}'", new object[]
				{
					mobileSpeechRecoResultType.ToString()
				});
				break;
			}
			string text = string.Empty;
			CombinedScenarioRecoResult[] obj;
			if (mobileSpeechRecoResultType == MobileSpeechRecoResultType.None)
			{
				obj = new CombinedScenarioRecoResult[0];
			}
			else
			{
				mobileSpeechRecognitionResultHandler.ProcessAndFormatSpeechRecognitionResults(result, out jsonResponse, out httpStatus);
				if (httpStatus != SpeechRecognitionProcessor.SpeechHttpStatus.Success)
				{
					return;
				}
				CombinedScenarioRecoResult combinedScenarioRecoResult = new CombinedScenarioRecoResult();
				combinedScenarioRecoResult.RequestId = this.parameters.RequestId.ToString("N", CultureInfo.InvariantCulture);
				text = this.GetResultTextForLogging(result);
				combinedScenarioRecoResult.Text = text;
				combinedScenarioRecoResult.JsonResponse = jsonResponse;
				combinedScenarioRecoResult.ResultType = CombinedScenarioRecoResult.MapSpeechRecoResultTypeToCombinedRecoResultType(mobileSpeechRecoResultType);
				obj = new CombinedScenarioRecoResult[]
				{
					combinedScenarioRecoResult
				};
			}
			jsonResponse = CombinedScenarioRecoResult.JsonSerialize(obj);
			this.CollectAndLogStatisticsInformation(mobileSpeechRecoResultType, text);
			ExTraceGlobals.SpeechRecognitionTracer.TraceDebug<string>((long)this.GetHashCode(), "Return json from CombinedScenarioResult: '{0}'", jsonResponse);
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x000720CC File Offset: 0x000702CC
		private string GetResultTextForLogging(string result)
		{
			using (XmlReader xmlReader = XmlReader.Create(new StringReader(result)))
			{
				if (xmlReader.ReadToFollowing("Alternate") && xmlReader.MoveToAttribute("text"))
				{
					return xmlReader.ReadContentAsString();
				}
			}
			return string.Empty;
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x0007212C File Offset: 0x0007032C
		private void CollectAndLogStatisticsInformation(MobileSpeechRecoResultType mobileScenarioResultType, string resultText)
		{
			SpeechProcessorStatisticsLogger.SpeechProcessorStatisticsLogRow row = this.CollectStatisticsLog(mobileScenarioResultType, resultText);
			SpeechRecognitionHandler.SpeechProcessorStatisticsLogger.Append(row);
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x00072150 File Offset: 0x00070350
		private SpeechProcessorStatisticsLogger.SpeechProcessorStatisticsLogRow CollectStatisticsLog(MobileSpeechRecoResultType resultType, string resultText)
		{
			return new SpeechProcessorStatisticsLogger.SpeechProcessorStatisticsLogRow
			{
				RequestId = this.parameters.RequestId,
				StartTime = ExDateTime.UtcNow,
				ProcessType = new SpeechLoggerProcessType?(SpeechLoggerProcessType.HandleRecoResults),
				Culture = this.parameters.Culture,
				Tag = this.parameters.Tag,
				TenantGuid = new Guid?(this.parameters.TenantGuid),
				UserObjectGuid = new Guid?(this.parameters.UserObjectGuid),
				TimeZone = this.parameters.TimeZone.ToString(),
				AudioLength = -1,
				MobileSpeechRecoResultType = new MobileSpeechRecoResultType?(resultType),
				ResultText = resultText
			};
		}

		// Token: 0x04001017 RID: 4119
		private UserContext userContext;

		// Token: 0x04001018 RID: 4120
		private RequestParameters parameters;

		// Token: 0x04001019 RID: 4121
		private HttpContext httpContext;
	}
}
