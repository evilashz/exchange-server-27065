using System;
using System.Net;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Transport.RightsManagement;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x0200001E RID: 30
	internal static class RmsEventLogHandler
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00007FDC File Offset: 0x000061DC
		public static void LogException(ExEventLog logger, MailItem mailItem, RmsComponent component, Exception exception, bool permanentFailure)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			RmsEventLogHandler.EventParam param = new RmsEventLogHandler.EventParam(mailItem, component, exception, permanentFailure);
			if (!RmsEventLogHandler.LogRightsManagementException(logger, param))
			{
				RmsEventLogHandler.LogGeneralException(logger, param);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00008030 File Offset: 0x00006230
		private static bool LogRightsManagementException(ExEventLog logger, RmsEventLogHandler.EventParam param)
		{
			if (string.IsNullOrEmpty(param.RmsObjectName) || !(param.Exception is RightsManagementException))
			{
				return false;
			}
			RightsManagementException ex = (RightsManagementException)param.Exception;
			RightsManagementFailureCode failureCode = ex.FailureCode;
			if (ex.InnerException is WebException && ex.IsPermanent)
			{
				RmsEventLogHandler.LogPermanentWebException(logger, param, failureCode);
				return true;
			}
			if (failureCode == RightsManagementFailureCode.ServerRightNotGranted && param.RmsComponent == RmsComponent.JournalReportDecryptionAgent)
			{
				RmsEventLogHandler.LogNoRightSoapException(logger, param);
				return true;
			}
			return false;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000080AC File Offset: 0x000062AC
		private static void LogPermanentWebException(ExEventLog logger, RmsEventLogHandler.EventParam param, RightsManagementFailureCode failureCode)
		{
			WebException ex = param.Exception.InnerException as WebException;
			string text = string.Format("WebExceptionStatus = {0}", ex.Status);
			string text2 = param.IsEnterprise ? ex.Status.ToString() : string.Format("{0}_{1}", param.TenantId, ex.Status);
			if (ex.Status == WebExceptionStatus.ProtocolError)
			{
				HttpWebResponse httpWebResponse = ex.Response as HttpWebResponse;
				if (httpWebResponse != null)
				{
					text2 = string.Format("{0}_{1}", text2, httpWebResponse.StatusCode);
					text = string.Format("{0} | HttpStatusCode = {1}", text, httpWebResponse.StatusCode);
				}
			}
			switch (failureCode)
			{
			case RightsManagementFailureCode.Http3xxFailure:
			case RightsManagementFailureCode.ConnectFailure:
			{
				LocalizedString localizedString = RMSvcAgentStrings.RmsErrorTextForConnectFailure(param.TenantName, param.RmsComponentName, param.RmsObjectName, param.RmsServerName, text);
				logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_RmsConnectFailure, text2, new object[]
				{
					localizedString,
					param.EffectText,
					param.Exception
				});
				return;
			}
			case RightsManagementFailureCode.TrustFailure:
			{
				LocalizedString localizedString = RMSvcAgentStrings.RmsErrorTextForTrustFailure(param.TenantName, param.RmsComponentName, param.RmsObjectName, param.RmsServerName, text);
				logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_RmsTrustFailure, text2, new object[]
				{
					localizedString,
					param.EffectText,
					param.Exception
				});
				return;
			}
			case RightsManagementFailureCode.HttpUnauthorizedFailure:
			{
				LocalizedString localizedString = RMSvcAgentStrings.RmsErrorTextFor401(param.TenantName, param.RmsComponentName, param.RmsObjectName, param.RmsServerName, text);
				logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_Rms401Failure, text2, new object[]
				{
					localizedString,
					param.EffectText,
					param.Exception
				});
				return;
			}
			case RightsManagementFailureCode.HttpForbiddenFailure:
			{
				LocalizedString localizedString = RMSvcAgentStrings.RmsErrorTextFor403(param.TenantName, param.RmsComponentName, param.RmsObjectName, param.RmsServerName, text);
				logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_Rms403Failure, text2, new object[]
				{
					localizedString,
					param.EffectText,
					param.Exception
				});
				return;
			}
			case RightsManagementFailureCode.HttpNotFoundFailure:
			{
				LocalizedString localizedString = RMSvcAgentStrings.RmsErrorTextFor404(param.TenantName, param.RmsComponentName, param.RmsObjectName, param.RmsServerName, text);
				logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_Rms404Failure, text2, new object[]
				{
					localizedString,
					param.EffectText,
					param.Exception
				});
				return;
			}
			default:
			{
				LocalizedString localizedString = RMSvcAgentStrings.RmsErrorTextForSpecialException(param.TenantName, param.RmsComponentName, param.RmsObjectName, param.RmsServerName, string.Format("WebException ({0})", text));
				logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_RmsSpecialFailure, text2, new object[]
				{
					localizedString,
					param.EffectText,
					param.Exception
				});
				return;
			}
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000083E8 File Offset: 0x000065E8
		private static void LogNoRightSoapException(ExEventLog logger, RmsEventLogHandler.EventParam param)
		{
			string periodicKey = param.IsEnterprise ? null : param.TenantId.ToString();
			LocalizedString localizedString = RMSvcAgentStrings.RmsErrorTextForNoRightException(param.TenantName, param.RmsComponentName, param.RmsObjectName, param.RmsServerName);
			logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_RmsNoRightFailure, periodicKey, new object[]
			{
				localizedString,
				param.EffectText,
				param.Exception
			});
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00008470 File Offset: 0x00006670
		private static void LogSpecialException(ExEventLog logger, RmsEventLogHandler.EventParam param)
		{
			string name = param.Exception.GetType().Name;
			string periodicKey = param.IsEnterprise ? name : string.Format("{0}_{1}", param.TenantId, name);
			LocalizedString localizedString = RMSvcAgentStrings.RmsErrorTextForSpecialException(param.TenantName, param.RmsComponentName, param.RmsObjectName, param.RmsServerName, name);
			logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_RmsSpecialFailure, periodicKey, new object[]
			{
				localizedString,
				param.EffectText,
				param.Exception
			});
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00008510 File Offset: 0x00006710
		private static void LogGeneralException(ExEventLog logger, RmsEventLogHandler.EventParam param)
		{
			string name = param.Exception.GetType().Name;
			string periodicKey = param.IsEnterprise ? name : string.Format("{0}_{1}", param.TenantId, name);
			LocalizedString localizedString = RMSvcAgentStrings.RmsErrorTextForGeneralException(param.TenantName, param.RmsComponentName, name);
			logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_RmsGeneralFailure, periodicKey, new object[]
			{
				localizedString,
				param.EffectText,
				param.Exception
			});
		}

		// Token: 0x0200001F RID: 31
		internal struct EventParam
		{
			// Token: 0x06000098 RID: 152 RVA: 0x000085A0 File Offset: 0x000067A0
			public EventParam(MailItem mailItem, RmsComponent component, Exception exception, bool permanentError)
			{
				this.RmsComponent = component;
				this.Exception = exception;
				this.TenantId = mailItem.TenantId;
				OrganizationId a = Utils.OrgIdFromMailItem(mailItem);
				this.IsEnterprise = (a == OrganizationId.ForestWideOrgId);
				this.RmsComponentName = RmsEventLogHandler.EventParam.GetRmsComponentName(component);
				this.RmsServerName = RmsEventLogHandler.EventParam.GetRmsServerName(exception);
				this.TenantName = (this.IsEnterprise ? null : string.Format("(Tenant Id: {0}) ", this.TenantId));
				this.RmsObjectName = RmsEventLogHandler.EventParam.GetRmsObjectName(exception);
				this.EffectText = RmsEventLogHandler.EventParam.GetRmsErrorEffectText(component, permanentError, mailItem, this.RmsObjectName);
			}

			// Token: 0x06000099 RID: 153 RVA: 0x00008640 File Offset: 0x00006840
			private static string GetRmsComponentName(RmsComponent component)
			{
				string result = null;
				switch (component)
				{
				case RmsComponent.EncryptionAgent:
					result = "Encryption Agent";
					break;
				case RmsComponent.PrelicensingAgent:
					result = "Prelicensing Agent";
					break;
				case RmsComponent.JournalReportDecryptionAgent:
					result = "Journal Report Decryption Agent";
					break;
				}
				return result;
			}

			// Token: 0x0600009A RID: 154 RVA: 0x00008684 File Offset: 0x00006884
			private static string GetRmsServerName(Exception exception)
			{
				RightsManagementException ex = exception as RightsManagementException;
				string result;
				if (ex != null && !string.IsNullOrEmpty(ex.RmsUrl))
				{
					result = RMSvcAgentStrings.RmsAdNameWithUrl(ex.RmsUrl);
				}
				else
				{
					result = RMSvcAgentStrings.RmsAdName;
				}
				return result;
			}

			// Token: 0x0600009B RID: 155 RVA: 0x000086CC File Offset: 0x000068CC
			private static LocalizedString GetRmsErrorEffectText(RmsComponent component, bool isPermanentFailure, MailItem mailItem, string rmsObjectName)
			{
				string messageId = mailItem.Message.MessageId;
				switch (component)
				{
				case RmsComponent.EncryptionAgent:
					return isPermanentFailure ? RMSvcAgentStrings.RmsErrorTextForNDR(messageId) : RMSvcAgentStrings.RmsErrorTextForDefer(messageId);
				case RmsComponent.PrelicensingAgent:
					if (!isPermanentFailure)
					{
						return RMSvcAgentStrings.RmsErrorTextForDefer(messageId);
					}
					if (string.Equals(rmsObjectName, "License", StringComparison.OrdinalIgnoreCase))
					{
						return RMSvcAgentStrings.RmsErrorTextForNoServerPL(messageId);
					}
					return RMSvcAgentStrings.RmsErrorTextForNoPL(messageId);
				case RmsComponent.JournalReportDecryptionAgent:
					return isPermanentFailure ? RMSvcAgentStrings.RmsErrorTextForNoJR(messageId) : RMSvcAgentStrings.RmsErrorTextForDeferJR(messageId);
				}
				return LocalizedString.Empty;
			}

			// Token: 0x0600009C RID: 156 RVA: 0x0000875C File Offset: 0x0000695C
			private static string GetRmsObjectName(Exception exception)
			{
				string result = null;
				RightsManagementException ex = exception as RightsManagementException;
				if (ex != null)
				{
					switch (ex.FailureCode)
					{
					case RightsManagementFailureCode.PreLicenseAcquisitionFailed:
						result = "PreLicense";
						break;
					case RightsManagementFailureCode.ClcAcquisitionFailed:
						result = "CLC";
						break;
					case RightsManagementFailureCode.RacAcquisitionFailed:
						result = "GIC";
						break;
					case RightsManagementFailureCode.TemplateAcquisitionFailed:
						result = "Templates";
						break;
					case RightsManagementFailureCode.UseLicenseAcquisitionFailed:
						result = "License";
						break;
					case RightsManagementFailureCode.FindServiceLocationFailed:
						result = "FindServiceLocation";
						break;
					}
				}
				return result;
			}

			// Token: 0x040000EF RID: 239
			private const string RmsEntityRAC = "GIC";

			// Token: 0x040000F0 RID: 240
			private const string RmsEntityCLC = "CLC";

			// Token: 0x040000F1 RID: 241
			private const string RmsEntityLicense = "License";

			// Token: 0x040000F2 RID: 242
			private const string RmsEntityPreLicense = "PreLicense";

			// Token: 0x040000F3 RID: 243
			private const string RmsEntityTemplates = "Templates";

			// Token: 0x040000F4 RID: 244
			private const string RmsEntityFSL = "FindServiceLocation";

			// Token: 0x040000F5 RID: 245
			public bool IsEnterprise;

			// Token: 0x040000F6 RID: 246
			public Guid TenantId;

			// Token: 0x040000F7 RID: 247
			public RmsComponent RmsComponent;

			// Token: 0x040000F8 RID: 248
			public string RmsComponentName;

			// Token: 0x040000F9 RID: 249
			public string RmsServerName;

			// Token: 0x040000FA RID: 250
			public string RmsObjectName;

			// Token: 0x040000FB RID: 251
			public string TenantName;

			// Token: 0x040000FC RID: 252
			public LocalizedString EffectText;

			// Token: 0x040000FD RID: 253
			public Exception Exception;
		}
	}
}
