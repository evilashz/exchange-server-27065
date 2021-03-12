using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Filtering;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000231 RID: 561
	internal static class ScanResultExtensions
	{
		// Token: 0x06001570 RID: 5488 RVA: 0x0004C314 File Offset: 0x0004A514
		public static string ToClientString(this ClientScanResultStorage storage)
		{
			if (storage == null)
			{
				throw new ArgumentNullException("storage");
			}
			string text = storage.ClassifiedParts.ToClientString();
			string text2 = storage.DlpDetectedClassificationObjects.ToClientString();
			string text3 = storage.DlpDetectedClassificationObjects.ToNamesClientString();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(ScanResultExtensions.ClientScanResultStorageToStringFormat, new object[]
			{
				storage.RecoveryOptions.ToString(),
				text,
				text2,
				text3
			});
			return stringBuilder.ToString();
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0004C398 File Offset: 0x0004A598
		public static ClientScanResultStorage ToClientScanResultStorage(this string clientData)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			ScanResultExtensions.SplitClientData(clientData, out empty, out empty2);
			string[] dcnames = ScanResultExtensions.GetDCNames(empty2);
			if (string.IsNullOrEmpty(empty))
			{
				throw new ClientScanResultParseException("leftClientData is empty");
			}
			string[] array = empty.Split(new char[]
			{
				'?'
			});
			if (array == null || array.Length != 3)
			{
				throw new ClientScanResultParseException("parts are invalid or don't contain 3 elements");
			}
			RecoveryOptions recoveryOptions;
			if (!Enum.TryParse<RecoveryOptions>(array[0], out recoveryOptions) || !(Enum.IsDefined(typeof(RecoveryOptions), recoveryOptions) | recoveryOptions.ToString().Contains(",")))
			{
				throw new ClientScanResultParseException("recoveryOptions is not a valid RecoveryOptions enum value");
			}
			ClientScanResultStorage clientScanResultStorage = new ClientScanResultStorage();
			clientScanResultStorage.RecoveryOptions = (int)recoveryOptions;
			clientScanResultStorage.ClassifiedParts = array[1].ToClassifiedParts();
			clientScanResultStorage.DlpDetectedClassificationObjects = array[2].ToDlpDetectedClassificationObjects();
			if (dcnames.Length != clientScanResultStorage.DlpDetectedClassificationObjects.Count)
			{
				throw new ClientScanResultParseException("Numberof dcNames don't match the number of DlpDetectedClassificationObjects.");
			}
			for (int i = 0; i < dcnames.Length; i++)
			{
				clientScanResultStorage.DlpDetectedClassificationObjects[i].ClassificationName = dcnames[i];
			}
			return clientScanResultStorage;
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0004C4BC File Offset: 0x0004A6BC
		public static string[] GetDCNames(string rightClientData)
		{
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(rightClientData))
			{
				int length = "<DC>".Length;
				int num = "<DC>".Length + "</DC>".Length;
				while (rightClientData.Length > num && rightClientData.StartsWith("<DC>", StringComparison.OrdinalIgnoreCase))
				{
					int num2 = rightClientData.IndexOf("</DC>", StringComparison.OrdinalIgnoreCase);
					int num3 = num2 - length;
					if (num3 >= 0)
					{
						list.Add(rightClientData.Substring(length, num3));
						rightClientData = rightClientData.Substring(num2 + "</DC>".Length);
					}
				}
				if (rightClientData.Length != 0)
				{
					throw new ClientScanResultParseException("DCNames doesn't have valid number of <DC> and </DC> nodes.");
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0004C568 File Offset: 0x0004A768
		public static void SplitClientData(string clientData, out string leftClientData, out string rightClientData)
		{
			leftClientData = string.Empty;
			rightClientData = string.Empty;
			if (!clientData.EndsWith("</DCs>", StringComparison.OrdinalIgnoreCase))
			{
				throw new ClientScanResultParseException("clientData does not end with </DCs>");
			}
			int num = clientData.IndexOf("?<DCs>", StringComparison.OrdinalIgnoreCase);
			if (num < 0)
			{
				throw new ClientScanResultParseException("clientData does not contain ?<DCs>");
			}
			leftClientData = clientData.Substring(0, num);
			int num2 = num + "?<DCs>".Length;
			int num3 = clientData.Length - "</DCs>".Length;
			int num4 = num3 - num2;
			if (num2 >= clientData.Length || num3 <= 0 || num3 >= clientData.Length || num4 < 0)
			{
				throw new ClientScanResultParseException("clientData is invalid as dcsStart and/or dcsEnd are out of range");
			}
			rightClientData = clientData.Substring(num2, num4);
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0004C614 File Offset: 0x0004A814
		public static List<DiscoveredDataClassification> ToDlpDetectedClassificationObjects(this string clientDetectionData)
		{
			if (string.IsNullOrEmpty(clientDetectionData))
			{
				return new List<DiscoveredDataClassification>();
			}
			string[] array = clientDetectionData.Split(new char[]
			{
				'|'
			});
			if (array == null)
			{
				throw new ClientScanResultParseException("classifications are invalid or doesn't contain atleast 1 element");
			}
			List<DiscoveredDataClassification> list = new List<DiscoveredDataClassification>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				DiscoveredDataClassification item = array[i].ToDiscoveredDataClassification();
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0004C684 File Offset: 0x0004A884
		public static string ToClientString(this List<DiscoveredDataClassification> discoveredDataClassifications)
		{
			if (discoveredDataClassifications == null || discoveredDataClassifications.Count == 0)
			{
				return string.Empty;
			}
			return string.Join("|", from discoveredDataClassification in discoveredDataClassifications
			select discoveredDataClassification.ToClientString());
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0004C6C4 File Offset: 0x0004A8C4
		public static string ToNamesClientString(this List<DiscoveredDataClassification> discoveredDataClassifications)
		{
			if (discoveredDataClassifications == null || discoveredDataClassifications.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (DiscoveredDataClassification discoveredDataClassification in discoveredDataClassifications)
			{
				stringBuilder.AppendFormat("{0}{1}{2}", "<DC>", discoveredDataClassification.ClassificationName ?? string.Empty, "</DC>");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0004C750 File Offset: 0x0004A950
		public static DiscoveredDataClassification ToDiscoveredDataClassification(this string classification)
		{
			if (string.IsNullOrEmpty(classification))
			{
				throw new ClientScanResultParseException("classification is null");
			}
			string[] array = classification.Split(new char[]
			{
				'>'
			});
			if (array == null || array.Length != 3)
			{
				throw new ClientScanResultParseException("classificationParts is invalid or doesn't contain 3 elements");
			}
			string text = array[0];
			if (string.IsNullOrEmpty(text))
			{
				throw new ClientScanResultParseException("id is empty");
			}
			uint recommendedMinimumConfidence;
			if (!uint.TryParse(array[1], out recommendedMinimumConfidence))
			{
				throw new ClientScanResultParseException("recMinConfidence is not a uint");
			}
			if (string.IsNullOrEmpty(array[2]))
			{
				throw new ClientScanResultParseException("sourceInfo part of classificationParts is empty");
			}
			return new DiscoveredDataClassification(text, string.Empty, recommendedMinimumConfidence, array[2].ToDataClassificationSourceInfos());
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0004C7F0 File Offset: 0x0004A9F0
		public static string ToClientString(this DiscoveredDataClassification discoveredDataClassification)
		{
			if (discoveredDataClassification == null)
			{
				throw new ArgumentNullException("discoveredDataClassification");
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(ScanResultExtensions.ClassificationStringFormat, discoveredDataClassification.Id, discoveredDataClassification.RecommendedMinimumConfidence, discoveredDataClassification.MatchingSourceInfos.ToClientString());
			return stringBuilder.ToString();
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0004C840 File Offset: 0x0004AA40
		public static List<DataClassificationSourceInfo> ToDataClassificationSourceInfos(this string sourceInfos)
		{
			if (string.IsNullOrEmpty(sourceInfos))
			{
				throw new ClientScanResultParseException("sourceInfos is null");
			}
			string[] array = sourceInfos.Split(new char[]
			{
				'*'
			});
			if (array.Length < 1)
			{
				throw new ClientScanResultParseException("sourceInfosParts doesn't contain atleast 1 element");
			}
			List<DataClassificationSourceInfo> list = new List<DataClassificationSourceInfo>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				DataClassificationSourceInfo item = array[i].ToDataClassificationSourceInfo();
				list.Add(item);
			}
			return list;
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x0004C8B8 File Offset: 0x0004AAB8
		public static string ToClientString(this List<DataClassificationSourceInfo> sourceInfos)
		{
			if (sourceInfos == null || sourceInfos.Count == 0)
			{
				throw new ArgumentNullException("sourceInfos");
			}
			return string.Join("*", from sourceInfo in sourceInfos
			select sourceInfo.ToClientString());
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0004C908 File Offset: 0x0004AB08
		public static DataClassificationSourceInfo ToDataClassificationSourceInfo(this string sourceInfo)
		{
			if (string.IsNullOrEmpty(sourceInfo))
			{
				throw new ClientScanResultParseException("sourceInfoParts is null");
			}
			string[] array = sourceInfo.Split(new char[]
			{
				'\\'
			});
			if (array == null || array.Length != 5)
			{
				throw new ClientScanResultParseException(string.Format("sourceInfoParts: {0} doesn't contain 5 elements", sourceInfo));
			}
			int num;
			if (!int.TryParse(array[0], out num))
			{
				throw new ClientScanResultParseException(string.Format("id:{0} is not an int", num));
			}
			string text = array[1];
			if (string.IsNullOrEmpty(text))
			{
				throw new ClientScanResultParseException("name is empty");
			}
			string text2 = array[2];
			if (string.IsNullOrEmpty(text2))
			{
				text2 = text;
			}
			int num2;
			if (!int.TryParse(array[3], out num2))
			{
				throw new ClientScanResultParseException(string.Format("count: {0} is not an int", num2));
			}
			uint num3;
			if (!uint.TryParse(array[4], out num3))
			{
				throw new ClientScanResultParseException(string.Format("confidence: {0} is not a uint", num3));
			}
			return new DataClassificationSourceInfo(num, text, text2, num2, num3);
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0004C9F4 File Offset: 0x0004ABF4
		public static string ToClientString(this DataClassificationSourceInfo sourceInfo)
		{
			if (sourceInfo == null)
			{
				throw new ArgumentNullException("sourceInfo");
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(ScanResultExtensions.SourceInfoStringFormat, new object[]
			{
				sourceInfo.SourceId,
				sourceInfo.SourceName,
				sourceInfo.SourceName.Equals(sourceInfo.TopLevelSourceName, StringComparison.OrdinalIgnoreCase) ? string.Empty : sourceInfo.TopLevelSourceName,
				sourceInfo.Count,
				sourceInfo.ConfidenceLevel
			});
			return stringBuilder.ToString();
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0004CA88 File Offset: 0x0004AC88
		public static List<string> ToClassifiedParts(this string clientPartsData)
		{
			if (string.IsNullOrEmpty(clientPartsData))
			{
				return new List<string>();
			}
			List<string> list = new List<string>();
			string[] array = clientPartsData.Split(new char[]
			{
				'|'
			});
			for (int i = 0; i < array.Length; i++)
			{
				bool flag = false;
				if (!string.IsNullOrEmpty(array[i]))
				{
					flag = array[i].Equals(ScanResultStorageProvider.MessageBodyName, StringComparison.OrdinalIgnoreCase);
					if (!flag)
					{
						string[] array2 = array[i].Split(new char[]
						{
							':'
						});
						flag = (array2 != null && array2.Length == 2 && !string.IsNullOrEmpty(array2[0]) && !string.IsNullOrEmpty(array2[1]));
					}
				}
				if (!flag)
				{
					throw new ClientScanResultParseException(string.Format("parts[{0}]: {1} doesn't match either Message Body or attachmentName:attachmentId formats.", i, array[i]));
				}
				list.Add(array[i]);
			}
			return list;
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0004CB5A File Offset: 0x0004AD5A
		public static string ToClientString(this List<string> clientParts)
		{
			if (clientParts == null)
			{
				return string.Empty;
			}
			return string.Join("|", clientParts);
		}

		// Token: 0x04000B85 RID: 2949
		private const string ClientScanResultStorageToStringSeparator = "?";

		// Token: 0x04000B86 RID: 2950
		private const char ClientScanResultStorageToStringSeparatorChar = '?';

		// Token: 0x04000B87 RID: 2951
		private const string DataClassificationArrayStartNodeName = "<DCs>";

		// Token: 0x04000B88 RID: 2952
		private const string DataClassificationArrayEndNodeName = "</DCs>";

		// Token: 0x04000B89 RID: 2953
		private const string DataClassificationArrayElementStartNodeName = "<DC>";

		// Token: 0x04000B8A RID: 2954
		private const string DataClassificationArrayElementEndNodeName = "</DC>";

		// Token: 0x04000B8B RID: 2955
		private const string ClassifiedPartsArraySeparator = "|";

		// Token: 0x04000B8C RID: 2956
		private const char ClassifiedPartsArraySeparatorChar = '|';

		// Token: 0x04000B8D RID: 2957
		private const string ClassificationArraySeparator = "|";

		// Token: 0x04000B8E RID: 2958
		private const char ClassificationArraySeparatorChar = '|';

		// Token: 0x04000B8F RID: 2959
		private const string ClassificationPropertiesSeparator = ">";

		// Token: 0x04000B90 RID: 2960
		private const char ClassificationPropertiesSeparatorChar = '>';

		// Token: 0x04000B91 RID: 2961
		private const string SourceInfoArraySeparator = "*";

		// Token: 0x04000B92 RID: 2962
		private const char SourceInfoArraySeparatorChar = '*';

		// Token: 0x04000B93 RID: 2963
		private const string SourceInfoPropertiesSeparator = "\\";

		// Token: 0x04000B94 RID: 2964
		private const char SourceInfoPropertiesSeparatorChar = '\\';

		// Token: 0x04000B95 RID: 2965
		private static readonly string ClientScanResultStorageToStringFormat = string.Join<string>("?", new string[]
		{
			"{0}",
			"{1}",
			"{2}",
			string.Format("{0}{1}{2}", "<DCs>", "{3}", "</DCs>")
		});

		// Token: 0x04000B96 RID: 2966
		private static readonly string ClassificationStringFormat = string.Join(">", new string[]
		{
			"{0}",
			"{1}",
			"{2}"
		});

		// Token: 0x04000B97 RID: 2967
		private static readonly string SourceInfoStringFormat = string.Join("\\", new string[]
		{
			"{0}",
			"{1}",
			"{2}",
			"{3}",
			"{4}"
		});
	}
}
