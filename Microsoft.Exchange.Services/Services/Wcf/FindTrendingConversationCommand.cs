using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000916 RID: 2326
	internal sealed class FindTrendingConversationCommand : SingleStepServiceCommand<FindTrendingConversationRequest, ConversationType[]>
	{
		// Token: 0x06004367 RID: 17255 RVA: 0x000E363A File Offset: 0x000E183A
		public FindTrendingConversationCommand(CallContext callContext, FindTrendingConversationRequest request) : base(callContext, request)
		{
			this.request = request;
			OwsLogRegistry.Register(base.GetType().Name, typeof(FindTrendingConversationMetadata), new Type[0]);
		}

		// Token: 0x06004368 RID: 17256 RVA: 0x000E366C File Offset: 0x000E186C
		internal override IExchangeWebMethodResponse GetResponse()
		{
			int? totalConversationsInView = null;
			if (base.Result.Value != null)
			{
				totalConversationsInView = new int?(base.Result.Value.Length);
			}
			return new FindConversationResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value, null, totalConversationsInView, null, false);
		}

		// Token: 0x06004369 RID: 17257 RVA: 0x000E36D8 File Offset: 0x000E18D8
		internal override ServiceResult<ConversationType[]> Execute()
		{
			ServiceCommandBase.ThrowIfNull(this.request.ParentFolderId, "parentFolderId", "FindTrendingConversationCommand::Execute");
			IdAndSession session = null;
			try
			{
				session = base.IdConverter.ConvertFolderIdToIdAndSession(this.request.ParentFolderId.BaseFolderId, IdConverter.ConvertOption.IgnoreChangeKey);
			}
			catch (ObjectNotFoundException innerException)
			{
				throw new ParentFolderNotFoundException(innerException);
			}
			FindTrendingConversation findTrendingConversation = new FindTrendingConversation(session, this.request.PageSize);
			return findTrendingConversation.Execute();
		}

		// Token: 0x04002762 RID: 10082
		private readonly FindTrendingConversationRequest request;
	}
}
