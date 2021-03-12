using System;

namespace Microsoft.Exchange.Autodiscover.Providers
{
	// Token: 0x02000017 RID: 23
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	internal sealed class ProviderAttribute : Attribute
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00005408 File Offset: 0x00003608
		public ProviderAttribute(string guid, string requestSchema, string requestSchemaFile, string responseSchema, string responseSchemaFile)
		{
			this.guid = new Guid(guid);
			if (string.IsNullOrEmpty(requestSchema))
			{
				throw new ArgumentNullException("requestSchema");
			}
			this.requestSchema = requestSchema;
			if (string.IsNullOrEmpty(requestSchemaFile))
			{
				throw new ArgumentNullException("requestSchemaFile");
			}
			this.requestSchemaFile = requestSchemaFile;
			if (string.IsNullOrEmpty(responseSchema))
			{
				throw new ArgumentNullException("responseSchema");
			}
			this.responseSchema = responseSchema;
			if (string.IsNullOrEmpty(responseSchemaFile))
			{
				throw new ArgumentNullException("responseSchemaFile");
			}
			this.responseSchemaFile = responseSchemaFile;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00005493 File Offset: 0x00003693
		public string Guid
		{
			get
			{
				return this.guid.ToString();
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000054A6 File Offset: 0x000036A6
		public string RequestSchema
		{
			get
			{
				return this.requestSchema;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000054AE File Offset: 0x000036AE
		public string ResponseSchema
		{
			get
			{
				return this.responseSchema;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000054B6 File Offset: 0x000036B6
		public string RequestSchemaFile
		{
			get
			{
				return this.requestSchemaFile;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000054BE File Offset: 0x000036BE
		public string ResponseSchemaFile
		{
			get
			{
				return this.responseSchemaFile;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000054C6 File Offset: 0x000036C6
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x000054CE File Offset: 0x000036CE
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x040000F9 RID: 249
		private Guid guid;

		// Token: 0x040000FA RID: 250
		private string requestSchema;

		// Token: 0x040000FB RID: 251
		private string requestSchemaFile;

		// Token: 0x040000FC RID: 252
		private string responseSchema;

		// Token: 0x040000FD RID: 253
		private string responseSchemaFile;

		// Token: 0x040000FE RID: 254
		private string description;
	}
}
