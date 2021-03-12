using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x020002AB RID: 683
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ParsingParameterRangeException : ParsingException
	{
		// Token: 0x060018CB RID: 6347 RVA: 0x0005C4DD File Offset: 0x0005A6DD
		public ParsingParameterRangeException(string invalidQuery, int position) : base(Strings.ExceptionParameterRange(invalidQuery, position))
		{
			this.invalidQuery = invalidQuery;
			this.position = position;
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x0005C4FA File Offset: 0x0005A6FA
		public ParsingParameterRangeException(string invalidQuery, int position, Exception innerException) : base(Strings.ExceptionParameterRange(invalidQuery, position), innerException)
		{
			this.invalidQuery = invalidQuery;
			this.position = position;
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0005C518 File Offset: 0x0005A718
		protected ParsingParameterRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.invalidQuery = (string)info.GetValue("invalidQuery", typeof(string));
			this.position = (int)info.GetValue("position", typeof(int));
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x0005C56D File Offset: 0x0005A76D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("invalidQuery", this.invalidQuery);
			info.AddValue("position", this.position);
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x0005C599 File Offset: 0x0005A799
		public string InvalidQuery
		{
			get
			{
				return this.invalidQuery;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0005C5A1 File Offset: 0x0005A7A1
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x04000984 RID: 2436
		private readonly string invalidQuery;

		// Token: 0x04000985 RID: 2437
		private readonly int position;
	}
}
