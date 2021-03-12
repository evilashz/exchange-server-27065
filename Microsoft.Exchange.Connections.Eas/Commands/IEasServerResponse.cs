using System;

namespace Microsoft.Exchange.Connections.Eas.Commands
{
	// Token: 0x02000015 RID: 21
	public interface IEasServerResponse<T> : IHaveAnHttpStatus where T : struct, IConvertible
	{
		// Token: 0x06000095 RID: 149
		bool IsSucceeded(T status);

		// Token: 0x06000096 RID: 150
		T ConvertStatusToEnum();
	}
}
