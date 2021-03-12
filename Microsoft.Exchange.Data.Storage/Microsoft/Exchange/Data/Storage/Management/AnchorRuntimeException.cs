using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020001A4 RID: 420
	[Serializable]
	public class AnchorRuntimeException : AnchorLocalizedExceptionBase
	{
		// Token: 0x06001789 RID: 6025 RVA: 0x00070E09 File Offset: 0x0006F009
		public AnchorRuntimeException(LocalizedString localizedErrorMessage, string internalError, Exception ex) : base(localizedErrorMessage, internalError, ex)
		{
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00070E14 File Offset: 0x0006F014
		public AnchorRuntimeException(LocalizedString localizedErrorMessage, string internalError) : base(localizedErrorMessage, internalError)
		{
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x00070E1E File Offset: 0x0006F01E
		public AnchorRuntimeException(LocalizedString localizedErrorMessage, Exception ex) : base(localizedErrorMessage, null, ex)
		{
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00070E29 File Offset: 0x0006F029
		public AnchorRuntimeException(LocalizedString localizedErrorMessage) : base(localizedErrorMessage, null)
		{
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00070E33 File Offset: 0x0006F033
		protected AnchorRuntimeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
