using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000348 RID: 840
	internal class MarkAllItemsAsRead : MultiStepServiceCommand<MarkAllItemsAsReadRequest, ServiceResultNone>
	{
		// Token: 0x060017B1 RID: 6065 RVA: 0x0007EEFE File Offset: 0x0007D0FE
		public MarkAllItemsAsRead(CallContext callContext, MarkAllItemsAsReadRequest request) : base(callContext, request)
		{
			this.InitializeTracers();
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0007EF24 File Offset: 0x0007D124
		internal override void PreExecuteCommand()
		{
			this.folderIds = base.Request.FolderIds;
			this.readFlag = base.Request.ReadFlag;
			this.supressReadReceipts = base.Request.SuppressReadReceipts;
			ServiceCommandBase.ThrowIfNullOrEmpty<BaseFolderId>(this.folderIds, "folderIds", "MarkAllItemsAsRead::PreExecuteCommand");
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0007EF7C File Offset: 0x0007D17C
		internal override IExchangeWebMethodResponse GetResponse()
		{
			MarkAllItemsAsReadResponse markAllItemsAsReadResponse = new MarkAllItemsAsReadResponse();
			markAllItemsAsReadResponse.BuildForNoReturnValue(base.Results);
			return markAllItemsAsReadResponse;
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x0007EF9C File Offset: 0x0007D19C
		internal override int StepCount
		{
			get
			{
				return this.folderIds.Length;
			}
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x0007EFA8 File Offset: 0x0007D1A8
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			IdAndSession folderIdAndSession = base.IdConverter.ConvertFolderIdToIdAndSessionReadOnly(this.folderIds[base.CurrentStep]);
			this.MarkAllItemsAsReadOrUnread(folderIdAndSession);
			this.objectsChanged++;
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0007EFF0 File Offset: 0x0007D1F0
		private void MarkAllItemsAsReadOrUnread(IdAndSession folderIdAndSession)
		{
			this.tracer.TraceDebug<StoreId>((long)this.GetHashCode(), "MarkAllItemsAsReadOrUnread called for folderId '{0}'", folderIdAndSession.Id);
			bool suppressReadReceipts = this.supressReadReceipts;
			MailboxSession mailboxSession = folderIdAndSession.Session as MailboxSession;
			if (mailboxSession != null)
			{
				StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.JunkEmail);
				if (folderIdAndSession.Id.Equals(defaultFolderId))
				{
					suppressReadReceipts = true;
				}
			}
			else if (folderIdAndSession.Session is PublicFolderSession)
			{
				suppressReadReceipts = true;
			}
			using (Folder folder = Folder.Bind(folderIdAndSession.Session, folderIdAndSession.Id, null))
			{
				if (this.readFlag)
				{
					folder.MarkAllAsRead(suppressReadReceipts);
				}
				else
				{
					folder.MarkAllAsUnread(suppressReadReceipts);
				}
			}
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0007F0A0 File Offset: 0x0007D2A0
		protected override void LogTracesForCurrentRequest()
		{
			ServiceCommandBase.TraceLoggerFactory.Create(base.CallContext.HttpContext.Response.Headers).LogTraces(this.requestTracer);
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0007F0CC File Offset: 0x0007D2CC
		private void InitializeTracers()
		{
			ITracer tracer;
			if (!base.IsRequestTracingEnabled)
			{
				ITracer instance = NullTracer.Instance;
				tracer = instance;
			}
			else
			{
				tracer = new InMemoryTracer(ExTraceGlobals.MarkAllItemsAsReadTracer.Category, ExTraceGlobals.MarkAllItemsAsReadTracer.TraceTag);
			}
			this.requestTracer = tracer;
			this.tracer = ExTraceGlobals.MarkAllItemsAsReadTracer.Compose(this.requestTracer);
		}

		// Token: 0x04000FED RID: 4077
		protected BaseFolderId[] folderIds;

		// Token: 0x04000FEE RID: 4078
		private bool readFlag;

		// Token: 0x04000FEF RID: 4079
		protected bool supressReadReceipts;

		// Token: 0x04000FF0 RID: 4080
		protected ITracer tracer = ExTraceGlobals.MarkAllItemsAsReadTracer;

		// Token: 0x04000FF1 RID: 4081
		private ITracer requestTracer = NullTracer.Instance;
	}
}
