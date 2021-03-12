using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.LogUploaderProxy
{
	// Token: 0x0200001B RID: 27
	public static class Strings
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00002F08 File Offset: 0x00001108
		static Strings()
		{
			Strings.stringIDs.Add(590305096U, "ErrorPermanentDALException");
			Strings.stringIDs.Add(3093538361U, "ErrorDataStoreUnavailable");
			Strings.stringIDs.Add(797725276U, "ErrorTransientDALExceptionMaxRetries");
			Strings.stringIDs.Add(3879120206U, "LogFileRangeNegativeEndOffset");
			Strings.stringIDs.Add(2833073812U, "ErrorTransientDALExceptionAmbientTransaction");
			Strings.stringIDs.Add(810055759U, "LogFileRangeNegativeStartOffset");
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00002FBB File Offset: 0x000011BB
		public static string FailedToCastToRequestedType(Type t, string fieldName)
		{
			return string.Format(Strings.ResourceManager.GetString("FailedToCastToRequestedType"), t, fieldName);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00002FD3 File Offset: 0x000011D3
		public static string ErrorPermanentDALException
		{
			get
			{
				return Strings.ResourceManager.GetString("ErrorPermanentDALException");
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00002FE4 File Offset: 0x000011E4
		public static string FailedToParseField(string fieldName, Type type)
		{
			return string.Format(Strings.ResourceManager.GetString("FailedToParseField"), fieldName, type);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00002FFC File Offset: 0x000011FC
		public static string ErrorDataStoreUnavailable
		{
			get
			{
				return Strings.ResourceManager.GetString("ErrorDataStoreUnavailable");
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000300D File Offset: 0x0000120D
		public static string UnknownField(string fieldName)
		{
			return string.Format(Strings.ResourceManager.GetString("UnknownField"), fieldName);
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003024 File Offset: 0x00001224
		public static string ErrorTransientDALExceptionMaxRetries
		{
			get
			{
				return Strings.ResourceManager.GetString("ErrorTransientDALExceptionMaxRetries");
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003035 File Offset: 0x00001235
		public static string MalformedLogRangeLine(string line)
		{
			return string.Format(Strings.ResourceManager.GetString("MalformedLogRangeLine"), line);
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000304C File Offset: 0x0000124C
		public static string LogFileRangeNegativeEndOffset
		{
			get
			{
				return Strings.ResourceManager.GetString("LogFileRangeNegativeEndOffset");
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000305D File Offset: 0x0000125D
		public static string ErrorTransientDALExceptionAmbientTransaction
		{
			get
			{
				return Strings.ResourceManager.GetString("ErrorTransientDALExceptionAmbientTransaction");
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000306E File Offset: 0x0000126E
		public static string FailedToInstantiateLogFileInfoFileNotExist(string file)
		{
			return string.Format(Strings.ResourceManager.GetString("FailedToInstantiateLogFileInfoFileNotExist"), file);
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003085 File Offset: 0x00001285
		public static string LogFileRangeNegativeStartOffset
		{
			get
			{
				return Strings.ResourceManager.GetString("LogFileRangeNegativeStartOffset");
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003096 File Offset: 0x00001296
		public static string RequestedCustomDataFieldMissing(string fieldName)
		{
			return string.Format(Strings.ResourceManager.GetString("RequestedCustomDataFieldMissing"), fieldName);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000030B0 File Offset: 0x000012B0
		public static string MergeLogRangesFailed(long otherStartOffset, long otherEndOffset, long startOffset, long endOffset)
		{
			return string.Format(Strings.ResourceManager.GetString("MergeLogRangesFailed"), new object[]
			{
				otherStartOffset,
				otherEndOffset,
				startOffset,
				endOffset
			});
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000030FD File Offset: 0x000012FD
		public static string LogFileRangeWrongOffsets(long start, long end)
		{
			return string.Format(Strings.ResourceManager.GetString("LogFileRangeWrongOffsets"), start, end);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000311F File Offset: 0x0000131F
		public static string GetLogTimeStampFailed(string fileName)
		{
			return string.Format(Strings.ResourceManager.GetString("GetLogTimeStampFailed"), fileName);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003136 File Offset: 0x00001336
		public static string GetLocalizedString(Strings.IDs key)
		{
			return Strings.ResourceManager.GetString(Strings.stringIDs[(uint)key]);
		}

		// Token: 0x04000043 RID: 67
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(6);

		// Token: 0x04000044 RID: 68
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.LogUploaderProxy.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200001C RID: 28
		public enum IDs : uint
		{
			// Token: 0x04000046 RID: 70
			ErrorPermanentDALException = 590305096U,
			// Token: 0x04000047 RID: 71
			ErrorDataStoreUnavailable = 3093538361U,
			// Token: 0x04000048 RID: 72
			ErrorTransientDALExceptionMaxRetries = 797725276U,
			// Token: 0x04000049 RID: 73
			LogFileRangeNegativeEndOffset = 3879120206U,
			// Token: 0x0400004A RID: 74
			ErrorTransientDALExceptionAmbientTransaction = 2833073812U,
			// Token: 0x0400004B RID: 75
			LogFileRangeNegativeStartOffset = 810055759U
		}

		// Token: 0x0200001D RID: 29
		private enum ParamIDs
		{
			// Token: 0x0400004D RID: 77
			FailedToCastToRequestedType,
			// Token: 0x0400004E RID: 78
			FailedToParseField,
			// Token: 0x0400004F RID: 79
			UnknownField,
			// Token: 0x04000050 RID: 80
			MalformedLogRangeLine,
			// Token: 0x04000051 RID: 81
			FailedToInstantiateLogFileInfoFileNotExist,
			// Token: 0x04000052 RID: 82
			RequestedCustomDataFieldMissing,
			// Token: 0x04000053 RID: 83
			MergeLogRangesFailed,
			// Token: 0x04000054 RID: 84
			LogFileRangeWrongOffsets,
			// Token: 0x04000055 RID: 85
			GetLogTimeStampFailed
		}
	}
}
