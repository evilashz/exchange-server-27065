using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x020002AA RID: 682
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ParsingLexingException : ParsingException
	{
		// Token: 0x060018C5 RID: 6341 RVA: 0x0005C411 File Offset: 0x0005A611
		public ParsingLexingException(string invalidQuery, int position) : base(Strings.ExceptionLexError(invalidQuery, position))
		{
			this.invalidQuery = invalidQuery;
			this.position = position;
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0005C42E File Offset: 0x0005A62E
		public ParsingLexingException(string invalidQuery, int position, Exception innerException) : base(Strings.ExceptionLexError(invalidQuery, position), innerException)
		{
			this.invalidQuery = invalidQuery;
			this.position = position;
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0005C44C File Offset: 0x0005A64C
		protected ParsingLexingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.invalidQuery = (string)info.GetValue("invalidQuery", typeof(string));
			this.position = (int)info.GetValue("position", typeof(int));
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0005C4A1 File Offset: 0x0005A6A1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("invalidQuery", this.invalidQuery);
			info.AddValue("position", this.position);
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x0005C4CD File Offset: 0x0005A6CD
		public string InvalidQuery
		{
			get
			{
				return this.invalidQuery;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x0005C4D5 File Offset: 0x0005A6D5
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x04000982 RID: 2434
		private readonly string invalidQuery;

		// Token: 0x04000983 RID: 2435
		private readonly int position;
	}
}
