using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200020C RID: 524
	internal class ServerDownloadManager
	{
		// Token: 0x060026D6 RID: 9942 RVA: 0x00079314 File Offset: 0x00077514
		private ServerDownloadManager()
		{
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x0007931C File Offset: 0x0007751C
		public void ProcessDownloadRequest(HttpContext httpContext)
		{
			string handlerType = httpContext.Request.QueryString["handlerClass"];
			string text = string.Format("Download request has failed for {0}.", httpContext.Request.Path);
			IDownloadHandler downloadHandler = this.CreateInstance(handlerType, text);
			PowerShellResults powerShellResults = downloadHandler.ProcessRequest(httpContext);
			if (powerShellResults.Failed)
			{
				text = text + "Exception = " + powerShellResults.ErrorRecords.ToTraceString();
				throw new BadRequestException(new Exception(text));
			}
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x00079394 File Offset: 0x00077594
		private IDownloadHandler CreateInstance(string handlerType, string failureMsg)
		{
			Type type = null;
			if (handlerType == null)
			{
				throw new BadRequestException(new Exception(failureMsg + "HandlerType cannot be null."));
			}
			ServerDownloadManager.KnownHandlers.TryGetValue(handlerType, out type);
			if (!(type != null))
			{
				throw new BadRequestException(new Exception(failureMsg + "Unknown HandlerType: \"" + handlerType + "\" ."));
			}
			if (type.GetInterface(typeof(IDownloadHandler).FullName) != null)
			{
				return Activator.CreateInstance(type) as IDownloadHandler;
			}
			throw new HttpException(string.Concat(new string[]
			{
				failureMsg,
				"HandlerType: \"",
				type.FullName,
				"\" doesn't implement ",
				typeof(IDownloadHandler).FullName
			}));
		}

		// Token: 0x04001FA3 RID: 8099
		internal const string HandlerClassKey = "handlerClass";

		// Token: 0x04001FA4 RID: 8100
		private static readonly Dictionary<string, Type> KnownHandlers = new Dictionary<string, Type>
		{
			{
				"MigrationReportHandler",
				typeof(MigrationReportHandler)
			},
			{
				"ExportUMCallDataRecordHandler",
				typeof(ExportUMCallDataRecordHandler)
			},
			{
				"UserPhotoDownloadHandler",
				typeof(UserPhotoDownloadHandler)
			},
			{
				"ExportCsvHandler",
				typeof(ExportCsvHandler)
			},
			{
				"MigrationUserReportDownloadHandler",
				typeof(MigrationUserReportDownloadHandler)
			}
		};

		// Token: 0x04001FA5 RID: 8101
		public static readonly LazilyInitialized<ServerDownloadManager> Instance = new LazilyInitialized<ServerDownloadManager>(() => new ServerDownloadManager());
	}
}
