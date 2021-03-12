using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.DirectoryProcessorAssistant;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant
{
	// Token: 0x02000197 RID: 407
	internal abstract class ADCrawler : DirectoryProcessorBaseTask
	{
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x0005D935 File Offset: 0x0005BB35
		protected override Trace Trace
		{
			get
			{
				return ExTraceGlobals.ADCrawlerTracer;
			}
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0005D93C File Offset: 0x0005BB3C
		public ADCrawler(RunData runData) : base(runData)
		{
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001001 RID: 4097
		public abstract string ADEntriesFileName { get; }

		// Token: 0x06001002 RID: 4098 RVA: 0x0005D945 File Offset: 0x0005BB45
		public static bool TryParseWhenChangedUtc(string input, out DateTime output)
		{
			return DateTime.TryParse(input, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out output);
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0005D955 File Offset: 0x0005BB55
		public override bool ShouldWatson(Exception e)
		{
			return !(e is IOException) && !(e is UnauthorizedAccessException) && !(e is ADTransientException) && !(e is ADOperationException) && !(e is DataValidationException);
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001004 RID: 4100
		public abstract QueryFilter RecipientFilter { get; }

		// Token: 0x06001005 RID: 4101 RVA: 0x0005D984 File Offset: 0x0005BB84
		public static ADCrawler Create(RunData runData, RecipientType recipientType)
		{
			ADCrawler result = null;
			if (recipientType != RecipientType.User)
			{
				if (recipientType != RecipientType.Group)
				{
					ExAssert.RetailAssert(false, "Add new crawler. ");
				}
				else
				{
					result = new DLADCrawler(runData);
				}
			}
			else
			{
				result = new UserADCrawler(runData);
			}
			return result;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0005D9BD File Offset: 0x0005BBBD
		public static string GetEntriesFilePath(string runFolderPath, string ADEntriesFileName)
		{
			return Utilities.GetEntriesFilePath(runFolderPath, ADEntriesFileName, ".xml");
		}

		// Token: 0x06001007 RID: 4103
		protected abstract void UpdateRecipientCount(int recipientCount);

		// Token: 0x06001008 RID: 4104 RVA: 0x0005D9CC File Offset: 0x0005BBCC
		protected override DirectoryProcessorBaseTaskContext DoChunkWork(DirectoryProcessorBaseTaskContext context, RecipientType recipientType)
		{
			try
			{
				if (!(context is ADCrawlerTaskContext))
				{
					ADCrawlerTaskContext adcrawlerTaskContext = new ADCrawlerTaskContext(context.MailboxData, context.Job, context.TaskQueue, context.Step, context.TaskStatus, context.RunData, context.DeferredFinalizeTasks);
					this.Initialize(recipientType);
					base.Logger.TraceDebug(this, "First time ADCrawler.DoChunkWork is called. ", new object[0]);
				}
				this.DownloadRecipients(base.RunData.RunFolderPath);
			}
			catch (Exception obj)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CopyADToFileFailed, null, new object[]
				{
					this.ADEntriesFileName,
					base.TenantId,
					base.RunId,
					CommonUtil.ToEventLogString(obj)
				});
				if (recipientType != RecipientType.User)
				{
					if (recipientType == RecipientType.Group)
					{
						context.TaskStatus |= TaskStatus.DLADCrawlerFailed;
					}
					else
					{
						ExAssert.RetailAssert(false, "Unsupported type. ");
					}
				}
				else
				{
					context.TaskStatus |= TaskStatus.UserADCrawlerFailed;
				}
				throw;
			}
			if (recipientType == RecipientType.User)
			{
				OrgMailboxScaleOutHelper orgMailboxScaleOutHelper = new OrgMailboxScaleOutHelper(base.RunData, base.Logger);
				orgMailboxScaleOutHelper.CheckScaleRequirements();
			}
			return null;
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0005DB38 File Offset: 0x0005BD38
		private void DownloadRecipients(string runFolderPath)
		{
			base.Logger.TraceDebug(this, "Entering ADCrawler.DownloadRecipients", new object[0]);
			string entriesFilePath = this.GetEntriesFilePath(runFolderPath);
			base.Logger.TraceDebug(this, "ADCrawler.DownloadRecipients - entriesFilePath='{0}'", new object[]
			{
				entriesFilePath
			});
			using (XmlWriter entryWriter = XmlWriter.Create(entriesFilePath, this.CreateXmlWriterSettings()))
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CopyADToFileStarted, null, new object[]
				{
					this.ADEntriesFileName,
					base.TenantId,
					base.RunId
				});
				int numRecipients = 0;
				entryWriter.WriteStartDocument();
				entryWriter.WriteStartElement("ADEntries");
				IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromOrganizationId(base.OrgId, null);
				iadrecipientLookup.ProcessRecipients(this.RecipientFilter, GrammarRecipientHelper.LookupProperties, delegate(ADRawEntry entry)
				{
					numRecipients++;
					this.WriteADEntry(entryWriter, entry);
					this.RunData.ThrowIfShuttingDown();
				}, 5);
				entryWriter.WriteEndElement();
				entryWriter.WriteEndDocument();
				this.UpdateRecipientCount(numRecipients);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CopyADToFileCompleted, null, new object[]
				{
					this.ADEntriesFileName,
					base.TenantId,
					base.RunId
				});
			}
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0005DCD0 File Offset: 0x0005BED0
		private void WriteADEntry(XmlWriter entryWriter, ADRawEntry entry)
		{
			base.Logger.TraceDebug(this, "Entering ADCrawler.WriteADEntry", new object[0]);
			object[] properties = entry.GetProperties(GrammarRecipientHelper.LookupProperties);
			RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)properties[5];
			bool flag = this.ShouldAcceptADEntry(recipientTypeDetails);
			if (flag)
			{
				entryWriter.WriteStartElement("ADEntry");
				int i = 0;
				while (i < properties.Length)
				{
					string text = string.Empty;
					if (properties[i] == null)
					{
						base.Logger.TraceDebug(this, "ADEntry - Property='{0}', Value is null", new object[]
						{
							GrammarRecipientHelper.LookupProperties[i].Name
						});
						goto IL_22D;
					}
					if (GrammarRecipientHelper.LookupProperties[i] == ADRecipientSchema.UMRecipientDialPlanId)
					{
						ADObjectId adobjectId = properties[i] as ADObjectId;
						text = adobjectId.ObjectGuid.ToString();
						goto IL_22D;
					}
					if (GrammarRecipientHelper.LookupProperties[i] == ADRecipientSchema.AddressListMembership)
					{
						ADMultiValuedProperty<ADObjectId> admultiValuedProperty = properties[i] as ADMultiValuedProperty<ADObjectId>;
						List<string> list = new List<string>(admultiValuedProperty.Count);
						foreach (ADObjectId adobjectId2 in admultiValuedProperty)
						{
							list.Add(adobjectId2.ObjectGuid.ToString());
						}
						text = string.Join(",", list.ToArray());
						goto IL_22D;
					}
					if (GrammarRecipientHelper.LookupProperties[i] == ADRecipientSchema.DisplayName || GrammarRecipientHelper.LookupProperties[i] == ADRecipientSchema.PhoneticDisplayName || GrammarRecipientHelper.LookupProperties[i] == ADObjectSchema.DistinguishedName)
					{
						text = GrammarRecipientHelper.GetSanitizedDisplayNameForXMLEntry(properties[i].ToString());
						goto IL_22D;
					}
					if (GrammarRecipientHelper.LookupProperties[i] == ADRecipientSchema.PrimarySmtpAddress || GrammarRecipientHelper.LookupProperties[i] == ADObjectSchema.Guid || GrammarRecipientHelper.LookupProperties[i] == ADRecipientSchema.RecipientType || GrammarRecipientHelper.LookupProperties[i] == ADObjectSchema.WhenChangedUTC)
					{
						text = properties[i].ToString();
						goto IL_22D;
					}
					if (GrammarRecipientHelper.LookupProperties[i] != ADRecipientSchema.RecipientTypeDetails)
					{
						ExAssert.RetailAssert(false, "Invalid lookup property '{0}'", new object[]
						{
							GrammarRecipientHelper.LookupProperties[i].Name
						});
						goto IL_22D;
					}
					base.Logger.TraceDebug(this, "Skipping property='{0}'", new object[]
					{
						GrammarRecipientHelper.LookupProperties[i].Name
					});
					IL_27E:
					i++;
					continue;
					IL_22D:
					base.Logger.TraceDebug(this, "ADEntry -  Property='{0}', Value='{1}'", new object[]
					{
						GrammarRecipientHelper.LookupProperties[i].Name,
						text
					});
					entryWriter.WriteStartAttribute(GrammarRecipientHelper.LookupProperties[i].Name);
					entryWriter.WriteString(text);
					entryWriter.WriteEndAttribute();
					goto IL_27E;
				}
				entryWriter.WriteEndElement();
			}
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0005DF80 File Offset: 0x0005C180
		private bool ShouldAcceptADEntry(RecipientTypeDetails recipientTypeDetails)
		{
			return recipientTypeDetails != RecipientTypeDetails.MailboxPlan;
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0005DF90 File Offset: 0x0005C190
		private XmlWriterSettings CreateXmlWriterSettings()
		{
			return new XmlWriterSettings
			{
				Indent = true,
				OmitXmlDeclaration = true,
				Encoding = new UTF8Encoding(false)
			};
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0005DFC0 File Offset: 0x0005C1C0
		private string GetEntriesFilePath(string runFolderPath)
		{
			return ADCrawler.GetEntriesFilePath(runFolderPath, this.ADEntriesFileName);
		}

		// Token: 0x04000A2A RID: 2602
		private const string ADEntryElementContainerName = "ADEntries";

		// Token: 0x04000A2B RID: 2603
		public const string ADEntriesFileNameExt = ".xml";

		// Token: 0x04000A2C RID: 2604
		public const string ADEntryElementName = "ADEntry";
	}
}
