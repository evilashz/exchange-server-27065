using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006C5 RID: 1733
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class CorruptDataException : DocumentLibraryException
	{
		// Token: 0x0600457F RID: 17791 RVA: 0x00127BEE File Offset: 0x00125DEE
		internal CorruptDataException(object obj, string message) : this(obj, message, null)
		{
		}

		// Token: 0x06004580 RID: 17792 RVA: 0x00127BF9 File Offset: 0x00125DF9
		internal CorruptDataException(object obj, string message, Exception innerException) : base(message, innerException)
		{
			this.obj = obj;
		}

		// Token: 0x17001425 RID: 5157
		// (get) Token: 0x06004581 RID: 17793 RVA: 0x00127C0A File Offset: 0x00125E0A
		public object CorruptedObject
		{
			get
			{
				return this.obj;
			}
		}

		// Token: 0x04002610 RID: 9744
		private readonly object obj;
	}
}
