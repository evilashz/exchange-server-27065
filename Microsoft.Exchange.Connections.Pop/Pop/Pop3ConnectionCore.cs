using System;
using System.Collections.Generic;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class Pop3ConnectionCore
	{
		// Token: 0x06000085 RID: 133 RVA: 0x000051EB File Offset: 0x000033EB
		public static AsyncOperationResult<Pop3ResultData> DeleteEmails(Pop3ConnectionContext connectionContext, List<int> messageIds, AsyncCallback callback = null, object callbackState = null)
		{
			return Pop3Client.EndDeleteEmails(Pop3Client.BeginDeleteEmails(connectionContext.Client, messageIds, callback, callbackState, null));
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005201 File Offset: 0x00003401
		public static AsyncOperationResult<Pop3ResultData> GetUniqueIds(Pop3ConnectionContext connectionContext, AsyncCallback callback = null, object callbackState = null)
		{
			return Pop3Client.EndGetUniqueIds(Pop3Client.BeginGetUniqueIds(connectionContext.Client, callback, callbackState, null));
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005216 File Offset: 0x00003416
		public static AsyncOperationResult<Pop3ResultData> GetEmail(Pop3ConnectionContext connectionContext, int messageId, AsyncCallback callback = null, object callbackState = null)
		{
			return Pop3Client.EndGetEmail(Pop3Client.BeginGetEmail(connectionContext.Client, messageId, callback, callbackState, null));
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000522C File Offset: 0x0000342C
		public static AsyncOperationResult<Pop3ResultData> Quit(Pop3ConnectionContext connectionContext, AsyncCallback callback = null, object callbackState = null)
		{
			return Pop3Client.EndQuit(Pop3Client.BeginQuit(connectionContext.Client, callback, callbackState, null));
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005241 File Offset: 0x00003441
		public static AsyncOperationResult<Pop3ResultData> VerifyAccount(Pop3ConnectionContext connectionContext, AsyncCallback callback = null, object callbackState = null)
		{
			return Pop3Client.EndVerifyAccount(Pop3Client.BeginVerifyAccount(connectionContext.Client, callback, callbackState, null));
		}
	}
}
