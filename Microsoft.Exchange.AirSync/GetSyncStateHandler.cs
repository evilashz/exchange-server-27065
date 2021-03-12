using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000080 RID: 128
	internal sealed class GetSyncStateHandler : ExchangeDiagnosableWrapper<GetSyncStateResult>
	{
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00025E76 File Offset: 0x00024076
		protected override string UsageText
		{
			get
			{
				return "This diagnostics handler returns sync state metadata and blob data for a given mailbox. The handler supports \"EmailAddress\", \"DeviceID\", \"DeviceType\", \"SyncState\" and \"FidMapping\" arguments. Below are examples for using this diagnostics handler:\r\n\r\n";
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x00025E7D File Offset: 0x0002407D
		protected override string UsageSample
		{
			get
			{
				return "Example 1: Return all metadata for a given mailbox\r\nGet-ExchangeDiagnosticInfo -Process MSExchangeSyncAppPool -Component GetSyncState -Argument \"EmailAddress=jondoe@contoso.com\"\r\n\r\nExample 2: Return all metadata for a given device\r\nGet-ExchangeDiagnosticInfo -Process MSExchangeSyncAppPool -Component GetSyncState -Argument \"EmailAddress=jondoe@contoso.com;DeviceId=WP123;DeviceType=WP8\"\r\n\r\nExample 3: Return a particular named SyncState for a given device\r\nGet-ExchangeDiagnosticInfo -Process MSExchangeSyncAppPool -Component GetSyncState -Argument \"EmailAddress=jondoe@contoso.com;DeviceId=WP123;DeviceType=WP8;SyncState=5\"\r\n\r\nExample 4: Return all sync states for a given device\r\nGet-ExchangeDiagnosticInfo -Process MSExchangeSyncAppPool -Component GetSyncState -Argument \"EmailAddress=jondoe@contoso.com;DeviceId=WP123;DeviceType=WP8\"\r\n\r\nExample 5: Return the fid mapping for a given device\r\nGet-ExchangeDiagnosticInfo -Process MSExchangeSyncAppPool -Component GetSyncState -Argument \"EmailAddress=jondoe@contoso.com;deviceId=WP123;DeviceType=WP8;FidMapping\"\r\n\r\nNOTE:\r\n1. EmailAddress is always required.\r\n2. DeviceId and DeviceType MUST be used together.\r\n3. If SyncState is supplied, DeviceId and DeviceType must also be supplied.\r\n4. The actual blob and size is ONLY returned when fetching a named sync state.\r\n5. FidMapping and SyncStateName should be mutually exclusive.  Both require DeviceId and DeviceType";
			}
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00025E84 File Offset: 0x00024084
		public static GetSyncStateHandler GetInstance()
		{
			if (GetSyncStateHandler.instance == null)
			{
				lock (GetSyncStateHandler.lockObject)
				{
					if (GetSyncStateHandler.instance == null)
					{
						GetSyncStateHandler.instance = new GetSyncStateHandler();
					}
				}
			}
			return GetSyncStateHandler.instance;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00025EDC File Offset: 0x000240DC
		private GetSyncStateHandler()
		{
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00025EE4 File Offset: 0x000240E4
		protected override string ComponentName
		{
			get
			{
				return "GetSyncState";
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00025EEC File Offset: 0x000240EC
		internal override GetSyncStateResult GetExchangeDiagnosticsInfoData(DiagnosableParameters arguments)
		{
			ParsedCallData parsedCallData = this.ParseCallData(arguments.Argument);
			ExchangePrincipal mailboxOwner = ExchangePrincipal.FromProxyAddress(ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(parsedCallData.Mailbox.Domain), parsedCallData.Mailbox.ToString(), RemotingOptions.AllowCrossSite);
			GetSyncStateResult result;
			using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.CurrentCulture, "Client=ActiveSync"))
			{
				GetSyncStateResult data = SyncStateDiagnostics.GetData(mailboxSession, parsedCallData);
				if (parsedCallData.FidMapping)
				{
					this.FillFidMapping(data, mailboxSession);
				}
				result = data;
			}
			return result;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00025F84 File Offset: 0x00024184
		private void FillFidMapping(GetSyncStateResult results, MailboxSession session)
		{
			foreach (DeviceData deviceData in results.Devices)
			{
				foreach (SyncStateFolderData syncStateFolderData in deviceData.SyncFolders)
				{
					if (syncStateFolderData != null && !string.IsNullOrEmpty(syncStateFolderData.SyncStateBlob))
					{
						syncStateFolderData.FolderMapping = new List<FolderMappingData>();
						using (PooledMemoryStream pooledMemoryStream = new PooledMemoryStream(102400))
						{
							byte[] array = Convert.FromBase64String(syncStateFolderData.SyncStateBlob);
							pooledMemoryStream.Write(array, 0, array.Length);
							pooledMemoryStream.Flush();
							pooledMemoryStream.Position = 0L;
							int num;
							int num2;
							long num3;
							long num4;
							Dictionary<string, bool> dictionary;
							GenericDictionaryData<ConstStringData, string, DerivedData<ICustomSerializableBuilder>> genericDictionaryData = SyncState.InternalDeserializeData(pooledMemoryStream, out num, out num2, out num3, out num4, out dictionary);
							FolderIdMapping folderIdMapping = genericDictionaryData.Data["IdMapping"].Data as FolderIdMapping;
							IDictionaryEnumerator syncIdIdEnumerator = folderIdMapping.SyncIdIdEnumerator;
							while (syncIdIdEnumerator.MoveNext())
							{
								string shortId = syncIdIdEnumerator.Key as string;
								ISyncItemId syncItemId = syncIdIdEnumerator.Value as ISyncItemId;
								StoreObjectId storeObjectId = syncItemId.NativeId as StoreObjectId;
								try
								{
									using (Folder folder = Folder.Bind(session, storeObjectId, new PropertyDefinition[]
									{
										FolderSchema.DisplayName
									}))
									{
										DefaultFolderType defaultFolderType = session.IsDefaultFolderType(folder.Id);
										syncStateFolderData.FolderMapping.Add(new FolderMappingData
										{
											ShortId = shortId,
											LongId = storeObjectId.ToString(),
											Name = folder.DisplayName,
											DefaultFolderType = defaultFolderType.ToString(),
											Exception = null
										});
									}
								}
								catch (Exception ex)
								{
									syncStateFolderData.FolderMapping.Add(new FolderMappingData
									{
										ShortId = shortId,
										LongId = "[Error]",
										Name = "[Error]",
										DefaultFolderType = "[Error]",
										Exception = ex.ToString()
									});
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0002622C File Offset: 0x0002442C
		private ParsedCallData ParseCallData(string arguments)
		{
			ParsedCallData parsedCallData = new ParsedCallData();
			parsedCallData.Metadata = true;
			string text = arguments.ToLower().Trim();
			string[] array = text.Split(new char[]
			{
				';'
			});
			foreach (string text2 in array)
			{
				if (text2.StartsWith("emailaddress="))
				{
					parsedCallData.Mailbox = SmtpAddress.Parse(text2.Substring("emailaddress=".Length));
				}
				else if (text2.StartsWith("deviceid="))
				{
					parsedCallData.Metadata = false;
					parsedCallData.DeviceId = text2.Substring("deviceid=".Length);
				}
				else if (text2.StartsWith("devicetype="))
				{
					parsedCallData.Metadata = false;
					parsedCallData.DeviceType = text2.Substring("devicetype=".Length);
				}
				else if (text2.StartsWith("syncstate="))
				{
					parsedCallData.Metadata = false;
					parsedCallData.SyncStateName = text2.Substring("syncstate=".Length);
				}
				else if (text2.StartsWith("fidmapping"))
				{
					parsedCallData.Metadata = false;
					parsedCallData.FidMapping = true;
					parsedCallData.SyncStateName = "FolderIdMapping";
				}
			}
			if (parsedCallData.Mailbox == SmtpAddress.Empty)
			{
				throw new ArgumentException(string.Format("{0} argument MUST be specified and must be a valid Smtp address.", "emailaddress="));
			}
			bool flag = string.IsNullOrEmpty(parsedCallData.DeviceId);
			bool flag2 = string.IsNullOrEmpty(parsedCallData.DeviceType);
			if (flag != flag2)
			{
				throw new ArgumentException(string.Format("{0} and {1} arguments must be both present.", "deviceid=", "devicetype="));
			}
			if ((!string.IsNullOrEmpty(parsedCallData.SyncStateName) || parsedCallData.FidMapping) && flag)
			{
				throw new ArgumentException(string.Format("If {0} or {1} is specified, then {2} and {3} must also be specified", new object[]
				{
					"syncstate=",
					"fidmapping",
					"deviceid=",
					"devicetype="
				}));
			}
			return parsedCallData;
		}

		// Token: 0x040004BB RID: 1211
		private const string DeviceIdArgument = "deviceid=";

		// Token: 0x040004BC RID: 1212
		private const string DeviceTypeArgument = "devicetype=";

		// Token: 0x040004BD RID: 1213
		private const string SyncStateNameArgument = "syncstate=";

		// Token: 0x040004BE RID: 1214
		private const string FidMappingArgument = "fidmapping";

		// Token: 0x040004BF RID: 1215
		private static GetSyncStateHandler instance;

		// Token: 0x040004C0 RID: 1216
		private static object lockObject = new object();
	}
}
