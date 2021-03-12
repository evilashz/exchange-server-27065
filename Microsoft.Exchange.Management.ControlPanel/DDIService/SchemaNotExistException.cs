using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200015C RID: 348
	public class SchemaNotExistException : Exception
	{
		// Token: 0x060021B3 RID: 8627 RVA: 0x0006565D File Offset: 0x0006385D
		public SchemaNotExistException(string schema)
		{
			this.schema = schema;
		}

		// Token: 0x17001A7B RID: 6779
		// (get) Token: 0x060021B4 RID: 8628 RVA: 0x0006566C File Offset: 0x0006386C
		public string Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x04001D3D RID: 7485
		private readonly string schema;
	}
}
