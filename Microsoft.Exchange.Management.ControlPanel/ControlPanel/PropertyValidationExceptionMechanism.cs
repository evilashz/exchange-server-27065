using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006C0 RID: 1728
	internal class PropertyValidationExceptionMechanism : ITranslationMechanism
	{
		// Token: 0x17002807 RID: 10247
		// (get) Token: 0x060049B6 RID: 18870 RVA: 0x000E0B84 File Offset: 0x000DED84
		// (set) Token: 0x060049B7 RID: 18871 RVA: 0x000E0B8C File Offset: 0x000DED8C
		private bool IsLogging { get; set; }

		// Token: 0x060049B8 RID: 18872 RVA: 0x000E0B95 File Offset: 0x000DED95
		public PropertyValidationExceptionMechanism(bool isLogging)
		{
			this.IsLogging = isLogging;
		}

		// Token: 0x060049B9 RID: 18873 RVA: 0x000E0BA4 File Offset: 0x000DEDA4
		public string Translate(Identity id, Exception ex, string originalMessage)
		{
			string text = null;
			PropertyValidationException ex2 = ex as PropertyValidationException;
			if (ex2 != null)
			{
				StringBuilder stringBuilder = new StringBuilder(128);
				foreach (PropertyValidationError propertyValidationError in ex2.PropertyValidationErrors)
				{
					stringBuilder.Append(propertyValidationError.Description);
					stringBuilder.Append(" ");
				}
				text = stringBuilder.ToString();
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
	}
}
