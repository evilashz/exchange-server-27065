using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000AC RID: 172
	[Serializable]
	internal class InvalidDataResultException : OwaPermanentException
	{
		// Token: 0x060006EE RID: 1774 RVA: 0x00014BE4 File Offset: 0x00012DE4
		public InvalidDataResultException(string errorMessage) : this(errorMessage, null)
		{
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00014BEE File Offset: 0x00012DEE
		public InvalidDataResultException(string errorMessage, Exception innerException) : base(errorMessage, innerException)
		{
		}
	}
}
