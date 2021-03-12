using System;
using System.Collections;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.ClientAccess.Messages;
using Microsoft.Exchange.UM.Prompts.Provisioning;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000203 RID: 515
	internal class UMAACustomPromptRpcRequestHandler : UMPromptRpcRequestHandler
	{
		// Token: 0x06000F0A RID: 3850 RVA: 0x0004418C File Offset: 0x0004238C
		protected override ArrayList GetPrompts(RequestBase requestBase)
		{
			UMAACustomPromptRpcRequest umaacustomPromptRpcRequest = requestBase as UMAACustomPromptRpcRequest;
			ArrayList customPrompts;
			using (IPublishingSession publishingSession = PublishingPoint.GetPublishingSession("UM", umaacustomPromptRpcRequest.AutoAttendant))
			{
				this.promptFile = publishingSession.DownloadAsWav(umaacustomPromptRpcRequest.PromptFileName);
				if (Utils.RunningInTestMode)
				{
					string path;
					this.promptFile = UMPromptRpcRequestHandler.ChangeFileNameForTests(this.promptFile, umaacustomPromptRpcRequest.PromptFileName, out path);
					customPrompts = this.GetCustomPrompts(path, umaacustomPromptRpcRequest.AutoAttendant.Language.Culture);
				}
				else
				{
					customPrompts = this.GetCustomPrompts(this.promptFile.FilePath, umaacustomPromptRpcRequest.AutoAttendant.Language.Culture);
				}
			}
			return customPrompts;
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0004423C File Offset: 0x0004243C
		private ArrayList GetCustomPrompts(string path, CultureInfo c)
		{
			ArrayList arrayList = GlobCfg.DefaultPromptHelper.Build(null, c, new PromptConfigBase[]
			{
				GlobCfg.DefaultPromptsForPreview.AACustomPrompt
			});
			VariablePrompt<string>.SetActualPromptValues(arrayList, "customPrompt", path);
			return arrayList;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00044274 File Offset: 0x00042474
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing && this.promptFile != null)
				{
					this.promptFile.Dispose();
				}
			}
			finally
			{
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x000442B4 File Offset: 0x000424B4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UMAACustomPromptRpcRequestHandler>(this);
		}

		// Token: 0x04000B32 RID: 2866
		private ITempFile promptFile;
	}
}
