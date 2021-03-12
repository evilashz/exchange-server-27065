using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A19 RID: 2585
	[Serializable]
	public class MigrationError
	{
		// Token: 0x06005F1E RID: 24350 RVA: 0x00191902 File Offset: 0x0018FB02
		public MigrationError(MigrationError error)
		{
			this.EmailAddress = error.EmailAddress;
			this.LocalizedErrorMessage = error.LocalizedErrorMessage;
		}

		// Token: 0x06005F1F RID: 24351 RVA: 0x00191922 File Offset: 0x0018FB22
		protected MigrationError()
		{
		}

		// Token: 0x17001A22 RID: 6690
		// (get) Token: 0x06005F20 RID: 24352 RVA: 0x0019192A File Offset: 0x0018FB2A
		// (set) Token: 0x06005F21 RID: 24353 RVA: 0x00191932 File Offset: 0x0018FB32
		public string EmailAddress { get; internal set; }

		// Token: 0x17001A23 RID: 6691
		// (get) Token: 0x06005F22 RID: 24354 RVA: 0x0019193C File Offset: 0x0018FB3C
		// (set) Token: 0x06005F23 RID: 24355 RVA: 0x00191966 File Offset: 0x0018FB66
		public LocalizedString LocalizedErrorMessage
		{
			get
			{
				LocalizedString? localizedString = this.localizedErrorMessage;
				if (localizedString == null)
				{
					return Strings.UnknownMigrationError;
				}
				return localizedString.GetValueOrDefault();
			}
			internal set
			{
				this.localizedErrorMessage = new LocalizedString?(value);
			}
		}

		// Token: 0x06005F24 RID: 24356 RVA: 0x00191974 File Offset: 0x0018FB74
		public override string ToString()
		{
			return ServerStrings.MigrationErrorString(this.EmailAddress, this.LocalizedErrorMessage);
		}

		// Token: 0x040034ED RID: 13549
		private LocalizedString? localizedErrorMessage;
	}
}
