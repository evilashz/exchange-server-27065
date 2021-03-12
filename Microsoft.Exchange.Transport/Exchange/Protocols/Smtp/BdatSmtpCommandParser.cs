using System;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Logging;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000476 RID: 1142
	internal static class BdatSmtpCommandParser
	{
		// Token: 0x06003492 RID: 13458 RVA: 0x000D672C File Offset: 0x000D492C
		public static ParseResult Parse(CommandContext context, SmtpInSessionState state, long totalChunkSize, out long chunkSize)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ArgumentValidator.ThrowIfNull("state", state);
			chunkSize = 0L;
			Offset offset;
			if (!context.GetNextArgumentOffset(out offset))
			{
				return ParseResult.InvalidArguments;
			}
			string @string = Encoding.ASCII.GetString(context.Command, offset.Start, offset.Length);
			long num;
			ParseResult parseResult = BdatSmtpCommandParser.ValidateChunkSizeArg(state.Tracer, @string, out num);
			if (parseResult.IsFailed)
			{
				return parseResult;
			}
			parseResult = BdatSmtpCommandParser.ValidateOptionalArg(context, state.Tracer);
			if (parseResult.IsFailed)
			{
				return parseResult;
			}
			parseResult = BdatSmtpCommandParser.PerformLastChunkChecks(state.Tracer, parseResult != ParseResult.MoreDataRequired, num, state.FirstBdatCall);
			if (parseResult.IsFailed)
			{
				return parseResult;
			}
			if (!BdatSmtpCommandParser.ValidateChunkSizeLimits(state.ProtocolLogSession, totalChunkSize, num))
			{
				parseResult = ParseResult.MessageTooLarge;
				if (state.ReceivePerfCounters != null)
				{
					state.ReceivePerfCounters.MessagesRefusedForSize.Increment();
				}
			}
			else
			{
				chunkSize = num;
			}
			return parseResult;
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x000D6810 File Offset: 0x000D4A10
		public static bool IsMessageSizeExceeded(bool bypassMessageSizeLimit, long totalChunkSize, long chunkSize, long originalMessageSize, long messageSizeLimit, IProtocolLogSession protocolLogSession, ITracer tracer)
		{
			ArgumentValidator.ThrowIfNull("protocolLogSession", protocolLogSession);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			if (bypassMessageSizeLimit)
			{
				return false;
			}
			long num = totalChunkSize + chunkSize;
			if (Math.Min(num, originalMessageSize) > messageSizeLimit)
			{
				protocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "NextTotalChunkSize: {0} > MessageSizeLimit: {1}", new object[]
				{
					num,
					messageSizeLimit
				});
				tracer.TraceDebug<long>(0L, "Reject message based on size: nextTotalChunkSize={0}.", num);
				return true;
			}
			return false;
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x000D6884 File Offset: 0x000D4A84
		public static bool IsLastChunk(ParsingStatus status)
		{
			return status == ParsingStatus.Complete;
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x000D688C File Offset: 0x000D4A8C
		private static ParseResult ValidateChunkSizeArg(ITracer tracer, string chunkSizeArg, out long chunkSize)
		{
			if (long.TryParse(chunkSizeArg, out chunkSize) && chunkSize >= 0L)
			{
				return ParseResult.ParsingComplete;
			}
			if (chunkSizeArg.Length <= 20 && chunkSizeArg.All(new Func<char, bool>(char.IsDigit)))
			{
				tracer.TraceDebug(0L, "Size Overflow");
				return ParseResult.MessageTooLarge;
			}
			tracer.TraceDebug(0L, "Reject message based on invalid size argument. Drop connection.");
			return ParseResult.InvalidArguments;
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x000D68F4 File Offset: 0x000D4AF4
		private static ParseResult ValidateOptionalArg(CommandContext context, ITracer tracer)
		{
			Offset offset;
			if (!context.GetNextArgumentOffset(out offset))
			{
				return ParseResult.MoreDataRequired;
			}
			bool flag = BufferParser.CompareArg(BdatSmtpCommandParser.LAST, context.Command, offset.Start, offset.Length);
			if (!flag || context.HasArguments)
			{
				tracer.TraceDebug(0L, "Reject message based on invalid command. Drop connection.");
				return ParseResult.InvalidArguments;
			}
			return ParseResult.ParsingComplete;
		}

		// Token: 0x06003497 RID: 13463 RVA: 0x000D6954 File Offset: 0x000D4B54
		private static ParseResult PerformLastChunkChecks(ITracer tracer, bool isLastChunk, long chunkSize, bool firstBdatCall)
		{
			if ((firstBdatCall && isLastChunk && chunkSize == 0L) || (!isLastChunk && chunkSize == 0L))
			{
				tracer.TraceDebug(0L, "Reject message based on invalid command. Drop connection.");
				return ParseResult.InvalidArguments;
			}
			if (!isLastChunk)
			{
				return ParseResult.MoreDataRequired;
			}
			return ParseResult.ParsingComplete;
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x000D699C File Offset: 0x000D4B9C
		private static bool ValidateChunkSizeLimits(IProtocolLogSession protocolLogSession, long totalChunkSize, long currentChunkSize)
		{
			long num = totalChunkSize + currentChunkSize;
			if (num < totalChunkSize)
			{
				protocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Total Chunk Overflow");
				return false;
			}
			if (num > 2147483647L)
			{
				protocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "NextTotalChunkSize: {0} > Int32.MaxValue: {1}", new object[]
				{
					num,
					int.MaxValue
				});
				return false;
			}
			return true;
		}

		// Token: 0x04001ABF RID: 6847
		public const string CommandKeyword = "BDAT";

		// Token: 0x04001AC0 RID: 6848
		private static readonly byte[] LAST = Util.AsciiStringToBytes("last");
	}
}
