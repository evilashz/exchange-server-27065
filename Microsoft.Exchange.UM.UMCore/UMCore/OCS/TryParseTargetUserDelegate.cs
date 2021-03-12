using System;

namespace Microsoft.Exchange.UM.UMCore.OCS
{
	// Token: 0x02000194 RID: 404
	// (Invoke) Token: 0x06000BF5 RID: 3061
	internal delegate bool TryParseTargetUserDelegate(string target, out string userPart, out string hostPart, out bool isPhoneNumber);
}
