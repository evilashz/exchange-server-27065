using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.DirectoryProcessorAssistant;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMDtmfMapGenerator
{
	// Token: 0x0200019F RID: 415
	internal class DtmfMapGenerator : DirectoryProcessorBaseTask
	{
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x0005F13D File Offset: 0x0005D33D
		protected override Trace Trace
		{
			get
			{
				return ExTraceGlobals.DtmfMapGeneratorTracer;
			}
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0005F144 File Offset: 0x0005D344
		public DtmfMapGenerator(RunData runData, ICollection<DirectoryProcessorMailboxData> mailboxesToProcess) : base(runData)
		{
			ValidateArgument.NotNull(mailboxesToProcess, "mailboxesToProcess");
			this.mailboxesToProcess = mailboxesToProcess;
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0005F194 File Offset: 0x0005D394
		public override bool ShouldRun(RecipientType recipientType)
		{
			base.Logger.TraceDebug(this, "Entering DtmfMapGenerator.ShouldRun recipientType='{0}', mailboxGuid='{1}', orgId='{2}'", new object[]
			{
				recipientType,
				base.MailboxGuid,
				base.OrgId
			});
			if (!this.mailboxesToProcess.Contains(new DirectoryProcessorMailboxData(base.OrgId, base.DatabaseGuid, base.MailboxGuid)))
			{
				base.Logger.TraceDebug(this, "Entering DtmfMapGenerator.ShouldRun mailboxGuid='{0}', orgId='{1}' should not be processed", new object[]
				{
					base.MailboxGuid,
					base.OrgId
				});
				return false;
			}
			if (VariantConfiguration.InvariantNoFlightingSnapshot.UM.DTMFMapGenerator.Enabled)
			{
				base.Logger.TraceDebug(this, "Entering DtmfMapGenerator.ShouldRun is false in datacenter", new object[0]);
				return false;
			}
			return true;
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0005F263 File Offset: 0x0005D463
		public override bool ShouldWatson(Exception e)
		{
			return !(e is IOException) && !(e is UnauthorizedAccessException) && !(e is ADTransientException) && !(e is ADOperationException) && !(e is DataValidationException);
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0005F290 File Offset: 0x0005D490
		protected override DirectoryProcessorBaseTaskContext DoChunkWork(DirectoryProcessorBaseTaskContext context, RecipientType recipientType)
		{
			ValidateArgument.NotNull(context, "context");
			base.Logger.TraceDebug(this, "Entering DtmfMapGenerator.DoChunkWork recipientType='{0}'", new object[]
			{
				recipientType
			});
			if ((RecipientType.Group == recipientType && (context.TaskStatus & TaskStatus.DLADCrawlerFailed) != TaskStatus.NoError) || (RecipientType.User == recipientType && (context.TaskStatus & TaskStatus.UserADCrawlerFailed) != TaskStatus.NoError))
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_DtmfMapGenerationSkippedNoADFile, null, new object[]
				{
					base.TenantId,
					base.RunId,
					recipientType
				});
				return null;
			}
			DtmfMapGeneratorTaskContext dtmfMapGeneratorTaskContext = context as DtmfMapGeneratorTaskContext;
			if (dtmfMapGeneratorTaskContext == null)
			{
				base.Logger.TraceDebug(this, "First time DtmfMapGenerator.DoChunkWork is called", new object[0]);
				dtmfMapGeneratorTaskContext = this.InitializeTask(recipientType, context);
			}
			DtmfMapGeneratorTaskContext dtmfMapGeneratorTaskContext2 = dtmfMapGeneratorTaskContext;
			try
			{
				if (this.ProcessNextChunk(dtmfMapGeneratorTaskContext))
				{
					this.LogCompletion(dtmfMapGeneratorTaskContext, recipientType);
					dtmfMapGeneratorTaskContext2 = null;
				}
			}
			catch (Exception)
			{
				dtmfMapGeneratorTaskContext2 = null;
				throw;
			}
			finally
			{
				if (dtmfMapGeneratorTaskContext2 == null)
				{
					dtmfMapGeneratorTaskContext.Dispose();
				}
			}
			return dtmfMapGeneratorTaskContext2;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0005F390 File Offset: 0x0005D590
		private DtmfMapGeneratorTaskContext InitializeTask(RecipientType recipientType, DirectoryProcessorBaseTaskContext context)
		{
			base.Logger.TraceDebug(null, "Entering DtmfMapGenerator.InitializeTask recipientType='{0}'", new object[]
			{
				recipientType
			});
			string text = null;
			if (recipientType != RecipientType.User)
			{
				if (recipientType != RecipientType.Group)
				{
					ExAssert.RetailAssert(false, "Unsupported recipient type");
				}
				else
				{
					text = "DistributionList";
				}
			}
			else
			{
				text = "User";
			}
			string entriesFilePath = ADCrawler.GetEntriesFilePath(base.RunData.RunFolderPath, text);
			base.Logger.TraceDebug(null, "DtmfMapGenerator.InitializeTask adEntriesFileName='{0}', entriesFilePath='{1}'", new object[]
			{
				text,
				entriesFilePath
			});
			XmlReader adEntriesReader = XmlReader.Create(entriesFilePath);
			DtmfMapGenerationMetadata metadata = this.GetMetadata(recipientType);
			DtmfMapGeneratorTaskContext dtmfMapGeneratorTaskContext = new DtmfMapGeneratorTaskContext(context.MailboxData, context.Job, context.TaskQueue, context.Step, context.TaskStatus, adEntriesReader, metadata, this.IsFullUpdateRequired(metadata), context.RunData, context.DeferredFinalizeTasks);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_DtmfMapGenerationStarted, null, new object[]
			{
				base.TenantId,
				base.RunId,
				recipientType.ToString(),
				dtmfMapGeneratorTaskContext.IsFullUpdate
			});
			return dtmfMapGeneratorTaskContext;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0005F57C File Offset: 0x0005D77C
		private bool ProcessNextChunk(DtmfMapGeneratorTaskContext taskContext)
		{
			base.Logger.TraceDebug(this, "Entering DtmfMapGenerator.ProcessNextChunk", new object[0]);
			IRecipientSession writableSession = this.CreateWritableSession(base.RunData.OrgId);
			List<ADObjectId> recipientIds;
			bool recipientIds2 = this.GetRecipientIds(taskContext, out recipientIds);
			if (recipientIds.Count > 0)
			{
				Result<ADRecipient>[] recipients = null;
				Exception ex = Utilities.RunSafeADOperation(this.Trace, delegate
				{
					recipients = writableSession.ReadMultiple(recipientIds.ToArray());
				}, "DtmfMapGenerator.ProcessNextChunk: Batched read of users matching given list of ADObjectIds");
				if (ex != null)
				{
					throw ex;
				}
				for (int i = 0; i < recipients.Length; i++)
				{
					Result<ADRecipient> result = recipients[i];
					if (result.Error == null)
					{
						ADRecipient recipient = result.Data;
						Utilities.RunSafeADOperation(this.Trace, delegate
						{
							this.Logger.TraceDebug(this, "DtmfMapGenerator.ProcessNextChunk - Processing recipient='{0}'", new object[]
							{
								recipient
							});
							recipient.PopulateDtmfMap(true);
							if (recipient.UMDtmfMap.Changed)
							{
								writableSession.Save(recipient);
							}
						}, "DtmfMapGenerator.ProcessNextChunk : Calculating and saving DTMF map for user");
					}
					else
					{
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_DtmfMapUpdateFailed, null, new object[]
						{
							recipientIds[i],
							base.RunData.TenantId,
							base.RunData.RunId
						});
					}
					base.RunData.ThrowIfShuttingDown();
				}
			}
			base.Logger.TraceDebug(this, "DtmfMapGenerator.ProcessNextChunk - Number of recipients processed='{0}'", new object[]
			{
				recipientIds.Count
			});
			return recipientIds2;
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0005F730 File Offset: 0x0005D930
		private bool GetRecipientIds(DtmfMapGeneratorTaskContext taskContext, out List<ADObjectId> recipientIds)
		{
			base.Logger.TraceDebug(this, "Entering DtmfMapGenerator.GetRecipientIds", new object[0]);
			recipientIds = new List<ADObjectId>(100);
			bool result = true;
			XmlReader adEntriesReader = taskContext.AdEntriesReader;
			while (adEntriesReader.ReadToFollowing("ADEntry"))
			{
				base.Logger.TraceDebug(this, "DtmfMapGenerator.GetRecipientIds - Read next recipient id", new object[0]);
				Guid guid = new Guid(adEntriesReader.GetAttribute(GrammarRecipientHelper.LookupProperties[3].Name));
				RecipientType recipientType = (RecipientType)Enum.Parse(typeof(RecipientType), adEntriesReader.GetAttribute(GrammarRecipientHelper.LookupProperties[4].Name));
				string attribute = adEntriesReader.GetAttribute(GrammarRecipientHelper.LookupProperties[7].Name);
				DateTime dateTime = this.ParseDateTime(attribute);
				base.Logger.TraceDebug(this, "DtmfMapGenerator.GetRecipientIds - objectGuid='{0}', recipientType='{1}', whenChangedUtcString='{2}', whenChangedUtc='{3}'", new object[]
				{
					guid,
					recipientType,
					attribute,
					dateTime
				});
				DateTime lastIncrementalUpdateTimeUtc = taskContext.Metadata.LastIncrementalUpdateTimeUtc;
				if (recipientType != RecipientType.DynamicDistributionGroup && (taskContext.IsFullUpdate || dateTime > lastIncrementalUpdateTimeUtc))
				{
					base.Logger.TraceDebug(this, "DtmfMapGenerator.GetRecipientIds - Adding objectGuid='{0}'", new object[]
					{
						guid
					});
					recipientIds.Add(new ADObjectId(guid));
					if (recipientIds.Count >= 100)
					{
						base.Logger.TraceDebug(this, "Max ids read from file in this chunk='{0}'", new object[]
						{
							recipientIds.Count
						});
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0005F8C4 File Offset: 0x0005DAC4
		private IRecipientSession CreateWritableSession(OrganizationId orgId)
		{
			ExAssert.RetailAssert(orgId == OrganizationId.ForestWideOrgId, "organizationId should be ForestWideOrgId");
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), orgId, null, false);
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, null, 0, false, ConsistencyMode.IgnoreInvalid, null, sessionSettings, 474, "CreateWritableSession", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\DirectoryProcessor\\DtmfMapGenerator\\DtmfMapGenerator.cs");
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0005F918 File Offset: 0x0005DB18
		private void LogCompletion(DtmfMapGeneratorTaskContext taskContext, RecipientType recipientType)
		{
			base.Logger.TraceDebug(null, "Entering DtmfMapGenerator.LogCompletion recipientType='{0}'", new object[]
			{
				recipientType
			});
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_DtmfMapGenerationSuccessful, null, new object[]
			{
				base.RunData.TenantId,
				base.RunData.RunId,
				recipientType
			});
			DateTime runStartTime = base.RunStartTime;
			DateTime lastFullUpdateTimeUtc = taskContext.IsFullUpdate ? base.RunStartTime : taskContext.Metadata.LastFullUpdateTimeUtc;
			DtmfMapGenerationMetadata metadata = new DtmfMapGenerationMetadata(1, base.TenantId, base.RunId, Utils.GetLocalHostFqdn(), "15.00.1497.010", runStartTime, lastFullUpdateTimeUtc);
			string metadataFileName = this.GetMetadataFileName(recipientType);
			string dtmfMapFolderPath = GrammarFileDistributionShare.GetDtmfMapFolderPath(base.RunData.OrgId, base.RunData.MailboxGuid);
			base.Logger.TraceDebug(this, "LogCompletion folderPath='{0}', fileName='{1}'", new object[]
			{
				dtmfMapFolderPath,
				metadataFileName
			});
			string text = DtmfMapGenerationMetadata.Serialize(metadata, metadataFileName, dtmfMapFolderPath);
			if (text != null)
			{
				this.UploadMetadata(text, metadataFileName);
			}
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0005FA38 File Offset: 0x0005DC38
		private DtmfMapGenerationMetadata GetMetadata(RecipientType recipientType)
		{
			base.Logger.TraceDebug(this, "GetMetadata recipientType='{0}'", new object[]
			{
				recipientType
			});
			DtmfMapGenerationMetadata dtmfMapGenerationMetadata = null;
			string metadataFilePath = this.GetMetadataFilePath(recipientType);
			if (metadataFilePath != null)
			{
				base.Logger.TraceDebug(this, "Metadata found, metadataFilePath='{0}'", new object[]
				{
					metadataFilePath
				});
				dtmfMapGenerationMetadata = DtmfMapGenerationMetadata.Deserialize(metadataFilePath);
			}
			if (dtmfMapGenerationMetadata == null)
			{
				base.Logger.TraceDebug(this, "GetMetadata could not retrieve metadata", new object[0]);
				dtmfMapGenerationMetadata = new DtmfMapGenerationMetadata(1, base.TenantId, base.RunId, Utils.GetLocalHostFqdn(), "15.00.1497.010", DateTime.MinValue, DateTime.MinValue);
			}
			return dtmfMapGenerationMetadata;
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0005FADC File Offset: 0x0005DCDC
		private bool IsFullUpdateRequired(DtmfMapGenerationMetadata metadata)
		{
			base.Logger.TraceDebug(this, "Last full update time UTC='{0}'", new object[]
			{
				metadata.LastFullUpdateTimeUtc
			});
			return DateTime.UtcNow - metadata.LastFullUpdateTimeUtc > this.FullUpdateInterval;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0005FB2C File Offset: 0x0005DD2C
		private string GetMetadataFileName(RecipientType recipientType)
		{
			string text = null;
			if (recipientType != RecipientType.User)
			{
				if (recipientType != RecipientType.Group)
				{
					ExAssert.RetailAssert(false, "Unsupported recipient type");
				}
				else
				{
					text = "DistributionList.xml";
				}
			}
			else
			{
				text = "User.xml";
			}
			base.Logger.TraceDebug(this, "GetMetadataFileName fileName='{0}'", new object[]
			{
				text
			});
			return text;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0005FB80 File Offset: 0x0005DD80
		private DateTime ParseDateTime(string input)
		{
			DateTime maxValue = DateTime.MaxValue;
			if (!ADCrawler.TryParseWhenChangedUtc(input, out maxValue))
			{
				maxValue = DateTime.MaxValue;
			}
			return maxValue;
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0005FBA4 File Offset: 0x0005DDA4
		private void UploadMetadata(string filePath, string fileName)
		{
			DtmfMapMetadataMailboxFileStore dtmfMapMetadataMailboxFileStore = DtmfMapMetadataMailboxFileStore.FromMailboxGuid(base.OrgId, base.MailboxGuid);
			if (dtmfMapMetadataMailboxFileStore != null)
			{
				dtmfMapMetadataMailboxFileStore.UploadMetadata(filePath, fileName, 1.ToString());
			}
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0005FBD8 File Offset: 0x0005DDD8
		private string GetMetadataFilePath(RecipientType recipientType)
		{
			bool flag = false;
			DateTime dateTime = DateTime.MinValue;
			string metadataFileName = this.GetMetadataFileName(recipientType);
			string dtmfMapFolderPath = GrammarFileDistributionShare.GetDtmfMapFolderPath(base.RunData.OrgId, base.RunData.MailboxGuid);
			string text = Path.Combine(dtmfMapFolderPath, metadataFileName);
			if (File.Exists(text))
			{
				flag = true;
				dateTime = File.GetLastWriteTimeUtc(text);
				base.Logger.TraceDebug(this, "GetMetadataFilePath filePath='{0}' exists, lastModifiedTimeUtc='{1}'", new object[]
				{
					text,
					dateTime
				});
			}
			if (DateTime.UtcNow - dateTime > this.MetadataExpiration)
			{
				base.Logger.TraceDebug(this, "Downloading metadata to filePath='{0}'", new object[]
				{
					text
				});
				DtmfMapMetadataMailboxFileStore dtmfMapMetadataMailboxFileStore = DtmfMapMetadataMailboxFileStore.FromMailboxGuid(base.OrgId, base.MailboxGuid);
				if (dtmfMapMetadataMailboxFileStore != null && dtmfMapMetadataMailboxFileStore.DownloadMetadata(text, metadataFileName, 1.ToString(), dateTime))
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return null;
			}
			return text;
		}

		// Token: 0x04000A4B RID: 2635
		private const int MaxChunkSize = 100;

		// Token: 0x04000A4C RID: 2636
		private const string UserMetadataFileName = "User.xml";

		// Token: 0x04000A4D RID: 2637
		private const string DLMetadataFileName = "DistributionList.xml";

		// Token: 0x04000A4E RID: 2638
		private readonly TimeSpan FullUpdateInterval = TimeSpan.FromDays(30.0);

		// Token: 0x04000A4F RID: 2639
		private readonly TimeSpan MetadataExpiration = TimeSpan.FromDays(5.0);

		// Token: 0x04000A50 RID: 2640
		private readonly ICollection<DirectoryProcessorMailboxData> mailboxesToProcess;
	}
}
