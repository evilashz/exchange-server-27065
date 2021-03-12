using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x0200010B RID: 267
	[Serializable]
	public class MigrationPermanentException : AnchorLocalizedExceptionBase
	{
		// Token: 0x060013C6 RID: 5062 RVA: 0x00069C23 File Offset: 0x00067E23
		public MigrationPermanentException(LocalizedString localizedErrorMessage, string internalError, Exception ex) : base(localizedErrorMessage, internalError, ex)
		{
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00069C39 File Offset: 0x00067E39
		public MigrationPermanentException(LocalizedString localizedErrorMessage, string internalError) : base(localizedErrorMessage, internalError)
		{
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x00069C4E File Offset: 0x00067E4E
		public MigrationPermanentException(LocalizedString localizedErrorMessage) : base(localizedErrorMessage, null)
		{
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x00069C63 File Offset: 0x00067E63
		public MigrationPermanentException(LocalizedString localizedErrorMessage, Exception ex) : base(localizedErrorMessage, null, ex)
		{
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00069C79 File Offset: 0x00067E79
		protected MigrationPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x00069C8E File Offset: 0x00067E8E
		public override string StackTrace
		{
			get
			{
				if (!string.IsNullOrEmpty(base.StackTrace))
				{
					return base.StackTrace;
				}
				return this.createdStack.ToString();
			}
		}

		// Token: 0x0400099A RID: 2458
		private readonly StackTrace createdStack = new StackTrace();
	}
}
