using System;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Handler;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000079 RID: 121
	internal class MessageIteratorClient : ServerObject, IMessageIteratorClient, IDisposable, WatsonHelper.IProvideWatsonReportData
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x000219A8 File Offset: 0x0001FBA8
		internal MessageIteratorClient(CoreFolder folder)
		{
			this.session = folder.Session;
			if (!this.session.IsMoveUser)
			{
				throw new ArgumentException("MessageIteratorClient", "IsMoveUser");
			}
			this.folderId = folder.Id.ObjectId;
			this.logonString8Encoding = Encoding.ASCII;
			this.useNullLogon = true;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00021A08 File Offset: 0x0001FC08
		internal MessageIteratorClient(Folder folder, Logon logon) : base(logon)
		{
			this.session = folder.Session;
			this.folderId = folder.CoreFolder.Id.ObjectId;
			this.logonString8Encoding = base.LogonObject.LogonString8Encoding;
			this.useNullLogon = false;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00021A58 File Offset: 0x0001FC58
		public IMessage UploadMessage(bool isAssociatedMessage)
		{
			IMessage result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				CoreItem coreItem = CoreItem.Create(this.session, this.folderId, isAssociatedMessage ? CreateMessageType.Associated : CreateMessageType.Normal);
				disposeGuard.Add<CoreItem>(coreItem);
				ReferenceCount<CoreItem> referenceCount = new ReferenceCount<CoreItem>(coreItem);
				try
				{
					MessageAdaptor messageAdaptor = new MessageAdaptor(referenceCount, new MessageAdaptor.Options
					{
						IsReadOnly = false,
						IsEmbedded = false,
						DownloadBodyOption = DownloadBodyOption.AllBodyProperties,
						IsUpload = false
					}, this.logonString8Encoding, true, this.useNullLogon ? null : base.LogonObject);
					disposeGuard.Success();
					result = messageAdaptor;
				}
				finally
				{
					referenceCount.Release();
				}
			}
			return result;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00021B20 File Offset: 0x0001FD20
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MessageIteratorClient>(this);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00021B28 File Offset: 0x0001FD28
		string WatsonHelper.IProvideWatsonReportData.GetWatsonReportString()
		{
			base.CheckDisposed();
			return string.Format("MessageIteratorClient: Last Message = \"{0}\".", "<No message has been uploaded yet>");
		}

		// Token: 0x040001C4 RID: 452
		private const string LastMessage = "<No message has been uploaded yet>";

		// Token: 0x040001C5 RID: 453
		private readonly StoreSession session;

		// Token: 0x040001C6 RID: 454
		private readonly StoreObjectId folderId;

		// Token: 0x040001C7 RID: 455
		private readonly Encoding logonString8Encoding;

		// Token: 0x040001C8 RID: 456
		private readonly bool useNullLogon;
	}
}
