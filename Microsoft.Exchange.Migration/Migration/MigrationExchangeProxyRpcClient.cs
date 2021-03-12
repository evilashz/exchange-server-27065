using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Exchange.Migration.Logging;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Nspi.Rfri;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.MigrationService;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000DF RID: 223
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationExchangeProxyRpcClient : IMigrationAutodiscoverClient, IMigrationNspiClient, IMigrationMrsClient
	{
		// Token: 0x06000B97 RID: 2967 RVA: 0x000333A0 File Offset: 0x000315A0
		public AutodiscoverClientResponse GetUserSettings(ExchangeOutlookAnywhereEndpoint endpoint, string emailAddress)
		{
			MigrationUtil.ThrowOnNullArgument(endpoint, "connectionSettings");
			MigrationUtil.ThrowOnNullOrEmptyArgument(emailAddress, "emailAddress");
			ExchangeVersion? exchangeVersion = null;
			Uri autodiscoverUrl = null;
			MigrationAutodiscoverGetUserSettingsRpcArgs request = new MigrationAutodiscoverGetUserSettingsRpcArgs(endpoint.Username, endpoint.EncryptedPassword, endpoint.Domain, EwsUtilities.DomainFromEmailAddress(emailAddress), autodiscoverUrl, exchangeVersion, emailAddress);
			MigrationProxyRpcResult migrationProxyRpcResult;
			PropRowSet propRowSet;
			MigrationExchangeProxyRpcClient.CallRpc(request, out migrationProxyRpcResult, out propRowSet);
			return new AutodiscoverClientResponse((MigrationAutodiscoverGetUserSettingsRpcResult)migrationProxyRpcResult);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00033404 File Offset: 0x00031604
		public AutodiscoverClientResponse GetUserSettings(string userName, string encryptedPassword, string userDomain, string emailAddress)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(emailAddress, "emailAddress");
			MigrationAutodiscoverGetUserSettingsRpcArgs request = new MigrationAutodiscoverGetUserSettingsRpcArgs(userName, encryptedPassword, userDomain, EwsUtilities.DomainFromEmailAddress(emailAddress), null, null, emailAddress);
			MigrationProxyRpcResult migrationProxyRpcResult;
			PropRowSet propRowSet;
			MigrationExchangeProxyRpcClient.CallRpc(request, out migrationProxyRpcResult, out propRowSet);
			MigrationAutodiscoverGetUserSettingsRpcResult migrationAutodiscoverGetUserSettingsRpcResult = migrationProxyRpcResult as MigrationAutodiscoverGetUserSettingsRpcResult;
			if (migrationAutodiscoverGetUserSettingsRpcResult == null)
			{
				return null;
			}
			return new AutodiscoverClientResponse(migrationAutodiscoverGetUserSettingsRpcResult);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00033458 File Offset: 0x00031658
		public IList<PropRow> QueryRows(ExchangeOutlookAnywhereEndpoint connectionSettings, int? batchSize, int? startIndex, long[] longPropTags)
		{
			MigrationUtil.ThrowOnNullArgument(connectionSettings, "connectionSettings");
			MigrationNspiQueryRowsRpcArgs request = new MigrationNspiQueryRowsRpcArgs(connectionSettings, batchSize, startIndex, longPropTags);
			MigrationProxyRpcResult migrationProxyRpcResult;
			PropRowSet propRowSet;
			MigrationExchangeProxyRpcClient.CallRpc(request, out migrationProxyRpcResult, out propRowSet);
			if (propRowSet == null || propRowSet.Rows == null || propRowSet.Rows.Count == 0)
			{
				return null;
			}
			return propRowSet.Rows;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x000334A8 File Offset: 0x000316A8
		public PropRow GetRecipient(ExchangeOutlookAnywhereEndpoint connectionSettings, string recipientSmtpAddress, long[] longPropTags)
		{
			MigrationUtil.ThrowOnNullArgument(connectionSettings, "connectionSettings");
			MigrationUtil.ThrowOnNullOrEmptyArgument(recipientSmtpAddress, "recipientSmtpAddress");
			MigrationNspiGetRecipientRpcArgs request = new MigrationNspiGetRecipientRpcArgs(connectionSettings, recipientSmtpAddress, longPropTags);
			MigrationProxyRpcResult migrationProxyRpcResult;
			PropRowSet propRowSet;
			MigrationExchangeProxyRpcClient.CallRpc(request, out migrationProxyRpcResult, out propRowSet);
			if (propRowSet == null || propRowSet.Rows == null || propRowSet.Rows.Count != 1)
			{
				return null;
			}
			return propRowSet.Rows[0];
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00033508 File Offset: 0x00031708
		public void SetRecipient(ExchangeOutlookAnywhereEndpoint connectionSettings, string recipientSmtpAddress, string recipientLegDN, string[] propTagValues, long[] longPropTags)
		{
			MigrationUtil.ThrowOnNullArgument(connectionSettings, "connectionSettings");
			MigrationUtil.ThrowOnNullOrEmptyArgument(recipientSmtpAddress, "recipientSmtpAddress");
			MigrationUtil.ThrowOnNullOrEmptyArgument(recipientLegDN, "recipientLegDN");
			MigrationNspiSetRecipientRpcArgs request = new MigrationNspiSetRecipientRpcArgs(connectionSettings, recipientSmtpAddress, recipientLegDN, propTagValues, longPropTags);
			MigrationProxyRpcResult migrationProxyRpcResult;
			PropRowSet propRowSet;
			MigrationExchangeProxyRpcClient.CallRpc(request, out migrationProxyRpcResult, out propRowSet);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00033550 File Offset: 0x00031750
		public IList<PropRow> GetGroupMembers(ExchangeOutlookAnywhereEndpoint connectionSettings, string groupSmtpAddress)
		{
			MigrationUtil.ThrowOnNullArgument(connectionSettings, "connectionSettings");
			MigrationUtil.ThrowOnNullOrEmptyArgument(groupSmtpAddress, "groupSmtpAddress");
			MigrationNspiGetGroupMembersRpcArgs request = new MigrationNspiGetGroupMembersRpcArgs(connectionSettings, groupSmtpAddress);
			MigrationProxyRpcResult migrationProxyRpcResult;
			PropRowSet propRowSet;
			MigrationExchangeProxyRpcClient.CallRpc(request, out migrationProxyRpcResult, out propRowSet);
			if (propRowSet == null || propRowSet.Rows == null || propRowSet.Rows.Count == 0)
			{
				return null;
			}
			return propRowSet.Rows;
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x000335A8 File Offset: 0x000317A8
		public string GetNewDSA(ExchangeOutlookAnywhereEndpoint connectionSettings)
		{
			MigrationUtil.ThrowOnNullArgument(connectionSettings, "connectionSettings");
			MigrationNspiGetNewDsaRpcArgs request = new MigrationNspiGetNewDsaRpcArgs(connectionSettings);
			MigrationProxyRpcResult migrationProxyRpcResult;
			PropRowSet propRowSet;
			MigrationExchangeProxyRpcClient.CallRpc(request, out migrationProxyRpcResult, out propRowSet);
			return ((MigrationNspiGetNewDsaRpcResult)migrationProxyRpcResult).NspiServer;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x000335DC File Offset: 0x000317DC
		public bool CanConnectToMrsProxy(Fqdn serverName, Guid mbxGuid, NetworkCredential credentials, out LocalizedException error)
		{
			MigrationUtil.ThrowOnNullArgument(serverName, "serverName");
			bool result;
			using (MailboxReplicationServiceClient mailboxReplicationServiceClient = MailboxReplicationServiceClient.Create(MigrationServiceFactory.Instance.GetLocalServerFqdn(), MRSCapabilities.MrsProxyVerification))
			{
				bool flag = false;
				try
				{
					string username = null;
					string password = null;
					string domain = null;
					if (credentials != null)
					{
						username = credentials.UserName;
						password = credentials.Password;
						domain = credentials.Domain;
					}
					mailboxReplicationServiceClient.AttemptConnectToMRSProxy(serverName.ToString(), mbxGuid, username, password, domain);
					error = null;
					flag = true;
				}
				catch (MailboxReplicationPermanentException ex)
				{
					error = ex;
					MigrationLogger.Log(MigrationEventType.Information, ex, "Could not connect to MRS proxy at '{0}'.", new object[]
					{
						serverName
					});
				}
				catch (MailboxReplicationTransientException ex2)
				{
					error = ex2;
					MigrationLogger.Log(MigrationEventType.Information, ex2, "Could not connect to MRS proxy at '{0}'.", new object[]
					{
						serverName
					});
				}
				catch (CommunicationObjectFaultedException ex3)
				{
					error = new CommunicationErrorTransientException(serverName.ToString(), Strings.ErrorConnectionFailed(serverName.ToString()), ex3);
					MigrationLogger.Log(MigrationEventType.Information, ex3, "Could not connect to MRS proxy at '{0}'.", new object[]
					{
						serverName
					});
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00033710 File Offset: 0x00031910
		private static PropRowSet GetRowSetAndDisposeHandle(SafeRpcMemoryHandle rowsetHandle)
		{
			PropRowSet result;
			if (rowsetHandle != null)
			{
				result = new PropRowSet(rowsetHandle, true);
				rowsetHandle.Dispose();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00033734 File Offset: 0x00031934
		private static void CallRpc(MigrationProxyRpcArgs request, out MigrationProxyRpcResult result, out PropRowSet rowset)
		{
			string message = "request";
			if (request == null && !request.Validate(out message))
			{
				throw new ArgumentException(message);
			}
			int num = 5;
			while (num != 0)
			{
				num--;
				string localServerFqdn = MigrationServiceFactory.Instance.GetLocalServerFqdn();
				try
				{
					using (IMigrationProxyRpc migrationProxyRpcClient = MigrationServiceFactory.Instance.GetMigrationProxyRpcClient(localServerFqdn))
					{
						switch (request.Type)
						{
						case MigrationProxyRpcType.QueryRows:
						{
							byte[] resultBlob;
							SafeRpcMemoryHandle rowsetHandle;
							NspiStatus nspiStatus = migrationProxyRpcClient.NspiQueryRows(1, request.GetBytes(), out resultBlob, out rowsetHandle);
							rowset = MigrationExchangeProxyRpcClient.GetRowSetAndDisposeHandle(rowsetHandle);
							result = new MigrationNspiQueryRowsRpcResult(resultBlob);
							MigrationExchangeProxyRpcClient.LogAndHandleError(request, result, nspiStatus.ToString(), nspiStatus != NspiStatus.Success, true);
							return;
						}
						case MigrationProxyRpcType.GetGroupMembers:
						{
							byte[] resultBlob;
							SafeRpcMemoryHandle rowsetHandle2;
							NspiStatus nspiStatus2 = migrationProxyRpcClient.NspiGetGroupMembers(1, request.GetBytes(), out resultBlob, out rowsetHandle2);
							rowset = MigrationExchangeProxyRpcClient.GetRowSetAndDisposeHandle(rowsetHandle2);
							result = new MigrationNspiGetGroupMembersRpcResult(resultBlob);
							MigrationExchangeProxyRpcClient.LogAndHandleError(request, result, nspiStatus2.ToString(), nspiStatus2 != NspiStatus.Success, true);
							return;
						}
						case MigrationProxyRpcType.GetNewDSA:
						{
							rowset = null;
							byte[] resultBlob;
							RfriStatus rfriStatus = migrationProxyRpcClient.NspiRfrGetNewDSA(1, request.GetBytes(), out resultBlob);
							result = new MigrationNspiGetNewDsaRpcResult(resultBlob);
							MigrationExchangeProxyRpcClient.LogAndHandleError(request, result, rfriStatus.ToString(), rfriStatus != RfriStatus.Success, true);
							return;
						}
						case MigrationProxyRpcType.GetUserSettings:
						{
							rowset = null;
							byte[] resultBlob;
							migrationProxyRpcClient.AutodiscoverGetUserSettings(1, request.GetBytes(), out resultBlob);
							MigrationAutodiscoverGetUserSettingsRpcResult migrationAutodiscoverGetUserSettingsRpcResult = new MigrationAutodiscoverGetUserSettingsRpcResult(resultBlob);
							result = migrationAutodiscoverGetUserSettingsRpcResult;
							MigrationExchangeProxyRpcClient.LogAndHandleError(request, result, null, migrationAutodiscoverGetUserSettingsRpcResult.Status == null || migrationAutodiscoverGetUserSettingsRpcResult.Status.Value != AutodiscoverClientStatus.NoError, false);
							return;
						}
						case MigrationProxyRpcType.GetRecipient:
						{
							byte[] resultBlob;
							SafeRpcMemoryHandle rowsetHandle3;
							NspiStatus nspiStatus3 = migrationProxyRpcClient.NspiGetRecipient(1, request.GetBytes(), out resultBlob, out rowsetHandle3);
							rowset = MigrationExchangeProxyRpcClient.GetRowSetAndDisposeHandle(rowsetHandle3);
							result = new MigrationNspiGetRecipientRpcResult(resultBlob);
							MigrationExchangeProxyRpcClient.LogAndHandleError(request, result, nspiStatus3.ToString(), nspiStatus3 != NspiStatus.Success, true);
							return;
						}
						case MigrationProxyRpcType.SetRecipient:
						{
							rowset = null;
							byte[] resultBlob;
							NspiStatus nspiStatus4 = migrationProxyRpcClient.NspiSetRecipient(1, request.GetBytes(), out resultBlob);
							result = new MigrationNspiSetRecipientRpcResult(resultBlob);
							MigrationExchangeProxyRpcClient.LogAndHandleError(request, result, nspiStatus4.ToString(), nspiStatus4 != NspiStatus.Success, true);
							return;
						}
						default:
							throw new InvalidOperationException("MigrationExchangeProxyRpcClient.CallRpc() must handle all Migration Proxy Rpc.");
						}
					}
				}
				catch (RpcException ex)
				{
					if (num == 0 || !MigrationExchangeProxyRpcClient.ShouldRetryOnException(ex))
					{
						string format = string.Format("MigrationExchangeProxyRpcClient.CallRpc has thrown RpcException. Request = '{0}', Server = '{1}'", request, localServerFqdn);
						MigrationLogger.Log(MigrationEventType.Error, ex, format, new object[0]);
						throw new MigrationRpcRequestTransientException(request.Type.ToString(), localServerFqdn, ex);
					}
					MigrationLogger.Log(MigrationEventType.Warning, ex, "MigrationExchangeProxyRpcClient.CallRpc has thrown RpcException. Retry-Count = {0}. Request = '{1}', Server = '{2}'.", new object[]
					{
						5 - num,
						request,
						localServerFqdn
					});
				}
				catch (MdbefException ex2)
				{
					string format2 = string.Format("MigrationExchangeProxyRpcClient.CallRpc has thrown MdbefException. Request = '{0}', Server = '{1}.", request, localServerFqdn);
					MigrationLogger.Log(MigrationEventType.Error, ex2, format2, new object[0]);
					throw new MigrationRpcRequestTransientException(request.Type.ToString(), localServerFqdn, ex2);
				}
			}
			throw new InvalidOperationException("MigrationExchangeProxyRpcClient.CallRpc() must return not-null response.");
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00033A78 File Offset: 0x00031C78
		private static void LogAndHandleError(MigrationProxyRpcArgs request, MigrationProxyRpcResult result, string status, bool isError, bool throwException)
		{
			if (isError)
			{
				string text = string.Format("Migration Proxy Rpc failed. Status = {0}. Request = '{1}'. Response = '{2}'.", status, request, result);
				MigrationLogger.Log(MigrationEventType.Error, text, new object[0]);
				if (throwException)
				{
					if (result.RpcErrorCode == 5)
					{
						throw new MigrationTransientException(Strings.MigrationExchangeCredentialFailure, text);
					}
					throw new MigrationTransientException(Strings.MigrationExchangeRpcConnectionFailure, text);
				}
			}
			else
			{
				MigrationLogger.Log(MigrationEventType.Information, "Migration Proxy Rpc succeeded. Status = '{0}'. Request = '{1}'. Response = '{2}'.", new object[]
				{
					status,
					request,
					result
				});
			}
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00033AE8 File Offset: 0x00031CE8
		private static bool ShouldRetryOnException(RpcException ex)
		{
			int errorCode = ex.ErrorCode;
			return true;
		}

		// Token: 0x04000472 RID: 1138
		private const int NspiRpcCurrentVersion = 1;

		// Token: 0x04000473 RID: 1139
		private const int AutodiscoverRpcCurrentVersion = 1;

		// Token: 0x04000474 RID: 1140
		private const int MaxRpcRetryCount = 5;
	}
}
