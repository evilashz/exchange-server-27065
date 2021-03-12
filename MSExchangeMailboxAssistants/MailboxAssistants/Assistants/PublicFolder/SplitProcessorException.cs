using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000183 RID: 387
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SplitProcessorException : Exception
	{
		// Token: 0x06000F7D RID: 3965 RVA: 0x0005BD24 File Offset: 0x00059F24
		internal SplitProcessorException(string operation, Exception innerException) : base(string.Format("Error in operation {0}", operation), innerException)
		{
			this.isTransient = (innerException is TransientException || (innerException.InnerException != null && innerException.InnerException is TransientException));
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0005BD64 File Offset: 0x00059F64
		protected SplitProcessorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.isTransient = (base.InnerException is TransientException || (base.InnerException.InnerException != null && base.InnerException.InnerException is TransientException));
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x0005BDB2 File Offset: 0x00059FB2
		internal bool IsTransient
		{
			get
			{
				return this.isTransient;
			}
		}

		// Token: 0x040009CB RID: 2507
		private readonly bool isTransient;
	}
}
