using System;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Interop;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentFilter;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;

namespace Microsoft.Exchange.Transport.Agent.ContentFilter
{
	// Token: 0x0200001D RID: 29
	internal static class Util
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00006C8C File Offset: 0x00004E8C
		public static void LogContentFilterInitialized()
		{
			Util.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_ContentFilterInitialized, null, null);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006CA0 File Offset: 0x00004EA0
		public static void LogContentFilterNotInitialized(Exception e)
		{
			Util.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_ContentFilterNotInitialized, null, new object[]
			{
				e
			});
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006CCC File Offset: 0x00004ECC
		public static void LogFailedWithUnauthorizedAccess(string path, Exception e)
		{
			Util.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_ContentFilterInitFailedUnauthorizedAccess, null, new object[]
			{
				path,
				e
			});
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006CFC File Offset: 0x00004EFC
		public static void LogFailedWithBadImageFormat(string path, Exception e)
		{
			Util.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_ContentFilterInitFailedBadImageFormat, null, new object[]
			{
				path,
				e
			});
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00006D2C File Offset: 0x00004F2C
		public static void LogFailedFSWatcherAlreadyInitialized(Exception e)
		{
			Util.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_ContentFilterInitFailedFSWatcherAlreadyInitialized, null, new object[]
			{
				e
			});
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00006D58 File Offset: 0x00004F58
		public static void LogFailedInsufficientBuffer(Exception e)
		{
			Util.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_ContentFilterInitFailedInsufficientBuffer, null, new object[]
			{
				e
			});
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00006D82 File Offset: 0x00004F82
		public static void LogQuarantineMailboxIsInvalid()
		{
			Util.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_ContentFilterQuarantineMailboxIsInvalid, null, null);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00006D96 File Offset: 0x00004F96
		public static void LogWrapperNotResponding()
		{
			Util.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_ContentFilterWrapperNotResponding, null, null);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006DAA File Offset: 0x00004FAA
		public static void LogWrapperBeingRecycled()
		{
			Util.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_ContentFilterWrapperBeingRecycled, null, null);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00006DBE File Offset: 0x00004FBE
		public static void LogWrapperSuccessfullyRecycled()
		{
			Util.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_ContentFilterWrapperSuccessfullyRecycled, null, null);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00006DD2 File Offset: 0x00004FD2
		public static void LogWrapperRecycleTimedout()
		{
			Util.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_ContentFilterWrapperRecycleTimedout, null, null);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00006DE8 File Offset: 0x00004FE8
		public static void LogWrapperRecycleError(Exception e)
		{
			Util.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_ContentFilterWrapperRecycleError, null, new object[]
			{
				e
			});
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00006E14 File Offset: 0x00005014
		public static void LogWrapperSendingPingRequest(int numberOfMessages)
		{
			Util.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_ContentFilterWrapperSendingPingRequest, null, new object[]
			{
				numberOfMessages
			});
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00006E44 File Offset: 0x00005044
		public static void LogErrorSubmittingMessage(Exception e)
		{
			Util.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_ContentFilterWrapperErrorSubmittingMessage, null, new object[]
			{
				e
			});
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006E70 File Offset: 0x00005070
		public static void LogExSMimeFailedToInitialize(Exception e)
		{
			Util.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_ExSMimeFailedToInitialize, null, new object[]
			{
				e
			});
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00006E9C File Offset: 0x0000509C
		public static void LogUnexpectedFailureScanningMessage(string messageID, int hresult, string details)
		{
			string periodicKey = hresult.ToString("X", CultureInfo.InvariantCulture);
			Util.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_UnexpectedFailureScanningMessage, periodicKey, new object[]
			{
				messageID,
				details
			});
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006EDC File Offset: 0x000050DC
		public static void LogUpdateModeChangedReinitializingSmartScreen()
		{
			Util.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_AntispamUpdateModeChanged, null, null);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00006EF0 File Offset: 0x000050F0
		public static void LogFailedToReadAntispamUpdateMode(Exception e)
		{
			Util.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_FailedToReadAntispamUpdateMode, e.Message, new object[]
			{
				e
			});
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00006F20 File Offset: 0x00005120
		public static void LogContentFilterInitFailedFileNotFound(Exception e)
		{
			Util.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_ContentFilterInitFailedFileNotFound, null, new object[]
			{
				e
			});
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00006F4C File Offset: 0x0000514C
		public static void InvokeExLapi(ComProxy comProxy, ComProxy.AsyncCompletionCallback callback, ComArguments comArguments, MailItem mailItem, byte[] requestType)
		{
			if (comArguments == null)
			{
				comArguments = new ComArguments();
			}
			comArguments[1] = requestType;
			try
			{
				comProxy.Invoke(callback, comArguments, mailItem);
			}
			catch (COMException ex)
			{
				ExTraceGlobals.InitializationTracer.TraceError<int>(0L, "ComProxy.Invoke() threw a COMException with the following HRESULT: {0}", ex.ErrorCode);
				throw;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00006FA4 File Offset: 0x000051A4
		public static bool IsUnexpectedMessageFailure(uint failureHResult)
		{
			return Array.BinarySearch<uint>(Constants.ExpectedMessageFailureHResults, failureHResult) < 0;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00006FB4 File Offset: 0x000051B4
		public static void InitializeFilter(ComProxy comProxy, ComArguments comArguments)
		{
			Util.InvokeExLapi(comProxy, null, comArguments, null, Constants.RequestTypes.Initialize);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006FC4 File Offset: 0x000051C4
		public static byte[] SerializeByteArrays(int totalLength, params byte[][][] byteArrays)
		{
			if (byteArrays.Length == 0)
			{
				throw new InvalidOperationException("You must pass in at least one byte[][] parameter");
			}
			int num = byteArrays[0].Length;
			byte[] bytes = BitConverter.GetBytes(num);
			byte[] array = new byte[totalLength + bytes.Length];
			int num2 = 0;
			Util.WriteToBuffer(array, ref num2, bytes);
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < byteArrays.Length; j++)
				{
					Util.WriteToBuffer(array, ref num2, byteArrays[j][i]);
				}
			}
			return array;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00007036 File Offset: 0x00005236
		private static void WriteToBuffer(byte[] buffer, ref int bufferIndex, byte[] value)
		{
			value.CopyTo(buffer, bufferIndex);
			bufferIndex += value.Length;
		}

		// Token: 0x040000BC RID: 188
		internal static readonly ExEventLog EventLogger = new ExEventLog(ExTraceGlobals.ScanMessageTracer.Category, "MSExchange Antispam");

		// Token: 0x0200001E RID: 30
		public static class PerformanceCounters
		{
			// Token: 0x060000AD RID: 173 RVA: 0x00007064 File Offset: 0x00005264
			public static void MessageScanned()
			{
				ContentFilterPerfCounters.TotalMessagesScanned.Increment();
			}

			// Token: 0x060000AE RID: 174 RVA: 0x00007071 File Offset: 0x00005271
			public static void MessageNotScanned()
			{
				ContentFilterPerfCounters.TotalMessagesNotScanned.Increment();
			}

			// Token: 0x060000AF RID: 175 RVA: 0x0000707E File Offset: 0x0000527E
			public static void PreExistingSCL()
			{
				ContentFilterPerfCounters.TotalMessagesWithPreExistingSCL.Increment();
			}

			// Token: 0x060000B0 RID: 176 RVA: 0x0000708B File Offset: 0x0000528B
			public static void FilterFailure()
			{
				ContentFilterPerfCounters.TotalMessagesThatCauseFilterFailure.Increment();
			}

			// Token: 0x060000B1 RID: 177 RVA: 0x00007098 File Offset: 0x00005298
			public static void MessageIsAtSCL(int scl)
			{
				if (scl < 0 || scl > 9)
				{
					throw new ArgumentOutOfRangeException("scl", scl, "SCL must be within 0-9");
				}
				Util.PerformanceCounters.SclCounters[scl].Increment();
			}

			// Token: 0x060000B2 RID: 178 RVA: 0x000070C6 File Offset: 0x000052C6
			public static void MessageDeleted()
			{
				ContentFilterPerfCounters.TotalMessagesDeleted.Increment();
			}

			// Token: 0x060000B3 RID: 179 RVA: 0x000070D3 File Offset: 0x000052D3
			public static void MessageRejected()
			{
				ContentFilterPerfCounters.TotalMessagesRejected.Increment();
			}

			// Token: 0x060000B4 RID: 180 RVA: 0x000070E0 File Offset: 0x000052E0
			public static void MessageQuarantined()
			{
				ContentFilterPerfCounters.TotalMessagesQuarantined.Increment();
			}

			// Token: 0x060000B5 RID: 181 RVA: 0x000070ED File Offset: 0x000052ED
			public static void MessageNotScannedDueToOrgSafeSender()
			{
				ContentFilterPerfCounters.TotalMessagesNotScannedDueToOrgSafeSender.Increment();
			}

			// Token: 0x060000B6 RID: 182 RVA: 0x000070FA File Offset: 0x000052FA
			public static void BypassedRecipientDueToPerRecipientSafeSender()
			{
				ContentFilterPerfCounters.TotalBypassedRecipientsDueToPerRecipientSafeSender.Increment();
			}

			// Token: 0x060000B7 RID: 183 RVA: 0x00007107 File Offset: 0x00005307
			public static void BypassedRecipientDueToPerRecipientSafeRecipient()
			{
				ContentFilterPerfCounters.TotalBypassedRecipientsDueToPerRecipientSafeRecipient.Increment();
			}

			// Token: 0x060000B8 RID: 184 RVA: 0x00007114 File Offset: 0x00005314
			public static void MessagesWithInvalidPostmarks()
			{
				ContentFilterPerfCounters.TotalMessagesWithInvalidPostmarks.Increment();
			}

			// Token: 0x060000B9 RID: 185 RVA: 0x00007121 File Offset: 0x00005321
			public static void MessagesWithValidPostmarks()
			{
				ContentFilterPerfCounters.TotalMessagesWithValidPostmarks.Increment();
			}

			// Token: 0x060000BA RID: 186 RVA: 0x00007130 File Offset: 0x00005330
			public static void RemoveCounters()
			{
				ContentFilterPerfCounters.TotalMessagesScanned.RawValue = 0L;
				ContentFilterPerfCounters.TotalMessagesNotScanned.RawValue = 0L;
				ContentFilterPerfCounters.TotalMessagesWithPreExistingSCL.RawValue = 0L;
				ContentFilterPerfCounters.TotalMessagesThatCauseFilterFailure.RawValue = 0L;
				ContentFilterPerfCounters.TotalMessagesDeleted.RawValue = 0L;
				ContentFilterPerfCounters.TotalMessagesRejected.RawValue = 0L;
				ContentFilterPerfCounters.TotalMessagesQuarantined.RawValue = 0L;
				ContentFilterPerfCounters.TotalMessagesNotScannedDueToOrgSafeSender.RawValue = 0L;
				ContentFilterPerfCounters.TotalBypassedRecipientsDueToPerRecipientSafeSender.RawValue = 0L;
				ContentFilterPerfCounters.TotalBypassedRecipientsDueToPerRecipientSafeRecipient.RawValue = 0L;
				ContentFilterPerfCounters.TotalMessagesWithValidPostmarks.RawValue = 0L;
				ContentFilterPerfCounters.TotalMessagesWithInvalidPostmarks.RawValue = 0L;
				foreach (ExPerformanceCounter exPerformanceCounter in Util.PerformanceCounters.SclCounters)
				{
					exPerformanceCounter.RawValue = 0L;
				}
			}

			// Token: 0x040000BD RID: 189
			private static readonly ExPerformanceCounter[] SclCounters = new ExPerformanceCounter[]
			{
				ContentFilterPerfCounters.MessagesAtSCL0,
				ContentFilterPerfCounters.MessagesAtSCL1,
				ContentFilterPerfCounters.MessagesAtSCL2,
				ContentFilterPerfCounters.MessagesAtSCL3,
				ContentFilterPerfCounters.MessagesAtSCL4,
				ContentFilterPerfCounters.MessagesAtSCL5,
				ContentFilterPerfCounters.MessagesAtSCL6,
				ContentFilterPerfCounters.MessagesAtSCL7,
				ContentFilterPerfCounters.MessagesAtSCL8,
				ContentFilterPerfCounters.MessagesAtSCL9
			};
		}
	}
}
