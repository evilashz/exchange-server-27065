using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000DF RID: 223
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ParsingVariablesNotSupported : ParsingException
	{
		// Token: 0x060007F4 RID: 2036 RVA: 0x0001AB9A File Offset: 0x00018D9A
		public ParsingVariablesNotSupported(string invalidQuery, int position) : base(DataStrings.ExceptionVariablesNotSupported(invalidQuery, position))
		{
			this.invalidQuery = invalidQuery;
			this.position = position;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001ABB7 File Offset: 0x00018DB7
		public ParsingVariablesNotSupported(string invalidQuery, int position, Exception innerException) : base(DataStrings.ExceptionVariablesNotSupported(invalidQuery, position), innerException)
		{
			this.invalidQuery = invalidQuery;
			this.position = position;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001ABD8 File Offset: 0x00018DD8
		protected ParsingVariablesNotSupported(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.invalidQuery = (string)info.GetValue("invalidQuery", typeof(string));
			this.position = (int)info.GetValue("position", typeof(int));
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001AC2D File Offset: 0x00018E2D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("invalidQuery", this.invalidQuery);
			info.AddValue("position", this.position);
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0001AC59 File Offset: 0x00018E59
		public string InvalidQuery
		{
			get
			{
				return this.invalidQuery;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0001AC61 File Offset: 0x00018E61
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x0400057E RID: 1406
		private readonly string invalidQuery;

		// Token: 0x0400057F RID: 1407
		private readonly int position;
	}
}
