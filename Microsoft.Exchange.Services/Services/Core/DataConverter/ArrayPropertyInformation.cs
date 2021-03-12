using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000206 RID: 518
	internal class ArrayPropertyInformation : PropertyInformation
	{
		// Token: 0x06000D8C RID: 3468 RVA: 0x00043B57 File Offset: 0x00041D57
		public ArrayPropertyInformation(string arrayLocalName, ExchangeVersion effectiveVersion, string arrayItemLocalName, PropertyDefinition propertyDefinition, PropertyPath propertyPath, PropertyCommand.CreatePropertyCommand createCommand) : base(arrayLocalName, effectiveVersion, propertyDefinition, propertyPath, createCommand)
		{
			this.arrayItemLocalName = arrayItemLocalName;
			this.arrayLocalName = arrayLocalName;
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x00043B75 File Offset: 0x00041D75
		public string ArrayItemLocalName
		{
			get
			{
				return this.arrayItemLocalName;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00043B7D File Offset: 0x00041D7D
		public string ArrayLocalName
		{
			get
			{
				return this.arrayLocalName;
			}
		}

		// Token: 0x04000AA0 RID: 2720
		private readonly string arrayLocalName;

		// Token: 0x04000AA1 RID: 2721
		private string arrayItemLocalName;
	}
}
