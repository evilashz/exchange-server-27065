using System;
using System.ComponentModel;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001FA RID: 506
	public static class ErrorInformationBaseExtensions
	{
		// Token: 0x0600267C RID: 9852 RVA: 0x00077928 File Offset: 0x00075B28
		public static InfoCore ToInfo(this ErrorInformationBase error)
		{
			InfoCore infoCore = new InfoCore();
			if (error.Exception is WarningException)
			{
				infoCore.MessageBoxType = ModalDialogType.Warning;
				infoCore.JsonTitle = ClientStrings.Warning;
			}
			else
			{
				infoCore.MessageBoxType = ModalDialogType.Error;
				infoCore.JsonTitle = ClientStrings.Error;
				infoCore.StackTrace = error.CallStack;
			}
			if (error.Exception is LocalizedException)
			{
				infoCore.HelpUrl = HelpUtil.BuildErrorAssistanceUrl(error.Exception as LocalizedException);
				infoCore.Help = ClientStrings.ClickHereForHelp;
			}
			infoCore.Message = error.Message;
			infoCore.Details = ((error.Exception.InnerException != null) ? error.Exception.InnerException.GetFullMessage() : string.Empty);
			return infoCore;
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x000779E7 File Offset: 0x00075BE7
		public static InfoCore[] ToInfos(this ErrorInformationBase[] errors)
		{
			return Array.ConvertAll<ErrorInformationBase, InfoCore>(errors, (ErrorInformationBase x) => x.ToInfo());
		}
	}
}
