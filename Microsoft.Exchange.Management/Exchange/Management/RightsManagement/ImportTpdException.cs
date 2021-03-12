using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000720 RID: 1824
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class ImportTpdException : Exception
	{
		// Token: 0x060040C8 RID: 16584 RVA: 0x0010980C File Offset: 0x00107A0C
		public ImportTpdException()
		{
		}

		// Token: 0x060040C9 RID: 16585 RVA: 0x00109814 File Offset: 0x00107A14
		public ImportTpdException(string message, Exception innerException = null) : base(message, innerException)
		{
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x0010981E File Offset: 0x00107A1E
		protected ImportTpdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
