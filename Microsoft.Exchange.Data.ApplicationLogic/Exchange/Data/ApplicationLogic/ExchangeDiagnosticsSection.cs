using System;
using System.Configuration;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x020000ED RID: 237
	public class ExchangeDiagnosticsSection : ConfigurationSection
	{
		// Token: 0x060009D6 RID: 2518 RVA: 0x000268B8 File Offset: 0x00024AB8
		public static ExchangeDiagnosticsSection GetConfig()
		{
			return ((ExchangeDiagnosticsSection)ConfigurationManager.GetSection("ExchangeDiagnosticsSection")) ?? new ExchangeDiagnosticsSection();
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x000268D4 File Offset: 0x00024AD4
		[ConfigurationProperty("DiagnosticsComponents")]
		public DiagnosticsComponents DiagnosticComponents
		{
			get
			{
				object obj = base["DiagnosticsComponents"];
				return obj as DiagnosticsComponents;
			}
		}
	}
}
