using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000281 RID: 641
	internal class SuccessfulRegisterNotificationResult : RopResult
	{
		// Token: 0x06000DE2 RID: 3554 RVA: 0x00029F5C File Offset: 0x0002815C
		internal SuccessfulRegisterNotificationResult(IServerObject serverObject) : base(RopId.RegisterNotification, ErrorCode.None, serverObject)
		{
			if (serverObject == null)
			{
				throw new ArgumentNullException("serverObject");
			}
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00029F76 File Offset: 0x00028176
		internal SuccessfulRegisterNotificationResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00029F7F File Offset: 0x0002817F
		internal static SuccessfulRegisterNotificationResult Parse(Reader reader)
		{
			return new SuccessfulRegisterNotificationResult(reader);
		}
	}
}
