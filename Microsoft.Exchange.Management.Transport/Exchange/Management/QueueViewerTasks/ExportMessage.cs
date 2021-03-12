using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Transport.QueueViewer;

namespace Microsoft.Exchange.Management.QueueViewerTasks
{
	// Token: 0x02000076 RID: 118
	[Cmdlet("Export", "Message", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ExportMessage : MessageActionWithIdentity
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x000103B1 File Offset: 0x0000E5B1
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return QueueViewerStrings.ConfirmationMessageExportMessage(base.Identity.ToString());
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000103C4 File Offset: 0x0000E5C4
		protected override void RunAction()
		{
			using (QueueViewerClient<ExtensibleMessageInfo> queueViewerClient = new QueueViewerClient<ExtensibleMessageInfo>((string)base.Server))
			{
				try
				{
					int num = 0;
					for (;;)
					{
						byte[] array = queueViewerClient.ReadMessageBody(base.Identity, num, 65536);
						if (array == null)
						{
							break;
						}
						num += array.Length;
						BinaryFileDataObject binaryFileDataObject = new BinaryFileDataObject();
						binaryFileDataObject.SetIdentity(base.Identity);
						binaryFileDataObject.FileData = array;
						base.WriteObject(binaryFileDataObject);
					}
				}
				catch (RpcException ex)
				{
					if (ex.ErrorCode == 1753 || ex.ErrorCode == 1727)
					{
						base.WriteError(ErrorMapper.GetLocalizedException(ex.ErrorCode, null, base.Server), (ErrorCategory)1002, null);
					}
					throw;
				}
			}
		}

		// Token: 0x0400017F RID: 383
		private const int BlockSize = 65536;
	}
}
