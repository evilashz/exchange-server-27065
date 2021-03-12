using System;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000011 RID: 17
	public abstract class DiagnosticQueryStringFormatter : DiagnosticQueryFormatter<StringBuilder>
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00004DF9 File Offset: 0x00002FF9
		protected DiagnosticQueryStringFormatter(DiagnosticQueryResults results) : base(results)
		{
			this.builder = new StringBuilder(4096);
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00004E12 File Offset: 0x00003012
		protected StringBuilder Builder
		{
			get
			{
				return this.builder;
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004E1A File Offset: 0x0000301A
		public override StringBuilder FormatResults()
		{
			this.WriteHeader();
			this.WriteValues();
			return this.builder;
		}

		// Token: 0x06000097 RID: 151
		protected abstract void WriteHeader();

		// Token: 0x06000098 RID: 152
		protected abstract void WriteValues();

		// Token: 0x04000088 RID: 136
		private readonly StringBuilder builder;
	}
}
