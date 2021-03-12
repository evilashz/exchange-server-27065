using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000166 RID: 358
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MapiExceptionJetErrorDatabaseBufferDependenciesCorrupted : MapiPermanentException
	{
		// Token: 0x060005AB RID: 1451 RVA: 0x00013EDE File Offset: 0x000120DE
		internal MapiExceptionJetErrorDatabaseBufferDependenciesCorrupted(string message, int hr, int ec, DiagnosticContext context, Exception innerException) : base("MapiExceptionJetErrorDatabaseBufferDependenciesCorrupted", message, hr, ec, context, innerException)
		{
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00013EF2 File Offset: 0x000120F2
		private MapiExceptionJetErrorDatabaseBufferDependenciesCorrupted(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
