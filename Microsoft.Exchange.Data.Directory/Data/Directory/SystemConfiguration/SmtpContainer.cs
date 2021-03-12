using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005A9 RID: 1449
	[ObjectScope(ConfigScopes.Server)]
	[Serializable]
	public class SmtpContainer : Container
	{
		// Token: 0x170015EB RID: 5611
		// (get) Token: 0x06004309 RID: 17161 RVA: 0x000FC2BC File Offset: 0x000FA4BC
		internal override ADObjectSchema Schema
		{
			get
			{
				return SmtpContainer.schema;
			}
		}

		// Token: 0x170015EC RID: 5612
		// (get) Token: 0x0600430A RID: 17162 RVA: 0x000FC2C3 File Offset: 0x000FA4C3
		internal override string MostDerivedObjectClass
		{
			get
			{
				return SmtpContainer.mostDerivedClass;
			}
		}

		// Token: 0x04002D85 RID: 11653
		private static SmtpContainerSchema schema = ObjectSchema.GetInstance<SmtpContainerSchema>();

		// Token: 0x04002D86 RID: 11654
		private static string mostDerivedClass = "msExchProtocolCfgSMTPContainer";
	}
}
