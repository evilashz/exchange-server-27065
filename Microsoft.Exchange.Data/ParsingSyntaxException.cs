using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000DD RID: 221
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ParsingSyntaxException : ParsingException
	{
		// Token: 0x060007E7 RID: 2023 RVA: 0x0001A9B5 File Offset: 0x00018BB5
		public ParsingSyntaxException(string invalidQuery, int position) : base(DataStrings.ExceptionParseError(invalidQuery, position))
		{
			this.invalidQuery = invalidQuery;
			this.position = position;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001A9D2 File Offset: 0x00018BD2
		public ParsingSyntaxException(string invalidQuery, int position, Exception innerException) : base(DataStrings.ExceptionParseError(invalidQuery, position), innerException)
		{
			this.invalidQuery = invalidQuery;
			this.position = position;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001A9F0 File Offset: 0x00018BF0
		protected ParsingSyntaxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.invalidQuery = (string)info.GetValue("invalidQuery", typeof(string));
			this.position = (int)info.GetValue("position", typeof(int));
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001AA45 File Offset: 0x00018C45
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("invalidQuery", this.invalidQuery);
			info.AddValue("position", this.position);
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x0001AA71 File Offset: 0x00018C71
		public string InvalidQuery
		{
			get
			{
				return this.invalidQuery;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0001AA79 File Offset: 0x00018C79
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x04000579 RID: 1401
		private readonly string invalidQuery;

		// Token: 0x0400057A RID: 1402
		private readonly int position;
	}
}
