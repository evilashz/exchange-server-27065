using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x0200012E RID: 302
	[Serializable]
	internal class GlsPropertyValidationError : ValidationError
	{
		// Token: 0x06000C9B RID: 3227 RVA: 0x00038AEE File Offset: 0x00036CEE
		public GlsPropertyValidationError(LocalizedString description, GlsProperty propertyDefinition, object invalidData) : base(description, propertyDefinition.Name)
		{
			this.invalidData = invalidData;
			this.propertyDefinition = propertyDefinition;
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00038B0B File Offset: 0x00036D0B
		public object InvalidData
		{
			get
			{
				return this.invalidData;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x00038B13 File Offset: 0x00036D13
		public GlsProperty PropertyDefinition
		{
			get
			{
				return this.propertyDefinition;
			}
		}

		// Token: 0x04000683 RID: 1667
		private object invalidData;

		// Token: 0x04000684 RID: 1668
		private GlsProperty propertyDefinition;
	}
}
