using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.Cafe
{
	// Token: 0x020000B2 RID: 178
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class BackEndLocatorException : ServerNotFoundException
	{
		// Token: 0x06000796 RID: 1942 RVA: 0x0001DC50 File Offset: 0x0001BE50
		public BackEndLocatorException(Exception innerException) : base(innerException.Message, innerException)
		{
		}
	}
}
