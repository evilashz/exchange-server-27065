using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000658 RID: 1624
	public class ServerUploadManager
	{
		// Token: 0x060046B2 RID: 18098 RVA: 0x000D5D42 File Offset: 0x000D3F42
		private ServerUploadManager()
		{
		}

		// Token: 0x060046B3 RID: 18099 RVA: 0x000D5D4C File Offset: 0x000D3F4C
		public PowerShellResults ProcessFileUploadRequest(HttpContext httpContext)
		{
			HttpPostedFile httpPostedFile = httpContext.Request.Files["uploadFile"];
			if (httpPostedFile != null && !string.IsNullOrEmpty(httpPostedFile.FileName) && httpPostedFile.ContentLength > 0)
			{
				string text = httpContext.Request.Form["handlerClass"];
				IUploadHandler uploadHandler = this.CreateInstance(text);
				if (httpPostedFile.ContentLength <= uploadHandler.MaxFileSize)
				{
					string text2 = httpContext.Request.Form["parameters"];
					object obj = text2.JsonDeserialize(uploadHandler.SetParameterType, null);
					UploadFileContext context = new UploadFileContext(httpPostedFile.FileName, httpPostedFile.InputStream);
					try
					{
						EcpEventLogConstants.Tuple_UploadRequestStarted.LogEvent(new object[]
						{
							EcpEventLogExtensions.GetUserNameToLog(),
							text,
							text2
						});
						return uploadHandler.ProcessUpload(context, (WebServiceParameters)obj);
					}
					finally
					{
						EcpEventLogConstants.Tuple_UploadRequestCompleted.LogEvent(new object[]
						{
							EcpEventLogExtensions.GetUserNameToLog()
						});
					}
				}
				ByteQuantifiedSize byteQuantifiedSize = new ByteQuantifiedSize((ulong)((long)uploadHandler.MaxFileSize));
				throw new HttpException(Strings.FileExceedsLimit(byteQuantifiedSize.ToString()));
			}
			throw new HttpException(Strings.UploadFileCannotBeEmpty);
		}

		// Token: 0x060046B4 RID: 18100 RVA: 0x000D5EA4 File Offset: 0x000D40A4
		private IUploadHandler CreateInstance(string handlerType)
		{
			if (handlerType == null)
			{
				throw new BadRequestException(new Exception("HandlerType cannot be null."));
			}
			Type type = null;
			UploadHandlers key;
			try
			{
				key = (UploadHandlers)Enum.Parse(typeof(UploadHandlers), handlerType, true);
			}
			catch (ArgumentException innerException)
			{
				throw new BadQueryParameterException("handlerClass", innerException);
			}
			ServerUploadManager.KnownHandlers.TryGetValue(key, out type);
			if (!(type != null))
			{
				throw new BadRequestException(new Exception("Unknown HandlerType: \"" + handlerType + "\" ."));
			}
			if (typeof(IUploadHandler).IsAssignableFrom(type))
			{
				return Activator.CreateInstance(type) as IUploadHandler;
			}
			throw new HttpException("HandlerType: \"" + type.FullName + "\" doesn't implement " + typeof(IUploadHandler).FullName);
		}

		// Token: 0x04002FBF RID: 12223
		internal const string UploadParameterkey = "parameters";

		// Token: 0x04002FC0 RID: 12224
		internal const string HandlerClassKey = "handlerClass";

		// Token: 0x04002FC1 RID: 12225
		internal const string UploadFileKey = "uploadFile";

		// Token: 0x04002FC2 RID: 12226
		internal const string UploadHandlerFileName = "UploadHandler.ashx";

		// Token: 0x04002FC3 RID: 12227
		internal static readonly LazilyInitialized<ServerUploadManager> Instance = new LazilyInitialized<ServerUploadManager>(() => new ServerUploadManager());

		// Token: 0x04002FC4 RID: 12228
		private static readonly Dictionary<UploadHandlers, Type> KnownHandlers = new Dictionary<UploadHandlers, Type>
		{
			{
				UploadHandlers.UMAutoAttendantService,
				typeof(UMAutoAttendantService)
			},
			{
				UploadHandlers.UMDialPlanService,
				typeof(UMDialPlanService)
			},
			{
				UploadHandlers.AddExtensionService,
				typeof(AddExtensionService)
			},
			{
				UploadHandlers.OrgAddExtensionService,
				typeof(OrgAddExtensionService)
			},
			{
				UploadHandlers.UserPhotoService,
				typeof(UserPhotoService)
			},
			{
				UploadHandlers.FileEncodeUploadHandler,
				typeof(FileEncodeUploadHandler)
			},
			{
				UploadHandlers.MigrationCsvUploadHandler,
				typeof(MigrationCsvUploadHandler)
			},
			{
				UploadHandlers.FingerprintUploadHandler,
				typeof(FingerprintUploadHandler)
			}
		};
	}
}
