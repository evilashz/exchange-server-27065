using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000DE RID: 222
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ParsingInvalidFilterOperator : ParsingException
	{
		// Token: 0x060007ED RID: 2029 RVA: 0x0001AA81 File Offset: 0x00018C81
		public ParsingInvalidFilterOperator(string op, string invalidQuery, int position) : base(DataStrings.ExceptionInvalidFilterOperator(op, invalidQuery, position))
		{
			this.op = op;
			this.invalidQuery = invalidQuery;
			this.position = position;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0001AAA6 File Offset: 0x00018CA6
		public ParsingInvalidFilterOperator(string op, string invalidQuery, int position, Exception innerException) : base(DataStrings.ExceptionInvalidFilterOperator(op, invalidQuery, position), innerException)
		{
			this.op = op;
			this.invalidQuery = invalidQuery;
			this.position = position;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0001AAD0 File Offset: 0x00018CD0
		protected ParsingInvalidFilterOperator(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.op = (string)info.GetValue("op", typeof(string));
			this.invalidQuery = (string)info.GetValue("invalidQuery", typeof(string));
			this.position = (int)info.GetValue("position", typeof(int));
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001AB45 File Offset: 0x00018D45
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("op", this.op);
			info.AddValue("invalidQuery", this.invalidQuery);
			info.AddValue("position", this.position);
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x0001AB82 File Offset: 0x00018D82
		public string Op
		{
			get
			{
				return this.op;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0001AB8A File Offset: 0x00018D8A
		public string InvalidQuery
		{
			get
			{
				return this.invalidQuery;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x0001AB92 File Offset: 0x00018D92
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x0400057B RID: 1403
		private readonly string op;

		// Token: 0x0400057C RID: 1404
		private readonly string invalidQuery;

		// Token: 0x0400057D RID: 1405
		private readonly int position;
	}
}
