using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006AE RID: 1710
	public class NewStringMechanism : TranslationMechanismBase
	{
		// Token: 0x170027C9 RID: 10185
		// (get) Token: 0x06004907 RID: 18695 RVA: 0x000DF5EB File Offset: 0x000DD7EB
		// (set) Token: 0x06004908 RID: 18696 RVA: 0x000DF5F3 File Offset: 0x000DD7F3
		private LocalizedString MessageWithDisplayName { get; set; }

		// Token: 0x06004909 RID: 18697 RVA: 0x000DF5FC File Offset: 0x000DD7FC
		public NewStringMechanism(LocalizedString messageWithDisplayName, LocalizedString messageWithoutDisplayName, bool isLogging) : base(messageWithoutDisplayName, isLogging)
		{
			this.MessageWithDisplayName = messageWithDisplayName;
		}

		// Token: 0x0600490A RID: 18698 RVA: 0x000DF60D File Offset: 0x000DD80D
		public NewStringMechanism(LocalizedString newMessage, bool isLogging) : this(newMessage, newMessage, isLogging)
		{
		}

		// Token: 0x0600490B RID: 18699 RVA: 0x000DF618 File Offset: 0x000DD818
		public NewStringMechanism(LocalizedString newMessage) : this(newMessage, newMessage, true)
		{
		}

		// Token: 0x0600490C RID: 18700 RVA: 0x000DF623 File Offset: 0x000DD823
		protected override string TranslationWithDisplayName(Identity id, string originalMessage)
		{
			return string.Format(this.MessageWithDisplayName, id.DisplayName);
		}
	}
}
