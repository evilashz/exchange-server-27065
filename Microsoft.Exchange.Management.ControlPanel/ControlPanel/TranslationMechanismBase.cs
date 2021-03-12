using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006A9 RID: 1705
	public abstract class TranslationMechanismBase : ITranslationMechanism
	{
		// Token: 0x170027C3 RID: 10179
		// (get) Token: 0x060048E8 RID: 18664 RVA: 0x000DF09B File Offset: 0x000DD29B
		// (set) Token: 0x060048E9 RID: 18665 RVA: 0x000DF0A3 File Offset: 0x000DD2A3
		private LocalizedString MessageWithoutDisplayName { get; set; }

		// Token: 0x170027C4 RID: 10180
		// (get) Token: 0x060048EA RID: 18666 RVA: 0x000DF0AC File Offset: 0x000DD2AC
		// (set) Token: 0x060048EB RID: 18667 RVA: 0x000DF0B4 File Offset: 0x000DD2B4
		private bool IsLogging { get; set; }

		// Token: 0x060048EC RID: 18668 RVA: 0x000DF0BD File Offset: 0x000DD2BD
		public TranslationMechanismBase(LocalizedString messageWithoutDisplayName, bool isLogging)
		{
			this.MessageWithoutDisplayName = messageWithoutDisplayName;
			this.IsLogging = isLogging;
		}

		// Token: 0x060048ED RID: 18669 RVA: 0x000DF0D4 File Offset: 0x000DD2D4
		public string Translate(Identity id, Exception ex, string originalMessage)
		{
			string text;
			if (this.HasDisplayName(id))
			{
				text = this.TranslationWithDisplayName(id, originalMessage);
			}
			else
			{
				text = this.MessageWithoutDisplayName;
			}
			if (this.IsLogging)
			{
				EcpEventLogConstants.Tuple_PowershellExceptionTranslated.LogEvent(new object[]
				{
					EcpEventLogExtensions.GetUserNameToLog(),
					ex.GetType(),
					text,
					originalMessage
				});
			}
			return text;
		}

		// Token: 0x060048EE RID: 18670 RVA: 0x000DF137 File Offset: 0x000DD337
		private bool HasDisplayName(Identity id)
		{
			return null != id && !string.IsNullOrEmpty(id.DisplayName) && !id.DisplayName.Equals(id.RawIdentity);
		}

		// Token: 0x060048EF RID: 18671
		protected abstract string TranslationWithDisplayName(Identity id, string originalMessage);
	}
}
