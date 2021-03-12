using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Xml;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.MailTips;
using Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.GroupMetricsGenerator
{
	// Token: 0x020001A4 RID: 420
	internal class GroupMetricsGenerator : DirectoryProcessorBaseTask
	{
		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x0005FEB8 File Offset: 0x0005E0B8
		protected override Trace Trace
		{
			get
			{
				return GroupMetricsUtility.Tracer;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x0005FEBF File Offset: 0x0005E0BF
		public string ChangedGroupListPath
		{
			get
			{
				return this.changedGroupListPath;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x0005FEC7 File Offset: 0x0005E0C7
		// (set) Token: 0x0600107C RID: 4220 RVA: 0x0005FECF File Offset: 0x0005E0CF
		internal TimeSpan FullSyncInterval
		{
			get
			{
				return this.fullSyncInterval;
			}
			set
			{
				this.fullSyncInterval = value;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x0005FED8 File Offset: 0x0005E0D8
		internal int GroupsExpanded
		{
			get
			{
				return this.groupsExpanded;
			}
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0005FEE0 File Offset: 0x0005E0E0
		static GroupMetricsGenerator()
		{
			GroupMetricsGenerator.groupProperties.Add(ADRecipientSchema.MemberOfGroup);
			GroupMetricsGenerator.groupProperties.Add(ADGroupSchema.GroupMemberCount);
			GroupMetricsGenerator.groupProperties.Add(ADGroupSchema.GroupExternalMemberCount);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0005FF48 File Offset: 0x0005E148
		public GroupMetricsGenerator(RunData runData, ICollection<DirectoryProcessorMailboxData> mailboxesToProcess) : base(runData)
		{
			this.mailboxesToProcess = mailboxesToProcess;
			this.tenantDirectory = GroupMetricsUtility.GetTenantDirectoryPath(base.OrgId);
			this.changedGroupListPath = Path.Combine(this.tenantDirectory, "ChangedGroups.txt");
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0005FFB2 File Offset: 0x0005E1B2
		public override bool ShouldRun(RecipientType recipientType)
		{
			return RecipientType.Group == recipientType && this.mailboxesToProcess.Contains(new DirectoryProcessorMailboxData(base.OrgId, base.DatabaseGuid, base.MailboxGuid));
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0005FFDC File Offset: 0x0005E1DC
		public override void Initialize(RecipientType recipientType)
		{
			base.Initialize(recipientType);
			base.Logger.TraceDebug(null, "Entering GroupMetricsGenerator.Initialize at {0}", new object[]
			{
				DateTime.UtcNow.ToString("o")
			});
			this.taskStartTime = DateTime.UtcNow;
			GroupMetricsUtility.CreateGroupMetricsDirectory();
			GroupMetricsGenerator.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_GroupMetricsGenerationStarted, null, new object[]
			{
				base.TenantId,
				base.RunId,
				recipientType.ToString()
			});
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00060070 File Offset: 0x0005E270
		public override void FinalizeMe(DirectoryProcessorBaseTaskContext taskContext)
		{
			base.Logger.TraceDebug(null, "Entering GroupMetricsGenerator.FinalizeMe at {0}, LastSyncType={1}", new object[]
			{
				DateTime.UtcNow.ToString("o"),
				this.lastSyncType
			});
			if (this.exception == null)
			{
				if (this.lastSyncType == GroupMetricsSyncType.Full || this.lastSyncType == GroupMetricsSyncType.Delta)
				{
					this.SaveCookies(this.updatedCookies);
				}
				DateTime utcNow = DateTime.UtcNow;
				TimeSpan timeSpan = utcNow - this.taskStartTime;
				GroupMetricsGenerator.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_GroupMetricsGenerationSuccessful, null, new object[]
				{
					base.RunData.TenantId,
					base.RunData.RunId,
					timeSpan,
					this.groupsExpanded,
					this.syncType
				});
			}
			else
			{
				GroupMetricsGenerator.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_GroupMetricsGenerationFailed, null, new object[]
				{
					base.RunData.TenantId,
					base.RunData.RunId,
					CommonUtil.ToEventLogString(this.exception)
				});
			}
			base.FinalizeMe(taskContext);
			base.Logger.TraceDebug(null, "Exiting GroupMetricsGenerator.FinalizeMe at {0}", new object[]
			{
				DateTime.UtcNow.ToString("o")
			});
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x000601E4 File Offset: 0x0005E3E4
		public GroupMetricsSyncType UpdateGroupMetrics(GroupMetricsGeneratorTaskContext context)
		{
			try
			{
				if (context.LastProcessedGroupDistinguishedName == null)
				{
					CachedOrganizationConfiguration instance = CachedOrganizationConfiguration.GetInstance(base.OrgId, CachedOrganizationConfiguration.ConfigurationTypes.Domains);
					this.domains = instance.Domains;
					this.updatedCookies = new List<GroupMetricsCookie>();
					bool flag;
					if (!this.TryDeltaSync(out flag, this.updatedCookies))
					{
						this.updatedCookies.Clear();
						if (!flag || !this.TryFullSync(this.updatedCookies))
						{
							this.lastSyncType = GroupMetricsSyncType.Failed;
						}
					}
				}
				if (!this.GetMetricsForChangedGroups(context))
				{
					this.lastSyncType = GroupMetricsSyncType.Failed;
				}
			}
			catch (Exception ex)
			{
				this.exception = ex;
				this.lastSyncType = GroupMetricsSyncType.Failed;
				return this.lastSyncType;
			}
			return this.lastSyncType;
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x000602E8 File Offset: 0x0005E4E8
		internal static IList<string> GetDomains()
		{
			List<string> domains = new List<string>();
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ADForest localForest = ADForest.GetLocalForest();
				ADCrossRef[] domainPartitions = localForest.GetDomainPartitions();
				foreach (ADCrossRef adcrossRef in domainPartitions)
				{
					domains.Add(adcrossRef.NCName.DistinguishedName);
				}
			}, 3);
			if (!adoperationResult.Succeeded)
			{
				GroupMetricsGenerator.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToGetDomainList, null, new object[]
				{
					adoperationResult.Exception.GetType().FullName,
					adoperationResult.Exception.Message
				});
			}
			return domains;
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0006036C File Offset: 0x0005E56C
		internal bool GenerateGroupList(bool delta, out bool tryAgain, IList<GroupMetricsCookie> updatedCookies)
		{
			IList<string> list = GroupMetricsGenerator.GetDomains();
			this.currentCookiePathCollection = list.ConvertAll((string domain) => this.GetCookiePath(domain));
			this.hasDownloadedCookiesFromMailbox = false;
			bool result;
			try
			{
				using (LazyWriter lazyWriter = new LazyWriter(this.changedGroupListPath))
				{
					foreach (string domain2 in list)
					{
						string cookiePath = this.GetCookiePath(domain2);
						GroupMetricsCookie groupMetricsCookie;
						if (delta)
						{
							groupMetricsCookie = this.LoadCookie(cookiePath, domain2);
							if (groupMetricsCookie == null)
							{
								tryAgain = true;
								return false;
							}
						}
						else
						{
							groupMetricsCookie = new GroupMetricsCookie(domain2);
						}
						if (!this.GenerateChangedGroupList(domain2, groupMetricsCookie, lazyWriter, delta))
						{
							tryAgain = false;
							return false;
						}
						updatedCookies.Add(groupMetricsCookie);
					}
					if (!delta && !this.TenantHasChangedGroups())
					{
						this.lastSyncType = GroupMetricsSyncType.Empty;
						GroupMetricsUtility.DeleteDirectory(this.tenantDirectory);
					}
					else if (!this.TenantHasChangedGroups())
					{
						lazyWriter.WriteLine("");
					}
					tryAgain = false;
					result = true;
				}
			}
			catch (LazyWriterException)
			{
				tryAgain = false;
				result = false;
			}
			return result;
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00060498 File Offset: 0x0005E698
		internal string GetCookiePath(string domain)
		{
			string str = domain.GetHashCode().ToString("X8");
			string path = "Cookie_" + str + ".dsc";
			return Path.Combine(this.tenantDirectory, path);
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x000604D8 File Offset: 0x0005E6D8
		internal void SaveCookies(IList<GroupMetricsCookie> cookies)
		{
			base.Logger.TraceDebug(null, "Calling SaveCookies", new object[0]);
			string[] array = new string[cookies.Count];
			int num = 0;
			foreach (GroupMetricsCookie groupMetricsCookie in cookies)
			{
				string domain = groupMetricsCookie.Domain;
				string cookiePath = this.GetCookiePath(domain);
				array[num++] = cookiePath;
				this.SaveCookie(cookiePath, domain, groupMetricsCookie);
			}
			GroupMetricsMailboxFileStore groupMetricsMailboxFileStore = GroupMetricsMailboxFileStore.FromMailboxGuid(base.OrgId, base.MailboxGuid);
			if (groupMetricsMailboxFileStore != null)
			{
				groupMetricsMailboxFileStore.UploadCookies(array);
			}
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00060588 File Offset: 0x0005E788
		internal bool ShouldSaveToAD(int oldValue, int newValue)
		{
			if (newValue < 100)
			{
				return oldValue != newValue;
			}
			if (newValue < 200)
			{
				return Math.Abs(oldValue - newValue) >= 5;
			}
			if (newValue < 300)
			{
				return Math.Abs(oldValue - newValue) >= 10;
			}
			if (newValue < 400)
			{
				return Math.Abs(oldValue - newValue) >= 15;
			}
			if (newValue < 500)
			{
				return Math.Abs(oldValue - newValue) >= 20;
			}
			if (newValue < 1000)
			{
				return Math.Abs(oldValue - newValue) >= 25;
			}
			return oldValue > 1000 || oldValue < 1000;
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x000606C4 File Offset: 0x0005E8C4
		internal void SaveGroupMetricsToAD(IRecipientSession session, ADRawEntry group, int totalMemberCount, int externalMemberCount)
		{
			bool savedToAD = false;
			this.TryRunPerGroupADOperation(delegate
			{
				ADRawEntry adrawEntry = session.Read(group.Id);
				ADGroup adgroup = adrawEntry as ADGroup;
				ADDynamicGroup addynamicGroup = adrawEntry as ADDynamicGroup;
				if (adgroup != null)
				{
					adgroup.GroupMemberCount = totalMemberCount;
					adgroup.GroupExternalMemberCount = externalMemberCount;
					session.Save(adgroup);
					savedToAD = true;
					return;
				}
				if (addynamicGroup != null)
				{
					addynamicGroup.GroupMemberCount = totalMemberCount;
					addynamicGroup.GroupExternalMemberCount = externalMemberCount;
					session.Save(addynamicGroup);
					savedToAD = true;
				}
			}, group.Id.DistinguishedName);
			if (!savedToAD)
			{
				string organizationIdString = this.GetOrganizationIdString();
				GroupMetricsGenerator.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToSaveGroupMetricsToAD, organizationIdString, new object[]
				{
					organizationIdString,
					group.Id.DistinguishedName
				});
			}
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00060890 File Offset: 0x0005EA90
		internal void GetMetricsForSingleGroup(IRecipientSession session, string groupDN, TypedHashSet calculatedGroups, Dictionary<string, object> parentGroupDictionary, IList<string> parentGroupList)
		{
			ADObjectId id = new ADObjectId(groupDN);
			ADRawEntry group = null;
			ADOperationResult adoperationResult = this.TryRunPerGroupADOperation(delegate
			{
				group = session.ReadADRawEntry(id, GroupMetricsGenerator.groupProperties);
			}, groupDN);
			if (!adoperationResult.Succeeded || group == null)
			{
				return;
			}
			ulong hash = GroupMetricsUtility.GetHash(group.Id.ObjectGuid);
			if (calculatedGroups.Contains(hash))
			{
				return;
			}
			int externalMemberCount = 0;
			HashSet<ulong> allMemberHashes = new HashSet<ulong>(2000);
			ADRecipientExpansion expander = new ADRecipientExpansion(session, false);
			adoperationResult = this.TryRunPerGroupADOperation(delegate
			{
				if (GroupMetricsUtility.Fault == GroupMetricsFault.TransientExceptionInExpansion)
				{
					throw new ADTransientException(new LocalizedString("Fault Injection"));
				}
				if (GroupMetricsUtility.Fault == GroupMetricsFault.InvalidCredentialExceptionInExpansion)
				{
					throw new ADInvalidCredentialException(new LocalizedString("Fault Injection"));
				}
				if (GroupMetricsUtility.Fault == GroupMetricsFault.PermanentExceptionInExpansion)
				{
					throw new DataSourceOperationException(new LocalizedString("Fault Injection"));
				}
				expander.Expand(group, delegate(ADRawEntry member, ExpansionType recipientExpansionType, ADRawEntry parent, ExpansionType parentExpansionType)
				{
					this.RunData.ThrowIfShuttingDown();
					if (recipientExpansionType == ExpansionType.GroupMembership)
					{
						return ExpansionControl.Continue;
					}
					ulong hash2 = GroupMetricsUtility.GetHash(member.Id.ObjectGuid);
					if (allMemberHashes.TryAdd(hash2) && this.IsExternal(member))
					{
						externalMemberCount++;
					}
					if (allMemberHashes.Count >= 1000)
					{
						return ExpansionControl.Terminate;
					}
					if (recipientExpansionType != ExpansionType.None)
					{
						return ExpansionControl.Skip;
					}
					return ExpansionControl.Continue;
				}, (ExpansionFailure failure, ADRawEntry member, ExpansionType recipientExpansionType, ADRawEntry parent, ExpansionType parentExpansionType) => ExpansionControl.Continue);
			}, groupDN);
			if (!adoperationResult.Succeeded)
			{
				return;
			}
			MultiValuedProperty<ADObjectId> multiValuedProperty = group[ADRecipientSchema.MemberOfGroup] as MultiValuedProperty<ADObjectId>;
			foreach (ADObjectId adobjectId in multiValuedProperty)
			{
				string distinguishedName = adobjectId.DistinguishedName;
				if (!parentGroupDictionary.ContainsKey(distinguishedName))
				{
					parentGroupDictionary.Add(distinguishedName, null);
					parentGroupList.Add(distinguishedName);
				}
			}
			int count = allMemberHashes.Count;
			calculatedGroups.Add(hash);
			this.groupsExpanded++;
			int oldValue = (int)group[ADGroupSchema.GroupMemberCount];
			int oldValue2 = (int)group[ADGroupSchema.GroupExternalMemberCount];
			if (this.ShouldSaveToAD(oldValue, count) || this.ShouldSaveToAD(oldValue2, externalMemberCount))
			{
				this.SaveGroupMetricsToAD(session, group, count, externalMemberCount);
			}
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x00060A64 File Offset: 0x0005EC64
		private GroupMetricsCookie LoadCookie(string path, string domain)
		{
			string cookieString = this.ReadCookieString(path, domain);
			GroupMetricsCookie result = null;
			if (!this.TryParseCookieString(cookieString, path, domain, out result))
			{
				this.DeleteCookieFile(path, domain);
			}
			return result;
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00060A94 File Offset: 0x0005EC94
		private string ReadCookieString(string path, string domain)
		{
			if (!File.Exists(path) && !this.hasDownloadedCookiesFromMailbox)
			{
				GroupMetricsMailboxFileStore groupMetricsMailboxFileStore = GroupMetricsMailboxFileStore.FromMailboxGuid(base.OrgId, base.MailboxGuid);
				if (groupMetricsMailboxFileStore != null)
				{
					this.hasDownloadedCookiesFromMailbox = groupMetricsMailboxFileStore.DownloadCookies(this.currentCookiePathCollection);
				}
			}
			string result = null;
			Exception ex = null;
			try
			{
				using (StreamReader streamReader = new StreamReader(path))
				{
					result = streamReader.ReadLine();
				}
			}
			catch (FileNotFoundException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			catch (UnauthorizedAccessException ex4)
			{
				ex = ex4;
			}
			catch (SecurityException ex5)
			{
				ex = ex5;
			}
			if (ex != null && base.OrgId == OrganizationId.ForestWideOrgId)
			{
				GroupMetricsGenerator.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToReadGroupMetricsCookie, null, new object[]
				{
					this.GetOrganizationIdString(),
					path,
					domain,
					ex.GetType().FullName,
					ex.Message
				});
				return string.Empty;
			}
			return result;
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00060BB8 File Offset: 0x0005EDB8
		private bool TryParseCookieString(string cookieString, string path, string domain, out GroupMetricsCookie cookie)
		{
			cookie = null;
			if (string.IsNullOrEmpty(cookieString))
			{
				return false;
			}
			if (!GroupMetricsCookie.TryDeserialize(cookieString, out cookie))
			{
				GroupMetricsGenerator.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToDeserializeGroupMetricsCookie, null, new object[]
				{
					this.GetOrganizationIdString(),
					path,
					domain
				});
				return false;
			}
			TimeSpan timeSpan = cookie.LastDeltaSync - DateTime.UtcNow;
			if (timeSpan > GroupMetricsGenerator.CookieLifetime)
			{
				GroupMetricsGenerator.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_GroupMetricsCookieExpired, null, new object[]
				{
					this.GetOrganizationIdString(),
					domain,
					cookie.LastDeltaSync,
					timeSpan,
					GroupMetricsGenerator.CookieLifetime
				});
				return false;
			}
			return true;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00060C78 File Offset: 0x0005EE78
		private void DeleteCookieFile(string path, string domain)
		{
			Exception ex = null;
			try
			{
				File.Delete(path);
			}
			catch (FileNotFoundException)
			{
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				ex = ex3;
			}
			catch (SecurityException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				GroupMetricsGenerator.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToRemoveCorruptGroupMetricsCookie, null, new object[]
				{
					this.GetOrganizationIdString(),
					path,
					domain,
					ex.GetType().FullName,
					ex.Message
				});
			}
			GroupMetricsMailboxFileStore groupMetricsMailboxFileStore = GroupMetricsMailboxFileStore.FromMailboxGuid(base.OrgId, base.MailboxGuid);
			if (groupMetricsMailboxFileStore != null)
			{
				groupMetricsMailboxFileStore.DeleteCookie(path);
			}
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00060D40 File Offset: 0x0005EF40
		private ADOperationResult TryRunPerGroupADOperation(ADOperation adOperation, string groupDN)
		{
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(adOperation, 3);
			switch (adoperationResult.ErrorCode)
			{
			case ADOperationErrorCode.RetryableError:
				GroupMetricsGenerator.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_GroupExpansionHaltedWarning, this.GetOrganizationIdString(), new object[]
				{
					adoperationResult.Exception.GetType().Name,
					groupDN,
					adoperationResult.Exception
				});
				break;
			case ADOperationErrorCode.PermanentError:
				GroupMetricsGenerator.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_GroupExpansionHaltedError, this.GetOrganizationIdString(), new object[]
				{
					adoperationResult.Exception.GetType().Name,
					groupDN,
					adoperationResult.Exception
				});
				break;
			}
			return adoperationResult;
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00060DF0 File Offset: 0x0005EFF0
		private void SaveCookie(string path, string domain, GroupMetricsCookie cookie)
		{
			string value = cookie.Serialize();
			Exception ex = null;
			try
			{
				string directoryName = Path.GetDirectoryName(path);
				Directory.CreateDirectory(directoryName);
				using (StreamWriter streamWriter = new StreamWriter(path))
				{
					streamWriter.WriteLine(value);
				}
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				ex = ex3;
			}
			catch (SecurityException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				GroupMetricsGenerator.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToSaveGroupMetricsCookie, null, new object[]
				{
					this.GetOrganizationIdString(),
					path,
					domain,
					ex.GetType().FullName,
					ex.Message
				});
			}
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00060EC4 File Offset: 0x0005F0C4
		private bool TryFullSync(List<GroupMetricsCookie> updatedCookies)
		{
			bool flag;
			if (!this.GenerateGroupList(false, out flag, updatedCookies))
			{
				this.lastSyncType = GroupMetricsSyncType.Failed;
				return false;
			}
			if (!this.TenantHasChangedGroups())
			{
				this.lastSyncType = GroupMetricsSyncType.Empty;
				return true;
			}
			this.lastSyncType = GroupMetricsSyncType.Full;
			return true;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00060EFF File Offset: 0x0005F0FF
		private bool TryDeltaSync(out bool tryFullSync, List<GroupMetricsCookie> updatedCookies)
		{
			if (!this.GenerateGroupList(true, out tryFullSync, updatedCookies))
			{
				tryFullSync = true;
				return false;
			}
			this.lastSyncType = GroupMetricsSyncType.Delta;
			tryFullSync = false;
			return true;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00060F1C File Offset: 0x0005F11C
		private bool GenerateChangedGroupList(string domain, GroupMetricsCookie cookie, LazyWriter changedWriter, bool delta)
		{
			DateTime dateTime = delta ? cookie.LastDeltaSync : cookie.LastFullSync;
			if (default(DateTime) != dateTime)
			{
				dateTime = dateTime.AddHours(-1.0);
			}
			DateTime utcNow = DateTime.UtcNow;
			cookie.LastDeltaSync = utcNow;
			if (!delta)
			{
				cookie.LastFullSync = utcNow;
			}
			string entriesFilePath = ADCrawler.GetEntriesFilePath(base.RunData.RunFolderPath, "DistributionList");
			try
			{
				using (XmlReader xmlReader = XmlReader.Create(entriesFilePath))
				{
					if (xmlReader.ReadToFollowing("ADEntry"))
					{
						do
						{
							string attribute = xmlReader.GetAttribute(GrammarRecipientHelper.LookupProperties[2].Name);
							string attribute2 = xmlReader.GetAttribute(GrammarRecipientHelper.LookupProperties[0].Name);
							Guid guid = new Guid(xmlReader.GetAttribute(GrammarRecipientHelper.LookupProperties[3].Name));
							RecipientType recipientType = (RecipientType)Enum.Parse(typeof(RecipientType), xmlReader.GetAttribute(GrammarRecipientHelper.LookupProperties[4].Name));
							string attribute3 = xmlReader.GetAttribute(GrammarRecipientHelper.LookupProperties[7].Name);
							string attribute4 = xmlReader.GetAttribute(GrammarRecipientHelper.LookupProperties[8].Name);
							base.Logger.TraceDebug(this, "GroupMetricsGenerator.GenerateChangedGroupList read AD entry - displayName='{0}', distinguishedName='{1}', smtpAddress='{2}', objectGuid='{3}', recipientType='{4}', whenChangedUTCString='{5}'", new object[]
							{
								attribute2,
								attribute4,
								attribute,
								guid,
								recipientType,
								attribute3
							});
							DateTime utcNow2 = DateTime.UtcNow;
							bool flag = GroupMetricsCookie.TryParseDateTime(attribute3, out utcNow2);
							if (!delta || !flag || utcNow2 > dateTime || RecipientType.DynamicDistributionGroup == recipientType)
							{
								this.changedGroups++;
								changedWriter.WriteLine(attribute4);
								base.Logger.TraceDebug(this, "GroupMetricsGenerator.GenerateChangedGroupList write AD entry to changed list- displayName='{0}', distinguishedName='{1}', smtpAddress='{2}', objectGuid='{3}', recipientType='{4}', whenChangedUTCString='{5}'", new object[]
								{
									attribute2,
									attribute4,
									attribute,
									guid,
									recipientType,
									attribute3
								});
							}
						}
						while (xmlReader.ReadToFollowing("ADEntry"));
					}
				}
			}
			catch (Exception ex)
			{
				this.exception = ex;
				GroupMetricsGenerator.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToGetListOfChangedGroupsForDomain, null, new object[]
				{
					domain,
					this.GetOrganizationIdString(),
					ex.GetType().FullName,
					ex.Message
				});
				return false;
			}
			return true;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x000611B4 File Offset: 0x0005F3B4
		private bool GetMetricsForChangedGroups(GroupMetricsGeneratorTaskContext context)
		{
			string text = this.changedGroupListPath;
			TypedHashSet calculatedGroups = new TypedHashSet(20000);
			try
			{
				if (GroupMetricsUtility.Fault == GroupMetricsFault.UnreadableChangedGroupFile)
				{
					text = this.tenantDirectory;
				}
				List<string> list = new List<string>();
				Dictionary<string, object> parentGroupDictionary = new Dictionary<string, object>();
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(base.OrgId), 1316, "GetMetricsForChangedGroups", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\DirectoryProcessor\\GroupMetricsGenerator\\GroupMetricsGenerator.cs");
				using (StreamReader streamReader = new StreamReader(text))
				{
					string text2;
					if (context.LastProcessedGroupDistinguishedName != null)
					{
						while ((text2 = streamReader.ReadLine()) != null && text2 != context.LastProcessedGroupDistinguishedName)
						{
						}
					}
					DateTime utcNow = DateTime.UtcNow;
					while ((text2 = streamReader.ReadLine()) != null)
					{
						this.GetMetricsForSingleGroup(tenantOrRootOrgRecipientSession, text2, calculatedGroups, parentGroupDictionary, list);
						context.LastProcessedGroupDistinguishedName = text2;
						if ((Utilities.TestForceYieldChunk && this.groupsExpanded > 2) || DateTime.UtcNow - utcNow > this.OneChunkTimeLimit)
						{
							break;
						}
						base.RunData.ThrowIfShuttingDown();
					}
					if (text2 == null)
					{
						context.AllChunksFinished = true;
					}
				}
				for (int i = 0; i < list.Count; i++)
				{
					string text2 = list[i];
					this.GetMetricsForSingleGroup(tenantOrRootOrgRecipientSession, text2, calculatedGroups, parentGroupDictionary, list);
				}
				return true;
			}
			catch (FileNotFoundException)
			{
				context.AllChunksFinished = true;
				return true;
			}
			catch (IOException ex)
			{
				GroupMetricsGenerator.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToReadChangedGroupList, null, new object[]
				{
					this.GetOrganizationIdString(),
					text,
					ex.GetType().FullName,
					ex.Message
				});
			}
			catch (UnauthorizedAccessException ex2)
			{
				GroupMetricsGenerator.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToReadChangedGroupList, null, new object[]
				{
					this.GetOrganizationIdString(),
					text,
					ex2.GetType().FullName,
					ex2.Message
				});
			}
			catch (SecurityException ex3)
			{
				GroupMetricsGenerator.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToReadChangedGroupList, null, new object[]
				{
					this.GetOrganizationIdString(),
					text,
					ex3.GetType().FullName,
					ex3.Message
				});
			}
			return false;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00061458 File Offset: 0x0005F658
		protected override DirectoryProcessorBaseTaskContext DoChunkWork(DirectoryProcessorBaseTaskContext context, RecipientType recipientType)
		{
			base.Logger.TraceDebug(this, "Entering GroupMetricsGenerator.DoChunkWork recipientType='{0}' at {1}", new object[]
			{
				recipientType.ToString(),
				DateTime.UtcNow.ToString("o")
			});
			if ((context.TaskStatus & TaskStatus.DLADCrawlerFailed) != TaskStatus.NoError)
			{
				GroupMetricsGenerator.EventLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_GroupMetricsGenerationSkippedNoADFile, null, new object[]
				{
					base.RunData.TenantId,
					base.RunData.RunId,
					recipientType
				});
				return null;
			}
			GroupMetricsGeneratorTaskContext groupMetricsGeneratorTaskContext = context as GroupMetricsGeneratorTaskContext;
			if (groupMetricsGeneratorTaskContext == null)
			{
				groupMetricsGeneratorTaskContext = new GroupMetricsGeneratorTaskContext(context.MailboxData, context.Job, context.TaskQueue, context.Step, context.TaskStatus, context.RunData, context.DeferredFinalizeTasks);
				this.Initialize(recipientType);
				base.Logger.TraceDebug(this, "First time GroupMetricsGenerator.DoChunkWork is called.", new object[0]);
			}
			this.syncType = this.UpdateGroupMetrics(groupMetricsGeneratorTaskContext);
			if (GroupMetricsSyncType.Failed == this.syncType || groupMetricsGeneratorTaskContext.AllChunksFinished)
			{
				return null;
			}
			return groupMetricsGeneratorTaskContext;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0006156C File Offset: 0x0005F76C
		private bool IsExternal(ADRawEntry member)
		{
			string targetAddressDomain = MailTipsUtility.GetTargetAddressDomain((ProxyAddress)member[ADRecipientSchema.ExternalEmailAddress]);
			return !string.IsNullOrEmpty(targetAddressDomain) && !this.domains.IsInternal(targetAddressDomain);
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x000615A8 File Offset: 0x0005F7A8
		private bool TenantHasChangedGroups()
		{
			return this.changedGroups != 0 || base.OrgId == OrganizationId.ForestWideOrgId;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x000615C8 File Offset: 0x0005F7C8
		private string GetOrganizationIdString()
		{
			if (null == base.OrgId || OrganizationId.ForestWideOrgId.Equals(base.OrgId))
			{
				return string.Empty;
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}{1} {2}", new object[]
			{
				Environment.NewLine,
				Strings.mailTipsTenant,
				((IOrganizationIdForEventLog)base.OrgId).IdForEventLog
			});
		}

		// Token: 0x04000A5E RID: 2654
		internal const string CookieFileNamePrefix = "Cookie_";

		// Token: 0x04000A5F RID: 2655
		internal const string ChangedGroupListFileName = "ChangedGroups.txt";

		// Token: 0x04000A60 RID: 2656
		private const int CalculatedGroupsHashInitialCapacity = 20000;

		// Token: 0x04000A61 RID: 2657
		private const string QueryFilterDateTimeFormatter = "yyyyMMddHHmmss.0Z";

		// Token: 0x04000A62 RID: 2658
		private readonly TimeSpan OneChunkTimeLimit = TimeSpan.FromMinutes(8.0);

		// Token: 0x04000A63 RID: 2659
		internal static readonly ExEventLog EventLogger = GroupMetricsUtility.Logger;

		// Token: 0x04000A64 RID: 2660
		private static readonly TimeSpan CookieLifetime = TimeSpan.FromDays(7.0);

		// Token: 0x04000A65 RID: 2661
		private static IList<PropertyDefinition> groupProperties = new List<PropertyDefinition>(ADRecipientExpansion.RequiredProperties);

		// Token: 0x04000A66 RID: 2662
		private Exception exception;

		// Token: 0x04000A67 RID: 2663
		private GroupMetricsSyncType syncType;

		// Token: 0x04000A68 RID: 2664
		private ICollection<DirectoryProcessorMailboxData> mailboxesToProcess;

		// Token: 0x04000A69 RID: 2665
		private DateTime taskStartTime;

		// Token: 0x04000A6A RID: 2666
		private OrganizationDomains domains;

		// Token: 0x04000A6B RID: 2667
		private TimeSpan fullSyncInterval = TimeSpan.FromDays(7.0);

		// Token: 0x04000A6C RID: 2668
		private GroupMetricsSyncType lastSyncType;

		// Token: 0x04000A6D RID: 2669
		private List<GroupMetricsCookie> updatedCookies;

		// Token: 0x04000A6E RID: 2670
		private readonly string tenantDirectory;

		// Token: 0x04000A6F RID: 2671
		private readonly string changedGroupListPath;

		// Token: 0x04000A70 RID: 2672
		private int changedGroups;

		// Token: 0x04000A71 RID: 2673
		private int groupsExpanded;

		// Token: 0x04000A72 RID: 2674
		private ICollection<string> currentCookiePathCollection;

		// Token: 0x04000A73 RID: 2675
		private bool hasDownloadedCookiesFromMailbox;
	}
}
