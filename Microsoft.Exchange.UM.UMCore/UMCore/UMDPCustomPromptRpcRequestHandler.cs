using System;
using System.Collections;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.ClientAccess.Messages;
using Microsoft.Exchange.UM.Prompts.Provisioning;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200020F RID: 527
	internal class UMDPCustomPromptRpcRequestHandler : UMPromptRpcRequestHandler
	{
		// Token: 0x06000F68 RID: 3944 RVA: 0x00045A50 File Offset: 0x00043C50
		protected override ArrayList GetPrompts(RequestBase requestBase)
		{
			UMDPCustomPromptRpcRequest umdpcustomPromptRpcRequest = requestBase as UMDPCustomPromptRpcRequest;
			ArrayList customPrompts;
			using (IPublishingSession publishingSession = PublishingPoint.GetPublishingSession("UM", umdpcustomPromptRpcRequest.DialPlan))
			{
				this.promptFile = publishingSession.DownloadAsWav(umdpcustomPromptRpcRequest.PromptFileName);
				if (Utils.RunningInTestMode)
				{
					string path;
					this.promptFile = UMPromptRpcRequestHandler.ChangeFileNameForTests(this.promptFile, umdpcustomPromptRpcRequest.PromptFileName, out path);
					customPrompts = this.GetCustomPrompts(path, umdpcustomPromptRpcRequest.DialPlan.DefaultLanguage.Culture);
				}
				else
				{
					customPrompts = this.GetCustomPrompts(this.promptFile.FilePath, umdpcustomPromptRpcRequest.DialPlan.DefaultLanguage.Culture);
				}
			}
			return customPrompts;
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00045B00 File Offset: 0x00043D00
		private ArrayList GetCustomPrompts(string path, CultureInfo c)
		{
			ArrayList arrayList = GlobCfg.DefaultPromptHelper.Build(null, c, new PromptConfigBase[]
			{
				GlobCfg.DefaultPromptsForPreview.AACustomPrompt
			});
			VariablePrompt<string>.SetActualPromptValues(arrayList, "customPrompt", path);
			return arrayList;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x00045B38 File Offset: 0x00043D38
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

		// Token: 0x06000F6B RID: 3947 RVA: 0x00045B78 File Offset: 0x00043D78
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UMDPCustomPromptRpcRequestHandler>(this);
		}

		// Token: 0x04000B43 RID: 2883
		private ITempFile promptFile;
	}
}
