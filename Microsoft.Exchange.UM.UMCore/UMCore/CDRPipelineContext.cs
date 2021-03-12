using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002B7 RID: 695
	internal class CDRPipelineContext : PipelineContext
	{
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x0005A998 File Offset: 0x00058B98
		internal override Pipeline Pipeline
		{
			get
			{
				return CDRPipeline.Instance;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x0005A99F File Offset: 0x00058B9F
		// (set) Token: 0x06001514 RID: 5396 RVA: 0x0005A9A7 File Offset: 0x00058BA7
		public ADUser CDRMessageRecipient { get; set; }

		// Token: 0x06001515 RID: 5397 RVA: 0x0005A9B0 File Offset: 0x00058BB0
		public CDRPipelineContext(CDRData cdrData)
		{
			bool flag = false;
			try
			{
				ValidateArgument.NotNull(cdrData, "cdrData");
				base.MessageType = "CDR";
				this.cdrDataXml = UMTypeSerializer.SerializeToString<CDRData>(cdrData);
				this.cdrData = cdrData;
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "Submitted a CDR message to the pipeline. CDRXML = {0}", new object[]
				{
					this.cdrDataXml
				});
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.Dispose();
				}
			}
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0005AA30 File Offset: 0x00058C30
		public CDRPipelineContext(CDRData cdrData, string cdrDataXml) : base(new SubmissionHelper(cdrData.CallIdentity, PhoneNumber.Empty, cdrData.EDiscoveryUserObjectGuid, "SystemMailbox{e0dc1c29-89c3-4034-b678-e6c29d823ed9}", CultureInfo.InvariantCulture.Name, null, null, cdrData.TenantGuid))
		{
			base.MessageType = "CDR";
			this.cdrData = cdrData;
			this.cdrDataXml = cdrDataXml;
			this.CDRMessageRecipient = (base.CreateADRecipientFromObjectGuid(cdrData.EDiscoveryUserObjectGuid, cdrData.TenantGuid) as ADUser);
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0005AAA8 File Offset: 0x00058CA8
		public static CDRPipelineContext Deserialize(string cdrDataXml)
		{
			CDRData cdrdata = UMTypeSerializer.DeserializeFromString<CDRData>(cdrDataXml);
			return new CDRPipelineContext(cdrdata, cdrDataXml);
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x0005AAC5 File Offset: 0x00058CC5
		internal override void WriteCustomHeaderFields(StreamWriter headerStream)
		{
			headerStream.WriteLine("CDRData : " + this.cdrDataXml);
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0005AAE0 File Offset: 0x00058CE0
		public override PipelineDispatcher.WIThrottleData GetThrottlingData()
		{
			return new PipelineDispatcher.WIThrottleData
			{
				Key = this.CDRMessageRecipient.Database.ObjectGuid.ToString(),
				RecipientId = this.GetRecipientIdForThrottling(),
				WorkItemType = PipelineDispatcher.ThrottledWorkItemType.CDRWorkItem
			};
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0005AB2B File Offset: 0x00058D2B
		public override string GetMailboxServerId()
		{
			return this.CDRMessageRecipient.ServerLegacyDN + ":CDR";
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0005AB42 File Offset: 0x00058D42
		public override string GetRecipientIdForThrottling()
		{
			return null;
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0005AB48 File Offset: 0x00058D48
		public override void PrepareUnProtectedMessage()
		{
			try
			{
				using (IUMCallDataRecordStorage umcallDataRecordsAcessor = InterServerMailboxAccessor.GetUMCallDataRecordsAcessor(this.CDRMessageRecipient))
				{
					umcallDataRecordsAcessor.CreateUMCallDataRecord(this.cdrData);
				}
			}
			catch (LocalizedException ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.VoiceMailTracer, this, ex.ToString(), new object[0]);
				if (!(ex is QuotaExceededException) && !(ex is InvalidObjectGuidException) && !(ex is ObjectNotFoundException) && !(ex is StorageTransientException))
				{
					throw;
				}
				if (ex is QuotaExceededException)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UnableToSaveCDR, null, new object[]
					{
						Strings.EDiscoveryMailboxFull(this.CDRMessageRecipient.DistinguishedName, CommonUtil.ToEventLogString(ex))
					});
				}
				else
				{
					if (ex is StorageTransientException)
					{
						throw new MailboxUnavailableException(base.MessageType, this.CDRMessageRecipient.Database.DistinguishedName, ex.Message, ex);
					}
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UnableToSaveCDR, null, new object[]
					{
						CommonUtil.ToEventLogString(ex)
					});
				}
			}
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x0005AC64 File Offset: 0x00058E64
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CDRPipelineContext>(this);
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0005AC6C File Offset: 0x00058E6C
		protected override void WriteCommonHeaderFields(StreamWriter headerStream)
		{
		}

		// Token: 0x04000CC4 RID: 3268
		private CDRData cdrData;

		// Token: 0x04000CC5 RID: 3269
		private string cdrDataXml;
	}
}
