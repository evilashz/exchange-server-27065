using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001C3 RID: 451
	[Serializable]
	public class OwaAccessDeniedException : OwaPermanentException
	{
		// Token: 0x06000F26 RID: 3878 RVA: 0x0005E8B5 File Offset: 0x0005CAB5
		public OwaAccessDeniedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0005E8BF File Offset: 0x0005CABF
		public OwaAccessDeniedException(string message) : base(message)
		{
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0005E8C8 File Offset: 0x0005CAC8
		public OwaAccessDeniedException(string message, bool isWebPartFailure) : base(message)
		{
			this.isWebPartFailure = isWebPartFailure;
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x0005E8D8 File Offset: 0x0005CAD8
		public bool IsWebPartFailure
		{
			get
			{
				return this.isWebPartFailure;
			}
		}

		// Token: 0x04000A29 RID: 2601
		private bool isWebPartFailure;
	}
}
