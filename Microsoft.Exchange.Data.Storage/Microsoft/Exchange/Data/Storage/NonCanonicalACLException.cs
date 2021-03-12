using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000752 RID: 1874
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class NonCanonicalACLException : StoragePermanentException
	{
		// Token: 0x06004849 RID: 18505 RVA: 0x00130D67 File Offset: 0x0012EF67
		public NonCanonicalACLException(string canonicalErrorInformation) : base(ServerStrings.NonCanonicalACL(canonicalErrorInformation))
		{
		}

		// Token: 0x0600484A RID: 18506 RVA: 0x00130D75 File Offset: 0x0012EF75
		protected NonCanonicalACLException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
