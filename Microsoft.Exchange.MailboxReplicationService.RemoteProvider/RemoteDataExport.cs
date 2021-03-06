using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000002 RID: 2
	internal static class RemoteDataExport
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static void ExportRoutine(IMailboxReplicationProxyService mrsProxy, long dataExportHandle, IDataImport destProxy, DataExportBatch firstBatch, bool useCompression)
		{
			try
			{
				DataExportBatch dataExportBatch;
				do
				{
					if (firstBatch != null)
					{
						dataExportBatch = firstBatch;
						firstBatch = null;
					}
					else
					{
						if (TestIntegration.Instance.AbortConnectionDuringFX)
						{
							MailboxReplicationProxyClient mailboxReplicationProxyClient = (MailboxReplicationProxyClient)mrsProxy;
							mailboxReplicationProxyClient.Abort();
						}
						dataExportBatch = mrsProxy.DataExport_ExportData2(dataExportHandle);
						if (dataExportBatch.IsLastBatch)
						{
							dataExportHandle = 0L;
						}
					}
					bool flag = false;
					try
					{
						if (dataExportBatch.Opcode != 0)
						{
							IDataMessage message = DataMessageSerializer.Deserialize(dataExportBatch.Opcode, dataExportBatch.Data, useCompression);
							destProxy.SendMessage(message);
						}
						if (dataExportBatch.FlushAfterImport)
						{
							destProxy.SendMessageAndWaitForReply(FlushMessage.Instance);
						}
						flag = true;
					}
					finally
					{
						if (!flag && dataExportHandle != 0L)
						{
							MrsTracer.ProxyClient.Error("Exception was thrown during import/flush, canceling export.", new object[0]);
							mrsProxy.DataExport_CancelExport(dataExportHandle);
							dataExportHandle = 0L;
						}
					}
				}
				while (!dataExportBatch.IsLastBatch);
			}
			finally
			{
				if (dataExportHandle != 0L)
				{
					mrsProxy.CloseHandle(dataExportHandle);
				}
			}
		}
	}
}
