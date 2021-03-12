using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Instrumentation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Diagnostics
{
	// Token: 0x02000011 RID: 17
	internal abstract class ExceptionHandler
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000034B1 File Offset: 0x000016B1
		static ExceptionHandler()
		{
			ExceptionHandler.watch.Start();
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000034EF File Offset: 0x000016EF
		public static ExceptionHandler DataSource
		{
			get
			{
				return ExceptionHandler.dataSource;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000034F6 File Offset: 0x000016F6
		public static ExceptionHandler Proxy
		{
			get
			{
				return ExceptionHandler.proxy;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000034FD File Offset: 0x000016FD
		public static ExceptionHandler Parser
		{
			get
			{
				return ExceptionHandler.parser;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00003504 File Offset: 0x00001704
		public static ExceptionHandler Gray
		{
			get
			{
				return ExceptionHandler.gray;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000350C File Offset: 0x0000170C
		public static void FaultMessage(ComplianceMessage message, FaultDefinition fault, bool isFatal)
		{
			if (fault != null)
			{
				if (message != null)
				{
					fault.IsFatalFailure = isFatal;
					if (message.ProtocolContext.FaultDefinition != null && message.ProtocolContext.FaultDefinition == fault)
					{
						return;
					}
					foreach (FaultRecord faultRecord in fault.Faults)
					{
						if (!faultRecord.Data.ContainsKey("CID"))
						{
							faultRecord.Data["CID"] = message.CorrelationId.ToString();
							faultRecord.Data["MID"] = message.MessageId;
							faultRecord.Data["MSID"] = message.MessageSourceId;
							if (message.MessageTarget != null)
							{
								faultRecord.Data["TID"] = message.MessageTarget.Identifier;
								faultRecord.Data["TTYPE"] = message.MessageTarget.TargetType.ToString();
								if (message.MessageTarget.Mailbox != Guid.Empty)
								{
									faultRecord.Data["TDB"] = message.MessageTarget.Mailbox.ToString();
									faultRecord.Data["TMBX"] = message.MessageTarget.Database.ToString();
								}
							}
							if (message.MessageSource != null)
							{
								faultRecord.Data["STID"] = message.MessageSource.Identifier;
								faultRecord.Data["STTYPE"] = message.MessageSource.TargetType.ToString();
							}
							OrganizationId organizationId;
							if (message.TenantId != null && OrganizationId.TryCreateFromBytes(message.TenantId, Encoding.UTF8, out organizationId))
							{
								faultRecord.Data["TENANT"] = organizationId.OrganizationalUnit.ToString();
								faultRecord.Data["TGUID"] = organizationId.OrganizationalUnit.ObjectGuid.ToString();
							}
							faultRecord.Data["FEX"] = fault.IsFatalFailure.ToString();
						}
					}
					if (isFatal)
					{
						if (message.ProtocolContext.FaultDefinition != null)
						{
							message.ProtocolContext.FaultDefinition.Merge(fault);
						}
						else
						{
							message.ProtocolContext.FaultDefinition = fault;
						}
					}
				}
				MessageLogger.Instance.LogMessageFaulted(message, fault);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000037B4 File Offset: 0x000019B4
		public static bool IsFaulted(ComplianceMessage message)
		{
			return message.ProtocolContext.FaultDefinition != null && message.ProtocolContext.FaultDefinition.IsFatalFailure;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000037D8 File Offset: 0x000019D8
		public static FaultDefinition GetFaultDefinition(ComplianceMessage message)
		{
			return message.ProtocolContext.FaultDefinition;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003848 File Offset: 0x00001A48
		public virtual bool TryRun(Action code, TimeSpan duration, out FaultDefinition faultDefinition, ComplianceMessage context = null, Action<ExceptionHandler.ExceptionData> exceptionHandler = null, CancellationToken cancelToken = default(CancellationToken), double[] retrySchedule = null, [CallerMemberName] string callerMember = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
		{
			bool result2;
			try
			{
				if (retrySchedule == null)
				{
					retrySchedule = new double[]
					{
						10.0,
						20.0,
						30.0
					};
				}
				int num = 0;
				long elapsedMilliseconds = ExceptionHandler.watch.ElapsedMilliseconds;
				ExceptionHandler.ExceptionData args = new ExceptionHandler.ExceptionData
				{
					Exception = null,
					RetryCount = num,
					ShouldRetry = false,
					Context = context
				};
				for (;;)
				{
					args.RetryCount = num;
					bool result = false;
					ExWatson.SendReportOnUnhandledException(delegate()
					{
						result = this.TryRunInternal(code, ref args);
					});
					if (result)
					{
						break;
					}
					if (exceptionHandler != null)
					{
						exceptionHandler(args);
					}
					if (!args.ShouldRetry)
					{
						goto IL_17D;
					}
					int num2 = (int)(duration.TotalMilliseconds * (retrySchedule[(num >= retrySchedule.Length) ? (retrySchedule.Length - 1) : num] / 100.0));
					if (num2 > 0)
					{
						faultDefinition = FaultDefinition.FromException(args.Exception, true, args.ShouldRetry, callerMember, callerFilePath, callerLineNumber);
						ExceptionHandler.FaultMessage(context, faultDefinition, false);
						if (cancelToken.WaitHandle.WaitOne(num2))
						{
							goto IL_17D;
						}
					}
					num++;
					if ((double)(ExceptionHandler.watch.ElapsedMilliseconds - elapsedMilliseconds) >= duration.TotalMilliseconds || elapsedMilliseconds == ExceptionHandler.watch.ElapsedMilliseconds)
					{
						goto IL_17D;
					}
				}
				faultDefinition = null;
				return true;
				IL_17D:
				faultDefinition = new FaultDefinition();
				FaultRecord faultRecord = new FaultRecord();
				faultDefinition.Faults.TryAdd(faultRecord);
				faultRecord.Data["RC"] = args.RetryCount.ToString();
				faultRecord.Data["TEX"] = args.ShouldRetry.ToString();
				faultRecord.Data["EFILE"] = callerFilePath;
				faultRecord.Data["EFUNC"] = callerMember;
				faultRecord.Data["ELINE"] = callerLineNumber.ToString();
				if (args.Exception != null)
				{
					faultRecord.Data["EX"] = args.Exception.ToString();
					LocalizedException ex = args.Exception as LocalizedException;
					if (ex != null)
					{
						faultRecord.Data["UM"] = ex.Message;
					}
				}
				faultDefinition = FaultDefinition.FromException(args.Exception, true, args.ShouldRetry, callerMember, callerFilePath, callerLineNumber);
				ExceptionHandler.FaultMessage(context, faultDefinition, false);
				result2 = false;
			}
			catch (Exception error)
			{
				faultDefinition = FaultDefinition.FromException(error, true, false, callerMember, callerFilePath, callerLineNumber);
				ExceptionHandler.FaultMessage(context, faultDefinition, true);
				throw;
			}
			return result2;
		}

		// Token: 0x0600002D RID: 45
		protected abstract bool TryRunInternal(Action code, ref ExceptionHandler.ExceptionData args);

		// Token: 0x0400001A RID: 26
		private static Stopwatch watch = new Stopwatch();

		// Token: 0x0400001B RID: 27
		private static ExceptionHandler dataSource = new ExceptionHandler.DataSourceExceptionHandler();

		// Token: 0x0400001C RID: 28
		private static ExceptionHandler proxy = new ExceptionHandler.ProxyExceptionHandler();

		// Token: 0x0400001D RID: 29
		private static ExceptionHandler parser = new ExceptionHandler.ParserExceptionHandler();

		// Token: 0x0400001E RID: 30
		private static ExceptionHandler gray = new ExceptionHandler.GrayExceptionHandler();

		// Token: 0x02000012 RID: 18
		public class ExceptionData
		{
			// Token: 0x17000008 RID: 8
			// (get) Token: 0x0600002F RID: 47 RVA: 0x00003B54 File Offset: 0x00001D54
			// (set) Token: 0x06000030 RID: 48 RVA: 0x00003B5C File Offset: 0x00001D5C
			public Exception Exception { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000031 RID: 49 RVA: 0x00003B65 File Offset: 0x00001D65
			// (set) Token: 0x06000032 RID: 50 RVA: 0x00003B6D File Offset: 0x00001D6D
			public int RetryCount { get; set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000033 RID: 51 RVA: 0x00003B76 File Offset: 0x00001D76
			// (set) Token: 0x06000034 RID: 52 RVA: 0x00003B7E File Offset: 0x00001D7E
			public bool ShouldRetry { get; set; }

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000035 RID: 53 RVA: 0x00003B87 File Offset: 0x00001D87
			// (set) Token: 0x06000036 RID: 54 RVA: 0x00003B8F File Offset: 0x00001D8F
			public ComplianceMessage Context { get; set; }
		}

		// Token: 0x02000013 RID: 19
		private class DataSourceExceptionHandler : ExceptionHandler
		{
			// Token: 0x06000038 RID: 56 RVA: 0x00003C00 File Offset: 0x00001E00
			protected override bool TryRunInternal(Action code, ref ExceptionHandler.ExceptionData args)
			{
				bool retry = false;
				bool result = false;
				Exception exception = null;
				try
				{
					GrayException.MapAndReportGrayExceptions(delegate()
					{
						try
						{
							code();
							result = true;
						}
						catch (TransientException exception2)
						{
							retry = true;
							exception = exception2;
						}
						catch (LocalizedException exception3)
						{
							exception = exception3;
						}
					});
				}
				catch (GrayException exception)
				{
					GrayException exception4;
					exception = exception4;
				}
				args.ShouldRetry = retry;
				args.Exception = exception;
				return result;
			}
		}

		// Token: 0x02000014 RID: 20
		private class ProxyExceptionHandler : ExceptionHandler
		{
			// Token: 0x0600003A RID: 58 RVA: 0x00003D24 File Offset: 0x00001F24
			protected override bool TryRunInternal(Action code, ref ExceptionHandler.ExceptionData args)
			{
				bool retry = false;
				bool result = false;
				Exception exception = null;
				try
				{
					GrayException.MapAndReportGrayExceptions(delegate()
					{
						try
						{
							code();
							result = true;
						}
						catch (TimeoutException exception2)
						{
							retry = true;
							exception = exception2;
						}
						catch (CommunicationException ex)
						{
							if (ex is ServerTooBusyException || ex is RetryException || ex is ChannelTerminatedException || ex is CommunicationObjectAbortedException || ex is CommunicationObjectFaultedException || ex is SecurityNegotiationException)
							{
								retry = true;
							}
							exception = ex;
						}
					});
				}
				catch (GrayException exception)
				{
					GrayException exception3;
					exception = exception3;
				}
				args.ShouldRetry = retry;
				args.Exception = exception;
				return result;
			}
		}

		// Token: 0x02000015 RID: 21
		private class ParserExceptionHandler : ExceptionHandler
		{
			// Token: 0x0600003C RID: 60 RVA: 0x00003E18 File Offset: 0x00002018
			protected override bool TryRunInternal(Action code, ref ExceptionHandler.ExceptionData args)
			{
				bool retry = false;
				bool result = false;
				Exception exception = null;
				try
				{
					GrayException.MapAndReportGrayExceptions(delegate()
					{
						try
						{
							code();
							result = true;
						}
						catch (CultureNotFoundException exception2)
						{
							retry = false;
							exception = exception2;
						}
						catch (ParserException exception3)
						{
							retry = false;
							exception = exception3;
						}
					});
				}
				catch (GrayException exception)
				{
					GrayException exception4;
					exception = exception4;
				}
				args.ShouldRetry = retry;
				args.Exception = exception;
				return result;
			}
		}

		// Token: 0x02000016 RID: 22
		private class GrayExceptionHandler : ExceptionHandler
		{
			// Token: 0x0600003E RID: 62 RVA: 0x00003EC0 File Offset: 0x000020C0
			protected override bool TryRunInternal(Action code, ref ExceptionHandler.ExceptionData args)
			{
				bool shouldRetry = false;
				bool result = false;
				Exception exception = null;
				try
				{
					GrayException.MapAndReportGrayExceptions(delegate()
					{
						code();
						result = true;
					});
				}
				catch (GrayException ex)
				{
					exception = ex;
				}
				args.ShouldRetry = shouldRetry;
				args.Exception = exception;
				return result;
			}
		}
	}
}
