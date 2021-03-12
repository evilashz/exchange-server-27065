using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using System.Web;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x0200021B RID: 539
	internal class PswsErrorHandling : TaskIOPipelineBase, ITaskModule, ICriticalFeature
	{
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x0003CAE4 File Offset: 0x0003ACE4
		// (set) Token: 0x060012BD RID: 4797 RVA: 0x0003CAEC File Offset: 0x0003ACEC
		private List<LocalizedString> Warnings { get; set; }

		// Token: 0x060012BE RID: 4798 RVA: 0x0003CAF5 File Offset: 0x0003ACF5
		public PswsErrorHandling(TaskContext context)
		{
			this.context = context;
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0003CB04 File Offset: 0x0003AD04
		bool ICriticalFeature.IsCriticalException(Exception ex)
		{
			return false;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0003CB08 File Offset: 0x0003AD08
		public void Init(ITaskEvent task)
		{
			task.Error += this.Task_Error;
			task.Stop += this.OnStop;
			task.Release += this.OnRelease;
			this.context.CommandShell.PrependTaskIOPipelineHandler(this);
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0003CB5C File Offset: 0x0003AD5C
		public void Dispose()
		{
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0003CB5E File Offset: 0x0003AD5E
		public override bool ShouldContinue(string query, string caption, ref bool yesToAll, ref bool noToAll, out bool? output)
		{
			output = null;
			PswsErrorHandling.SendErrorToClient(PswsErrorCode.CmdletShouldContinue, null, query);
			return true;
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x0003CB75 File Offset: 0x0003AD75
		public override bool ShouldProcess(string verboseDescription, string verboseWarning, string caption, out bool? output)
		{
			output = new bool?(true);
			return false;
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x0003CB88 File Offset: 0x0003AD88
		internal static void SendErrorToClient(PswsErrorCode errorCode, Exception exception, string additionalInfo)
		{
			ExTraceGlobals.PublicPluginAPITracer.TraceDebug<PswsErrorCode, Exception, string>(0L, "[PswsErrorHandling.SendErrorToClient] Error Code = {0}, Exception = \"{1}\", additionalInfo = \"{2}\".", errorCode, exception, additionalInfo);
			try
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext != null)
				{
					HttpResponse response = httpContext.Response;
					if (response != null)
					{
						if (response.Headers["X-Psws-ErrorCode"] == null)
						{
							response.AddHeader("X-Psws-ErrorCode", ((int)PswsErrorHandling.TranslateErrorCode(errorCode, exception, additionalInfo)).ToString());
							HttpLogger.SafeSetLogger(ServiceCommonMetadata.ErrorCode, errorCode);
							if (exception != null)
							{
								string text = exception.GetType() + "," + exception.Message;
								if (text.Length > 500)
								{
									text = text.Substring(0, 500 - "...(truncated)".Length);
									text += "...(truncated)";
								}
								response.AddHeader("X-Psws-Exception", text);
								HttpLogger.SafeAppendGenericInfo("PswsExceptionInfo", text);
							}
							if (!string.IsNullOrWhiteSpace(additionalInfo))
							{
								response.AddHeader("X-Psws-ErrorInfo", additionalInfo);
								HttpLogger.SafeAppendGenericInfo("PswsErrorAdditionalInfo", additionalInfo);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.PublicPluginAPITracer.TraceDebug<Exception>(0L, "[PswsErrorHandling.SendErrorToClient] Exception {0}.", ex);
				HttpLogger.SafeAppendGenericError("SendErrorToClientError", ex, new Func<Exception, bool>(KnownException.IsUnhandledException));
			}
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0003CCCC File Offset: 0x0003AECC
		private static PswsErrorCode TranslateErrorCode(PswsErrorCode orginalErrorCode, Exception exception, string additionalInfo)
		{
			if (orginalErrorCode == PswsErrorCode.CmdletExecutionFailure)
			{
				foreach (KeyValuePair<PswsErrorCode, Type[]> keyValuePair in PswsErrorHandling.ExceptionErrorCodeMapping)
				{
					foreach (Type type in keyValuePair.Value)
					{
						if (type.IsInstanceOfType(exception))
						{
							return keyValuePair.Key;
						}
					}
				}
				return orginalErrorCode;
			}
			return orginalErrorCode;
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x0003CD58 File Offset: 0x0003AF58
		private void Task_Error(object sender, GenericEventArg<TaskErrorEventArg> e)
		{
			if (e.Data.ExceptionHandled)
			{
				return;
			}
			PswsErrorHandling.SendErrorToClient(PswsErrorCode.CmdletExecutionFailure, e.Data.Exception, null);
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0003CD7E File Offset: 0x0003AF7E
		public override bool WriteWarning(LocalizedString input, string helperUrl, out LocalizedString output)
		{
			if (this.Warnings == null)
			{
				this.Warnings = new List<LocalizedString>();
			}
			this.Warnings.Add(input);
			return base.WriteWarning(input, helperUrl, out output);
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0003CDA8 File Offset: 0x0003AFA8
		private void OnStop(object sender, EventArgs eventArgs)
		{
			this.SendWarningToClientIfNeeded();
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0003CDB0 File Offset: 0x0003AFB0
		private void OnRelease(object sender, EventArgs eventArgs)
		{
			this.SendWarningToClientIfNeeded();
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0003CDB8 File Offset: 0x0003AFB8
		private void SendWarningToClientIfNeeded()
		{
			if (this.Warnings != null && this.Warnings.Count > 0)
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext == null)
				{
					return;
				}
				HttpResponse response = httpContext.Response;
				if (response == null)
				{
					return;
				}
				StringBuilder stringBuilder = new StringBuilder(response.Headers["X-Psws-Warning"]);
				int num = 0;
				while (num < this.Warnings.Count && stringBuilder.Length < 500)
				{
					stringBuilder.Append(this.Warnings[num].ToString());
					num++;
				}
				if (stringBuilder.Length > 500)
				{
					stringBuilder.Length = 500 - "...(truncated)".Length;
					stringBuilder.Append("...(truncated)");
				}
				HttpLogger.SafeAppendGenericInfo("PswsWarningInfo", stringBuilder.ToString());
				try
				{
					response.Headers["X-Psws-Warning"] = stringBuilder.ToString();
				}
				catch (Exception ex)
				{
					ExTraceGlobals.PublicPluginAPITracer.TraceDebug<Exception>(0L, "[PswsErrorHandling.SendErrorToClient] Exception {0}.", ex);
					HttpLogger.SafeAppendGenericError("SendErrorToClientError", ex, new Func<Exception, bool>(KnownException.IsUnhandledException));
				}
				this.Warnings.Clear();
			}
		}

		// Token: 0x040004A4 RID: 1188
		private const string ErrorCodeHttpHeaderKey = "X-Psws-ErrorCode";

		// Token: 0x040004A5 RID: 1189
		private const string ExceptionHttpHeaderKey = "X-Psws-Exception";

		// Token: 0x040004A6 RID: 1190
		private const string AdditionalInfoHttpHeaderKey = "X-Psws-ErrorInfo";

		// Token: 0x040004A7 RID: 1191
		private const string WarningHttpHeaderKey = "X-Psws-Warning";

		// Token: 0x040004A8 RID: 1192
		private const int HeaderLenghtLimitation = 500;

		// Token: 0x040004A9 RID: 1193
		private const string SuffixTruncatedHeader = "...(truncated)";

		// Token: 0x040004AA RID: 1194
		private readonly TaskContext context;

		// Token: 0x040004AB RID: 1195
		private static readonly Dictionary<PswsErrorCode, Type[]> ExceptionErrorCodeMapping = new Dictionary<PswsErrorCode, Type[]>
		{
			{
				PswsErrorCode.RetriableTransientException,
				new Type[]
				{
					typeof(TransientException)
				}
			},
			{
				PswsErrorCode.ParameterValidationFailed,
				new Type[]
				{
					typeof(DataValidationException),
					typeof(ParameterBindingException)
				}
			},
			{
				PswsErrorCode.DuplicateObjectCreation,
				new Type[]
				{
					typeof(ProxyAddressExistsException)
				}
			},
			{
				PswsErrorCode.ScopePermissionDenied,
				new Type[]
				{
					typeof(OperationRequiresGroupManagerException)
				}
			}
		};
	}
}
