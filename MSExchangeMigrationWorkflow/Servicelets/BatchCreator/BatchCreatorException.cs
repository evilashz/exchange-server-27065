using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Servicelets.BatchCreator
{
	// Token: 0x0200000C RID: 12
	internal class BatchCreatorException : AnchorLocalizedExceptionBase
	{
		// Token: 0x06000032 RID: 50 RVA: 0x000029ED File Offset: 0x00000BED
		protected BatchCreatorException(string msg) : base(Strings.MigrationGenericError, msg)
		{
		}
	}
}
