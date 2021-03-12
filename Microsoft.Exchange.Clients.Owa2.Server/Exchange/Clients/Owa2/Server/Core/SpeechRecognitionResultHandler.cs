using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000382 RID: 898
	internal static class SpeechRecognitionResultHandler
	{
		// Token: 0x06001CCB RID: 7371 RVA: 0x00073480 File Offset: 0x00071680
		public static void HandleRecoResult(string result, RequestParameters parameters, HttpContext httpContext, UserContext userContext, out string jsonResponse, out SpeechRecognitionProcessor.SpeechHttpStatus httpStatus)
		{
			jsonResponse = null;
			httpStatus = SpeechRecognitionProcessor.SpeechHttpStatus.Success;
			if (string.IsNullOrEmpty(result))
			{
				return;
			}
			IMobileSpeechRecognitionResultHandler mobileSpeechRecognitionResultHandler = null;
			switch (parameters.RequestType)
			{
			case MobileSpeechRecoRequestType.FindPeople:
				mobileSpeechRecognitionResultHandler = new FindPeopleSpeechRecognitionResultHandler(parameters, userContext, httpContext);
				goto IL_95;
			case MobileSpeechRecoRequestType.CombinedScenarios:
				mobileSpeechRecognitionResultHandler = new CombinedScenarioRecognitionResultHandler(parameters, userContext, httpContext);
				goto IL_95;
			case MobileSpeechRecoRequestType.DaySearch:
				mobileSpeechRecognitionResultHandler = new DaySearchRecognitionResultHandler(parameters.TimeZone);
				goto IL_95;
			case MobileSpeechRecoRequestType.AppointmentCreation:
				mobileSpeechRecognitionResultHandler = new DateTimeandDurationRecognitionResultHandler(parameters.TimeZone);
				goto IL_95;
			}
			ExAssert.RetailAssert(false, "Invalid request type '{0}'", new object[]
			{
				parameters.RequestType
			});
			IL_95:
			mobileSpeechRecognitionResultHandler.ProcessAndFormatSpeechRecognitionResults(result, out jsonResponse, out httpStatus);
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x00073530 File Offset: 0x00071730
		public static string JsonSerialize(object obj)
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(obj.GetType());
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				dataContractJsonSerializer.WriteObject(memoryStream, obj);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				using (StreamReader streamReader = new StreamReader(memoryStream))
				{
					result = streamReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x000735A4 File Offset: 0x000717A4
		public static object JsonDeserialize(string result, Type targetType)
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(targetType);
			object result2;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(result)))
			{
				result2 = dataContractJsonSerializer.ReadObject(memoryStream);
			}
			return result2;
		}
	}
}
